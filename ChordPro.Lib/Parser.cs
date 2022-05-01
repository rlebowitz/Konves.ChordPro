using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ChordPro.Tests")]
namespace ChordPro.Library
{
    internal sealed class Parser
    {
        private TextReader TextReader { get; }
        private IReadOnlyDictionary<string, DirectiveHandler> DirectiveParsers { get; }
        internal bool IsInTab { get; set; } = false;
        private int LineNumber { get; set; } = 1;

        internal Parser(TextReader textReader) : this(textReader, null)
        {
        }

        internal Parser(TextReader textReader, IEnumerable<DirectiveHandler> customHandlers)
        {
            TextReader = textReader;
            DirectiveParsers = DirectiveHandlerUtility.GetHandlersDictionaryByName(customHandlers);
        }

        internal IEnumerable<ILine> Parse()
        {
            string line;
            while ((line = TextReader.ReadLine()) != null)
            {
                if (IsInTab)
                {
                    if (GetLineType(line) == LineType.Directive && ParseDirective(line) is EndOfTabDirective)
                        yield return new EndOfTabDirective();
                    else
                        yield return new TabLine(line);
                }
                else
                {
                    switch (GetLineType(line))
                    {
                        case LineType.Comment:
                            // Do nothing
                            break;
                        case LineType.Directive:
                            yield return ParseDirective(line);
                            break;
                        case LineType.Text:
                            yield return ParseSongLine(line);
                            break;
                        case LineType.Whitespace:
                            yield return new SongLine();
                            break;
                    }
                }

                LineNumber++;
            }
        }

        internal Directive ParseDirective(string line)
        {
            if (DirectiveComponents.TryParse(line, out DirectiveComponents components))
            {
                if (components != null)
                {
                    if (DirectiveParsers.TryGetValue(components.Key, out DirectiveHandler handler) && handler.TryParse(components, out Directive directive))
                    {
                        if (directive is StartOfTabDirective)
                            IsInTab = true;

                        if (directive is EndOfTabDirective)
                            IsInTab = false;

                        return directive;
                    }
                }
            }
            throw new FormatException($"Invalid directive at line {LineNumber}.");
        }

        internal SongLine ParseSongLine(string line)
        {
            var blocks = SplitIntoBlocks(line);
            return new SongLine(blocks.Select(ParseBlock));
        }

        //internal static IEnumerable<string> SplitIntoBlocks(string line)
        //{
        //    Guard.NotNull(line);

        //    int start = 0;
        //    bool isInBlock = false;
        //    bool isInChord = false;
        //    for (int i = 0; i < line.Length; i++)
        //    {
        //        if (isInBlock && !isInChord)
        //        {
        //            if (char.IsWhiteSpace(line[i]))
        //            {
        //                yield return line[start..i];
        //                isInBlock = false;
        //                continue;
        //            }
        //            else if (i == line.Length - 1)
        //            {
        //                yield return line.Substring(start, i - start + 1);
        //                isInBlock = false;
        //                continue;
        //            }
        //        }

        //        if (!isInBlock && !char.IsWhiteSpace(line[i]))
        //        {
        //            isInBlock = true;
        //            start = i;
        //        }

        //        if (!isInChord && line[i] == '[')
        //        {
        //            isInChord = true;
        //        }

        //        if (isInChord && line[i] == ']')
        //        {
        //            isInChord = false;
        //        }
        //    }
        //}

        internal IEnumerable<string> SplitIntoBlocks(string line)
        {
            Guard.NotNull(line);

            int start = 0;
            bool isInBlock = false;
            bool isInChord = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (isInBlock && !isInChord)
                {
                    if (char.IsWhiteSpace(line[i]))
                    {
                        yield return line.Substring(start, i - start);
                        isInBlock = false;
                        continue;
                    }
                    else if (i == line.Length - 1)
                    {
                        yield return line.Substring(start, i - start + 1);
                        isInBlock = false;
                        continue;
                    }
                }

                if (!isInBlock && !char.IsWhiteSpace(line[i]))
                {
                    isInBlock = true;
                    start = i;
                }

                if (!isInChord && line[i] == '[')
                {
                    isInChord = true;
                }

                if (isInChord && line[i] == ']')
                {
                    isInChord = false;
                }
            }
        }

        internal static IEnumerable<string> SplitIntoSyllables(string block)
        {
            int start = 0;
            bool isInChord = false;
            for (int i = 0; i < block.Length; i++)
            {

                if (!isInChord && block[i] == '[')
                {
                    if (i > 0)
                    {
                        yield return block.Substring(start, i - start);
                        start = i;
                    }

                    isInChord = true;
                }

                if (isInChord && block[i] == ']')
                {
                    isInChord = false;
                }

                if (i == block.Length - 1)
                    yield return block.Substring(start, i - start + 1);
            }
        }

        internal Block ParseBlock(string block)
        {
            List<string> syllables = SplitIntoSyllables(block).ToList();

            Chord chord;

            if (syllables.Count == 1 && TryParseChord(syllables.Single(), out chord))
                return chord;

            return new Word(syllables.Select(ParseSyllable));
        }

        internal static bool TryParseChord(string s, out Chord chord)
        {
            if (s.StartsWith("[") && s.EndsWith("]") && s.Length > 2)
            {
                chord = new Chord(s.Substring(1, s.Length - 2).Trim());
                return true;
            }

            chord = null;
            return false;
        }

        internal Syllable ParseSyllable(string syllable)
        {
            int i = syllable.IndexOf("]");

            Chord chord = null;

            if (i > -1)
            {
                if (!TryParseChord(syllable[..(i + 1)], out chord))
                    throw new FormatException($"Incorrect chord format at line {LineNumber}.");
            }

            string text = syllable.Substring(i + 1);

            return new Syllable(chord, text.Length == 0 ? null : text);
        }

        internal static LineType GetLineType(string line)
        {
            Guard.NotNull(line);

            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '#':
                        return LineType.Comment;
                    case '{':
                        return LineType.Directive;
                    default:
                        if (!char.IsWhiteSpace(line[i]))
                            return LineType.Text;
                        break;
                }
            }

            return LineType.Whitespace;
        }

        internal enum LineType
        {
            Comment,
            Text,
            Directive,
            Whitespace
        }


    }
}

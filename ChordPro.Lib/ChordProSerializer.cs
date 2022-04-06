using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;

namespace ChordPro.Lib
{
    public static class ChordProSerializer
    {
        public static Document Deserialize(Stream stream)
        {
            return Deserialize(stream, null);
        }

        public static Document Deserialize(Stream stream, IEnumerable<DirectiveHandler> customHandlers)
        {
            using StreamReader reader = new(stream);
            Parser parser = new(reader);
            return new Document(parser.Parse());
        }

        public static void Serialize(Document document, TextWriter writer, SerializerSettings settings = null)
        {
            Guard.NotNull(document);
            Guard.NotNull(writer);
            var index = DirectiveHandlerUtility.GetHandlersDictionaryByType(settings?.CustomHandlers);

            foreach (ILine line in document.Lines)
            {
                if (line == null)
                {
                    continue;
                }
                else if (line is Directive directive) { 
                    writer.WriteLine(index[line.GetType()].GetString(directive, settings?.ShortenDirectives == true)); 
                }
                else if (line is SongLine songLine)
                {
                    Write(writer, songLine);
                }
                else if (line is TabLine tabLine) { 
                    writer.WriteLine(tabLine.Text);
                }
                else
                    throw new ArgumentException("unknown line type");
            }
        }

        internal static void Write(TextWriter writer, SongLine line)
        {
            bool addSpace = false;

            foreach (Block block in line.Blocks)
            {
                if (addSpace)
                    writer.Write(' ');

                if (block is Word word)
                {
                    foreach (Syllable syllable in word.Syllables)
                    {
                        if (syllable.Chord != null) { 
                            writer.Write($"[{syllable.Chord.Text}]");
                        }
                        writer.Write(syllable.Text);
                    }
                }
                else if (block is Chord chord)
                {
                    writer.Write($"[{chord.Text}]");
                }

                addSpace = true;
            }

            writer.WriteLine();
        }
    }
}

using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;

namespace ChordPro.Library
{
    public static class ChordProSerializer
    {
        /// <summary>
        /// Deserializes ChordPro formatted text.
        /// </summary>
        /// <param name="stream">The specified text stream.</param>
        /// <returns>A ChordPro Document.</returns>
        /// <exception cref="FormatException">Throws if a parser error occurs.</exception>
        public static Document Deserialize(Stream stream)
        {
            return Deserialize(stream, null);
        }

        /// <summary>
        /// Deserializes ChordPro formatted text.
        /// </summary>
        /// <param name="stream">The specified text stream.</param>
        /// <returns>A ChordPro Document.</returns>
        /// <exception cref="FormatException">Throws if a parser error occurs.</exception>
        public static Document Deserialize(Stream stream, IEnumerable<DirectiveHandler> customHandlers)
        {
            using StreamReader reader = new(stream);
            Parser parser = new(reader);
            return new Document(parser.Parse());
        }

        /// <summary>
        /// Deserializes ChordPro formatted text.
        /// </summary>
        /// <param name="reader">The specified text reader.</param>
        /// <returns>A ChordPro Document.</returns>
        /// <exception cref="FormatException">Throws if a parser error occurs.</exception>
        public static Document Deserialize(TextReader reader)
        {
            Guard.NotNull(reader);
            Parser parser = new Parser(reader);
            return new Document(parser.Parse());
        }

        public static void Serialize(Document document, TextWriter writer, SerializerSettings settings = null)
        {
            Guard.NotNull(document);
            Guard.NotNull(writer);
            var index = DirectiveHandlerUtility.GetHandlersDictionaryByType(settings?.CustomHandlers);

            foreach (ILine line in document.Lines)
            {
                switch (line)
                {
                    case null:
                        continue;
                    case Directive directive:
                        var type = line.GetType();
                        writer.WriteLine(index[type].GetString(directive, settings?.ShortenDirectives == true));
                        break;
                    case SongLine songLine:
                        Write(writer, songLine);
                        break;
                    case TabLine tabLine:
                        writer.WriteLine(tabLine.Text);
                        break;
                    default:
                        throw new ArgumentException("Unknown line type");
                }
            }
        }

        internal static void Write(TextWriter writer, SongLine line)
        {
            bool addSpace = false;

            foreach (Block block in line.Blocks)
            {
                if (addSpace)
                    writer.Write(' ');

                switch (block)
                {
                    case Word word:
                        {
                            foreach (Syllable syllable in word.Syllables)
                            {
                                if (syllable.Chord != null)
                                {
                                    writer.Write($"[{syllable.Chord.Text}]");
                                }
                                writer.Write(syllable.Text);
                            }

                            break;
                        }

                    case Chord chord:
                        writer.Write($"[{chord.Text}]");
                        break;
                }

                addSpace = true;
            }

            writer.WriteLine();
        }
    }
}

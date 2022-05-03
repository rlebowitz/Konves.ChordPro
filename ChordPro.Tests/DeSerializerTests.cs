using ChordPro.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace ChordPro.Tests
{
    public class DeSerializerTests
    {
        [Fact]
        public void SongLineTest()
        {
            // Arrange
            string[] lines = {
                "{c: }".Trim(),
                "[Dm][G7][C][Am][F][Dm][Bb][G7]".Trim(),
                "[Dm][G7][C][Am][F][G7][C] (PAUSE 4)".Trim()
            };

            string text = string.Join(Environment.NewLine, lines);
            List<ILine> result;
            using (TextReader reader = new StringReader(text))
            {
                var parser = new Parser(reader);
                // Act
                result = parser.Parse().ToList();
            }
            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            var document = new Document(result);

            StringBuilder sb = new();
            TextWriter writer = new StringWriter(sb);

            ChordProSerializer.Serialize(document, writer, new SerializerSettings { ShortenDirectives = true});
            string output = sb.ToString().Trim();

            Assert.NotNull(output);
            Assert.Equal(text, output);
        }




    }
}


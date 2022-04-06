using ChordPro.Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace ChordPro.Tests
{
    public class ParserTests
    {
        private const string resourceName = "ChordPro.Tests.Data.swing-low.cho";

        [Fact]
        public void TestParser()
        {
            // Arrange
            List<ILine> result;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (TextReader reader = new StreamReader(stream))
            {
               var parser = new Parser(reader);
                // Act
                result = parser.Parse().ToList();
            }
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeserializeTest()
        {
            // Arrange
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            // Act
            Document result = ChordProSerializer.Deserialize(stream);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SerializeTest()
        {
            // Arrange
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                Document doc = ChordProSerializer.Deserialize(stream);
                StringBuilder sb = new();
                TextWriter writer = new StringWriter(sb);

                // Act
                ChordProSerializer.Serialize(doc, writer);
                string result = sb.ToString();

                // Assert
                Assert.NotNull(result);
            }
        }
    }
}

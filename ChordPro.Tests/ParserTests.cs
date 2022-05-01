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
        private const string resourceName = "ChordPro.Tests.Data.Abracadabra.pro";

        private string[] resources = new string[]
        {
            "ChordPro.Tests.Data.Abracadabra.pro",
            "ChordPro.Tests.Data.All-Shook-Up.pro",
            "ChordPro.Tests.Data.All-My-LovingEight-Days-a-Week-C.pro",
            "ChordPro.Tests.Data.All-I-Want-To-Do-Is-Make-Love-To-You.pro",
            "ChordPro.Tests.Data.Ambitions.pro",
            "ChordPro.Tests.Data.Addams-Family-Theme-The.pro",
            "ChordPro.Tests.Data.After-The-Goldrush.pro"
        };
        public ParserTests()
        {
        }

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

        [Theory]
        [InlineData("ChordPro.Tests.Data.Abracadabra.pro")]
        [InlineData("ChordPro.Tests.Data.All-Shook-Up.pro")]
        [InlineData("ChordPro.Tests.Data.All-My-LovingEight-Days-a-Week-C.pro")]
        [InlineData("ChordPro.Tests.Data.All-I-Want-To-Do-Is-Make-Love-To-You.pro")]
        [InlineData("ChordPro.Tests.Data.Ambitions.pro")]
        [InlineData("ChordPro.Tests.Data.Addams-Family-Theme-The.pro")]
        [InlineData("ChordPro.Tests.Data.After-The-Goldrush.pro")]
        public void MultipleDeserializeTests(string fileName)
        {
            // Arrange
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
            Document doc = ChordProSerializer.Deserialize(stream);
            StringBuilder sb = new();
            TextWriter writer = new StringWriter(sb);

            // Act
            ChordProSerializer.Serialize(doc, writer);
            string result = sb.ToString();

            // Assert
            Assert.NotNull(result);
        }
  

        [Fact]
        public void SerializeTest()
        {
            // Arrange
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
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

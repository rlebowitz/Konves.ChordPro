using ChordPro.Library;
using ChordPro.Library.Directives;
using ChordPro.Library.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ChordPro.Tests
{
    public class ParserTestFixture

    {
        [Fact]
        public void ParseSongLineTest()
        {
            // Arrange
            Parser parser = new(null);
            string line = "asdf [X]asdf asdf";
            int expectedBlockCount = 3;

            // Act
            SongLine result = parser.ParseSongLine(line);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Blocks);
            Assert.Equal(expectedBlockCount, result.Blocks.Count);
        }

        [Theory]
        [InlineData("{directive}", LineType.Directive)]
        [InlineData("    {directive}", LineType.Directive)]
        [InlineData("# Comment", LineType.Comment)]
        [InlineData("    # Comment", LineType.Comment)]
        [InlineData("song line", LineType.Text)]
        [InlineData("    song line", LineType.Text)]
        [InlineData("", LineType.Whitespace)]
        [InlineData("    ", LineType.Whitespace)]
        public void GetLineTypeTest(string line, LineType expected)
        {
            LineType result = Parser.GetLineType(line);
            // Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetLineTypeTest_Null()
        {
            // Arrange
            string s = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => Parser.GetLineType(s));
        }

        [Theory]
        [InlineData("asdf [X]asdf asdf", "asdf", "[X]asdf", "asdf")]
        [InlineData("asdf asdf[X] asdf", "asdf", "asdf[X]", "asdf")]
        [InlineData("asdf [x]asdf[x] asdf", "asdf", "[x]asdf[x]", "asdf")]
        [InlineData("asdf asdf[x]asdf asdf", "asdf", "asdf[x]asdf", "asdf")]
        [InlineData("asdf [x][x]asdf[x][x] asdf", "asdf", "[x][x]asdf[x][x]", "asdf")]
        [InlineData("asdf asdf[x][x]asdf asdf", "asdf", "asdf[x][x]asdf", "asdf")]
        [InlineData(" [X] ", "[X]")]
        [InlineData(" asdf ", "asdf")]
        [InlineData(" [X]asdf ", "[X]asdf")]
        [InlineData(" asdf[X] ", "asdf[X]")]
        [InlineData(" [x]asdf[x] ", "[x]asdf[x]")]
        [InlineData(" asdf[x]asdf ", "asdf[x]asdf")]
        [InlineData("asdf [ X ]asdf asdf", "asdf", "[X]asdf", "asdf")]
        [InlineData("asdf asdf[ X ] asdf", "asdf", "asdf[X]", "asdf")]
        [InlineData("asdf [ x ]asdf[ x ] asdf", "asdf", "[x]asdf[x]", "asdf")]
        [InlineData("asdf asdf[ x ]asdf asdf", "asdf", "asdf[x]asdf", "asdf")]
        [InlineData("asdf [ x ][ x ]asdf[ x ][ x ] asdf", "asdf", "[x][x]asdf[x][x]", "asdf")]
        [InlineData("asdf asdf[ x ][ x ]asdf asdf", "asdf", "asdf[x][x]asdf", "asdf")]
        public void DoSplitIntoBlocksTest(string line, params string[] expectedBlocks)
        {
            var result = Parser.SplitIntoBlocks(line).ToArray();
            Assert.Equal(expectedBlocks, result);
        }

       

        [Theory]
        [InlineData("[x]", "[x]")]
        [InlineData("asdf", "asdf")]
        [InlineData("[x]asdf", "[x]asdf")]
        [InlineData("asdf[x]", "asdf", "[x]")]
        [InlineData("[x]asdf[x]", "[x]asdf", "[x]")]
        [InlineData("asdf[x]asdf", "asdf", "[x]asdf")]
        [InlineData("asdf[x][x]asdf", "asdf", "[x]", "[x]asdf")]
        [InlineData("[x][x]asdf", "[x]", "[x]asdf")]
        public void SplitIntoSyllablesTest(string block, params string[] expectedSyllables)
        {
            List<string> result = Parser.SplitIntoSyllables(block).ToList();
            Assert.Equal(expectedSyllables, result);
        }

        [Theory]
        [ClassData(typeof(ParseChordData))]
        public void DoTryParseChordTest(string s, bool expectedResult, Chord expectedChord)
        {
            // Act
            bool result = Parser.TryParseChord(s, out Chord chord);
            // Assert
            Assert.Equal(expectedResult, result);
            if (expectedChord is null)
            {
                Assert.Null(chord);
            }
            Assert.Equal(expectedChord?.Text, chord?.Text);
        }

        private class ParseChordData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "[X]", true, new Chord("X") };
                yield return new object[] { "[ X ]", true, new Chord("X") };
                yield return new object[] { "X", false, null };
                yield return new object[] { "[]", false, null };
                yield return new object[] { "[X", false, null };
                yield return new object[] { "X]", false, null };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void ParseSyllableTest()
        {
            DoParseSyllableTest("[X]", new Syllable(new Chord("X"), null));
            DoParseSyllableTest("[X]asdf", new Syllable(new Chord("X"), "asdf"));
            DoParseSyllableTest("asdf", new Syllable(null, "asdf"));
        }

        [Fact]
        public void ParseSyllableTest_Exception()
        {
            // Arrange
            var parser = new Parser(null);
            string syllable = "[]";
            // Act
            Assert.Throws<FormatException>(() => parser.ParseSyllable(syllable));
        }

        [Theory]
        [ClassData(typeof(ParseSyllableData))]
        private void DoParseSyllableTest(string s, Syllable expectedSyllable)
        {
            // Arrange
            Parser parser = new Parser(null);
            // Act
            Syllable result = parser.ParseSyllable(s);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSyllable?.Chord?.Text, result?.Chord?.Text);
            Assert.Equal(expectedSyllable?.Text, result?.Text);
        }

        private class ParseSyllableData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "[X]", new Syllable(new Chord("X"), null) };
                yield return new object[] { "[X]asdf", new Syllable(new Chord("X"), "asdf") };
                yield return new object[] { "asdf", new Syllable(null, "asdf") };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class ParseBlockData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "[x]", new Chord("x") };
                yield return new object[] { "asdf", new Word(new[] { new Syllable(null, "asdf") }) };
                yield return new object[] { "[x]asdf", new Word(new[] { new Syllable(new Chord("x"), "asdf") }) };
                yield return new object[] { "asdf[x]", new Word(new[] { new Syllable(null, "asdf"), new Syllable(new Chord("x"), null) }) };
                yield return new object[] { "[x]asdf[x]", new Word(new[] {
                    new Syllable(new Chord("x"), "asdf"),
                    new Syllable(new Chord("x"), null)})};
                yield return new object[] { "asdf[x]asdf", new Word(new[] {
                    new Syllable(null, "asdf"),
                    new Syllable(new Chord("x"), "asdf")})};
                yield return new object[] {"asdf[x][x]asdf", new Word(new[] {
                    new Syllable(null, "asdf"),
                    new Syllable(new Chord("x"), null),
                    new Syllable(new Chord("x"), "asdf")})};
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ParseBlockData))]
        public void DoParseBlockTest(string s, Block expected)
        {
            // Arrange
            var parser = new Parser(null);

            // Act
            Block result = parser.ParseBlock(s);

            // Assert
            Assert.NotNull(result);
            if (expected is Chord)
            {
                Assert.IsType<Chord>(result);

                Chord resultChord = result as Chord;
                Chord expectedChord = expected as Chord;

                Assert.Equal(expectedChord.Text, resultChord.Text);
            }
            else if (expected is Word)
            {
                Assert.IsType<Word>(result);

                Word resultWord = result as Word;
                Word expectedWord = expected as Word;

                Assert.Equal(expectedWord.Syllables.Count, resultWord.Syllables.Count);
            }
        }

        [Fact]
        public void ParseDirectiveTest_NotADirective()
        {
            // Arrange
            string line = "{some invalid format";
            var parser = new Parser(null);
            // Act
            Assert.Throws<FormatException>(() => parser.ParseDirective(line));
        }

        [Fact]
        public void ParseDirectiveTest_UnknownDirective()
        {
            // Arrange
            string line = "{unknowndirective}";
            var parser = new Parser(null);

            // Act
            Assert.Throws<FormatException>(() => parser.ParseDirective(line));
        }

        [Fact]
        public void ParseDirectiveTest_InvalidFormat()
        {
            // Arrange
            string line = "{title}"; // Missing value
            var parser = new Parser(null);

            // Act
            Assert.Throws<FormatException>(() => parser.ParseDirective(line));
        }

        [Fact]
        public void ParseDirectiveTest_StartOfTab()
        {
            // Arrange
            string line = "{start_of_tab}";
            bool expectedIsInTab = true;
            var parser = new Parser(null);
            // Act
            Directive result = parser.ParseDirective(line);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedIsInTab, parser.IsInTab);
        }

        [Fact]
        public void ParseDirectiveTest_EndOfTab()
        {
            // Arrange
            string line = "{end_of_tab}";
            bool expectedIsInTab = false;
            var parser = new Parser(null);
            // Act
            Directive result = parser.ParseDirective(line);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedIsInTab, parser.IsInTab);
        }

        [Fact]
        public void ParseTest()
        {
            // Arrange
            string song = @"# Comment
{sot}
   tab line
{eot}

Song Line preceded by whitespace";
            int expectedLineCount = 5;
            TextReader reader = new StringReader(song);
            var parser = new Parser(reader);
            // Act
            List<ILine> result = parser.Parse().ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLineCount, result.Count);
            Assert.IsType<StartOfTabDirective>(result[0]);
            Assert.IsType<TabLine>(result[1]);
            Assert.IsType<EndOfTabDirective>(result[2]);
            Assert.IsType<SongLine>(result[3]);
            Assert.IsType<SongLine>(result[4]);

        }

        [Fact]
        public void SerializeTest()
        {
            //Arrange
            string test = @"All my [Am/C] loving [Caug] darling I'll be [C] true (PAUSE 4)
{c: }
[Dm] [G7] [C] [Am] [F] [Dm] [Bb] [G7]
";
            var expected = @"All my [Am/C] loving [Caug] darling I'll be [C] true (PAUSE 4)
{c}
[Dm] [G7] [C] [Am] [F] [Dm] [Bb] [G7]";
            TextReader reader = new StringReader(test);
            var parser = new Parser(reader);
            // Act
            var document = new Document(parser.Parse());
            Assert.NotNull(document);
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var settings = new SerializerSettings { ShortenDirectives = true };
            ChordProSerializer.Serialize(document, writer, settings);
            Assert.Equal(expected, writer.ToString().Trim());
        }
    }
}

using Xunit;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using ChordPro.Library;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class ChordSizeHandlerTestFixture
    {
        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_LongForm()
        {
            // Arrange
            int fontSize = 9;
            string input = $"{{chordsize: {fontSize}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordSizeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ChordSizeDirective>(directive);
            Assert.Equal(fontSize, (directive as ChordSizeDirective).FontSize);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_ShortForm()
        {
            // Arrange
            int fontSize = 9;
            string input = $"{{cs: {fontSize}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordSizeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ChordSizeDirective>(directive);
            Assert.Equal(fontSize, (directive as ChordSizeDirective).FontSize);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_NaN()
        {
            // Arrange
            string input = $"{{chordsize: NaN}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordSizeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
            Assert.Null(directive);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_LongForm()
        {
            // Arrange
            int fontSize = 9;
            Directive directive = new ChordSizeDirective(fontSize);
            string expectedText = $"{{chordsize: {fontSize}}}";
            DirectiveHandler sut = ChordSizeHandler.Instance;
            // Act
            string text = sut.GetString(directive, shorten: false);
            // Assert
            Assert.Equal(expectedText, text);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_ShortForm()
        {
            // Arrange
            int fontSize = 9;
            Directive directive = new ChordSizeDirective(fontSize);
            string expectedText = $"{{cs: {fontSize}}}";
            DirectiveHandler sut = ChordSizeHandler.Instance;
            // Act
            string text = sut.GetString(directive, shorten: true);
            // Assert
            Assert.Equal(expectedText, text);
        }
    }
}

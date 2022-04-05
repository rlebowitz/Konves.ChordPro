using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class ChordFontHandlerTestFixture
    {
        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_LongForm()
        {
            // Arrange
            string fontFamily = "times";
            string input = $"{{chordfont: {fontFamily}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordFontHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ChordFontDirective>(directive);
            Assert.Equal(fontFamily, (directive as ChordFontDirective).FontFamily);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_ShortForm()
        {
            // Arrange
            string fontFamily = "times";
            string input = $"{{cf: {fontFamily}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordFontHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ChordFontDirective>(directive);
            Assert.Equal(fontFamily, (directive as ChordFontDirective).FontFamily);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_LongForm()
        {
            // Arrange
            string fontFamily = "times";
            Directive directive = new ChordFontDirective(fontFamily);
            string expectedText = $"{{chordfont: {fontFamily}}}";
            DirectiveHandler handler = ChordFontHandler.Instance;
            // Act
            string text = handler.GetString(directive, shorten: false);
            // Assert
            Assert.Equal(expectedText, text);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_ShortForm()
        {
            // Arrange
            string fontFamily = "times";
            Directive directive = new ChordFontDirective(fontFamily);
            string expectedText = $"{{cf: {fontFamily}}}";
            DirectiveHandler handler = ChordFontHandler.Instance;
            // Act
            string text = handler.GetString(directive, shorten: true);
            // Assert
            Assert.Equal(expectedText, text);
        }
    }
}


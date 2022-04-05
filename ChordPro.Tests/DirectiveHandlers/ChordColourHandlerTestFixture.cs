using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class ChordColourHandlerTestFixture
    {
        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_LongForm()
        {
            // Arrange
            string color = "red";
            string input = $"{{chordcolour: {color}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ChordColourHandler.Instance;

            // Act
            bool result = handler.TryParse(components, out Directive directive);

            // Assert
            Assert.True(result);
            Assert.IsType<ChordColourDirective>(directive);
            Assert.Equal(color, (directive as ChordColourDirective).Colour);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_LongForm()
        {
            // Arrange
            string color = "red";
            Directive directive = new ChordColourDirective(color);
            string expectedText = $"{{chordcolour: {color}}}";
            DirectiveHandler sut = ChordColourHandler.Instance;
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
            string color = "red";
            Directive directive = new ChordColourDirective(color);
            string expectedText = $"{{chordcolour: {color}}}";
            DirectiveHandler handler = ChordColourHandler.Instance;
            // Act
            string text = handler.GetString(directive, shorten: true);
            // Assert
            Assert.Equal(expectedText, text);
        }
    }
}

using ChordPro.Library;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class ColumnsHandlerTestFixture
    {
        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_LongForm()
        {
            // Arrange
            int number = 3;
            string input = $"{{columns: {number}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ColumnsHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ColumnsDirective>(directive);
            Assert.Equal(number, (directive as ColumnsDirective).Number);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_ShortForm()
        {
            // Arrange
            int number = 3;
            string input = $"{{col: {number}}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler handler = ColumnsHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
            Assert.IsType<ColumnsDirective>(directive);
            Assert.Equal(number, (directive as ColumnsDirective).Number);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void TryParseTest_NaN()
        {
            // Arrange
            string input = $"{{columns: NaN}}";
            DirectiveComponents components = DirectiveComponents.Parse(input);
            DirectiveHandler sut = ColumnsHandler.Instance;
            // Act
            bool result = sut.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
            Assert.Null(directive);
        }

        [Fact]
        [Trait("Category", "DirectiveHandler")]
        public void GetStringTest_LongForm()
        {
            // Arrange
            int number = 3;
            Directive directive = new ColumnsDirective(number);
            string expectedText = $"{{columns: {number}}}";
            DirectiveHandler handler = ColumnsHandler.Instance;
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
            int number = 3;
            Directive directive = new ColumnsDirective(number);
            string expectedText = $"{{col: {number}}}";
            DirectiveHandler handler = ColumnsHandler.Instance;
            // Act
            string text = handler.GetString(directive, shorten: true);
            // Assert
            Assert.Equal(expectedText, text);
        }
    }
}

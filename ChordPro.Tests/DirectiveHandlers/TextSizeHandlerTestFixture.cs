using ChordPro.Library;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class TextSizeHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{textsize: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TextSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = handler.TryParse(components, out directive);

			// Assert
			Assert.True(result);
			Assert.IsType<TextSizeDirective>(directive);
			Assert.Equal(fontSize, (directive as TextSizeDirective).FontSize);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{ts: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TextSizeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TextSizeDirective>(directive);
			Assert.Equal(fontSize, (directive as TextSizeDirective).FontSize);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_NaN()
		{
			// Arrange
			string input = $"{{textsize: NaN}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TextSizeHandler.Instance;
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
			Directive directive = new TextSizeDirective(fontSize);
			string expectedText = $"{{textsize: {fontSize}}}";
			DirectiveHandler handler = TextSizeHandler.Instance;
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
			int fontSize = 9;
			Directive directive = new TextSizeDirective(fontSize);
			string expectedText = $"{{ts: {fontSize}}}";
			DirectiveHandler handler = TextSizeHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

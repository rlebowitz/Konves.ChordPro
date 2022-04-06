using ChordPro.Library;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class TextFontHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string fontFamily = "times";
			string input = $"{{textfont: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TextFontHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TextFontDirective>(directive);
			Assert.Equal(fontFamily, (directive as TextFontDirective).FontFamily);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string fontFamily = "times";
			string input = $"{{tf: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TextFontHandler.Instance;

            // Act
            bool result = handler.TryParse(components, out Directive directive);

            // Assert
            Assert.True(result);
			Assert.IsType<TextFontDirective>(directive);
			Assert.Equal(fontFamily, (directive as TextFontDirective).FontFamily);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string fontFamily = "times";
			Directive directive = new TextFontDirective(fontFamily);
			string expectedText = $"{{textfont: {fontFamily}}}";
			DirectiveHandler handler = TextFontHandler.Instance;
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
			Directive directive = new TextFontDirective(fontFamily);
			string expectedText = $"{{tf: {fontFamily}}}";
			DirectiveHandler handler = TextFontHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

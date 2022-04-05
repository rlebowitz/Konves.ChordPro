using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class TitleHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string title = "some title";
			string input = $"{{title: {title}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TitleHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TitleDirective>(directive);
			Assert.Equal(title, (directive as TitleDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string title = "some title";
			string input = $"{{t: {title}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TitleHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TitleDirective>(directive);
			Assert.Equal(title, (directive as TitleDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string title = "some title";
			Directive directive = new TitleDirective(title);
			string expectedText = $"{{title: {title}}}";
			DirectiveHandler handler = TitleHandler.Instance;
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
			string title = "some title";
			Directive directive = new TitleDirective(title);
			string expectedText = $"{{t: {title}}}";
			DirectiveHandler handler = TitleHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

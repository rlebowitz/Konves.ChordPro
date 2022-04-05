using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class PageTypeHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_Letter()
		{
			// Arrange
			PageType pageType = PageType.Letter;
			string input = $"{{pagetype: letter}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = PageTypeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<PageTypeDirective>(directive);
			Assert.Equal(pageType, (directive as PageTypeDirective).PageType);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_A4()
		{
			// Arrange
			PageType pageType = PageType.A4;
			string input = $"{{pagetype: a4}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = PageTypeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<PageTypeDirective>(directive);
			Assert.Equal(pageType, (directive as PageTypeDirective).PageType);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_Other()
		{
			// Arrange
			string input = $"{{pagetype: not a valid page type}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = PageTypeHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			Directive directive = new PageTypeDirective(PageType.Letter);
			string expectedText = $"{{pagetype: letter}}";
			DirectiveHandler handler = PageTypeHandler.Instance;
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
			Directive directive = new PageTypeDirective(PageType.Letter);
			string expectedText = $"{{pagetype: letter}}";
			DirectiveHandler handler = PageTypeHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class TitlesHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_Left()
		{
			// Arrange
			Alignment alignment = Alignment.Left;
			string input = $"{{titles: left}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TitlesHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TitlesDirective>(directive);
			Assert.Equal(alignment, (directive as TitlesDirective).Flush);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_Center()
		{
			// Arrange
			Alignment alignment = Alignment.Center;
			string input = $"{{titles: center}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TitlesHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TitlesDirective>(directive);
			Assert.Equal(alignment, (directive as TitlesDirective).Flush);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm_Other()
		{
			// Arrange
			string input = $"{{titles: not a valid alignment}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = TitlesHandler.Instance;
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
			Directive directive = new TitlesDirective(Alignment.Left);
			string expectedText = $"{{titles: left}}";
			DirectiveHandler handler = TitlesHandler.Instance;
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
			Directive directive = new TitlesDirective(Alignment.Left);
			string expectedText = $"{{titles: left}}";
			DirectiveHandler handler = TitlesHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

using ChordPro.Library;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class SubtitleHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			string input = $"{{subtitle: {subtitle}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = SubtitleHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<SubtitleDirective>(directive);
			Assert.Equal(subtitle, (directive as SubtitleDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			string input = $"{{st: {subtitle}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = SubtitleHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<SubtitleDirective>(directive);
			Assert.Equal(subtitle, (directive as SubtitleDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			Directive directive = new SubtitleDirective(subtitle);
			string expectedText = $"{{subtitle: {subtitle}}}";
			DirectiveHandler handler = SubtitleHandler.Instance;
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
			string subtitle = "some subtitle";
			Directive directive = new SubtitleDirective(subtitle);
			string expectedText = $"{{st: {subtitle}}}";
			DirectiveHandler handler = SubtitleHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

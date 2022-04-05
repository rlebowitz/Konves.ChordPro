using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class CommentItalicHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{comment_italic: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = CommentItalicHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<CommentItalicDirective>(directive);
			Assert.Equal(comment, (directive as CommentItalicDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{ci: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = CommentItalicHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<CommentItalicDirective>(directive);
			Assert.Equal(comment, (directive as CommentItalicDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentItalicDirective(comment);
			string expectedText = $"{{comment_italic: {comment}}}";
			DirectiveHandler handler = CommentItalicHandler.Instance;
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
			string comment = "some comment";
			Directive directive = new CommentItalicDirective(comment);
			string expectedText = $"{{ci: {comment}}}";
			DirectiveHandler handler = CommentItalicHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

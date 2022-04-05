using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class CommentHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{comment: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = CommentHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<CommentDirective>(directive);
			Assert.Equal(comment, (directive as CommentDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{c: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = CommentHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<CommentDirective>(directive);
			Assert.Equal(comment, (directive as CommentDirective).Text);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentDirective(comment);
			string expectedText = $"{{comment: {comment}}}";
			DirectiveHandler handler = CommentHandler.Instance;
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
			Directive directive = new CommentDirective(comment);
			string expectedText = $"{{c: {comment}}}";
			DirectiveHandler handler = CommentHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

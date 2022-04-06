using Xunit;
using ChordPro.Library.Directives;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library;

namespace ChordPro.Tests.DirectiveHandlers
{
    public abstract class KeyOnlyBaseTestFixture<TDirective> where TDirective : Directive, new()
	{
		private string LongForm { get; }
		private string ShortForm { get; }
		private DirectiveHandler Handler { get; }

		protected KeyOnlyBaseTestFixture(string longForm, string shortForm, DirectiveHandler handler)
		{
			LongForm = longForm;
			ShortForm = shortForm;
			Handler = handler;
		}
		
		public virtual void TryParseTest_LongForm()
		{
			// Arrange
			string input = LongForm;
			DirectiveComponents components = DirectiveComponents.Parse(input);
            // Act
            bool result = Handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TDirective>(directive);
		}

		public virtual void TryParseTest_ShortForm()
		{
			// Arrange
			string input = ShortForm;
			DirectiveComponents components = DirectiveComponents.Parse(input);
            // Act
            bool result = Handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<TDirective>(directive);
		}

		public virtual void GetStringTest_LongForm()
		{
			// Arrange
			Directive directive = new TDirective();
			string expectedText = LongForm;
			// Act
			string text = Handler.GetString(directive, shorten: false);
			// Assert
			Assert.Equal(expectedText, text);
		}

		public virtual void GetStringTest_ShortForm()
		{
			// Arrange
			Directive directive = new TDirective();
			string expectedText = ShortForm;
			// Act
			string text = Handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}

	}
}
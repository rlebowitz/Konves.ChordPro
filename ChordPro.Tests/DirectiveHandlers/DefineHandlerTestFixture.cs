using ChordPro.Library;
using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class DefineHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string chord = "X";
			string definition = "some definition";
			string input = $"{{define {chord}: {definition}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler handler = DefineHandler.Instance;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.True(result);
			Assert.IsType<DefineDirective>(directive);
			Assert.Equal(chord, (directive as DefineDirective).Chord);
			Assert.Equal(definition, (directive as DefineDirective).Definition);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string chord = "X";
			string definition = "some definition";
			Directive directive = new DefineDirective(chord, definition);
			string expectedText = $"{{define {chord}: {definition}}}";
			DirectiveHandler handler = DefineHandler.Instance;
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
			string chord = "X";
			string definition = "some definition";
			Directive directive = new DefineDirective(chord, definition);
			string expectedText = $"{{define {chord}: {definition}}}";
			DirectiveHandler handler = DefineHandler.Instance;
			// Act
			string text = handler.GetString(directive, shorten: true);
			// Assert
			Assert.Equal(expectedText, text);
		}
	}
}

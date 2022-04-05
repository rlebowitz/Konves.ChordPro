using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Moq;
using Moq.Protected;
using System;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
	public class DirectiveHandlerTestFixture
	{
		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenComponentKeyDoesNotMatch()
		{
			// Arrange
			string longName = nameof(longName);
			string shortName = nameof(shortName);
			string key = nameof(key);

			var components = new DirectiveComponents(key, null, null);
			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.LongName).Returns(longName);
			mock.Setup(h => h.ShortName).Returns(shortName);
			DirectiveHandler handler = mock.Object;
            // Act
            bool result = handler.TryParse(components, out Directive directive);

            // Assert
            Assert.NotEqual(longName, key);
			Assert.NotEqual(shortName, key);
			Assert.False(result);
			Assert.Null(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenValueIsRequiredButIsMissing()
		{
			// Arrange
			string key = nameof(key);
			DirectiveComponents components = new DirectiveComponents(key, null, null);

			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.LongName).Returns(key);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.Optional);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Required);
			DirectiveHandler sut = mock.Object;
            // Act
            bool result = sut.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
			Assert.Null(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenValueIsNotAllowedButIsPresent()
		{
			// Arrange
			string key = nameof(key);
			string value = nameof(value);

			DirectiveComponents components = new DirectiveComponents(key, null, value);

			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.LongName).Returns(key);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.Optional);
			mock.Setup(h => h.Value).Returns(ComponentPresence.NotAllowed);
			DirectiveHandler handler = mock.Object;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
			Assert.Null(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenSubKeyIsRequiredButIsMissing()
		{
			// Arrange
			string key = nameof(key);
			DirectiveComponents components = new DirectiveComponents(key, null, null);

			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.LongName).Returns(key);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.Required);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Optional);

			DirectiveHandler handler = mock.Object;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
			Assert.Null(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenSubKeyIsNotAllowedButIsPresent()
		{
			// Arrange
			string key = nameof(key);
			string subkey = nameof(subkey);
			var components = new DirectiveComponents(key, subkey, null);

			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.LongName).Returns(key);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.NotAllowed);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Optional);

			DirectiveHandler handler = mock.Object;
            // Act
            bool result = handler.TryParse(components, out Directive directive);
            // Assert
            Assert.False(result);
			Assert.Null(directive);
		}

		[Theory]
		[Trait("Category", "DirectiveHandler")]
        [InlineData("asdf", ComponentPresence.NotAllowed, null)]
        [InlineData("asdf", ComponentPresence.Optional, " asdf")]
        [InlineData("asdf", ComponentPresence.Required, " asdf")]
		public void GetSubKeyStringTest(string subkey, ComponentPresence subKeyPresence, string expectedResult)
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.SubKey).Returns(subKeyPresence);
			DirectiveHandler handler = mock.Object;
			// Act
			string result = handler.GetSubKeyString(subkey);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		private void DoGetSubKeyStringTest(string subkey, ComponentPresence subKeyPresence, string expectedResult)
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.SubKey).Returns(subKeyPresence);
			DirectiveHandler handler = mock.Object;
			// Act
			string result = handler.GetSubKeyString(subkey);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[Trait("Category", "DirectiveHandler")]
        [InlineData("asdf", ComponentPresence.NotAllowed, null)]
        [InlineData("asdf", ComponentPresence.Optional, ": asdf")]
        [InlineData("asdf", ComponentPresence.Required, ": asdf")]
        [InlineData(null, ComponentPresence.NotAllowed, null)]
        [InlineData(null, ComponentPresence.Optional, null)]
        [InlineData(null, ComponentPresence.Required, null)]
		public void GetValueStringTest(string value, ComponentPresence valuePresence, string expectedResult)
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();
			mock.Setup(h => h.Value).Returns(valuePresence);
			DirectiveHandler handler = mock.Object;
			// Act
			string result = handler.GetValueString(value);
			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[Trait("Category", "DirectiveHandler")]
        [InlineData("longName", "shortName", "subkey", "value", false, "{longName subkey: value}")]
        [InlineData("longName", "shortName", "subkey", "value", true, "{shortName subkey: value}")]
		public void GetStringTest(string longName, string shortName, string subkey, string value, bool shorten, string expected)
		{
			Directive directive = new Mock<Directive>().Object;

			var mock = new Mock<DirectiveHandler>();
			mock.Protected().Setup<string>("GetSubKey", ItExpr.IsAny<Directive>()).Returns(subkey);
			mock.Protected().Setup<string>("GetValue", ItExpr.IsAny<Directive>()).Returns(value);
			mock.Setup(h => h.LongName).Returns(longName);
			mock.Setup(h => h.ShortName).Returns(shortName);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.Optional);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Optional);

			DirectiveHandler handler = mock.Object;
			// Act
			string result = handler.GetString(directive, shorten);
			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest_Null()
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();
			DirectiveHandler handler = mock.Object;
			// Act
			Assert.Throws<ArgumentNullException>(() => handler.GetString(null));
		}
	}
}

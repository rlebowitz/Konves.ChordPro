﻿using ChordPro.Lib;
using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Moq;
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

			DirectiveHandler sut = mock.Object;

			// Act
			Directive directive;
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsFalse(result);
			Assert.IsNull(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void WhenSubKeyIsNotAllowedButIsPresent()
		{
			// Arrange
			string key = nameof(key);
			string subkey = nameof(subkey);

			DirectiveComponents components = new DirectiveComponents(key, subkey, null);

			var mock = new Mock<DirectiveHandler>();

			mock.Setup(h => h.LongName).Returns(key);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.NotAllowed);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Optional);

			DirectiveHandler sut = mock.Object;

			// Act
			Directive directive;
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsFalse(result);
			Assert.IsNull(directive);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetSubKeyStringTest()
		{
			DoGetSubKeyStringTest("asdf", ComponentPresence.NotAllowed, null);
			DoGetSubKeyStringTest("asdf", ComponentPresence.Optional, " asdf");
			DoGetSubKeyStringTest("asdf", ComponentPresence.Required, " asdf");
		}

		private void DoGetSubKeyStringTest(string subkey, ComponentPresence subKeyPresence, string expectedResult)
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();

			mock.Setup(h => h.SubKey).Returns(subKeyPresence);

			DirectiveHandler sut = mock.Object;

			// Act
			string result = sut.GetSubKeyString(subkey);

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetValueStringTest()
		{
			DoGetValueStringTest("asdf", ComponentPresence.NotAllowed, null);
			DoGetValueStringTest("asdf", ComponentPresence.Optional, ": asdf");
			DoGetValueStringTest("asdf", ComponentPresence.Required, ": asdf");

			DoGetValueStringTest(null, ComponentPresence.NotAllowed, null);
			DoGetValueStringTest(null, ComponentPresence.Optional, null);
			DoGetValueStringTest(null, ComponentPresence.Required, null);
		}

		private void DoGetValueStringTest(string value, ComponentPresence valuePresence, string expectedResult)
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();

			mock.Setup(h => h.Value).Returns(valuePresence);

			DirectiveHandler sut = mock.Object;

			// Act
			string result = sut.GetValueString(value);

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public void GetStringTest()
		{
			DoGetStringTest("longName", "shortName", "subkey", "value", false, "{longName subkey: value}");
			DoGetStringTest("longName", "shortName", "subkey", "value", true, "{shortName subkey: value}");
		}

		private void DoGetStringTest(string longName, string shortName, string subkey, string value, bool shorten, string expected)
		{
			// Arrange
			Directive directive = new Mock<Directive>().Object;

			var mock = new Mock<DirectiveHandler>();
			mock.Protected().Setup<string>("GetSubKey", ItExpr.IsAny<Directive>()).Returns(subkey);
			mock.Protected().Setup<string>("GetValue", ItExpr.IsAny<Directive>()).Returns(value);
			mock.Setup(h => h.LongName).Returns(longName);
			mock.Setup(h => h.ShortName).Returns(shortName);
			mock.Setup(h => h.SubKey).Returns(ComponentPresence.Optional);
			mock.Setup(h => h.Value).Returns(ComponentPresence.Optional);

			DirectiveHandler sut = mock.Object;

			// Act
			string result = sut.GetString(directive, shorten);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetStringTest_Null()
		{
			// Arrange
			var mock = new Mock<DirectiveHandler>();
			DirectiveHandler sut = mock.Object;

			// Act
			string result = sut.GetString(null);
		}
	}
}

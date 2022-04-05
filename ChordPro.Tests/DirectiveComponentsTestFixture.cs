using ChordPro.Lib;
using Xunit;

namespace ChordPro.Tests
{
    public class DirectiveComponentsTestFixture
    {
        [Theory]
        [InlineData("{asdf:qwerty}", true, "asdf", "", "qwerty")]
        [InlineData("  {  asdf  :  qwerty  }  ", true, "asdf", "", "qwerty")]
        [InlineData("{asdf abc:qwerty}", true, "asdf", "abc", "qwerty")]
        [InlineData("  {  asdf   abc  :  qwerty  asdf  }  ", true, "asdf", "abc", "qwerty  asdf")]
        [InlineData("{asdf:qwerty}#Comment", true, "asdf", "", "qwerty")]
        [InlineData("  {  asdf  :  qwerty  }  # Comment", true, "asdf", "", "qwerty")]
        [InlineData("{asdf abc:qwerty}# Comment", true, "asdf", "abc", "qwerty")]
        [InlineData("  {  asdf   abc  :  qwerty  asdf  }  # Comment", true, "asdf", "abc", "qwerty  asdf")]
        [InlineData("{}", false, null, null, null)]
        [InlineData("asdf", false, null, null, null)]
        [InlineData("{:asdf}", false, null, null, null)]
        public void TryParseTest(string input, bool expectedResult, string expectedKey, string expectedSubKey, string expectedValue)
        {
            // Act
            bool result = DirectiveComponents.TryParse(input, out DirectiveComponents components);
            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedKey, components?.Key);
            Assert.Equal(expectedSubKey, components?.SubKey);
            Assert.Equal(expectedValue, components?.Value);
        }
        
    }
}

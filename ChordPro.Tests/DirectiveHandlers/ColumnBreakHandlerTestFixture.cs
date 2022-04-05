using ChordPro.Lib.DirectiveHandlers;
using ChordPro.Lib.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class ColumnBreakHandlerTestFixture : KeyOnlyBaseTestFixture<ColumnBreakDirective>
	{
		public ColumnBreakHandlerTestFixture() : base("{column_break}", "{colb}", ColumnBreakHandler.Instance) { }

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public override void TryParseTest_LongForm() { base.TryParseTest_LongForm(); }

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public override void TryParseTest_ShortForm() { base.TryParseTest_ShortForm(); }

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public override void GetStringTest_LongForm() { base.GetStringTest_LongForm(); }

		[Fact]
		[Trait("Category", "DirectiveHandler")]
		public override void GetStringTest_ShortForm() { base.GetStringTest_ShortForm(); }
	}
}
﻿using ChordPro.Library.DirectiveHandlers;
using ChordPro.Library.Directives;
using Xunit;

namespace ChordPro.Tests.DirectiveHandlers
{
    public class GridHandlerTestFixture : KeyOnlyBaseTestFixture<GridDirective>
	{
		public GridHandlerTestFixture() : base("{grid}", "{g}", GridHandler.Instance) { }

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

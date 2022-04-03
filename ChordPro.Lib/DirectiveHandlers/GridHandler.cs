﻿using ChordPro.Lib;
using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class GridHandler : DirectiveHandler<GridDirective>
    {
        private GridHandler() { }

        public static GridHandler Instance { get; } = new GridHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new GridDirective();
            return true;
        }

        public override string LongName { get { return "grid"; } }
        public override string ShortName { get { return "g"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
    }
}

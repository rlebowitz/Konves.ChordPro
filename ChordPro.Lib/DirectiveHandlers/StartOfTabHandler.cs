﻿using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class StartOfTabHandler : DirectiveHandler<StartOfTabDirective>
    {
        private StartOfTabHandler() { }

        public static StartOfTabHandler Instance { get; } = new StartOfTabHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new StartOfTabDirective();
            return true;
        }

        public override string LongName { get { return "start_of_tab"; } }
        public override string ShortName { get { return "sot"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
    }
}

﻿using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class StartOfChorusHandler : DirectiveHandler<StartOfChorusDirective>
    {
        private StartOfChorusHandler() { }

        public static StartOfChorusHandler Instance { get; } = new StartOfChorusHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new StartOfChorusDirective();
            return true;
        }

        public override string LongName { get { return "start_of_chorus"; } }
        public override string ShortName { get { return "soc"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
    }
}

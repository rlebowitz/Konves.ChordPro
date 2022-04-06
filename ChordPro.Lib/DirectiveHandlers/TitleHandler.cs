﻿using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class TitleHandler : DirectiveHandler<TitleDirective>
    {
        private TitleHandler() { }

        public static TitleHandler Instance { get; } = new TitleHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new TitleDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as TitleDirective)?.Text;
        }

        public override string LongName { get { return "title"; } }
        public override string ShortName { get { return "t"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

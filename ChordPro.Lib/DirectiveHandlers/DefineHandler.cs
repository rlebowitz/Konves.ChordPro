﻿using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class DefineHandler : DirectiveHandler<DefineDirective>
    {
        private DefineHandler() { }

        public static DefineHandler Instance { get; } = new DefineHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new DefineDirective(components.SubKey, components.Value); //TODO: allow various formats
            return true;
        }

        protected override string GetSubKey(Directive directive)
        {
            return (directive as DefineDirective)?.Chord;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as DefineDirective)?.Definition;
        }

        public override string LongName { get { return "define"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.Required; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

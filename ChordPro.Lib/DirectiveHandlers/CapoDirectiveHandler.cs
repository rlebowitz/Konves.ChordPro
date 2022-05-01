using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class CapoDirectiveHandler : DirectiveHandler<CapoDirective>
    {
        private CapoDirectiveHandler() { }

        public static CapoDirectiveHandler Instance { get; } = new CapoDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            if (int.TryParse(components.Value, out int value))
            {
                directive = new CapoDirective(value);
                return true;
            }
            directive = null;
            return false;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as CapoDirective)?.Capo.ToString();
        }

        public override string LongName { get { return "capo"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

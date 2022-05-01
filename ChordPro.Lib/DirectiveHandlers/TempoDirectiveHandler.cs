using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class TempoDirectiveHandler : DirectiveHandler<TempoDirective>
    {
        private TempoDirectiveHandler() { }

        public static TempoDirectiveHandler Instance { get; } = new TempoDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            if (int.TryParse(components.Value, out int value))
            {
                directive = new TempoDirective(value);
                return true;
            }
            directive = null;
            return false;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as TempoDirective)?.Tempo.ToString();
        }

        public override string LongName { get { return "tempo"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

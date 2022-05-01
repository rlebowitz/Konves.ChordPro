using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class DurationDirectiveHandler : DirectiveHandler<DurationDirective>
    {
        private DurationDirectiveHandler() { }

        public static DurationDirectiveHandler Instance { get; } = new DurationDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            if (int.TryParse(components.Value, out int value))
            {
                directive = new DurationDirective(value);
                return true;
            }
            directive = null;
            return false;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as DurationDirective)?.Duration.ToString();
        }

        public override string LongName { get { return "duration"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

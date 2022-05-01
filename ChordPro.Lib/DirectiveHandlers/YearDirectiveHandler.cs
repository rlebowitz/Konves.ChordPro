using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class YearDirectiveHandler : DirectiveHandler<YearDirective>
    {
        private YearDirectiveHandler() { }

        public static YearDirectiveHandler Instance { get; } = new YearDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            if (int.TryParse(components.Value, out int value))
            {
                directive = new YearDirective(value);
                return true;
            }
            directive = null;
            return false;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as YearDirective)?.Year.ToString();
        }

        public override string LongName { get { return "year"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

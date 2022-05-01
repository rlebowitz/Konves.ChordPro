using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class CopyrightDirectiveHandler : DirectiveHandler<CopyrightDirective>
    {
        private CopyrightDirectiveHandler() { }

        public static CopyrightDirectiveHandler Instance { get; } = new CopyrightDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            if (int.TryParse(components.SubKey, out int value))
            {
                directive = new CopyrightDirective(value, components.Value);
                return true;
            }

            directive = null;
            return false;
        }

        protected override string GetSubKey(Directive directive)
        {
            return (directive as CopyrightDirective)?.Year.ToString();
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as CopyrightDirective)?.Owner;
        }

        public override string LongName { get { return "copyright"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.Required; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

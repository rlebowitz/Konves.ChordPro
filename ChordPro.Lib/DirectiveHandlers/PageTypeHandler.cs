using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class PageTypeHandler : DirectiveHandler<PageTypeDirective>
    {
        private PageTypeHandler() { }

        public static PageTypeHandler Instance { get; } = new PageTypeHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            switch (components.Value.ToLower())
            {
                case "a4":
                    directive = new PageTypeDirective(PageType.A4);
                    return true;
                case "letter":
                    directive = new PageTypeDirective(PageType.Letter);
                    return true;
                default:
                    directive = null;
                    return false;
            }
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as PageTypeDirective)?.PageType.ToString().ToLower();
        }

        public override string LongName { get { return "pagetype"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

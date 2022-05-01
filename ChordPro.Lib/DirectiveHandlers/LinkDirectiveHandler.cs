using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class LinkDirectiveHandler : DirectiveHandler<LinkDirective>
    {
        private LinkDirectiveHandler() { }

        public static LinkDirectiveHandler Instance { get; } = new LinkDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new LinkDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as LinkDirective)?.Link;
        }

        public override string LongName { get { return "link"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

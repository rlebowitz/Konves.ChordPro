using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class SortTitleDirectiveHandler : DirectiveHandler<SortTitleDirective>
    {
        private SortTitleDirectiveHandler() { }

        public static SortTitleDirectiveHandler Instance { get; } = new SortTitleDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new SortTitleDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as SortTitleDirective)?.SortTitle;
        }

        public override string LongName { get { return "sorttitle"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

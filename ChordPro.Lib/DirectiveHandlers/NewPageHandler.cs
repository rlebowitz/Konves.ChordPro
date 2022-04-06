using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class NewPageHandler : DirectiveHandler<NewPageDirective>
    {
        private NewPageHandler() { }

        public static NewPageHandler Instance { get; } = new NewPageHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new NewPageDirective();
            return true;
        }

        public override string LongName { get { return "new_page"; } }
        public override string ShortName { get { return "np"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
    }
}

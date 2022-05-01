using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class ComposerDirectiveHandler : DirectiveHandler<ComposerDirective>
    {
        private ComposerDirectiveHandler() { }

        public static ComposerDirectiveHandler Instance { get; } = new ComposerDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new ComposerDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as ComposerDirective)?.Composer;
        }

        public override string LongName { get { return "composer"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

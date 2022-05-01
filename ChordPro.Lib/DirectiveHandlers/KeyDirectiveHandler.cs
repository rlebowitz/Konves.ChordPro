using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class KeyDirectiveHandler : DirectiveHandler<KeyDirective>
    {
        private KeyDirectiveHandler() { }

        public static KeyDirectiveHandler Instance { get; } = new KeyDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new KeyDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as KeyDirective)?.Key;
        }

        public override string LongName { get { return "key"; } }
        public override string ShortName { get { return "k"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

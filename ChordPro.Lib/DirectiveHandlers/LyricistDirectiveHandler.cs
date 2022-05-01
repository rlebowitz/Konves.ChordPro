using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class LyricistDirectiveHandler : DirectiveHandler<LyricistDirective>
    {
        private LyricistDirectiveHandler() { }

        public static LyricistDirectiveHandler Instance { get; } = new LyricistDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new LyricistDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as LyricistDirective)?.Lyricist;
        }

        public override string LongName { get { return "lyricist"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class ArtistDirectiveHandler : DirectiveHandler<ArtistDirective>
    {
        private ArtistDirectiveHandler() { }

        public static ArtistDirectiveHandler Instance { get; } = new ArtistDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new ArtistDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as ArtistDirective)?.Artist;
        }

        public override string LongName { get { return "artist"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

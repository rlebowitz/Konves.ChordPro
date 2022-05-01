using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class AlbumDirectiveHandler : DirectiveHandler<AlbumDirective>
    {
        private AlbumDirectiveHandler() { }

        public static AlbumDirectiveHandler Instance { get; } = new AlbumDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new AlbumDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as AlbumDirective)?.Album;
        }

        public override string LongName { get { return "album"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class ColumnBreakHandler : DirectiveHandler<ColumnBreakDirective>
    {
        private ColumnBreakHandler() { }

        public static ColumnBreakHandler Instance { get; } = new ColumnBreakHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new ColumnBreakDirective();
            return true;
        }

        public override string LongName { get { return "column_break"; } }
        public override string ShortName { get { return "colb"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
    }
}

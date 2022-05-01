using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public sealed class TimeDirectiveHandler : DirectiveHandler<TimeDirective>
    {
        private TimeDirectiveHandler() { }

        public static TimeDirectiveHandler Instance { get; } = new TimeDirectiveHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new TimeDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as TimeDirective)?.Time;
        }

        public override string LongName { get { return "time"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

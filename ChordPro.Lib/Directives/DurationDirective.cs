namespace ChordPro.Library.Directives
{
    public sealed class DurationDirective : Directive
    {
        public DurationDirective(int duration)
        {
            Duration = duration;
        }

        public int Duration { get; set; }
    }
}

namespace ChordPro.Library.Directives
{
    public sealed class TimeDirective : Directive
    {
        public TimeDirective(string time)
        {
            Time = time;
        }

        public string Time { get; set; }
    }
}

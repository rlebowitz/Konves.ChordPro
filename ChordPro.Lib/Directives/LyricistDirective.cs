namespace ChordPro.Library.Directives
{
    public sealed class LyricistDirective : Directive
    {
        public LyricistDirective(string lyricist)
        {
            Lyricist = lyricist;
        }

        public string Lyricist { get; set; }
    }
}

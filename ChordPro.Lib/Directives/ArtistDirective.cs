namespace ChordPro.Library.Directives
{
    public sealed class ArtistDirective : Directive
    {
        public ArtistDirective(string artist)
        {
            Artist = artist;
        }

        public string Artist { get; set; }
    }
}

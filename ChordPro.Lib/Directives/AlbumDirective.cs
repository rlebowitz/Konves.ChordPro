namespace ChordPro.Library.Directives
{
    public sealed class AlbumDirective : Directive
    {
        public AlbumDirective(string album)
        {
            Album = album;
        }

        public string Album { get; set; }
    }
}

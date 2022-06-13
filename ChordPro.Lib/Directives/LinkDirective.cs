namespace ChordPro.Library.Directives
{
    public sealed class LinkDirective : Directive
    {
        public LinkDirective(string link) => Link = link;

        public string Link { get; set; }
    }
}

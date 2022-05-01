namespace ChordPro.Library.Directives
{
    public sealed class SortTitleDirective : Directive
    {
        public SortTitleDirective(string sortTitle)
        {
            SortTitle = sortTitle;
        }

        public string SortTitle { get; set; }
    }
}

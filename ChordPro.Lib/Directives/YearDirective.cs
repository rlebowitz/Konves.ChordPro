namespace ChordPro.Library.Directives
{
    public sealed class YearDirective : Directive
    {
        public YearDirective(int year)
        {
            Year = year;
        }

        public int Year { get; set; }
    }
}

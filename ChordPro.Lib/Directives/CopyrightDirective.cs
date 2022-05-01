namespace ChordPro.Library.Directives
{
    public sealed class CopyrightDirective : Directive
    {
        public CopyrightDirective(int year, string owner)
        {
            Year = year;
            Owner = owner;
        }

        public int Year { get; set; }
        public string Owner { get; set; }
    }
}

namespace ChordPro.Library.Directives
{
    public sealed class CapoDirective : Directive
    {
        public CapoDirective(int capo)
        {
            Capo = capo;
        }

        public int Capo { get; set; }
    }
}

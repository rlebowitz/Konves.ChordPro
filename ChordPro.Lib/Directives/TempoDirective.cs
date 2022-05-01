namespace ChordPro.Library.Directives
{
    public sealed class TempoDirective : Directive
    {
        public TempoDirective(int tempo)
        {
            Tempo = tempo;
        }

        public int Tempo { get; set; }
    }
}

namespace ChordPro.Library.Directives
{
    public sealed class ComposerDirective : Directive
    {
        public ComposerDirective(string composer)
        {
            Composer = composer;
        }

        public string Composer { get; set; }
    }
}

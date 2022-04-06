namespace ChordPro.Library.Directives
{
    public sealed class TextFontDirective : Directive
    {
        public TextFontDirective(string fontFamily)
        {
            FontFamily = fontFamily;
        }

        public string FontFamily { get; set; }
    }
}

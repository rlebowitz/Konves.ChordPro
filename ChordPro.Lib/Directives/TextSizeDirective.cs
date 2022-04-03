namespace ChordPro.Lib.Directives
{
    public sealed class TextSizeDirective : Directive
    {
        public TextSizeDirective(int fontSize)
        {
            FontSize = fontSize;
        }

        public int FontSize { get; set; }
    }
}

﻿namespace ChordPro.Lib.Directives
{
    public sealed class ChordSizeDirective : Directive
    {
        public ChordSizeDirective(int fontSize)
        {
            FontSize = fontSize;
        }

        public int FontSize { get; set; }
    }
}

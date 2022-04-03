﻿namespace ChordPro.Lib.Directives
{
    public sealed class TitlesDirective : Directive
    {
        public TitlesDirective(Alignment flush)
        {
            Flush = flush;
        }

        public Alignment Flush { get; set; }
    }
}

﻿namespace ChordPro.Library.Directives
{
    public sealed class SubtitleDirective : Directive
    {
        public SubtitleDirective(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}

﻿namespace ChordPro.Lib.Directives
{
    public sealed class CommentItalicDirective : Directive
    {
        public CommentItalicDirective(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}

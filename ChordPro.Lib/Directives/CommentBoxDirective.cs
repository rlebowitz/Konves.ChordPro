namespace ChordPro.Library.Directives
{
    public sealed class CommentBoxDirective : Directive
    {
        public CommentBoxDirective(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}

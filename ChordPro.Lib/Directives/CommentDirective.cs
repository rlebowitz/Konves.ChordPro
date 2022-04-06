namespace ChordPro.Library.Directives
{
    public sealed class CommentDirective : Directive
    {
        public CommentDirective(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}

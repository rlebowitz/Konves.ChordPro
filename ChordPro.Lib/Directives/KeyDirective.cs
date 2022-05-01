namespace ChordPro.Library.Directives
{
    public sealed class KeyDirective : Directive
    {
        public KeyDirective(string key)
        {
            Key = key;
        }

        public string Key { get; set; }
    }
}

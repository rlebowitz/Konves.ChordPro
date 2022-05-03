using ChordPro.Library.Directives;

namespace ChordPro.Library.DirectiveHandlers
{
    public abstract class DirectiveHandler<TDirective> : DirectiveHandler where TDirective : Directive
    {
        public override Type DirectiveType { get { return typeof(TDirective); } }
    }

    public abstract class DirectiveHandler
    {
        public bool TryParse(DirectiveComponents components, out Directive directive)
        {
            directive = null;

            if (components.Key == LongName || components.Key == ShortName)
            {
                if (Value == ComponentPresence.Required && string.IsNullOrWhiteSpace(components.Value))
                {
                    return false;
                }

                if (Value == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.Value))
                {
                    return false;
                }

                if (SubKey == ComponentPresence.Required && string.IsNullOrWhiteSpace(components.SubKey))
                {
                    return false;
                }

                return SubKey == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.SubKey)
                    ? false
                    : TryCreate(components, out directive);
            }
            return false;
        }

        protected abstract bool TryCreate(DirectiveComponents components, out Directive directive);

        protected virtual string GetValue(Directive directive)
        {
            return null;
        }

        protected virtual string GetSubKey(Directive directive)
        {
            return null;
        }

        public string GetString(Directive directive, bool shorten = false)
        {
            Guard.NotNull(directive);
            string key = shorten ? ShortName : LongName;
            string subkey = GetSubKey(directive);
            string value = GetValue(directive);

            string subkeyString = GetSubKeyString(subkey);
            string valueString = GetValueString(value);
            var temp = $"{key}{subkeyString}{valueString}";
            return $"{{{temp}}}";
        }

        internal string GetSubKeyString(string subkey)
        {
            if (string.IsNullOrWhiteSpace(subkey))
            {
                return string.Empty;
            }
            return SubKey switch
            {
                ComponentPresence.Required => $" {subkey}",
                ComponentPresence.Optional => $" {subkey}",
                ComponentPresence.NotAllowed => string.Empty,
                _ => string.Empty,
            };
        }

        internal string GetValueString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return Value switch
            {
                ComponentPresence.Required => $": {value}",
                ComponentPresence.Optional => $": {value}",
                ComponentPresence.NotAllowed => string.Empty,
                _ => string.Empty,
            };
        }

        public abstract ComponentPresence SubKey { get; }
        public abstract ComponentPresence Value { get; }
        public abstract string LongName { get; }
        /// <summary>
        /// Not all ChordPro directives have a short name, therefore return the long name by default.
        /// </summary>
        public virtual string ShortName { get { return LongName; } }
        public abstract Type DirectiveType { get; }
    }

    public enum ComponentPresence
    {
        Required,
        Optional,
        NotAllowed
    }
}

using System;
using System.Text.RegularExpressions;

namespace ChordPro.Lib
{
    public sealed class DirectiveComponents
    {
        public string Key { get; private set; }
        public string SubKey { get; private set; }
        public string Value { get; private set; }
        private static Regex DirectiveRegex { get; } = new Regex(@"^\s*{\s*(?<key>[^\s:}]+)(?:\s+(?<subkey>[^:}]*))?(?:\:(?<value>[^}]+))?}\s*", RegexOptions.Compiled);

       
        public DirectiveComponents(string key, string subKey, string value)
        {
            Key = key;
            SubKey = subKey;
            Value = value;
        }

        public static DirectiveComponents Parse(string s)
        {
            if (TryParse(s, out DirectiveComponents components)) { 
                return components; 
            }

            throw new FormatException("Directive is not in the format {key[ subkey][:value]}");
        }

        public static bool TryParse(string s, out DirectiveComponents components)
        {
            Match match = DirectiveRegex.Match(s);

            if (match == null || string.IsNullOrWhiteSpace(match.Groups["key"].Value))
            {
                components = null;
                return false;
            }
            var key = match.Groups["key"]?.Value;
            var subKey = match.Groups["subkey"]?.Value;
            var value = match.Groups["value"]?.Value;
            if (key == null)
            {
                components = null;
                return false;
            }
            components = new DirectiveComponents(key.ToLower().Trim(), subKey?.Trim(), value?.Trim()); 
            return true;
        }

       
    }
}

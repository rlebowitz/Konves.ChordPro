using ChordPro.Library.DirectiveHandlers;
using System.Collections.Generic;

namespace ChordPro.Library
{
    public sealed class SerializerSettings
    {
        public bool ShortenDirectives { get; set; }
        public List<DirectiveHandler> CustomHandlers { get; set; }
    }
}

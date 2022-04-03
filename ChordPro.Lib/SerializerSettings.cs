using ChordPro.Lib.DirectiveHandlers;
using System.Collections.Generic;

namespace ChordPro.Lib
{
    public sealed class SerializerSettings
    {
        public bool ShortenDirectives { get; set; }
        public List<DirectiveHandler> CustomHandlers { get; set; }
    }
}

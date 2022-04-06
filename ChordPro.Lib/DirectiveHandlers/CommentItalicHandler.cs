﻿using ChordPro.Lib.Directives;

namespace ChordPro.Lib.DirectiveHandlers
{
    public sealed class CommentItalicHandler : DirectiveHandler<CommentItalicDirective>
    {
        private CommentItalicHandler() { }

        public static CommentItalicHandler Instance { get; } = new CommentItalicHandler();

        protected override bool TryCreate(DirectiveComponents components, out Directive directive)
        {
            directive = new CommentItalicDirective(components.Value);
            return true;
        }

        protected override string GetValue(Directive directive)
        {
            return (directive as CommentItalicDirective)?.Text;
        }

        public override string LongName { get { return "comment_italic"; } }
        public override string ShortName { get { return "ci"; } }
        public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
        public override ComponentPresence Value { get { return ComponentPresence.Required; } }
    }
}

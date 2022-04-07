﻿using ChordPro.Library.DirectiveHandlers;

namespace ChordPro.Library
{
    internal static class DirectiveHandlerUtility
    {
        private static IReadOnlyCollection<DirectiveHandler> DefaultDirectiveParsers { get; } = new DirectiveHandler[] {
            ChordColourHandler.Instance,
            ChordFontHandler.Instance,
            ChordSizeHandler.Instance,
            ColumnsHandler.Instance,
            ColumnBreakHandler.Instance,
            CommentHandler.Instance,
            CommentBoxHandler.Instance,
            CommentItalicHandler.Instance,
            DefineHandler.Instance,
            EndOfChorusHandler.Instance,
            EndOfTabHandler.Instance,
            GridHandler.Instance,
            NewPageHandler.Instance,
            NewPhysicalPageHandler.Instance,
            NewSongHandler.Instance,
            NoGridHandler.Instance,
            PageTypeHandler.Instance,
            StartOfChorusHandler.Instance,
            StartOfTabHandler.Instance,
            SubtitleHandler.Instance,
            TextFontHandler.Instance,
            TextSizeHandler.Instance,
            TitleHandler.Instance,
            TitlesHandler.Instance,
        };
        internal static IReadOnlyDictionary<string, DirectiveHandler> GetHandlersDictionaryByName(IEnumerable<DirectiveHandler> customParsers)
        {
            Dictionary<string, DirectiveHandler> index = new Dictionary<string, DirectiveHandler>();

            foreach (DirectiveHandler parser in DefaultDirectiveParsers ?? Enumerable.Empty<DirectiveHandler>())
            {
                index[parser.LongName] = parser;
                index[parser.ShortName] = parser;
            }

            foreach (DirectiveHandler parser in customParsers ?? Enumerable.Empty<DirectiveHandler>())
            {
                index[parser.LongName] = parser;
                index[parser.ShortName] = parser;
            }

            return index;
        }

        internal static IReadOnlyDictionary<Type, DirectiveHandler> GetHandlersDictionaryByType(IEnumerable<DirectiveHandler> customParsers)
        {
            Dictionary<Type, DirectiveHandler> index = new Dictionary<Type, DirectiveHandler>();

            foreach (DirectiveHandler parser in DefaultDirectiveParsers ?? Enumerable.Empty<DirectiveHandler>())
            {
                index[parser.DirectiveType] = parser;
            }

            foreach (DirectiveHandler parser in customParsers ?? Enumerable.Empty<DirectiveHandler>())
            {
                index[parser.DirectiveType] = parser;
            }

            return index;
        }

        
    }
}

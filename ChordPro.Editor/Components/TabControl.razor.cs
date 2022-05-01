using Microsoft.AspNetCore.Components;

namespace ChordPro.Editor.Components
{
    public partial class TabControl : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public TabPage ActivePage { get; set; }
        private List<TabPage> Pages { get; } = new List<TabPage>();

        internal void AddPage(TabPage tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
            {
                ActivePage = tabPage;
            }
            StateHasChanged();
        }
        private string GetButtonClass(TabPage page)
        {
            return page == ActivePage ? "btn-primary" : "btn-secondary";
        }
        void ActivatePage(TabPage page)
        {
            ActivePage = page;
        }
    }
}

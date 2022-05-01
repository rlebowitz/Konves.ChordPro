﻿using Microsoft.AspNetCore.Components;

namespace ChordPro.Editor.Components
{
    public partial class TabPage : ComponentBase
    {
        [CascadingParameter]
        private TabControl Parent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Text { get; set; }
        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
            }
            Parent.AddPage(this);
            base.OnInitialized();
        }
    }
}

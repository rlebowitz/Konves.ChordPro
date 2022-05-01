using Microsoft.AspNetCore.Components;

namespace ChordPro.Editor.Components
{
    public partial class EditorControl : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
    }
}

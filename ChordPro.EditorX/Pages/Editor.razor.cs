using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordPro.Editor.Pages
{
    public partial class Editor
    {
        private string Text { get; set; }
        private string Show { get; set; } = string.Empty;
        private bool Toggler { get; set; } = false;
        private string Style { get; set; } = string.Empty;
        private async Task LoadFile()
        {
            var result = await FilePicker.PickAsync();
            if (result == null)
            {
                return;
            }
            using StreamReader reader = new(await result.OpenReadAsync());
            while (reader.Peek() >= 0)
            {
                Text = await reader.ReadToEndAsync();
            }
        }

        private void Toggle()
        {
            Toggler = !Toggler;
            Show = Toggler ? "show" : string.Empty;
            Style = Toggler ? "position: absolute;inset: 0px auto auto 0px;margin: 0px;transform: translate(0px, 40px);" : string.Empty;
        }
    }
}

using ChordPro.Library;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChordPro.Editor.Pages
{
    public partial class Index
    {
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private IFileSavePicker FileSavePicker { get; set; }
        private string Text { get; set; }
        private string FileName { get; set; }
        private Document Document { get; set; }
        private bool DisplayEditor => !string.IsNullOrEmpty(Text);

        private FilePickerFileType FileTypes { get; } = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
             {
                 { DevicePlatform.WinUI, new[] { "*.pro", "*.cho" } },
                 { DevicePlatform.Android, new[] { "text/plain" } },
             });

        private DialogOptions DialogOptions { get; set; } = new DialogOptions { ShowTitle = true, Style = "min-height:auto;min-width:auto;width:auto" };

        private async Task LoadFile(MenuItemEventArgs args)
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Please select a ChordPro file",
                    FileTypes = FileTypes
                };
                var result = await FilePicker.PickAsync(options);
                if (result == null)
                {
                    return;
                }
                FileName = result.FileName;
                Text = await new StreamReader(await result.OpenReadAsync()).ReadToEndAsync();
            }
            catch (Exception ex)
            {
                await DialogService.OpenAsync("Load File Error", ds => Index.ParseContent(ex.Message), DialogOptions);
            }
        }

        private async Task SaveFile(MenuItemEventArgs args)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(FileName))
                {
                    var filePath = await FileSavePicker.SaveFile(FileName);

                    using StreamWriter writer = new(new FileStream(filePath, FileMode.Create));
                    await writer.WriteAsync(Text);
                    await writer.FlushAsync();
                }
            }
            catch (IOException iox)
            {
                await DialogService.OpenAsync("Save Error", ds => Index.ParseContent(iox.Message), DialogOptions);

            }
        }

        private async Task Parse(MenuItemEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                try
                {
                    Document = ChordProSerializer.Deserialize(new StringReader(Text));
                    var sb = new StringBuilder();
                    using TextWriter writer = new StringWriter(sb);
                    ChordProSerializer.Serialize(Document, writer);
                    Text = sb.ToString();
                }
                catch (FormatException fex)
                {
                    await DialogService.OpenAsync("Parsing Error", ds => ParseContent(fex.Message), DialogOptions);
                }
            }
        }

        private static RenderFragment ParseContent(string content) => builder =>
        {
            builder.OpenElement(0, "div");
            builder.OpenElement(1, "div");
            builder.AddAttribute(2, "class", "row");
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "col-md-12");
            builder.AddContent(5, content);
            builder.CloseElement();
            builder.CloseElement();
            builder.CloseElement();
        };

        private static void Exit(MenuItemEventArgs args)
        {
            Application.Current.Quit();
        }

    }
}

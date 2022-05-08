﻿using ChordPro.Library;
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
        private string Text { get; set; }
        private string FileName { get; set; }
        private Document Document { get; set; }
        private string ErrorMessage { get; set; }
        private bool DisplayEditor => !string.IsNullOrEmpty(Text);
        private bool DisplayError => !string.IsNullOrEmpty(ErrorMessage);
        private FilePickerFileType FileTypes { get; } = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
             {
                 { DevicePlatform.WinUI, new[] { "*.pro", "*.cho" } },
                 { DevicePlatform.Android, new[] { "text/plain" } },
             });

        private async Task LoadFile(MenuItemEventArgs args)
        {
            ErrorMessage = String.Empty;
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
                ErrorMessage = ex.Message;
            }
        }

        private async Task SaveFile(MenuItemEventArgs args)
        {
            ErrorMessage = String.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(FileName))
                {
                    // Act
                    using StreamWriter writer = new(new FileStream(Path.Combine("D:\\Music", FileName), FileMode.Create));
                    await writer.WriteAsync(Text);
                    await writer.FlushAsync();
                }
            }
            catch (IOException iox)
            {
                ErrorMessage = iox.Message;
            }
        }

        private void Parse(MenuItemEventArgs args)
        {
            ErrorMessage = String.Empty;
            if (!string.IsNullOrWhiteSpace(Text))
            {
                try
                {
                    ErrorMessage = string.Empty;
                    Document = ChordProSerializer.Deserialize(new StringReader(Text));
                    var sb = new StringBuilder();
                    using TextWriter writer = new StringWriter(sb);
                    ChordProSerializer.Serialize(Document, writer);
                    Text = sb.ToString();
                }
                catch (FormatException fex)
                {
                    ErrorMessage = fex.Message;
                }
            }
        }

        private static void Exit(MenuItemEventArgs args)
        {
            Application.Current.Quit();
        }

    }
}

using ChordPro.Library;
using System.Text;

namespace ChordPro.Editor.Pages
{
    public partial class Editor
    {
        private string Text { get; set; }
        private bool DisplaySubMenu { get; set; }
        private string FileName { get; set; }
        private Document Document { get; set; }
        private string ErrorMessage { get; set; }

        private const string Style = "position: absolute;inset: 0px auto auto 0px;margin: 0px;transform: translate(0px, 40px);";
        private async Task LoadFile()
        {
            ErrorMessage = String.Empty;
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Please select a ChordPro file"
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
            DisplaySubMenu = false;
        }

        private async Task SaveFile()
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
            DisplaySubMenu = false;
        }

        private void Parse()
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

    }
}

using Windows.Storage.Pickers;
using WindowsFileSavePicker = Windows.Storage.Pickers.FileSavePicker;

namespace ChordPro.Editor.Platforms.Windows
{
    //https://github.com/jfversluis/MauiFolderPickerSample
    public class FileSavePicker : IFileSavePicker
    {
        public async Task<string> SaveFile(string fileName)
        {
            try
            {
                var picker = new WindowsFileSavePicker
                {
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary
                };
                // Dropdown of file types the user can save the file as
                picker.FileTypeChoices.Add("ChordPro", new List<string>() { ".cho", ".pro" });
                // Default file name if the user does not type one in or select a file to replace
                picker.SuggestedFileName = string.IsNullOrWhiteSpace(fileName) ? "New Document" : fileName;

                // Get the current window's HWND by passing in the Window object
                var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;

                // Associate the HWND with the file picker
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

                var result = await picker.PickSaveFileAsync();

                return result.Path;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
    }

}

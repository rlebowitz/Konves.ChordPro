﻿using WindowsFolderPicker = Windows.Storage.Pickers.FolderPicker;


namespace ChordPro.Editor.Platforms.Windows
{
    //https://github.com/jfversluis/MauiFolderPickerSample
    public class FolderPicker : IFolderPicker
    {
        public async Task<string> PickFolder()
        {
            var folderPicker = new WindowsFolderPicker();

            // Get the current window's HWND by passing in the Window object
            var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            var result = await folderPicker.PickSingleFolderAsync();

            return result.Path;
        }
    }

}

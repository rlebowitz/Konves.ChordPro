namespace ChordPro.Editor
{
    public interface IFileSavePicker
    {
        Task<string> SaveFile(string fileName);
    }


}
namespace ChordPro.DB
{
    public class Lyricist
    {
        public int LyricistId { get; set; }
        public string LyricistName { get; set; }
        public string Description { get; set; }
        public List<SongLyricist> SongLyricists { get; set; } 
    }
}

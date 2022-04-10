namespace ChordPro.DB
{
    public class SongLyricist
    {
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int LyricistId { get; set; }
        public Lyricist Lyricist { get; set; }

    }
}

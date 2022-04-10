namespace ChordPro.DB
{
    public class SongComposer
    {
        public int SongId { get; set; }
        public Song Song { get; set; }
        public int ComposerId { get; set; }
        public Composer Composer { get; set; }
    }
}

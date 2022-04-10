namespace ChordPro.DB
{
    public class SongTitle
    {
        public int SongTitleId { get; set; }
        public string Title { get; set; }
        public bool IsSortTitle { get; set; }
        public bool IsSubtitle { get; set; }

        // used to establish the one to many relationship
        public int SongId { get; set;}
        public Song Song { get; set; }

    }
}

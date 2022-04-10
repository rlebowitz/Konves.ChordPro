namespace ChordPro.DB
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Description { get; set; }
        public List<SongArtist> SongArtists { get; set; }

    }
}

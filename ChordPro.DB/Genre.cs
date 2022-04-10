namespace ChordPro.DB
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<SongGenre> SongGenres { get; set; }
    }
}

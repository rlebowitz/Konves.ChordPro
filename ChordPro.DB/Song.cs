namespace ChordPro.DB
{
    public class Song
    {
        public int SongId { get; set; }
        public string ChordPro { get; set; }
        public string Link { get; set; }
        public int? Copyright_Year { get; set; }
        public string Copyright_Owner { get; set; }
        public string Key { get; set; }
        public int? Capo { get; set; }
        public string Album_Name { get; set; }
        /// <summary>
        /// One to many relationship
        /// </summary>
        public List<SongTitle> SongTitles { get; set; } 
        public List<SongArtist> SongArtists { get; set; } 
        public List<SongComposer> SongComposers { get; set; }
        public List<SongLyricist> SongLyricists { get; set; } 
        public List<SongGenre> SongGenres { get; set; } 

    }
}

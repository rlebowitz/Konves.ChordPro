namespace ChordPro.DB
{
    public class Composer
    {
        public int ComposerId { get; set; }
        public string ComposerName { get; set; }
        public string Description { get; set; }
        public List<SongComposer> SongComposers { get; set; }


    }
}

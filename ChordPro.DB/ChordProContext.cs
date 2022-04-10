using Microsoft.EntityFrameworkCore;

namespace ChordPro.DB
{
    public class ChordProContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongTitle> SongTitles { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Composer> Composers { get; set; }
        public DbSet<Lyricist> Lyricists { get; set; }

        private const string ConnectionString = "server=192.168.68.126;database=ChordPro;user=chordpro;password=ch0rdpr0";
        private MySqlServerVersion Version { get; } = new MySqlServerVersion(new Version(10, 3));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, Version);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasKey(e => e.SongId);
            });

            modelBuilder.Entity<SongTitle>(entity =>
            {
                entity.HasIndex(e => e.Title);
                entity.HasKey(e => e.SongTitleId);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.IsSortTitle).HasDefaultValue(true);
                entity.Property(e => e.IsSubtitle).HasDefaultValue(false);
                entity.HasOne(e => e.Song).WithMany(p => p.SongTitles);
            });

            // Song - Composer Many to Many Relationship

            modelBuilder.Entity<Composer>(entity =>
            {
                entity.HasKey(e => e.ComposerId);
                entity.HasIndex(e => e.ComposerName);
            });

            modelBuilder.Entity<SongComposer>().HasKey(k => new { k.SongId, k.ComposerId });

            modelBuilder.Entity<SongComposer>()
                .HasOne(sc => sc.Song)
                .WithMany(s => s.SongComposers)
                .HasForeignKey(sc => sc.SongId);

            modelBuilder.Entity<SongComposer>()
               .HasOne(sc => sc.Composer)
               .WithMany(s => s.SongComposers)
               .HasForeignKey(sc => sc.ComposerId);

            // Song - Artist Many to Many Relationship

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.ArtistId);
                entity.HasIndex(e => e.ArtistName);
            });

            modelBuilder.Entity<SongArtist>().HasKey(k => new { k.SongId, k.ArtistId });

            modelBuilder.Entity<SongArtist>()
                .HasOne(sa => sa.Song)
                .WithMany(s => s.SongArtists)
                .HasForeignKey(sa => sa.SongId);

            modelBuilder.Entity<SongArtist>()
               .HasOne(sa => sa.Artist)
               .WithMany(s => s.SongArtists)
               .HasForeignKey(sa => sa.ArtistId);

            // Song - Lyricist Many to Many Relationship

            modelBuilder.Entity<Lyricist>(entity =>
            {
                entity.HasKey(e => e.LyricistId);
                entity.HasIndex(e => e.LyricistName);
            });

            modelBuilder.Entity<SongLyricist>().HasKey(k => new { k.SongId, k.LyricistId });

            modelBuilder.Entity<SongLyricist>()
                .HasOne(sl => sl.Song)
                .WithMany(s => s.SongLyricists)
                .HasForeignKey(sl => sl.SongId);

            modelBuilder.Entity<SongLyricist>()
               .HasOne(sl => sl.Lyricist)
               .WithMany(s => s.SongLyricists)
               .HasForeignKey(sl => sl.LyricistId);

            // Song - Genre Many to Many Relationship

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreId);
                entity.HasIndex(e => e.Name);
            });

            modelBuilder.Entity<SongGenre>().HasKey(k => new { k.SongId, k.GenreId });

            modelBuilder.Entity<SongGenre>()
                .HasOne(sg => sg.Song)
                .WithMany(s => s.SongGenres)
                .HasForeignKey(sg => sg.SongId);

            modelBuilder.Entity<SongGenre>()
               .HasOne(sg => sg.Genre)
               .WithMany(s => s.SongGenres)
               .HasForeignKey(sg => sg.GenreId);

        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace RewatchIt.Data
{
    public class WatchedMovie
    {
        #region Properties

        public int ID { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }

        #endregion
    }

    public class WatchedMovieContext : DbContext
    {
        #region Constructors

        public WatchedMovieContext(DbContextOptions<WatchedMovieContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<WatchedMovie> Movies { get; set; }

        #endregion

        #region Event Handlers

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchedMovie>().ToTable("WatchedMovie");
        }

        #endregion
    }
}
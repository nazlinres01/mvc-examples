using Microsoft.EntityFrameworkCore;

namespace FilmListesiUygulamasi
{
    public class FilmContext : DbContext
    {
        public DbSet<Film>? Films { get; set; }
        public DbSet<Models.Users>? Users { get; set; }
        public DbSet<Models.UserNotes> UserNotes { get; set; }

        public object Ratings { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-FCU915E;Database=database;User Id=username;Password=913782; TrustServerCertificate=true");
        }
    }
}

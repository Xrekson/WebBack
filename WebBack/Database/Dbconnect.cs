using dotenv.net.Utilities;
using Microsoft.EntityFrameworkCore;
using WebBack.Model;

namespace WebBack.Database
{
    public partial class Dbconnect : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Dbconnect(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(EnvReader.GetStringValue("DB_Connection"));
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<City> Cites { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(e => new { e.Email, e.password });
            modelBuilder.Entity<Users>().HasIndex(i => i.Mobile);
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

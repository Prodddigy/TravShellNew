
using Microsoft.EntityFrameworkCore;

using TravShell.Core.Entities;
namespace TravShell.Infrastructure.Data;
public class DataContext : DbContext 
{


    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }


    public DbSet<User> Users { get; set; }

    public DbSet<Trip> Trips { get; set; }

	public DbSet<Attraction> Attractions { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public DbSet<Tag> Tags { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
	=> optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TravShellDB;Integrated Security=true;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.User)
            .WithMany(x => x.ReviewList)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.Trip)
            .WithMany(x => x.Reviews)
            .OnDelete(DeleteBehavior.NoAction);

    }
}



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


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
	=> optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TravShellDB;Integrated Security=true;Trusted_Connection=true;TrustServerCertificate=true;");

}


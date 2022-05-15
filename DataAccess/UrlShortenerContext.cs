using Microsoft.EntityFrameworkCore;
using Models.Concerete;

namespace DataAccess;

public class UrlShortenerContext : DbContext
{
    public virtual DbSet<UrlShorteningModel> UrlShortenings { get; set; }

    public UrlShortenerContext(string connectionString) : base(GetOptions(connectionString))
    {
        this.Database.EnsureCreated();
    }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CashBook.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CashBookDbContext>
    {
        public CashBookDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CashBookDbContext>();
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CashBook.API")).AddJsonFile("appsettings.Development.json").Build();

            builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new CashBookDbContext(builder.Options);
        }
    }
}
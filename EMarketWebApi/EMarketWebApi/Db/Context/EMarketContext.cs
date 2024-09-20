using EMarketWebApi.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace EMarketWebApi.Db.Context
{
    public class EMarketContext:DbContext
    {
        public EMarketContext(DbContextOptions<EMarketContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}

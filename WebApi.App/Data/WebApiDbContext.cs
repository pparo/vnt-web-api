using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.App.Data
{
    public class WebApiDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
            
        }
    }
}
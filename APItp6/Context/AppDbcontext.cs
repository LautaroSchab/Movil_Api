using APItp6.Models;
using Microsoft.EntityFrameworkCore;
namespace APItp6.Context
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}

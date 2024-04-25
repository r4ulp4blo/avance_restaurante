using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace restaurante.Models
{
    public class restauranteDbContext : DbContext
    {
        public  restauranteDbContext(DbContextOptions options) : base(options) { }

        public DbSet<plato> plato { get; set; }
        public DbSet<mesa> mesa { get; set; }
        public DbSet<detallePedido> detallePedido { get; set; }
        public DbSet<caja> caja { get; set; }
        public DbSet<factura> factura { get; set; }
    }
}

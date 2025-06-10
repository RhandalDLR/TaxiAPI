using Microsoft.EntityFrameworkCore;

namespace Taxi.Server.Models
{
    public class TaxiContext:DbContext
    {
        public TaxiContext(DbContextOptions<TaxiContext> options) : base(options)
        { 

        }
        public DbSet<Vehiculo> Taxi { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GrupoUsuarios> GrupoUsuarios { get; set; }
        public DbSet<GrupoUsuariosDetalle> GrupoUsuariosDetalles { get; set; }
        public DbSet<Viaje>Viajes { get; set; }
        
    }
}

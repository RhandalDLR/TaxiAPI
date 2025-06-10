namespace Taxi.Server.Models
{
    public class GrupoUsuariosDetalle
    {
        public int id { get; set; } 
        public Usuario Usuario { get; set; }
        public GrupoUsuarios GrupoUsuarios { get; set; } 


    }
}

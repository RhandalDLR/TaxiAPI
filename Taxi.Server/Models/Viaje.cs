using System.Globalization;

namespace Taxi.Server.Models
{
    public class Viaje
    {
        public int id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public int Calificacion { get; set; }
        public Vehiculo Taxi { get; set; }
        public Usuario Usuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proy01.Models
{
    /// Modelo de presentacion para una fila de la lista de alquileres
    /// ("Mis alquileres" / "Gestionar alquileres").
    public class AlquilerListaItem
    {
        public int IdAlquiler { get; set; }
        public string NombreJugador { get; set; }
        public string NombreSucursal { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public decimal CostoTotal { get; set; }
        public string EstadoTexto { get; set; }
    }
}
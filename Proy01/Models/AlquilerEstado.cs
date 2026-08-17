using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proy01.Models
{
    public static class AlquilerEstado
    {
        /// Calcula el texto de estado que debe mostrarse en pantalla para un
        /// alquiler, a partir del estado almacenado en BD y de sus fechas
        /// El estado guardado en la tabla Alquiler solo distingue
        /// Activo ('A') e Inactivo ('I'); esta clase deriva el texto mas
        /// descriptivo que ve el usuario: Cancelado, Finalizado, En proceso o
        /// En espera.
        public static string CalcularTexto(char estado, DateTime fechaInicio, DateTime fechaDevolucion)
        {
            var ahora = DateTime.Now;

            if (estado == 'I') return "Cancelado";
            if (fechaDevolucion < ahora) return "Finalizado";
            if (fechaInicio <= ahora) return "En proceso";
            return "En espera";
        }
    }
}
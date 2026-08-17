using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proy01.Models
{
    /// Representa al jugador autenticado. Se guarda directamente en
    /// Session["jugadorSesion"] luego de un login exitoso y se
    /// lee desde ahi en cada pagina que lo necesite
    public class JugadorSesion
    {
        public int IdJugador { get; set; }
        public string NombreCompleto { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModels;

namespace Proy01.Models
{
    /// Fabrica centralizada para obtener conexiones a la base de datos del
    /// sistema (LinqToDB), usando el nombre de cadena de conexion
    /// configurado en Web.config ("MyDatabase").
    public static class ConexionBD
    {
        private const string NombreConexion = "MyDatabase";

        /// Crea una nueva conexion a la base de datos RentaVideojuegos.
        public static RentaVideojuegosDB ObtenerConexion()
        {
            return new RentaVideojuegosDB(NombreConexion);
        }
    }
}
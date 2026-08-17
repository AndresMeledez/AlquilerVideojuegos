using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proy01.Models;

namespace Proy01.Pages
{
    public partial class Error : System.Web.UI.Page
    {
        /// Pantalla generica de error, utilizada cuando ocurre una excepcion al
        /// procesar una operacion sobre la base de datos.
        protected void Page_Load(object sender, EventArgs e)
        {
            // "Regresar" lleva al listado de alquileres correspondiente segun el
            // tipo de usuario.
            if (Session["jugadorSesion"] is JugadorSesion jugador)
            {
                lnkRegresar.NavigateUrl = jugador.EsAdministrador
                    ? "~/Pages/Alquiler/GestionarAlquileres.aspx"
                    : "~/Pages/Alquiler/MisAlquileres.aspx";
            }
        }
    }
}
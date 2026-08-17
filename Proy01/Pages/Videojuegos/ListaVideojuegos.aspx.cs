using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using Proy01.Models;

namespace Proy01.Pages.Videojuegos
{
    /// Listado de videojuegos, exclusivo para administradores.
    /// Ordenado por nombre de sucursal, estado y titulo (lo resuelve el
    /// stored procedure SP_Videojuego_Listar).
    public partial class ListaVideojuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["jugadorSesion"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            var jugador = (JugadorSesion)Session["jugadorSesion"];
            if (!jugador.EsAdministrador)
            {
                Response.Redirect("~/Pages/Alquiler/MisAlquileres.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarVideojuegos();
            }
        }

        private void CargarVideojuegos()
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    gvVideojuegos.DataSource = db.SpVideojuegoListar().ToList();
                    gvVideojuegos.DataBind();
                }
            }
            catch
            {
                Response.Redirect("~/Pages/Error.aspx");
            }
        }
    }
}
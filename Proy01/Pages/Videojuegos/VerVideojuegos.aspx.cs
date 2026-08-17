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
    /// Catalogo de solo lectura con los videojuegos activos disponibles en
    /// todas las sucursales. No forma parte de los requerimientos
    /// obligatorios del enunciado; se agrega como mejora de usabilidad para que el jugador
    /// pueda ver que hay disponible antes de rentar.
    public partial class VerVideojuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["jugadorSesion"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
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
                    gvVideojuegos.DataSource = db.SpVideojuegoListarActivos().ToList();
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
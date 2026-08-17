using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proy01.Models;

namespace Proy01
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["jugadorSesion"] != null)
            {
                JugadorSesion jugador = (JugadorSesion)Session["jugadorSesion"];

                lblNombreJugador.Text = jugador.NombreCompleto;
                phMenuAdministrador.Visible = jugador.EsAdministrador;
                phMenuJugador.Visible = !jugador.EsAdministrador;
                btnCerrarSesion.Visible = true;
            }
        }

        /// <summary>Cierra la sesion activa y regresa al login (RF-001-g).</summary>
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Pages/Login.aspx");
        }
    }
}
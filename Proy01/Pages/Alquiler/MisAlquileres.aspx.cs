using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using Proy01.Models;

namespace Proy01.Pages
{
    /// "Mis alquileres": lista los alquileres del usuario
    /// autenticado (jugador o administrador), ordenados descendentemente
    /// por idAlquiler.
    public partial class MisAlquileres : System.Web.UI.Page
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
                CargarAlquileres();
            }
        }

        private void CargarAlquileres()
        {
            var jugador = (JugadorSesion)Session["jugadorSesion"];

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var datos = db.SpAlquilerListarPorJugador(jugador.IdJugador)
                        .Select(a => new AlquilerListaItem
                        {
                            IdAlquiler = a.IdAlquiler,
                            NombreJugador = a.NombreCompleto,
                            NombreSucursal = a.NombreSucursal,
                            Titulo = a.Titulo,
                            FechaInicio = a.FechaInicio,
                            FechaDevolucion = a.FechaDevolucion,
                            CostoTotal = a.CostoTotal,
                            EstadoTexto = AlquilerEstado.CalcularTexto(a.Estado, a.FechaInicio, a.FechaDevolucion)
                        })
                        .ToList();

                    gvAlquileres.DataSource = datos;
                    gvAlquileres.DataBind();
                }
            }
            catch
            {
                Response.Redirect("~/Pages/Error.aspx");
            }
        }
    }
}
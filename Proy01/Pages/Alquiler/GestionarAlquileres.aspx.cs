using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using Proy01.Models;

namespace Proy01.Pages
{
    /// "Gestionar alquileres": lista todos los alquileres del
    /// sistema, exclusiva para administradores. Permite filtrar de forma
    /// opcional por jugador y, si se indican ambas fechas, por el rango de
    /// fecha de inicio / devolucion.
    public partial class GestionarAlquileres : System.Web.UI.Page
    {
        private const string FormatoFecha = "yyyy-MM-dd";

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
                CargarJugadores();
                CargarAlquileres(null, null, null);
            }
        }

        private void CargarJugadores()
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    ddlJugador.DataSource = db.SpJugadorListarActivos().ToList();
                    ddlJugador.DataBind();
                    ddlJugador.Items.Insert(0, new ListItem("Todos los jugadores", ""));
                }
            }
            catch (Exception ex)
            {
                Session["UltimoError"] = ex.Message;
                Response.Redirect("~/Pages/Error.aspx");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            litMensajeFiltro.Text = "";

            int? idJugador = string.IsNullOrEmpty(ddlJugador.SelectedValue) ? (int?)null : int.Parse(ddlJugador.SelectedValue);

            bool tieneInicio = DateTime.TryParseExact(txtFechaInicio.Text, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaInicio);
            bool tieneDevolucion = DateTime.TryParseExact(txtFechaDevolucion.Text, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaDevolucion);

            if (tieneInicio ^ tieneDevolucion)
            {
                MostrarMensajeFiltro("Para filtrar por fecha debe indicar tanto la fecha de inicio como la fecha de devolución.");
                return;
            }

            DateTime? filtroInicio = null;
            DateTime? filtroDevolucion = null;

            if (tieneInicio && tieneDevolucion)
            {
                if (fechaDevolucion.Date < fechaInicio.Date)
                {
                    MostrarMensajeFiltro("La fecha de devolución debe ser mayor o igual a la fecha de inicio.");
                    return;
                }

                filtroInicio = fechaInicio;
                filtroDevolucion = fechaDevolucion;
            }

            CargarAlquileres(idJugador, filtroInicio, filtroDevolucion);
        }

        private void CargarAlquileres(int? idJugador, DateTime? fechaInicio, DateTime? fechaDevolucion)
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var datos = db.SpAlquilerListarTodos(idJugador, fechaInicio, fechaDevolucion)
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
            catch (Exception ex)
            {
                Session["UltimoError"] = ex.Message;
                Response.Redirect("~/Pages/Error.aspx");
            }
        }

        private void MostrarMensajeFiltro(string mensaje)
        {
            litMensajeFiltro.Text = "<div class='alert alert-warning mt-2'>" + mensaje + "</div>";
        }
    }
}
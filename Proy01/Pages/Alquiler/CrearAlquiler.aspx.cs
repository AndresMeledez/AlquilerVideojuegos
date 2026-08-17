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
    /// Creacion de un nuevo alquiler. Valida disponibilidad de
    /// videojuegos en la sucursal seleccionada para el rango de fechas
    /// indicado y asigna automaticamente el videojuego con menor cantidad
    /// de alquileres registrados, segun el algoritmo del enunciado.
    public partial class CrearAlquiler : System.Web.UI.Page
    {
        private const string FormatoFecha = "yyyy-MM-dd";
        private JugadorSesion jugadorSesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["jugadorSesion"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            jugadorSesion = (JugadorSesion)Session["jugadorSesion"];
            lnkRegresar.NavigateUrl = jugadorSesion.EsAdministrador
                ? "~/Pages/Alquiler/GestionarAlquileres.aspx"
                : "~/Pages/Alquiler/MisAlquileres.aspx";

            if (!IsPostBack)
            {
                CargarSucursales();
                CargarJugadores();
            }
        }

        private void CargarSucursales()
        {
            using (var db = ConexionBD.ObtenerConexion())
            {
                var sucursales = db.SpSucursalListarActivas().ToList();
                ddlSucursal.DataSource = sucursales;
                ddlSucursal.DataBind();
                ddlSucursal.Items.Insert(0, new ListItem("Seleccione una sucursal", ""));
            }
        }

        private void CargarJugadores()
        {
            using (var db = ConexionBD.ObtenerConexion())
            {
                var jugadores = db.SpJugadorListarActivos().ToList();
                ddlJugador.DataSource = jugadores;
                ddlJugador.DataBind();
            }

            if (!jugadorSesion.EsAdministrador)
            {
                // El jugador que no es administrador solo puede crear alquileres a su propio nombre.
                ddlJugador.SelectedValue = jugadorSesion.IdJugador.ToString();
                ddlJugador.Enabled = false;
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVideojuegosDisponibles();
        }

        protected void Fechas_TextChanged(object sender, EventArgs e)
        {
            CargarVideojuegosDisponibles();
        }

        protected void ModoAsignacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVideojuegosDisponibles();
        }

        /// Refresca la seccion de asignacion de videojuego segun la sucursal,
        /// las fechas y el modo elegido (rblModoAsignacion):
        /// - Automatica / Aleatoria: se muestra solo la lista informativa de
        ///   disponibles (la eleccion real ocurre al guardar).
        /// - Manual: se muestra un combo para que el usuario elija el
        ///   videojuego entre los disponibles
        private void CargarVideojuegosDisponibles()
        {
            litVideojuegosDisponibles.Text = "";
            pnlSeleccionManual.Visible = false;
            rfvVideojuego.Enabled = false;

            if (string.IsNullOrEmpty(ddlSucursal.SelectedValue)) return;

            int idSucursal = int.Parse(ddlSucursal.SelectedValue);
            DateTime? fechaInicio = ParseFechaONull(txtFechaInicio.Text);
            DateTime? fechaDevolucion = ParseFechaONull(txtFechaDevolucion.Text);
            bool esManual = rblModoAsignacion.SelectedValue == "Manual";

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var disponibles = db.SpVideojuegoListarDisponibles(idSucursal, fechaInicio, fechaDevolucion).ToList();

                    if (esManual)
                    {
                        pnlSeleccionManual.Visible = true;
                        rfvVideojuego.Enabled = true;
                        ddlVideojuego.DataSource = disponibles;
                        ddlVideojuego.DataBind();
                        return;
                    }

                    if (disponibles.Count == 0)
                    {
                        litVideojuegosDisponibles.Text = "<p class='small mt-2 mb-0'>No hay videojuegos disponibles en esta sucursal" +
                            (fechaInicio.HasValue && fechaDevolucion.HasValue ? " para las fechas indicadas." : ".") + "</p>";
                        return;
                    }

                    var sb = new System.Text.StringBuilder();
                    sb.Append("<div class='mt-2'>");
                    sb.Append("<p class='form-label small mb-1'>Videojuegos disponibles en esta sucursal:</p>");
                    sb.Append("<ul class='mb-0 ps-3 small'>");
                    foreach (var v in disponibles)
                    {
                        sb.Append("<li>").Append(System.Web.HttpUtility.HtmlEncode(v.Titulo)).Append("</li>");
                    }
                    sb.Append("</ul></div>");

                    litVideojuegosDisponibles.Text = sb.ToString();
                }
            }
            catch
            {
                litVideojuegosDisponibles.Text = "";
            }
        }

        private DateTime? ParseFechaONull(string texto)
        {
            return DateTime.TryParseExact(texto, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d)
                ? (DateTime?)d
                : null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            litMensaje.Text = "";

            if (!Page.IsValid) return;

            if (!DateTime.TryParseExact(txtFechaInicio.Text, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaInicio) ||
                !DateTime.TryParseExact(txtFechaDevolucion.Text, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaDevolucion))
            {
                MostrarError("Las fechas indicadas no son válidas.");
                return;
            }

            if (fechaInicio.Date <= DateTime.Now.Date)
            {
                MostrarError("La fecha de inicio no puede ser igual o menor a la fecha actual.");
                return;
            }

            if (fechaDevolucion.Date < fechaInicio.Date)
            {
                MostrarError("La fecha de devolución no puede ser menor a la fecha de inicio.");
                return;
            }

            int idJugador = jugadorSesion.EsAdministrador ? int.Parse(ddlJugador.SelectedValue) : jugadorSesion.IdJugador;
            int idSucursal = int.Parse(ddlSucursal.SelectedValue);
            string modo = rblModoAsignacion.SelectedValue;

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    int idVideojuegoElegido;

                    if (modo == "Manual")
                    {
                        if (string.IsNullOrEmpty(ddlVideojuego.SelectedValue))
                        {
                            MostrarError("Debe seleccionar un videojuego.");
                            return;
                        }

                        idVideojuegoElegido = int.Parse(ddlVideojuego.SelectedValue);

                        // Se revalida la disponibilidad real al guardar, por si cambio algo
                        // entre que se cargo la lista y el momento de dar clic en Guardar.
                        var disponiblesActual = db.SpVideojuegoListarDisponibles(idSucursal, fechaInicio, fechaDevolucion).ToList();
                        if (!disponiblesActual.Any(v => v.IdVideojuego == idVideojuegoElegido))
                        {
                            MostrarError("El videojuego seleccionado ya no está disponible para esas fechas. Elige otro de la lista.");
                            CargarVideojuegosDisponibles();
                            return;
                        }
                    }
                    else if (modo == "Aleatoria")
                    {
                        var disponiblesActual = db.SpVideojuegoListarDisponibles(idSucursal, fechaInicio, fechaDevolucion).ToList();
                        if (disponiblesActual.Count == 0)
                        {
                            MostrarError("No hay videojuegos disponibles en la sucursal seleccionada para el rango de fechas indicado.");
                            return;
                        }

                        var random = new Random();
                        idVideojuegoElegido = disponiblesActual[random.Next(disponiblesActual.Count)].IdVideojuego;
                    }
                    else
                    {
                        // Automatica: el que tiene menos alquileres registrados en su historial (RF-003 original).
                        var disponible = db.SpVideojuegoConsultarDisponible(idSucursal, fechaInicio, fechaDevolucion).FirstOrDefault();
                        if (disponible == null)
                        {
                            MostrarError("No hay videojuegos disponibles en la sucursal seleccionada para el rango de fechas indicado.");
                            return;
                        }

                        idVideojuegoElegido = disponible.IdVideojuego;
                    }

                    int? idAlquilerCreado = null;
                    db.SpAlquilerCrear(idJugador, idVideojuegoElegido, fechaInicio, fechaDevolucion, jugadorSesion.IdJugador, ref idAlquilerCreado);

                    Response.Redirect("~/Pages/Alquiler/DetalleAlquiler.aspx?id=" + idAlquilerCreado);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Response.Redirect lanza esta excepcion internamente; no es un error real, se debe relanzar.
                throw;
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo guardar el alquiler. Detalle: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            litMensaje.Text = "<div class='alert alert-warning mt-2'>" + mensaje + "</div>";
        }
    }
}
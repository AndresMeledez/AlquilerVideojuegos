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
    /// Modificacion de un alquiler existente. Valida las reglas de
    /// acceso segun el tipo de usuario y el estado/fechas del alquiler, y
    /// recalcula el total de dias y el costo total al guardar.
    public partial class EditarAlquiler : System.Web.UI.Page
    {
        private const string FormatoFecha = "yyyy-MM-dd";
        private int idAlquiler;
        private JugadorSesion jugadorSesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["jugadorSesion"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            jugadorSesion = (JugadorSesion)Session["jugadorSesion"];
            string urlListado = jugadorSesion.EsAdministrador
                ? "~/Pages/Alquiler/GestionarAlquileres.aspx"
                : "~/Pages/Alquiler/MisAlquileres.aspx";
            lnkRegresar.NavigateUrl = urlListado;

            if (!int.TryParse(Request.QueryString["id"], out idAlquiler))
            {
                Response.Redirect(urlListado);
                return;
            }

            if (!IsPostBack)
            {
                CargarAlquiler(urlListado);
            }
        }

        private void CargarAlquiler(string urlListado)
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var alquiler = db.SpAlquilerConsultarDetalle(idAlquiler).FirstOrDefault();

                    if (alquiler == null)
                    {
                        Response.Redirect(urlListado);
                        return;
                    }

                    if (!jugadorSesion.EsAdministrador && alquiler.IdJugador != jugadorSesion.IdJugador)
                    {
                        Response.Redirect(urlListado);
                        return;
                    }

                    var ahora = DateTime.Now;

                    // El alquiler cancelado o ya devuelto no se puede editar.
                    if (alquiler.Estado == 'I' || alquiler.FechaDevolucion <= ahora)
                    {
                        Response.Redirect(urlListado);
                        return;
                    }

                    // Un jugador (no administrador) no puede editar un alquiler que ya esta en proceso.
                    bool enProceso = alquiler.FechaInicio <= ahora && alquiler.FechaDevolucion > ahora;
                    if (enProceso && !jugadorSesion.EsAdministrador)
                    {
                        Response.Redirect("~/Pages/Alquiler/MisAlquileres.aspx");
                        return;
                    }

                    litIdAlquiler.Text = alquiler.IdAlquiler.ToString();
                    litResumen.Text = alquiler.Titulo + " (" + alquiler.NombreSucursal + ") — " + alquiler.NombreCompleto;
                    txtFechaInicio.Text = alquiler.FechaInicio.ToString(FormatoFecha);
                    txtFechaDevolucion.Text = alquiler.FechaDevolucion.ToString(FormatoFecha);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Session["UltimoError"] = ex.Message;
                Response.Redirect("~/Pages/Error.aspx");
            }
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

            var ahora = DateTime.Now;

            if (fechaInicio.Date <= ahora.Date)
            {
                MostrarError("La fecha de inicio no puede ser igual o menor a la fecha actual.");
                return;
            }

            if (fechaDevolucion.Date <= ahora.Date)
            {
                MostrarError("La fecha de devolución no puede ser igual o menor a la fecha actual.");
                return;
            }

            if (fechaDevolucion.Date < fechaInicio.Date)
            {
                MostrarError("La fecha de devolución no puede ser menor a la fecha de inicio.");
                return;
            }

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    db.SpAlquilerModificar(idAlquiler, fechaInicio, fechaDevolucion, jugadorSesion.IdJugador);
                }

                Response.Redirect("~/Pages/Alquiler/DetalleAlquiler.aspx?id=" + idAlquiler);
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Session["UltimoError"] = ex.Message;
                Response.Redirect("~/Pages/Error.aspx");
            }
        }

        private void MostrarError(string mensaje)
        {
            litMensaje.Text = "<div class='alert alert-warning mt-2'>" + mensaje + "</div>";
        }
    }
}
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
    /// Consulta de detalle de un alquiler junto con su bitacora, mostrada
    /// como comprobante. Tambien expone los botones de Editar y
    /// Cancelar segun las reglas de negocio, y procesa la cancelacion
    public partial class DetalleAlquiler : System.Web.UI.Page
    {
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

            if (!int.TryParse(Request.QueryString["id"], out idAlquiler))
            {
                Response.Redirect(UrlListado(jugadorSesion));
                return;
            }

            if (!IsPostBack)
            {
                CargarDetalle();
            }
        }

        private void CargarDetalle()
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var alquiler = db.SpAlquilerConsultarDetalle(idAlquiler).FirstOrDefault();

                    if (alquiler == null)
                    {
                        Response.Redirect(UrlListado(jugadorSesion));
                        return;
                    }

                    // Un jugador regular solo puede ver el detalle de sus propios alquileres.
                    if (!jugadorSesion.EsAdministrador && alquiler.IdJugador != jugadorSesion.IdJugador)
                    {
                        Response.Redirect(UrlListado(jugadorSesion));
                        return;
                    }

                    litIdAlquiler.Text = alquiler.IdAlquiler.ToString();
                    litSucursal.Text = alquiler.NombreSucursal;
                    litVideojuego.Text = alquiler.Titulo;
                    litJugador.Text = alquiler.NombreCompleto;
                    litFechaInicio.Text = alquiler.FechaInicio.ToString("dd/MM/yyyy");
                    litFechaDevolucion.Text = alquiler.FechaDevolucion.ToString("dd/MM/yyyy");
                    litDias.Text = alquiler.TotalDiasAlquiler.ToString();
                    litCostoTotal.Text = alquiler.CostoTotal.ToString("C2");
                    litEstado.Text = AlquilerEstado.CalcularTexto(alquiler.Estado, alquiler.FechaInicio, alquiler.FechaDevolucion);

                    var bitacora = db.SpBitacoraListarPorAlquiler(idAlquiler).ToList();
                    gvBitacora.DataSource = bitacora;
                    gvBitacora.DataBind();

                    ConfigurarBotones(alquiler);
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

        private void ConfigurarBotones(RentaVideojuegosDBStoredProcedures.SpAlquilerConsultarDetalleResult alquiler)
        {
            var ahora = DateTime.Now;
            bool estaActivo = alquiler.Estado == 'A';

            bool puedeEditar = estaActivo && (
                (jugadorSesion.EsAdministrador && alquiler.FechaDevolucion > ahora) ||
                (!jugadorSesion.EsAdministrador && alquiler.FechaInicio > ahora)
            );

            bool puedeCancelar = estaActivo && alquiler.FechaInicio > ahora;

            lnkEditar.Visible = puedeEditar;
            lnkEditar.NavigateUrl = "~/Pages/Alquiler/EditarAlquiler.aspx?id=" + idAlquiler;

            btnCancelar.Visible = puedeCancelar;

            lnkRegresar.NavigateUrl = UrlListado(jugadorSesion);
        }

        /// <summary>Procesa la cancelacion del alquiler actual (RF-005).</summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    db.SpAlquilerCancelar(idAlquiler, jugadorSesion.IdJugador);
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

        private string UrlListado(JugadorSesion jugador)
        {
            return jugador.EsAdministrador
                ? "~/Pages/Alquiler/GestionarAlquileres.aspx"
                : "~/Pages/Alquiler/MisAlquileres.aspx";
        }
    }
}
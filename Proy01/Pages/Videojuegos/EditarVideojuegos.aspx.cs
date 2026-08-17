using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using Proy01.Models;

namespace Proy01.Pages.Videojuegos
{
    /// Edicion de un videojuego existente. La sucursal no se
    /// puede modificar. Bloquea por completo el acceso al formulario si el
    /// videojuego esta inactivo o si tiene un alquiler en proceso.
    public partial class EditarVideojuegos : System.Web.UI.Page
    {
        private const string FormatoFecha = "yyyy-MM-dd";
        private int idVideojuego;

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

            if (!int.TryParse(Request.QueryString["id"], out idVideojuego))
            {
                Response.Redirect("~/Pages/Videojuegos/ListaVideojuegos.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarVideojuego();
            }
        }

        private void CargarVideojuego()
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var videojuego = db.SpVideojuegoConsultarPorId(idVideojuego).FirstOrDefault();

                    if (videojuego == null)
                    {
                        Response.Redirect("~/Pages/Videojuegos/ListaVideojuegos.aspx");
                        return;
                    }

                    if (videojuego.Estado == 'I')
                    {
                        BloquearFormulario("Este videojuego está inactivo y no puede modificarse.");
                        return;
                    }

                    bool tieneAlquilerActivo = db.SpVideojuegoTieneAlquilerActivo(idVideojuego).First().TieneAlquilerActivo == 1;
                    if (tieneAlquilerActivo)
                    {
                        BloquearFormulario("Este videojuego tiene un alquiler en proceso y no puede modificarse.");
                        return;
                    }

                    txtSucursal.Text = videojuego.NombreSucursal;
                    txtTitulo.Text = videojuego.Titulo;
                    txtDescripcion.Text = videojuego.Descripcion;
                    txtCategoria.Text = videojuego.IdCategoria;
                    txtFechaLanzamiento.Text = videojuego.FechaLanzamiento.ToString(FormatoFecha);
                    txtDesarrolladora.Text = videojuego.Desarrolladora;
                    txtDistribuidora.Text = videojuego.Distribuidora;
                    txtImagen.Text = videojuego.Imagen;
                    txtTrailer.Text = videojuego.Trailer;
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

        private void BloquearFormulario(string mensaje)
        {
            phFormulario.Visible = false;
            litMensajeAcceso.Text = "<div class='alert alert-warning mt-2'>" + mensaje + "</div>";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            litMensaje.Text = "";

            if (!Page.IsValid) return;

            if (!DateTime.TryParseExact(txtFechaLanzamiento.Text, FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaLanzamiento))
            {
                MostrarError("La fecha de lanzamiento no es válida.");
                return;
            }

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var actual = db.SpVideojuegoConsultarPorId(idVideojuego).FirstOrDefault();
                    if (actual == null)
                    {
                        Response.Redirect("~/Pages/Videojuegos/ListaVideojuegos.aspx");
                        return;
                    }

                    int duplicados = db.SpVideojuegoValidarTituloDuplicado(actual.IdSucursal, txtTitulo.Text.Trim(), idVideojuego).First().Cantidad ?? 0;
                    if (duplicados > 0)
                    {
                        MostrarError("Ya existe otro videojuego con ese título en la misma sucursal.");
                        return;
                    }

                    db.SpVideojuegoModificar(
                        idVideojuego, txtTitulo.Text.Trim(), txtDescripcion.Text.Trim(), txtCategoria.Text.Trim(), fechaLanzamiento,
                        txtDesarrolladora.Text.Trim(), txtDistribuidora.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtImagen.Text) ? null : txtImagen.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtTrailer.Text) ? null : txtTrailer.Text.Trim());
                }

                Response.Redirect("~/Pages/Videojuegos/ListaVideojuegos.aspx");
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

        /// <summary>Inactiva el videojuego sin ejecutar validaciones de formulario (RF-006).</summary>
        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    db.SpVideojuegoInactivar(idVideojuego);
                }

                Response.Redirect("~/Pages/Videojuegos/ListaVideojuegos.aspx");
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
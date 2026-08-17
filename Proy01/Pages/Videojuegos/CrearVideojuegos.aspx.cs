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
    /// Creacion de un nuevo videojuego para una sucursal. Valida
    /// que el titulo no este duplicado dentro de la misma sucursal antes de
    /// guardar.
    public partial class CrearVideojuegos : System.Web.UI.Page
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
                CargarSucursales();
            }
        }

        private void CargarSucursales()
        {
            using (var db = ConexionBD.ObtenerConexion())
            {
                var sucursales = db.SpSucursalListarTodas().ToList();
                ddlSucursal.DataSource = sucursales;
                ddlSucursal.DataBind();
                ddlSucursal.Items.Insert(0, new ListItem("Seleccione una sucursal", ""));
            }
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

            int idSucursal = int.Parse(ddlSucursal.SelectedValue);
            string titulo = txtTitulo.Text.Trim();

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    int duplicados = db.SpVideojuegoValidarTituloDuplicado(idSucursal, titulo, null).First().Cantidad ?? 0;

                    if (duplicados > 0)
                    {
                        MostrarError("Ya existe un videojuego con ese título en la sucursal seleccionada.");
                        return;
                    }

                    int? idCreado = null;
                    db.SpVideojuegoCrear(
                        idSucursal, titulo, txtDescripcion.Text.Trim(), txtCategoria.Text.Trim(), fechaLanzamiento,
                        txtDesarrolladora.Text.Trim(), txtDistribuidora.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtImagen.Text) ? null : txtImagen.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtTrailer.Text) ? null : txtTrailer.Text.Trim(),
                        ref idCreado);
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
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
    public partial class Login : System.Web.UI.Page
    {
        /// Pantalla de autenticacion del sistema. Valida las
        /// credenciales digitadas contra la base de datos y, si son correctas,
        /// guarda el jugador en Session["jugadorSesion"] y lo redirige a la
        /// pantalla inicial que le corresponde segun su tipo.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya existe una sesion activa no tiene sentido mostrar el login de nuevo.
            if (!IsPostBack && Session["jugadorSesion"] != null)
            {
                RedirigirSegunTipoUsuario((JugadorSesion)Session["jugadorSesion"]);
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                using (var db = ConexionBD.ObtenerConexion())
                {
                    var resultado = db.SpJugadorAutenticar(txtEmail.Text.Trim(), txtClave.Text.Trim()).FirstOrDefault();

                    if (resultado == null)
                    {
                        MostrarMensaje("Las credenciales digitadas no son correctas.");
                        return;
                    }

                    var jugador = new JugadorSesion
                    {
                        IdJugador = resultado.IdJugador,
                        NombreCompleto = resultado.NombreCompleto,
                        EsAdministrador = resultado.EsAdministrador
                    };

                    Session["jugadorSesion"] = jugador;
                    RedirigirSegunTipoUsuario(jugador);
                }
            }
            catch
            {
                MostrarMensaje("Ocurrió un error al validar las credenciales. Intente nuevamente.");
            }
        }

        private void RedirigirSegunTipoUsuario(JugadorSesion jugador)
        {
            Response.Redirect(jugador.EsAdministrador
                ? "~/Pages/Alquiler/GestionarAlquileres.aspx"
                : "~/Pages/Alquiler/MisAlquileres.aspx");
        }

        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = "<div class='alert alert-danger mt-2'>" + mensaje + "</div>";
        }
    }
}
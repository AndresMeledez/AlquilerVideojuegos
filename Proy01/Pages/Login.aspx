<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proy01.Pages.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar sesión - NextGen Rentals</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" runat="server" />
    <link href="~/Content/Site.css" rel="stylesheet" runat="server" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body class="gv-login-body">
    <form id="formLogin" runat="server">
        <div class="gv-login-contenedor">
            <div class="gv-login-caja">
                <h1 class="gv-login-marca">NextGen Rentals</h1>
                <p class="gv-login-subtitulo">Sistema de Renta de Videojuegos</p>

                <div class="mb-3">
                    <label class="form-label">Correo electrónico</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="*" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Clave</label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="*" Display="Dynamic" />
                </div>

                <asp:Literal ID="litMensaje" runat="server" />

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn gv-btn-primario w-100 mt-2" OnClick="btnIngresar_Click" />
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarVideojuegos.aspx.cs" Inherits="Proy01.Pages.Videojuegos.EditarVideojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Literal ID="litMensajeAcceso" runat="server" />

    <asp:PlaceHolder ID="phFormulario" runat="server">
        <div class="gv-formulario">
            <div class="mb-3">
                <label class="form-label">Sucursal</label>
                <asp:TextBox ID="txtSucursal" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="mb-3">
                <label class="form-label">Título</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" MaxLength="100" />
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo"
                    CssClass="text-danger d-block small mt-1" ErrorMessage="El título es requerido." Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" />
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                    CssClass="text-danger d-block small mt-1" ErrorMessage="La descripción es requerida." Display="Dynamic" />
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Categoría</label>
                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="txtCategoria"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La categoría es requerida." Display="Dynamic" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Fecha de lanzamiento</label>
                    <asp:TextBox ID="txtFechaLanzamiento" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="rfvFechaLanzamiento" runat="server" ControlToValidate="txtFechaLanzamiento"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La fecha de lanzamiento es requerida." Display="Dynamic" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Desarrolladora</label>
                    <asp:TextBox ID="txtDesarrolladora" runat="server" CssClass="form-control" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="rfvDesarrolladora" runat="server" ControlToValidate="txtDesarrolladora"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La desarrolladora es requerida." Display="Dynamic" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Distribuidora</label>
                    <asp:TextBox ID="txtDistribuidora" runat="server" CssClass="form-control" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="rfvDistribuidora" runat="server" ControlToValidate="txtDistribuidora"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La distribuidora es requerida." Display="Dynamic" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Imagen (opcional)</label>
                    <asp:TextBox ID="txtImagen" runat="server" CssClass="form-control" MaxLength="255" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Tráiler (opcional)</label>
                    <asp:TextBox ID="txtTrailer" runat="server" CssClass="form-control" MaxLength="255" />
                </div>
            </div>

            <asp:Literal ID="litMensaje" runat="server" />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn gv-btn-primario me-2" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-danger me-2" CausesValidation="false"
                OnClientClick="return confirm('¿Desea inactivar este videojuego?');" OnClick="btnInactivar_Click" />
        </div>
    </asp:PlaceHolder>

    <asp:HyperLink ID="lnkRegresar" runat="server" CssClass="btn gv-btn-secundario mt-3" NavigateUrl="~/Pages/Videojuegos/ListaVideojuegos.aspx">Regresar</asp:HyperLink>


</asp:Content>

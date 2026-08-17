<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarAlquiler.aspx.cs" Inherits="Proy01.Pages.EditarAlquiler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Literal ID="litMensajeAcceso" runat="server" />

    <asp:PlaceHolder ID="phFormulario" runat="server">
        <div class="gv-formulario">
            <p><strong>Alquiler #</strong><asp:Literal ID="litIdAlquiler" runat="server" /> — <asp:Literal ID="litResumen" runat="server" /></p>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Fecha de inicio</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La fecha de inicio es requerida." Display="Dynamic" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Fecha de devolución</label>
                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="rfvFechaDevolucion" runat="server" ControlToValidate="txtFechaDevolucion"
                        CssClass="text-danger d-block small mt-1" ErrorMessage="La fecha de devolución es requerida." Display="Dynamic" />
                </div>
            </div>

            <asp:Literal ID="litMensaje" runat="server" />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn gv-btn-primario me-2" OnClick="btnGuardar_Click" />
        </div>
    </asp:PlaceHolder>

    <asp:HyperLink ID="lnkRegresar" runat="server" CssClass="btn gv-btn-secundario mt-3">Regresar</asp:HyperLink>



</asp:Content>

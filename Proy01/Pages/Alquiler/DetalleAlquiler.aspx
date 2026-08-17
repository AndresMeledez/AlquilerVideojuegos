<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleAlquiler.aspx.cs" Inherits="Proy01.Pages.DetalleAlquiler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="litMensaje" runat="server" />

    <div class="gv-comprobante">
        <h4 class="gv-comprobante-titulo">Comprobante de alquiler #<asp:Literal ID="litIdAlquiler" runat="server" /></h4>
        <div class="row">
            <div class="col-md-6">
                <p><strong>Sucursal:</strong> <asp:Literal ID="litSucursal" runat="server" /></p>
                <p><strong>Videojuego:</strong> <asp:Literal ID="litVideojuego" runat="server" /></p>
                <p><strong>Jugador:</strong> <asp:Literal ID="litJugador" runat="server" /></p>
            </div>
            <div class="col-md-6">
                <p><strong>Fecha de inicio:</strong> <asp:Literal ID="litFechaInicio" runat="server" /></p>
                <p><strong>Fecha de devolución:</strong> <asp:Literal ID="litFechaDevolucion" runat="server" /></p>
                <p><strong>Días de alquiler:</strong> <asp:Literal ID="litDias" runat="server" /></p>
                <p><strong>Costo total:</strong> <asp:Literal ID="litCostoTotal" runat="server" /></p>
                <p><strong>Estado:</strong> <asp:Literal ID="litEstado" runat="server" /></p>
            </div>
        </div>

        <h5 class="mt-4">Bitácora</h5>
        <asp:GridView ID="gvBitacora" runat="server" CssClass="table table-dark table-sm gv-tabla" AutoGenerateColumns="false"
            EmptyDataText="Sin movimientos registrados." GridLines="None">
            <Columns>
                <asp:BoundField DataField="NombreCompleto" HeaderText="Usuario" />
                <asp:BoundField DataField="AccionRealizada" HeaderText="Acción" />
                <asp:BoundField DataField="FechaDeLaAccion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            </Columns>
        </asp:GridView>

        <div class="mt-4 gv-acciones">
            <asp:HyperLink ID="lnkEditar" runat="server" CssClass="btn gv-btn-primario me-2">Editar alquiler</asp:HyperLink>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar alquiler" CssClass="btn btn-danger me-2"
                OnClientClick="return confirm('¿Desea cancelar este alquiler?');" OnClick="btnCancelar_Click" CausesValidation="false" />
            <asp:HyperLink ID="lnkRegresar" runat="server" CssClass="btn gv-btn-secundario">Regresar</asp:HyperLink>
        </div>
    </div>
</asp:Content>

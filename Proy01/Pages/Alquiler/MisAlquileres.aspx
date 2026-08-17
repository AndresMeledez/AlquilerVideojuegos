<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisAlquileres.aspx.cs" Inherits="Proy01.Pages.MisAlquileres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
    <p class="text-muted mb-0">Historial de alquileres del usuario autenticado.</p>
    <asp:HyperLink ID="lnkCrearAlquiler" runat="server" CssClass="btn gv-btn-primario" NavigateUrl="~/Pages/Alquiler/CrearAlquiler.aspx">+ Nuevo alquiler</asp:HyperLink>
</div>

<asp:GridView ID="gvAlquileres" runat="server" CssClass="table table-dark table-hover gv-tabla" AutoGenerateColumns="false"
    EmptyDataText="No tiene alquileres registrados." GridLines="None">
    <Columns>
        <asp:BoundField DataField="IdAlquiler" HeaderText="# Alquiler" />
        <asp:BoundField DataField="NombreSucursal" HeaderText="Sucursal" />
        <asp:BoundField DataField="Titulo" HeaderText="Videojuego" />
        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha inicio" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="FechaDevolucion" HeaderText="Fecha devolución" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="CostoTotal" HeaderText="Costo total" DataFormatString="{0:C2}" />
        <asp:BoundField DataField="EstadoTexto" HeaderText="Estado" />
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:HyperLink runat="server" CssClass="btn btn-sm gv-btn-secundario"
                    NavigateUrl='<%# "~/Pages/Alquiler/DetalleAlquiler.aspx?id=" + Eval("IdAlquiler") %>'>Ver detalle</asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>

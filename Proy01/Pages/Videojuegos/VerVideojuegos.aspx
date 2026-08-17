<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerVideojuegos.aspx.cs" Inherits="Proy01.Pages.Videojuegos.VerVideojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p class="text-muted">Catálogo de videojuegos activos disponibles en todas las sucursales. Esta pantalla es solo de consulta.</p>

    <asp:GridView ID="gvVideojuegos" runat="server" CssClass="table table-dark table-hover gv-tabla" AutoGenerateColumns="false"
        EmptyDataText="No hay videojuegos disponibles en este momento." GridLines="None">
        <Columns>
            <asp:BoundField DataField="NombreSucursal" HeaderText="Sucursal" />
            <asp:BoundField DataField="Titulo" HeaderText="Título" />
            <asp:BoundField DataField="IdCategoria" HeaderText="Categoría" />
            <asp:BoundField DataField="Desarrolladora" HeaderText="Desarrolladora" />
            <asp:BoundField DataField="Distribuidora" HeaderText="Distribuidora" />
            <asp:BoundField DataField="FechaLanzamiento" HeaderText="Lanzamiento" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
        </Columns>
    </asp:GridView>

</asp:Content>

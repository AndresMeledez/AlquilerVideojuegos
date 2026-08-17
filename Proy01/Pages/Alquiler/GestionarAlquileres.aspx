<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarAlquileres.aspx.cs" Inherits="Proy01.Pages.GestionarAlquileres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="d-flex justify-content-end mb-3">
        <asp:HyperLink ID="lnkCrearAlquiler" runat="server" CssClass="btn gv-btn-primario" NavigateUrl="~/Pages/Alquiler/CrearAlquiler.aspx">+ Nuevo alquiler</asp:HyperLink>
    </div>

    <div class="gv-panel-filtro mb-4">
        <div class="row g-3 align-items-end">
            <div class="col-md-4">
                <label class="form-label">Jugador</label>
                <asp:DropDownList ID="ddlJugador" runat="server" CssClass="form-select" DataTextField="NombreCompleto" DataValueField="IdJugador" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Fecha de inicio</label>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <label class="form-label">Fecha de devolución</label>
                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn gv-btn-primario w-100" OnClick="btnFiltrar_Click" CausesValidation="false" />
            </div>
        </div>
        <asp:Literal ID="litMensajeFiltro" runat="server" />
    </div>

    <asp:GridView ID="gvAlquileres" runat="server" CssClass="table table-dark table-hover gv-tabla" AutoGenerateColumns="false"
        EmptyDataText="No se encontraron alquileres con los criterios indicados." GridLines="None">
        <Columns>
            <asp:BoundField DataField="IdAlquiler" HeaderText="# Alquiler" />
            <asp:BoundField DataField="NombreJugador" HeaderText="Jugador" />
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaVideojuegos.aspx.cs" Inherits="Proy01.Pages.Videojuegos.ListaVideojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="d-flex justify-content-between align-items-center mb-3">
         <p class="text-muted mb-0">Videojuegos registrados por sucursal.</p>
         <asp:HyperLink ID="lnkCrearVideojuego" runat="server" CssClass="btn gv-btn-primario" NavigateUrl="~/Pages/Videojuegos/CrearVideojuegos.aspx">+ Nuevo videojuego</asp:HyperLink>
     </div>

     <asp:GridView ID="gvVideojuegos" runat="server" CssClass="table table-dark table-hover gv-tabla" AutoGenerateColumns="false"
         EmptyDataText="No hay videojuegos registrados." GridLines="None">
         <Columns>
             <asp:BoundField DataField="IdVideojuego" HeaderText="Id" />
             <asp:BoundField DataField="NombreSucursal" HeaderText="Sucursal" />
             <asp:BoundField DataField="Titulo" HeaderText="Título" />
             <asp:BoundField DataField="IdCategoria" HeaderText="Categoría" />
             <asp:TemplateField HeaderText="Estado">
                 <ItemTemplate>
                     <%# Eval("Estado").ToString() == "A" ? "Activo" : "Inactivo" %>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="">
                 <ItemTemplate>
                     <asp:HyperLink runat="server" CssClass="btn btn-sm gv-btn-secundario"
                         Visible='<%# Eval("Estado").ToString() == "A" %>'
                         NavigateUrl='<%# "~/Pages/Videojuegos/EditarVideojuegos.aspx?id=" + Eval("IdVideojuego") %>'>Editar</asp:HyperLink>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
     </asp:GridView>

</asp:Content>

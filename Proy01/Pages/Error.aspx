<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Proy01.Pages.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-danger">
        <p class="mb-0">Ha ocurrido un error al procesar la solicitud. Intente nuevamente.</p>
    </div>
    <asp:HyperLink ID="lnkRegresar" runat="server" CssClass="btn gv-btn-secundario" NavigateUrl="~/Default.aspx">Regresar</asp:HyperLink>
</asp:Content>

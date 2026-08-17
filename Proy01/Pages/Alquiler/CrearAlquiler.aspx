<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearAlquiler.aspx.cs" Inherits="Proy01.Pages.CrearAlquiler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="gv-formulario">
    <div class="mb-3">
        <label class="form-label">Sucursal</label>
        <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="IdSucursal"
            AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
            <asp:ListItem Text="Seleccione una sucursal" Value="" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ControlToValidate="ddlSucursal"
            CssClass="text-danger d-block small mt-1" ErrorMessage="Debe seleccionar una sucursal." Display="Dynamic" InitialValue="" />
    </div>

    <div class="mb-3">
        <label class="form-label">Jugador</label>
        <asp:DropDownList ID="ddlJugador" runat="server" CssClass="form-select" DataTextField="NombreCompleto" DataValueField="IdJugador" />
    </div>

    <div class="row">
        <div class="col-md-6 mb-3">
            <label class="form-label">Fecha de inicio</label>
            <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date"
                AutoPostBack="true" OnTextChanged="Fechas_TextChanged" />
            <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio"
                CssClass="text-danger d-block small mt-1" ErrorMessage="La fecha de inicio es requerida." Display="Dynamic" />
        </div>
        <div class="col-md-6 mb-3">
            <label class="form-label">Fecha de devolución</label>
            <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" TextMode="Date"
                AutoPostBack="true" OnTextChanged="Fechas_TextChanged" />
            <asp:RequiredFieldValidator ID="rfvFechaDevolucion" runat="server" ControlToValidate="txtFechaDevolucion"
                CssClass="text-danger d-block small mt-1" ErrorMessage="La fecha de devolución es requerida." Display="Dynamic" />
        </div>
    </div>

    <div class="mb-3">
        <label class="form-label d-block">Asignación del videojuego</label>
        <asp:RadioButtonList ID="rblModoAsignacion" runat="server" RepeatDirection="Horizontal" CssClass="mb-2"
            AutoPostBack="true" OnSelectedIndexChanged="ModoAsignacion_SelectedIndexChanged">
            <asp:ListItem Text="Automática (menos alquilado)" Value="Automatica" Selected="True" />
            <asp:ListItem Text="Aleatoria" Value="Aleatoria" />
            <asp:ListItem Text="Elegir manualmente" Value="Manual" />
        </asp:RadioButtonList>

        <asp:Literal ID="litVideojuegosDisponibles" runat="server" />

        <asp:Panel ID="pnlSeleccionManual" runat="server" Visible="false" CssClass="mt-2">
            <asp:DropDownList ID="ddlVideojuego" runat="server" CssClass="form-select" DataTextField="Titulo" DataValueField="IdVideojuego" />
            <asp:RequiredFieldValidator ID="rfvVideojuego" runat="server" ControlToValidate="ddlVideojuego"
                CssClass="text-danger d-block small mt-1" ErrorMessage="Debe seleccionar un videojuego." Display="Dynamic" Enabled="false" />
        </asp:Panel>
    </div>

    <asp:Literal ID="litMensaje" runat="server" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn gv-btn-primario me-2" OnClick="btnGuardar_Click" />
    <asp:HyperLink ID="lnkRegresar" runat="server" CssClass="btn gv-btn-secundario">Regresar</asp:HyperLink>
</div>
</asp:Content>

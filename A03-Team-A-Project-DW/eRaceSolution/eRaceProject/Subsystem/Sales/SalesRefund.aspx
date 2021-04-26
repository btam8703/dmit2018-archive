<%@ Page Title="Refunds" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesRefund.aspx.cs" Inherits="eRaceProject.Subsystem.SalesRefund" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-10">
            <h1>Refunds</h1>
        </div>
    </div>

    <div>
        <asp:Label ID="Label1" runat="server" Text="User : "></asp:Label>
        <asp:Label ID="LoggedUser" runat="server"></asp:Label>
        <asp:Label ID="EmployeeID" runat="server" Visible="false"></asp:Label>
    </div>&nbsp;&nbsp;

    <%-- MessageUserControl --%>
    <div class="row" style="color:darkred; font-size: 15px; font-weight:bold;">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>&nbsp;

    <%-- Form Controls --%>
    <div class="col-md-10 d-flex">

        <asp:TextBox ID="InvoiceLookupTextBox" runat="server" CssClass="form-control" Placeholder="Enter your Invoice#" TextMode="Number" Width="200px"></asp:TextBox>&nbsp;&nbsp;
        <asp:LinkButton ID="FindInvoiceButton" runat="server" CssClass="btn btn-info" OnClick="FindInvoiceButton_Click" >Find Invoice</asp:LinkButton>&nbsp;&nbsp;
        <asp:LinkButton ID="ClearButton" runat="server" CssClass="btn btn-secondary" OnClick="ClearButton_Click" >Clear</asp:LinkButton>&nbsp;&nbsp;
    </div>
    &nbsp;&nbsp;

    <%-- GridView / Refund Gridview--%>
    <%-- Will use a ViewModel to populate --%>
    <asp:GridView ID="InvoiceDetailsGridView" runat="server" CssClass="table table-dark table-striped" ItemType="eRaceSystem.ViewModels.ReturnInvoiceDetails" DataKeyNames="ProductID" AutoGenerateColumns="false">

        <Columns>
            <asp:TemplateField HeaderText="ProductID" Visible="true" >
                <ItemTemplate>
                    <asp:Label ID="ProductID" runat="server" Text="<%# Item.ProductID %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Product">
                <ItemTemplate>
                    <asp:Label ID="ProductName" runat="server" Text="<%# Item.ProductName %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="Quantity" runat="server" Text="<%# Item.Quantity %>" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Price">
                <ItemTemplate>
                    <asp:Label ID="Price" runat="server" Text="<%# Item.Price %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <asp:Label ID="Amount" runat="server" Text="<%# Item.Amount%>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Restock Charge">
                <ItemTemplate>
                    <asp:CheckBox ID="RestockCheckBox" runat="server" Enabled="false"/>
                    <asp:Label ID="RestockCharge" runat="server" Text="<%# Item.ReStockCharge %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Refund Reason">
                <ItemTemplate>
                    <%-- Cannot enable Confectionaries --%>
                    <asp:CheckBox ID="RefundReasonCheckBox" runat="server" OnCheckedChanged="RefundReasonCheckBox_CheckedChanged" AutoPostBack="true" Enabled='<%# int.Parse(Eval("CategoryID").ToString()) == 3 ? false : true %>'/>
                    <asp:TextBox ID="RefundReasonTextBox" runat="server" Enabled='<%# int.Parse(Eval("CategoryID").ToString()) == 3 ? false : true %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No Invoice to display. Please input an existing Invoice#</EmptyDataTemplate>
    </asp:GridView>
    <br />

    <%-- Buttons and Display --%>
    <div class="container">

        <div class="row">

            <div class="col-md-2">
                <asp:LinkButton ID="RefundButton" runat="server" CssClass="btn btn-success" Height="60px" OnClick="RefundButton_Click">Refund</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            </div>

            <div class="col form-horizontal">

                <div class="form-group">
                    <asp:Label ID="SubtotalLabel" runat="server" Text="Subtotal ($)"></asp:Label>
                    <asp:TextBox ID="SubtotalTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="TaxLabel" runat="server" Text="Tax ($)"></asp:Label>
                    <asp:TextBox ID="TaxTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

                <div class="form-group">
                    <span style="font-weight:bold;"><asp:Label ID="TotalLabel" runat="server" Text="Refund Total ($)" ></asp:Label></span>
                    <asp:TextBox ID="TotalTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="InvoiceLabel" runat="server" Text="Your Invoice# "></asp:Label>
                    <asp:TextBox ID="InvoiceTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

            </div>
        </div>

    </div>



</asp:Content>

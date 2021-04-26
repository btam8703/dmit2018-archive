<%@ Page Title="Sales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="eRaceProject.Subsystem.Sales" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-10">
            <h1>Sales</h1>
        </div>
    </div>

    <div>
        <asp:Label ID="Label1" runat="server" Text="User : "></asp:Label>
        <asp:Label ID="LoggedUser" runat="server"></asp:Label>
        <asp:Label ID="EmployeeID" runat="server" Visible="false"></asp:Label>
    </div>&nbsp;&nbsp;
    
    <div class="row" style="color:darkred; font-size: 15px; font-weight:bold;">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl"/>
    </div>&nbsp;
    

    <%-- Main Contents --%>
    <h1>In-Store Purchases</h1>

    <%-- Form Controls --%>
    <div class="col-md-10 d-flex">

        <asp:DropDownList ID="CategoryProductDDL" runat="server" CssClass="form-control" Width="400px" 
           AppendDataBoundItems="true" DataSourceID="CategoryODS" DataTextField="Displaytext" DataValueField="ValueID" AutoPostBack="true">
            <asp:ListItem Value="0">[Select a category]</asp:ListItem>
        </asp:DropDownList>&nbsp;

        <asp:DropDownList ID="ProductDDL" runat="server" CssClass="form-control" Width="400px" 
            DataSourceID="CategoryProductODS" DataTextField="ItemName" DataValueField="ProductID">
            <asp:ListItem Value="0">[Select a Product]</asp:ListItem>
        </asp:DropDownList>&nbsp;

        <asp:TextBox ID="QuantityText" runat="server" CssClass="form-control" TextMode="Number" Width="80px" Min="1"></asp:TextBox>&nbsp;

        <asp:LinkButton ID="AddToCartButton" runat="server" CssClass="form-control btn btn-info" Width="60px" OnClick="AddToCartButton_Click">Add</asp:LinkButton>&nbsp;
    </div>&nbsp;&nbsp;

    <%-- GridView / Shopping Cart--%>
    <%-- Will use a ViewModel to populate --%>
    <asp:GridView ID="CartGridView" runat="server" CssClass="table table-dark table-striped" ItemType="eRaceSystem.ViewModels.ShoppingCartList" DataKeyNames="ProductID" AutoGenerateColumns="false" OnRowCommand="CartGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="ProductID" Visible="false">
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
                    <asp:LinkButton ID="Refresh" runat="server" OnClick="Refresh_Click" CssClass="btn btn-info"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <asp:Label ID="Amount" runat="server" Text="<%# Item.Amount %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:ButtonField Text="Remove" ControlStyle-CssClass="btn btn-danger"></asp:ButtonField>
        </Columns>

        <EmptyDataTemplate>No products to display. Please select a category first then a product</EmptyDataTemplate>
        <Columns>

        </Columns>
    </asp:GridView><br />

    <%-- Buttons and Display --%>
    <div class="container">

        <div class="row">

            <div class="col">
                <asp:LinkButton ID="PaymentButton" runat="server" CssClass="btn btn-success" Height="60px" OnClick="PaymentButton_Click">Payment</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="ClearCartButton" runat="server" CssClass="btn btn-info" OnClick="ClearCartButton_Click">
                    <i class="fa fa-cart-plus" style="color:antiquewhite;"></i>Clear Cart
                </asp:LinkButton>
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
                    <asp:Label ID="TotalLabel" runat="server" Text="Total ($)"></asp:Label>
                    <asp:TextBox ID="TotalTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label ID="InvoiceLabel" runat="server" Text="Your Invoice# "></asp:Label>
                    <asp:TextBox ID="InvoiceTextBox" runat="server" CssClass="form-control" Width="100px" Enabled="False"></asp:TextBox>
                </div>

            </div>
        </div>

    </div>

    <%-- Link to StoreRefund --%>
    <asp:LinkButton ID="LinkToRefund" runat="server" OnClick="LinkToRefund_Click">
        <span style="font-size:medium">
            Want to return a product? >>>
        </span>
    </asp:LinkButton>

    <%-- ObjectDataSources --%>
    <asp:ObjectDataSource ID="CategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Categories" TypeName="eRaceSystem.BLL.Sales.SalesController"
        OnSelected="SelectCheckForException">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CategoryProductODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_ProductsByCategory" TypeName="eRaceSystem.BLL.Sales.SalesController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryProductDDL" PropertyName="SelectedValue" Name="categoryid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

<%@ Page Title="Product Query ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductQueryODS.aspx.cs" Inherits="GroceryListWebApp.ExercisePages.ProductQueryODS" %>

<%--   Registers MessageUserControl! uc1 refers to different version of a label for example idk or name control (same name, different objects eg a checkbox and a dropdown box with the same name--%>
<%--UC1 is the first one--%>
<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Product Query ODS</h1>

    <div class ="row">
        <uc1:messageusercontrol runat="server" id="MessageUserControl" />
    </div>
<%--    MessageUserControl!--%>
    <div class ="row">
        <asp:Label ID="Label1" runat="server" Text="Select a Category"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="CategoryDropDownList" runat="server" DataSourceID="CategoryListODS" DataTextField="DisplayText" DataValueField="ValueId" AppendDataBoundItems="True">
            <asp:ListItem Value="0">Select...</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        <asp:LinkButton ID="SearchProducts" runat="server" OnClick="SearchProducts_Click">Search for Products</asp:LinkButton>
    </div>
    <br /> <br />
    <div class ="row">
        <asp:Label ID="MessageLabel" runat="server" Text="Label"></asp:Label>
    </div>
    <div class ="row">
        <asp:GridView ID="ProductGridview" runat="server" AutoGenerateColumns="False" DataSourceID="ProductListODS" AllowPaging="True" EmptyDataText="No Data Available">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID"></asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="{0:n2}"></asp:BoundField>
                <asp:BoundField DataField="Discount" HeaderText="Disc" SortExpression="Discount" DataFormatString="{0:n2}"></asp:BoundField>
                <asp:BoundField DataField="UnitSize" HeaderText="Unit Size" SortExpression="UnitSize"></asp:BoundField>
                <asp:CheckBoxField DataField="Taxable" HeaderText="Taxable" SortExpression="Taxable">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:CheckBoxField>
            </Columns>
        </asp:GridView>
    </div>
    <div class ="row">
        <asp:LinkButton ID="BackButton" runat="server" OnClick="BackButton_Click">Back</asp:LinkButton>
    </div>
    <asp:ObjectDataSource ID="CategoryListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" OnSelected="SelectCheckForException" TypeName="GroceryListSystem.BLL.CategoryController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProductListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Product_Get" OnSelected="SelectCheckForException" TypeName="GroceryListSystem.BLL.ProductController">
        <SelectParameters>
                <asp:ControlParameter ControlID="CategoryDropDownList" PropertyName="SelectedValue" DefaultValue="0" Name="CategoryID" Type="Int32"></asp:ControlParameter>
         </SelectParameters> 
    </asp:ObjectDataSource>
</asp:Content>

<%@ Page Title="Product CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCRUD.aspx.cs" Inherits="GroceryListWebApp.ExercisePages.ProductCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<%--   Registers MessageUserControl! uc1 refers to different version of a label for example idk or name control (same name, different objects eg a checkbox and a dropdown box with the same name--%>
<%--UC1 is the first one--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Product CRUD</h1>
    <div class ="row">
        <div class ="offset-1">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" HeaderText="Data Issues:" ValidationGroup="igroup"/>
            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" HeaderText="Data Issues:" ValidationGroup="egroup"/>
        </div>
    </div>
    <div class ="row">
        <div class ="offset-1">
            <asp:ListView ID="ProductListView" runat="server" DataSourceID="ProductODS" InsertItemPosition="LastItem"
                 DataKeyNames="ProductID" OnSelectedIndexChanged="ProductListView_SelectedIndexChanged">
                <%-- ADD DATAKEYNAMES --%>
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" 
                                OnClientClick="return confirm('Delete permanently?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="40"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="400"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Price", "{0:0.00}") %>' runat="server" ID="PriceLabel" Width="50" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Discount", "{0:0.00}") %>' runat="server" ID="DiscountLabel" Width="50"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" Width="100"/></td>
                        <td>
                            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="DisplayText" DataValueField="ValueId" selectedvalue='<%# Eval("CategoryID") %>' Enabled="false"></asp:DropDownList></td>
                        <td align="center">
                            <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="RegExPriceFormatE" runat="server" 
                        ErrorMessage="Price must be in a proper format" Display="None" 
                        ControlToValidate="PriceTextBoxE" ValidationGroup="egroup" ValidationExpression="^(\d{1,5}|\d{0,5}\.\d{1,2})$">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegExDiscountFormatE" runat="server" 
                        ErrorMessage="Discount must be in a proper format" Display="None" 
                        ControlToValidate="DiscountTextBoxE" ValidationGroup="egroup" ValidationExpression="^(\d{1,5}|\d{0,5}\.\d{1,2})$">
                    </asp:RegularExpressionValidator>
                    <tr style="background-color: #999999;">
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="egroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ProductID") %>' runat="server" ID="ProductIDTextBox" Enabled="false" Width="40"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" Width="400"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Price", "{0:0.00}") %>' runat="server" ID="PriceTextBoxE" Width="50"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Discount", "{0:0.00}") %>' runat="server" ID="DiscountTextBoxE" Width="50"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitSize") %>' runat="server" ID="UnitSizeTextBox" Width="100"/></td>
                        <td>
                            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="DisplayText" DataValueField="ValueId" selectedvalue='<%# Bind("CategoryID") %>'></asp:DropDownList></td>
                        <td align="center">
                            <asp:CheckBox Checked='<%# Bind("Taxable") %>' runat="server" ID="TaxableCheckBox" /></td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <asp:RegularExpressionValidator ID="RegExPriceFormatI" runat="server" 
                        ErrorMessage="Price must be in a proper format" Display="None" 
                        ControlToValidate="PriceTextBoxI" ValidationGroup="igroup" ValidationExpression="^(\d{1,5}|\d{0,5}\.\d{1,2})$">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegExDiscountFormatI" runat="server" 
                        ErrorMessage="Discount must be in a proper format" Display="None" 
                        ControlToValidate="DiscountTextBoxI" ValidationGroup="igroup" ValidationExpression="^(\d{1,5}|\d{0,5}\.\d{1,2})$">
                    </asp:RegularExpressionValidator>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="igroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ProductID") %>' runat="server" ID="ProductIDTextBox" Enabled="false" Width="40"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" Width="400"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Price", "{0:0.00}") %>' runat="server" ID="PriceTextBoxI" Width="50"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Discount", "{0:0.00}") %>' runat="server" ID="DiscountTextBoxI" Width="50"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitSize") %>' runat="server" ID="UnitSizeTextBox" Width="100"/></td>
                        <td>
                            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="DisplayText" DataValueField="ValueId" selectedvalue='<%# Bind("CategoryID") %>'></asp:DropDownList></td>
                        <td align="center">
                            <asp:CheckBox Checked='<%# Bind("Taxable") %>' runat="server" ID="TaxableCheckBox" /></td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" 
                                OnClientClick="return confirm('Delete permanently?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="40"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="400"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Price", "{0:0.00}") %>' runat="server" ID="PriceLabel" Width="50"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Discount", "{0:0.00}") %>' runat="server" ID="DiscountLabel" Width="50"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" Width="100"/></td>
                        <td>
                            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="DisplayText" DataValueField="ValueId" selectedvalue='<%# Eval("CategoryID") %>' Enabled="false"></asp:DropDownList></td>
                        <td align="center">
                            <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                        <th runat="server"></th>
                                        <th runat="server">ID</th>
                                        <th runat="server">Description</th>
                                        <th runat="server">Price</th>
                                        <th runat="server">Disc</th>
                                        <th runat="server">Unit Size</th>
                                        <th runat="server">Category</th>
                                        <th runat="server">Taxable</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" 
                                OnClientClick="return confirm('Delete permanently?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="40"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="400"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Price", "{0:0.00}") %>' runat="server" ID="PriceLabel" Width="50"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Discount", "{0:0.00}") %>' runat="server" ID="DiscountLabel" Width="50"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" Width="100"/></td>
                        <td>
                            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="CategoryODS" DataTextField="DisplayText" DataValueField="ValueId" selectedvalue='<%# Eval("CategoryID") %>'></asp:DropDownList></td>
                        <td align="center">
                            <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <asp:ObjectDataSource ID="ProductODS" runat="server" 
        DataObjectTypeName="GroceryListSystem.ViewModels.ProductList" 
        DeleteMethod="Product_Delete" 
        InsertMethod="Product_Add" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Product_List" 
        TypeName="GroceryListSystem.BLL.ProductController" 
        UpdateMethod="Product_Update"
         OnDeleted="DeleteCheckForException"
         OnInserted="InsertCheckForException"
         OnSelected="SelectCheckForException"
         OnUpdated="UpdateCheckForException">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CategoryODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Category_List" 
        TypeName="GroceryListSystem.BLL.CategoryController" OnSelected ="SelectCheckForException"></asp:ObjectDataSource>
</asp:Content>

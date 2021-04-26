<%@ Page Title="Purchasing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchasing.aspx.cs" Inherits="eRaceProject.Subsystem.Purchasing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-10">
            <h1>Purchasing</h1>
        </div>
    </div>

    <div class="col-2">
        <asp:Label ID="Label1" runat="server" Text="User : "></asp:Label>
        <asp:Label ID="LoggedUser" runat="server"></asp:Label>
        <asp:Label ID="EmployeeIDField" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
    </div>
    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <br />
        <br />
    </div>

    <div class="row">
        <div class="col-md-8">
            <br />
            <br />
            <div class="row">
                <asp:Label ID="VendorDDLLabel" runat="server" Text="Vendor:"></asp:Label>&nbsp;
                <asp:DropDownList ID="VendorSelectionDDL" runat="server" DataSourceID="VendorDDLODS" DataTextField="Displaytext" DataValueField="ValueID" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">[Select a Vendor]</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="VendorDDLODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Vendors" TypeName="eRaceSystem.BLL.Purchasing.VendorController"></asp:ObjectDataSource>
                &nbsp;

                <asp:LinkButton ID="SelectVendor" runat="server" CssClass="btn btn-success btn-xs" OnClick="SelectVendor_Click">Select</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="PlaceOrder" runat="server" CssClass="btn btn-success btn-xs" OnClick="PlaceOrder_Click" >Place Order</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="SaveOrder" runat="server" CssClass="btn btn-success btn-xs" Enabled="false" OnClick="Save_Click">Save</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="DeleteOrder" runat="server" CssClass="btn btn-success btn-xs" Enabled="false" OnClick="Delete_Click">Delete</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="CancelOrder" runat="server" CssClass="btn btn-success btn-xs" Enabled="false" OnClick="Cancel_Click">Cancel</asp:LinkButton>
            </div>


            <asp:Panel ID="OrderInfoPanel" runat="server" Visible="false">
                <br />
                <asp:Label ID="VendorInfoLabel" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="OrderIDLabel" Visible="false" runat="server"></asp:Label>
                <div class="row">
                    <asp:TextBox ID="CommentTextBox" runat="server" Width="500px" placeholder="Comments" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    <div class="col-md-1">
                        <asp:Label ID="TaxLabel" runat="server" Text="Tax:" Height="20px"></asp:Label>
                        <asp:Label ID="SubTotalLabel" runat="server" Text="SubTotal:" Height="20px"></asp:Label>
                        <asp:Label ID="TotalLabel" runat="server" Text="Total:" Height="20px"></asp:Label>
                    </div>
                    &nbsp;
                    <div class="col-md-3">
                        <asp:TextBox ID="TaxTextBox" Enabled="false" runat="server" Width="110px" Height="20px"></asp:TextBox>
                        <asp:TextBox ID="SubTotalTextBox" Enabled="false" runat="server" Width="110px" Height="20px"></asp:TextBox>
                        <asp:TextBox ID="TotalTextBox" Enabled="false" runat="server" Width="110px" Height="20px"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <br />
        <br />
        <div class="col-md-9">
            <br />
            <br />
            <asp:GridView ID="OrderDetailItemsGV" runat="server" Visible="false" OnRowDataBound="OrderDetailItemsGV_RowDataBound" DataKeyNames="OrderDetailID"
                ItemType="eRaceSystem.ViewModels.PurchasingModels.ItemSummary" OnRowCommand="OrderDetailItemsGV_RowCommand" CssClass="table table-hover table-sm" AutoGenerateColumns="false" OnSelectedIndexChanged="OrderDetailItemsGV_SelectedIndexChanged">
                <Columns>
                    <asp:ButtonField CommandName="RemoveItem" ButtonType="Button" ControlStyle-CssClass="btn btn-danger btn-xs"></asp:ButtonField>
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                            <asp:HiddenField ID="ProductID" runat="server" Value="<%# Item.ProductID %>" />
                            <asp:HiddenField ID="BaseItemPrice" runat="server" Value="<%# Item.SellingPrice %>" />
                            <asp:HiddenField ID="ReOrderAmount" runat="server" Value="<%# Item.ReOrderLevel %>" />
                            <asp:HiddenField ID="InStock" runat="server" Value="<%# Item.QuantityOnHand %>" />
                            <asp:HiddenField ID="OnOrder" runat="server" Value="<%# Item.QuantityOnOrder %>" />
                            <asp:Label ID="Description" runat="server" Text="<%# Item.Description %>" />
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="OrderQtyTextboxGV" TextMode="Number" runat="server" Width="70px" Text="<%# Item.Quantity %>"></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Size">
                        <ItemTemplate>
                            <asp:Label ID="UnitSizeLabel" runat="server" Width="140px" Text="<%# Item.OrderUnitSize %>"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Cost">
                        <ItemTemplate>
                            <asp:TextBox ID="UnitCostTextboxGV" runat="server" TextMode="Number" Width="90px" Text="<%# Item.UnitCost %>"></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Per-Item Cost">
                        <ItemTemplate>
                            <asp:LinkButton ID="Refresh" CommandName="RefreshAmounts" runat="server">Refresh</asp:LinkButton>
                            <asp:Label ID="OverItemCostsLabel" runat="server"></asp:Label>
                            <asp:Label ID="PerItemCostLabel" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Extended Cost">
                        <ItemTemplate>
                            <asp:Label ID="ExtendedCostLabel" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate><i>No selected items</i></EmptyDataTemplate>
            </asp:GridView>


        </div>
    </div>

    <br />
    <br />
    <div class="row">
        <asp:GridView ID="VendorCatalogGV" OnDataBinding="VendorCatalogGV_DataBinding" OnRowDataBound="VendorCatalogGV_RowDataBound" Visible="false" ItemType="eRaceSystem.ViewModels.PurchasingModels.ItemSummary"
            AutoGenerateColumns="false" runat="server" OnRowCommand="VendorCatalogGV_RowCommand" CssClass="table table-hover table-sm">
            <Columns>

                <asp:ButtonField ControlStyle-CssClass="btn btn-success btn-xs" ButtonType="Button"></asp:ButtonField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:HiddenField ID="BaseItemPrice" runat="server" Value="<%# Item.SellingPrice %>" />
                        <asp:HiddenField ID="ProductID" runat="server" Value="<%# Item.ProductID %>" />
                        <asp:HiddenField ID="UnitCost" runat="server" Value="<%# Item.UnitCost %>" />
                        <asp:Label ID="Description" runat="server" Text="<%# Item.Description %>" />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder">
                    <ItemTemplate>
                        <asp:Label ID="ReOrderLevel" runat="server" Text="<%# Item.ReOrderLevel %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Stock">
                    <ItemTemplate>
                        <asp:Label ID="InStock" runat="server" Text="<%# Item.QuantityOnHand %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="On Order">
                    <ItemTemplate>
                        <asp:Label ID="OnOrder" runat="server" Text="<%# Item.QuantityOnOrder %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                        <asp:Label ID="UnitSize" runat="server" Text="<%# Item.OrderUnitSize %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><i>No available items</i></EmptyDataTemplate>
        </asp:GridView>

        <%--https://stackoverflow.com/questions/24773645/in-gridview-how-to-use-rowcommand-event-for-a-button--%>
    </div>
</asp:Content>

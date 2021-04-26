<%@ Page Title="CRUD: Theatre" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssessmentCRUD.aspx.cs" Inherits="WebAppOMST.Assessments.AssessmentCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Assessment: CRUD</h1>
    <div class ="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
    <div class ="row">
        <asp:ListView ID="ListView1" runat="server" DataSourceID="TheatreODS" InsertItemPosition="LastItem">
            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #284775;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreID") %>' runat="server" ID="TheatreIDLabel" Width ="50" /></td>
                    <td>
                        <asp:DropDownList ID="LocationDropDown" runat="server" DataSourceID="LocationODS" DataTextField="DisplayText" DataValueField="ValueId" SelectedValue='<%# Eval("LocationID") %>' Enabled="false"></asp:DropDownList>
                        </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreNumber") %>' runat="server" ID="TheatreNumberLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SeatingSize") %>' runat="server" ID="SeatingSizeLabel" Width="110"/></td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #999999;">
                    <td>
                        <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("TheatreID") %>' runat="server" ID="TheatreIDTextBox" Enabled="false" Width ="50"/></td>
                    <td>
                        <asp:DropDownList ID="LocationDropDown" runat="server" DataSourceID="LocationODS" DataTextField="DisplayText" DataValueField="ValueId" SelectedValue='<%# Bind("LocationID") %>'></asp:DropDownList></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("TheatreNumber") %>' runat="server" ID="TheatreNumberTextBox" Width ="80"/></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SeatingSize") %>' runat="server" ID="SeatingSizeTextBox" Width="110"/></td>
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
                <tr style="">
                    <td>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("TheatreID") %>' runat="server" ID="TheatreIDTextBox" Enabled="false" Width ="50"/></td>
                    <td>
                        <asp:DropDownList ID="LocationDropDown" runat="server" DataSourceID="LocationODS" DataTextField="DisplayText" DataValueField="ValueId" SelectedValue='<%# Bind("LocationID") %>'></asp:DropDownList></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("TheatreNumber") %>' runat="server" ID="TheatreNumberTextBox"  Width ="80"/></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SeatingSize") %>' runat="server" ID="SeatingSizeTextBox" Width="110"/></td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #E0FFFF; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreID") %>' runat="server" ID="TheatreIDLabel" Width ="50"/></td>
                    <td>
                        <asp:DropDownList ID="LocationDropDown" runat="server" DataSourceID="LocationODS" DataTextField="DisplayText" DataValueField="ValueId" SelectedValue='<%# Eval("LocationID") %>' Enabled="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreNumber") %>' runat="server" ID="TheatreNumberLabel"  Width ="80"/></td>
                    <td>
                        <asp:Label Text='<%# Eval("SeatingSize") %>' runat="server" ID="SeatingSizeLabel" Width="110"/></td>
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
                                    <th runat="server">Location</th>
                                    <th runat="server">Theatre</th>
                                    <th runat="server">Seating</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreID") %>' runat="server" ID="TheatreIDLabel" Width ="50"/></td>
                    <td>
                        <asp:DropDownList ID="LocationDropDown" runat="server" DataSourceID="LocationODS" DataTextField="DisplayText" DataValueField="ValueId" SelectedValue='<%# Eval("LocationID") %>' Enabled="false"></asp:DropDownList>
                            </td>
                    <td>
                        <asp:Label Text='<%# Eval("TheatreNumber") %>' runat="server" ID="TheatreNumberLabel"  Width ="80"/></td>
                    <td>
                        <asp:Label Text='<%# Eval("SeatingSize") %>' runat="server" ID="SeatingSizeLabel" Width="110"/></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
    </div>
    <asp:ObjectDataSource ID="TheatreODS" runat="server" DataObjectTypeName="OMSTSystem.ViewModels.TheatreItem" DeleteMethod="Theatre_Delete" InsertMethod="Theatre_Add" OldValuesParameterFormatString="original_{0}" SelectMethod="Theatres_List" TypeName="OMSTSystem.BLL.TheatreController" UpdateMethod="Theatre_Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="LocationODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Location_Get" TypeName="OMSTSystem.BLL.LocationController"></asp:ObjectDataSource>
</asp:Content>

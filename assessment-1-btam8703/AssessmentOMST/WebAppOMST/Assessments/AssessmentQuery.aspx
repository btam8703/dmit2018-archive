<%@ Page Title="Query: Movies " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssessmentQuery.aspx.cs" Inherits="WebAppOMST.Assessments.AssessmentQuery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Assessment: Query</h1>
    <asp:GridView ID="MovieGridView" runat="server" AutoGenerateColumns="False" DataSourceID="MovieODS" AllowPaging="True" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"></asp:BoundField>
            <asp:BoundField DataField="Rating" HeaderText="Rating" SortExpression="Rating"></asp:BoundField>
            <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre"></asp:BoundField>
            <asp:BoundField DataField="ScreenType" HeaderText="ScreenType" SortExpression="ScreenType"></asp:BoundField>
            <asp:BoundField DataField="Length" HeaderText="Length" SortExpression="Length"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="MovieODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Movies_ListByYear" TypeName="OMSTSystem.BLL.MovieController"></asp:ObjectDataSource>
</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eRaceProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%-- LOGO --%>

    <%-- Group Logo
        Team member names
        Subsystem
        Setup/Security Responsibilities
        Known Bugs list --%>
    <img src="Images/grouplogo.png" alt="Car logo" />

    <h1>Sales</h1>
        <h2>Jean Pacificar</h2>
        <h2>Security</h2>
        <div>
            <h2>Known Bugs - .....</h2>
            <p>-Font-awesome icons cannot be implemented</p>
            <p>-RefundController - Returnproducts method not working properly</p>
        </div>
        

    <h1>Receiving</h1>
        <p>Fazal Bacchus</p>
        <p>Security</p>
        <p>Known Bugs - .....</p>

    <h1>Purchasing</h1>
        <p>Ben Tam</p>
        <p>Security</p>
        <p>Known Bugs - .....</p>

</asp:Content>

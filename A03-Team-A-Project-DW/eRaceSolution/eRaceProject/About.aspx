<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="eRaceProject.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Jean Pacificar: </h2>
        <h3>Clerk</h3>
        <p>Username: CalderJ</p>
        <p>Password: Password</p>
    </div>

    <div class="jumbotron">
        <h2>Ben Tam: </h2>
        <h3>Director</h3>
        <p>Username: NewlandA</p> 
        <p>Password: Password</p>

        <h3>Office Manager</h3>
        <p>Username: YatesK</p> 
        <p>Password: Password</p>
    </div>
    
    <div class="jumbotron">
        <h2>Fazal Bacchus: </h2>
        <h3>Clerk</h3>
        <p>Username: CalderJ</p>
        <p>Password: Password</p>

        <h3>Food Service</h3>
        <p>Username: KreegM</p> 
        <p>Password: Password</p>
    </div>

    <div class="jumbotron">
        <h2>Webmaster: </h2>
        <p>Username: Webmaster</p>
        <p>Password: Password</p>
        <p>This user is not granted access to any subsystems</p>
    </div>

    <div class="jumbotron">
        <h2>Database Connection String</h2>
        <code>
            <!--Default-->
            name="DefaultConnection" connectionString="Data Source=.;Initial Catalog=aspnet-eRaceProject-20201110111838;Integrated Security=True" providerName="System.Data.SqlClient" 
        </code><br /><br />
        <code>
            name="eRaceDB"
            connectionString="Data Source=.; Initial Catalog=eRace; Integrated Security=true"
            providerName="System.Data.SqlClient"
        </code>
    </div>


    
</asp:Content>

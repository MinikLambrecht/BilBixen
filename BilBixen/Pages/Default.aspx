<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BilBixen.Pages.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" style="margin-bottom: 25px; margin-top: 25px; max-height: 25vw; width: 100%;">
        <img src="../Content/Images/LandingPage_Image.png" alt="Cars for sale" style="height: 25vw; width: 100%;"/>
    </div>

    <div class="row">

        <div class="col-sm-6 col-md-4">
            <div runat="server" class="thumbnail ItemCard" id="PassengerCars">

            </div>
        </div>


        <div class="col-sm-6 col-md-4">
            <div runat="server" class="thumbnail ItemCard" id="WreckedCars">

            </div>
        </div>


        <div class="col-sm-6 col-md-4">
            <div runat="server" class="thumbnail ItemCard" id="IndustrialCars">

            </div>
        </div>
    </div>

</asp:Content>
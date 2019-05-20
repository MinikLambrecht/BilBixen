<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BilBixen.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" style="max-height: 25vw; width: 100%; margin-bottom: 25px; margin-top: 25px">
        <img src="../Content/Images/LandingPage_Image.jpg" alt="Cars for sale" style="width: 100%; height: 25vw" />
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

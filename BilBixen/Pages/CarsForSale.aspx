<%@ Page Title="For Sale" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarsForSale.aspx.cs" Inherits="BilBixen.Pages.CarsForSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="SearchContentContainer" >

    </div>


    <div class="CarGalleryContainer" >
        <div runat="server" class="thumbnail ItemCard" id="SearchResults">
          
        </div>
    </div>

</asp:Content>

<%@ Page Title="Selling" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:AjaxFileUpload ID="FilUploader" runat="server" ThrobberID="MyThrobberID" Mode="Auto" OnUploadComplete="AdPictures_UploadComplete" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <asp:Label ID="List" runat="server" Text="Default Label Text" ></asp:Label>

</asp:Content>

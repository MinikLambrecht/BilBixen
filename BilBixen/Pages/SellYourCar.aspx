<%@ Page Title="Selling" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                <asp:FileUpload ID="AdPictures" runat="server" AllowMultiple="true" />
                <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                        ControlToValidate="AdPictures"
                        ErrorMessage="Only Jpg, Jpeg & PNG images are allowed" 
                        ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])|.*\.([Pp][Nn][Gg])$)">
                </asp:RegularExpressionValidator>
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                <asp:Button runat="server" ID="UploadFiles" Text="Upload" OnClick="uploadFiles_Click" />
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                <asp:Label ID="List" runat="server"  Text="Default Text"/>
            </div>
        </div>
    </div>

</asp:Content>

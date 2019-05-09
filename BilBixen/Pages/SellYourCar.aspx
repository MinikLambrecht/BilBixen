<%@ Page Title="Selling" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <asp:AjaxFileUpload ID="AdPictures" runat="server" />
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <asp:Button runat="server" ID="UploadFiles" Text="Upload" OnClick="uploadFiles_Click" />
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <asp:Label ID="List" runat="server"  Text="Default Text"/>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="UploadFiles" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

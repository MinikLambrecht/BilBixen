<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommentVerification.aspx.cs" Inherits="BilBixen.Pages.Admin.CommentVerification" Async="true" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>


            <div>
                <div runat="server" id="Comment" class="CommentContainer">

                </div>

                <br />

                <div>
                    <asp:Button runat="server" Text="Approve" OnClick="ApproveComment_OnClick"/>
                    <asp:Button runat="server" Text="Reject" OnClick="RejectComment_OnClick"/>
                    <asp:Label runat= "server" Text="" ID="ID_LABEL"></asp:Label>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>
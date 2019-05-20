<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BilBixen.Pages.Login" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/Custom/LoginPage.css" type="text/css" media="screen" />

    <div class="well" style="margin-top: 5%; margin-left: 30%; width: 35vw">
        <ul class="nav nav-tabs">
            <li class="active">
                <a href="#Login" data-toggle="tab">Login</a>
            </li>
            <li>
                <a href="#Register" data-toggle="tab">Create Account</a>
            </li>
        </ul>
        <div id="TabContent" class="tab-content">
            <div class="tab-pane active in" id="Login"></div>

            <div class="tab-pane fade" id="Register">
                <div class="Content">
                    <asp:CreateUserWizard ID="Wiz1" runat="server">
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="NewAccountSteps" runat="server">
                                <ContentTemplate>
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Label ID="UserNameLabel" runat="server" Text="Full Name:" AssociatedControlID="UserName" />

                                                <asp:TextBox ID="UserName" class="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ForeColor="Red" Text="*" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Wiz1" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Label ID="PasswordLabel" runat="server" Text="Password:" AssociatedControlID="Password" />

                                                <asp:TextBox ID="Password" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ForeColor="Red" Text="*" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Wiz1" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm password:" AssociatedControlID="ConfirmPassword" />

                                                <br />

                                                <asp:TextBox ID="ConfirmPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ForeColor="Red" Text="*" ControlToValidate="ConfirmPassword"
                                                    ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                                    ValidationGroup="Wiz1" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Label ID="EmailLabel" runat="server" Text="E-mail:" AssociatedControlID="Email" />

                                                <br />

                                                <asp:TextBox ID="Email" class="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ForeColor="Red" Text="*" ControlToValidate="Email"
                                                    ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="Wiz1" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                                    ValidationGroup="Wiz1" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:Literal ID="ErrorMessage" runat="server" Text="Test error" EnableViewState="False"></asp:Literal>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <CustomNavigationTemplate>
                                    <asp:Button ID="CreateUser" runat="server" CssClass="btn btn-primary" Text="Create User" CommandName="MoveNext" />
                                </CustomNavigationTemplate>
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep runat="server">
                                <ContentTemplate>
                                    <table border="0" style="font-size: 100%; font-family: Verdana" id="TABLE1">
                                        <tr>
                                            <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #5d7b9d; height: 18px;">Complete</td>
                                        </tr>
                                        <tr>
                                            <td>Your account has been successfully created.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">&nbsp;<asp:Button ID="ContinueButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="Continue"
                                                Font-Names="Verdana" ForeColor="#284775" Text="Continue" ValidationGroup="Wiz1" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
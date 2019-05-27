<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BilBixen.Pages.Login" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/Custom/LoginPage.css" type="text/css" media="screen"/>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="well">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a href="#Login" data-toggle="tab">Login</a>
                    </li>
                    <li>
                        <a href="#Register" data-toggle="tab">Create Account</a>
                    </li>
                </ul>
                <div id="TabContent" class="tab-content">
                    <div class="tab-pane active in" id="Login">
                        <asp:Login ID="LoginControl" runat="server" DestinationPageUrl="Default.aspx">
                            <LayoutTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Username is required."
                                                                        ToolTip="Username is required." ForeColor="Red" ValidationGroup="LoginControl"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required."
                                                                        ToolTip="Password is required." ForeColor="Red" ValidationGroup="LoginControl"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="checkbox">
                                                <label><input type="checkbox" id="RememberMe" runat="server">Remember Me</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="LoginButton" CssClass="btn btn-primary" runat="server" CommandName="Login" CausesValidation="True" Text="Log In" ValidationGroup="LoginControl"/>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:Login>
                    </div>

                    <div class="tab-pane fade" id="Register">
                        <asp:CreateUserWizard ID="Wiz1" runat="server" LoginCreatedUser="False" RequireEmail="True" OnCreatedUser="Wiz1_OnCreatedUser">
                            <WizardSteps>
                                <asp:CreateUserWizardStep ID="NewAccountSteps" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="UserNameLabel" runat="server" Text="Username:" AssociatedControlID="UserName"/><span style="color: red;"> *</span>

                                                    <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ForeColor="Red" ControlToValidate="UserName"
                                                                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Wiz1"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="PasswordLabel" runat="server" Text="Password:" AssociatedControlID="Password"/><span style="color: red;"> *</span>

                                                    <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ForeColor="Red" ControlToValidate="Password"
                                                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Wiz1"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm password:" AssociatedControlID="ConfirmPassword"/><span style="color: red;"> *</span>

                                                    <asp:TextBox ID="ConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ForeColor="Red" ControlToValidate="ConfirmPassword"
                                                                                ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                                                                ValidationGroup="Wiz1"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="EmailLabel" runat="server" Text="E-mail:" AssociatedControlID="Email"/><span style="color: red;"> *</span>

                                                    <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ForeColor="Red" ControlToValidate="Email"
                                                                                ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="Wiz1"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                                          ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                                                          ValidationGroup="Wiz1"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: red;">
                                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <CustomNavigationTemplate>
                                        <tr>
                                            <td>
                                                <asp:Button ID="CreateUser" CssClass="btn btn-primary" runat="server" CausesValidation="True" Text="Create User" CommandName="MoveNext"></asp:Button>
                                            </td>
                                        </tr>
                                    </CustomNavigationTemplate>
                                </asp:CreateUserWizardStep>
                                <asp:CompleteWizardStep runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    Your account has been successfully created.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br/>
                                                    <asp:Button ID="ContinueButton" runat="server" CssClass="btn btn-primary" CausesValidation="False" CommandName="Continue" Text="Continue" ValidationGroup="Wiz1"/>
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

            <asp:HiddenField ID="TabName" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
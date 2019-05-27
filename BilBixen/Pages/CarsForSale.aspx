<%@ Page Title="For Sale" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarsForSale.aspx.cs" Inherits="BilBixen.Pages.CarsForSale" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid ContentBlockExtended">
                <div class="row">
                    <!-- Dropdowns -->
                    <div class="col-md-4">
                        <div class="col-md-4">
                            <asp:Label runat="server" Text="Make: "></asp:Label>

                            <br/>
                            <br/>

                            <asp:Label runat="server" Text="Model: "></asp:Label>

                            <br/>
                            <br/>

                            <asp:Label runat="server" Text="Car Type: "></asp:Label>

                            <br/>
                            <br/>

                            <asp:Label runat="server" Text="Fuel: "></asp:Label>

                        </div>

                        <div class="col-md-8">
                            <asp:DropDownList AutoPostBack="true" runat="server" ID="MAKE_LABEL_DROP" OnSelectedIndexChanged="MakeSelect_OnChange">
                                <asp:ListItem Value="0" Text="Any" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <br/>
                            <br/>

                            <asp:DropDownList AutoPostBack="true" runat="server" ID="MODEL_LABEL_DROP" Enabled="false" OnSelectedIndexChanged="AutoSearch_OnChange">
                                <asp:ListItem Value="0" Text="Any" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <br/>
                            <br/>

                            <asp:DropDownList AutoPostBack="true" runat="server" ID="CAR_TYPE_LABEL_DROP" OnSelectedIndexChanged="AutoSearch_OnChange">
                                <asp:ListItem Value="0" Text="Any" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <br/>
                            <br/>

                            <asp:DropDownList AutoPostBack="true" runat="server" ID="FUEL_LABEL_DROP" OnSelectedIndexChanged="AutoSearch_OnChange">
                                <asp:ListItem Value="0" Text="Any" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- -->
                    <div class="col-md-4">
                        <div class="col-md-4">
                            <asp:Label runat="server" Text="KM: "></asp:Label>

                            <br/>
                            <br/>

                            <asp:Label runat="server" Text="PRICE: "></asp:Label>

                            <br/>
                            <br/>

                            <asp:Label runat="server" Text="YEAR: "></asp:Label>

                        </div>

                        <div class="col-md-8">

                            <asp:TextBox AutoPostBack="true" runat="server" ID="KM_LABEL_TEXT1" type="number" style="width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>
                            <asp:TextBox AutoPostBack="true" runat="server" ID="KM_LABEL_TEXT2" type="number" style="width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>

                            <br/>
                            <br/>

                            <asp:TextBox AutoPostBack="true" runat="server" ID="PRICE_LABEL_TEXT1" type="number" style="bottom: 7px; position: relative; width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>
                            <asp:TextBox AutoPostBack="true" runat="server" ID="PRICE_LABEL_TEXT2" type="number" style="bottom: 7px; position: relative; width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>

                            <br/>
                            <br/>

                            <asp:TextBox AutoPostBack="true" runat="server" ID="YEAR_LABEL_TEXT1" type="number" style="bottom: 12px; position: relative; width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>
                            <asp:TextBox AutoPostBack="true" runat="server" ID="YEAR_LABEL_TEXT2" type="number" style="bottom: 12px; position: relative; width: 40%;" OnTextChanged="AutoSearch_OnChange"></asp:TextBox>

                        </div>

                    </div>

                    <!-- -->
                    <div class="col-md-4">

                    </div>

                </div>

                <br/>

                <asp:label runat="server" ID="SearchResultsLabel" Text="0 : Results"/>
            </div>

            <br/>

            <div runat="server" id="CarGalleryContainer">

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
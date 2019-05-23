<%@ Page Title="For Sale" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarsForSale.aspx.cs" Inherits="BilBixen.Pages.CarsForSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="SearchContentContainer container-fluid ContentBlockExtended" >
                <div class="row">
                    <!-- Dropdowns -->
                    <div class="col-md-4">
                        <div class="col-md-4">
                            <asp:Label runat="server" class="MAKE_LABEL" Text="Make: "></asp:Label>

                            <br />
                            <br />

                            <asp:Label runat="server" class="MODEL_LABEL" Text="Model: "></asp:Label>

                            <br />
                            <br />

                            <asp:Label runat="server" class="CAR_TYPE_LABEL" Text="Car Type: "></asp:Label>

                            <br />
                            <br />

                            <asp:Label runat="server" class="FUEL_LABEL" Text="Fuel: "></asp:Label>

                        </div>

                        <div class="col-md-8">
                            <asp:DropDownList AutoPostBack="true" runat="server" class="MAKE_LABEL_DROP" ID="MAKE_LABEL_DROP" OnSelectedIndexChanged="MakeSelect_OnChange">
                                    <asp:ListItem runat="server" ID="Custom_Make" Value="0" Text="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <br />
                            <br />

                            <asp:DropDownList runat="server" class="MODEL_LABEL_DROP" ID="MODEL_LABEL_DROP" Enabled="false">
                                <asp:ListItem runat="server" ID="ListItem1" Value="0" Text="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <br />
                            <br />

                            <asp:DropDownList runat="server" class="CAR_TYPE_LABEL_DROP" ID="CAR_TYPE_LABEL_DROP">

                            </asp:DropDownList>

                            <br />
                            <br />

                            <asp:DropDownList runat="server" class="FUEL_LABEL_DROP" ID="FUEL_LABEL_DROP">

                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- -->
                    <div class="col-md-4">
                        <div class="col-md-4">
                            <asp:Label runat="server" class="KM_LABEL" Text="KM: "></asp:Label>

                            <br />
                            <br />

                            <asp:Label runat="server" class="PRICE_LABEL" Text="PRICE: "></asp:Label>

                            <br />
                            <br />

                            <asp:Label runat="server" class="YEAR_LABEL" Text="YEAR: "></asp:Label>

                        </div>

                        <div class="col-md-8">

                            <input runat="server" id="KM_LABEL_TEXT1" type="text" style="width:40%;"/>
                            <input runat="server" id="KM_LABEL_TEXT2" type="text" style="width:40%;"/>

                            <br />
                            <br />

                            <input runat="server" id="PRICE_LABEL_TEXT1" type="text" style="width:40%; position:relative; bottom: 7px;"/>
                            <input runat="server" id="PRICE_LABEL_TEXT2" type="text" style="width:40%; position:relative; bottom: 7px;"/>

                            <br />
                            <br />

                            <input runat="server" id="YEAR_LABEL_TEXT1" type="text" style="width:40%; position:relative; bottom: 12px;"/>
                            <input runat="server" id="YEAR_LABEL_TEXT2" type="text" style="width:40%; position:relative; bottom: 12px;"/>

                        </div>

                    </div>

                    <!-- -->
                    <div class="col-md-4">

                    </div>

                </div>

                <br />

                <asp:label runat="server" ID="SearchResultsLabel" Text="0 : Results"/>
            </div>


            <div runat="server" id="CarGalleryContainer" class="CarGalleryContainer" >

                <!--

                    Example
                <div runat="server" class="thumbnail ItemCard" id="SearchResults">
                    <img class="ItemCardImage" src="/Images/{_AD_ID}/{files[i].Name}">
                    <br />
                    <div class="caption ContentBlock ItemCardDesc">
                    <h3>{_MAKE}, {_MODEL}</h3>
                    <h4>{_ENGINE}</h4>
                    <p>KM: {_KM}</p>
                    <p>{_PRICE},- DKK</p>
                    <p>
                    <a href = "/AD/{_AD_ID}" class="btn btn-primary" role="button">View Car</a>
                    </p>
                    </div>
                </div>
                    -->

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

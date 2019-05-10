<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" Async="true" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <hr />

            <input id="PlateOrVIN" runat="server" placeholder="Type in License plate or VIN" style="width:250px; text-transform: uppercase" />
            <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="LoadData" />

            <hr />

            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    <asp:Label ID="LicensePlatelbl" runat="server" Text="License plate: "></asp:Label>
                    <br />
                    <asp:Label ID="Statuslbl" runat="server" Text="Status: "></asp:Label>
                    <br />
                    <asp:Label ID="StatusDatelbl" runat="server" Text="Status date: "></asp:Label>
                    <br />
                    <asp:Label ID="CarTypelbl" runat="server" Text="Car type: "></asp:Label>
                    <br />
                    <asp:Label ID="Uselbl" runat="server" Text="Use: "></asp:Label>
                    <br />
                    <asp:Label ID="FirstRegistrationlbl" runat="server" Text="First registration: "></asp:Label>
                    <br />
                    <asp:Label ID="VINNumberlbl" runat="server" Text="VIN: "></asp:Label>
                    <br />
                    <asp:Label ID="OwnWeightlbl" runat="server" Text="Own weight: "></asp:Label>
                    <br />
                    <asp:Label ID="TotalWeightlbl" runat="server" Text="Total weight: "></asp:Label>
                    <br />
                    <asp:Label ID="Axelslbl" runat="server" Text="Axels: "></asp:Label>
                    <br />
                    <asp:Label ID="PullingAxelslbl" runat="server" Text="Pulling axels: "></asp:Label>
                    <br />
                    <asp:Label ID="Seatslbl" runat="server" Text="Seats: "></asp:Label>
                    <br />
                    <asp:Label ID="Clutchlbl" runat="server" Text="Clutch: "></asp:Label>
                    <br />
                    <asp:Label ID="Doorslbl" runat="server" Text="Doors: "></asp:Label>
                    <br />
                    <asp:Label ID="Makelbl" runat="server" Text="Make: "></asp:Label>
                    <br />
                    <asp:Label ID="Modellbl" runat="server" Text="Model: "></asp:Label>
                    <br />
                    <asp:Label ID="Variantlbl" runat="server" Text="Variant: "></asp:Label>
                    <br />
                    <asp:Label ID="ModelTypelbl" runat="server" Text="Model type: "></asp:Label>
                    <br />
                    <asp:Label ID="ModelYearlbl" runat="server" Text="Model year: "></asp:Label>
                    <br />
                    <asp:Label ID="Colorlbl" runat="server" Text="Color: "></asp:Label>
                    <br />
                    <asp:Label ID="ChasisTypelbl" runat="server" Text="Chasis type: "></asp:Label>
                    <br />
                    <asp:Label ID="EngineCylinderslbl" runat="server" Text="Engine cylinders: "></asp:Label>
                    <br />
                    <asp:Label ID="EngineVolumelbl" runat="server" Text="Engine volume: "></asp:Label>
                    <br />
                    <asp:Label ID="EnginePowerlbl" runat="server" Text="Engine power: "></asp:Label>
                    <br />
                    <asp:Label ID="FuelTypelbl" runat="server" Text="Fuel type: "></asp:Label>
                    <br />
                    <asp:Label ID="RegistrationZIPlbl" runat="server" Text="Registration zip: "></asp:Label>
                </div>

                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                    <asp:Label ID="LicensePlatelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="StatuslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="StatusDatelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CarTypelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="UselblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="FirstRegistrationlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="VINNumberlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="OwnWeightlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="TotalWeightlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="AxelslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="PullingAxelslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="SeatslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ClutchlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="DoorslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="MakelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ModellblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="VariantlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ModelTypelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ModelYearlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ColorlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ChasisTypelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="EngineCylinderslblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="EngineVolumelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="EnginePowerlblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="FuelTypelblText" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="RegistrationZIPlblText" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

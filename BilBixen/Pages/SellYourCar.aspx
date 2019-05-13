<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" Async="true" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            
            <asp:AsyncFileUpload ID="ASYNC_FILE_UPLOAD" runat="server" OnClientUploadComplete="uploadComplete"  UploaderStyle="Modern" Width="100%" UploadingBackColor="#CCFFFF" ThrobberID="myThrobber" />

            
            <script>
                function uploadComplete() {
                    var adId = '<%= _AD_ID%>';
                    alert(adId);
                }
                function uploadError() {

                }
            </script>
            
            <asp:button ID="TEST_BUTTON" runat="server" OnClientClick="javascript:alert();" Text="Click me!" />

            <hr />

            <input id="PLATEORVIN_LABEL_TEXT" runat="server" placeholder="Type in License plate or VIN" style="width:250px; text-transform: uppercase" />
            <asp:Button ID="SEARCH_BUTTON" runat="server" Text="Search" OnClick="LoadData" />

            <hr />

            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    <asp:Label ID="LICENSE_PLATE" runat="server" Text="License plate: "></asp:Label>
                    <br />
                    <asp:Label ID="STATUS_LABEL" runat="server" Text="Status: "/>
                    <br />
                    <asp:Label ID="STATUS_DATE_LABEL" runat="server" Text="Status date: "></asp:Label>
                    <br />
                    <asp:Label ID="CAR_TYPE_LABEL" runat="server" Text="Car type: "></asp:Label>
                    <br />
                    <asp:Label ID="USE_LABEL" runat="server" Text="Use: "></asp:Label>
                    <br />
                    <asp:Label ID="FIRST_REGISTRATION_LABEL" runat="server" Text="First registration: "></asp:Label>
                    <br />
                    <asp:Label ID="VIN_NUMBER_LABEL" runat="server" Text="VIN: "></asp:Label>
                    <br />
                    <asp:Label ID="OWN_WEIGHT_LABEL" runat="server" Text="Own weight: "></asp:Label>
                    <br />
                    <asp:Label ID="TOTAL_WEIGHT_LABEL" runat="server" Text="Total weight: "></asp:Label>
                    <br />
                    <asp:Label ID="AXLES_LABEL" runat="server" Text="Axles: "></asp:Label>
                    <br />
                    <asp:Label ID="PULLING_AXLES_LABEL" runat="server" Text="Pulling axles: "></asp:Label>
                    <br />
                    <asp:Label ID="SEATS_LABEL" runat="server" Text="Seats: "></asp:Label>
                    <br />
                    <asp:Label ID="CLUTCH_LABEL" runat="server" Text="Clutch: "></asp:Label>
                    <br />
                    <asp:Label ID="DOORS_LABEL" runat="server" Text="Doors: "></asp:Label>
                    <br />
                    <asp:Label ID="MAKE_LABEL" runat="server" Text="Make: "></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_LABEL" runat="server" Text="Model: "></asp:Label>
                    <br />
                    <asp:Label ID="VARIANT_LABEL" runat="server" Text="Variant: "></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_TYPE_LABEL" runat="server" Text="Model type: "></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_YEAR_LABEL" runat="server" Text="Model year: "></asp:Label>
                    <br />
                    <asp:Label ID="COLOR_LABEL" runat="server" Text="Color: "></asp:Label>
                    <br />
                    <asp:Label ID="CHASSIS_LABEL" runat="server" Text="Chassis type: "></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_CYLINDERS_LABEL" runat="server" Text="Engine cylinders: "></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_VOLUME_LABEL" runat="server" Text="Engine volume: "></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_POWER_LABEL" runat="server" Text="Engine power: "></asp:Label>
                    <br />
                    <asp:Label ID="FUEL_TYPE_LABEL" runat="server" Text="Fuel type: "></asp:Label>
                    <br />
                    <asp:Label ID="REGISTRATIONZIPCODE_LABEL" runat="server" Text="Registration zip: "></asp:Label>
                </div>

                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                    <asp:Label ID="LICENSE_PLATE_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="STATUS_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="STATUS_DATE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CAR_TYPE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="USE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="FIRST_REGISTRATION_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="VIN_NUMBER_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="OWN_WEIGHT_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="TOTAL_WEIGHT_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="AXLES_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="PULLING_AXLES_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="SEATS_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CLUTCH_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="DOORS_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="MAKE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="VARIANT_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_TYPE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="MODEL_YEAR_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="COLOR_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CHASSIS_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_CYLINDERS_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_VOLUME_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ENGINE_POWER_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="FUEL_TYPE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="REGISTRATIONZIPCODE_LABEL_TEXT" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

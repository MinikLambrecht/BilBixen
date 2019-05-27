<%@ Page Title="Sell your car" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellYourCar.aspx.cs" Inherits="BilBixen.Pages.SellYourCar" Async="true" %>

<asp:Content ID="Body_Content" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Label runat="server" ID="ADID_LABEL"><%= ViewState["CAR_AD_ID"] %></asp:Label>

            <hr/>

            <asp:FileUpload ID="fileDocument" runat="server"/>

            <br/>
            <asp:Label runat="server" ID="infoLabel" Text="Label Stuff..."></asp:Label>

            <script>
                //Trim the input text
                function Trim(input) {
                    var lre = /^\s*/;
                    var rre = /\s*$/;
                    input = input.replace(lre, "");
                    input = input.replace(rre, "");
                    return input;
                }

                // filter the files before Uploading for text file only
                function CheckForTestFile() {
                    var file = document.getElementById('<%= fileDocument.ClientID %>');
                    var fileName = file.value;
                    //Checking for file browsed or not
                    if (Trim(fileName) === '') {
                        alert("No file has been selected!");
                        file.focus();
                        return false;
                    }

                    //Setting the extension array for diff. type of text files
                    var extArray = new Array(".jpeg", ".jpg", ".png");

                    //getting the file name
                    while (fileName.indexOf("\\") !== -1)
                        fileName = fileName.slice(fileName.indexOf("\\") + 1);

                    //Getting the file extension
                    var ext = fileName.slice(fileName.indexOf(".")).toLowerCase();

                    //matching extension with our given extensions.
                    for (var i = 0; i < extArray.length; i++) {
                        if (extArray[i] === ext) {
                            return true;
                        }
                    }
                    alert("Please only upload files that end in types:  " +
                        (extArray.join("  ")) +
                        "\nPlease select a new " +
                        "file to upload and submit again.");
                    file.focus();
                    return false;
                }
            </script>

            <hr/>

            <p>When searching all uploaded images has to be reuploaded</p>
            <p>Please upload after using the plate search</p>
            <input id="PLATEORVIN_LABEL_TEXT" runat="server" placeholder="Type in License plate or VIN" style="text-transform: uppercase; width: 250px;"/>
            <asp:Button ID="SEARCH_BUTTON" runat="server" Text="Search" OnClick="LoadData" CausesValidation="false"/>

            <hr/>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:Label ID="PRICE_LABEL" runat="server" Text="Price: "></asp:Label>
                    <input ID="PRICE_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="LICENSE_PLATE" runat="server" Text="License plate: "></asp:Label>
                    <input ID="LICENSE_PLATE_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="MAKE_LABEL" runat="server" Text="Make: "></asp:Label>
                    <asp:DropDownList AutoPostBack="true" ID="MAKE_LABEL_TEXT" runat="server" OnSelectedIndexChanged="MakeSelect_OnChange">
                        <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <br/>
                    <br/>
                    <asp:Label ID="MODEL_LABEL" runat="server" Text="Model: "></asp:Label>
                    <asp:DropDownList runat="server" ID="MODEL_LABEL_TEXT" Enabled="false">
                        <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <br/>
                    <br/>
                    <asp:Label ID="KM_LABEL" runat="server" Text="KM: "></asp:Label>
                    <input ID="KM_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="FUEL_TYPE_LABEL" runat="server" Text="Fuel type: "></asp:Label>
                    <select ID="FUEL_TYPE_LABEL_TEXT" runat="server">

                    </select>
                    <br/>
                    <br/>
                    <asp:Label ID="CAR_TYPE_LABEL" runat="server" Text="Car type: "></asp:Label>
                    <select ID="CAR_TYPE_LABEL_TEXT" runat="server">

                    </select>
                    <br/>
                    <br/>
                    <asp:Label ID="VARIANT_LABEL" runat="server" Text="Variant: "></asp:Label>
                    <input ID="VARIANT_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="MODEL_TYPE_LABEL" runat="server" Text="Model type: "></asp:Label>
                    <input ID="MODEL_TYPE_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="ENGINE_POWER_LABEL" runat="server" Text="Engine power: "></asp:Label>
                    <input ID="ENGINE_POWER_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="FIRST_REGISTRATION_LABEL" runat="server" Text="First registration: "></asp:Label>
                    <input ID="FIRST_REGISTRATION_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="VIN_NUMBER_LABEL" runat="server" Text="VIN: "></asp:Label>
                    <input ID="VIN_NUMBER_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="OWN_WEIGHT_LABEL" runat="server" Text="Own weight: "></asp:Label>
                    <input ID="OWN_WEIGHT_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="TOTAL_WEIGHT_LABEL" runat="server" Text="Total weight: "></asp:Label>
                    <input ID="TOTAL_WEIGHT_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="SEATS_LABEL" runat="server" Text="Seats: "></asp:Label>
                    <input ID="SEATS_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="DOORS_LABEL" runat="server" Text="Doors: "></asp:Label>
                    <input ID="DOORS_LABEL_TEXT" runat="server" Value=""/>
                    <br/>
                    <br/>
                    <asp:Label ID="MODEL_YEAR_LABEL" runat="server" Text="Model year: "></asp:Label>
                    <input ID="MODEL_YEAR_LABEL_TEXT" runat="server" Value=""/>
                </div>
            </div>

            <br/>

            <div>
                <asp:button runat="server" ID="SubmitAdButton" class="btn btn-primary" OnClick="SubmitAD_OnClick" Text="Submit Ad"/>
            </div>

            <hr/>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="SubmitAdButton"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
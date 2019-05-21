<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarAdPage.aspx.cs" Inherits="BilBixen.Pages.CarAdPage" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="MainContainer container" style="background-color: white; border-radius: 10px">
        <h1><%= _MAKE %></h1>
        <p class="xLargeText"> <%= $"{_MODEL}, {_ENGINE} - {_FUELTYPE}" %></p>

        <div class="row">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-8">
                        <!-- Bootstrap CSS -->
                        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
                        <div class="container-fluid">
		
                            <!-- Carousel container -->
                            <div id="my-pics" class="carousel slide" data-ride="carousel" style="margin:auto;">

                                <!-- Content -->
                                <div runat="server" id="ImageMenu" class="carousel-inner ImageMenu" role="listbox">



                                </div>

                                <!-- Previous/Next controls -->
                                <a class="left carousel-control" href="#my-pics" role="button" data-slide="prev">
                                    <span class="icon-prev" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#my-pics" role="button" data-slide="next">
                                    <span class="icon-next" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>

                            </div>
                        </div>
		
                        <!-- jQuery library -->
                        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
                        <!-- Bootstrap JS -->
                        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

                        <!-- Initialize Bootstrap functionality -->
                        <script>
                            // Initialize tooltip component
                            $(function () {
                                $('[data-toggle="tooltip"]').tooltip()
                            })

                            // Initialize popover component
                            $(function () {
                                $('[data-toggle="popover"]').popover()
                            })
                        </script>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="col-md-4">
                <div class="BackPlate">
                    <p class="LargerText">Heres a list</p>

                    <ul class="LargerText">
                        <li><b>Price: </b><br /> <%= _PRICE %></li>
                        <br />
                        <li><b>KM: </b><br /> <%= _KM %></li>
                        <br />
                        <li><b>1st Register: </b><br /> <%= _FIRSTREGISTRATION %></li>
                        <br />
                        <li><b>Model year: </b><br /> <%= _MODELYEAR %></li>
                    </ul>
                </div>
            </div>
        </div>

        <br />


        <div class="CommentSection BackPlate">

            <ul class="nav nav-pills">
                <li class="active"><a data-toggle="pill" href="#Desc">Description</a></li>
                <li><a data-toggle="pill" href="#Info">Info</a></li>
                <li><a data-toggle="pill" href="#CommentArea">Comments<span class="badge"> <%= commentAmount %></span></a></li>
            </ul>

            <div class="TabContentBackground">

                <div class="tab-content">
                    <br />
                    <hr />
                    
                    <!-- Description -->
                    <div id="Desc" class="tab-pane fade in active">
                        
                        <h2 class="DescTitle">This is a title</h2>
                        <p class="DescText">Here there will be lots of text like lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots & lots of text.</p>
                    </div>


                    <!-- Info -->
                    <div id="Info" class="tab-pane fade">

                        <div class="ContentBlockExtended">
                            <h2 class="InfoTitle">Details: </h2>
                        
                            <ul class="LargerText">
                                <li><b>Make: </b><br /> <%= _MAKE %></li>
                                <br />
                                <li><b>Model: </b><br /> <%= _MODEL %></li>
                                <br />
                                <li><b>Type: </b><br /> <%= _ENGINE %></li>
                                <br />
                                <li><b>Fuel: </b><br /> <%= _FUELTYPE %></li>
                                <br />
                                <li><b>KM: </b><br /> <%= _KM %></li>
                                <br />
                                <li><b>Model year: </b><br /> <%= _MODELYEAR %></li>
                                <br />
                                <li><b>1st Register: </b><br /> <%= _FIRSTREGISTRATION %></li>
                                <br />
                                <li><b>License Plate: </b><br /> <%= _PLATE %></li>
                                <br />
                                <li><b>Total weight: </b><br /> <%= _TOTALWEIGHT %></li>'
                                <br />
                                <li><b>Doors: </b><br /> <%= _DOORS %></li>
                                <br />
                                <li><b>Color: </b><br /> <%= _COLOR %></li>
                            </ul>
                        </div>
                    </div>

                    <!-- Comments -->
                    <div id="CommentArea" class="tab-pane fade">

                        <div runat="server" id="Comments" class="CommentContainer">

                        </div>

                        <hr />

                        <div class="WriteComment input-block-level">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="container-fluid">
                                        <div class="col-md-10">
                                            <textarea runat="server" id="CommentTextArea" class="CommentTextbox" placeholder="Write a comment here" required></textarea>
                                        </div>
                            
                                        <div class="col-md-2">
                                            <asp:Button runat="server" CssClass="btn btn-success CommentSubmitButton" Text="Submit" OnClick="Comment_Click"/>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarAdPage.aspx.cs" Inherits="BilBixen.Pages.CarAdPage" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="MainContainer container" style="background-color: white; border-radius: 10px">
        <h1><%= _MAKE %></h1>
        <p class="xLargeText"> <%= $"{_MODEL} {_FUELTYPE}" %></p>

        <div class="row">
            <div class="col-md-8" style="display:flex; justify-content:center">
                <img src="../Assets/Images/Assests/img/1391804290_bf9cbcc629_z.jpg" style="width: 90%; height: 90%; border-radius: 10px; border:8px solid lightgray"/>
            </div>

            <div class="col-md-4">
                <div class="BackPlate">
                    <p class="LargerText">Heres a list</p>

                    <ul class="LargerText">
                        <li><b>Price: </b><br /> Number</li>
                        <br />
                        <li><b>KM: </b><br /> Number</li>
                        <br />
                        <li><b>1st Register: </b><br /> <%= _FIRSTREGISTRATION %></li>
                        <br />
                        <li><b>Model year: </b><br /> <%= _MODELYEAR %></li>
                    </ul>
                </div>
            </div>
        </div>


        <div class="CommentSection BackPlate">

            <ul class="nav nav-pills">
                <li class="active"><a data-toggle="pill" href="#Desc">Description</a></li>
                <li><a data-toggle="pill" href="#Info">Info</a></li>
                <li><a data-toggle="pill" href="#CommentArea">Comments</a></li>
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

                        <h2 class="InfoTitle"><%= $"{_MAKE} {_MODEL}" %></h2>
                        
                        <ul>
                            <li> Info</li>
                        </ul>
                    </div>


                    <!-- Comments -->
                    <div id="CommentArea" class="tab-pane fade">

                        <div runat="server" id="Comments">

                        </div>

                        <hr />

                        <div class="WriteComment input-block-level">
                            <textarea class="CommentTextbox" placeholder="Write your comment here" required> </textarea>
                            
                            <input class="btn btn-success CommentSubmitButton" type="submit"/>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

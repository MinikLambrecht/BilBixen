<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarAdPage.aspx.cs" Inherits="BilBixen.Pages.CarAdPage" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="MainContainer container" style="background-color: white; border-radius: 10px">
        <h1><%= _MAKE %></h1>
        <p class="xLargeText"> <%= $"{_MAKE} {_FUELTYPE}" %></p>

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
                <li><a data-toggle="pill" href="#Comments">Comments</a></li>
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

                        <h2 class="InfoTitle">Menu 1</h2>
                        <p>Some content in menu 1.</p>
                    </div>


                    <!-- Comments -->
                    <div id="Comments" class="tab-pane fade">

                        <div class="CommentBox" style="">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will be placing comment text here</p>
                        </div>

                        <div class="CommentBox">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will be placing comment text here but more</p>
                        </div>

                        <div class="CommentBox">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will place less text here</p>
                        </div>

                        <div class="CommentBox">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will be placing comment text here</p>
                        </div>

                        <div class="CommentBox">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will be placing comment text here</p>
                        </div>

                        <div class="CommentBox">
                            <p class="CommentUsername">Username</p>
                            <p class="CommentText">I will be placing comment text here</p>
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

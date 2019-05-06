<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarAdPage.aspx.cs" Inherits="BilBixen.Pages.CarAdPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="MainContainer container" style="background-color: white; border-radius: 10px">
        <h1>CarTitle</h1>
        <p class="xLargeText"> Quick Info</p>

        <div class="row">
            <div class="col-md-8" style="display:flex; justify-content:center">
                <img src="../Assets/Images/Assests/img/1391804290_bf9cbcc629_z.jpg" style="width: 90%; height: 90%; border-radius: 10px; border:8px solid lightgray"/>
            </div>

            <div class="col-md-4">
                <div class="jumbotron">
                    <p class="LargerText">Heres a list</p>

                    <ul class="LargerText">
                        <li><b>Price: </b><br /> Number</li>
                        <br />
                        <li><b>KM: </b><br /> Number</li>
                        <br />
                        <li><b>1st Register: </b><br /> Date</li>
                        <br />
                        <li><b>KM/l: </b><br /> Number</li>
                        <br />
                        <li><b>Model year: </b><br /> Number</li>
                    </ul>
                </div>
            </div>
        </div>


        <div class="CommentSection jumbotron">

            <ul class="nav nav-pills">
                <li class="active"><a data-toggle="pill" href="#Desc">Description</a></li>
                <li><a data-toggle="pill" href="#Info">Info</a></li>
                <li><a data-toggle="pill" href="#Comments">Comments</a></li>
            </ul>

            <div class="TabContentBackground">

                <div class="tab-content">
                    <div id="Desc" class="tab-pane fade in active">
                        <br />
                        <hr />

                        <h3>HOME</h3>
                        <p>Some content.</p>
                    </div>

                    <div id="Info" class="tab-pane fade">
                        <br />
                        <hr />

                        <h3>Menu 1</h3>
                        <p>Some content in menu 1.</p>
                    </div>

                    <div id="Comments" class="tab-pane fade">
                        <br />
                        <hr />

                        <div class="CommentBox" style="">
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

                        <div class="WriteComment">
                            <textarea class="CommentTextbox" type="text" placeholder="Write your comment here" required> </textarea>
                            <br />
                            <input class="btn btn-success pull-right" type="submit"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Biblioteca.Register" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        
        <asp:Panel ID="pnlError" runat="server" Visible="false">
            <div class="alert alert-danger alert-dismissible" role="alert">
                <button class="close" data-dismiss="alert" type="button">
                    <span aria-hidden="true">&times</span> <span class="sr-only">Close</span>
                </button>
                <asp:Label ID="lblError" runat="server" Text="" />

            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
            <div class="alert alert-success alert-dismissible" role="alert">
                
                <button type="button" class="close" data-dismiss="alert" >
                    <span aria-hidden="true">&times;</span> <span class="sr-only">Close</span>
                </button>
                <asp:Label ID="lblSuccess" runat="server" Text="" />
            </div>
        </asp:Panel>
        <h1 class="page-header">
            <span class="type-title">
            <asp:Label ID="lblBooks" runat="server" Text="Register" />
            </span>

        </h1>
        <div class="form-horizontal" role="form">
            <div class="">
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h4>Use a local account to log in.</h4>
                        <hr />
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtUserName" CssClass="col-md-2 control-label">User Name</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtConfPass" CssClass="col-md-2 control-label">Confirm Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtConfPass" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-default" OnClick="btnRegister_Click"  />
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </asp:Content>


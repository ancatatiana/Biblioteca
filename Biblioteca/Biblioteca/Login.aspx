<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Biblioteca.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <div class="alert alert-danger alert-dismissible" role="alert">
            <asp:Label ID="lblError" runat="server" Text="" />

            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span> <span class="sr-only">Close</span>
            </button>
           </div>
            
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissible" role="alert">
                    <asp:Label ID="lblSuccess" runat="server" Text="" />
                    <button class="close" data-dismiss="alert" type="button">
                        <span aria-hidden="true">×</span> <span class="sr-only">Close</span>
                    </button>
                #</div>
            </asp:Panel>
            
       
    </asp:Panel>
    <h1 class="page-header"><span class="type-title">
        <asp:Label ID="lblBooks" runat="server" Text="Login" />
        <i class="fa fa-angle-down"></i></span>

    </h1>
    <div class="form-horizontal" role="form">
    <div class="">
    </div>
   </div>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                </div>
    <h4>Use a local account to log in.</h4>
    <hr />
 <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtUserName" CssClass="col-md-2 control-label">User Name</asp:Label>
    <div class="col-md-10">
    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Height="32px" Width="1109px" />
        </div>
 </div>
    <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Parola</asp:Label>
    <div class="col-md-10">

    
    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Width="1107px"  />
    </div>
   </div>
    <div class="form-group">
    <div class="col-md-offset-2 col-md-10">
        <asp:Button ID="Log" runat="server" Text="Login" CssClass="btn btn-default" OnClick="Log_Click" />
    </div>
   </div>
</section>
    </div>
    </div>
    </div>

</asp:Content>



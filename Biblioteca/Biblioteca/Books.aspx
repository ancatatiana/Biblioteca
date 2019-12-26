<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Biblioteca.Books" %>


<asp:Content ID="Content" runat="server" ContentPlaceHolderID="MainContent">
        <asp:Label ID="lblError" runat="server" Text="" />
            <asp:Panel ID="pnlError" runat="server" Visible="false">
                <div class="alert alert-danger alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert" >
                        <span aria-hidden="true">&times;</span> <span class="sr-only">Close</span>
                    </button>
                    [lblError]</div>   
                
            </asp:Panel>
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissible" role="alert">
                    <button type="button"class="close" data-dismiss="alert" >
                        <span aria-hidden="true">&times;</span> <span class="sr-only">Close</span>
                    </button>
                    <asp:Label ID="lblSuccess" runat="server" Text="" />
                </div>
            </asp:Panel>
        
        
            
        <h1 class="page-header">
            <span class="type-title"><i class="fa fa-history"></i>
            
            <asp:Label ID="lblBooks" runat="server" Text="Books" />
            <i class="fa fa-angle-down"></i></span>

        </h1>
        <div class="form-horizontal" role="form">
            <div class="">
                <asp:UpdatePanel ID="pnlBook" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="divFirstRow" class="row">
                            <div id="divBook" class="form-group col-sm-4">
                                <asp:Label ID="lblNameOfBook" runat="server" AssociatedControlID="txtNameOfBook" CssClass="col-sm-4 control-label" Text="Name of book" />
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtNameOfBook" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="divAuthor" class="form-group col-sm-4">
                                <asp:Label ID="lblAuthor" runat="server" AssociatedControlID="txtAuthor" CssClass="col-sm-4 control-label" Text="The author's name" />
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="divYear" class="form-group col-sm-4 has-feedback">
                                <asp:Label ID="lblYear" runat="server" AssociatedControlID="ddlYear" CssClass="col-sm-4 control-label" Text="Year" />
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlYEAR" runat="server" CssClass="btn btn-default dropdown-toggle" Enabled="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div id="divSecondRow" class="row">
                            <div id="divISBN" class="form-group col-sm-4">
                                <asp:Label ID="lblISBN" runat="server" AssociatedControlID="ddlISBN" CssClass="col-sm-4 control-label" Text="ISBN" />
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlISBN" runat="server" CssClass="btn btn-default dropdown-toggle" Enabled="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-4">
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-4">
                                <asp:Button ID="btnSaveBook" runat="server" CssClass="btn btn-success " OnClick="btnSaveBook_Click" Text="Save Book" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="divFifthRow" class="row">
                    <div class="form-group col-sm-4"></div>
            </div>
        </div>
                        <asp:Panel ID="Panel1" runat="server">
                        
    
    <%--<asp:GridView ID="gridListOfBooks" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" CssClass="table table-hover table-striped bootstrap-pagination" DataKeyNames="id_books" GridLines="None" OnPageIndexChanging="gridListOfBooks_PageIndexChanging" OnRowCommand="gridListOfBooks_RowCommand" OnSorting="gridListOfBooks_Sorting" PageSize="5" Width="1126px" OnSelectedIndexChanged="gridListOfBooks_SelectedIndexChanged">--%>
                            <asp:UpdatePanel ID="updateListOfBooks" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gridListOfBooks" />
                                </Triggers>
                            </asp:UpdatePanel>
    <asp:GridView ID="gridListOfBooks" runat="server" AllowSorting="True" AllowPaging="True"
                                AutoGenerateColumns="False" OnSorting="gridListOfBooks_Sorting" DataKeyNames="id_books"
                                PageSize="5" OnRowCommand="gridListOfBooks_RowCommand" OnPageIndexChanging="gridListOfBooks_PageIndexChanging"
                                BorderStyle="None" GridLines="None"
                                CssClass="table table-hover table-striped bootstrap-pagination" UseAccessibleHeader="true" OnSelectedIndexChanged="gridListOfBooks_SelectedIndexChanged">        


            <Columns>
                <asp:TemplateField HeaderText="" SortExpression="id_books">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkReference" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nr_crt" HeaderText="Nr. Crt" />
                <asp:BoundField DataField="name_of_book" HeaderText="Name of book" SortExpression="name_of_book" />
                <asp:BoundField DataField="author_name" HeaderText="Author name" SortExpression="author_name" />
                <asp:BoundField DataField="year" HeaderText="Year" SortExpression="year" />
                <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" />
                <asp:TemplateField ItemStyle-Width="190" ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("id_book") %>' CommandName="deleteBook" CssClass="btn btn-default" UseSubmitBehavior="true">
                                    <i class="fa fa-remove"></i>
                                    <span> Delete&nbsp; </span>
                                            </asp:LinkButton>
                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="false" CommandArgument='<%# Eval("id_book") %>' CommandName="updateBook" CssClass="btn btn-default" UseSubmitBehavior="true">
                                    <i class="fa fa-remove"></i>
                                    <span> Update&nbsp; </span>
                                            </asp:LinkButton>
                    </ItemTemplate>

<ItemStyle Width="190px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       </asp:Panel>                         
        </div>

</asp:Content>


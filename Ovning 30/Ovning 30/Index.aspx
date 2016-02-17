<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Ovning_30.WebForm3" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceMain" runat="server">

    <asp:Literal ID="Literal" runat="server"></asp:Literal>
    <!-- Trigger the modal with a button -->
    <button type="button" class="btn btn-default btn-sm" onclick="showEditModal('', '', '', '', 'add')">Add Contact</button>

    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Contact</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="firstName" runat="server" Text="" CssClass="editFirstName form-control" ></asp:TextBox>
                    <br>
                    <p>First name</p>
                    <br>
                    <asp:TextBox ID="lastName" runat="server" Text="" CssClass="editLastName form-control"></asp:TextBox>
                    <br>
                    <p>Last name</p>
                    <br>
                    <asp:TextBox ID="ssn" runat="server" Text="" CssClass="editSSN form-control"></asp:TextBox>
                    <br>
                    <p>Social security number</p>
                    <a/>
                    <asp:TextBox ID="txtAction" runat="server" CssClass="txtAction fade" ></asp:TextBox>
                    <asp:TextBox ID="txtId" runat="server" CssClass="txtId fade"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="submit" id="Button1" class="btn btn-info buttonName" data-submit="modal">Add</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

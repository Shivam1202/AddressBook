<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
            <h2>
                State List
            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink runat="server" Text="Add New State" NavigateUrl="~/AdminPanel/State/StateAddEdit.aspx" CssClass="btn btn-success"></asp:HyperLink>
         </div>
    </div>
       <div class="col-md-12">
            <h4>
              <asp:Label runat="server" ID="lblMessage" EnableViewState="false" />
            </h4>
        </div>
    <div class="row">
    
    <div class="row">
        <div class="col-md-12 col-xs-6">
            <asp:GridView ID="gvState" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" OnRowCommand="gvState_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink CssClass="btn btn-primary btn-sm" runat="server" ID="hlEdit" Text="Edit" NavigateUrl='<%# "~/AdminPanel/State/StateAddEdit.aspx?StateID="+ Eval("StateID").ToString().Trim() %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete"  OnClientClick="javascript:return confirm('Are you sure you want to delete record ?');" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("StateID").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <%--<asp:BoundField DataField="StateID" HeaderText="ID" />--%>
                     <asp:BoundField DataField="CountryName" HeaderText="Country" />
                     <asp:BoundField DataField="StateName" HeaderText="State" />
                    <asp:BoundField DataField="StateCode" HeaderText="State Code" />
                   
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
    </div>
    </div>
</div>
</asp:Content>


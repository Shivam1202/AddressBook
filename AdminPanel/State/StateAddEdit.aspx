<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateAddEdit.aspx.cs" Inherits="AdminPanel_State_StateAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>State Add Edit Page</h2>
        </div>
    </div><br />
      <div class="row">
                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblMessage" EnableViewState="false"></asp:Label>
                </div>
      </div><br /> 
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-3">
                    Country *
                </div>
                <div class="col-md-5">
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    State Name *
                </div>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="txtStateName" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3" >
                    State Code *
                </div>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="txtStateCode" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
               
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-sm " style="background-color:black;color:white;" OnClick="btnSave_Click" />
                    &nbsp
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click" />

                </div>
            </div><br />
          
        </div>
    </div>
</asp:Content>


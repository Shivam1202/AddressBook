<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Country Add Edit Page</h2>
        </div>
    </div>
      <div class="row">
        <div class="col-md-12">
            <asp:Label runat="server" ID="lblMessage" EnableViewState="false" ></asp:Label>
            
        </div>
    </div><br />
     <div class="row">
        <div class="col-md-2">
            Country Name *
        </div>
         <div class="col-md-5">
             <asp:TextBox runat="server" ID="txtCountryName" CssClass="form-control"></asp:TextBox>
         </div>
    </div>
         <br />
    <div class="row">
        <div class="col-md-2">
            Country Code *
        </div>
         <div class="col-md-5">
             <asp:TextBox runat="server" ID="txtCountryCode" CssClass="form-control"></asp:TextBox>
         </div>
    </div><br />
    <div class="row">
        <div class="col-md-5">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-sm" style="background-color:black;color:white;" OnClick="btnSave_Click" />
            &nbsp 
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click"  />
        </div>
    </div>
  
</asp:Content>


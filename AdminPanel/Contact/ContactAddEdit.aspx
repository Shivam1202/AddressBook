<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Add Edit Page</h2>
        </div>
    </div><br />
     <div class="row">
        <div class="col-md-12">
           
            <asp:Label runat="server" ID="lblMessage" EnableViewState="false" ></asp:Label>
           
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-4">
                    Country *
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged1"></asp:DropDownList>
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-md-4">
                   State *
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlStateID"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStateID_SelectedIndexChanged"  ></asp:DropDownList>
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-md-4">
                   City *
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlCityID" CssClass="form-control"  AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <br />
          <%--   <div class="row">
                <div class="col-md-4">
                   Contact Category*
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlContactCategoryID" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>--%>
             <div class="row">
                <div class="col-md-4">
                   Contact Category *
                </div>
                <div class="col-md-8">
                   <asp:CheckBoxList runat="server" ID="cblContactCategoryID"  />
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-4">
                   Contact Name *
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                   Contact No *
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    WhatsApp No
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtWhatsAppNo" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    BirthDate
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" TextMode="DateTime"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                   Email *
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RevEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please Enter Valid Email Id" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    Age
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtAge" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                   Address *
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    Blood Group
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtBloodGroup" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    Facebook Id
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtFBID" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    Linkedin Id
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtLNDID" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
             <div class="row">
                <div class="col-md-4">
                   Upload Your Photo
                </div>
                <div class="col-md-8">
                    <asp:FileUpload runat="server" ID="fuContactPhoto" />
                    <asp:HiddenField runat="server" ID="hfContact" />
                </div>
            </div>
            <br />
            <div class="row">
                
                <div class="col-md-8">
                    <asp:Image runat="server" ID="ImgPreview" Visible="false" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    
                </div>
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-sm " style="background-color:black;color:white;" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click"  />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


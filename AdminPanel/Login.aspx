<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="AdminPanel_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css">
      <script src="https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js"></script>
      <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
    <style>
        @media (min-width: 1025px) {
        .h-custom {
            height: 100vh !important;
          }
        }
        .card-registration .select-input.form-control[readonly]:not([disabled]) {
          font-size: 1rem;
          line-height: 2.15;
          padding-left: .75em;
          padding-right: .75em;
        }
        .card-registration .select-arrow {
          top: 13px;
        }

        .gradient-custom-2 {
          /* fallback for old browsers */
          background: #a1c4fd;

          /* Chrome 10-25, Safari 5.1-6 */
          background: -webkit-linear-gradient(to right, rgba(161, 196, 253, 1), rgba(194, 233, 251, 1));

          /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
          background: linear-gradient(to right, rgba(161, 196, 253, 1), rgba(194, 233, 251, 1))
        }

        .bg-indigo {
          background-color: #4835d4;
        }
        @media (min-width: 992px) {
          .card-registration-2 .bg-indigo {
            border-top-right-radius: 15px;
            border-bottom-right-radius: 15px;
          }
        }
        @media (max-width: 991px) {
          .card-registration-2 .bg-indigo {
            border-bottom-left-radius: 15px;
            border-bottom-right-radius: 15px;
          }
        }
    </style>
     
</head>
<body>
    <form id="form1" runat="server" class="text-center" >
         
      <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
          <div class="col-12">
            <div class="card card-registration card-registration-2" style="border-radius: 15px;">
              <div class="card-body p-0">
                <div class="row g-0">
                  <div class="col-lg-6">
                    <div class="p-5">
                      <h3 class="fw-normal mb-5" style="color: #4835d4;">Login</h3>

                      <div class="mb-4 pb-2">
                        <asp:Label ID="lblMessage" runat="server" EnableViewState="true"></asp:Label>
                      </div>

                      <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline ">
                               <asp:Label runat="server" Text="UserName" ID="Lbluname"></asp:Label>
                          </div>
                        </div>
                        <div class="col-md-8 mb-4 pb-2">
                          <div class="form-outline">
                            <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control form-control-lg"></asp:TextBox>
                         </div>
                        </div>
                   
                      </div>
                    <div class="row">
                         <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline">
                            <asp:Label runat="server" Text="Password" ID="Lblpass"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline">
                         <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>
                          </div>

                        </div>
                       </div>

                      <div class="row">
                        <div class="col-md-6 mb-4 pb-2">

                          <div class="form-outline">
                      
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-dark" OnClick="btnLogin_Click"  />  
                      
                          </div>

                        </div>
                  
                      </div>

                    </div>
                  </div>
                  <div class="col-lg-6 bg-indigo text-white">
                    <div class="p-5">
                      <h3 class="fw-normal mb-5">Register</h3>

                      <div class="mb-4 pb-2">
                        <div class="form-outline form-white">
                          <asp:Label runat="server" ID="lblMsg" EnableViewState="false"></asp:Label>
                        </div>
                      </div>


                      <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:Label runat="server" ID="LblUserNamw" Text="UserName"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:TextBox runat="server" ID="txtInsertUserName" CssClass="form-control"></asp:TextBox>
                          </div>

                        </div>
                      </div>
                      <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:Label runat="server" ID="LblPassword" Text="Password"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline form-white">
                                 <asp:TextBox runat="server" ID="txtInsertPassword" CssClass="form-control"></asp:TextBox>
                          </div>

                        </div>
                      </div>
                      <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:Label runat="server" ID="Lbldisp" Text="Display Name"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline form-white">
                                 <asp:TextBox runat="server" ID="txtInsertDisplayName" CssClass="form-control"></asp:TextBox>
                          </div>

                        </div>
                      </div>
                       <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:Label runat="server" ID="LblMobileNo" Text="Mobile No"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:TextBox runat="server" ID="txtInsertMobileNo" CssClass="form-control"></asp:TextBox>
                          </div>

                        </div>
                      </div>
                        <div class="row">
                        <div class="col-md-4 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:Label runat="server" ID="LblEmail" Text="Email Id"></asp:Label>
                          </div>

                        </div>
                        <div class="col-md-8 mb-4 pb-2">

                          <div class="form-outline form-white">
                                <asp:TextBox runat="server" ID="txtInsertEmail" CssClass="form-control"></asp:TextBox>
                          </div>

                        </div>
                      </div>

                    <div class="row">
                        <div class="col-md-6 mb-4 pb-2">

                          <div class="form-outline">
                      
                              <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-dark" OnClick="btnRegister_Click"   />  
                      
                          </div>

                        </div>
                  
                      </div>
                 
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

    
        
  
   </form>   
  
</body>
</html>

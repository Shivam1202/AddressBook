using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Local Variable 
        
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;

        #endregion

        #region Server Side Validation
        String strErrorMessage = "";

        if (txtUserName.Text.Trim() == String.Empty)
        {
            strErrorMessage += "- Enter UserName -";
        }

        if (txtPassword.Text.Trim() == String.Empty)
        {
            strErrorMessage += "- Enter Password -";
        }

        if (strErrorMessage != "")
        {
            lblMessage.Text = "Kindly solve following Error(s) <br />" + strErrorMessage;
            return;
        }
        #endregion

        #region Assign the Value

        if (txtUserName.Text.Trim() != "")
        {
            strUserName = txtUserName.Text.Trim();
        }
        if (txtPassword.Text.Trim() != "")
        {
            strPassword = txtPassword.Text.Trim();
        }

        #endregion Assign the Value

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open){
                 objConn.Open();
            }
               

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[PR_User_SelectByUserNamePassword]";

            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if(objSDR.HasRows){
                lblMessage.Text = "Valid User";
                while (objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    } 
                    break;
                }
                Response.Redirect("~/AdminPanel/Default.aspx", true); 
            }
            else{
               lblMessage.Text = "Either UserName or Password is not Valid , Try Again"; 
            }
        }
         catch (Exception Ex)
        {
            lblMessage.Text = Ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {

        #region Local Variable
        SqlString strInsertUserName = SqlString.Null;
        SqlString strInsertPassword = SqlString.Null;
        SqlString strInsertDisplayName = SqlString.Null;
        SqlString strInsertEmail = SqlString.Null;
        SqlString strInsertMobileNo = SqlString.Null;
        String SetErrorMessage = "";
        #endregion Local Variable

        #region Server Side Validation
        if (txtInsertUserName.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter User Name";
        }
        else if (txtInsertPassword.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter Password";
        }
        else if (txtInsertDisplayName.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter Display Name";
        }
        else if (txtInsertEmail.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter Display Name";
        }
        if (SetErrorMessage != "")
        {
            lblMessage.Text = SetErrorMessage;
            return;
        }
        #endregion Server Side Validation

        
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_Insert";

            strInsertUserName = txtInsertUserName.Text.Trim();
            strInsertPassword = txtInsertPassword.Text.Trim();
            strInsertDisplayName = txtInsertDisplayName.Text.Trim();
            strInsertMobileNo = txtInsertMobileNo.Text.Trim();
            strInsertEmail = txtInsertEmail.Text.Trim();
          
            objCmd.Parameters.AddWithValue("@UserName", strInsertUserName);
            objCmd.Parameters.AddWithValue("@Password", strInsertPassword);
            objCmd.Parameters.AddWithValue("@DisplayName", strInsertDisplayName);
            objCmd.Parameters.AddWithValue("@MobileNo", strInsertMobileNo);
            objCmd.Parameters.AddWithValue("@Email", strInsertEmail);
            #endregion Set Connection & Command Object

                #region Insert Record

                //Add Mode
                objCmd.CommandText = "PR_User_Insert";
                lblMsg.Text = "Data Inserted Successfully";
                objCmd.ExecuteNonQuery();
                txtInsertUserName.Text = "";
                txtInsertPassword.Text = "";
                txtInsertDisplayName.Text = "";
                txtInsertMobileNo.Text = "";
                txtInsertEmail.Text = "";
                txtUserName.Focus();

                #endregion Insert Record

           

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
  


    }


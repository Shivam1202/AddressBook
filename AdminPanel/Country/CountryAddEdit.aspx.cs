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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CountryID"] != null)
            {
                //lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = "Edit Country > CountryID = " + Request.QueryString["CountryID"].ToString();
                fillControl(Convert.ToInt32(Request.QueryString["CountryID"]));
            }
            else
            {
               // lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = "Add New Country";
            }
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString strCountryName = SqlString.Null;
        SqlInt32 strCountryCode = SqlInt32.Null;
        String SetErrorMessage = "";
        #endregion Local Variable


        #region Server Side Validation
        if (txtCountryName.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter Country Name";
        }else if (txtCountryCode.Text.Trim() == "")
        {
            SetErrorMessage += "Please Enter Country Code";
        }
        if (SetErrorMessage != "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            objCmd.CommandText = "PR_Country_Insert";

            strCountryName = txtCountryName.Text.Trim();
            strCountryCode = Int32.Parse(txtCountryCode.Text.Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);
            #endregion Set Connection & Command Object

           
           
            if (Request.QueryString["CountryID"] != null)
            {
                #region Update Record
                //Edit Mode

                objCmd.Parameters.AddWithValue("@CountryID", Request.QueryString["CountryID"].ToString().Trim());
                objCmd.CommandText = "[PR_Country_UpdateByPK]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/CountryList.aspx", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record

                //Add Mode
                objCmd.CommandText = "PR_Country_Insert";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Successfully";
                objCmd.ExecuteNonQuery();
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();

                #endregion Insert Record

            }

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception Ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = Ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Country/CountryList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill Control
    private void fillControl(SqlInt32 CountryID)
    {
        #region Local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local variable

        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            
          
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectByPK";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            #endregion Set Connection & Command Object

            #region Read the value and  set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if (objSDR["CountryName"].Equals(DBNull.Value) != true)
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the CountryId = " + CountryID.ToString();
            }
            #endregion Read the value and  set the controls
        }


        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill Control

}
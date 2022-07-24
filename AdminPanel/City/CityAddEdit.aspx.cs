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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillDropDownList();

            if (Request.QueryString["CityID"] != null)
            {
                lblMessage.Text = "Edit Mode > CityID = " + Request.QueryString["CityID"].ToString();
                fillControl(Convert.ToInt32(Request.QueryString["CityID"]));
            }
            else
            {

                lblMessage.Text = "Add New City";
            }
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlInt32 strStateId = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
        #endregion Local variable

        try
        {
            #region Server side validation
            string strErrorMessage = "";

            if (txtCityName.Text.Trim() == "" && txtSTDCode.Text.Trim() == "" && txtPinCode.Text.Trim()=="" && ddlStateID.SelectedIndex == 0)
            {
                strErrorMessage += "- Enter State and City Name and Std/Pin Code <br />";
            }
            else if (txtCityName.Text.Trim() == "" && txtSTDCode.Text.Trim() == "" && txtPinCode.Text.Trim() == "")
            {
                strErrorMessage += "- Enter City Name and STD/Pin Code <br />";
            }
            else if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select State <br />";
            }
            else if (txtCityName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter City Name <br />";
            }
           
            if (strErrorMessage != "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server side validation

            #region Gather Information
            if (ddlStateID.SelectedIndex > 0)
            {
                strStateId = Convert.ToInt32(ddlStateID.SelectedValue);
            }
            if (txtCityName.Text.Trim() != "")
            {
                strCityName = txtCityName.Text.Trim();
            }
            if (txtSTDCode.Text.Trim() != "")
            {
                strSTDCode = txtSTDCode.Text.Trim();
            }
            if (txtPinCode.Text.Trim() != "")
            {
                strPinCode = txtPinCode.Text.Trim();
            }
            #endregion Gather Information

            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)            
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            objCmd.Parameters.AddWithValue("@StateID", strStateId);
            objCmd.Parameters.AddWithValue("@CityName", strCityName);
            objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);

            #endregion Set Connection & Command Object

            if (Request.QueryString["CityID"] != null)
            {
                #region Update Record
                //Edit Mode
                objCmd.Parameters.AddWithValue("@CityID", Request.QueryString["CityID"].ToString().Trim());
                objCmd.CommandText = "PR_City_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/CityList.aspx", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //Add Mode
                objCmd.CommandText = "PR_City_Insert";
                objCmd.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Successfully";
                ddlStateID.SelectedIndex = 0;
                txtSTDCode.Text = "";
                txtPinCode.Text = "";
                txtCityName.Text = "";
                ddlStateID.Focus();
                #endregion Insert Record
            }

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = ex.Message;
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
        Response.Redirect("~/AdminPanel/City/CityList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill DropDownList
    private void fillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListState(ddlStateID, Convert.ToInt32(Session["UserID"]));
      
    }
    #endregion Fill DropDownList

    #region Fill Control
    private void fillControl(SqlInt32 CityID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByPK";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
            #endregion Set Connection & Command Object

            #region Read the value and  set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if (objSDR["CityName"].Equals(DBNull.Value) != true)
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    if (objSDR["StateID"].Equals(DBNull.Value) != true)
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the CityID = " + CityID.ToString();
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
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

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            FillDropDownList();
            if (Request.QueryString["StateID"] != null)
            {
                lblMessage.Visible = false;
                fillControl(Convert.ToInt32(Request.QueryString["StateID"]));
            }
            else
            {
                
                lblMessage.Text = "Add New State";
            }
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlInt32 strCountryId = SqlInt32.Null;
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
        #endregion Local Variable

        try
        {
            #region Server Side Validation
            //Server Side
            string strErrorMessage = "";

            if (txtStateName.Text.Trim() == "" && txtStateCode.Text.Trim() == "" && ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Enter Country and State Name/Code <br />";
            }
            else if (txtStateName.Text.Trim() == "" && txtStateCode.Text.Trim() == "")
            {
                strErrorMessage += "- Enter State Name/Code <br />";
            }
            else if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select Country <br />";
            }
            else if (txtStateName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter State Name <br />";
            }
            else if (txtStateCode.Text.Trim() == "")
            {
                strErrorMessage += "- Enter State Code <br />";
            }

            if (strErrorMessage != "")
            {
              lblMessage.ForeColor = System.Drawing.Color.Red;
              lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information

            if (ddlCountryID.SelectedIndex > 0)
            {
                strCountryId = Convert.ToInt32(ddlCountryID.SelectedValue);
            }
            if (txtStateName.Text.Trim() != "")
            {
                strStateName = txtStateName.Text.Trim();
            }
            if (txtStateCode.Text.Trim() != "")
            {
                strStateCode = txtStateCode.Text.Trim();
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
            objCmd.Parameters.AddWithValue("@CountryID", strCountryId);
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);

            #endregion Set Connection & Command Object


            if (Request.QueryString["StateID"] != null)
            {
                #region Update Record
                //Edit Mode
                objCmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                objCmd.CommandText = "PR_State_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/StateList.aspx",true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //Add Mode
                objCmd.CommandText = "PR_State_Insert";
                objCmd.ExecuteNonQuery();
                ddlCountryID.SelectedIndex = 0;
                txtStateCode.Text = "";
                txtStateName.Text = "";
                ddlCountryID.Focus();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Successfully";
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
        Response.Redirect("~/AdminPanel/State/StateList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill DropDownList

     private void FillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Convert.ToInt32(Session["UserID"]));

    }

    //private void fillDropDownList()
    //{
    //    #region Local Variable
    //    SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
    //    #endregion Local Variable

    //    try
    //    {
    //        #region Set Connection & Command Object

    //        if (objConn.State != ConnectionState.Open)
    //        {
    //            objConn.Open();
    //        }


    //        SqlCommand objCmd = objConn.CreateCommand();
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.CommandText = "PR_Country_SelectForDropDownList";
    //        if (Session["UserID"] != null)
    //        {
    //            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

    //        }
    //        SqlDataReader objSDR = objCmd.ExecuteReader();

    //        #endregion Set Connection & Command Object

    //        if (objSDR.HasRows == true)
    //        {
    //            ddlCountryID.DataSource = objSDR;
    //            ddlCountryID.DataValueField = "CountryID";
    //            ddlCountryID.DataTextField = "CountryName";
    //            ddlCountryID.DataBind();
    //        }

    //        ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));

    //        objConn.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //    finally
    //    {
    //        if (objConn.State == ConnectionState.Open)
    //        {
    //            objConn.Close();
    //        }
    //    }
    //}
    #endregion Fill DropDownList

    #region Fill Control
    private void fillControl(SqlInt32 StateID)
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
            objCmd.CommandText = "PR_State_SelectByPK";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());

            #endregion Set Connection & Command Object

            #region Read the value and  set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if(objSDR["StateName"].Equals(DBNull.Value)!=true)
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }
                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
               lblMessage.Text = "No data available for the StateId = " + StateID.ToString();
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
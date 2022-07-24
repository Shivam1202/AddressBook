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

public partial class AdminPanel_Country_CountryList : System.Web.UI.Page
{

    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {

            fillGridView();
        }

    }
    #endregion Load Event

    #region Fill GridView
    private void fillGridView()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
        objConn.Open();

        SqlCommand objCmd = new SqlCommand();
        objCmd.Connection = objConn;
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.CommandText = "[PR_Country_SelectByUserID]";
        if (Session["UserID"] != null)
        {
            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

        }
        
        SqlDataReader objSDR = objCmd.ExecuteReader();
        gvCountry.DataSource = objSDR;
        gvCountry.DataBind();

        objConn.Close();

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objConn.Close();
        }
    }
    #endregion Fill GridView

    #region gvCountry : RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteRecord(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete Record
    }
    #endregion gvCountry : RowCommand

    #region Delete Country Record
    private void DeleteRecord(SqlInt32 CountryID)
    {
        SqlConnection ObjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());

        try
        {
            ObjCon.Open();

            SqlCommand ObjCmd = ObjCon.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Country_DeleteByPK";
            if (Session["UserID"] != null)
            {
                ObjCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            ObjCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString());
            ObjCmd.ExecuteNonQuery();

            ObjCon.Close();

            fillGridView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            ObjCon.Close();
        }
    }
    #endregion Delete Country Record

    #region btnAdd : Redirect to AddEdit Page
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Country/CountryAddEdit.aspx");
    }
    #endregion btnAdd : Redirect to AddEdit Page
}
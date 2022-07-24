using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Content_Contact : System.Web.UI.Page
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
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_SelectByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvContact.DataSource = objSDR;
            gvContact.DataBind();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
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
    #endregion Fill FridView

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] {','});
                string Choose = commandArgs[0];
                string CID = commandArgs[1];
                DeleteRecord(Convert.ToInt32(CID), Choose);
            }
        }
        #endregion Delete Record

    }
    #endregion gvContact : RowCommand

    #region Delete Contact Record
    private void DeleteRecord(SqlInt32 ContactID, String ChooseFile)
    {
        
        SqlConnection ObjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());

        try
        {
            if (ObjCon.State != ConnectionState.Open)
                ObjCon.Open();

            
            String PhotoPath = ChooseFile;

            FileInfo file = new FileInfo(Server.MapPath(PhotoPath));

            if (file.Exists)
            {
                file.Delete();
            }

            //Delete ContactWiseContactCategory Records
            SqlCommand objCmdContactCategory = ObjCon.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_DeleteByPK";
            objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID.ToString());
            objCmdContactCategory.ExecuteNonQuery();

            
            SqlCommand ObjCmd = ObjCon.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Contact_DeleteByPK";
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
            if (Session["UserID"] != null)
            {
                ObjCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }

            if (ObjCon.State == ConnectionState.Open)
                ObjCon.Close();

            fillGridView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (ObjCon.State == ConnectionState.Open)
                ObjCon.Close();
        }
    }
    #endregion Delete Contact Record

    #region btnAdd : Redirect to AddEdit Page
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Contact/ContactAddEdit.aspx");
    }
    #endregion btnAdd : Redirect to AddEdit Page

}
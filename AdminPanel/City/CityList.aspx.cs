﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityList : System.Web.UI.Page
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
            objCmd.CommandText = "PR_City_SelectByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }

            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvCity.DataSource = objSDR;
            gvCity.DataBind();

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
    #endregion Fill GridView

    #region gvCity : RowCommand
    protected void gvCity_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteCity(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete Record

    }
    #endregion gvCity : RowCommand

    #region Delete City Record
    private void DeleteCity(SqlInt32 CityID)
    {
        SqlConnection ObjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());

        try
        {
            if (ObjCon.State != ConnectionState.Open)
                ObjCon.Open();

            SqlCommand ObjCmd = ObjCon.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_City_DeleteByPK";
            if (Session["UserID"] != null)
            {
                ObjCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            }
            ObjCmd.Parameters.AddWithValue("@CityID", CityID.ToString());
            ObjCmd.ExecuteNonQuery();

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
    #endregion Delete City Record

    #region Button : Add

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/City/CityAddEdit.aspx");
    }
    #endregion Button : Add

}
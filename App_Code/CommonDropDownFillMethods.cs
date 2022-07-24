using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
public static class CommonDropDownFillMethods
{
    public static void FillDropDownListCountry(DropDownList  ddl,SqlInt32 UserID ){
       
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        #region Set Connection & Command Object
        if (objConn.State != ConnectionState.Open)
            objConn.Open();

        SqlCommand objComd = objConn.CreateCommand();
        objComd.CommandType = CommandType.StoredProcedure;
        objComd.CommandText = "PR_Country_SelectForDropDownList";
        objComd.Parameters.AddWithValue("@UserID",UserID);
        

        #endregion Set Connection & Command Object


        SqlDataReader objSDR = objComd.ExecuteReader();

        if (objSDR.HasRows == true)
        {
            ddl.DataSource = objSDR;
            ddl.DataValueField = "CountryID";
            ddl.DataTextField = "CountryName";
            ddl.DataBind();
        }

        ddl.Items.Insert(0, new ListItem("Select Country", "-1"));

        if (objConn.State == ConnectionState.Open)
            objConn.Close();
    }
   
    public static void FillDropDownListState(DropDownList ddl,SqlInt32 UserID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objComd = objConn.CreateCommand();
            objComd.CommandType = CommandType.StoredProcedure;
            objComd.CommandText = "PR_State_SelectForDropDownList";
            objComd.Parameters.AddWithValue("@UserID", UserID);
        

            SqlDataReader objSDR = objComd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "StateName";
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Select State", "-1"));

            if (objConn.State != ConnectionState.Open)
                objConn.Close();
        }
        
    

    public static void FillDropDownListStateByCountryID(DropDownList ddl, SqlInt32 CountryID,SqlInt32 UserID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlInt32 strCountryID = SqlInt32.Null;
        #endregion Local Variable

        if (ddl.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddl.SelectedValue);
        }

        #region Set Connection & Command Object

        if (objConn.State != ConnectionState.Open)
            objConn.Open();


        SqlCommand objComd = objConn.CreateCommand();
        objComd.CommandType = CommandType.StoredProcedure;
        objComd.CommandText = "PR_State_SelectForDropDownListByCountryID";
        objComd.Parameters.AddWithValue("@CountryID", CountryID);
        #endregion Set Connection & Command Object

        SqlDataReader objSDR = objComd.ExecuteReader();

        if (objSDR.HasRows == true)
        {
            ddl.DataSource = objSDR;
            ddl.DataValueField = "StateID";
            ddl.DataTextField = "StateName";
            ddl.DataBind();
        }

        ddl.Items.Insert(0, new ListItem("Select State", "-1"));

        if (objConn.State == ConnectionState.Open)
            objConn.Close();
    }

    public static void FillDropDownListCity(DropDownList ddl)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlInt32 strStateID = SqlInt32.Null;
        #endregion Local Variable

        if (ddl.SelectedIndex > 0)
        {
            strStateID = Convert.ToInt32(ddl.SelectedValue);
        }


        #region Set Connection & Command Object
        if (objConn.State != ConnectionState.Open)
            objConn.Open();

        SqlCommand objComd = objConn.CreateCommand();
        objComd.CommandType = CommandType.StoredProcedure;
        objComd.CommandText = "PR_City_SelectForDropDownList";
        objComd.Parameters.AddWithValue("@StateID", strStateID);

        #endregion Set Connection & Command Object

        SqlDataReader objSDR = objComd.ExecuteReader();

        if (objSDR.HasRows == true)
        {
            ddl.DataSource = objSDR;
            ddl.DataValueField = "CityID";
            ddl.DataTextField = "CityName";
            ddl.DataBind();
        }

        ddl.Items.Insert(0, new ListItem("Select City", "-1"));

        if (objConn.State == ConnectionState.Open)
            objConn.Close();
    }

    public static void FillDropDownListCityBySateID(DropDownList ddl , SqlInt32 StateID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlInt32 strStateID = SqlInt32.Null;
        #endregion Local Variable

        if (ddl.SelectedIndex > 0)
        {
            strStateID = Convert.ToInt32(ddl.SelectedValue);
        }


        #region Set Connection & Command Object
        if (objConn.State != ConnectionState.Open)
            objConn.Open();

        SqlCommand objComd = objConn.CreateCommand();
        objComd.CommandType = CommandType.StoredProcedure;
        objComd.CommandText = "PR_City_SelectForDropDownListByStateID";
        objComd.Parameters.AddWithValue("@StateID", StateID);

        #endregion Set Connection & Command Object

        SqlDataReader objSDR = objComd.ExecuteReader();

        if (objSDR.HasRows == true)
        {
            ddl.DataSource = objSDR;
            ddl.DataValueField = "CityID";
            ddl.DataTextField = "CityName";
            ddl.DataBind();
        }

        ddl.Items.Insert(0, new ListItem("Select City", "-1"));

        if (objConn.State == ConnectionState.Open)
            objConn.Close();
    }
   
	
}
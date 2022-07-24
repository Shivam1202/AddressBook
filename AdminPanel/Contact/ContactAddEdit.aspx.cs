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

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

            fillCountryDropDownList();
            fillContactCategoryID();
           
           // fillContactCategoryDropDownList();

            if (Request.QueryString["ContactID"] != null)
            {
                lblMessage.Text = "Edit Contact > ContactID = " + Request.QueryString["ContactID"].ToString();
                fillControl(Convert.ToInt32(Request.QueryString["ContactID"]));
                FillControlCBLContactCategory(Convert.ToInt32(Request.QueryString["ContactID"]));
            }
            else
            {
                lblMessage.Text = "Add New Contact";
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
        SqlInt32 strStateId = SqlInt32.Null;
        SqlInt32 strCityId = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;
        SqlString strWhatsAppNo = SqlString.Null;
        SqlDateTime strBirthDate = SqlDateTime.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strAge = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFBId = SqlString.Null;
        SqlString strLNId = SqlString.Null;

        String ContactPhotoPath = "~/UserContent/";
        String AbsulatePath = Server.MapPath(ContactPhotoPath);
        String Path = "";
      
        #endregion Local Variable

        try
        {
            #region Server Side Validation

            string strErrorMessage = "";

            if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select Country <br />";
            }
            if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select State <br />";
            }
            if (ddlCityID.SelectedIndex == 0)
            {
                strErrorMessage += "- Select City <br />";
            }
            //if (ddlContactCategoryID.SelectedIndex == 0)
            //{
            //    strErrorMessage += "- Select Contact Category <br />";
            //}
            if (txtContactName.Text == "")
            {
                strErrorMessage += "- Please Enter Contact Name <br />";
            }
            if (txtContactNo.Text == "")
            {
                strErrorMessage += "- Please Enter Contact No <br />";
            }
            if (txtAddress.Text == "")
            {
                strErrorMessage += "- Please Enter Address  <br />";
            }
            if (txtEmail.Text == "" )
            {
                strErrorMessage += "- Please Enter Email  <br />";
            }
            //if (!fuContactPhoto.HasFile)
            //{
            //    strErrorMessage += " - Please Select File <br />";
            //}
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
            if (ddlStateID.SelectedIndex > 0){ 
                strStateId = Convert.ToInt32(ddlStateID.SelectedValue);
            }
            if (ddlCityID.SelectedIndex > 0)
            {
                strCityId = Convert.ToInt32(ddlCityID.SelectedValue);
            }
            //if (ddlContactCategoryID.SelectedIndex > 0)
            //{
            //    strContactCategoryId = Convert.ToInt32(ddlContactCategoryID.SelectedValue);
            //}
            //if (txtContactName.Text.Trim() != "") 
            { 
                strContactName = txtContactName.Text.Trim(); 
            }
            if (txtContactNo.Text.Trim() != ""){
                strContactNo = txtContactNo.Text.Trim();
            }
            if (txtWhatsAppNo.Text.Trim() != "") {
                strWhatsAppNo = txtWhatsAppNo.Text.Trim();
            }
            if (txtBirthDate.Text.Trim() != "")
            {
                strBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim());
            }
            if (txtEmail.Text.Trim() != "") {
                strEmail = txtEmail.Text.Trim();
            }
            if (txtAge.Text.Trim() != "") {
                strAge = txtAge.Text.Trim();
            }
            if (txtAddress.Text.Trim() != "") {
                strAddress = txtAddress.Text.Trim();
            }
            if (txtBloodGroup.Text.Trim() != "")
            {
                strBloodGroup = txtBloodGroup.Text.Trim();
            }
            if (txtFBID.Text.Trim() != "") {

                strFBId = txtFBID.Text.Trim();
            }
            if (txtLNDID.Text.Trim() != "")
            {
                strLNId = txtLNDID.Text.Trim();
            }
            if (fuContactPhoto.HasFile)
            {
                AbsulatePath = Server.MapPath(ContactPhotoPath);
            }
           
            #endregion Gather Information

            //#region File Upload
            //if (fuContactPhoto.HasFile)
            //{
            //    ContactPhotoPath = "~/UserContent/" + fuContactPhoto.FileName.ToString().Trim();

            //    fuContactPhoto.SaveAs(Server.MapPath(ContactPhotoPath));
            //}
            //#endregion File Upload

            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            
            objCmd.Parameters.AddWithValue("@CountryID", strCountryId);
            objCmd.Parameters.AddWithValue("@StateID", strStateId);
            objCmd.Parameters.AddWithValue("@CityID", strCityId);
            //objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryId);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FaceBookId", strFBId);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLNId);
            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
           

            #endregion Set Connection & Command Object

            if (Request.QueryString["ContactID"] != null)
            {
                
                //Edit Mode
                #region Update Record
               

                if (fuContactPhoto.HasFile)
                                {
                                    if (!Directory.Exists(AbsulatePath))
                                   {
                                        Directory.CreateDirectory(AbsulatePath);
                                    }
                                    //try
                                    //{
                                        FileInfo fileinfoDelete = new FileInfo(Server.MapPath(hfContact.Value));
                                        fileinfoDelete.Delete();
                                    //}
                                    //catch (Exception ex) { 
                                    //}

                                  fuContactPhoto.SaveAs(Server.MapPath(ContactPhotoPath + fuContactPhoto.FileName.Trim()));
                                    objCmd.Parameters.AddWithValue("@PhotoPath", ContactPhotoPath + fuContactPhoto.FileName.Trim());
                                }
                else
                {
                    Path = hfContact.Value;
                    objCmd.Parameters.AddWithValue("@PhotoPath", Path);
                }

                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
            


                objCmd.ExecuteNonQuery();
                //Delete ContactWiseContactCategory Records
                SqlCommand objCmdContactCategory = objConn.CreateCommand();
                objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_DeleteByPK";
                objCmdContactCategory.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmdContactCategory.ExecuteNonQuery();

                foreach (ListItem liContactCategory in cblContactCategoryID.Items)
                {
                    if (liContactCategory.Selected)
                    {
                        SqlCommand objCmdContactCategoryInsert = objConn.CreateCommand();
                        objCmdContactCategoryInsert.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategoryInsert.CommandText = "PR_ContactWiseContactCategory_Insert";
                        objCmdContactCategoryInsert.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                        objCmdContactCategoryInsert.Parameters.AddWithValue("@ContactCategoryID", liContactCategory.Value.ToString());
                        objCmdContactCategoryInsert.ExecuteNonQuery();
                    }
                }

                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx", true);
                // ddlCountryID.Focus();
                #endregion Update Record

               
            }
            else
            {
                #region Insert Record
                //Add Mode
                if (fuContactPhoto.HasFile)
                {
                    if (!Directory.Exists(AbsulatePath))
                    {
                        Directory.CreateDirectory(AbsulatePath);
                    }
                    fuContactPhoto.SaveAs(AbsulatePath + fuContactPhoto.FileName.Trim());
                }

                objCmd.Parameters.AddWithValue("@PhotoPath", ContactPhotoPath + fuContactPhoto.FileName.ToString().Trim());

                objCmd.CommandText = "PR_Contact_Insert";
             
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
               
                objCmd.ExecuteNonQuery();

                SqlInt32 ContactID = 0;
                ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);
                     
                foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
                {
                    if (liContactCategoryID.Selected)
                    {
                        SqlCommand objComdccid = objConn.CreateCommand();
                        objComdccid.CommandType = CommandType.StoredProcedure;
                        objComdccid.CommandText = "[PR_ContactWiseContactCategory_Insert]";
                        objComdccid.Parameters.AddWithValue("ContactID", ContactID.ToString());
                        objComdccid.Parameters.AddWithValue("ContactCategoryId", liContactCategoryID.Value.ToString());
                        objComdccid.ExecuteNonQuery();

                    }
                }
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Successfully";
                ddlCountryID.SelectedIndex = 0;
                ddlStateID.SelectedIndex = 0;
                ddlCityID.SelectedIndex = 0;
           //     ddlContactCategoryID.SelectedIndex = 0;
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtContactName.Text = "";
                txtContactNo.Text = "";
                txtBirthDate.Text = "";
                txtWhatsAppNo.Text = "";
                txtBloodGroup.Text = "";
                txtFBID.Text = "";
                txtLNDID.Text = "";
                txtAge.Text = "";
                ddlCountryID.Focus();
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

    private void fillCountryDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Convert.ToInt32(Session["UserID"]));
       
        
    }
    private void fillStateDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListStateByCountryID(ddlStateID,Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(Session["UserID"]));

    }
    private void fillCityDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCityBySateID(ddlCityID , Convert.ToInt32(ddlStateID.SelectedValue));

    }
  
    #region FillContactCategoryID 
    private void fillContactCategoryID()
    {
       SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
       try { 
       if (objConn.State != ConnectionState.Open)
       {
           objConn.Open();
       }

       SqlCommand objComd = objConn.CreateCommand();
       objComd.CommandType = CommandType.StoredProcedure;
       objComd.CommandText = "[PR_ContactCategory_SelectForCheckBoxList]";

       SqlDataReader objSDR = objComd.ExecuteReader();

        if (objSDR.HasRows == true)
        {
            cblContactCategoryID.DataSource = objSDR;
            cblContactCategoryID.DataValueField = "ContactCategoryID";
            cblContactCategoryID.DataTextField = "ContactCategoryName";
            cblContactCategoryID.DataBind();
        }
        if (objConn.State != ConnectionState.Closed)
        {
            objConn.Close();
        }
       }
       catch (Exception ex)
       {
           lblMessage.Text = ex.Message;
       }

    }
    #endregion FillContactCategoryID

    #region Fill Control
    private void fillControl(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_Contact_SelectByPK";
         
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            #endregion Set Connection & Command Object

            #region Read the value and  set the controls
          
                
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                  
                    if (objSDR["ContactName"].Equals(DBNull.Value) != true)
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                    {
                        txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthDate.Text = objSDR["BirthDate"].ToString().Trim();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["FaceBookID"].Equals(DBNull.Value))
                    {
                        txtFBID.Text = objSDR["FaceBookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                    {
                        txtLNDID.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        fillStateDropDownList();
                    }
                    if (objSDR["StateID"].Equals(DBNull.Value) != true)
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                        fillCityDropDownList();
                    }
                    if (objSDR["CityID"].Equals(DBNull.Value) != true)
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    //if (objSDR["ContactCategoryID"].Equals(DBNull.Value) != true)
                    //{
                    //    ddlContactCategoryID.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    //}
                    if (objSDR["PhotoPath"].Equals(DBNull.Value) != true)
                    {
                        hfContact.Value  = objSDR["PhotoPath"].ToString().Trim();
                    }
                    ImgPreview.Visible = true;
                    ImgPreview.ImageUrl = hfContact.Value.ToString();
                    
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the ContactID = " + ContactID.ToString();
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

    #region btnCancel : Redirect to List page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Contact/ContactList.aspx",true);
    }
    #endregion btnCancel : Redirect to List page

    #region IndexChangedDropDownList
    protected void ddlCountryID_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillStateDropDownList();
        ddlCityID.ClearSelection();
    }
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCityDropDownList();
    }
    #endregion IndexChangedDropDownList

    #region FillCblContactCategory
    private void FillControlCBLContactCategory(SqlInt32 ContactID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmdContactCategory = objConn.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_SelectByContactID";
            objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();
            #endregion Set Connection & Command Object

            while (objSDRContactCategory.Read())
            {
                foreach (ListItem li in cblContactCategoryID.Items)
                {
                    if (li.Value == objSDRContactCategory["ContactCategoryID"].ToString().Trim())
                    {
                        li.Selected = true;
                    }
                }
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
    #endregion FillCblContactCategory



}

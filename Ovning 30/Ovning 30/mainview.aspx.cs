using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ovning_30
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        const string CON_STR = "Data Source=ACADEMY028-vm;Initial Catalog=Contacts;Integrated Security=SSPI";

        protected void Page_Load(object sender, EventArgs e)
        {
            //AddAdress();
        }

        //private void AddAdress()
        //{
        //    int selID = int.Parse(lboxContacts.SelectedValue);
            
        //    selID;
        //    SqlConnection myConnection = new SqlConnection();
        //    myConnection.ConnectionString = WebForm2.CON_STR;

        //    SqlCommand myCommand = new SqlCommand();
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        myConnection.Open();
        //        SqlParameter paramFirstname = new SqlParameter("@Type", SqlDbType.VarChar);
        //        paramFirstname.Value = txtType.Text;
        //        myCommand.Parameters.Add(paramFirstname);

        //        SqlParameter paramLastname = new SqlParameter("@Street", SqlDbType.VarChar);
        //        paramLastname.Value = txtStreet.Text;
        //        myCommand.Parameters.Add(paramLastname);

        //        SqlParameter paramSSN = new SqlParameter("@City", SqlDbType.VarChar);
        //        paramSSN.Value = txtCity.Text;
        //        myCommand.Parameters.Add(paramSSN);

        //        SqlParameter paramCID = new SqlParameter("@new_CID", SqlDbType.Int);
        //        paramCID.Value = selID;
        //        myCommand.Parameters.Add(paramCID);

        //        SqlParameter paramAID = new SqlParameter("@new_AID", SqlDbType.Int);
        //        paramAID.Direction = ParameterDirection.Output;
        //        myCommand.Parameters.Add(paramAID);

        //        myCommand.CommandText = $"spAddAdress";
        //        myCommand.ExecuteNonQuery();

        //        UpdateListBox();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //}
    }
}
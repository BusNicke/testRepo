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
    public partial class WebForm1 : System.Web.UI.Page
    {
        const string CON_STR = "Data Source=ACADEMY028-vm;Initial Catalog=Contacts;Integrated Security=SSPI";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                UpdateTable();
        }
        private void UpdateTable()
        {
            int id = int.Parse(Request.QueryString["id"]);

            #region Start Table
            Literal.Text = "<div class=\"container\">";
            Literal.Text += "<h2>Adress list</h2>";
            Literal.Text += "<div class=\"table-responsive\">";
            Literal.Text += "<table class=\"table\">";
            Literal.Text += "<thead>";
            Literal.Text += "<tr>";
            Literal.Text += "<th>#</th>";
            Literal.Text += "<th>Type</th>";
            Literal.Text += "<th>Street</th>";
            Literal.Text += "<th>City</th>";
            Literal.Text += "<th></th>";
            Literal.Text += "<th></th>";
            Literal.Text += "<th></th>";
            Literal.Text += "</tr>";
            Literal.Text += "</thead>";
            #endregion
            #region Adding from SQL
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = CON_STR;

            SqlCommand showCommand = new SqlCommand();
            showCommand.Connection = myConnection;
            showCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                paramID.Value = id;
                showCommand.Parameters.Add(paramID);

                myConnection.Open();
                int count = 0;
                showCommand.CommandText = "spPrintAdresses";
                SqlDataReader myReader = showCommand.ExecuteReader();
                while (myReader.Read())
                {
                    string typeTmp = myReader["Type"].ToString();
                    string streetTmp = myReader["street"].ToString();
                    string cityTmp = myReader["city"].ToString();

                    Literal.Text += "<tbody>";
                    Literal.Text += "<tr>";
                    Literal.Text += $"<td> {++count} </td>";
                    Literal.Text += $"<td> {typeTmp} </td>";
                    Literal.Text += $"<td> {streetTmp} </td>";
                    Literal.Text += $"<td> {cityTmp} </td>";
                    Literal.Text += $"<td>";
                    //Literal.Text += $"<a href=\"#\" onclick=\"showEditModal('{typeTmp}', '{streetTmp}', '{cityTmp}', '{myReader["CID"].ToString()}', 'edit')\">Edit</a>";
                    Literal.Text += $"<td>";
                    Literal.Text += $"<a href=\"ViewContact.aspx?delete={myReader["AID"].ToString()}\">Delete</a>";
                    Literal.Text += "</tr>";
                    Literal.Text += "</tbody>";
                }
                myReader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                myConnection.Close();
            }
            #endregion
            #region EndTags
            Literal.Text += "</table>";
            Literal.Text += "</div>";
            Literal.Text += "</div>";
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ovning_30
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public int CID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                CID = int.Parse(Request.QueryString["id"]);
            }
            
            if (!IsPostBack)
            {
                UpdateTable();
            }
                

            if (Request.QueryString["delete"] != null)
            {
                deleteAdress();
            }

            if (txtAction.Text == "add")
            {
                AddAdress();
            }
        }

        private void AddAdress()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = WebConfigurationManager.ConnectionStrings["GustavsSQL"].ToString();

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                myConnection.Open();
                SqlParameter paramType = new SqlParameter("@type", SqlDbType.VarChar);
                paramType.Value = firstName.Text;
                myCommand.Parameters.Add(paramType);

                SqlParameter paramStreet = new SqlParameter("@street", SqlDbType.VarChar);
                paramStreet.Value = lastName.Text;
                myCommand.Parameters.Add(paramStreet);

                SqlParameter paramCity = new SqlParameter("@city", SqlDbType.VarChar);
                paramCity.Value = ssn.Text;
                myCommand.Parameters.Add(paramCity);

                SqlParameter paramCID = new SqlParameter("@new_CID", SqlDbType.VarChar);
                paramCID.Value = CID;
                myCommand.Parameters.Add(paramCID);

                SqlParameter paramAID = new SqlParameter("@new_AID", SqlDbType.Int);
                paramAID.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(paramAID);

                myCommand.CommandText = $"spAddAdress";
                int rows = myCommand.ExecuteNonQuery();

                UpdateTable();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                firstName.Text = string.Empty;
                lastName.Text = string.Empty;
                ssn.Text = string.Empty;
                myConnection.Close();
            }
        }

        private void deleteAdress()
        {
            int nr = int.Parse(Request.QueryString["delete"]);
            if (nr >= 0)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = WebConfigurationManager.ConnectionStrings["GustavsSQL"].ToString();

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                try
                {
                    myConnection.Open();

                    SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                    paramID.Value = nr;
                    myCommand.Parameters.Add(paramID);

                    myCommand.CommandText = $"spDeleteAdress";
                    int rows = myCommand.ExecuteNonQuery();

                    UpdateTable();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    myConnection.Close();
                }
            }
        }

        private void UpdateTable()
        {
            

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
            myConnection.ConnectionString = WebConfigurationManager.ConnectionStrings["GustavsSQL"].ToString();

            SqlCommand showCommand = new SqlCommand();
            showCommand.Connection = myConnection;
            showCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                paramID.Value = CID;
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
                    Literal.Text += $"<a href=\"#\" onclick=\"showEditModal('{typeTmp}', '{streetTmp}', '{cityTmp}', '{myReader["AID"].ToString()}', 'edit')\">Edit</a>";
                    Literal.Text += $"<td>";
                    Literal.Text += $"<a href=\"ViewContact.aspx?id={CID}&delete={myReader["AID"].ToString()}\">Delete</a>";
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
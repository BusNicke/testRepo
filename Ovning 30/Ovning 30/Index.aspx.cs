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
    public partial class WebForm3 : System.Web.UI.Page
    {
        public static int selID;
        const string CON_STR = "Data Source=ACADEMY028-vm;Initial Catalog=Contacts;Integrated Security=SSPI";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["delete"] != null)
            {
                int nr = int.Parse(Request.QueryString["delete"]);
                DeleteContact(nr);
            }

            if (Request.QueryString["mod"] != null)
            {
                int nr = int.Parse(Request.QueryString["mod"]);
                EditContact(nr);
            }

            if (!IsPostBack)
            {
                UpdateTable();
            }

            if(txtAction.Text == "edit")
            {
                EditContact(int.Parse(txtId.Text));
            }

            if(txtAction.Text == "add")
            {
                AddContact();
            }

            if(txtAction.Text == "view")
            {
                //ViewContact();
            }

        }

       

        private void EditContact(int nr)
        {
            if (nr >= 0)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = CON_STR;

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    myConnection.Open();
                    SqlParameter paramFirstname = new SqlParameter("@firstName", SqlDbType.VarChar);
                    paramFirstname.Value = firstName.Text;
                    myCommand.Parameters.Add(paramFirstname);

                    SqlParameter paramLastname = new SqlParameter("@lastname", SqlDbType.VarChar);
                    paramLastname.Value = lastName.Text;
                    myCommand.Parameters.Add(paramLastname);

                    SqlParameter paramSSN = new SqlParameter("@ssn", SqlDbType.VarChar);
                    paramSSN.Value = ssn.Text;
                    myCommand.Parameters.Add(paramSSN);

                    SqlParameter paramID = new SqlParameter("@new_cid", SqlDbType.Int);
                    paramID.Value = nr;
                    myCommand.Parameters.Add(paramID);

                    myCommand.CommandText = $"spEditContact";
                    myCommand.ExecuteNonQuery();

                    UpdateTable();
                }
                catch (Exception)
                {
                }
                finally
                {
                    myConnection.Close();
                }
            }
        }

        private void AddContact()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = CON_STR;

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                myConnection.Open();
                SqlParameter paramFirstname = new SqlParameter("@firstName", SqlDbType.VarChar);
                paramFirstname.Value = firstName.Text;
                myCommand.Parameters.Add(paramFirstname);

                SqlParameter paramLastname = new SqlParameter("@lastname", SqlDbType.VarChar);
                paramLastname.Value = lastName.Text;
                myCommand.Parameters.Add(paramLastname);

                SqlParameter paramSSN = new SqlParameter("@ssn", SqlDbType.VarChar);
                paramSSN.Value = ssn.Text;
                myCommand.Parameters.Add(paramSSN);

                SqlParameter paramID = new SqlParameter("@new_CID", SqlDbType.Int);
                paramID.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(paramID);

                myCommand.CommandText = $"spAddContact";
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

        private void DeleteContact(int nr)
        {
            if (nr >= 0)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = CON_STR;

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                try
                {
                    myConnection.Open();

                    SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                    paramID.Value = nr;
                    myCommand.Parameters.Add(paramID);

                    myCommand.CommandText = $"spDeleteContact";
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
            Literal.Text += "<h2>Contact list</h2>";
            Literal.Text += "<div class=\"table-responsive\">";
            Literal.Text += "<table class=\"table\">";
            Literal.Text += "<thead>";
            Literal.Text += "<tr>";
            Literal.Text += "<th>#</th>";
            Literal.Text += "<th>Firstname</th>";
            Literal.Text += "<th>Lastname</th>";
            Literal.Text += "<th>Socialsecuritynumber</th>";
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
                myConnection.Open();
                showCommand.CommandText = "spPrintAllContacts";
                SqlDataReader myReader = showCommand.ExecuteReader();
                int count = 0;
                while (myReader.Read())
                {
                    string firtNameTmp = myReader["Firstname"].ToString();
                    string lastNameTmp = myReader["Lastname"].ToString();
                    string SSNTmp = myReader["SSN"].ToString();

                    Literal.Text += "<tbody>";
                    Literal.Text += "<tr>";
                    Literal.Text += $"<td> {++count} </td>";
                    Literal.Text += $"<td> {firtNameTmp} </td>";
                    Literal.Text += $"<td> {lastNameTmp} </td>";
                    Literal.Text += $"<td> {SSNTmp} </td>";
                    Literal.Text += $"<td>";
                    Literal.Text += $"<a href=\"#\" onclick=\"showEditModal('{firtNameTmp}', '{lastNameTmp}', '{SSNTmp}', '{myReader["CID"].ToString()}', 'edit')\">Edit</a>";
                    Literal.Text += $"<td>";
                    Literal.Text += $"<a href=\"index.aspx?delete={myReader["CID"].ToString()}\">Delete</a>";
                    Literal.Text += $"<td>";
                    Literal.Text += $"<a href=\"ViewContact.aspx?id={myReader["CID"].ToString()}\">View</a>";
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
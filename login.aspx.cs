using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fitnessProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string userEmail = email.Text;
            string userPassword = password.Text;

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT user_id, password, role FROM Users WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPassword = reader["password"].ToString();
                            string role = reader["role"].ToString();
                            int userId = Convert.ToInt32(reader["user_id"]);  // Get user_id from the database

                            if (BCrypt.Net.BCrypt.Verify(userPassword, hashedPassword))
                            {
                                // Set the session for the logged-in user
                                Session["UserID"] = userId;  // Store the user ID in the session
                                Session["Role"] = role;

                                if (role == "admin")
                                {
                                    Response.Redirect("AddDiet.aspx");
                                }
                                else if (role == "customer")
                                {
                                    Response.Redirect("Diet.aspx");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Unknown role.')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid password.')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Email not found.')</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
                }
            }
        }
    }
}
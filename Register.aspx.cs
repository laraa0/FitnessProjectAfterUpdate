using System;
using MySql.Data.MySqlClient;

namespace fitnessProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string userEmail = email.Text.Trim();
            string userPassword = password.Text.Trim();
            string confirmPass = confirmPassword.Text.Trim();

            if (userPassword != confirmPass)
            {
                Response.Write("<script>alert('Passwords do not match.');</script>");
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);
            string userRole = "customer";

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE email = @Email";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Email", userEmail);
                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        Response.Write("<script>alert('Email already registered.');</script>");
                        return;
                    }

                    string query = "INSERT INTO Users (email, password, role) VALUES (@Email, @Password, @Role)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", userRole);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Response.Write("<script>alert('Registration successful! Redirecting to login...'); window.location='Login.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Registration failed.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}

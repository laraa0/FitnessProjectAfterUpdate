using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fitnessProject
{
    public partial class Diet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDiets();
        }

        private void LoadDiets()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string query = "SELECT diet_id, diet_image, mealName, macronutrient, calories FROM diet";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Repeater1.DataSource = reader;
                Repeater1.DataBind();
            }
        }

        protected void RptDiets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string connectionString = "server=localhost;user id=root;password=;database=fitness_and_diet_project;";

            if (e.CommandName == "Add")
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                int dietId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserID"]);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Check if already exists
                        string checkQuery = "SELECT COUNT(*) FROM routine WHERE user_id = @userId AND diet_id = @dietId";
                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@userId", userId);
                        checkCmd.Parameters.AddWithValue("@dietId", dietId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "MealExists", @"
                        <script>
                            alert('This meal is already added to your routine!');
                        </script>", false);
                        }
                        else
                        {
                            // Insert
                            string insertQuery = "INSERT INTO routine (user_id, diet_id) VALUES (@userId, @dietId)";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@userId", userId);
                            insertCmd.Parameters.AddWithValue("@dietId", dietId);
                            insertCmd.ExecuteNonQuery();

                            successMessage.Style["display"] = "block";

                            ClientScript.RegisterStartupScript(this.GetType(), "HideSuccess", @"
                        <script>
                            setTimeout(function() {
                                document.getElementById('successMessage').style.display = 'none';
                            }, 1000);
                        </script>", false);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
                    }
                }
            }
            else if (e.CommandName == "Delete")
            {
                int dietId = Convert.ToInt32(e.CommandArgument);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM diet WHERE diet_id = @dietId";
                        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                        deleteCmd.Parameters.AddWithValue("@dietId", dietId);
                        deleteCmd.ExecuteNonQuery();

                        successDeleteMessage.Style["display"] = "block";

                        ClientScript.RegisterStartupScript(this.GetType(), "HideSuccess", @"
                        <script>
                            setTimeout(function() {
                                document.getElementById('successDeleteMessage').style.display = 'none';
                            }, 1000);
                        </script>", false);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Delete error: " + ex.Message + "')</script>");
                    }
                }

                LoadDiets(); // Refresh the repeater after delete
            }
            else if (e.CommandName == "Edit")
            {
                // Show edit panel for selected diet
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    Panel panel = (Panel)item.FindControl("editPanel");
                    panel.Visible = false;
                }

                RepeaterItem selectedItem = e.Item;
                Panel selectedPanel = (Panel)selectedItem.FindControl("editPanel");
                selectedPanel.Visible = true;
            }
            else if (e.CommandName == "Save")
            {
                int dietId = Convert.ToInt32(e.CommandArgument);
                RepeaterItem item = e.Item;

                TextBox txtMealName = (TextBox)item.FindControl("txtMealName");
                TextBox txtMacros = (TextBox)item.FindControl("txtMacros");
                TextBox txtCalories = (TextBox)item.FindControl("txtCalories");
                TextBox txtImage = (TextBox)item.FindControl("txtImage");

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string updateQuery = "UPDATE diet SET mealName = @mealName, macronutrient = @macros, calories = @calories, diet_image = @image WHERE diet_id = @dietId";
                        MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@mealName", txtMealName.Text);
                        cmd.Parameters.AddWithValue("@macros", txtMacros.Text);
                        cmd.Parameters.AddWithValue("@calories", txtCalories.Text);
                        cmd.Parameters.AddWithValue("@image", txtImage.Text);
                        cmd.Parameters.AddWithValue("@dietId", dietId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Update error: " + ex.Message + "')</script>");
                    }
                }

                LoadDiets(); // Refresh list after saving
            }
        }

    }
}

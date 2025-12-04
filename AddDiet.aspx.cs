using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace fitnessProject
{
    public partial class AddDiet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void AddDietButton_Click(object sender, EventArgs e)
        {
            string imageUrl = dietImage.Text;
            string name = mealName.Text;
            string macronutrientValue = macronutrient.Text;
            string caloriesText = calories.Text;

            // Basic validation for empty fields
            if (string.IsNullOrEmpty(imageUrl) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(macronutrientValue) || string.IsNullOrEmpty(caloriesText))
            {
                Response.Write("<script>alert('Please fill all fields.')</script>");
                return;
            }

            // Validate calories as a number
            int caloriesValue;
            if (!int.TryParse(caloriesText, out caloriesValue))
            {
                Response.Write("<script>alert('Calories must be a valid number.')</script>");
                return;
            }

            // Ensure user is logged in
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('You must be logged in to add a diet.')</script>");
                return;
            }

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO diet (diet_image, mealName, macronutrient, calories, user_id) VALUES (@dietImage, @mealName, @macronutrient, @calories, @user_id)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@dietImage", imageUrl);
                    cmd.Parameters.AddWithValue("@mealName", name);
                    cmd.Parameters.AddWithValue("@macronutrient", macronutrientValue);
                    cmd.Parameters.AddWithValue("@calories", caloriesValue); // now as integer
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["UserID"]));

                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('Diet added successfully!')</script>");
                    Response.Redirect("Diet.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "')</script>");
                }
            }
        }

    }
}

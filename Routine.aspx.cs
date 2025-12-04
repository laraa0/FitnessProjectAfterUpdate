using System;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fitnessProject
{
    public partial class Routine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoutine();
            }
        }

        private void LoadRoutine()
        {
            string connectionString = "server=localhost;user id=root;password=;database=fitness_and_diet_project;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.routine_id,d.diet_image, d.mealName, d.macronutrient, d.calories
                    FROM routine r
                    JOIN diet d ON r.diet_id = d.diet_id
                    WHERE r.user_id = @userId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", Session["UserID"]);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                rptRoutine.DataSource = reader;
                rptRoutine.DataBind();


            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMeal")
            {
                int routineId = Convert.ToInt32(e.CommandArgument);
                string connStr = "server=localhost;user id=root;password=;database=fitness_and_diet_project;";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    string deleteQuery = "DELETE FROM routine WHERE routine_id = @routineId";
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@routineId", routineId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Reload the list
                LoadRoutine();
            }
        }

    }
}

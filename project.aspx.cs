using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySqlConnector;

namespace fitnessProject
{
    public partial class project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Response.Write("✅ MySQL Connected Successfully!");
                }
                catch (Exception ex)
                {
                    Response.Write("❌ Connection Error: " + ex.Message);
                }
            }
        }


    }
}
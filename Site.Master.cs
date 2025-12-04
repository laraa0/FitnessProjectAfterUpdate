using System;
using System.Web;
using System.Web.UI;

namespace fitnessProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string role = Session["Role"] as string;

                if (Session["UserID"] != null)
                {
                    btnLogin.Visible = false;
                    btnLogout.Visible = true;

                    if (role == "admin")
                    {
                        btnAdminDiet.Visible = true;
                        btnAddDiet.Visible = true;
                        btnUserDiet.Visible = false;
                        btnRoutine.Visible = false;
                    }
                    else if (role == "customer")
                    {
                        btnUserDiet.Visible = true;
                        btnRoutine.Visible = true;
                        btnAdminDiet.Visible = false;
                        btnAddDiet.Visible = false;
                    }
                }
                else
                {
                    // User not logged in
                    btnLogin.Visible = true;
                    btnLogout.Visible = false;

                    btnUserDiet.Visible = false;
                    btnRoutine.Visible = false;
                    btnAdminDiet.Visible = false;
                    btnAddDiet.Visible = false;
                }
            }
        }


        protected void BtnDiet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Diet.aspx");
        }

        protected void BtnRoutine_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Routine.aspx");
        }

        protected void BtnAddDiet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddDiet.aspx");
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}

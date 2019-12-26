using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioteca
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            UserClass user = new UserClass();
            user.username = txtUserName.Text;
            user.password = txtPassword.Text;
            user.confpass = txtConfPass.Text;

            if (user.password != user.confpass)
            {
                lblError.Text = "The passwords do not match";
                lblError.Visible = true;
                pnlError.Visible = true;
            }
            else
            {
                UserClass.InregistrareUser(user);
                Session["Successful recording"] = "ok";
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}
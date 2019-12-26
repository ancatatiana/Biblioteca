using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioteca
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ascund orice mesaj de alerta ramas vizibil din greseala;
            pnlError.Visible = false;
            pnlSuccess.Visible = false;
            if (Session["Successful registration"] == "ok")
            {
                lblSuccess.Text = "Successful registration";
                lblSuccess.Visible = true;
                pnlSuccess.Visible = true;
                Session["Successful registration"] = String.Empty;
            }
            else if (Session["User not logged in"] == "true")
            {
                lblError.Text = "Please log in before any action";
                lblError.Visible = true;
                pnlError.Visible = true;
            }
        }




        protected void Log_Click(object sender, EventArgs e)
        {
            //select in db, vezi daca useru exista si are parola buna

            //daca da, redirect catre pagina de myBooks


            // luam datele din pagina pt logare
            UserClass user = new UserClass();
            user.username = txtUserName.Text;
            user.password = txtPassword.Text;
            // accesam baza de date pt a compara datele pt logare
            user = UserClass.Login(user);
            // daca e ok mergem pe pag de materii
            if (user != null)
            {
                Session["logareSucces"] = "ok";
                Session["user"] = user;
                Response.Redirect("~/Books.aspx");
            }
            else
            {
                // daca nu e ok ii cerem sa se logeze din nou
                lblError.Text = "Incorrect password!";
                lblError.Visible = true;
                pnlError.Visible = true;
            }






        }

    }
}

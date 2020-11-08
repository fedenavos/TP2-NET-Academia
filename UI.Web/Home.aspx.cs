using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Home : System.Web.UI.Page
    {
        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["user"];
            Label1.Text = "Bienvenido " + user.NombreUsuario;
        }
    }
}
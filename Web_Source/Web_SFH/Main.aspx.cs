using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_SFH
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = "Signin.aspx";
            var session = HttpContext.Current.Session["id"];           
            if (session == null)
                Response.Redirect(url);
        }
    }
}
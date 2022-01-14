using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_SFH.Donors
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = string.Empty;
            var session = HttpContext.Current.Session["id"];

            if (session == null)
            {
                url = "../Signin.aspx";
                Response.Redirect(url);
            }
        }
    }
}
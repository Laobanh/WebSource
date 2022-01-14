using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTT;

namespace Web_SFH
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = "";
            var session = HttpContext.Current.Session["id"];
            //int userid = session != null ? 1 : 0;
            if (session == null)
            {
                url = "Signin.aspx";
                Response.Redirect(url);
            }          
                
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_SignIn
    /// </summary>
    public class Handler_SignIn : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
         {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            String username = Lib.get_value_str(request["username"]);
            String password = Lib.get_value_str(request["password"]);

            response.ContentType = "text/plain";

            UserInfo userInfo = new UserInfo();

            ResultInfo resultInfo = new ResultInfo();

            int userID = 0;

            Boolean kq = userInfo.Authentication(username, password, ref userID);
            if (kq)
            {
                string sUserid = StringEncryptDecrypt.Encrypt(userID.ToString(), FieldKeys.PassKeyLID);
                resultInfo = new ResultInfo("Sign in authentication successful.", "ok", "/Company", sUserid);
                context.Session["id"] = userID;
                context.Session["username"] = username;
            }
            else
                resultInfo = new ResultInfo("Sign in authentication failed.", "no", "");
            response.Write(JsonConvert.SerializeObject(resultInfo));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
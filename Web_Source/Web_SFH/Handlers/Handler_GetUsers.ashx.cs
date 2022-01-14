using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetUsers
    /// </summary>
    public class Handler_GetUsers : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            if (context.Session["id"] != null)
            {
                UserInfo userInfo = new UserInfo();
                List<UserInfo> users = userInfo.GetUserInfos(context);
                List<UserInfo> userNew = new List<UserInfo>();
                foreach (UserInfo user in users)
                {
                    Person person = user.PersonalInfo.Person;
                    user.PersonalInfo.Person.FullName = person.FirstName + " " + person.LastName;
                    //int id = user.User_Id;
                    //user.ID = StringEncryptDecrypt.Encrypt(id.ToString(), FieldKeys.ID);
                    // user.PersonalInfo.Address.Address = StringEncryptDecrypt.Decrypt(user.ID, FieldKeys.ID);
                    userNew.Add(user);
                }
                response.Write(JsonConvert.SerializeObject(userNew));
            }
            else
                response.Write(JsonConvert.SerializeObject(new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx")));

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
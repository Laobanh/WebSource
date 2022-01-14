using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HTT
{
    public class UserInfo
    {
        //http://localhost:31548/Handlers/Handler_CreateUser.ashx?cmd=c&username=tkchutho&password=Gpsvn.vn&firstName=Tho&lastName=Thong&middleName=Chu&title=Mr&phonehome=0976543218&phonework=0902666405&phonemobile=0902666475&gender=M
        #region Fields            
        private readonly DateTime d = DateTime.Now;
        private String sID = "1";
        private int iUserId = 1;
        private AccountInfo account;
        private PersonalInfo personalInfo;
        private DateTime sCreatedDate, sModifiedDate;
        private String Cmd = "A";
       
        #endregion End Fields

        #region Constructors
        public UserInfo() { }
        public UserInfo(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.iUserId = Lib.get_value_int(request[FieldKeys.UserID]);
            this.account = new AccountInfo(context);
            this.personalInfo = new PersonalInfo(context);
            String sCmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            this.sID = Lib.get_value_str(request[FieldKeys.ID]);
            this.Cmd = sCmd.ToUpper();
            if (this.Cmd.Equals("C"))
                this.sCreatedDate = d;
            else if (this.Cmd.Equals("M"))
                this.sModifiedDate = d;
        }

        public int User_Id
        {
            get
            {
                return iUserId;
            }

            set
            {
                iUserId = value;
            }
        }
        public string ID { get => sID; set => sID = value; }
        public AccountInfo Account
        {
            get
            {
                return account;
            }

            set
            {
                account = value;
            }
        }

        
        public PersonalInfo PersonalInfo
        {
            get
            {
                return personalInfo;
            }

            set
            {
                personalInfo = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return sCreatedDate;
            }

            set
            {
                sCreatedDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return sModifiedDate;
            }

            set
            {
                sModifiedDate = value;
            }
        }

        
        #endregion End Constructors

        #region Methods
        public void CreatedUser(ref string error, HttpContext context, UserInfo user)
        {
            this.WriteUser(ref error, user);
        }

        public void ModifyUser(ref string error, HttpContext context, UserInfo user)
        {
            try
            {
                ResultInfo result = new ResultInfo();
                FilePath fp = new FilePath(FieldKeys.UserClass);

                String filePath = fp.FilePathName;

                UserInfo userModify = new UserInfo(context);

                List<UserInfo> users = this.GetListUser(filePath);
                String id = user.ID;
                
                Boolean kq = users.Exists(x => x.ID.Equals(id));
                if (kq)
                {

                    Boolean isChangePassword = Lib.get_value_int(context.Request[FieldKeys.isChangePassword]) == 0 ? false : true;
                    //var itemToRemove = users.Single(x => x.iUserId.Equals(userModify.iUserId));
                    var itemToRemove = users.Single(x => x.ID.Equals(id));
                    //No change username, password old
                    userModify.account.Username = itemToRemove.account.Username;

                    if (isChangePassword)
                        userModify.account.Password = Lib.EncoderStringSH1(Lib.get_value_str(context.Request[FieldKeys.Password]));
                    else
                        userModify.account.Password = itemToRemove.account.Password;

                    userModify.sCreatedDate = itemToRemove.sCreatedDate;

                    users.Remove(itemToRemove);

                    users.Add(userModify);

                    users.Sort((a, b) => a.iUserId.CompareTo(b.iUserId));

                    String data = JsonConvert.SerializeObject(users);
                    //Base64Encoding
                    //data = Lib.Base64Encoding(data);
                    data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                    Lib.WriteFileJson(ref error, filePath, data);

                    result.Message = error;
                    result.Code = 1;
                    result.Status = "OK";
                    error = JsonConvert.SerializeObject(result);
                }
                else
                {
                    result.Message = "User does not exist, Please check again.";
                    result.Code = 1;
                    result.Status = "NONE";
                    error = JsonConvert.SerializeObject(result);
                }

            }
            catch
            {
                error = "User ID does not exist, Please check again.";
            }
        }

        public void PostUser(ref String error, HttpContext context)
        {
            error = "Success";

            var user = new UserInfo(context);

            String cmd = user.Cmd.ToUpper();

            if (cmd.Equals("C"))
                this.CreatedUser(ref error, context, user);
            else if (cmd.Equals("M"))
                this.ModifyUser(ref error, context, user);

            else error = "No Execute Command";
        }

        public void WriteUser(ref String error, UserInfo user)
        {
            Boolean kq = false;

            List<UserInfo> users = new List<UserInfo>();

            FilePath fp = new FilePath(FieldKeys.UserClass);

            String fileName = fp.FileName;

            String folder = fp.Folder;

            String filePath = fp.FilePathName;

            if (!Directory.Exists(folder))
            {
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                //di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folder);
            }

            String username = user.account.Username;
            string password = user.account.Password;
            if (!String.IsNullOrEmpty(username) 
                && !username.Equals("0") 
                && !String.IsNullOrEmpty(password) 
                && !password.Equals("0"))
            {
                if (!File.Exists(filePath))
                {
                    user.iUserId = 1 + DateTime.Now.Year;
                    user.sID = StringEncryptDecrypt.Encrypt(user.User_Id.ToString(), FieldKeys.PassKeyLID);
                    users.Add(user);
                }
                else //Read data from file json exists
                {

                    users = this.GetListUser(filePath);

                    int count = users.Count + 1 + DateTime.Now.Year;
                    user.iUserId = count;
                    user.sID = StringEncryptDecrypt.Encrypt(user.User_Id.ToString(), FieldKeys.PassKeyLID);
                    //user.sUsername = user.sUsername + count;
                    kq = users.Exists(x => x.Account.Username.Equals(user.Account.Username));
                    if (!kq)
                        users.Add(user);
                    else
                        error = "Username already exists.";

                }

                String data = JsonConvert.SerializeObject(users);
                //Base64Encoding
                //data = Lib.Base64Encoding(data);
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                if (!kq)
                    Lib.WriteFileJson(ref error, filePath, data);
            }
            else
                error = "Username and Password is required.";
        }
        private List<UserInfo> GetListUser(String filePath)
        {
            List<UserInfo> users = new List<UserInfo>();
            try
            {

                String js = String.Empty;
                using (StreamReader read = new StreamReader(filePath))
                {
                    js = read.ReadToEnd();
                    //Base64Decoding
                    //js = Lib.Base64Decoding(js);
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                        users = JsonConvert.DeserializeObject<List<UserInfo>>(js);
                }


            }
            catch (Exception ex)
            {
                Lib.writerLog("User", "GetListUser", ex.Message, "error");

            }
            return users;
        }

        public List<UserInfo> GetUserInfos(HttpContext context)
        {
            List<UserInfo> users = new List<UserInfo>();
            try
            {
                FilePath fp = new FilePath(FieldKeys.UserClass);
               
                String js = String.Empty;
                using (StreamReader read = new StreamReader(fp.FilePathName))
                {
                    js = read.ReadToEnd();
                    //Base64Decoding
                    //js = Lib.Base64Decoding(js);
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                        users = JsonConvert.DeserializeObject<List<UserInfo>>(js);
                  
                }


            }
            catch (Exception ex)
            {
                Lib.writerLog("User", "GetUsers", ex.Message, "error");

            }
            return users;
        }
        public Boolean Authentication(String username, String password, ref int userid)
        {
            Boolean kq = false;
            FilePath fp = new FilePath(FieldKeys.UserClass);

            String filePath = fp.FilePathName;            

            List<UserInfo> users = this.GetListUser(filePath);        
            
            //username = Lib.EncoderStringSH1(username);
            password = Lib.EncoderStringSH1(password);
           
            kq = users.Exists(x => x.Account.Username.Equals(username) && x.Account.Password.Equals(password));

            if (kq)
            {                
                UserInfo user = users.Find(x => x.Account.Username.Equals(username) && x.Account.Password.Equals(password));
                userid = user.User_Id;
                HttpContext.Current.Session["id"] = userid;
            }

            return kq;
        }

        public Boolean Authentication()
        {
            Boolean kq = false;
            try
            {
                String userID = HttpContext.Current.Session["id"].ToString();

                if (userID != "" && String.IsNullOrEmpty(userID))
                    kq = true;
            }
            catch { }
            return kq;
        }

        public void PostAuth(HttpContext context)
        {
            HttpResponse response = context.Response;
            ResultInfo result = new ResultInfo();


            if (context.Session["id"] != null)
            {
                result = new ResultInfo("Authentication", "OK", "", 1);

            }
            else
                result = new ResultInfo("Not Authentication", "Failed", "", 0);


            response.Write(JsonConvert.SerializeObject(result));
        }
        #endregion End Methods
    }
}
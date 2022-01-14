using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;

 

namespace HTT
{
    public class Preferences
    {

        #region Fields
        private MyOrganization myOrganization;
        #endregion End Fields

        #region Constructors
        public Preferences()
        {
            this.myOrganization = new MyOrganization();
        }

        public Preferences(HttpContext context)
        {
            this.myOrganization = new MyOrganization(context);
        }

        public MyOrganization MyOrganization { get => myOrganization; set => myOrganization = value; }
        #endregion End Contructors

        #region Methods
        public static void Created(ref String error, HttpContext context)
        {
            FilePath fp = new FilePath("");
            String path = fp.Folder + "Preferences.json";
            Preferences preference = new Preferences(context);
            preference.MyOrganization.Password = StringEncryptDecrypt.Encrypt(preference.MyOrganization.Password, FieldKeys.PassKey);
            String js = JsonConvert.SerializeObject(preference);
            //Lib.writerLog("Preferences", "Created", js, "error");
            Lib.WriteFileJson(ref error, path, js);
        }

        public static Preferences Get()
        {
            Preferences preference = new Preferences();
            try
            {
                FilePath fp = new FilePath("");
                String path = fp.Folder + "Preferences.json";
                if (File.Exists(path))
                {
                    String js = File.ReadAllText(path);
                    //Lib.writerLog("Preferences", "Get", js, "error");
                    preference = JsonConvert.DeserializeObject<Preferences>(js);
                    preference.MyOrganization.Password = StringEncryptDecrypt.Decrypt(preference.MyOrganization.Password, FieldKeys.PassKey);
                   
                }
            }
            catch(Exception ex)
            {
                Lib.writerLog("Preferences", "Get", ex.Message, "error");
            }
            return preference;
        }
        #endregion End Methods
    }
}

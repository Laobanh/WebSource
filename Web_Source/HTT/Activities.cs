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
    //localhost/Handlers/Handler_CreateActivities.ashx?ID=1008&Details=People&Type=1&Action=Comment on KL STAR INC&Cmd=C
    public class Activities
    {
        #region Fields
        private DateTime dDate;
        private String sUserName = "", sAction = "", sDetails = "";
        private String sID = "";
        private int iType = 0;

        #endregion End Fields

        #region Constructors
        public Activities()
        {
            this.Date = DateTime.Now;
        }

        public Activities(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.dDate = DateTime.Now;
            this.sID = Lib.get_value_str(request[FieldKeys.ID]);
            this.sUserName = Lib.get_value_str(request[FieldKeys.Username]);
            this.sAction = Lib.get_value_str(request[FieldKeys.Action]);
            this.sDetails = Lib.get_value_str(request[FieldKeys.Details]);
            this.iType = Lib.get_value_int(request[FieldKeys.Type]);
        }


        public string ID { get => sID; set => sID = value; }
        public DateTime Date { get => dDate; set => dDate = value; }
        public string UserName { get => sUserName; set => sUserName = value; }
        public string Action { get => sAction; set => sAction = value; }
        public string Details { get => sDetails; set => sDetails = value; }
        public int Type { get => iType; set => iType = value; }
        
        #endregion End Constructors

        #region Methods
        public void Post(ref String error,Activities activity)
        {
            this.Write(ref error, activity);

        }

        public void Write(ref String error, Activities activity)
        {
            FilePath filePath = new FilePath(FieldKeys.ClassActivity);
            String fileName = filePath.Folder + "activity.json";

            if (!Directory.Exists(filePath.Folder))
            {
                Directory.CreateDirectory(filePath.Folder);
            }
            List<Activities> list = this.Gets();
            if (File.Exists(fileName))
            {
                var js = File.ReadAllText(fileName);
                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                list = JsonConvert.DeserializeObject<List<Activities>>(js);
            }

            
            list.Add(activity);
            String result = JsonConvert.SerializeObject(list);
            result = StringEncryptDecrypt.Encrypt(result, FieldKeys.PassKey);

            Lib.WriteFileJson(ref error, fileName, result);

        }
        
        public List<Activities> Gets()
        {
            List<Activities> list = new List<Activities>();
            try
            {
                FilePath filePath = new FilePath(FieldKeys.ClassActivity);
                String fileName = filePath.Folder + "activity.json";
                if (File.Exists(fileName))
                {
                    String js = File.ReadAllText(fileName);
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    list = JsonConvert.DeserializeObject<List<Activities>>(js);
                }
            }
            catch (Exception ex)
            {
                Lib.writerLog("Activities", "Gets", ex.Message, "error");
            }
            return list;

        }
        #endregion End Methods
    }
}

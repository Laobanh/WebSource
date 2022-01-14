using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace HTT
{
    public class Consortiums
    {
        #region Fields
        private String sID = "", sName = "", sCmd = "C";
        private DateTime dCreatedDate, dMofifyDate;
        #endregion End Fields

        #region Constructors
        public Consortiums()
        {
            DateTime d = DateTime.Now;
            this.dCreatedDate = d;
            this.dMofifyDate = d;
        }

        public Consortiums(HttpContext context)
        {
            DateTime d = DateTime.Now;
            HttpRequest request = context.Request;
            this.sID = Lib.get_value_str(request[FieldKeys.ID]);
            this.sName = Lib.get_value_str(request[FieldKeys.NewName]);
            this.sCmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            if (this.sCmd.Equals("C"))
                this.dCreatedDate = d;
            else
                this.dMofifyDate = d;
        }

        public string ID { get => sID; set => sID = value; }
        public string Name { get => sName; set => sName = value; }
        public string Cmd { get => sCmd; set => sCmd = value; }
        public DateTime CreatedDate { get => dCreatedDate; set => dCreatedDate = value; }
        public DateTime MofifyDate { get => dMofifyDate; set => dMofifyDate = value; }
        #endregion End Constructors

        #region Methods
        public void Created(ref String error, HttpContext context)
        {
            this.Write(ref error, context);
        }

        public void Modify(ref String error, HttpContext context)
        {
            try
            {
                FilePath fp = new FilePath(FieldKeys.ConsortiumClass);

                String filePath = fp.FilePathName;

                Consortiums consortiums = new Consortiums(context);

                List<Consortiums> list = this.Gets(filePath);

                int i = 0;
                foreach(var con in list)
                {
                    if (con.ID.Equals(consortiums.ID))
                    {
                        list[i].sName = consortiums.sName;
                    }
                    i++;
                }
               

                list.Sort((a, b) => a.ID.CompareTo(b.ID));

                String data = JsonConvert.SerializeObject(list);
                //Base64Encoding
                // data = Lib.Base64Encoding(data);  
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                Lib.WriteFileJson(ref error, filePath, data);

            }
            catch
            {
                error = "Consortium does not exist, Please check again.";
            }
        }

        public void Post(ref String error, HttpContext context)
        {
            error = "Success";

            var con = new Consortiums(context);

            String cmd = con.Cmd.ToUpper();

            if (cmd.Equals("C"))
                this.Created(ref error, context);
            else if (cmd.Equals("M"))
            {
                this.Modify(ref error, context);
            }
            else error = "No Execute Command";
        }

        public void Write(ref String error, HttpContext context)
        {
            Boolean kq = false;

            var consortium = new Consortiums(context);

            List<Consortiums> list = new List<Consortiums>();

            FilePath fp = new FilePath(FieldKeys.ConsortiumClass);

            String fileName = fp.FileName;

            String folder = fp.Folder;

            String filePath = fp.FilePathName;           

            if (!Directory.Exists(folder))
            {
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                //di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folder);
            }

            String name = consortium.Name;
            if (!String.IsNullOrEmpty(name) && !name.Equals("0"))
            {
                int classNumber = 4000;
                if (!File.Exists(filePath))
                {
                    consortium.ID = (1 + classNumber).ToString();
                    list.Add(consortium);
                }
                else //Read data from file json exists
                {

                    list = this.Gets(filePath);

                    int count = list.Count + 1 + classNumber;

                    consortium.ID = count.ToString();

                    list.Add(consortium);
                }

                String data = JsonConvert.SerializeObject(list);
                //Base64Encoding
                // data = Lib.Base64Encoding(data);
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                if (!kq)
                    Lib.WriteFileJson(ref error, filePath, data);
            }
            else
                error = "Consortium name is required.";
        }
        public List<Consortiums> Gets(String filePath)
        {
            List<Consortiums> list = new List<Consortiums>();
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
                        list = JsonConvert.DeserializeObject<List<Consortiums>>(js);
                }


            }
            catch (Exception ex)
            {
                Lib.writerLog("Consortium", "Gets", ex.Message, "error");

            }
            return list;
        }

        public static List<Consortiums> Gets()
        {
            FilePath fp = new FilePath(FieldKeys.ConsortiumClass);

            String filePath = fp.FilePathName;

            Consortiums con = new Consortiums();

            List<Consortiums> list = con.Gets(filePath);

            return list;
        }

        public static Consortiums Get(String id)
        {
            Consortiums con = new Consortiums();            
            FilePath filePath = new FilePath(FieldKeys.ConsortiumClass);

            List<Consortiums> list = con.Gets(filePath.FilePathName);

            var kq = list.Exists(x => x.ID.Equals(id));
            if (kq)
                con = list.Single(x => x.ID.Equals(id));
            return con;
        }      

        #endregion End Methods
    }
}

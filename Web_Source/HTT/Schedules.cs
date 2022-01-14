using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace HTT
{
    public class Schedules
    {
        #region Fields
        private String sID = "0", sName = "0";
        private int iType = 0, iConsortiumID = 0, isDot = 0;        
        private String sCompanyID = "";
        private Schedule_RunDetails details;
        private Schedule_Specimen specimen1, specimen2;
        private DateTime dStarted;
        private List<Schedule_Selections> selections;
            
        #endregion End Fields

        #region Constructors
        public Schedules()
        {            
            
        }

        public Schedules(HttpContext context)
        {
            HttpRequest request = context.Request;
            String name = Lib.get_value_str(request[FieldKeys.NewName]);            
            this.sName = name;
            this.iType = Lib.get_value_int(request[FieldKeys.Type]);
            this.iConsortiumID = Lib.get_value_int(request[FieldKeys.ConsortiumID]);
            this.sCompanyID = Lib.get_value_str(request[FieldKeys.CompanyID]);
            this.isDot = Lib.get_value_int(request[FieldKeys.IsDot]);
            this.details = new Schedule_RunDetails(request);
            this.specimen1 = new Schedule_Specimen(request, 1);
            this.specimen2 = new Schedule_Specimen(request, 2);
            this.dStarted = DateTime.Now;
            string cmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            // if (cmd.Equals("C"))
            this.sID = "SF" + (this.Count() + 60000 + 1);
           
            // this.selections = Schedule_Selections.Gets(request);

        }
        /// <summary>
        /// Schedule ID
        /// </summary>
        public string ID { get => sID; set => sID = value; }
        /// <summary>
        /// Schedule name
        /// </summary>
        public string Name { get => sName; set => sName = value; }
        /// <summary>
        /// Schedule type 0:Choose by company 1:Choose by Consortium
        /// </summary>
        public int Type { get => iType; set => iType = value; }
        /// <summary>
        /// Consortium ID
        /// </summary>
        public int ConsortiumID { get => iConsortiumID; set => iConsortiumID = value; }
        /// <summary>
        /// CompanyID list
        /// </summary>
        public String CompanyID { get => sCompanyID; set => sCompanyID = value; }
        
        public int IsDot { get => isDot; set => isDot = value; }
        /// <summary>
        /// Schedule details
        /// </summary>
        public Schedule_RunDetails Details { get => details; set => details = value; }
        /// <summary>
        /// Specimen 1
        /// </summary>
        public Schedule_Specimen Specimen1 { get => specimen1; set => specimen1 = value; }
        /// <summary>
        /// Specimen 2
        /// </summary>
        public Schedule_Specimen Specimen2 { get => specimen2; set => specimen2 = value; }        
        /// <summary>
        /// Datetime start Schedule
        /// </summary>
        public DateTime Started { get => dStarted; set => dStarted = value; }
        /// <summary>
        /// Donor Selection list
        /// </summary>
        public List<Schedule_Selections> Selections { get => selections; set => selections = value; }
       

        #endregion End Constructors

        #region Methods
        public void Created(ref String error,Schedules schedules)
        {            

            FilePath fp = new FilePath(FieldKeys.ScheduleClass);
            String filePath = fp.Folder + schedules.ID + ".json";

            if (!Directory.Exists(fp.Folder))
            {
                Directory.CreateDirectory(fp.Folder);
            }

            String js = JsonConvert.SerializeObject(schedules);

            js = StringEncryptDecrypt.Encrypt(js, FieldKeys.PassKey);

            Lib.WriteFileJson(ref error, filePath, js);
        }

        public void Modify(ref String error,Schedules schedules)
        {           

            FilePath fp = new FilePath(FieldKeys.ScheduleClass);
            //file path name
            String filePath = fp.Folder + schedules.ID + ".json";

            //Read all text file
            String str = File.ReadAllText(filePath);

            //Decrypt to strong
            str = StringEncryptDecrypt.Decrypt(str, FieldKeys.PassKey);
            //Convert to schedules object
            var sche = JsonConvert.DeserializeObject<Schedules>(str);

            sche.iType = schedules.Type;
            sche.iConsortiumID = schedules.ConsortiumID;          
            sche.isDot = schedules.IsDot;
            sche.sCompanyID = schedules.CompanyID;
            sche.dStarted = schedules.Started;
            sche.details = schedules.Details;
            sche.specimen1 = schedules.Specimen1;
            sche.specimen2 = schedules.Specimen2;

            //Convert to json 
            String js = JsonConvert.SerializeObject(sche);
            //Encrypt string
            js = StringEncryptDecrypt.Encrypt(js, FieldKeys.PassKey);
            //Write to file
            Lib.WriteFileJson(ref error, filePath, js);
        }

        public void Post(ref String error,Schedules schedules ,HttpContext context)
        {
            HttpRequest request = context.Request;
            String cmd = Lib.get_value_str(request[FieldKeys.Cmd]).ToUpper();
            if (cmd.Equals("C"))
                this.Created(ref error, schedules);
            else if (cmd.Equals("M"))
                this.Modify(ref error, schedules);

        }

        public static List<Schedules> Gets()
        {
            var schedule = new Schedules();

            List<Schedules> list = new List<Schedules>();

            String js = String.Empty;
            FilePath filePath = new FilePath(FieldKeys.ScheduleClass);

            String folder = filePath.Folder;

            var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

            foreach (var file in files)
            {
                String filename = folder + @"\" + file;

                js = File.ReadAllText(filename);

                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                schedule = JsonConvert.DeserializeObject<Schedules>(js);

                list.Add(schedule);

            }
            return list;
        }

        public static Schedules Get(String scheduleId)
        {
            var schedule = new Schedules();

            

            String js = String.Empty;
            FilePath filePath = new FilePath(FieldKeys.ScheduleClass);

            String folder = filePath.Folder;

            var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

            foreach (var file in files)
            {
                if (file.Equals(scheduleId))
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                    schedule = JsonConvert.DeserializeObject<Schedules>(js);
                }                            

            }
            return schedule;
        }

        public static Schedules ScheduleGet(String[] ids)
        {
            FilePath fp = new FilePath(FieldKeys.ScheduleClass);
            String filepath = fp.Folder + ids[0] + ".json";

            var js = File.ReadAllText(filepath);

            js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

            Schedules schedule = JsonConvert.DeserializeObject<Schedules>(js);

            return schedule;

        }
        public int Count()
        {
            int kq = 0;
            try
            {
                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.ScheduleClass);

                String folder = filePath.Folder;

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));
                kq = Directory.GetFiles(folder).Length;
            }
            catch { }
            
            return kq;
        }

        #endregion End Methods
    }
}
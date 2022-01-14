using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTT
{
    public class Schedule_Selections
    {
        #region Fields
        private String sID = "0", sName = "";
        private DateTime dRunOn;
        private int iStatus = 0, iCollected = 0;
        private List<Specimen> donorSpecimenList;

        #endregion End Fields

        #region Constructors
        public Schedule_Selections()
        {
            
        }

        public Schedule_Selections(String id, String name, DateTime runDate, int status, int collected, List<Specimen> donors)
        {
            this.sID = id;
            this.sName = name;
            this.dRunOn = runDate;
            this.iStatus = status;
            this.iCollected = collected;
            this.donorSpecimenList = donors;               
        }

        public string ID { get => sID; set => sID = value; }
        public string Name { get => sName; set => sName = value; }
        public DateTime RunOn { get => dRunOn; set => dRunOn = value; }
        public int Status { get => iStatus; set => iStatus = value; }
        public int Collected { get => iCollected; set => iCollected = value; }
        public List<Specimen> DonorSpecimenList { get => donorSpecimenList; set => donorSpecimenList = value; }

        #endregion End Constructors

        #region Methods
        public static List<Schedule_Selections> Gets(HttpContext context)
        {
            HttpRequest request = context.Request;

            Schedules schedule = new Schedules(context);


            List<Schedule_Selections> list = new List<Schedule_Selections>();

            var selection = new Schedule_Selections();

            //Get schedule repeat
            var runDetails = Schedule_RunDetails.Gets(request);

            List<Specimen> donorList = new List<Specimen>();

            int runNow = Lib.get_value_int(request[FieldKeys.RunNow]);
            String id = schedule.ID;  
            String name = String.Empty;
            int i = 0, status = 0, collection = 0;
            foreach (var detail in runDetails)
            {
                donorList = new List<Specimen>();
                status = 0;
                name = String.Empty;
                if (i.Equals(0))
                {
                    if (runNow.Equals(1))
                    {
                        donorList = Specimen.Random(context);
                        status = 1;                        
                        name = schedule.Name;
                    }
                }
                string sid = id + "_" + i.ToString();
                //string sname = name + "_" + i.ToString();

                selection = new Schedule_Selections(sid, name, detail, status, collection, donorList);
                list.Add(selection);
                i++;               
            }
            return list;
        }

       
        #endregion End Methods

    }
}

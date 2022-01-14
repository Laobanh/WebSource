using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HTT
{
    public class Schedule_RunDetails
    {
        #region Fields
        private int iRunNow = 0, iRepeats = 0, iRandomOnly = 0, iNumberOfTimes = 0;
        private DateTime dEndOn, dRunDate;
        private String sRunTime = "00:00", sDayOfWeek = "0,0,0,0,0,0,0";
        private String sRepeatText = "0";
        #endregion End Fields

        #region Constructors
        public Schedule_RunDetails()
        {

        }

        public Schedule_RunDetails(HttpRequest request)
        {
            this.iRunNow = Lib.get_value_int(request[FieldKeys.RunNow]);
            this.iRepeats = Lib.get_value_int(request[FieldKeys.Repeat]);
            this.iNumberOfTimes = Lib.get_value_int(request[FieldKeys.NumberOfTimes]);
            this.iRandomOnly = Lib.get_value_int(request[FieldKeys.RandomOnly]);            
            String sdate = Lib.get_value_str(request[FieldKeys.EndOn]);
            DateTime d = DateTime.Now;
            if (sdate.Equals("0"))
                this.dEndOn = new DateTime(d.Year, 12, 31);
            else
                this.dEndOn = Lib.RetDateTime(sdate + " 23:59");
            
            this.sRunTime = Lib.get_value_str(request[FieldKeys.RunTime]);
            this.sDayOfWeek = Lib.get_value_str(request[FieldKeys.DayOfWeek]);

            String s = Schedule_RunDetails.GetRepeatText(this.iRepeats);
            s = this.iNumberOfTimes + " " + (this.iRepeats.Equals(0) ? s : s.Substring(1));
            this.sRepeatText = s;
           
        }

        /// <summary>
        /// Run after created
        /// </summary>
        public int RunNow { get => iRunNow; set => iRunNow = value; }
        /// <summary>
        /// 0: Don't Repeat, 1: x times per week, 2: x times per month, 3: x times per quarter, 4: x times per year 
        /// </summary>
        public int Repeats { get => iRepeats; set => iRepeats = value; }
        /// <summary>
        /// Schedule random only
        /// </summary>
        public int RandomOnly { get => iRandomOnly; set => iRandomOnly = value; }
        public int NumberOfTimes { get => iNumberOfTimes; set => iNumberOfTimes = value; }
        /// <summary>
        /// Datetime End Schedules
        /// </summary>
        public DateTime EndOn { get => dEndOn; set => dEndOn = value; }
        /// <summary>
        /// Schedule run on date
        /// </summary>
        public DateTime RunDate { get => dRunDate; set => dRunDate = value; }
        /// <summary>
        /// Schedules run on time
        /// </summary>
        public string RunTime { get => sRunTime; set => sRunTime = value; }
        /// <summary>
        /// Schedule run on week
        /// </summary>
        public string DayOfWeek { get => sDayOfWeek; set => sDayOfWeek = value; }
        public string RepeatText { get => sRepeatText; set => sRepeatText = value; }
        #endregion End Constructors

        #region Methods
        public static List<DateTime> Gets(HttpRequest request)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            var details = new Schedule_RunDetails(request);
           
            DateTime d = new DateTime();
            
            String[] arrayDayOfWeek = details.DayOfWeek.Split(',');

            string[] arrayTime = details.RunTime.Split(':');

            int hour = 2; 

            int minute = 0; 

            DateTime dateCurrent = DateTime.Now;
            
            TimeSpan t;
            t = details.EndOn - dateCurrent;
            int times = 0;
            int count = 0;
            int numberOfTimes = 0;
            int numberTimes = details.NumberOfTimes;
            switch (details.Repeats)
            {
                case FieldVaules.noneRepeat:
                    count = 1;                   
                    break;
                case FieldVaules.xTimesPerWeek:
                    count = 7;                   
                    break;
                case FieldVaules.xTimesPerMonth:
                    count = 30;                   
                    break;
                case FieldVaules.xTimesPerQuarter:
                    count = 90;                   
                    break;
                case FieldVaules.xTimesPerYear:
                    count = 364;
                    break;
            }
            times = (int)t.TotalDays / count;

            numberOfTimes = (int)(Math.Ceiling((double)count / numberTimes));

            if (count > 1)
            {
                //Get Monday
                if (count.Equals(7))
                    dateCurrent = Lib.GetMonday(dateCurrent);
                else if (count.Equals(364))
                    times = numberTimes;

                // Lib.writerLog("Schedule_RunDetails", "Gets", times.ToString(), "error");

                for (int i = 0; i <= times; i++)
                {
                    for (int k = 0; k < numberTimes; k++)
                    {
                        if (i.Equals(0) && k.Equals(0) && details.RunNow.Equals(1))
                        {
                            d = DateTime.Now;
                            dateTimes.Add(d);
                        }
                        else
                        {
                            int dayPlus = count * i + (k * numberOfTimes);
                            d = dateCurrent.AddDays(dayPlus);
                            d = Lib.DayWorking(arrayDayOfWeek, d);
                            t = details.EndOn - d;
                            if ((int)t.TotalDays > 0)
                            {

                                if (arrayTime.Length.Equals(2))
                                {
                                    hour = 2; Lib.get_value_int(arrayTime[0]);
                                    minute = 0; Lib.get_value_int(arrayTime[1]);
                                }
                                d = new DateTime(d.Year, d.Month, d.Day, hour, minute, 0);
                                dateTimes.Add(d);
                            }
                        }



                    }
                }
            }
            else
            {
                d = dateCurrent;
                d = Lib.DayWorking(arrayDayOfWeek, d);
                t = details.EndOn - d;
                if (details.RunNow.Equals(1))
                {
                    d = DateTime.Now;
                    dateTimes.Add(d);
                }
                else if ((int)t.TotalDays > 0)
                {
                    d = new DateTime(d.Year, d.Month, d.Day, hour, minute, 0);
                    dateTimes.Add(d);
                }


            }

            dateTimes.Sort();
            return dateTimes;
        }


        public static String GetRepeatText(int repeatType)
        {
            String kq = String.Empty;
            try
            {
                FilePath fp = new FilePath("");
                String filePath = fp.Folder + "Configurations.json";
                if (File.Exists(filePath))
                {
                    String js = File.ReadAllText(filePath);
                    //Lib.writerLog("Schedule_RunDetail", "GetRepeatText", js, "error");
                    JObject list = JObject.Parse(js);
                    kq = (String)list["Repeat"][repeatType];
                }
            }
            catch { }
            return kq;
        }
        #endregion End MEthods
    }
}

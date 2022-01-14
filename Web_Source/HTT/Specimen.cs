using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace HTT
{
   public class Specimen
    {
        #region Fields
        private String sDonorID, sSpecimen1, sSpecimen2;
        private String sCompanyID;
        private String sCompanyName = "";
        private Boolean bSelected = false;
        private Boolean isAlternate = false;
        private String sFullName = "", sPrimaryID = "";
        #endregion End Fields

        #region Constructors
        public Specimen()
        {

        }

        public Specimen(String donorID,String companyid, String specimen1, String specimen2, Boolean selected,Boolean alternate)
        {
            this.sDonorID = donorID;
            this.sCompanyID = companyid;
            this.sSpecimen1 = specimen1;
            this.sSpecimen2 = specimen2;
            this.bSelected = selected;
            this.isAlternate = alternate;            
        }


        public string DonorID { get => sDonorID; set => sDonorID = value; }
        public string FullName { get => sFullName; set => sFullName = value; }
        public string PrimaryID { get => sPrimaryID; set => sPrimaryID = value; }
        public String CompanyID { get => sCompanyID; set => sCompanyID = value; }
        public string CompanyName { get => sCompanyName; set => sCompanyName = value; }
        public string Specimen1 { get => sSpecimen1; set => sSpecimen1 = value; }
        public string Specimen2 { get => sSpecimen2; set => sSpecimen2 = value; }
        public bool Selected { get => bSelected; set => bSelected = value; }
        public bool IsAlternate { get => isAlternate; set => isAlternate = value; }
        


        #endregion End Constructors

        #region Methods
        public static List<Specimen> Gets(String selectionId)
        {
            String[] ids = selectionId.Split('_');
            //Get Random selection file
            var schedule = Schedules.Get(ids[0]);

            Schedule_Selections selections = schedule.Selections.Single(x => x.ID.Equals(selectionId));

            List<Specimen> list = selections.DonorSpecimenList;

            return list;
        }

        public static List<Specimen> Gets(Schedules schedule)
        {
            List<Specimen> list = new List<Specimen>();

            List<string> companyIDs = new List<string>();

            if (schedule.Type.Equals(0))
            {
                companyIDs.Add(schedule.CompanyID);
            }
            else if (schedule.Type.Equals(1))
            {
                List<CompanyInfo> coms = CompanyInfo.Gets().FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID));
                foreach (var s in coms)
                {
                    companyIDs.Add(s.CompanyID.ToString());
                }
            }
            DonorInfo donorInfo = new DonorInfo();

            List<DonorInfo> donors =  donorInfo.Gets();

            foreach (var donor in donors)
            {
                
                if (companyIDs.Exists(x => x.Equals(donor.CompanyID)))
                {                    
                    list.Add(new Specimen(donor.ID, donor.CompanyID, String.Empty, String.Empty, false, false));
                }
                    

            }

            return list;
        }

        public static List<Specimen> Gets(HttpContext context)
        {
            //Init Schedule           
            Schedules schedule = new Schedules(context);

            List<Specimen> list = new List<Specimen>();

            List<string> companyIDs = new List<string>();
            //Type = 0 Pull by Company
            if (schedule.Type.Equals(0))
            {
                //Add Company ID to list
                companyIDs.Add(schedule.CompanyID);
            }
            else if (schedule.Type.Equals(1))//Type = 1 Pull by ConsrotiumID
            {
                //Get list companyID coms.FindAll(x => x.ConsortiumId.Equals(consortiumID.ToString())).Select(x => x.CompanyID).ToList();    
                var coms = CompanyInfo.Gets().FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID.ToString())).Select(x => x.CompanyID).ToList();
                //CompanyInfo.Gets().FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID));
                foreach (var s in coms)
                {
                    //Add companyID to list
                    companyIDs.Add(s.ToString());
                }
            }
            DonorInfo donorInfo = new DonorInfo();
            //Get All donor list
            List<DonorInfo> donors = donorInfo.Gets();
            donors = donors.FindAll(x => x.ExcludeFromSelection.Equals(0));
            foreach (var donor in donors)
            {
                if (companyIDs.Exists(x => x.Equals(donor.CompanyID)))
                    list.Add(new Specimen(donor.ID, donor.CompanyID, String.Empty, String.Empty, false, false));

            }

            return list;
        }

        public static List<Specimen> Random(HttpContext context)
        {
            //HttpRequest request = context.Request;
            List<Specimen> list = new List<Specimen>();

            var schedule = new Schedules(context);

            List<Specimen> specimenDonorList = Specimen.Gets(context);
            int maxLength = specimenDonorList.Count;

            Schedule_Specimen sp1 = schedule.Specimen1;
            Schedule_Specimen sp2 = schedule.Specimen2;

            // Lib.writerLog("Specimen", "Random", String.Format("NumDonor:{0} ", JsonConvert.SerializeObject(sp1)), "error");

            Boolean isAlternate = false;

            list = Specimen.Random(specimenDonorList, sp1, sp2, isAlternate);

            isAlternate = true;
            //int type1 = schedule.Specimen1.AlternateNumberDonorType;

            //double numDonor1 = schedule.Specimen1.AlternateNumberDonor;

            //int numdonorAlternate = Specimen.NumberDonor(type1, maxLength, numDonor1);

            //maxLength = maxLength - list.Count;

            //numdonorAlternate = (numdonorAlternate > maxLength ? maxLength : numdonorAlternate);

            sp1.NumberDonor = schedule.Specimen1.AlternateNumberDonor;
            sp1.NumberDonorType = schedule.Specimen1.AlternateNumberDonorType;

            sp2.NumberDonor = schedule.Specimen2.AlternateNumberDonor;
            sp2.NumberDonorType = schedule.Specimen2.AlternateNumberDonorType;

           // var list2 = specimenDonorList.FindAll(x => x.Selected.Equals(false));

           // Lib.writerLog("Specimen", "Random", String.Format("NumDonor:{0} ", JsonConvert.SerializeObject(list2)), "error");

            list = Specimen.Random(specimenDonorList, sp1, sp2, isAlternate);

            return specimenDonorList;
        }

        //
        public static List<Specimen> Random(List<Specimen> specimenDonorList, double numberDonors, int numberDonorType, String specimenName, int numberSpecimen, Boolean isAlternate)
        {

            List<Specimen> list = new List<Specimen>();

            List<Specimen> specimenList = specimenDonorList;
            //Alternate
            if (isAlternate)
            {
                specimenList = specimenList.FindAll(x => x.Selected.Equals(false));
               // specimenDonorList.Sort((x, y) => x.Selected.CompareTo(y.Selected));
            }
            Lib.writerLog("Specimen", "Random", String.Format("{0} numberDonor {1} Alternate {2}", numberSpecimen, numberDonors, isAlternate), "error");
            //List<String> companyIDs = specimenList.Select(x => x.CompanyID).ToList();

            List<int> numberRandomList = new List<int>();

            int k = 0;

            int maxLength = specimenList.Count;

            int donorNumber = Specimen.NumberDonor(numberDonorType, specimenDonorList.Count, numberDonors);

            if (numberSpecimen.Equals(2))
                donorNumber = (int)numberDonors;

            Lib.writerLog("Specimen", "Random", String.Format("{0} numberDonor {1} Alternate {2}", numberSpecimen, donorNumber,isAlternate), "error");

            //Kiem tra so nguoi chon co phai it hon danh sach khong
            if (donorNumber > maxLength)
                donorNumber = maxLength;
            //Set Random selection donor specimen 1
            while (k < donorNumber)
            {
                Random random = new Random();
                int r = random.Next(0, maxLength);
                if (!numberRandomList.Exists(x => x.Equals(r)))
                {
                    numberRandomList.Add(r);

                    specimenList[r].IsAlternate = isAlternate;
                    specimenList[r].Selected = !isAlternate;
                    if (numberSpecimen.Equals(1))
                        specimenList[r].Specimen1 = specimenName;
                    else
                        specimenList[r].Specimen2 = specimenName;
                    list.Add(specimenList[r]);
                    k++;
                }
            }


            return list;
        }

        
        public static List<Specimen> Random(List<Specimen> specimenDonorList, Schedule_Specimen sp1, Schedule_Specimen sp2, Boolean isAlternate)
        {

            List<Specimen> list = Random(specimenDonorList, sp1.NumberDonor, sp1.NumberDonorType, sp1.Type, 1, isAlternate);

            int numberDonor2 = Specimen.NumberDonor(sp2.NumberDonorType, specimenDonorList.Count, sp2.NumberDonor);

            if (list.Count.Equals(0))
                list = specimenDonorList;

            numberDonor2 = numberDonor2 > list.Count ? list.Count : numberDonor2;
            List<Specimen> list2 = Random(list, numberDonor2, sp2.NumberDonorType, sp2.Type, 2, isAlternate);

            return list;

        }

        public static int NumberDonor(int numberDonorType,int donorListLength,double numberDonors)
        {
            int number = 0;
            switch (numberDonorType)
            {
                case FieldVaules.peopleOfGroup:
                    number = (int)Lib.C1Round(numberDonors, 0);
                    break;
                case FieldVaules.percentOfPeopleGroup:
                    double d = (double)(numberDonors * donorListLength) / 100;
                    number = (int)Math.Ceiling(d);//(int)Lib.C1Round(d, 0);// 
                    break;
            }
            return number;
        }

        #endregion End Methods

    }
}

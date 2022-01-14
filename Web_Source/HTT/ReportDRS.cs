using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using Newtonsoft.Json;


namespace HTT
{
    public class ReportDRS
    {
        #region Fields
        private List<MROResult> mRORes;       

        #endregion End Fields

        #region Constructors

        public ReportDRS()
        {

        }

        public ReportDRS(XmlDocument doc)
        {
            MROResult results = new MROResult();
            XmlNodeList nodes = doc.SelectNodes("/drsMROExport/MROResult");
            this.mRORes = results.GetMROResults(nodes);
            
        }
        

        public List<MROResult> MRORes { get => mRORes; set => mRORes = value; }

        #endregion End Constructors

        #region Methods
        public void CreateJsonPDF(ref String kq,String xmlPath,String Name)
        {
            //String xmlPath = System.Web.HttpContext.Current.Server.MapPath("~") + @"Data\MROReports\";
            String fileName = xmlPath + @"\DRS\" + Name;
            

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);


            ReportDRS reports = new ReportDRS(doc);

            //Lib.WriteFileJson(ref kq, @"D:\PDF\test.json", JsonConvert.SerializeObject(reports));

            String reportFolder = xmlPath + "SFH";
            if (!Directory.Exists(reportFolder))
                Directory.CreateDirectory(reportFolder);

            //Write MRO Report PDF file
            String pdfFolder = reportFolder + @"\PDF";
            if (!Directory.Exists(pdfFolder))
                Directory.CreateDirectory(pdfFolder);


            List<ReportDRS> reportsList = new List<ReportDRS>();
            foreach (MROResult result in reports.MRORes)
            {

                String fileNameDestanition = pdfFolder + @"\" + result.PatientID + ".pdf";

                Lib.WriteFilePDF(ref kq, fileNameDestanition, Convert.FromBase64String(result.ReportBinary.Data));               

                //Create Report data
                String jsonFolder = reportFolder + @"\JSON";
                if (!Directory.Exists(jsonFolder))
                    Directory.CreateDirectory(jsonFolder);

                String jsonFileName = jsonFolder + @"\" + result.PatientID + ".json";

                result.ReportBinary = new ReportBinary("Data/MROReports/SFH/PDF/" + result.PatientID + ".pdf", "");

                String sData = JsonConvert.SerializeObject(result);

                //Write Json file           
                Lib.WriteFileJson(ref kq, jsonFileName, sData);
            }
        }
        #endregion End Methods


    }

    public class MROResult
    {
        #region Fields
        //MROResult
        private String sPatientID = "", sSpecimenNumber = "", sLastName = "", sFirstName = "", sSSN = "", sAlternateID = "", sLab = "", sLabAccount = "", sCompanyName = "", sResult = "", sTestReason = "", sSpecimenType = "", sTestType = "", sMROName = "";
        private String sCollectionDate = "", sResultDate = "", sMRODate = "";
        private ResultCollection resCollection;
        private List<ResultDrug> resDrugs;
        private List<ResultNotes> resNotes;
        private ReportBinary repBinary;
        #endregion End Fields

        #region Constructors
        public MROResult()
        {

        }

        public MROResult(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("/drsMROExport/MROResult");
            this.sPatientID = Lib.get_value_str(node["PatientID"].InnerText);
            this.SpecimenNumber = Lib.get_value_str(node["SpecimenNumber"].InnerText);
            this.sLastName = Lib.get_value_str(node["LastName"].InnerText);
            this.sFirstName = Lib.get_value_str(node["FirstName"].InnerText);
            this.sSSN = Lib.get_value_str(node["ssn"].InnerText);

            this.sAlternateID = Lib.get_value_str(node["altid"]?.InnerText);
            this.sLab = Lib.get_value_str(node["Lab"].InnerText);
            this.sLabAccount = Lib.get_value_str(node["LabAccount"].InnerText);
            this.sCompanyName = Lib.get_value_str(node["CompanyName"].InnerText);
            this.sResult = Lib.get_value_str(node["Result"].InnerText);

            this.sTestReason = Lib.get_value_str(node["TestReason"].InnerText);
            this.sSpecimenType = Lib.get_value_str(node["SpecimenType"].InnerText);
            this.sTestType = Lib.get_value_str(node["TestType"].InnerText);
            this.sMROName = Lib.get_value_str(node["MROName"].InnerText);

            this.sCollectionDate = Lib.get_value_str(node["CollectionDate"].InnerText);
            this.sResultDate = Lib.get_value_str(node["ResultDate"].InnerText);
            this.sMRODate = Lib.get_value_str(node["MRODate"].InnerText);

            //Set resCollection
            node = doc.SelectSingleNode("/drsMROExport/MROResult/resCollection");
            this.resCollection = new ResultCollection(node);

            //Set ResDrugs
            XmlNodeList nodes = doc.SelectNodes("/drsMROExport/MROResult/resDrug");
            ResultDrug drug = new ResultDrug();
            this.resDrugs = drug.GetDrugs(nodes);

            //set  resNotes
            nodes = doc.SelectNodes("/drsMROExport/MROResult/resNotes");
            ResultNotes note = new ResultNotes();
            this.resNotes = note.GetNotes(nodes);

            //set Report
            node = doc.SelectSingleNode("/drsMROExport/MROResult/ReportBinary");
            this.repBinary = new ReportBinary(node, this.SpecimenNumber);

        }

        public MROResult(XmlNode node)
        {
           // XmlNode node = doc.SelectSingleNode("/drsMROExport/MROResult");
            this.sPatientID = Lib.get_value_str(node["PatientID"].InnerText);
            this.SpecimenNumber = Lib.get_value_str(node["SpecimenNumber"].InnerText);
            this.sLastName = Lib.get_value_str(node["LastName"].InnerText);
            this.sFirstName = Lib.get_value_str(node["FirstName"].InnerText);
            this.sSSN = Lib.get_value_str(node["ssn"].InnerText);

            this.sAlternateID = Lib.get_value_str(node["altid"]?.InnerText);
            this.sLab = Lib.get_value_str(node["Lab"].InnerText);
            this.sLabAccount = Lib.get_value_str(node["LabAccount"].InnerText);
            this.sCompanyName = Lib.get_value_str(node["CompanyName"].InnerText);
            this.sResult = Lib.get_value_str(node["Result"].InnerText);

            this.sTestReason = Lib.get_value_str(node["TestReason"].InnerText);
            this.sSpecimenType = Lib.get_value_str(node["SpecimenType"].InnerText);
            this.sTestType = Lib.get_value_str(node["TestType"].InnerText);
            this.sMROName = Lib.get_value_str(node["MROName"].InnerText);

            this.sCollectionDate = Lib.get_value_str(node["CollectionDate"].InnerText);
            this.sResultDate = Lib.get_value_str(node["ResultDate"].InnerText);
            this.sMRODate = Lib.get_value_str(node["MRODate"].InnerText);

            //Set resCollection
            //XmlNode nodeCollection = node.SelectSingleNode("/drsMROExport/MROResult/resCollection");
            XmlNode nodeCollection = node.SelectSingleNode("resCollection");
            this.resCollection = new ResultCollection(nodeCollection);

            //Set ResDrugs
            XmlNodeList nodes = node.SelectNodes("resDrug");
            ResultDrug drug = new ResultDrug();
            this.resDrugs = drug.GetDrugs(nodes);

            //set  resNotes
            nodes = node.SelectNodes("resNotes");
            ResultNotes note = new ResultNotes();
            this.resNotes = note.GetNotes(nodes);

            //set Report
            node = node.SelectSingleNode("ReportBinary");
            this.repBinary = new ReportBinary(node, this.SpecimenNumber);

        }
        public string PatientID
        {
            get
            {
                return sPatientID;
            }

            set
            {
                sPatientID = value;
            }
        }

        public string SpecimenNumber
        {
            get
            {
                return sSpecimenNumber;
            }

            set
            {
                sSpecimenNumber = value;
            }
        }

        public string LastName
        {
            get
            {
                return sLastName;
            }

            set
            {
                sLastName = value;
            }
        }
        public string FirstName
        {
            get
            {
                return sFirstName;
            }

            set
            {
                sFirstName = value;
            }
        }

        public string SSN
        {
            get
            {
                return sSSN;
            }

            set
            {
                sSSN = value;
            }
        }

        public string AlternateID
        {
            get
            {
                return sAlternateID;
            }

            set
            {
                sAlternateID = value;
            }
        }

        public string Lab
        {
            get
            {
                return sLab;
            }

            set
            {
                sLab = value;
            }
        }

        public string LabAccount
        {
            get
            {
                return sLabAccount;
            }

            set
            {
                sLabAccount = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return sCompanyName;
            }

            set
            {
                sCompanyName = value;
            }
        }

        public string Result
        {
            get
            {
                return sResult;
            }

            set
            {
                sResult = value;
            }
        }

        public string TestReason
        {
            get
            {
                return sTestReason;
            }

            set
            {
                sTestReason = value;
            }
        }

        public string SpecimenType
        {
            get
            {
                return sSpecimenType;
            }

            set
            {
                sSpecimenType = value;
            }
        }

        public string TestType
        {
            get
            {
                return sTestType;
            }

            set
            {
                sTestType = value;
            }
        }

        public string MROName
        {
            get
            {
                return sMROName;
            }

            set
            {
                sMROName = value;
            }
        }

        public string CollectionDate
        {
            get
            {
                return sCollectionDate;
            }

            set
            {
                sCollectionDate = value;
            }
        }

        public string ResultDate
        {
            get
            {
                return sResultDate;
            }

            set
            {
                sResultDate = value;
            }
        }

        public string MRODate
        {
            get
            {
                return sMRODate;
            }

            set
            {
                sMRODate = value;
            }
        }

        public ResultCollection ResCollection
        {
            get
            {
                return resCollection;
            }

            set
            {
                resCollection = value;
            }
        }

        public List<ResultDrug> ResDrugs
        {
            get
            {
                return resDrugs;
            }

            set
            {
                resDrugs = value;
            }
        }

        public List<ResultNotes> ResNotes
        {
            get
            {
                return resNotes;
            }

            set
            {
                resNotes = value;
            }
        }

        public ReportBinary ReportBinary
        {
            get
            {
                return repBinary;
            }

            set
            {
                repBinary = value;
            }
        }

       

       


        #endregion End Constructors

        #region Methods

        public List<MROResult> GetMROResults(XmlNodeList nodes)
        {
            List<MROResult> list = new List<MROResult>();
            foreach(XmlNode node in nodes)
            {
                MROResult result = new MROResult(node);
                list.Add(result);
            }
            return list;
        }

        public List<MROResult> Gets()
        {
            List<MROResult> list = new List<MROResult>();

            FilePath fp = new FilePath(FieldKeys.MroReports);
            var jsonDirectory = fp.Folder + @"\SFH\JSON";

            var files = Directory.GetFiles(jsonDirectory).Select(x => Path.GetFileName(x));

            foreach (var file in files)
            {
                String filePath = jsonDirectory + @"\" + file;
                String json = File.ReadAllText(filePath);

                MROResult rep = JsonConvert.DeserializeObject<MROResult>(json);

                list.Add(rep);
            }

            list.Sort((x, y) => Lib.ret_date(y.CollectionDate).CompareTo(Lib.ret_date(x.CollectionDate)));

            return list;
        }
        public List<MROResult> Gets(String DonorID)
        {
            List<MROResult> list = new List<MROResult>();

            FilePath fp = new FilePath(FieldKeys.MroReports);
            var jsonDirectory = fp.Folder + @"\SFH\JSON";

            var files = Directory.GetFiles(jsonDirectory).Select(x => Path.GetFileName(x));

            foreach (var file in files)
            {
                String filePath = jsonDirectory + @"\" + file;
                String json = File.ReadAllText(filePath);

                MROResult rep = JsonConvert.DeserializeObject<MROResult>(json);
                var id = rep.AlternateID.Replace(" ", "");
                if (id.Equals(DonorID))
                    list.Add(rep);
            }

            list.Sort((x, y) => Lib.ret_date(y.CollectionDate).CompareTo(Lib.ret_date(x.CollectionDate)));

            return list;
        }
        #endregion End Methods

    }

    public class ResultCollection
    {
        #region Fields
        private String sPatientID = "", sLocation = "", sCollectorName = "", sCollectionSitePhone = "", sCollectionLocationCode = "";
        #endregion End Fields

        #region Constructors
        public ResultCollection()
        {

        }

        public ResultCollection(XmlNode node)
        {
            this.sPatientID = Lib.get_value_str(node["Patientid"]?.InnerText);
            this.sLocation = Lib.get_value_str(node["Location"]?.InnerText);
            this.sCollectorName = Lib.get_value_str(node["CollectorName"]?.InnerText);
            this.sCollectionSitePhone = Lib.get_value_str(node["CollectionSitePhone"]?.InnerText);
            this.sCollectionLocationCode = Lib.get_value_str(node["CollectionLocationCode"]?.InnerText);

        }

        public string PatientID
        {
            get
            {
                return sPatientID;
            }

            set
            {
                sPatientID = value;
            }
        }

        public string Location
        {
            get
            {
                return sLocation;
            }

            set
            {
                sLocation = value;
            }
        }

        public string CollectorName
        {
            get
            {
                return sCollectorName;
            }

            set
            {
                sCollectorName = value;
            }
        }

        public string CollectionSitePhone
        {
            get
            {
                return sCollectionSitePhone;
            }

            set
            {
                sCollectionSitePhone = value;
            }
        }

        public string CollectionLocationCode
        {
            get
            {
                return sCollectionLocationCode;
            }

            set
            {
                sCollectionLocationCode = value;
            }
        }
        #endregion End Constructors

        #region Methods
        #endregion End Methods
    }

    public class ResultDrug
    {
        #region Fields
        private String sPatientID = "", sDrugCode = "", sDrugName = "", sResult = "", sScreenCutoff = "", sConfirmCutoff = "";
        #endregion End Fields

        #region Constructors
        public ResultDrug()
        {

        }

        public ResultDrug(XmlNode node)
        {
            this.sPatientID = Lib.get_value_str(node["Patientid"].InnerText);
            this.sDrugCode = Lib.get_value_str(node["DrugCode"].InnerText);
            this.sDrugName = Lib.get_value_str(node["DrugName"].InnerText);
            this.sResult = Lib.get_value_str(node["Result"].InnerText);
            this.sScreenCutoff = "";
            this.sConfirmCutoff = "";
        }

        public string PatientID
        {
            get
            {
                return sPatientID;
            }

            set
            {
                sPatientID = value;
            }
        }

        public string DrugCode
        {
            get
            {
                return sDrugCode;
            }

            set
            {
                sDrugCode = value;
            }
        }

        public string DrugName
        {
            get
            {
                return sDrugName;
            }

            set
            {
                sDrugName = value;
            }
        }

        public string Result
        {
            get
            {
                return sResult;
            }

            set
            {
                sResult = value;
            }
        }

        public string ScreenCutoff
        {
            get
            {
                return sScreenCutoff;
            }

            set
            {
                sScreenCutoff = value;
            }
        }

        public string ConfirmCutoff
        {
            get
            {
                return sConfirmCutoff;
            }

            set
            {
                sConfirmCutoff = value;
            }
        }
        #endregion End Constructors

        #region Methods
        public List<ResultDrug> GetDrugs(XmlNodeList nodes)
        {
            List<ResultDrug> resultDrugs = new List<ResultDrug>();
            String kq = String.Empty;
           
            for (int i = 0; i < nodes.Count; i++)
            {
                ResultDrug resultDrug = new ResultDrug(nodes[i]);
                    
                kq += "<root>";
                foreach (XmlNode n in nodes[i].ChildNodes)
                {
                    if (n is XmlCDataSection)
                    {
                        kq += n.Value;
                    }

                }
                kq += "</root>";
               
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kq);
                XmlNodeList list = xmlDoc.SelectNodes("root");
                foreach (XmlNode xNode in list)
                {
                    String s1 = Lib.get_value_str(xNode["screen_cutoff"]?.InnerText);
                    String s2 = Lib.get_value_str(xNode["confirm_cutoff"]?.InnerText);
                    s1 = String.IsNullOrEmpty(xNode["screen_cutoff"]?.InnerText)  ? xNode["sd_screen_cutoff"]?.InnerText : s1;
                    s2 = String.IsNullOrEmpty(xNode["confirm_cutoff"]?.InnerText) ? xNode["sd_confirm_cutoff"]?.InnerText : s2;

                    resultDrug.sScreenCutoff = s1;
                    resultDrug.sConfirmCutoff = s2;
                }              
                kq = String.Empty;

                resultDrugs.Add(resultDrug);
            }


            return resultDrugs;
        }
        #endregion End Methods
    }

    public class ResultNotes
    {
        #region Fields
        private String sPatientID = "", sNote = "";
        #endregion End Fields

        #region Constructors
        public ResultNotes()
        {

        }
        public ResultNotes(XmlNode node)
        {
            this.sPatientID = node["Patientid"]?.InnerText;// Lib.get_value_str(node["PatientID"]?.InnerText);
            this.sNote = node["note"]?.InnerText;// Lib.get_value_str(node["Note"]?.InnerText);
        }
       

        public string PatientID
        {
            get
            {
                return sPatientID;
            }

            set
            {
                sPatientID = value;
            }
        }

        public string Note
        {
            get
            {
                return sNote;
            }

            set
            {
                sNote = value;
            }
        }
        #endregion End Constructors

        #region Methods
        public List<ResultNotes> GetNotes(XmlNodeList nodes)
        {
            List<ResultNotes> list = new List<ResultNotes>();
            foreach (XmlNode node in nodes)
            {
                ResultNotes note = new ResultNotes(node);
                list.Add(note);
            }
            return list;
        }

        #endregion End Methods
    }

    public class ReportBinary
    {
        #region Fields
        private String sName = "", sData = "";

        #endregion End Fields

        #region Constructors
        public ReportBinary()
        {

        }

        public ReportBinary(String name, String data)
        {
            this.sName = name;
            this.sData = data;
        }

        public ReportBinary(XmlNode node)
        {
            //this.sName = Lib.get_value_str(node["name"].InnerText);
            this.Data = Lib.get_value_str(node.InnerText);
        }
        public ReportBinary(XmlNode node, String fileName)
        {
            this.sName = Lib.get_value_str(fileName) + ".pdf";
            this.sData = Lib.get_value_str(node.InnerText);
        }

        public string Name
        {
            get
            {
                return sName;
            }

            set
            {
                sName = value;
            }
        }

        public string Data
        {
            get
            {
                return sData;
            }

            set
            {
                sData = value;
            }
        }
        #endregion End Constructors

        #region Methods

        #endregion End Methods
    }
}
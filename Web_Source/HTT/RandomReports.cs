using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace HTT
{
    public class RandomReports
    {
        public static String FONT = "c:/windows/fonts/WINGDING.TTF";
        public static String folder = String.Empty;// @"D:\Web_Source\Web_SFH\Web_SFH\DATA\";
        public static String CssStyle()
        {
            var css = @"table{width:90%;border-collapse:collapse;}";
            css += @"th{font-size:11px;text-align:left;padding:5px 0 1px 2px;}";
            css += @"td{font-family:sans-serif;font-size:8pt;padding:0;border:0;margin:0;}";
            css += @".col{border:solid 0.2px #000; border-bottom:0;border-right:0;padding:2px 0 0 3px;}";
            css += @".colMidle{border:solid 0.2px #000; border-bottom:0; margin-left:2pt;padding:2px 0 0 3px;}";
            css += @".colEnd{border:solid 0.2px #000;border-bottom:0;padding:2px 0 0 3px;}";
            css += @".col2{border:solid 0.2px #000; border-right:0;padding:2px 0 0 3px;}";
            css += ".col2End{border:solid 0.2px #000;padding:2px 0 0 3px;}";
            css += @".col3{border:solid 0.2px #000;border-bottom:0; padding:2px 0 0 3px;}";
            css += @".col3End{border:solid 0.2px #000;padding:2px 0 0 3px;}";
            css += ".colLine{border:solid 0.2px #000;}";
            css += "p{font-size:8pt; color:#000;}";
            css += ".space{width:80%;}";
            css += "td.textTop{vertical-align:top; margin-bottom:18pt;}";
            css += ".page{width:8.3in;height:11.7in;}";
            css += ".numberDonor{padding-right:80px;}";
            css += ".signature{font-family:'Brush Script MT', cursive; font-size:16pt; font-style:italic; text-align:left;padding-left:20pt;}";
            css += ".checkbox{border:solid 1px #000;}";
            return css;
        }
       

        public static void pdfContentByte(PdfWriter writer, String content, Boolean rotate)
        {
            var bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var bold = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            PdfContentByte pcb;

            //Alias to DirectContent
            pcb = writer.DirectContent;
            float x = rotate ? 400 : 280;
            //Show some text
            pcb.BeginText();
            pcb.SetFontAndSize(bf, 9);
            pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, content, x, 3, 0);
            pcb.EndText();
        }

        public static void pdfCheckBox(PdfWriter writer, List<Specimen> list)
        {
            var bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            PdfContentByte pcb;
            //Row 1
            //work days 1 
            for (int i = 0; i < 7; i++)
            {
                //Alias to DirectContent
                pcb = writer.DirectContent;
                float x = 462;
                float y = 618 + (i * 10);
                //Show some text
                pcb.BeginText();
                pcb.SetFontAndSize(bf, 12);
                pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                pcb.EndText();
            }

            //Gender 1 
            for (int i = 0; i < 2; i++)
            {
                //Alias to DirectContent
                pcb = writer.DirectContent;
                float x = 340 + (i * 40);
                float y = 689;
                //Show some text
                pcb.BeginText();
                pcb.SetFontAndSize(bf, 12);
                pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                pcb.EndText();
            }

            //Observation Required 1
            for (int i = 0; i < 2; i++)
            {
                //Alias to DirectContent
                pcb = writer.DirectContent;
                float x = 10;
                float y = 520 + (i * 20);
                //Show some text
                pcb.BeginText();
                pcb.SetFontAndSize(bf, 12);
                pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                pcb.EndText();
            }
            //ALternate
            if (list[0].IsAlternate)
            {
                //Alias to DirectContent
                pcb = writer.DirectContent;
                float x = 230;
                float y = 684;
                //Show some text
                pcb.BeginText();
                pcb.SetFontAndSize(bf, 11);
                pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "\u00fe", x, y, 0);
                pcb.EndText();
            }
            //Row 2
            if (list.Count.Equals(2))
            {
                //work days 2 
                for (int i = 0; i < 7; i++)
                {
                    //Alias to DirectContent
                    pcb = writer.DirectContent;
                    float x = 462;
                    float y = 342 + (i * 10);
                    //Show some text
                    pcb.BeginText();
                    pcb.SetFontAndSize(bf, 12);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                    pcb.EndText();
                }

                //Gender 2
                for (int i = 0; i < 2; i++)
                {
                    //Alias to DirectContent
                    pcb = writer.DirectContent;
                    float x = 340 + (i * 40);
                    float y = 412;
                    //Show some text
                    pcb.BeginText();
                    pcb.SetFontAndSize(bf, 12);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                    pcb.EndText();
                }
                //Observation Required 2
                for (int i = 0; i < 2; i++)
                {
                    //Alias to DirectContent
                    pcb = writer.DirectContent;
                    float x = 10;
                    float y = 244 + (20 * i);
                    //Show some text
                    pcb.BeginText();
                    pcb.SetFontAndSize(bf, 12);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "o", x, y, 0);
                    pcb.EndText();
                }
                if (list[1].IsAlternate)
                {
                    //Alias to DirectContent
                    pcb = writer.DirectContent;
                    float x = 230;
                    float y = 408;
                    //Show some text
                    pcb.BeginText();
                    pcb.SetFontAndSize(bf, 12);
                    pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "\u00fe", x, y, 0);
                    pcb.EndText();

                }
            }
        }

        #region 1 Report NotificationLetters
        public static String NotificationLetters(Specimen specimen, CompanyInfo com)
        {
            String str = String.Empty;
            try
            {
                //Console.WriteLine(JsonConvert.SerializeObject(specimen));
                //Console.WriteLine(JsonConvert.SerializeObject(com));
                List<People> peoples = People.PeopleGets(RandomReports.folder + FieldKeys.PeopleClass + @"\");

                String[] ids = specimen.DonorID.Split('_');

                People people = peoples.Single(x => x.ID.Equals(ids[1]));

                String value = String.Empty;

                str += "<div style='width:100%; heigth:100%;'>";
                str += "<p><b>Controlled Substance and / or Alcohol Test Notification</b></p>";
                str += "<p class='space' style='font-size:13pt'>&nbsp;</p>";
                str += "<table style='width:98%;'><tr><td class='colLine'><b>You've Been Randomly Selected for Testing</b></td></tr></table>";
                str += "<p class='space' style='font-size:20pt;'>&nbsp;</p>";
                str += "<table style='width:98%;'>";
                str += "<tr>";
                str += "<td class='textTop' style='width:40%'>";
                str += "<p><u>Employee</u></p>";
                str += "<p>";
                //Last name
                value = Lib.get_value_str(people.PersonalInfo.Person.LastName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += ", ";
                //First name
                value = Lib.get_value_str(people.PersonalInfo.Person.FirstName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                //PrimaryID
                value = Lib.get_value_str(people.Driver.PrimaryID);
                value = value.Equals("0") ? "&nbsp;" : "&nbsp;&nbsp;&nbsp;[" + value + "]";
                str += value;
                str += specimen.IsAlternate ? "<br/>(Selected as Alternate)" : "";
                str += "</p>";
                str += "</td>";
                str += "<td class='textTop' style='width:60%'>";
                str += "<p><u>Type of Test(s)</u></p>";
                str += "<p>";
                //str += "Controlled Substance / Breath Alcohol Test";
                //str += "Controlled Substance /";
                String sp1 = Lib.get_value_str(specimen.Specimen1);

                String sp2 = Lib.get_value_str(specimen.Specimen2);

                String sp = sp1.Equals("0") ? "" : sp1;

                sp += (!sp1.Equals("0") && !sp2.Equals("0")) ? " / " : "";

                sp += sp2.Equals("0") ? "" : "Breath " + sp2 + " Test";

                str += sp;
                str += "</p>";
                str += "</td>";
                str += "</tr>";
                str += "<tr>";
                str += "<td class='textTop' style='width:40%'>";
                //company name
                str += "<p><u>Company</u></p>";
                str += "<p>";
                value = Lib.get_value_str(com.CompanyName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                //Address
                str += "<p>";
                value = Lib.get_value_str(com.PersonalInfo.Address.Address);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                //City
                str += "<p>";
                value = Lib.get_value_str(com.PersonalInfo.Address.City);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + ", ";
                //State
                value = Lib.get_value_str(com.PersonalInfo.Address.State);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + "&nbsp;&nbsp;";

                //State
                value = Lib.get_value_str(com.PersonalInfo.Address.Zip.ToString());
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";

                //DER 
                str += "<p>";
                //lastname
                value = Lib.get_value_str(com.PersonalInfo.Person.LastName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + "&nbsp;";
                //firstname
                value = Lib.get_value_str(com.PersonalInfo.Person.FirstName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + "&nbsp;&nbsp;&nbsp;";
                //mobilephone
                value = Lib.get_value_str(com.PersonalInfo.Contact.MobilePhone);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value.Set_Tel();

                str += "</p>";

                str += "</td>";
                str += "<td class='textTop' style='width:60%'>";
                str += "<p><u>Location of Testing Facility</u></p>";
                str += "<p>";
                str += "SF HEALTH CLINIC LAI CHIROPRACTIC CORPORATION";
                str += "</p>";
                str += "<p>";
                str += "8818 GARVEY AVE, SUITE A";
                str += "</p>";
                str += "<p>";
                str += "ROSEMEAD, CA 91770";
                str += "</p>";
                str += "<p>";
                str += "213 - 268 - 6108";
                str += "</p>";
                str += "</td>";
                str += "</tr>";
                str += "</table>";
                //space
                str += "<p class='space' style='font-size:20pt;'>&nbsp;</p>";
                str += "<p><u>49 CFR 382.113-NOTIFICATION REQUIREMENTS</u></p>";
                str += "<p>Before Performing an Alcohol or Controlled Substance test under this part, each motor carrier shall notify a driver that</p>";
                str += "<p>the alcohol and/or controlled substance test is required by this part.</p>";
                //space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                str += "<p><u>Compliance is Mandatory</u></p>";
                str += "<p>You are hereby notified that you must submit to the above listed test(s) in compliance with the Federal Motor Carrier Safety</p>";
                str += "<p>Regulations. Pursant to those regulations, YOU MUST PROCEED DIRECTLY AND IMMEDIATELY to the testing site listed above.</p>";

                //space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                str += "<p><u>SPECIAL INSTRUCTIONS TO DRIVER:</u></p>";
                str += "<p>Present this notice with your photo I.D. to clinic staff and please cooperate with clinic staff at all times.</p>";
                //space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                str += "<p><u>SPECIAL INSTRUCTIONS TO COMPANY</u></p>";
                str += "<p>If driver fails to comply with 49 CFR 382, notifiy your program administrator for instructions.</p>";

                //space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                str += "<p><u>Declaration of Agreement</u></p>";
                str += "<p>I understand, as a condition of my membership, complaince with the above scheduled test(s) is required.</p>";

                //space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                str += "<table style='width:98%;'>";
                str += "<tr>";
                str += "<td style='width:50%; height:13pt;'>";
                str += "</td>";
                str += "<td style='width:50%;'>";
                str += "</td>";
                str += "</tr>";
                //Expected Testing Date
                str += "<tr>";
                str += "<td style='width:50%; height:13pt;' >";
                str += "</td>";
                str += "<td style='width:50%;' class='textTop'>";
                str += "<u>";
                int i = 0;
                for (i = 0; i < 60; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Expected Testing Date</p>";
                str += "</td>";
                str += "</tr>";

                //Employee Signature 
                str += "<tr style='height:60pt;'>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 100; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Employee Signature</p>";
                str += "</td>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 50; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Date/Time Notified</p>";
                str += "</td>";
                str += "</tr>";

                //Company Representative Signature 
                str += "<tr style='height:40pt;'>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 100; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Company Representative Signature </p>";
                str += "</td>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 50; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Date/Time Notified</p>";
                str += "</td>";
                str += "</tr>";

                //This area is reserved for clinic use only . .
                str += "<tr style='height:40pt;'>";
                str += "<td colspan='2'>";
                str += "<u>";
                for (i = 0; i < 254; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>This area is reserved for clinic use only...</p>";
                str += "</td>";
                str += "</tr>";

                //ID Presented
                str += "<tr style='height:30pt;'>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 100; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>ID Presented</p>";
                str += "</td>";
                str += "<td style='width:50%;'>";
                str += "<u>";
                for (i = 0; i < 50; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>Date Tested</p>";
                str += "</td>";
                str += "</tr>";

                //line 1
                str += "<tr style='height:25pt;'>";
                str += "<td colspan='2'>";
                str += "<u>";
                for (i = 0; i < 200; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "</td>";
                str += "</tr>";

                //line 2
                str += "<tr style='height:35pt;'>";
                str += "<td colspan='2'>";
                str += "<u>";
                for (i = 0; i < 200; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "</td>";
                str += "</tr>";

                //line 3
                str += "<tr style='height:35pt;'>";
                str += "<td colspan='2'>";
                str += "<u>";
                for (i = 0; i < 200; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "<p>";
                str += "Clinic Notes";
                str += "</p>";
                str += "</td>";
                str += "</tr>";

                str += "</table>";

                //space
                //str += "<p class='space' style='font-size:130pt;'>&nbsp;</p>";
                str += "</div>";
            }
            catch (Exception ex)
            {
              Lib.writerLog("RandomReports","Notification Letters", ex.Message,"error");
            }
            return str;
        }
        /// <summary>
        /// Report 1
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="companies"></param>
        /// <param name="ids"></param>
        public static Boolean PdfCreatedNotificationLetter(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            Boolean kq = false;
            
            try
            {
                String error = String.Empty;
                var filePath = "";// @"D:\NotificationLetters_" + ids[0] + "_" + ids[1] + ".pdf";
                filePath = new FilePath(FieldKeys.ReportClass).Folder + "NotificationLetters_" + ids[0] + "_" + ids[1];
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 30, 10))
                    {

                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Notification letter");
                            doc.AddAuthor("SFHCAdmin");
                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            // Boolean kq = false;
                            int maxLength = companies.Count;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {
                                CompanyInfo com = companies[i];
                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> specimens = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));

                                specimens = specimens.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));

                                for (int k = 0; k < specimens.Count; k++)
                                {
                                    doc.NewPage();
                                    Specimen specimen = specimens[k];

                                    var com1 = companies.Single(x => x.CompanyID.Equals(Lib.get_value_int(specimen.CompanyID)));

                                    html = NotificationLetters(specimen, com1);

                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {

                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);

                                        }
                                    }
                                    count++;
                                    int pageNumber = count;
                                    //Add page number
                                    pdfContentByte(writer, "- " + pageNumber + " -", false);

                                    //Console.WriteLine("Notification Letter Pdf {0}", pageNumber);


                                }

                                if (i == (maxLength - 1))
                                    kq = true;                               
                            }
                            
                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                filePath += ".pdf";
                Lib.WriteFilePDF(ref error, filePath, bytes);
                //System.IO.File.WriteAllBytes(testFile, bytes);

                //Created pdf by company
                RandomReports.PdfCreatedNotificationLetterByCompany(schedule, companies, ids);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Pdf Created Notification Letter{0}", ex.Message);
                Lib.writerLog("RandomReports", "Created Notification Letters 1", ex.Message, "error");
            }
            return kq;
        }

        public static Boolean PdfCreatedNotificationLetter(Schedules schedule, CompanyInfo com, String[] ids)
        {
            Boolean kq = false;

            try
            {
                String error = String.Empty;
                var filePath = "";// @"D:\NotificationLetters_" + ids[0] + "_" + ids[1] + ".pdf";
                filePath = new FilePath(FieldKeys.ReportClass).Folder + "NotificationLetters_" + ids[0] + "_" + ids[1];
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 30, 10))
                    {

                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Notification letter");
                            doc.AddAuthor("SFHCAdmin");
                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            // Boolean kq = false;
                            int maxLength = 1;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {
                               
                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> specimens = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));

                                specimens = specimens.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));

                                for (int k = 0; k < specimens.Count; k++)
                                {
                                    doc.NewPage();
                                    Specimen specimen = specimens[k];                                    

                                    html = NotificationLetters(specimen, com);

                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {

                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);

                                        }
                                    }
                                    count++;
                                    int pageNumber = count;
                                    //Add page number
                                    pdfContentByte(writer, "- " + pageNumber + " -", false);

                                    //Console.WriteLine("Notification Letter Pdf {0}", pageNumber);


                                }

                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                filePath += "_" + com.CompanyID + ".pdf";
                Lib.WriteFilePDF(ref error, filePath, bytes);
                //System.IO.File.WriteAllBytes(testFile, bytes);                
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Pdf Created Notification Letter{0}", ex.Message);
                Lib.writerLog("RandomReports", "Created Notification Letters 2", ex.Message, "error");
            }
            return kq;
        }

        public static void PdfCreatedNotificationLetterByCompany(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            if (schedule.Type.Equals(1))
            {
                foreach (var com in companies)
                {
                    RandomReports.PdfCreatedNotificationLetter(schedule, com, ids);
                }
            }
        }
        #endregion End Report 1

        #region 2 Report RandomSummary
        public static String RandomSummary(Schedules schedule, CompanyInfo com, String[] ids)
        {
            String str = String.Empty;
            try
            {
                String value = String.Empty;
                String selectionId = ids[0] + "_" + ids[1];
                int position = Lib.get_value_int(ids[1]);
                Schedule_Selections selection = schedule.Selections[position];// schedule.Selections.Single(x => x.ID.Equals(selectionId));
                DateTime d = selection.RunOn;
                List<Specimen> list = selection.DonorSpecimenList.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                List<Specimen> list2 = selection.DonorSpecimenList;
                str += "<div style='width:100%; height:100%;'>";
                str += "<table style='width:98%;'>";
                str += "<tr>";
                str += "<td class='textTop' style='width:12pt'>";
                str += "<p>To:</p>";
                str += "</td>";
                str += "<td class='textTop'>";
                //Email
                value = Lib.get_value_str(com.PersonalInfo.Contact.Email);
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += "<p>" + value + "</p>";
                //Company name
                value = Lib.get_value_str(com.CompanyName);
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += "<p>" + value + "</p>";
                //Address
                value = Lib.get_value_str(com.PersonalInfo.Address.Address);
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += "<p>" + value + "</p>";
                //City
                value = Lib.get_value_str(com.PersonalInfo.Address.City);
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += "<p>" + value;
                str += ", ";
                //City
                value = Lib.get_value_str(com.PersonalInfo.Address.State);
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += value;

                //City
                value = Lib.get_value_str(com.PersonalInfo.Address.Zip.ToString());
                value = value.Equals("0") ? "&nbsp;" : value.ToUpper();
                str += "&nbsp;" + value;
                str += "</p>";
                str += "</td>";
                str += "</tr>";
                str += "</table>";
                //Space
                str += "<p class='space' style='font-size:60pt'>&nbsp;</p>";
                //Random Selection Summary
                str += "<table style='width:98%;'><tr><td class='colLine'><b>Random Selection Summary</b></td></tr></table>";
                //Space
                str += "<p class='space' style='font-size:20pt'>&nbsp;</p>";
                //Random Selection Generation Date / Time / Generator ID
                str += "<p><u>Random Selection Generation Date / Time / Generator ID</u></p>";
                //Date: 08-14-2021 / Time: 22:43:14 / Gen ID: 304
                str += "<p>Date: " + d.ToString("MM-dd-yyyy") + " / Time: " + d.ToString("HH:mm:ss") + " / Gen ID: " + selectionId + "</p>";
                //Space
                str += "<p class='space' style='font-size:20pt'>&nbsp;</p>";
                //Eligibility Count
                str += "<p><u>Eligibility Count</u></p>";
                //numberdor Persons Eligible for Selection
                int number = list2.Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Persons Eligible for Selection</p>";
                //Space
                str += "<p class='space' style='font-size:10pt'>&nbsp;</p>";
                //Persons Selected for Drug and/or Alcohol Testing
                str += "<p><u>Persons Selected for Drug and/or Alcohol Testing</u></p>";

                number = list.FindAll(x => x.Selected.Equals(true) && !String.IsNullOrEmpty(x.Specimen1) && String.IsNullOrEmpty(x.Specimen2)).Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Number of Persons Selected for Drug Testing Only</p>";

                number = list.FindAll(x => x.Selected.Equals(true) && String.IsNullOrEmpty(x.Specimen1) && !String.IsNullOrEmpty(x.Specimen2)).Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Number of Persons Selected for Alcohol Testing Only</p>";

                number = list.FindAll(x => x.Selected.Equals(true) && !String.IsNullOrEmpty(x.Specimen1) && !String.IsNullOrEmpty(x.Specimen2)).Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Number of Persons Selected for Both</p>";
                //Space
                str += "<p class='space' style='font-size:10pt'>&nbsp;</p>";
                //Total Number of Individuals Selected for Testing      
                number = list.FindAll(x => x.Selected.Equals(true)).Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<u>Total Number of Individuals Selected for Testing </u></p>";
                //Space
                str += "<p class='space' style='font-size:10pt'>&nbsp;</p>";
                // Number of Persons Selected as Alternates     
                number = list.FindAll(x => x.IsAlternate.Equals(true)).Count;
                str += "<p>" + number + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<u>Number of Persons Selected as Alternates</u></p>";
                //Space
                str += "<p class='space' style='font-size:14pt'>&nbsp;</p>";
                //49 CFR 382.113-NOTIFICATION REQUIREMENTS     
                str += "<p><u>49 CFR 382.113-NOTIFICATION REQUIREMENTS</u></p>";
                str += "<p>BEFORE PERFORMING AN ALCOHOL OR CONTROLLED SUBSTANCE TEST UNDER THIS PART, EACH</p>";
                str += "<p>MOTOR CARRIER SHALL NOTIFY A DRIVER THAT THE ALCOHOL AND/OR CONTROLLED SUBSTANCE</p>";
                str += "<p>TEST IS REQUIRED BY THIS PART.</p>";
                //Space
                str += "<p class='space' style='font-size:10pt'>&nbsp;</p>";
                //Compliance Requirements
                str += "<p><u>Compliance Requirements</u></p>";
                str += "<p>Selected persons must report for drug and/or alcohol testing IMMEDIATELY after receipt</p>";
                str += "<p>of this document. Personnel who do not comply in a timely manner will be listed</p>";
                str += "<p>as Refusals. Selected individuals must have a picture ID to be processed.</p>";
                //Space
                str += "<p class='space' style='font-size:10pt'>&nbsp;</p>";
                //Certification of Participation
                str += "<p><u>Certification of Participation</u></p>";
                str += "<p>This document certifies that your company's personnel are subject to computer-generated</p>";
                str += "<p>random selection for drug and/or alcohol testing. File this document as a part of</p>";
                str += "<p>your permanent drug and alcohol testing records.</p>";
                //Space
                str += "<p class='space' style='font-size:20pt'>&nbsp;</p>";
                //signature
                str += "<p class='signature'>Vidian Tran</p>";
                str += "<p><u>";
                for (int i = 0; i < 100; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u></p>";
                str += "<p>Vidian Tran, Program Administrator</p>";

                str += "</div>";
        }
            catch (Exception ex)
            {
                Lib.writerLog("RandomReports", "Random Summary", ex.Message, "error");               
            }
            return str;
        }

        /// <summary>
        /// Report 2
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="companies"></param>
        /// <param name="ids"></param>      
        public static Boolean PdfCreatedRandomSummary(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 30, 10))
                    {

                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Random selection summary");
                            doc.AddAuthor("SFHCAdmin");
                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = companies.Count;
                            for (int i = 0; i < maxLength; i++)
                            {

                                CompanyInfo com = companies.Single(x => x.CompanyID.Equals(companies[i].CompanyID));
                                doc.NewPage();
                                html = RandomSummary(schedule, com, ids);
                                //Lib.writerLog("RandomReports", "Pdf Created Random Summary error",html, "error");
                                using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                {

                                    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                    {

                                        //Parse the HTML
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);


                                    }
                                }
                                //Add page number

                                pdfContentByte(writer, "- " + (i + 1) + " -", false);

                                //Console.WriteLine("Pdf Created Random Summary {0}", i + 1);

                                if (i == (maxLength - 1))
                                    kq = true;
                            }
                            doc.Close();

                            //if (kq)
                              //  Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.
                var testFile = @"D:\RandomSummary_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "RandomSummary_" + ids[0] + "_" + ids[1] + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);

                //Created pdf by company
                RandomReports.PdfCreatedRandomSummaryByCompany(schedule, companies, ids);
            }
            catch (Exception ex)
            {
                Lib.writerLog("RandomReports", "Pdf Created Random Summary error", ex.Message, "error");
            }
            return kq;
        }

        public static Boolean PdfCreatedRandomSummary(Schedules schedule, CompanyInfo com, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 30, 10))
                    {

                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Random selection summary");
                            doc.AddAuthor("SFHCAdmin");
                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = 1;
                            for (int i = 0; i < maxLength; i++)
                            {
                               
                                doc.NewPage();
                                html = RandomSummary(schedule, com, ids);
                                //Lib.writerLog("RandomReports", "Pdf Created Random Summary error",html, "error");
                                using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                {

                                    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                    {

                                        //Parse the HTML
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);


                                    }
                                }
                                //Add page number

                                pdfContentByte(writer, "- " + (i + 1) + " -", false);

                                //Console.WriteLine("Pdf Created Random Summary {0}", i + 1);

                                if (i == (maxLength - 1))
                                    kq = true;
                            }
                            doc.Close();

                            //if (kq)
                            //  Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.
                var testFile = @"D:\RandomSummary_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "RandomSummary_" + ids[0] + "_" + ids[1] + "_" + com.CompanyID + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);
            }
            catch (Exception ex)
            {
                Lib.writerLog("RandomReports", "Pdf Created Random Summary error 2", ex.Message, "error");
            }
            return kq;
        }

        public static void PdfCreatedRandomSummaryByCompany(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            if (schedule.Type.Equals(1))
            {
                foreach (var com in companies)
                {
                    RandomReports.PdfCreatedRandomSummary(schedule, com, ids);
                }
            }
        }

        #endregion End report 2

        #region 3 Report RandomList
        public static String RandomList(Schedules schedule, CompanyInfo com, int postion, int type)
        {
            String str = String.Empty;
            String value = String.Empty;
            try
            {
                Schedule_Selections selections = schedule.Selections[postion];

                List<Specimen> list = selections.DonorSpecimenList.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));

                int i = 0;
                str += "<div style='width:100%;height:100%;'>";


                //Random Selection
                str += "<p>";
                str += "<u>";
                str += "<b>Random Selection</b>";
                for (i = 0; i < 337; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "</p>";
                //Space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                //table
                str += "<table style='width:72%'>";
                str += "<tr>";
                str += "<td style='width:30%' class='textTop' rowspan='3'>";
                //Company name
                value = Lib.get_value_str(com.CompanyName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                //Address
                value = Lib.get_value_str(com.PersonalInfo.Address.Address);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                //City
                value = Lib.get_value_str(com.PersonalInfo.Address.City);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + ", ";
                //State
                value = Lib.get_value_str(com.PersonalInfo.Address.State);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + "&nbsp;";
                //Zip
                value = Lib.get_value_str(com.PersonalInfo.Address.Zip.ToString());
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                //Primary Contact
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Primary Contact</u></p>";
                //lastname
                value = Lib.get_value_str(com.PersonalInfo.Person.LastName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "&nbsp;";
                //firstname
                value = Lib.get_value_str(com.PersonalInfo.Person.FirstName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                //Contact Email
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Contact Email</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.Email);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                //Consortium
                str += "<td style='width:20%' class='textTop'>";
                str += "<p><u>ConsortiumID</u></p>";
                value = Lib.get_value_str(schedule.ConsortiumID.ToString());
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                //Phone 
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Phone</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.MobilePhone);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value.Set_Tel() + "</p>";
                str += "</td>";
                //Website
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Web Site</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.Website);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                //List File ID
                str += "<td style='width:20%' class='textTop'>";
                str += "<p><u>List File ID</u></p>";
                str += "<p>";
                value = Lib.get_value_str(schedule.ID);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                //Fax
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Fax</u></p>";
                str += "<p></p>";
                str += "</td>";
                //Random Selection Generation Date / Time / Generator ID
                str += "<td style='width:45%' colspan='2' class='textTop'>";
                str += "<p><u>Random Selection Generation Date / Time / Generator ID</u></p>";
                str += "<p>Date: " + selections.RunOn.ToString("MM-dd-yyyy");
                str += " / Time: " + selections.RunOn.ToString("HH:mm:ss");
                str += " / Gen ID: " + selections.ID + "</p>";
                str += "</td>";
                str += "</tr>";
                str += "</table>";

                //Space
                str += "<p class='space' style='font-size:20pt;'>&nbsp;</p>";
                //Company name
                value = Lib.get_value_str(com.CompanyName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p><Strong>" + value + "</strong></p>";
                //Space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                //Note: An Asterisk [*] Indicates Alternate
                str += "<p>Note: An Asterisk [*] Indicates Alternate</p>";
                //Space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";

                str += HtmlTableRandomList(list, schedule.Started, type);

                //str += "<p style='position: fixed;bottom:1pt;'>- " + (numberPage + 1) + " -</p>";
                str += "</div>";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                Lib.writerLog("RandomReports", "Random list", ex.Message, "error");
            }
            return str;
        }

        static String HtmlTableRandomList(List<Specimen> list, DateTime d, int type)
        {
            //Get People
            List<People> donors = People.PeopleGets(RandomReports.folder + FieldKeys.PeopleClass + @"\");

            var str = String.Empty;
            try
            {
                int i = 0;
                String date = d.ToString("MM-dd-yyyy");
                list = list.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                switch (type)
                {
                    case 0:
                        list = list.FindAll(x => x.IsAlternate.Equals(true));
                        break;
                    case 1:
                        list = list.FindAll(x => x.Selected.Equals(true));
                        break;
                }
                int maxLength = list.Count;

                str += "<table style='float:left; width:75%;'>";
                str += "<tr>";
                str += "<td style='width:65%;' >";
                str += "<p style='text-align:right;'>";
                str += "&#60;------- Dates -------&#62;";
                str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>";
                str += "</td>";
                str += "<td style='width:35%;'>";
                str += "<p>&nbsp;&nbsp;Obsrv</p>";
                str += "</td>";
                str += "</tr>";
                str += "</table>";


                str += "<table width:82%;'>";
                str += "<tr>";
                str += "<th style='width:4%'>&nbsp;</th>";
                str += "<th style='width:2%;  text-align:center;'>*</th>";
                str += "<th style='width:14%'>Last Name</th>";
                str += "<th style='width:14%'>First Name</th>";
                str += "<th style='width:3%;  text-align:center;'>M.i.</th>";
                str += "<th style='width:10%; text-align:center;'>ID No.</th>";
                str += "<th style='width:6%;  text-align:center;'>Selection</th>";
                str += "<th style='width:8%;  text-align:center;'>Test</th>";

                str += "<th></th>";
                str += "<th style='width:5%;  text-align:center;'>Rq'd</th>";
                str += "<th style='width:5%;  text-align:center;'>Alert! </th>";
                str += "<th style='width:15%; text-align:center;'>Test Type</th>";
                str += "<th style='width:20%; text-align:center;'>Misc.</th>";
                str += "<th style='width:8%;  text-align:center;'>Expiration</th>";
                str += "</tr>";

                //Console.WriteLine("donors: {0}", JsonConvert.SerializeObject(list));
                for (i = 0; i < maxLength; i++)
                {
                    //i = i > maxLength ? maxLength : i;
                    Specimen specimen = list[i];
                    String[] ids = specimen.DonorID.Split('_');
                    People donor = donors.Single(x => x.ID.Equals(ids[1]));
                    // Console.WriteLine("donor: {0}", JsonConvert.SerializeObject(donor));

                    String lastName = donor.PersonalInfo.Person.LastName;

                    String firstName = donor.PersonalInfo.Person.FirstName;

                    String primaryID = donor.Driver.PrimaryID;

                    String tel = donor.PersonalInfo.Contact.MobilePhone;

                    String sp1 = Lib.get_value_str(specimen.Specimen1);

                    String sp2 = Lib.get_value_str(specimen.Specimen2);

                    String sp = sp1.Equals("0") ? "" : sp1;

                    sp += (!sp1.Equals("0") && !sp2.Equals("0")) ? " / " : "";

                    sp += sp2.Equals("0") ? "" : sp2;

                    string col = i.Equals(maxLength - 1) ? "col2" : "col";

                    string colEnd = i.Equals(maxLength - 1) ? "col2End" : "colEnd";
                    str += "<tr>";
                    str += "<td class='" + col + "' style='width:4%'>" + (i + 1) + "</td>";
                    str += "<td class='" + col + "' style='width:2%; text-align:center; vertical-align:central;'>";
                    str += (specimen.IsAlternate ? "*" : "&nbsp;");
                    str += "</td>";
                    str += "<td class='" + col + "' style='width:14%'>" + lastName + "</td>";
                    str += "<td class='" + col + "' style='width:14%'>" + firstName + "</td>";
                    str += "<td class='" + col + "' style='width:3%'>&nbsp;</td>";
                    str += "<td class='" + col + "' style='width:10%'>" + primaryID + "</td>";
                    str += "<td class='" + col + "' style='width:6%'>" + date + "</td>";
                    str += "<td class='" + colEnd + "' style='width:8%'>&nbsp;</td>";

                    str += "<td style='font-size:4pt'>&nbsp;</td>";

                    str += "<td class='" + col + "' style='width:5%'>&nbsp;</td>";
                    str += "<td class='" + col + "' style='width:5%'>&nbsp;</td>";
                    str += "<td class='" + col + "' style='width:15%'>" + sp + "</td>";
                    str += "<td class='" + col + "' style='width:20%'>" + tel.Set_Tel() + "</td>";
                    str += "<td class='" + colEnd + "' style='width:8%'>&nbsp;</td>";
                    str += "</tr>";
                    // i++;

                }

                str += "</table>";
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Random list error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "Html Random list", ex.Message, "error");
            }
            return str;
        }
        /// <summary>
        /// Report 3
        /// </summary>
        /// <param name="schedule">Schedules</param>
        /// <param name="companies">Company list</param>
        /// <param name="ids"></param>
        public static Boolean PdfCreatedRandomList(Schedules schedule, List<CompanyInfo> companies, String[] ids, int type)
        {
            Boolean kq = false;
            try
            {
                String typeName = "Both";
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 15, 10))
                    {
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Random selection list");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = companies.Count;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {

                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> list = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.CompanyID.Equals(companies[i].CompanyID.ToString()));
                                switch (type)
                                {
                                    case 0:
                                        list = list.FindAll(x => x.IsAlternate.Equals(true));
                                        typeName = "Alternate";
                                        break;
                                    case 1:
                                        list = list.FindAll(x => x.Selected.Equals(true));
                                        typeName = "Selection";
                                        break;
                                }
                                if (list.Count > 0)
                                {
                                    //Add page number
                                    doc.NewPage();
                                    html = RandomList(schedule, companies[i], position, type);
                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {
                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                        }
                                    }
                                    count++;
                                    pdfContentByte(writer, "- " + (count) + " -", true);
                                    //Console.WriteLine("Random List Pdf {0}", count);
                                }
                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.
                //var testFile = @"D:\RandomList" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
                var testFile = "";// @"D:\RandomList_" + ids[0] + "_" + ids[1];
                //testFile += "_" + typeName + ".pdf";

                testFile = new FilePath(FieldKeys.ReportClass).Folder + "RandomList_" + ids[0] + "_" + ids[1];
                testFile += "_" + typeName + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);

                //Created pdf file by company
                RandomReports.PdfCreatedRandomListByCompany(schedule, companies, ids, type);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("PdfCreatedRandomList Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "PdfCreatedRandomList 1", ex.Message, "error");

            }
            return kq;
        }

        public static Boolean PdfCreatedRandomList(Schedules schedule, CompanyInfo com, String[] ids, int type)
        {
            Boolean kq = false;
            try
            {
                String typeName = "Both";
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 15, 10))
                    {
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Random selection list");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = 1;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {

                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> list = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                                switch (type)
                                {
                                    case 0:
                                        list = list.FindAll(x => x.IsAlternate.Equals(true));
                                        typeName = "Alternate";
                                        break;
                                    case 1:
                                        list = list.FindAll(x => x.Selected.Equals(true));
                                        typeName = "Selection";
                                        break;
                                }
                                if (list.Count > 0)
                                {
                                    //Add page number
                                    doc.NewPage();
                                    html = RandomList(schedule, com, position, type);
                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {
                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                        }
                                    }
                                    count++;
                                    pdfContentByte(writer, "- " + (count) + " -", true);
                                    //Console.WriteLine("Random List Pdf {0}", count);
                                }
                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.
                //var testFile = @"D:\RandomList" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
                var testFile = "";// @"D:\RandomList_" + ids[0] + "_" + ids[1];
                //testFile += "_" + typeName + ".pdf";

                testFile = new FilePath(FieldKeys.ReportClass).Folder + "RandomList_" + ids[0] + "_" + ids[1];
                testFile += "_" + com.CompanyID + "_" + typeName + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("PdfCreatedRandomList Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "PdfCreatedRandomList 2", ex.Message, "error");

            }
            return kq;
        }


        public static void PdfCreatedRandomListByCompany(Schedules schedule, List<CompanyInfo> companies, String[] ids, int type)
        {
            if (schedule.Type.Equals(1))
            {
                foreach (var com in companies)
                {
                    RandomReports.PdfCreatedRandomList(schedule, com, ids, type);
                }
            }
        }
        #endregion End report 3

        #region 5 Report Notificationslip
        public static String Notificationslip(Schedules schedule, CompanyInfo com, int postion, List<Specimen> list, int orderNumber)
        {
            String str = String.Empty;
            String value = String.Empty;
            try
            {
                Schedule_Selections selections = schedule.Selections[postion];

                //List<Specimen> list = selections.DonorSpecimenList.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));

                int i = 0;
                str += "<div style='width:100%;height:100%;'>";


                //Random Selection
                str += "<p>";
                str += "<u>";
                str += "<b>Random Selection</b>";
                for (i = 0; i < 227; i++)
                {
                    str += "&nbsp;";
                }
                str += "</u>";
                str += "</p>";
                //Space
                str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
                //table
                str += "<table style='width:100%'>";
                str += "<tr>";
                str += "<td style='width:30%' class='textTop' rowspan='3'>";
                //Company name
                value = Lib.get_value_str(com.CompanyName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                //Address
                value = Lib.get_value_str(com.PersonalInfo.Address.Address);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                //City
                value = Lib.get_value_str(com.PersonalInfo.Address.City);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + ", ";
                //State
                value = Lib.get_value_str(com.PersonalInfo.Address.State);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value + "&nbsp;";
                //Zip
                value = Lib.get_value_str(com.PersonalInfo.Address.Zip.ToString());
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                //Primary Contact
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Primary Contact</u></p>";
                //lastname
                value = Lib.get_value_str(com.PersonalInfo.Person.LastName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "&nbsp;";
                //firstname
                value = Lib.get_value_str(com.PersonalInfo.Person.FirstName);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                //Contact Email
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Contact Email</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.Email);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                //Consortium
                str += "<td style='width:20%' class='textTop'>";
                str += "<p><u>ConsortiumID</u></p>";
                value = Lib.get_value_str(schedule.ConsortiumID.ToString());
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                //Phone 
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Phone</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.MobilePhone);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value.Set_Tel() + "</p>";
                str += "</td>";
                //Website
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Web Site</u></p>";
                value = Lib.get_value_str(com.PersonalInfo.Contact.Website);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += "<p>" + value + "</p>";
                str += "</td>";
                //List File ID
                str += "<td style='width:20%' class='textTop'>";
                str += "<p><u>List File ID</u></p>";
                str += "<p>";
                value = Lib.get_value_str(schedule.ID);
                value = value.Equals("0") ? "&nbsp;" : value;
                str += value;
                str += "</p>";
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                //Fax
                str += "<td style='width:25%' class='textTop'>";
                str += "<p><u>Fax</u></p>";
                str += "<p></p>";
                str += "</td>";
                //Random Selection Generation Date / Time / Generator ID
                str += "<td style='width:45%' colspan='2' class='textTop'>";
                str += "<p><u>Random Selection Generation Date / Time / Generator ID</u></p>";
                str += "<p>Date: " + selections.RunOn.ToString("MM-dd-yyyy");
                str += " / Time: " + selections.RunOn.ToString("HH:mm:ss");
                str += " / Gen ID: " + selections.ID + "</p>";
                str += "</td>";
                str += "</tr>";
                str += "</table>";
                i = 0;
                foreach (var specimen in list)
                {
                    orderNumber = (orderNumber * 2) + i;
                    str += NotificationSlipTable(specimen, selections, com, orderNumber);
                    i++;
                }


                str += "</div>";
            }
            catch (Exception ex)
            {
                // Console.WriteLine("{0}", ex.Message);
                Lib.writerLog("RandomReports", "Notificationslip", ex.Message, "error");
            }
            return str;

        }

        public static String NotificationSlipTable(Specimen specimen, Schedule_Selections selection, CompanyInfo com, int orderNumber)
        {
            //Console.WriteLine("Specimen: {0}", JsonConvert.SerializeObject(specimen));
            String str = String.Empty;
            String[] ids = specimen.DonorID.Split('_');
            var donors = People.PeopleGets(folder + FieldKeys.PeopleClass + @"\");
            var donor = donors.Single(x => x.ID.Equals(ids[1]));
            String value = String.Empty;
            //Space
            str += "<p class='space' style='font-size:20pt'>";
            str += "<u>";
            for (int i = 0; i < 103; i++)
            {
                str += "&nbsp;";
            }
            str += "</u>";
            str += "</p>";
            //Space
            str += "<p class='space' style='font-size:3pt'>&nbsp;</p>";

            str += "<table style='width:100%;'>";
            str += "<tr>";
            //Row 1
            //column 1
            str += "<td class='textTop' style='width:38%;' colspan='2' rowspan='3'>";
            //Space
            //str += "<p class='space' style='font-size:20pt'></p>";
            //Donor name
            str += "<p>";
            str += "<strong>";
            value = Lib.get_value_str(donor.PersonalInfo.Person.LastName);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += ", ";
            value = Lib.get_value_str(donor.PersonalInfo.Person.LastName);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</strong>";
            str += "</p>";
            str += "</td>";
            //column 2
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 3
            str += "<td class='textTop' style='width:19%;'>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            str += specimen.IsAlternate ? "<b>Alternate</b>" : "";
            str += "</td>";
            //column 4
            str += "<td class='textTop' style='width:19%;'>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Male";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Female";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 5
            str += "<td class='textTop' style='width:2%'>";
            str += "&nbsp;";
            str += "</td>";
            //column 6
            str += "<td class='textTop' style='width:22%;' rowspan='4'>";
            str += "<p>";
            str += "<u>";
            str += "Work Days";
            str += "</u>";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:4pt;'>&nbsp;</p>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tuesday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Wednesday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thursday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Friday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Saturday<br/>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sunday<br/>";
            str += "</td>";
            str += "</tr>";
            //Row 2
            str += "<tr style='heigth:20pt;'>";
            //column 1
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 2
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 3
            str += "<td class='textTop' style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Employee ID";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(donor.Driver.PrimaryID);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4
            str += "<td class='textTop' style='width:19%'>";//
            str += "<p>";
            str += "<u>";
            str += "Alternate ID";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(donor.Driver.AlternateID);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 5
            str += "<td class='textTop' style='width:5%'>";
            str += "</td>";
            //column 6
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";


            //Row 3
            str += "<tr>";
            //column 1
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 2
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 3 Driver's License
            str += "<td style='width:19%'>";//
            str += "<p>";
            str += "<u>";
            str += "Driver's License";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4 Expiration Date
            str += "<td style='width:19%'>";//
            str += "<p>";
            str += "<u>";
            str += "Expiration Date";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 5
            str += "<td style='width:2%'>";
            str += "</td>";
            //column 6
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";

            //Row 4
            str += "<tr>";
            //column 1 Test Date
            str += "<td style='width:19%'>";//
            str += "<p>";
            str += "<u>";
            str += "Test Date";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 2 Origination Date
            str += "<td style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Origination Date";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = selection.RunOn.ToString("MM-dd-yyyy");
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 3 Work Phone
            str += "<td style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Work Phone";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(donor.PersonalInfo.Contact.MobilePhone);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value.Set_Tel();
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4
            str += "<td style='width:19%'>";
            str += "</td>";
            //column 5
            str += "<td style='width:2%'>";
            str += "</td>";
            //column 6
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";

            //Row 5
            str += "<tr>";
            //column 1 Test Type
            str += "<td style='width:19%' colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Test Type";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            String sp1 = Lib.get_value_str(specimen.Specimen1);

            String sp2 = Lib.get_value_str(specimen.Specimen2);

            String sp = sp1.Equals("0") ? "" : sp1;

            sp += (!sp1.Equals("0") && !sp2.Equals("0")) ? " / " : "";

            sp += sp2.Equals("0") ? "" : "BAT";

            str += sp;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 2
            // str += "<td style='width:19%'>";
            // str += "</td>";
            //column 3 Other Phone
            str += "<td style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Other Phone";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(donor.PersonalInfo.Contact.HomePhone);
            value = value.Equals("0") ? Lib.get_value_str(donor.PersonalInfo.Contact.WorkPhone) : value;
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value.Set_Tel();
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4 Shift
            str += "<td style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Shift";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 5  Location
            str += "<td colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Location";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 6 
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";

            //Row 6
            str += "<tr>";
            //column 1 Observation Required
            str += "<td style='width:19%' colspan='2'>";
            str += "<p>";
            //str += "<u>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Observation Required";
            //str += "</u>";
            str += "</p>";
            str += "<br/>";
            str += "<p>";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Alert / Attention";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 2
            //str += "<td style='width:19%'>";
            //str += "</td>";

            //column 3 Email Address
            str += "<td style='width:19%'>";
            str += "<p>";
            str += "<u>";
            str += "Email Address";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(donor.PersonalInfo.Contact.Email);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:15pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4
            str += "<td style='width:19%'>";
            str += "&nbsp;";
            str += "</td>";
            //column 5 Miscellaneous
            str += "<td style='width:5%' colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Miscellaneous";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Contact.MobilePhone);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value.Set_Tel();
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:15pt;'>&nbsp;</p>";
            str += "</td>";
            //column 6
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";

            //Row 7
            str += "<tr>";
            //column 1 Notes
            str += "<td style='width:19%' colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Notes";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:25pt;'>&nbsp;</p>";
            str += "</td>";
            //column 2
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 3 Comments
            str += "<td style='width:19%' colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Comments";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:25pt;'>&nbsp;</p>";
            str += "</td>";
            //column 4
            //str += "<td style='width:19%'>";
            //str += "</td>";
            //column 5 Random ID / Time / Order
            str += "<td style='width:5%' colspan='2'>";
            str += "<p>";
            str += "<u>";
            str += "Random ID / Time / Order";
            str += "</u>";
            str += "</p>";
            str += "<p>";
            str += selection.ID;
            str += " / ";
            str += selection.RunOn.ToString("HH:mm:ss");
            str += " / ";
            str += (orderNumber + 1) + "";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:25pt;'>&nbsp;</p>";
            str += "</td>";
            //column 6
            //str += "<td style='width:19%'>";
            //str += "</td>";
            str += "</tr>";



            str += "</table>";
            return str;
        }

        public static Boolean PdfCreatedNotificationslip(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 15, 10))
                    {
                        //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Notification slip");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = companies.Count;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {

                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> specimens = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.CompanyID.Equals(companies[i].CompanyID.ToString()));
                                specimens = specimens.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                                int k = 0;
                                while (k < specimens.Count)
                                {
                                    //Add page number
                                    doc.NewPage();

                                    List<Specimen> ls = new List<Specimen>();

                                    ls.Add(specimens[k]);
                                    if (k.Equals(specimens.Count))
                                    {
                                        if ((specimens.Count % 2).Equals(0))
                                            ls.Add(specimens[k + 1]);
                                    }
                                    else if (k < (specimens.Count - 1))
                                        ls.Add(specimens[k + 1]);

                                    Boolean towLine = ls.Count.Equals(1) ? false : true;
                                    //Add checkbox
                                    pdfCheckBox(writer, ls);

                                    html = Notificationslip(schedule, companies[i], position, ls, count);
                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {
                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                        }
                                    }
                                    count++;
                                    pdfContentByte(writer, "- " + (count) + " -", false);
                                    //Console.WriteLine("Notification slip Pdf {0}", count);
                                    k++;
                                    k = k + 1;
                                }

                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.

                var testFile = @"D:\Notificationslip_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "Notificationslip_" + ids[0] + "_" + ids[1] + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);

                RandomReports.PdfCreatedNotificationslipByCompany(schedule, companies, ids);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("PdfCreatedRandomList Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "PdfCreatedRandomList", ex.Message, "error");
            }
            return kq;
        }

        public static Boolean PdfCreatedNotificationslip(Schedules schedule, CompanyInfo com, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 15, 10))
                    {
                        //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Notification slip");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            //Boolean kq = false;
                            int maxLength = 1;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {

                                int position = Lib.get_value_int(ids[1]);
                                List<Specimen> specimens = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                                specimens = specimens.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                                int k = 0;
                                while (k < specimens.Count)
                                {
                                    //Add page number
                                    doc.NewPage();

                                    List<Specimen> ls = new List<Specimen>();

                                    ls.Add(specimens[k]);
                                    if (k.Equals(specimens.Count))
                                    {
                                        if ((specimens.Count % 2).Equals(0))
                                            ls.Add(specimens[k + 1]);
                                    }
                                    else if (k < (specimens.Count - 1))
                                        ls.Add(specimens[k + 1]);

                                    Boolean towLine = ls.Count.Equals(1) ? false : true;
                                    //Add checkbox
                                    pdfCheckBox(writer, ls);

                                    html = Notificationslip(schedule, com, position, ls, count);
                                    using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                    {

                                        using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                        {
                                            //Parse the HTML
                                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                        }
                                    }
                                    count++;
                                    pdfContentByte(writer, "- " + (count) + " -", false);
                                    //Console.WriteLine("Notification slip Pdf {0}", count);
                                    k++;
                                    k = k + 1;
                                }

                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.

                var testFile = @"D:\Notificationslip_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "Notificationslip_" + ids[0] + "_" + ids[1] + "_" + com.CompanyID + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("PdfCreatedRandomList Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "PdfCreatedRandomList", ex.Message, "error");
            }
            return kq;
        }

        public static void PdfCreatedNotificationslipByCompany(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            foreach(var com in companies)
            {
                RandomReports.PdfCreatedNotificationslip(schedule, com, ids);
            }
        }

        #endregion End report 5

        #region 6 Base List Maintained for Random selection
        public static String BaseListMaintained(Schedules schedule, CompanyInfo com, int position)
        {
            String str = String.Empty;
            int i = 0;
            String value = String.Empty;
            Schedule_Selections selection = schedule.Selections[position];
            //Base List Maintained for Random Selection
            str += "<div style='width:100%; heigth:100%'>";
            str += "<p>";
            str += "<u>";
            str += "<b>Base List Maintained for Random Selection</b>";
            for (i = 0; i < 183; i++)
            {
                str += "&nbsp;";
            }
            str += "</u>";
            str += "</p>";

            //space
            str += "<p class='space' style='font-size:5pt;'>&nbsp;</p>";
            //company Info
            str += "<table style='width:80%;'>";
            //row 1
            str += "<tr>";
            //column 1
            str += "<td class='textTop'  style='width:40%' rowspan='4'>";
            //Companyname
            str += "<p>";
            value = Lib.get_value_str(com.CompanyName);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //address
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Address.Address);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //City state zip
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Address.City);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += ", ";
            //State
            value = Lib.get_value_str(com.PersonalInfo.Address.State);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "&nbsp;&nbsp;";
            //Zip
            value = Lib.get_value_str(com.PersonalInfo.Address.Zip.ToString());
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            str += "</td>";
            //column 2
            str += "<td class='textTop'  style='width:30%' >";
            //space
            str += "&nbsp;";
            str += "</td>";
            //column 3
            str += "<td class='textTop'  style='width:30%' >";
            //Email
            str += "<p>";
            str += "<u>Contact Email</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Contact.Email);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            str += "</tr>";
            //End row 1

            //row 2
            str += "<tr>";
            //column 1
            //str += "<td>";
            //str += "</td>";
            //column 2
            str += "<td>";
            //Primary Contact
            str += "<p>";
            str += "<u>Primary Contact</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Person.LastName);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "&nbsp;";
            value = Lib.get_value_str(com.PersonalInfo.Person.FirstName);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 3
            str += "<td>";
            //Website
            str += "<p>";
            str += "<u>Website</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Contact.Website);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            str += "</tr>";
            //End row 2

            //row 3
            str += "<tr>";
            //column 1
            //str += "<td>";
            //str += "</td>";
            //column 2
            str += "<td>";
            //Phone
            str += "<p>";
            str += "<u>Phone</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(com.PersonalInfo.Contact.MobilePhone);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value.Set_Tel();
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 3
            str += "<td>";
            //List File ID
            str += "<p>";
            str += "<u>List File ID</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(selection.ID);
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            str += "</tr>";
            //End row 3

            //row 4
            str += "<tr>";
            //column 1
            //str += "<td>";
            // str += "</td>";
            //column 2
            str += "<td>";
            //Fax
            str += "<p>";
            str += "<u>Fax</u>";
            str += "</p>";
            str += "<p>";
            str += "&nbsp;";
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            //column 3
            str += "<td>";
            //ConsortiumID
            str += "<p>";
            str += "<u>ConsortiumID</u>";
            str += "</p>";
            str += "<p>";
            value = Lib.get_value_str(schedule.ConsortiumID.ToString());
            value = value.Equals("0") ? "&nbsp;" : value;
            str += value;
            str += "</p>";
            //space
            str += "<p class='space' style='font-size:10pt;'>&nbsp;</p>";
            str += "</td>";
            str += "</tr>";
            //End row 4

            str += "</table>";

            str += BaseListMaintainedTable(com, selection);

            str += "</div>";
            return str;
        }

        public static String BaseListMaintainedForConsortium(Schedules schedule, List<CompanyInfo> coms, int position)
        {
            String str = String.Empty;

            try
            {
                int i = 0;
                String value = String.Empty;
                Schedule_Selections selection = schedule.Selections[position];
                //Base List Maintained for Random Selection
                str += "<div style='width:100%; heigth:100%'>";
                //space
                //str += "<p class='space' style='font-size:20pt;'>&nbsp;</p>";
                str += "<p style='font-size:4pt;'><strong>";
                str += "Duplication List: " + Consortiums.Get(schedule.ConsortiumID.ToString()).Name + " - ";
                str += "Selection Date & Time: " + selection.RunOn.ToString("MM-dd-yyyy") + " / " + selection.RunOn.ToString("HH:mm:ss");
                str += "</strong></p>";

                //space
                str += "<p class='space' style='font-size:5pt;'>&nbsp;</p>";
                //Donor List           
                List<People> peoples = People.PeopleGets(RandomReports.folder + FieldKeys.PeopleClass + @"\");
                //var folder = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\Donor\";
                List<DonorInfo> donors = DonorInfo.GetsByFolder(RandomReports.folder + FieldKeys.DonorClass + @"\");
                //donors = donors.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                str += "<table style='width:75%;'>";
                str += "<tr>";
                str += "<th style='width:3%;font-size:4pt;'>&nbsp;</th>";
                str += "<th style='width:4%;font-size:4pt;text-align:center;'>Not Active</th>";
                str += "<th style='width:4%;font-size:4pt;text-align:center;'>Not Avail.</th>";
                str += "<th style='width:15%;font-size:4pt;'><br/>Last Name</th>";
                str += "<th style='width:15%;font-size:4pt;'><br/>First Name</th>";
                str += "<th style='width:3%;font-size:4pt; text-align:center;'><br/>M.i.</th>";
                str += "<th style='width:28%;font-size:4pt;'><br/>Comapny Name</th>";
                str += "<th style='width:9%;font-size:4pt;'><br/>DER Phone</th>";                
                str += "<th></th>";
                str += "<th style='width:10%;font-size:4pt;'><br/>Primary ID</th>";
                str += "<th style='width:9%;font-size:4pt;'><br/>Driver Phone</th>";
                
                str += "</tr>";

                //if (schedule.Type.Equals(1))
                //coms = coms.FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID.ToString()));
                //Lib.writerLog("RandomReports", "BaseListMaintainedForConsortium", Lib.SerializeJson(schedule), "error");
                //Lib.writerLog("RandomReports", "BaseListMaintainedForConsortium", Lib.SerializeJson(coms), "error");
                int k = 0, count = 0;
                int maxLength = coms.Count;
                coms.Sort((x, y) => x.CompanyName.CompareTo(y.CompanyName));
                foreach (var com in coms)
                {

                    //var specimens = selection.DonorSpecimenList;
                    var list = donors.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()) && x.NotActive.Equals(0));
                   

                    
                    i = 0;
                    foreach (var donor in list)
                    {
                        //var donor = donors.Single(x => x.ID.Equals(specimen.DonorID));
                        String[] ids = donor.ID.Split('_');

                        var people = peoples.Single(x => x.ID.Equals(ids[1]));                      

                        string col = count.Equals(k - 1) ? "col2" : "col";

                        string colEnd = count.Equals(k - 1) ? "col2End" : "colEnd";
                       
                        str += "<tr>";
                        str += "<td class='" + col + "' style='width:3%;font-size:4pt;'>" + (k + 1) + "</td>";
                        value = donor.NotActive.Equals(0) ? "&nbsp;" : "x";
                        str += "<td class='" + col + "'style='width:4%;font-size:4pt;text-align:center;'>" + value + "</td>";
                        value = donor.NotAvilable.Equals(0) ? "&nbsp;" : "x";
                        str += "<td class='" + col + "'style='width:4%;font-size:4pt;text-align:center;'>" + value + "</td>";
                        value = people.PersonalInfo.Person.LastName;
                        str += "<td class='" + col + "'style='width:15%;font-size:4pt;'>" + value + "</td>";
                        value = people.PersonalInfo.Person.FirstName;
                        str += "<td class='" + col + "'style='width:15%;font-size:4pt;'>" + value + "</td>";
                        str += "<td class='" + col + "'style='width:3%;font-size:4pt;'>&nbsp;</td>";
                        value = com.CompanyName;
                        str += "<td class='" + col + "'style='width:28%;font-size:4pt;'>" + value + "</td>";
                        value = com.PersonalInfo.Contact.MobilePhone;
                        str += "<td class='" + colEnd + "'style='width:9%;font-size:4pt;'>" + value + "</td>";

                        str += "<td style='font-size:4pt'>&nbsp;</td>";

                        value = people.Driver.PrimaryID;
                        str += "<td class='" + col + "' style='width:8%;font-size:4pt;'>" + value + "</td>";
                        //if (count.Equals(k - 1))
                        //    str += "<td class='col3End' style='width:10%;font-size:4pt;'>" + value + "</td>";
                        //else

                        value = people.PersonalInfo.Contact.MobilePhone;
                        str += "<td class='" + colEnd + "' style='width:9%;font-size:4pt;'>" + value + "</td>";

                        //str += "<td style='font-size:4pt'>&nbsp;</td>";

                        //str += "<td class='" + col + "'style='width:3%;font-size:4pt;'>&nbsp;</td>";
                        //str += "<td class='" + colEnd + "'style='width:3%;font-size:4pt;'>&nbsp;</td>";
                        str += "</tr>";
                      
                        i++;

                        count = k;
                        
                        k++;
                    }

                }
                str += "</table>";

                str += "</div>";

                
            }
            catch(Exception ex)
            {
                Lib.writerLog("RandomReports", "BaseListMaintainedForConsortium", ex.Message, "error");
            }
            return str;
        }

        public static String BaseListMaintainedTable(CompanyInfo com, Schedule_Selections selection)
        {
            String str = String.Empty;
            String value = String.Empty;
            List<People> peoples = People.PeopleGets(RandomReports.folder + FieldKeys.PeopleClass + @"\");
            //var folder = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\Donor\";
            List<DonorInfo> donors = DonorInfo.GetsByFolder(RandomReports.folder + FieldKeys.DonorClass + @"\");

            donors = donors.FindAll(x => x.CompanyID.Equals(com.CompanyID.ToString()) && x.NotActive.Equals(0));
           
            str += "<table style='width:80%;'>";
            str += "<tr>";
            str += "<th style='width:4%;'>&nbsp;</th>";
            str += "<th style='width:6%;  text-align:center;'>Not Active</th>";
            str += "<th style='width:6%;  text-align:center;'>Not Avail.</th>";
            str += "<th style='width:19%'><br/>Last Name</th>";
            str += "<th style='width:20%'><br/>First Name</th>";
            str += "<th style='width:4%;  text-align:center;'><br/>M.i.</th>";
            str += "<th style='width:16%; text-align:center;'><br/>Primary ID</th>";
            //str += "<th style='width:10%; text-align:center;'><br/>Mode</th>";
            str += "<th style='width:17%;'><br/>Driver Phone</th>";
            str += "<th style='width:4%;  text-align:center;'><br/>Frx</th>";
            str += "<th style='width:4%;  text-align:center;'><br/>FT</th>";
            str += "</tr>";

            int i = 0;
            int maxLength = donors.Count;
            foreach (var donor in donors)
            {
                String[] ids = donor.ID.Split('_');
                var people = peoples.Single(x => x.ID.Equals(ids[1]));

                string col = i.Equals(maxLength - 1) ? "col2" : "col";

                string colEnd = i.Equals(maxLength - 1) ? "col2End" : "colEnd";

                str += "<tr>";
                str += "<td class='" + col + "' style='width:4%;'>" + (i + 1) + "</td>";
                value = donor.NotActive.Equals(0) ? "&nbsp;" : "x";
                str += "<td class='" + col + "'style='width:6%;  text-align:center;'>" + value + "</td>";
                value = donor.NotAvilable.Equals(0) ? "&nbsp;" : "x";
                str += "<td class='" + col + "'style='width:6%;  text-align:center;'>" + value + "</td>";
                value = people.PersonalInfo.Person.LastName;
                str += "<td class='" + col + "'style='width:19%'>" + value + "</td>";
                value = people.PersonalInfo.Person.FirstName;
                str += "<td class='" + col + "'style='width:20%'>" + value + "</td>";
                str += "<td class='" + col + "'style='width:4%;  text-align:center;'>&nbsp;</td>";
                value = people.Driver.PrimaryID;
                str += "<td class='" + col + "'style='width:16%;text-align:center;'>" + value + "</td>";
                //value = people.Driver.Mode;
                // str += "<td class='" + col + "'style='width:10%;'>" + value + "</td>";
                value = people.PersonalInfo.Contact.MobilePhone;
                str += "<td class='" + col + "'style='width:17%;'>" + value + "</td>";
                str += "<td class='" + col + "'style='width:4%;'>&nbsp;</td>";
                str += "<td class='" + colEnd + "'style='width:4%;'>&nbsp;</td>";
                str += "</tr>";
                i++;
            }
            str += "</table>";
            return str;
        }

        public static Boolean PdfCreatedBaseListMaintained(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 28, 10))
                    {
                        //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Base List Maintained");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            // Boolean kq = false;
                            int maxLength = 1;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {
                                //Add page number
                                doc.NewPage();
                                int position = Lib.get_value_int(ids[1]);
                                if (schedule.Type.Equals(1))//By Consrotium
                                    html = BaseListMaintainedForConsortium(schedule, companies, position);
                                else //By Company
                                    html = BaseListMaintained(schedule, companies[i], position);

                                using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                {
                                    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                    {
                                        //Parse the HTML
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                    }
                                }
                                count++;
                                pdfContentByte(writer, "- " + (count) + " -", false);
                                //   Console.WriteLine("Base List Maintained Pdf {0}", count);

                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.

                var testFile = @"D:\BaseListMaintained_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "BaseListMaintained_" + ids[0] + "_" + ids[1] + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);
                if (schedule.Type.Equals(1))
                    RandomReports.PdfCreatedBaseListMaintainedByCompany(schedule, companies, ids);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Base List Maintained Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "Pdf Created Base List ", ex.Message, "error");
            }
            return kq;
        }

        public static Boolean PdfCreatedBaseListMaintained(Schedules schedule, CompanyInfo com, String[] ids)
        {
            Boolean kq = false;
            try
            {
                //Create a byte array that will eventually hold our final PDF
                Byte[] bytes;

                //Boilerplate iTextSharp setup here
                //Create a stream that we can write to, in this case a MemoryStream
                using (var ms = new MemoryStream())
                {

                    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var doc = new Document(PageSize.A4, 10, 5, 28, 10))
                    {
                        //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        //Create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {

                            //Open the document for writing
                            doc.Open();
                            doc.AddTitle("Base List Maintained");
                            doc.AddAuthor("SFHCAdmin");

                            //Our sample HTML and CSS
                            var html = String.Empty;
                            var css = CssStyle();


                            //In order to read CSS as a string we need to switch to a different constructor
                            //that takes Streams instead of TextReaders.
                            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                            // Boolean kq = false;
                            int maxLength = 1;
                            int count = 0;
                            for (int i = 0; i < maxLength; i++)
                            {
                                //Add page number
                                doc.NewPage();
                                int position = Lib.get_value_int(ids[1]);

                                html = BaseListMaintained(schedule, com, position);
                                using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                                {
                                    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                                    {
                                        //Parse the HTML
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                                    }
                                }
                                count++;
                                pdfContentByte(writer, "- " + (count) + " -", false);
                                //   Console.WriteLine("Base List Maintained Pdf {0}", count);

                                if (i == (maxLength - 1))
                                    kq = true;
                            }

                            doc.Close();

                            //if (kq)
                            //    Console.WriteLine("Writer PDF file Success.");
                        }
                    }

                    //After all of the PDF "stuff" above is done and closed but **before** we
                    //close the MemoryStream, grab all of the active bytes from the stream
                    bytes = ms.ToArray();
                }

                //Now we just need to do something with those bytes.
                //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
                //You could also write the bytes to a database in a varbinary() column (but please don't) or you
                //could pass them to another function for further PDF processing.

                var testFile = @"D:\BaseListMaintained_" + ids[0] + "_" + ids[1] + ".pdf";
                testFile = new FilePath(FieldKeys.ReportClass).Folder + "BaseListMaintained_" + ids[0] + "_" + ids[1] + "_" + com.CompanyID + ".pdf";
                //File.WriteAllBytes(testFile, bytes);
                Lib.WriteFilePDF(ref testFile, testFile, bytes);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Base List Maintained Error: {0}", ex.Message);
                Lib.writerLog("RandomReports", "Pdf Created Base List ", ex.Message, "error");
            }
            return kq;
        }

        public static void PdfCreatedBaseListMaintainedByCompany(Schedules schedule, List<CompanyInfo> companies, String[] ids)
        {
            if (schedule.Type.Equals(1))
            {
                foreach (var com in companies)
                {
                    RandomReports.PdfCreatedBaseListMaintained(schedule, com, ids);
                }
            }
        }
        #endregion
    }
}

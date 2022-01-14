using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace HTT
{
    public class PdfControl
    {

        private static void CreateSignatureImages(String base64, String INPUT, String OUTPUT)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            // 'INPUT' => already rendered pdf in iText
            PdfReader reader = new PdfReader(INPUT);
            string outputFile = Path.Combine(currentDir, OUTPUT);

            using (var stream = new FileStream(outputFile, FileMode.Create))
            {
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {

                    var fldPosition = "formMCSA-5876[0].page1[0].examinerBox[0].examinerSignature";
                    var fldPosition2 = "formMCSA-5876[0].page1[0].driverBox[0].driverSignature";

                    string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAKMAAABhCAYAAAC6TBE/AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsIAAA7CARUoSoAAAAMUSURBVHhe7dyNcpswEEVhO+//zk12JmqwrB+QAV3tnm+G6bSmgOFEgjjt89+PByDg6/dXYDpihAxihAxihAxihAxihAxihAxiFPd8Pv8v3hGjsAgBbhEjZBAjZBCjqGhTtCFGQRFDNMQIGcQoJuqoaIhRWLQfNSVGyCBGyCBGYXwCg2kiP7wYYhSVP7xEeJghRhH5qLj9fZSnamIUFPVfDxOjoKj3jsQoIOKUXEKMYvJRMVKcxCgs2ihJjJPV7g8jTtfECBnEOBGj4itinIQQ3xGjkMghGmKcoDQqRg/REOPNCLGO/0b5BrX7Q8Pp/0OMF2kFmHDqXzFNX2BPiHhHjCeyCGsh5qMgo+K7w9N05K/62qnaG+B2PWJ8R4wXKZ1WYmxjmj6ZRUZoY3iabvhkFiidVkbGNmLcuPMW5NPTnh+rh8sYOsYj8e05TXu3d3aIhhgXdHaANb39jG67tl1iXMRdI1ZLfgwj+2q9D2IUtifAu9566ViO7rv3fjxcRpff2umNIGlZmYf4cq5itAhro9CsAHsj2giPIRoXMfYiVJeOv/Y+tn/mNUSzfIyli2c8XzSv3N0zrjIaJrUvpqT3uifLP00rTmG9gNJx9kb1/PX8/R193aicoxKXT9Oz2MWvBWYshFpoW6MRpf2nZTXE+KGRi5+vu400WTGmT7mK8e4L2NpfKTAzcoy1UbG2j1xab8+6M7m6ZzRXvZ09EZX23Qt2q7bu4pdot+VHxr0XdIRtKy0tdgxXBRMlROPms+kzRpWRkFvbb22v9PdK6zu5PLu4j/EsI6fpaFz5+pFCNG4eYK66cLbds7YdLa6jQv8IWcmZp2N7DITY5zZGBcR4DN/0hgxihAxihAxihAweYCCDkREyiBEyiBEyiBEyiBGnsE+b0jKKp2kc1gtuNCliRNXoKDeaFNM0Xpwx3Y5iZFzIjEBKrkqmG6PKCcD97h6nmKbxwgJMy90YGRcyI5A7cc8IGUzTkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMkEGMEPF4fAMYephs/C16YwAAAABJRU5ErkJggg==";

                    SetImage(base64Image, fldPosition, stamper);
                    SetImage(base64Image, fldPosition2, stamper);
                }

            }


        }

        public static void SetImage(String imageBase64String, String pdfFieldPosition, PdfStamper stamper)
        {
            AcroFields form = stamper.AcroFields;
            var fldPosition = form.GetFieldPositions(pdfFieldPosition)[0];
            Rectangle rectangle = fldPosition.position;

            string base64Image = "data:image/png;base64," + imageBase64String;
            Regex regex = new Regex(@"^data:image/(?<mediaType>[^;]+);base64,(?<data>.*)");
            Match match = regex.Match(base64Image);
            Image image = Image.GetInstance(
                Convert.FromBase64String(match.Groups["data"].Value)
            );

            // best fit if image bigger than form field
            if (image.Height > rectangle.Height || image.Width > rectangle.Width)
            {
                image.ScaleAbsolute(rectangle);
            }

            // form field top left - change parameters as needed to set different position 
            image.SetAbsolutePosition(rectangle.Left + 122, rectangle.Top - 15);
            image.ScaleAbsoluteWidth(80);
            image.ScaleAbsoluteHeight(20);
            stamper.GetOverContent(fldPosition.page).AddImage(image);

        }

        public static void SetEncrypt(String inputPath, String outPutPath,String tempPath, String userPassword, String ownerPassword)
        {

            string tempInput = tempPath + @"\temp.pdf";
            if (File.Exists(tempInput))
                File.Delete(tempInput);
            File.Copy(inputPath, tempInput);

            PdfReader reader = new PdfReader(tempInput);

            using (Stream output = new FileStream(outPutPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {

                PdfEncryptor.Encrypt(reader, output, true, userPassword, ownerPassword, PdfWriter.ALLOW_PRINTING);
               
            }
        }

        public static void SetEncrypt(byte[] data, String outPutPath, String userPassword, String ownerPassword)
        {            

            PdfReader reader = new PdfReader(data);

            using (Stream output = new FileStream(outPutPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {

                PdfEncryptor.Encrypt(reader, output, true, userPassword, ownerPassword, PdfWriter.ALLOW_PRINTING);

            }
        }
    }
}

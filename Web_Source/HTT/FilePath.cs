using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTT
{
    public class FilePath
    {
        #region Fields
        private string folder = "", fileName = "", filePathName = "";

        #endregion End Fields

        #region Constructors
        public FilePath()
        {

        }

        public FilePath(String folder)
        {
            //switch (folder.ToLower())
            //{
            //    case FieldKeys.UserClass:
            this.fileName = Lib.EncoderStringSH1(folder + "SF") + ".json";

            String dataFolderPath = System.Web.HttpContext.Current.Server.MapPath("~") + @"Data";
            String folderName = dataFolderPath + @"\" + (String.IsNullOrEmpty(folder) || folder.Equals("") ? "" : (folder.ToUpper() + @"\"));

            if (!Directory.Exists(folderName))
            {
                //DirectoryInfo di = Directory.CreateDirectory(folderName);
                //di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folderName);
            }

            this.folder = folderName;

            this.filePathName = String.Format(folderName + FileName);
            //        break;               
            //}
        }

        public string Folder
        {
            get
            {
                return folder;
            }

            set
            {
                folder = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public string FilePathName
        {
            get
            {
                return filePathName;
            }

            set
            {
                filePathName = value;
            }
        }


        #endregion End Constructors

        public FilePath UserFilePath(String folder) {
            FilePath fp = new FilePath();

           fp.FileName = Lib.EncoderStringSH1(folder + "SF") + ".json";

            String folderName = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Data\" + folder + @"\";

            fp.Folder = folderName;

            fp.filePathName = String.Format(folderName + FileName);
            return fp;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTT
{
    public class ResultInfo
    {
        #region Fields
        private String sMessage = "", sStatus = "", sLink = "";       
        private Object oData;
        private int iCode = 0;
        #endregion End Fields

        #region Constuctors
        public ResultInfo()
        {

        }

        public ResultInfo(String message,String status, String link)
        {
            this.sMessage = message;
            this.sStatus = status;
            this.Link = link;
            this.iCode = 0;
        }

        public ResultInfo(String message, String status, String link, int code)
        {
            this.sMessage = message;
            this.sStatus = status;
            this.Link = link;
            this.iCode = code;
        }

        public ResultInfo(String message, String status, String link, Object data)
        {
            this.sMessage = message;
            this.sStatus = status;
            this.Link = link;           
            this.iCode = 1;
            this.oData = data;
        }

        public string Message { get => sMessage; set => sMessage = value; }
        public string Status { get => sStatus; set => sStatus = value; }
        public int Code { get => iCode; set => iCode = value; }
        public string Link { get => sLink; set => sLink = value; }            
        public object Data { get => oData; set => oData = value; }
        #endregion End Constructors
    }
}
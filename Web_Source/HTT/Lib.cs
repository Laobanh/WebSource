using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HTT
{
    public static class Lib
    {
        //public Lib()
        //{

        //}

        #region số tuần trong tháng
        public static int Weeks(int year, int month, int thu)
        {
            DayOfWeek wkstart = dayofweek(thu);
            DateTime first = new DateTime(year, month, 1);
            int firstwkday = (int)first.DayOfWeek;
            int otherwkday = (int)wkstart;
            int offset = ((otherwkday + 7) - firstwkday) % 7;
            double weeks = (double)(DateTime.DaysInMonth(year, month) - offset) / 7d;
            return (int)Math.Ceiling(weeks);
        }
        public static DayOfWeek dayofweek(int thu)
        {
            DayOfWeek weeks = DayOfWeek.Sunday;
            switch (thu)
            {
                case 1:
                    weeks = DayOfWeek.Sunday;
                    break;
                case 2:
                    weeks = DayOfWeek.Monday;
                    break;
                case 3:
                    weeks = DayOfWeek.Tuesday;
                    break;
                case 4:
                    weeks = DayOfWeek.Wednesday;
                    break;
                case 5:
                    weeks = DayOfWeek.Thursday;
                    break;
                case 6:
                    weeks = DayOfWeek.Friday;
                    break;
                case 7:
                    weeks = DayOfWeek.Saturday;
                    break;
                default:
                    weeks = DayOfWeek.Sunday;
                    break;
            }
            return weeks;
        }
        #endregion

        #region thêm 3 số không vào số tiền
        public static string tien(string duyet, int sotien)
        {
            string namestr = "";
            if (duyet == "1")
            {
                namestr = "<span style='color:Red'>" + (sotien * 1000).ToString("N0") + "</span>";
            }
            else
            {
                namestr = (sotien * 1000).ToString("N0");
            }
            return namestr;
        }
        #endregion       
        

        #region đổi chuỗi thành chuỗi có độ dài theo quy định
        public static string Ret_doidai(string so, int dodai)
        {
            string str = "";
            if (!string.IsNullOrEmpty(so))
            {
                while (so.Length < dodai)
                {
                    so = "0" + so;
                }
                str = so;
            }
            return str;
        }
        #endregion

        #region lấy địa chỉ url
        public static string ret_url()
        {
            string str = "";
            Uri uri = HttpContext.Current.Request.Url;
            string url_cur = uri.Query;
            string[] urlv = url_cur.Split('=');
            string path = urlv[urlv.Length - 1];
            //str = ZS_html.webconfig("url") + path.Replace("-", "/") + ".aspx" + url_cur;
            //str = path.Replace("-", "/") + ".aspx" + url_cur;
            return str;
        }
        public static string get_url()
        {
            string str = "";
            Uri uri = HttpContext.Current.Request.Url;
            string url_cur = uri.Query;
            string[] urlv = url_cur.Split('=');
            string path = urlv[urlv.Length - 1];
            str = path;
            return str;
        }
        #endregion

        #region lịch công tác trong tuần của nhân viên
        
        public static string Ret_weeks_vni(int day,int month,int year)
        {
            DateTime d = Lib.ret_date(day + "/" + month + "/" + year);
            string thu=d.DayOfWeek.ToString().ToLower();
            string[] istr = { "sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday" };
            string[] istr2 = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            string str = "";
            for (int i = 0; i < 7; i++)
            {
                if (thu.Contains(istr[i]))
                {
                    str = istr2[i];
                }
            }
            return str;

        }
        #endregion        

        #region đổi ra chuỗi tiền thêm 3 số 0 và có dấu phẩy
        public static string tien(string duyet, double sotien)
        {
            string str = "";
            if (duyet.Contains("1"))
            {
                str = "<strong style='color:Red'>" + (sotien * 1000).ToString("N0");
                str += "</strong>";
            }
            else
            {
                str = (sotien * 1000).ToString("N0");
            }
            return str;
        }
        #endregion

        #region làm tròn 1 số
        /// <summary>
        /// LÀm tròn 1 số
        /// </summary>
        /// <param name="value">giá trị cần làm tròn</param>
        /// <param name="digit">làm tròn đến bao nhiêu số thập phân</param>
        /// <returns></returns>
        public static double C1Round(double value, int digit)
        {
            double vt = Math.Pow(10, digit);
            double vx = value * vt;
            vx += 0.5;
            return (Math.Floor(vx) / vt);
        }
        #endregion

        #region tra du lieu kieu int
        /// <summary>
        /// Chuyển thành kiểu số nguyên
        /// </summary>
        /// <param name="chuoi"></param>
        /// <returns></returns>
        public static int get_value_int(string chuoi)
        {
            int number = 0;
            try
            {
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Replace(",", "").Replace(".", "");
                    if (ZS_Kiemtra.Ret_Num(chuoi))
                    {

                        number = Convert.ToInt32(chuoi);
                    }
                }
            }
            catch { }
            return number;
        }
        #endregion

        #region tra du lieu kieu double
        /// <summary>
        /// chuyển thành kiểu số thực
        /// </summary>
        /// <param name="chuoi"></param>
        /// <returns></returns>
        public static double get_value_double(string chuoi)
        {
            double number = 0;
            try
            {
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Replace(",", "");
                    number = Convert.ToDouble(chuoi);
                }
            }
            catch { }
            return number;
        }
        #endregion
        
        #region tra du lieu kieu float
        /// <summary>
        /// chuyển thành kiểu float
        /// </summary>
        /// <param name="chuoi"></param>
        /// <returns></returns>
        public static float get_value_float(string chuoi)
        {
            float number = 0;
            try
            {
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = chuoi.Replace(",", "");
                    number = float.Parse(chuoi);
                }
            }
            catch { }
            return number;
        }
        #endregion
        #region tra du lieu kieu string
        
        public static string get_value_str(string chuoi)
        {
            string str = "0";
            try
            {
                if (!string.IsNullOrEmpty(chuoi))
                {
                    str = chuoi.Trim();
                }
            }
            catch { }
            return str;
        }
        #endregion

        #region chuyển số thành chữ
        public static string ReadGroup3(string G3)
        {
            string[] ReadDigit = new string[10] { " Không", " Một", " Hai", " Ba", " Bốn", " Năm", " Sáu", " Bảy", " Tám", " Chín" };
            string temp = "";
            if (G3 == "000") return "";

            //Đọc số hàng trăm
            temp = ReadDigit[int.Parse(G3[0].ToString())] + " Trăm";
            //Đọc số hàng chục
            if (G3[1].ToString() == "0")
                if (G3[2].ToString() == "0") return temp;
                else
                {
                    temp += " Lẻ" + ReadDigit[int.Parse(G3[2].ToString())];
                    return temp;
                }
            else
                temp += ReadDigit[int.Parse(G3[1].ToString())] + " Mươi";
            //--------------Đọc hàng đơn vị

            if (G3[2].ToString() == "5") temp += " Lăm";
            else if (G3[2].ToString() != "0") temp += ReadDigit[int.Parse(G3[2].ToString())];
            return temp;
        }
        public static string ReadMoney(string Money)
        {
            string temp = "";
            if (Money == "" || Money == null || Money == "0")
            {
                temp = "Không";
            }
            // Cho đủ 12 số
            while (Money.Length < 12)
            {
                Money = "0" + Money;
            }
            string g1 = Money.Substring(0, 3);
            string g2 = Money.Substring(3, 3);
            string g3 = Money.Substring(6, 3);
            string g4 = Money.Substring(9, 3);

            //Đọc nhóm 1 ---------------------
            if (g1 != "000")
            {
                temp = ReadGroup3(g1);
                temp += " Tỷ";
            }
            //Đọc nhóm 2-----------------------
            if (g2 != "000")
            {
                temp += ReadGroup3(g2);
                temp += " Triệu";
            }
            //---------------------------------
            if (g3 != "000")
            {
                temp += ReadGroup3(g3);
                temp += " Ngàn";
            }
            //-----------------------------------
            //Chỗ này ko biết có nên ko ?
            //temp =temp + ReadGroup3(g4).Replace("Không Trăm Lẻ","Lẻ"); // Đọc 1000001 là Một Triệu Lẻ Một thay vì Một Triệu Không Trăm Lẻ 1;
            temp = temp + ReadGroup3(g4);
            //---------------------------------
            // Tinh chỉnh
            temp = temp.Replace("Một Mươi", "Mười");
            temp = temp.Trim();
            if (temp.IndexOf("Không Trăm") == 0)
                temp = temp.Remove(0, 10);
            temp = temp.Trim();
            if (temp.IndexOf("Lẻ") == 0)
                temp = temp.Remove(0, 2);
            temp = temp.Trim();
            temp = temp.Replace("Mươi Một", "Mươi Mốt");
            temp = temp.Trim();
            //Change Case
            return temp.Substring(0, 1).ToUpper() + temp.Substring(1).ToLower() + " Việt nam đồng.";

        }
        #endregion

        #region chuyển đổi định dạng ngày tháng
        /// <summary>
        /// Định dạng ngày tháng theo dang 
        /// loại ddmmyyyy; ddmmyy; ddmmyyyyhh;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string strtime(DateTime str, string loai)
        {
            string strtime = "";
            if (str.Year.ToString().Contains("1900"))
            {
                strtime = "0";
            }
            else
            {
                switch (loai.ToLower())
                {
                    case "ddmmyyyy":
                        strtime = Ret_doidai(str.Day.ToString(), 2) + "-" + Ret_doidai(str.Month.ToString(), 2) + "-" + str.Year.ToString();
                        break;
                    case "ddmmyy":
                        strtime = Ret_doidai(str.Day.ToString(), 2) + "-" + Ret_doidai(str.Month.ToString(), 2) + "-" + str.Year.ToString().Substring(2);
                        break;
                    case "ddmmyyyyhh":
                        strtime = Ret_doidai(str.Day.ToString(), 2) + "-" + Ret_doidai(str.Month.ToString(), 2) + "-" + str.Year + " " + Ret_doidai(str.Hour.ToString(), 2) + ":" + Ret_doidai(str.Minute.ToString(), 2) + ":" + Ret_doidai(str.Second.ToString(), 2);
                        break;
                    default:
                        strtime = Ret_doidai(str.Day.ToString(), 2) + "-" + Ret_doidai(str.Month.ToString(), 2) + "-" + str.Year.ToString();
                        break;
                }
            }
            return strtime;
        }
        #endregion
        
        #region chuyen ký tự VN thanh Eng
        public static string Ret_Tienganh(string inputstring, int maxlength)
        {
            StringBuilder sb = new StringBuilder();
            if ((inputstring != null) && (inputstring != string.Empty))
            {
                inputstring = inputstring.Trim().ToLower();
                if (inputstring.Length > maxlength)
                {
                    inputstring = inputstring.Substring(0, maxlength);
                }
                for (int i = 0; i < inputstring.Length; i++)
                {
                    switch (inputstring[i])
                    {
                        case ' ': sb.Append("-"); break;
                        case ',': sb.Append(""); break;
                        case '~': sb.Append(""); break;
                        case '/': sb.Append("-"); break;
                        case 'á': sb.Append("a"); break;
                        case 'à': sb.Append("a"); break;
                        case 'ả': sb.Append("a"); break;
                        case 'ã': sb.Append("a"); break;
                        case 'ạ': sb.Append("a"); break;
                        case 'ă': sb.Append("a"); break;
                        case 'ắ': sb.Append("a"); break;
                        case 'ằ': sb.Append("a"); break;
                        case 'ẳ': sb.Append("a"); break;
                        case 'ẵ': sb.Append("a"); break;
                        case 'ặ': sb.Append("a"); break;
                        case 'â': sb.Append("a"); break;
                        case 'ấ': sb.Append("a"); break;
                        case 'ầ': sb.Append("a"); break;
                        case 'ẩ': sb.Append("a"); break;
                        case 'ẫ': sb.Append("a"); break;
                        case 'ậ': sb.Append("a"); break;
                        case 'é': sb.Append("e"); break;
                        case 'è': sb.Append("e"); break;
                        case 'ẻ': sb.Append("e"); break;
                        case 'ẽ': sb.Append("e"); break;
                        case 'ẹ': sb.Append("e"); break;
                        case 'ê': sb.Append("e"); break;
                        case 'ế': sb.Append("e"); break;
                        case 'ề': sb.Append("e"); break;
                        case 'ể': sb.Append("e"); break;
                        case 'ễ': sb.Append("e"); break;
                        case 'ệ': sb.Append("e"); break;
                        case 'ú': sb.Append("u"); break;
                        case 'ù': sb.Append("u"); break;
                        case 'ủ': sb.Append("u"); break;
                        case 'ũ': sb.Append("u"); break;
                        case 'ụ': sb.Append("u"); break;
                        case 'ư': sb.Append("u"); break;
                        case 'ứ': sb.Append("u"); break;
                        case 'ừ': sb.Append("u"); break;
                        case 'ử': sb.Append("u"); break;
                        case 'ữ': sb.Append("u"); break;
                        case 'ự': sb.Append("u"); break;
                        case 'ó': sb.Append("o"); break;
                        case 'ò': sb.Append("o"); break;
                        case 'ỏ': sb.Append("o"); break;
                        case 'õ': sb.Append("o"); break;
                        case 'ọ': sb.Append("o"); break;
                        case 'ô': sb.Append("o"); break;
                        case 'ố': sb.Append("o"); break;
                        case 'ồ': sb.Append("o"); break;
                        case 'ổ': sb.Append("o"); break;
                        case 'ỗ': sb.Append("o"); break;
                        case 'ộ': sb.Append("o"); break;
                        case 'ơ': sb.Append("o"); break;
                        case 'ớ': sb.Append("o"); break;
                        case 'ờ': sb.Append("o"); break;
                        case 'ở': sb.Append("o"); break;
                        case 'ỡ': sb.Append("o"); break;
                        case 'ợ': sb.Append("o"); break;
                        case 'í': sb.Append("i"); break;
                        case 'ì': sb.Append("i"); break;
                        case 'ỉ': sb.Append("i"); break;
                        case 'ĩ': sb.Append("i"); break;
                        case 'ị': sb.Append("i"); break;
                        case 'đ': sb.Append("d"); break;
                        case 'ý': sb.Append("y"); break;
                        case 'ỳ': sb.Append("y"); break;
                        case 'ỷ': sb.Append("y"); break;
                        case 'ỹ': sb.Append("y"); break;
                        default: sb.Append(inputstring[i]); break;
                    }
                }
                sb.Replace("'", "");
            }
            return (sb.ToString());
        }
        #endregion

        #region xoa ky tu so
        public static string Del_Num(string inputstring, int maxlength)
        {
            StringBuilder sb = new StringBuilder();
            if ((inputstring != null) && (inputstring != string.Empty))
            {
                inputstring = inputstring.Trim().ToLower();
                if (inputstring.Length > maxlength)
                {
                    inputstring = inputstring.Substring(0, maxlength);
                }
                for (int i = 0; i < inputstring.Length; i++)
                {
                    switch (inputstring[i])
                    {
                        case '0': sb.Append(" "); break;
                        case '1': sb.Append(" "); break;
                        case '2': sb.Append(" "); break;
                        case '3': sb.Append(" "); break;
                        case '4': sb.Append(" "); break;
                        case '5': sb.Append(" "); break;
                        case '6': sb.Append(" "); break;
                        case '7': sb.Append(" "); break;
                        case '8': sb.Append(" "); break;
                        case '9': sb.Append(" "); break;
                        default: sb.Append(inputstring[i]); break;
                    }
                }
                sb.Replace("'", "");
            }
            return (sb.ToString());
        }
        #endregion
        
        #region đổi string thành ngày tháng
        /// <summary>
        /// format dd/MM/yyyy HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime ret_date(string datetime, string cultureInfo)
        {
            CultureInfo culinfo = new CultureInfo(cultureInfo);
            DateTime d = Convert.ToDateTime("1/1/1900");
            try
            {
                if (!string.IsNullOrEmpty(datetime))
                {
                    d = Convert.ToDateTime(datetime, culinfo);
                }
            }
            catch { }
            return d;
        }
        /// <summary>
        /// Return Datetime
        /// </summary>
        /// <param name="datetime">MM/dd/yyyy HH:mm</param>
        /// <returns></returns>
        public static DateTime RetDateTime(string datetime)
        {
            DateTime d = Convert.ToDateTime("1/1/1900");
            try
            {
                if (!string.IsNullOrEmpty(datetime))
                {
                    String[] arrayDate = datetime.Split('/', '-', ' ', ':');
                    if (arrayDate.Length.Equals(5))
                    {
                        int year = Lib.get_value_int(arrayDate[2]);
                        int month = Lib.get_value_int(arrayDate[0]);
                        int day = Lib.get_value_int(arrayDate[1]);
                        int hour = Lib.get_value_int(arrayDate[3]);
                        int minute = Lib.get_value_int(arrayDate[4]);
                        d = new DateTime(year, month, day, hour, minute, 0);
                    }
                }
            }
            catch { }
            return d;
        }

        /// <summary>
        /// Return Datetime
        /// </summary>
        /// <param name="datetime">MM/dd/yyyy</param>
        /// <returns></returns>
        public static DateTime ret_date(string datetime)
        {
            DateTime d = Convert.ToDateTime("1/1/1900");
            try
            {
                if (!string.IsNullOrEmpty(datetime))
                {
                    String[] arrayDate = datetime.Split('/', '-');
                    if (arrayDate.Length == 3)
                    {
                        int year = Lib.get_value_int(arrayDate[2]);
                        int month = Lib.get_value_int(arrayDate[0]);
                        int day = Lib.get_value_int(arrayDate[1]);
                        d = new DateTime(year, month, day);
                    }
                }
            }
            catch { }
            return d;
        }
        #endregion

        #region undecode unicode
        public static string UnicodeTochar(string sunicode)
        {
            string str = "";
            if (!string.IsNullOrEmpty(sunicode))
            {
                string[] idec = sunicode.Split(';');
                if (idec.Length > 0)
                {
                    for (int i = 0; i < idec.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(idec[i]))
                        {
                            if (ZS_Kiemtra.Ret_Num(idec[i]))
                            {
                                int d = Convert.ToInt32(idec[i]);
                                char c = (char)d;
                                str += c.ToString();
                            }
                        }
                    }
                }
            }
            return str;
        }
        #endregion
                
        #region mã hóa dữ liệu
        public static string EncoderStringSH1(string str)
        {
            string kq = "";
            if (!string.IsNullOrEmpty(str))
            {
                var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(str));
                kq = BitConverter.ToString(hash).Replace("-", String.Empty);
            }
            return kq;
        }
        #endregion

        #region xóa bỏ ký tự lạ
        /// <summary>
        /// xóa ký tự khoảng trắng
        /// </summary>
        /// <param name="inputstring"></param>
        /// <param name="maxlength"></param>
        /// <returns></returns>
        public static string ClearInputString(string inputstring, int maxlength)
        {
            StringBuilder sb = new StringBuilder();
            if ((inputstring != null) && (inputstring != string.Empty))
            {
                inputstring = inputstring.Trim();
                if (inputstring.Length > maxlength)
                {
                    inputstring = inputstring.Substring(0, maxlength);
                }
                for (int i = 0; i < inputstring.Length; i++)
                {
                    switch (inputstring[i])
                    {
                        case ' ': sb.Append(""); break;
                        case '"': sb.Append(""); break;
                        case '>': sb.Append(""); break;
                        case '<': sb.Append(""); break;
                        case '+': sb.Append(""); break;
                        case '|': sb.Append(""); break;
                        case '-': sb.Append(""); break;                       
                        default: sb.Append(inputstring[i]); break;
                    }
                }
                sb.Replace("'", "");
            }
            return (sb.ToString());
        }
        #endregion

        #region Convert JSON
        public static string SerializeJson(object param)
        {
            string str = "";
            try
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //js.MaxJsonLength = Int32.MaxValue;
                // js = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize();
                //str = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(param);
                //str = js.Serialize(param);
                 JsonSerializerOptions jso = new JsonSerializerOptions();
                jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                str = System.Text.Json.JsonSerializer.Serialize(param, jso);
            }
            catch { }
            return str;
        }

        public static JsonSerializerOptions jso()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return jso;
        }

        #endregion

        #region doi phut ra gio
        public static string Ret_Hour(int minutes)
        {
            string str = "";
            int HH = 0;
            int min = 0;
            if (minutes > 59)
            {
                HH=(minutes / 60);
                min=(minutes % 60);
                str += (HH > 9 ? HH.ToString() : "0" + HH.ToString()) + ":";
                str += min > 9 ? min.ToString() : "0" + min.ToString();
            }
            else
            {
                str += minutes.ToString() + "'";
            }
            return str;
        }
        #endregion

        #region redview ascii format
        public static string Redview_ascii_format(string inputstring, int maxlength)
        {
            inputstring = (string.IsNullOrEmpty(inputstring) ? "0" : inputstring);
            StringBuilder sb = new StringBuilder();
            if ((inputstring != null) && (inputstring != string.Empty))
            {
                inputstring = inputstring.Trim();
                if (inputstring.Length > maxlength)
                {
                    inputstring = inputstring.Substring(0, maxlength);
                }
                for (int i = 0; i < inputstring.Length; i++)
                {
                    switch (inputstring[i])
                    {
                        case ':': sb.Append("A"); break;
                        case ';': sb.Append("B"); break;
                        case '<': sb.Append("C"); break;
                        case '=': sb.Append("D"); break;
                        case '>': sb.Append("E"); break;
                        case '?': sb.Append("F"); break;
                        case '#': sb.Append(""); break;
                        default: sb.Append(inputstring[i]); break;
                    }
                }
            }
            return (sb.ToString());
        }
        #endregion

        #region Chuyển HexToNum
        /// <summary>
        /// Chuen Hex ==> Int
        /// </summary>
        /// <param name="sHex">Chuỗi Hex</param>
        /// <param name="loai">0: TK    1: review   2: meitrack   21:meiligao   3: C2S</param>
        /// <returns></returns>
        public static int HexToNum(string sHex, int loai)
        {
            int num = 0;
            string svalue = "0";
            if (loai == 1)
            {
                svalue = Redview_ascii_format(sHex, sHex.Length);
            }
            else
            {
                svalue = sHex;
            }
            try
            {                
                num = int.Parse(svalue, System.Globalization.NumberStyles.HexNumber);
            }
            catch { }
            return num;
        }
        #endregion

        #region Hex2Bin
        /// <summary>
        /// Hex2Bin
        /// </summary>
        /// <param name="strHexValue">Hex</param>
        /// <param name="byteLength">Length byte</param>
        /// <returns></returns>
        public static string HexToBin(string strHexValue)
        {
            string val = string.Empty;
            try
            {
                foreach (char ch in strHexValue)
                {
                    val += Ret_doidai(Convert.ToString(Convert.ToInt32(ch.ToString(), 16), 2), 4);
                }
            }
            catch { }
            return val;
        }
        #endregion     

        #region Hex2Bin
        public static string Hex2Bin(string strHex,int devType)
        {
            string str = "";
            try
            {
                switch (devType)
                {                    
                    case 7:
                        str = HexToBin(strHex.Substring(0, 2)).Substring(3, 5);
                        break;                    
                    default:
                        str = strHex.Substring(0, 5);
                        break;
                }
            }
            catch
            {
                str = "00000";
            }
            return str;
        }
#endregion

        #region CreatedTime
        /// <summary>
        /// Tao chuỗi ngày
        /// </summary>
        /// <param name="date">yyyymmdd</param>       
        /// <returns></returns>
        public static string CreatedArrayTime(string date)
        {
            string str = "";
            for (int i = 0; i < 24; i++)
            {
                str += date;
                str += (i > 9 ? i.ToString() : "0" + i);
                str += "00" + ",";
                for (int k = 1; k < 6; k++)
                {
                    str += date;
                    str += (i > 9 ? i.ToString() : "0" + i);
                    str += k + ",";

                }
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }
        #endregion

        #region CreatedDate
        /// <summary>
        /// Tao chuỗi ngày
        /// </summary>
        /// <param name="date">yyyymmdd</param>       
        /// <returns></returns>
        public static string CreatedArrayDate(int imonth,int iyear)
        {
            string str = "";
            int soNgayTrongThang=DateTime.DaysInMonth(iyear,imonth);
            for (int i = 0; i <= soNgayTrongThang; i++)
            {
                str += i > 9 ? i.ToString() : "0" + i.ToString();
                str += ",";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }
        #endregion

        #region Get A Double Maximun in Array
        public static double GetDoubleMax(double[] arrayDouble)
        {
            double num = 0;
            double max = 0;
            for (int i = 0; i < arrayDouble.Length; i++)
            {                
                if (arrayDouble[i] > max)
                {                    
                    num = arrayDouble[i];
                    max = num;
                }
            }
           
            return num;
        }
        #endregion
        
        #region WriterLog
        public static void writerLog(string sClassName, string sFunctionName, string sError, string logFolder)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~") + "//" + logFolder;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "error" + DateTime.Now.ToString("yyMMdd") + ".txt";
                StreamWriter swFromFile = new StreamWriter(Path.Combine(path, fileName), true);
                swFromFile.WriteLine("----------------------------------------------------------");
                swFromFile.WriteLine("[Date & Time]\t\t" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "");
                swFromFile.WriteLine("[Classes Name]\t\t" + sClassName.Trim() + "");
                swFromFile.WriteLine("[Functions Name]\t" + sFunctionName.Trim() + "");
                swFromFile.WriteLine("[Description Error]\t" + sError.Trim() + "");
                //swFromFile.WriteLine("END-----------------------------------------------------------");
                swFromFile.Flush();
                swFromFile.Close();
            }
            catch { }
        }
        #endregion WriterLog

        #region Hex2Int
        /// <summary>
        /// Hex to Int
        /// </summary>
        /// <param name="distanceHex"></param>
        /// <param name="loai">0: Redview</param>
        /// <returns></returns>
        public static int Hex2Int(string Hex, int loai)
        {
            int num = 0;
            string distancevalue = "0";

            if (loai == 1)
                distancevalue = Redview_ascii_format(Hex, Hex.Length);
            else
                distancevalue = Hex;

            try
            {
                num = int.Parse(distancevalue, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception e)
            {
                Lib.writerLog("Lib", "Hex2Int", "Convert Hex to Decimal: " + e.Message, "error");
            }
            return num;
        }


        #endregion
        
        #region Hex2Double
        public static double Hex2Double(string Hex)
        {
            double num = 0;
            try
            {
                num = double.Parse(Hex, System.Globalization.NumberStyles.HexNumber);
            }
            catch { }
            return num;
        }
        #endregion
        
        #region CreaterFile
        /// <summary>
        /// Write a file Json return defualt value False
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="path">Path</param>
        /// <param name="data">Json File Data</param>
        /// <returns></returns>
        public static void WriteFileJson(ref String error,String fileName, String data)
        {
            
            error = "Fialure";
            try
            {

                error = "Success";
                File.WriteAllText(fileName, data, Encoding.Unicode);
                
            }
            catch(Exception ex)
            {
                Lib.writerLog("Lib", "WriteFileJson", ex.Message, "Error");
            }
            
        }
        public static void WriteFilePDF(ref String error, String fileName, byte[] data)
        {

            error = "Fialure";
            try
            {

                error = "Success";
                //File.WriteAllBytes(fileName, data);

                //Set password pdf file
                PdfControl.SetEncrypt(data, fileName, "8818", "8818");

            }
            catch (Exception ex)
            {
                Lib.writerLog("Lib", "WriteFilePDF", ex.Message, "Error");
            }

        }
        #endregion

        #region Encoding
        public static String Base64Encoding(String data)
        {
            var b = Encoding.UTF8.GetBytes(Lib.get_value_str(data));
            return Convert.ToBase64String(b);
        }

        public static String Base64Decoding(String base64EncodingData)
        {
            var b = Convert.FromBase64String(base64EncodingData);
            String kq = Encoding.UTF8.GetString(b);
            return Lib.get_value_str(kq);
        }
        #endregion End Encoding

        #region Convert UTC to Location Time
        /// <summary>
        /// Convert UTC to Location Time
        /// </summary>
        /// <param name="timeUTC">YYYY/MM/DD HH:MM:SS</param>
        /// <returns></returns>
        public static DateTime Get_LocationTime(String timeUTC)
        {
            DateTime d = DateTime.Now;
            String[] utcs = Lib.get_value_str(timeUTC).Split('/', '-', ':', ' ');
            if (utcs.Length == 6)
            {
                int year = Lib.get_value_int(utcs[0]);
                int month = Lib.get_value_int(utcs[1]);
                int day = Lib.get_value_int(utcs[2]);
                int hour = Lib.get_value_int(utcs[3]);
                int mintute = Lib.get_value_int(utcs[4]);
                int secod = Lib.get_value_int(utcs[5]);
                TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOfset = currentTimeZone.GetUtcOffset(d);
                d = new DateTime(year, month, day, hour, mintute, secod);
                d.AddHours(currentOfset.Hours);
            }
            else
                d = new DateTime(1900, 1, 1, 0, 0, 0);

            return d;

        }
        /// <summary>
        /// Convert Location Time to UTC
        /// </summary>
        /// <param name="date">YYYY/MM/DD HH:MM:SS</param>
        /// <returns></returns>
        public static DateTime Get_UTCTime(String date)
        {
            DateTime d = DateTime.Now;
            String[] utcs = Lib.get_value_str(date).Split('/', '-', ':', ' ');
            if (utcs.Length == 6)
            {
                int year = Lib.get_value_int(utcs[0]);
                int month = Lib.get_value_int(utcs[1]);
                int day = Lib.get_value_int(utcs[2]);
                int hour = Lib.get_value_int(utcs[3]);
                int mintute = Lib.get_value_int(utcs[4]);
                int secod = Lib.get_value_int(utcs[5]);
                TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
               
                d = new DateTime(year, month, day, hour, mintute, secod);
                d = currentTimeZone.ToUniversalTime(d);
            }
            else
                d = new DateTime(1900, 1, 1, 0, 0, 0);

            return d;
        }
        #endregion End Convert UTC to Location Time

        #region CompareDateInBetween
        /// <summary>
        /// Return Boolean
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="sdate">MM/dd/yyyy or MM-dd-yyyy</param>
        /// <returns></returns>
        public static Boolean CompareDateInBetween(HttpContext context, String sdate)
        {
            Boolean kq = true;
            HttpRequest request = context.Request;
            try
            {

                string[] array = sdate.Split('/', '-');


                //data date
                int iYear = Lib.get_value_int(array[2]);
                int iMonth = Lib.get_value_int(array[0]);
                int iDay = Lib.get_value_int(array[1]);

                //From date
                int fYear = Lib.get_value_int(request["fYear"].ToString());
                int fMonth = Lib.get_value_int(request["fMonth"].ToString());
                int fDay = Lib.get_value_int(request["fDay"].ToString());

                //To Date
                int tYear = Lib.get_value_int(request["tYear"].ToString());
                int tMonth = Lib.get_value_int(request["tMonth"].ToString());
                int tDay = Lib.get_value_int(request["tDay"].ToString());

                if ((fYear * fMonth * fDay * tYear * tMonth * tDay) == 0)
                    kq = false;
                else
                {

                    DateTime d = new DateTime(iYear, iMonth, iDay);
                    DateTime dateFrom = new DateTime(fYear, fMonth, fDay);
                    DateTime dateTo = new DateTime(tYear, tMonth, tDay);

                    TimeSpan t1 = d - dateFrom;
                    TimeSpan t2 = dateTo - d;//                    
                    if (t1.TotalDays < 0 || t2.TotalDays < 0)
                    {
                        kq = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Lib.writerLog("LIB", "CompareDateInBetween", ex.Message, "error");
            }
            return kq;
        }

        /// <summary>
        /// Return Boolean
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="sdate">MM/dd/yyyy or MM-dd-yyyy</param>
        /// <returns></returns>
        public static Boolean CompareDateInBetween(String sdate, String edate)
        {
            Boolean kq = false;            
            try
            {

                string[] arrayS = sdate.Split('/', '-');
                string[] arrayE = edate.Split('/', '-');

                //start date
                int sYear = Lib.get_value_int(arrayS[2]);
                int sMonth = Lib.get_value_int(arrayS[0]);
                int sDay = Lib.get_value_int(arrayS[1]);

                //end date
                int eYear = Lib.get_value_int(arrayE[2]);
                int eMonth = Lib.get_value_int(arrayE[0]);
                int eDay = Lib.get_value_int(arrayE[1]);

                

                if ((eYear * eMonth * eDay * sYear * sMonth * sDay) == 0)
                    kq = false;
                else
                {

                    DateTime sDate = new DateTime(sYear, sMonth, sDay);
                    DateTime eDate = new DateTime(eYear, eMonth, eDay);


                    TimeSpan t = sDate - eDate;
                                  
                    if (t.TotalDays > 0)
                    {
                        kq = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Lib.writerLog("LIB", "CompareDateInBetween", ex.Message, "error");
            }
            return kq;
        }
        #endregion End CompareDateInBetween

        public static String ChangedValue(String name, String valueOld, String valueNew)
        {
            String kq = " Changed " + name + " from " + (valueOld.Equals("0") || String.IsNullOrEmpty(valueOld) ? "empty" : valueOld);
            kq += " to " + (valueNew.Equals("0") || String.IsNullOrEmpty(valueNew) ? "empty" : valueNew);
            return kq;
        }

        #region Convert String to List<int>
        public static List<int> ListNumber(String str)
        {
            List<int> list = new List<int>();
            String[] array = Lib.get_value_str(str).Split(',');
            foreach(string s in array)
            {
                int value = 0;
                if (int.TryParse(s, out value))
                    list.Add(value);
            }
            return list;
        }
        #endregion

        #region Ngay nghi
        public static Boolean DayOff(String[] days,DateTime d)
        {
            Boolean kq = false;
            if (days.Length.Equals(7))
            {
                for (int i = 0; i < 7; i++)
                {
                    int dayOfWeek = (int)d.DayOfWeek;
                    int value = Lib.get_value_int(days[dayOfWeek]);
                    if (i.Equals(dayOfWeek) && value.Equals(0))
                        kq = true;
                }
            }            
            return kq;
        }

        public static DateTime DayWorking(String[] days, DateTime d)
        {
            DateTime date = new DateTime();

            if (Lib.DayOff(days, d))
            {
                bool kq = true;
                int i = 0;
                while (kq)
                {
                    date = d.AddDays(i);
                    if (!Lib.DayOff(days, date))
                    {
                        kq = false;
                    }
                    i++;
                }
            }
            else
                date = d;
            return date;
        }

        public static DateTime GetMonday(DateTime d)
        {
            DateTime date = new DateTime();


            bool kq = true;
            int i = 0;
            while (kq)
            {
                date = d.AddDays(i);
                if (((int)date.DayOfWeek).Equals(1))
                {
                    kq = false;
                }
                i++;
            }

            return date;
        }
        #endregion

        #region Tel US Format (012)345-6789
        public static String Set_Tel(this String tel)
        {
            String kq = String.Empty;
            try
            {
                kq = tel.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                if (kq.Length.Equals(10))
                    kq = "(" + kq.Substring(0, 3) + ")" + kq.Substring(3, 3) + "-" + kq.Substring(6);

            }
            catch { }

            return kq;
        }
        #endregion

        public static String RetFileNameByType(String type, String selectionId, String companyId, int donorSelectionType, int scheduleType)
        {
            String kq = String.Empty;
            type = type.ToUpper();
            switch (type)
            {
                case FieldVaules.BaseList:
                    kq = "BaseListMaintained_";
                    break;
                case FieldVaules.NotificationLetters:
                    kq = "NotificationLetters_";
                    break;
                case FieldVaules.RandomSummary:
                    kq = "RandomSummary_";
                    break;
                case FieldVaules.RandomList:
                    kq = "RandomList_";// "_" + Lib.RetDonorSelectionType(donorSelectionType) + ".pdf";
                    break;
                case FieldVaules.Notificationslip:
                    kq = "Notificationslip_";
                    break;
            }
            kq += selectionId;
            //if (type.Equals(FieldVaules.BaseList))
            //{
            //    if(donorSelectionType.Equals(0))
            //        kq += "_" + companyId;
            //}
            //else
            //{
            if (scheduleType.Equals(1))
                kq += "_" + companyId;
            //}
            if (type.ToUpper().Equals(FieldVaules.RandomList))
                kq += "_" + Lib.RetDonorSelectionType(donorSelectionType);
            kq += ".pdf";
            return kq;
        }

        public static String RetFileNameByType(String type, String selectionId, int donorSelectionType)
        {
            String kq = String.Empty;
            switch (type.ToUpper())
            {
                case FieldVaules.BaseList:
                    kq = "BaseListMaintained_";
                    break;
                case FieldVaules.NotificationLetters:
                    kq = "NotificationLetters_";
                    break;
                case FieldVaules.RandomSummary:
                    kq = "RandomSummary_";
                    break;
                case FieldVaules.RandomList:
                    kq = "RandomList_";// "_" + Lib.RetDonorSelectionType(donorSelectionType) + ".pdf";
                    break;
                case FieldVaules.Notificationslip:
                    kq = "Notificationslip_";
                    break;
            }
            kq += selectionId;
            if (type.ToUpper().Equals(FieldVaules.RandomList))
                kq += "_" + Lib.RetDonorSelectionType(donorSelectionType);
            kq += ".pdf";
            return kq;
        }

        public static String RetDonorSelectionType(int type)
        {
            String kq = String.Empty;
            switch (type)
            {
                case 0:
                    kq = "Alternate";
                    break;
                case 1:
                    kq = "Selection";
                    break;
                case 2:
                    kq = "Both";
                    break;
            }

            return kq;
        }

        public static String Signature(MyOrganization myOrg)
        {
            String kq = String.Empty;
            kq += "<div style='margin-top:30px;'>";
            kq += "<p><strong><span style='font-size:12px;'>";
            kq += "Thx <br>" + myOrg.LastName + "&nbsp;" + myOrg.FirstName + "&nbsp;(MBA)";
            kq +="</span><br>";
            kq += "<span>" + myOrg.CompanyName + "</span><br>";
            kq += "<span>" + myOrg.Address + "</span><br>";
            kq += "<span>" + myOrg.City + ", " + myOrg.State + " " + myOrg.Zip + "</span><br>";
            kq += "<span>Phone: " + myOrg.MobilePhone + "</span><br>";
            kq += "<span>Fax: " + myOrg.WorkPhone + "</strong></p>";
            kq += "</div>";
            kq += myOrg.Notice;
            return kq;
        }

        public static int CalAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;

            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            return years;
        }

        public static String Get_Sates_hash(String path,String states)
        {
            String kq = String.Empty;
            var js = File.ReadAllText(path);
            var json = JObject.Parse(js);
            foreach (var s in json)
            {
                String s1 = Lib.get_value_str(s.Value.ToString()).ToLower();
                if (s1.Equals(states.ToLower()))
                    kq = s.Key.ToString();

            }
            return kq;

        }
    }



}

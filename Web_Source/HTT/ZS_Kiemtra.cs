using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;

namespace HTT
{
    public class ZS_Kiemtra
    {
        public ZS_Kiemtra()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region giải phóng session
        /// <summary>
        /// xóa session
        /// </summary>
        public void DropSession()
        {
            HttpContext.Current.Session["id"] = null;
            HttpContext.Current.Session["viewall"] = null;
            HttpContext.Current.Session["phongban"] = null;
            HttpContext.Current.Session["coso"] = null;
            HttpContext.Current.Session.Abandon();
        }
        #endregion

        #region kiểm tra chuỗi có chứa ký tự lạ ko
        /// <summary>
        /// Kiểm tra ký tự lạ ,|&|+|'|\"|or|
        /// </summary>
        /// <param name="str"></param>
        public static void check_str(string str)
        {

            string Illegal_Str = ",|&|+|'|\"|or|";
            string new_str = str.ToLower();
            string[] newstr = Illegal_Str.Split('|');
            for (int i = 0; i < (newstr.Length - 1); i++)
            {
                if (new_str.IndexOf(newstr[i]) != -1)
                {
                    HttpContext.Current.Response.Write("<script>alert('Chuỗi có chứa ký tự không cho phép');history.back()</script>");
                    HttpContext.Current.Response.End();
                }
            }
        }
        #endregion

        #region xuất thông báo lỗi
        /// <summary>
        /// Hiện thị thông báo
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        public static void Show_Msg(string msg)
        {

            HttpContext.Current.Response.Write("<script>alert(\"" + msg + "\");history.back();</script>");
            HttpContext.Current.Response.End();

        }
        /// <summary>
        /// Thông báo, chuyển qua trang khác
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void Show_Msg(string msg, string url)
        {
            HttpContext.Current.Response.Write("<script>alert(\"" + msg + "\");location.href='" + url + "';</script>");
        }
        #endregion

        #region kiểm tra chuỗi có phải là chuồi số
        /// <summary>
        /// Kiểm tra chuỗi có phải là ký tự số ko
        /// true chuỗi ký tự số
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Ret_Num(string str)
        {
            if (str == "" || str == null)
            {

                return false;

            }
            else
            {

                System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
                return reg1.IsMatch(str);
            }
        }
        #endregion

        #region kiểm tra đăng nhập
       /// <summary>
       /// Kiểm tra đăng nhập
       /// </summary>
       /// <param name="pagers"></param>
       /// <returns>0 chưa đăng nhập</returns>
        public static int Exec_check(int pagers)
        {
            int num = 0;
            if (HttpContext.Current.Session["id"] != null)
            {                
                //string[] idic = (string[])HttpContext.Current.Session["dic"].ToString().Split('|');
                num = 1;// Lib.get_value_int(idic[pagers]);
            }            
            return num;
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
                        default: sb.Append(inputstring[i]); break;
                    }
                }
                sb.Replace("'", "");
            }
            return (sb.ToString());
        }
        #endregion       

        #region kiểm tra dữ liệu giống nhau
        /// <summary>
        /// Kiểm tra dữ liệu giống nhau sql có = True
        /// </summary>
        /// <param name="sql">có = True</param>
        /// <returns></returns>
        public static bool IsAgain(string sql)
        {
            bool isok = false;
            
            return isok;
        }
        #endregion        

        #region loc bo ky tu la
        /// <summary>
        /// loc bo ky tu la
        /// </summary>
        /// <param name="str">chuoi can loc</param>
        /// <returns></returns>
        public static string newstr(string str)
        {
            //Loc or,and ',&,+,,,'', 
            String nstr = null;
            if (!string.IsNullOrEmpty(str))
            {
                nstr = str.Replace("'", "").Replace("&", "").Replace(",", "").Replace("''", "");
            }
            return nstr;
        }
        #endregion

        #region quyền thao tác
        public static bool quyenhan(int column, string[] diction)
        {
            bool isok = false;
            int tcolumn = column - 1;
            if (diction[tcolumn].ToString().Trim() == "1")
            {
                isok = true;
            }
            return isok;
        }
        #endregion
        
        #region Lic
        public static bool lic()
        {
            bool isok = false;
            int inam = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (inam > 2011 && month > 6)
            {
                isok = true;
            }
            return isok;
        }
        #endregion
    }
}
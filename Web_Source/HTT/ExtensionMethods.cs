using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTT;

namespace ExtensionMethods
{
    public static class MyExtension
    {

        #region Replace whitespace to empty
        public static String replaceWhiteSpaceToEmpty(this String str)
        {
            return Lib.get_value_str(str).Replace(" ", "");
        }
        #endregion
    }
}

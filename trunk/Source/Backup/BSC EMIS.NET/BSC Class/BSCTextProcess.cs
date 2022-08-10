using System;
using System.Collections.Generic;
using System.Text;

namespace BSC_HRM.NET.BSC_Class
{
    public class BSCTextProcess
    {
        public static string ToStandardize(string sText)
        {
            sText = sText.Trim();
            if (sText.Length < 1)
                return "";
            string[] STen = sText.Split(' ');
            sText = "";
            for (int i = 0; i < STen.Length; i++)
            {
                try
                {
                    sText += " " + STen[i].Trim();
                }
                catch (System.Exception ex)
                {
                    BSCException.LogException(ex);
                    return "";
                }
            }
            return sText.Trim();
        }
        public static string ToStandardizeName(string sText)
        {
            sText = sText.ToLower().Trim();
            if (sText.Length < 1)
                return "";
            string[] STen = sText.Split(' ');
            sText = "";
            for (int i = 0; i < STen.Length; i++)
            {
                try
                {
                    sText += " " + STen[i].Substring(0, 1).ToUpper() + STen[i].Substring(1).ToLower().Trim();
                }
                catch (System.Exception ex)
                {
                    BSCException.LogException(ex);
                    return "";
                }
            }
            return sText.Trim();
        }

        public static string Xoadau(string text)
        {
            string[] Vietnamese = new string[]
                {
                    "aAeEoOuUiIdDyY",
                    "áàạảãâấầậẩẫăắằặẳẵ",
                    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ",
                    "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ",
                    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ",
                    "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ",
                    "ÍÌỊỈĨ",
                    "đ",
                    "Đ",
                    "ýỳỵỷỹ",
                    "ÝỲỴỶỸ"
                };
            for (int i = 1; i < Vietnamese.Length; i++)
            {
                for (int j = 0; j < Vietnamese[i].Length; j++)
                    text = text.Replace(Vietnamese[i][j], Vietnamese[0][i - 1]).Trim();
            }
            return text;
        }
    }
}

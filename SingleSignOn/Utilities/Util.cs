using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingleSignOn.Models;
using System.Net;
using System.Web.Routing;
using System.Text;

namespace SingleSignOn.Utilities
{
    public static class Util
    {
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            //What is Entity Framework??
            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href='");
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }

            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + Encrypt(queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("");
            return new MvcHtmlString(ancor.ToString());
        }

        public static string Encrypt(string plainText)
        {
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(inputByte.ToArray());
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] inputByte = new byte[encryptedText.Length];
            inputByte = Convert.FromBase64String(encryptedText);
            return HttpUtility.HtmlDecode(Encoding.UTF8.GetString(inputByte.ToArray()));
        }

        public static string FormatNumber(int number)
        {
            var str1 = number.ToString();
            var str2 = "";
            for (; str1.Length > 3; str1 = str1.Substring(0, str1.Length - 3))
                str2 = "," + str1.Substring(str1.Length - 3) + str2;
            return str1 + str2;
        }

        public static string FormatNumber(string number)
        {
            var str1 = number;
            var str2 = "";
            for (; str1.Length > 3; str1 = str1.Substring(0, str1.Length - 3))
                str2 = "," + str1.Substring(str1.Length - 3) + str2;
            return str1 + str2;
        }

        public static string GetNameEmp(string idE)
        {
            if (string.IsNullOrEmpty(idE))
                return "-.0";
            var s = new PORTALEntities().HrEmpMasters.FirstOrDefault(c => c.Code == idE);
            return s != null ? s.LocalName + "." + s.DeptCode : "-.0";
        }

        public static string GetDeptCode(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "-";
            var d = new PORTALEntities().HrEmpMasters.FirstOrDefault(c => c.Code == id);
            return d != null ? d.DeptCode.ToString() : "-";
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static int IP2INT(string IP)
        {
            return BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes().Reverse().ToArray(), 0);
            //return BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes(), 0);
        }

        public static string INT2IP(int IntIP)
        {
            return new IPAddress(BitConverter.GetBytes(IntIP).Reverse().ToArray()).ToString();
            //return new IPAddress(BitConverter.GetBytes(IntIP)).ToString();
        }
    }
}
using System;
using System.Text;
using Sand.Security.Cryptography;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace Application.Common
{
    public class Utility
    {
        #region "Constructor"
        public Utility()
        {
        }
        #endregion

        public string Encrypt(string Parameter)
        {
            Encryptor Encry = new Encryptor(EncryptionAlgorithm.TripleDes);
            Encry.IV = Encoding.ASCII.GetBytes("t3ilc0m3");
            return Encry.Encrypt(Parameter, "3wmotherwdrtybnio12ewq23");
        }

        public string Decrypt(string Parameter)
        {
            Decryptor Decry = new Decryptor(EncryptionAlgorithm.TripleDes);
            Decry.IV = Encoding.ASCII.GetBytes("t3ilc0m3");
            return Decry.Decrypt(Parameter, "3wmotherwdrtybnio12ewq23");
        }

        public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public void SetSession()
        {
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
        }
        public static DateTime GetDateFromMMDDYYYY(string strDate, char separator='/')
        {
            int[] numArray = new int[3];
            if (string.IsNullOrEmpty(strDate))
            {
                throw new Exception("Date is empty.");
            }
            strDate = strDate.Trim();

            string[] dateString = strDate.Split(separator);
            return new DateTime(Convert.ToInt32(dateString[2]), Convert.ToInt32(dateString[0]), Convert.ToInt32(dateString[1]));
      
        }
        /// <summary>
        /// Gets the date from ddmmyyyy.
        /// </summary>
        /// <param name="strDate">The string date.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Date is empty.
        /// or
        /// Date is not complete.
        /// or
        /// Wrong format of the date string.
        /// or
        /// Wrong format of the date string.
        /// or
        /// Wrong format of the date string.
        /// </exception>
        public static DateTime GetDateFromDDMMMYYYY(string strDate)
        {
            int[] numArray = new int[3];
            Dictionary<string, int> monthsNames = new Dictionary<string, int>(){
            {"JAN",01},{"FEB",02}, {"MAR",03}, {"APR",04}, {"MAY",05}, {"JUN",06},{"JUL",07},{"AUG",08},{"SEP",09},{"OCT",10},{"NOV",11},{"DEC",12}
            };
            if (string.IsNullOrEmpty(strDate))
            {
                throw new Exception("Date is empty.");
            }
            strDate = strDate.Trim(); //10-jan-2014
            // strDate[]
            if (strDate.Length != 11)
            {
                throw new Exception("Date is not complete.");
            }
            if ((strDate[2] != '-') || (strDate[6] != '-'))
            {
                throw new Exception("Wrong format of the date string.");
            }
            string[] strArray = strDate.Split(new char[] { '-' });
            if (strArray.Length != 3)
            {
                throw new Exception("Wrong format of the date string.");
            }
            int monthNum = monthsNames[strArray[1].ToUpper()];
            strArray[1] = monthNum.ToString();
           
            for (int i = 0; i < 3; i++)
            {
               
                    if (!isNumeric(strArray[i]))
                    {
                        throw new Exception("Wrong format of the date string.");
                    }
                    numArray[i] = int.Parse(strArray[i]);
                
            }
            return new DateTime(numArray[2], numArray[1], numArray[0]);
        }
        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool isNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            Regex regex = new Regex("[^0-9]");
            if (regex.IsMatch(value))
            {
                return false;
            }
            return true;
        }


    }
    /// <summary>
    /// class for handling Byte Order Mark problems when generating reports using xsl transformation for xml
    /// </summary>
    public class XmlEncodingBOM
    {
        public string EncDescription;
        public Encoding TextEncoding;
        public int BOMLength;
        public byte[] ByteSequnce;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncodingBOM" /> struct.
        /// </summary>
        /// <param name="ED">The ed.</param>
        /// <param name="TE">The te.</param>
        /// <param name="BL">The bl.</param>
        /// <param name="BS">The bs.</param>
        private XmlEncodingBOM(string ED, Encoding TE, int BL, byte[] BS)
        {
            this.EncDescription = ED;
            this.TextEncoding = TE;
            this.BOMLength = BL;
            this.ByteSequnce = BS;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncodingBOM"/> class.
        /// </summary>
        XmlEncodingBOM()
        {
            DefaultXmlEncodings = new XmlEncodingBOM[10]{
			    new XmlEncodingBOM("UTF-8", Encoding.UTF8, 3, new byte[3]{0xEF, 0xBB, 0xBF}),
			    new XmlEncodingBOM("UTF-16", Encoding.BigEndianUnicode, 2, new byte[2]{0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-16", Encoding.Unicode, 2, new byte[2]{0xFF, 0xFE}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0x00, 0x00, 0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0xFF, 0xFE, 0x00, 0x00}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x38}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x39}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2B}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2F}),
			    new XmlEncodingBOM("ASCII", Encoding.ASCII, 0, null)
            };

        }
        static XmlEncodingBOM[] DefaultXmlEncodings = new XmlEncodingBOM[10]{
			    new XmlEncodingBOM("UTF-8", Encoding.UTF8, 3, new byte[3]{0xEF, 0xBB, 0xBF}),
			    new XmlEncodingBOM("UTF-16", Encoding.BigEndianUnicode, 2, new byte[2]{0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-16", Encoding.Unicode, 2, new byte[2]{0xFF, 0xFE}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0x00, 0x00, 0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0xFF, 0xFE, 0x00, 0x00}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x38}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x39}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2B}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2F}),
			    new XmlEncodingBOM("ASCII", Encoding.ASCII, 0, null)
            };
        /// <summary>
        /// Gets the encoding bom.
        /// </summary>
        /// <param name="ByteArray">The byte array.</param>
        /// <returns></returns>
        public static XmlEncodingBOM GetEncodingBOM(byte[] ByteArray)
        {
            int bomIndex = -1;
            int bomLength = 0;
            for (int i = 0; i < DefaultXmlEncodings.Length; i++)
            {
                if (ByteArray.Length < DefaultXmlEncodings[i].BOMLength) break;
                bool isTheOne = true;
                for (int x = 0; x < DefaultXmlEncodings[i].BOMLength; x++)
                {
                    if (DefaultXmlEncodings[i].ByteSequnce[x] != ByteArray[x]) isTheOne = false;
                }
                if (isTheOne)
                {
                    if (DefaultXmlEncodings[i].BOMLength >= bomLength)
                    {
                        bomLength = DefaultXmlEncodings[i].BOMLength;
                        bomIndex = i;
                    }
                }
            }

            if (bomIndex < 0)
            {
                return (new XmlEncodingBOM("UTF-8", Encoding.UTF8, 0, null));
            }
            else
            {
                return (DefaultXmlEncodings[bomIndex]);
            }
        }
        /// <summary>
        /// Gets the bom string.
        /// </summary>
        /// <param name="ByteArray">The byte array.</param>
        /// <returns></returns>
        public static string GetBOMString(byte[] ByteArray)
        {
            byte[] fileData;
            string strOut;
            XmlEncodingBOM encBOM = XmlEncodingBOM.GetEncodingBOM(ByteArray);
            int FileSize = ByteArray.Length;
            if (encBOM.BOMLength > 0)
            {
                fileData = new byte[FileSize - encBOM.BOMLength];
                Array.Copy(ByteArray, encBOM.BOMLength, fileData, 0, FileSize - encBOM.BOMLength);
            }
            else
            {
                fileData = ByteArray;
            }
            strOut = encBOM.TextEncoding.GetString(fileData);
            return strOut;
        }
    }
}

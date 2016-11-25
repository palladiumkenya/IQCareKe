using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Facility.Reports
{
    public static class ChartExtensions
    {
        public static void ExportList<T>(this IList<T> enumerableList)
        {
            ExportChartData(enumerableList);
        }

        public static void ExportChartData(IEnumerable chartData)
        {
            SaveFileDialog objSFD = new SaveFileDialog() { DefaultExt = "csv", Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 1 };
            
            if (objSFD.ShowDialog() == true)
            {
                string strFormat = objSFD.SafeFileName.Substring(objSFD.SafeFileName.IndexOf('.') + 1).ToUpper();
                StringBuilder strBuilder = new StringBuilder();

                if (chartData != null)
                {
                    List<string> lstFields = new List<string>();
                    Type type = Enumerable.SingleOrDefault<Type>(chartData.GetType().GetGenericArguments());
                    foreach (PropertyInfo info in type.GetProperties())
                    {
                        lstFields.Add(FormatField(info.Name, strFormat));
                    }
                    BuildStringOfRow(strBuilder, lstFields, strFormat);
                    Debug.WriteLine(strBuilder.ToString());
                    foreach (object cost in chartData)
                    {
                        lstFields.Clear();
                        foreach (PropertyInfo info in type.GetProperties())
                        {
                            string data = "";
                            PropertyInfo property = cost.GetType().GetProperty(info.Name.ToString());
                            if (property != null)
                            {
                                data = property.GetValue(cost, null).ToString();
                            }
                            lstFields.Add(FormatField(data, strFormat));
                        }
                        BuildStringOfRow(strBuilder, lstFields, strFormat);
                        Debug.WriteLine(strBuilder.ToString());
                    }

                    StreamWriter sw = new StreamWriter(objSFD.OpenFile());

                    if (strFormat == "XML")
                    {
                        //Let us write the headers for the Excel XML
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                        sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                        sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
                        sw.WriteLine("<Author>Subash Manickam</Author>");
                        sw.WriteLine("<Created>" + DateTime.Now.ToLocalTime().ToLongDateString() + "</Created>");
                        sw.WriteLine("<LastSaved>" + DateTime.Now.ToLocalTime().ToLongDateString() + "</LastSaved>");
                        sw.WriteLine("<Company>IQCare</Company>");
                        sw.WriteLine("<Version>12.00</Version>");
                        sw.WriteLine("</DocumentProperties>");
                        sw.WriteLine("<Worksheet ss:Name=\"Silverlight Export\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                        sw.WriteLine("<Table>");
                    }
                    sw.Write(strBuilder.ToString());
                    if (strFormat == "XML")
                    {
                        sw.WriteLine("</Table>");
                        sw.WriteLine("</Worksheet>");
                        sw.WriteLine("</Workbook>");
                    }
                    sw.Close();
                }
            }
        }

        private static void BuildStringOfRow(StringBuilder strBuilder, List<string> lstFields, string strFormat)
        {
            switch (strFormat)
            {
                case "XML":
                    strBuilder.AppendLine("<Row>");
                    strBuilder.AppendLine(String.Join("\r\n", lstFields.ToArray()));
                    strBuilder.AppendLine("</Row>");
                    break;
                case "CSV":
                    strBuilder.AppendLine(String.Join(",", lstFields.ToArray()));
                    break;
            }
        }
        private static string FormatField(string data, string format)
        {
            switch (format)
            {
                case "XML":
                    return String.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", data);
                case "CSV":
                    return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
            }
            return data;
        }
    }


}

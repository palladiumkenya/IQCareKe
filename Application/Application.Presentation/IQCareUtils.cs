using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Owc11;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace Application.Presentation
    {
    public class IQCareUtils
    {
        #region "Constructor"
        public IQCareUtils() 
        {
        }
        #endregion

        public DataView GridSort(DataTable theDT, string sortField, string sortDirection)
        {
            DataView theDV = new DataView(theDT);
            theDV.Sort = sortField +" "+ sortDirection.ToString();
            return theDV; 
        }

        public enum ChartType
        {
            PieChart,LineChart
        }

        public Bitmap DrawIQCareGraph(int theHeight,int theWidth,Hashtable theContent,Color theBGColor, ChartType Chart)
        {
            Bitmap thePic = new Bitmap(theWidth, theHeight,PixelFormat.Format64bppArgb);
            Graphics thegraph = Graphics.FromImage(thePic);
            SolidBrush theBrush = new SolidBrush(theBGColor);
            thegraph.FillRectangle(theBrush, 0, 0, theWidth, theHeight);
            theBrush.Dispose();

            // Create brushes for coloring the pie chart
            SolidBrush[] brushes = new SolidBrush[10];
            brushes[0] = new SolidBrush(Color.Yellow);
            brushes[1] = new SolidBrush(Color.Green);
            brushes[2] = new SolidBrush(Color.Blue);
            brushes[3] = new SolidBrush(Color.Cyan);
            brushes[4] = new SolidBrush(Color.Magenta);
            brushes[5] = new SolidBrush(Color.Red);
            brushes[6] = new SolidBrush(Color.Black);
            brushes[7] = new SolidBrush(Color.Gray);
            brushes[8] = new SolidBrush(Color.Maroon);
            brushes[9] = new SolidBrush(Color.LightBlue);

            if (Chart == ChartType.PieChart)
            {
                #region "PieChart"
                int i = 0;
                decimal theTotal = 0.0m;
                for (i = 1; i <= theContent.Count; i++)
                {
                    theTotal += Convert.ToDecimal(theContent[i].ToString());
                    i = i + 1;
                }

                i = 0;
                float theStart = 0.0f;
                float theEnd = 0.0f;
                theStart = theEnd;
                decimal thecurrent = 0.0m;
                Font thefont = new Font(FontFamily.GenericSerif, 10.0f, FontStyle.Bold);
                //thefont.Name = "Arial";
                float theheight = 20.0f;
                thegraph.DrawString("LEGENDS", thefont, brushes[6], 150.0f, theheight, StringFormat.GenericDefault);
                thefont.Dispose();
                Font thefont1 = new Font(FontFamily.GenericSerif, 7.0f, FontStyle.Regular);
                int j = 0;
                for (i = 1; i <= theContent.Count; i++)
                {
                    thecurrent += Convert.ToDecimal(theContent[i].ToString());
                    theStart = theEnd;
                    theEnd = (float)(thecurrent / theTotal) * 360.0f;
                    thegraph.FillRectangle(brushes[j], 150.0f, theheight + 20, 15, 15);
                    thegraph.DrawString(theContent[i + 1].ToString(), thefont1, brushes[6], 150.0f + 15.0f, theheight + 20, StringFormat.GenericDefault);
                    thegraph.FillPie(brushes[j], 25.0f, 25.0f, theWidth - 170, theHeight - 50, theStart, theEnd - theStart);
                    theheight = theheight + 20f;
                    i = i + 1;
                    j = j + 1;
                }
                #endregion
            }
            else if (Chart == ChartType.LineChart)
            {
                #region "LineChart"
                int i, theMax, thePValue;
                theMax = 0;
                thePValue = 0;
                float theInterval= 0.0f;
                for (i = 0; i < theContent.Count; i++)
                {
                    if (thePValue < Convert.ToInt32(theContent[i]))
                        if(theMax < Convert.ToInt32(theContent[i]))
                            theMax = Convert.ToInt32(theContent[i]);
                    thePValue = Convert.ToInt32(theContent[i]);
                    i = i + 1;
                }
                #region "Interval"
                if (theMax < 1000)
                {
                    theInterval = 1000f / 5f;
                }
                else if (theMax >= 1000 && theMax < 2000)
                {
                    theInterval = 2000f / 5f;
                }
                else if (theMax >= 2000 && theMax < 3000)
                {
                    theInterval = 3000f / 5f;
                }
                else if (theMax >= 3000 && theMax < 4000)
                {
                    theInterval = 4000f / 5f;
                }
                #endregion

                #region "DrawAxis"
                PointF Hpt1 = new PointF();
                Hpt1.X = theWidth - 200;
                Hpt1.Y = theHeight - 150;

                PointF Hpt2 = new PointF();
                Hpt2.X = theWidth - 200;
                Hpt2.Y = theHeight - 20;

                PointF Vpt1 = new PointF();
                Vpt1.X = theWidth - 200;
                Vpt1.Y = theHeight - 20;

                PointF Vpt2 = new PointF();
                Vpt2.X = theWidth - 20;
                Vpt2.Y = theHeight - 20;

                Pen thePen = new Pen(brushes[6]);
                thegraph.DrawLine(thePen,Hpt1,Hpt2);
                thegraph.DrawLine(thePen, Vpt1, Vpt2);
                int HInterval;
                Font theFnt = new Font(FontFamily.GenericSansSerif, 7.0f, FontStyle.Regular);  
                for (i = 0; i <= 5; i++)
                {
                    PointF yAxis = new PointF();
                    yAxis.X = theWidth - (200);
                    yAxis.Y = theHeight - (20 + 25 * i);

                    PointF yAxisMrk = new PointF();
                    yAxisMrk.X = theWidth - 205;
                    yAxisMrk.Y = theHeight - (20 + 25 * i);

                    PointF YAxisLbl = new PointF();
                    YAxisLbl.X = theWidth - 225;
                    YAxisLbl.Y = theHeight - (25 + 25 * i); 

                    thegraph.DrawLine(thePen, yAxis, yAxisMrk);
                    int tmplbl = (int)theInterval * i;
                    thegraph.DrawString(tmplbl.ToString(), theFnt, brushes[6],YAxisLbl); 
                }
                if (theContent.Count / 2 > 0)
                {
                    #region "Variables"
                    HInterval = (int)theInterval;
                    int theTags = theContent.Count / 2;
                    theInterval = 160 / theTags;
                    int j = 1;
                    int h = 0;
                    PointF PMark = new PointF();
                    PMark.X = 0;
                    PMark.Y = 0;

                    #endregion
                    for (i = 0; i < theTags; i++)
                    {
                        PointF xAxis = new PointF();
                        xAxis.X = theWidth - (200 - theInterval * i);
                        xAxis.Y = theHeight - (20);

                        PointF xAxisMrk = new PointF();
                        xAxisMrk.X = theWidth - (200 - theInterval * i);
                        xAxisMrk.Y = theHeight - (15);

                        PointF xAxisLbl = new PointF();
                        xAxisLbl.X = theWidth - (210 - theInterval * i);
                        xAxisLbl.Y = theHeight - (15);

                        thegraph.DrawLine(thePen, xAxis, xAxisMrk);
                        int tmplbl = (int)theInterval * i + 1;
                        thegraph.DrawString(theContent[j].ToString(), theFnt, brushes[6], xAxisLbl);

                        Pen GraphPn = new Pen(brushes[2]);
                        PointF GraphPoint = new PointF();
                        if (PMark.X == 0 && PMark.Y == 0)
                        {
                            PMark.X = theWidth - (200 - theInterval * i);
                            PMark.Y = theHeight - (20);
                        }
                        GraphPoint.X = theWidth - (200 - theInterval * i);
                        GraphPoint.Y = theHeight - (20.0f + (float)Convert.ToInt32(theContent[h]) / HInterval * 25);
                        thegraph.DrawLine(GraphPn, PMark, GraphPoint);

                        PMark.X = GraphPoint.X;
                        PMark.Y = GraphPoint.Y;
                        j = j + 2;
                        h = h + 2;
                    }
                }
                #endregion

                #endregion
            }
            foreach(SolidBrush CleanBrush in brushes)
                CleanBrush.Dispose();

            return thePic;                            
        }

        public Boolean NumericValues(Int32 AsciiValue)
        {
            if (AsciiValue == 8)
            {
                return true;
            }
            else if (AsciiValue >= 44)
            {
                if (AsciiValue <= 45)
                    return true;
            }
            else if (AsciiValue >= 48)
            {
                if (AsciiValue <= 57)
                    return true;
            }
            else
            {
                return false;
            }
            return false; 
        }

        public string MakeDate(string theDate)
        {
            if (theDate != "")
            {
                string theDay, theMonth, theYear;
                string[] theDT;
                theDT = theDate.Split(Convert.ToChar("-"));
                theDay = theDT[0];
                theMonth = theDT[1];
                theYear = theDT[2];
                return string.Format("{0}/{1}/{2}", theMonth, theDay, theYear);
            }
            return "01/01/1900";
        }

        public DataTable CreateTableFromDataView(DataView obDataView)
        {
            DataTable obNewDt = null;
            if (obDataView.Table != null)
            {
                obNewDt = obDataView.Table.Clone();
                int idx = 0;
                string[] strColNames = new string[obNewDt.Columns.Count];
                foreach (DataColumn col in obNewDt.Columns)
                {
                    strColNames[idx++] = col.ColumnName;
                }

                IEnumerator viewEnumerator = obDataView.GetEnumerator();
                while (viewEnumerator.MoveNext())
                {
                    DataRowView drv = (DataRowView)viewEnumerator.Current;
                    DataRow dr = obNewDt.NewRow();
                    foreach (string strName in strColNames)
                    {
                        dr[strName] = drv[strName];
                    }
                    obNewDt.Rows.Add(dr);
                }
                return obNewDt;
            }
            return obNewDt;
        }

        public DataTable CreateTimeTable(int TimeInterval)
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.String"));
            theDT.Columns.Add("Time", System.Type.GetType("System.String"));
            int theInterval = 24 * 60 / TimeInterval;
            int i = 0;
            DateTime stTime = Convert.ToDateTime("00:00:00");
            for (i = 0; i < theInterval; i++)
            {
                stTime = stTime.AddMinutes(TimeInterval); 
                DataRow theDR = theDT.NewRow();
                theDR[0] = stTime.TimeOfDay;
                theDR[1] = stTime.TimeOfDay;
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }
        public DataTable CreateHourFormat_24()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.String"));
            theDT.Columns.Add("Time", System.Type.GetType("System.String"));
            int i = 0;
            for (i = 0; i < 24; i++)
            {
                DataRow theDR = theDT.NewRow();
                
                if (i < 10)
                {
                    theDR[0] = '0' + i.ToString();
                    theDR[1] = '0' + i.ToString();
                }
                else
                {
                    theDR[0] = i.ToString();
                    theDR[1] = i.ToString();
                };
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }
        public DataTable CreateHourFormat_12()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.String"));
            theDT.Columns.Add("Time", System.Type.GetType("System.String"));
            int i = 0;
            for (i = 1; i <= 12; i++)
            {
                DataRow theDR = theDT.NewRow();
                theDR[0] = i;
                if (i < 10)
                {
                    theDR[0] = '0' + i.ToString();
                    theDR[1] = '0' + i.ToString();
                }
                else
                {
                    theDR[0] = i.ToString();
                    theDR[1] = i.ToString();
                };
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }
        public DataTable CreateMinuteFormat()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.String"));
            theDT.Columns.Add("Time", System.Type.GetType("System.String"));
            int i = 0;
            for (i = 0; i < 60; i++)
            {
                DataRow theDR = theDT.NewRow();
                
                if (i < 10)
                {
                    theDR[0] = '0' + i.ToString();
                    theDR[1] = '0' + i.ToString();
                }
                else
                {
                    theDR[0] = i.ToString();
                    theDR[1] = i.ToString();
                };
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }

        public DataTable CreateAMPM()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.String"));
            theDT.Columns.Add("Format", System.Type.GetType("System.String"));
            int i = 0;
            for (i = 0; i < 2; i++)
            {
                DataRow theDR = theDT.NewRow();
                if (i == 0)
                {
                    theDR[0] = "AM";
                    theDR[1] = "AM";
                }
                else
                {
                    theDR[0] = "PM";
                    theDR[1] = "PM";
                };
                theDT.Rows.Add(theDR);
            }
            return theDT;
        }

         public void ExportToExcel(DataTable theDT, string theFilePath, string theTemplatePath)
        {
/*
           IQCareUtils theUtils = new IQCareUtils();
            Excel.SpreadsheetClass theApp = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
            
            for (int i = 0; i < theDT.Columns.Count; i++)
            {
                theApp.ActiveSheet.Cells["1", i + 1] = theDT.Columns[i].ColumnName.ToString();
                Excel.Range theRange = (Excel.Range)theApp.ActiveSheet.Cells["1", i + 1];
                theRange.EntireRow.Font.set_Bold(true);
                theRange.EntireRow.set_ColumnWidth(20);
                theRange.BorderAround("1", Microsoft.Office.Interop.Owc11.XlBorderWeight.xlThin, Microsoft.Office.Interop.Owc11.XlColorIndex.xlColorIndexNone, "Black");
            }
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                for (int j = 0; j < theDT.Columns.Count; j++)
                {
                    theApp.ActiveSheet.Cells[(i + 2), (j + 1)] = theDT.Rows[i][j].ToString();
                    Excel.Range theRange = (Excel.Range)theApp.ActiveSheet.Cells[(i + 2), (j + 1)];
                    // theRange.ClearFormats();
                    theRange.BorderAround("1", Microsoft.Office.Interop.Owc11.XlBorderWeight.xlThin, Microsoft.Office.Interop.Owc11.XlColorIndex.xlColorIndexNone, "Black");
                    theRange.set_VerticalAlignment(Microsoft.Office.Interop.Owc11.XlVAlign.xlVAlignBottom);
                    theRange.set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignLeft);

                }
            }
          
            theApp.Export(theFilePath, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
        */
         }
        private void ExportToExcel_Windows(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public void ExportToExcel_Windows(DataTable theDT, string theFilePath, string theTemplatePath)
        {
           /* if (theDT.Rows.Count >= 1)
            {
                IQCareUtils theUtils = new IQCareUtils();
                Excel.SpreadsheetClass theApp = new Excel.SpreadsheetClass();
                for (int i = 0; i < theDT.Columns.Count; i++)
                {
                    theApp.ActiveSheet.Cells["1", i + 1] = theDT.Columns[i].ColumnName.ToString();
                    Excel.Range theRange = (Excel.Range) theApp.ActiveSheet.Cells["1", i + 1];
                    theRange.EntireRow.Font.set_Bold(true);
                    theRange.EntireRow.set_ColumnWidth(20);
                    theRange.BorderAround("1", Microsoft.Office.Interop.Owc11.XlBorderWeight.xlThin,
                                          Microsoft.Office.Interop.Owc11.XlColorIndex.xlColorIndexNone, "Black");
                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    for (int j = 0; j < theDT.Columns.Count; j++)
                    {
                        theApp.ActiveSheet.Cells[(i + 2), (j + 1)] = theDT.Rows[i][j].ToString();
                        Excel.Range theRange = (Excel.Range) theApp.ActiveSheet.Cells[(i + 2), (j + 1)];
                        // theRange.ClearFormats();
                        theRange.BorderAround("1", Microsoft.Office.Interop.Owc11.XlBorderWeight.xlThin,
                                              Microsoft.Office.Interop.Owc11.XlColorIndex.xlColorIndexNone, "Black");
                        theRange.set_VerticalAlignment(Microsoft.Office.Interop.Owc11.XlVAlign.xlVAlignBottom);
                        theRange.set_HorizontalAlignment(Microsoft.Office.Interop.Owc11.XlHAlign.xlHAlignLeft);

                    }
                }

                theApp.Export(theFilePath,
                              Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionOpenInExcel,
                              Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
            }*/
            Microsoft.Office.Interop.Excel.Application ExcelApp =
           new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);

            // Change properties of the Workbook   
            ExcelApp.Columns.ColumnWidth = 20;
            ExcelApp.Cells.ClearContents();
            // Storing header part in Excel  
            for (int i = 1; i < theDT.Columns.Count + 1; i++)
            {
                ExcelApp.Cells[1, i] = theDT.Columns[i - 1].ColumnName;
            }
            // Storing Each row and column value to excel sheet  
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                for (int j = 0; j < theDT.Columns.Count; j++)
                {
                    ExcelApp.Cells[i + 2, j + 1] = theDT.Rows[i][j].ToString();
                }
            }
            ExcelApp.Visible = true;
            ExcelApp.ActiveWorkbook.SaveCopyAs(@"" + theFilePath + "");
            ExcelApp.ActiveWorkbook.Saved = true;
            Marshal.FinalReleaseComObject(ExcelApp); 
        }
        
        private string GetIndexLetter(int idx)
        {
            string[] Alphabets = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            return Alphabets.GetValue(idx).ToString();
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public string OpenExcelFile(string theFilePath)
        {
            string theScript = "";
            theScript += "<script language = 'javascript' defer='defer' id='OpenExcelFile'>\n";
            theScript +="windowprops = 'ToolBar=no,Location=no,status=yes,scrollbars=yes,top=0,left=0,resizable=yes,width=400,height=400';\n";
            theScript += "window.open('" + theFilePath + "','',windowprops);\n";
            theScript += "</script>\n";
            return theScript;
        }
        public static void WriteCache(ref DataSet cache, DateTime lastWriteTime)
        {
            string xmlPath = GblIQCare.GetXMLPath();
            System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(xmlPath + "\\AllMasters.con");
            System.IO.FileInfo theFileInfo2 = new System.IO.FileInfo(xmlPath + "\\DrugMasters.con");
            System.IO.FileInfo theFileInfo3 = new System.IO.FileInfo(xmlPath + "\\LabMasters.con");
            if (theFileInfo1.LastWriteTime.Date != lastWriteTime.Date || theFileInfo2.LastWriteTime.Date != lastWriteTime.Date || theFileInfo3.LastWriteTime.Date != lastWriteTime.Date)
            {
                theFileInfo1.Delete();
                theFileInfo2.Delete();
                theFileInfo3.Delete();
                DataSet theMainDS = cache;
                DataSet WriteXMLDS = new DataSet();

                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Provider"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Ward"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Division"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_District"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Reason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Education"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Designation"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Employee"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Occupation"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Province"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Village"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Code"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVAIDSCareTypes"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARTSponsor"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisease"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Assessment"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Symptom"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Decode"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Feature"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Function"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisclosure"].Copy());
                //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Satellite"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LPTF"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_StoppedReason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_facility"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVCareStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_RelationshipType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_TBStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LostFollowreason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Regimen"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingTopic"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ReferredFrom"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_pmtctDeCode"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Module"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModDecode"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVSideEffects"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModCode"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Country"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Town"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["VWDiseaseSymptom"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["VW_ICDList"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_RegimenLine"].Copy());
                if (theMainDS.Tables.Contains("Users"))
                {
                    WriteXMLDS.Tables.Add(theMainDS.Tables["Users"].Copy());
                }
                try
                {
                    WriteXMLDS.WriteXml(xmlPath + "\\AllMasters.con", XmlWriteMode.WriteSchema);
                }
                catch { }
                WriteXMLDS.Tables.Clear();
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Strength"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FrequencyUnits"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Drug"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Generic"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugSchedule"].Copy());
                try
                {
                    WriteXMLDS.WriteXml(xmlPath + "\\DrugMasters.con", XmlWriteMode.WriteSchema);
                }
                catch { }

                WriteXMLDS.Tables.Clear();
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LabTest"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_TestParameter"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_LabValue"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_ParameterResult"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["LabTestOrder"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_PatientLabPeriod"].Copy());
                try
                {
                    WriteXMLDS.WriteXml(xmlPath + "\\LabMasters.con", XmlWriteMode.WriteSchema);
                }
                catch { }

                //Query Builder report
                try
                {
                    WriteXMLDS.Tables.Clear();
                    WriteXMLDS = new DataSet("QBReportList");
                    WriteXMLDS.Tables.Add(theMainDS.Tables["QueryBuilderReports"].Copy());
                    WriteXMLDS.WriteXml(xmlPath + "\\QueryBuilderReports.con", XmlWriteMode.WriteSchema);
                }
                catch { }

            }
        }
    }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.FormBuilder;
using Interface.Clinical;
using Application.Presentation;
using System.Data;

namespace IQCare.Web.UILogic
{
   public class CustomFormService
    {
       public DataSet FormFieldLabels{private set; get;}

       //to move to businessprocess
       public  List<FormSection> GetRegistrationFormSections(int featureId)
       {
           int patientId = 0;
           if (CurrentSession.Current != null && CurrentSession.Current.CurrentPatient != null)
           {
               patientId = CurrentSession.Current.CurrentPatient.Id;
           }
           IPatientRegistration IPatientCustomFormMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
           DataSet ds = IPatientCustomFormMgr.GetFieldName_and_Label(featureId, patientId);
           this.FormFieldLabels = ds;
           string[] columnNames = { "FeatureId", "SectionId", "SectionName", "SeqSection", "IsGridView", "SectionInfo" };
           DataTable dt = ds.Tables[1];
           DataTable dtSections = ds.Tables[1].DefaultView.ToTable(true, columnNames);
           int? nullInt = null;
           dtSections.DefaultView.Sort = "SeqSection Asc";
           dtSections = dtSections.DefaultView.ToTable();
           var x = (from section in dtSections.AsEnumerable()
                    select new FormSection()
                    {
                        Id = Convert.ToInt32(section["SectionId"]),
                        Name = Convert.ToString(section["SectionName"]),
                        Description = Convert.ToString(section["SectionInfo"]),
                        DeleteFlag = false,
                        Active = true,
                        Rank = Convert.ToDouble(section["SeqSection"]),
                        GridView = Convert.ToBoolean(section["IsGridView"]),
                        FeatureId = Convert.ToInt32(section["FeatureId"]),
                        FieldSet = (from row in dt.AsEnumerable()
                                    where Convert.ToInt32(row["SectionId"]) == Convert.ToInt32(section["SectionId"])
                                    select new FormField()
                                    {
                                        FeatureId = Convert.ToInt32(row["FeatureId"]),
                                        FieldLabel = row["FieldLabel"].ToString(),
                                        Rank = Convert.ToDouble(row["seq"]),
                                        SectionId = Convert.ToInt32(row["SectionId"]),
                                        FieldId = Convert.ToInt32(row["FieldId"]),
                                        PersistField = row["FieldName"].ToString(),
                                               PersistTable = row["PDFTableName"].ToString(),
                                        Field = ((Convert.ToBoolean(row["Predefined"]) == false) ?
                                           (IFormField)(new CustomFormField()
                                           {
                                               Active = true,
                                               Id = Convert.ToInt32(row["FieldId"]),
                                               Name = row["FieldName"].ToString(),
                                               Registration = true,
                                               CategoryId = row["CodeId"] == DBNull.Value ? nullInt : Convert.ToInt32(row["CodeId"]),
                                               CareEnd = false,
                                               FieldType = new Entities.Common.FieldControlType() { Id = Convert.ToInt32(row["ControlId"]), Active = true, DeleteFlag = false },
                                               BindTable = row["BindSource"].ToString()
                                           }) : (IFormField)(new PredefinedFormField()
                                           {

                                               Active = true,
                                               Id = Convert.ToInt32(row["FieldId"]),
                                               Name = row["FieldName"].ToString(),
                                               Registration = true,
                                               CategoryId = row["CodeId"] == DBNull.Value ? nullInt : Convert.ToInt32(row["CodeId"]),
                                               CareEnd = false,
                                               FieldType = new Entities.Common.FieldControlType() { Id = Convert.ToInt32(row["ControlId"]), Active = true, DeleteFlag = false },
                                               BindTable = row["BindSource"].ToString()
                                               
                                           })
                                           )

                                    }
                                                ).ToList()
                    }
                       ).ToList();

           return x;
       }
    }
}

using System;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using Entities.CCC.Baseline;

namespace IQCare.Web.CCC.OneTimeEvents
{
    public partial class OneTimeEventsTracker : System.Web.UI.Page
    {
        private string Msg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    PatientDisclosureManager disclosure = new PatientDisclosureManager();
                    INHProphylaxisManager inhProphylaxis = new INHProphylaxisManager();

                    int patientId = int.Parse(Session["PatientId"].ToString());

                    string Stage1DateValue = Stage1Date.Value;
                    string Stage2DateValue = Stage2Date.Value;
                    string Stage3DateValue = Stage3Date.Value;
                    string SexPartnerDateValue = SexPartnerDate.Value;
                    string INHStartDateValue = INHStartDate.Text;
                    bool INHCompletion = false;
                    string CompletionDate = INHCompletionDate.Text;

                    if (CompletionYes.Checked == true)
                    {
                        INHCompletion = true;
                    }
                    else if(CompletionNo.Checked == true)
                    {
                        INHCompletion = false;
                    }

                    //bool ischecked = CompletionNo.Checked;
                    //bool isnotchecked = CompletionYes.Checked;


                    if (Stage1DateValue != null)
                    {
                        disclosure.AddPatientDisclosure(patientId , 10, "Adolescents", "Stage1", DateTime.Parse(Stage1DateValue));
                    }
                    else if (Stage2DateValue!=null)
                    {
                        disclosure.AddPatientDisclosure(patientId, 10, "Adolescents", "Stage2", DateTime.Parse(Stage2DateValue));
                    }
                    else if (Stage3DateValue != null)
                    {
                        disclosure.AddPatientDisclosure(patientId, 10, "Adolescents", "Stage3", DateTime.Parse(Stage3DateValue));
                    }
                    else if (SexPartnerDateValue != null)
                    {
                        disclosure.AddPatientDisclosure(patientId, 10, "Adolescents", "SexPartner", DateTime.Parse(SexPartnerDateValue));
                    }

                    /*INHProphylaxis prophylaxis = new INHProphylaxis()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = null,
                        StartDate = DateTime.Parse(INHStartDateValue),
                        Complete = INHCompletion,
                        CompletionDate = DateTime.Parse(CompletionDate)
                    };

                    inhProphylaxis.addINHProphylaxis(prophylaxis);*/
                    //disclosure.AddPatientDisclosure(patientid, 10, "Stage1", "Stage1", DateTime.Parse(Stage1DateValue));
                }
                catch (Exception ex)
                {
                    Msg = ex.Message + ' ' + ex.InnerException;
                }
                //return Msg;
            }
        }
    }
}
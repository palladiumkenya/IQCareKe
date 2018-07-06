using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic.Adherence;
using Entities.CCC.Adherence;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucPsychosocialCircumstances : System.Web.UI.UserControl
    {
        public int PCId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                getPsychosocialCircumstances(PatientId,PatientMasterVisitId);
            }
        }
        protected void getPsychosocialCircumstances(int patientId, int patientMasterVisitId)
        {
            var PC = new AdherenceLogic();
            List<PsychosocialCircumstances> psychosocialList = PC.getPsychosocialCircumstances(patientId, PatientMasterVisitId);
            foreach (var value in psychosocialList)
            {
                PCId = value.Id;
                tbLivingWith.Text = value.LivingWith;
                tbAware.Text = value.Aware;
                rbSupportSystem.SelectedValue = value.SupportSystem.ToString();
                tbSupportSystemNotes.Text = value.SupportSystemNotes;
                rbRelationshipChanges.SelectedValue = value.RelationshipChanges.ToString();
                tbRelationshipChangesNotes.Text = value.RelationshipChangesNotes;
                rbBothered.SelectedValue = value.Bothered.ToString();
                tbBotheredNotes.Text = value.BotheredNotes;
                rbTreatedDifferently.SelectedValue = value.TreatedDifferently.ToString();
                tbTreatedDifferentlyNotes.Text = value.TreatedDifferentlyNotes;
                rbInterferenceStigma.SelectedValue = value.InterferenceStigma.ToString();
                tbInterferenceStigmaNotes.Text = value.InterferenceStigmaNotes;
                rbStoppedMedication.SelectedValue = value.StoppedMedication.ToString();
                tbStoppedMedicationNotes.Text = value.StoppedMedicationNotes;
            }
        }
    }
}
function DrawDataTable(ctrlName, arrUI) {

    if (arrUI.length > 0) {
        var table = $("#" + ctrlName).DataTable();
        table.rows.add(arrUI).draw().nodes();
    }
}
var reactionEventList = new Array();
var reactionCauseList = new Array();
var reactionSeverityList = new Array();
var reactionActionList = new Array();

function AddAdverseReaction() {

    var adverseEvent = $("#adverseEvent").val();
    var medicineCausingAE = $("#AdverseEventCause").val();
    var adverseEventSeverity = $('#ddlAdverseEventSeverity').find(":selected").text();
    var adverseEventSeverityID = $('#ddlAdverseEventSeverity').find(":selected").val();
    var adverseEventAction = $("#AdverseEventAction").find(":selected").text();
    //Validate duplication
   
    var reactionEventFound = 0;
    var reactionCauseFound = 0;
    var reactionSeverityFound = 0;
    var reactionActionFound = 0;

    if (adverseEvent == "") {
        toastr.error("Error", "Please enter adverse event");
        return false;
    }

    if (medicineCausingAE == "") {
        toastr.error("Error", "Please enter medicine causing adverse event");
        return false;
    }

    if (adverseEventSeverityID == "0") {
        toastr.error("Error", "Please enter Adverse Event Severity");
        return false;
    }

    if (adverseEventAction == "") {
        toastr.error("Error", "Please enter  Action");
        return false;
    }

    reactionEventFound = $.inArray("" + adverseEvent + "", reactionEventList);
    reactionCauseFound = $.inArray("" + medicineCausingAE + "", reactionCauseList);
    reactionSeverityFound = $.inArray("" + adverseEventSeverityID + "", reactionSeverityList);
    reactionActionFound = $.inArray("" + adverseEventAction + "", reactionActionList);

    if (reactionEventFound > -1){
    
        toastr.error("Error", adverseEvent + " Adverse Event already exists in the List");
        return false; // message box herer

   

    }
    reactionEventList.push("" + adverseEvent + "");
    reactionCauseList.push("" + medicineCausingAE + "");
    reactionSeverityList.push("" + adverseEventSeverityID + "");
    reactionActionList.push("" + adverseEventAction + "");
    arrAdverseEventUI = [];
  
    arrAdverseEventUI.push([
        adverseEventSeverityID, adverseEvent, medicineCausingAE, adverseEventSeverity, adverseEventAction,
        "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
    ]);
   
    DrawDataTable("dtlAdverseEvents", arrAdverseEventUI);

    $("#adverseEvent").val("");
    $("#AdverseEventCause").val("");
    $('#ddlAdverseEventSeverity').val("0");
    $("#AdverseEventAction").val("");
}

var chronicIllnessList = new Array();
function AddChronicIllness() {
    var chronicIllness = $('#ChronicIllnessName').find(":selected").text();
    var chronicIllnessID = $('#ChronicIllnessName').find(":selected").val();
    var illnessTreatment = $("#illnessTreatment").val();
    var treatmentDose = $('#treatmentDose').val();
    var onSet = $('#txtOnsetDate').val();
    //Validate duplication
    var chronicIllnessFound = 0;

    if (chronicIllness == "" || chronicIllness == "Select") {
        toastr.error("Error", "Please enter chronic illness");
        return false;
    }
   //if (!IsNumeric(treatmentDose)) {
   //    toastr.error("Error", "Treatment dose should be numeric.");
   //    return false;
   //}

   chronicIllnessFound = $.inArray("" + chronicIllness + "", chronicIllnessList);


   if (chronicIllnessFound > -1) {
       toastr.error("Error", "Chronic Illness already exists.");
       return false;
    }

    else {
       
       chronicIllnessList.push("" + chronicIllness + "");
        arrChronicIllnessUI = [];
        arrChronicIllnessUI.push([chronicIllnessID, chronicIllness, illnessTreatment, treatmentDose, onSet,
            "<input type='checkbox' id='chkChronic" + chronicIllnessID + "' >", "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
        
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);

        $('#ChronicIllnessName').val("0");
        $("#illnessTreatment").val("");
        $('#treatmentDose').val("");
        $('#txtOnsetDate').val("");
    }
}

var AllergyList = new Array();
function AddAllergy() {
    var allergyID = $('#txtAllergyId').val();
    var allergy = $('#txtAllergy').val();
    var allergyReactionID = $('#txtReactionTypeID').val();
    var allergyReaction = $('#txtReactionType').val();
    var severityId = $('#ddlAllergySeverity').find(":selected").val();
    var severity = $('#ddlAllergySeverity').find(":selected").text();
    var allergyDate = $("#txtAllergyDate").val();

    //Validate duplication
    var allergyFound = 0;

    if (allergy == "") {
        toastr.error("Error", "Please enter allergy");
        return false;
    }

    allergyFound = $.inArray("" + allergy + "", AllergyList);


    if (allergyFound > -1) {
        toastr.error("Error", "Allergy already exists.");
        return false;
    }

    else {

        AllergyList.push("" + allergy + "");
        arrAllergyUI = [];
        arrAllergyUI.push([allergyID, allergyReactionID, severityId, allergy, allergyReaction, severity, allergyDate, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);

        DrawDataTable("dtlAllergy", arrAllergyUI);

        $('#txtAllergy').val("");
        $("#txtReactionType").val("");
        $("#ddlAllergySeverity").val("0");
        $('#txtAllergyDate').val("");
    }
}




var vaccineList = new Array();
var vaccineStageList = new Array();
function AddVaccine() {
    var vaccine = $('#ddlVaccine').find(":selected").text();
    var vaccineID = $('#ddlVaccine').find(":selected").val();
    var vaccineStage = $('#ddlVaccineStage').find(":selected").text();
    var vaccineStageID = $('#ddlVaccineStage').find(":selected").val();
    var vaccinationDate = $('#txtVaccinationDate').val();
    //Validate duplication
    var vaccineFound = 0;
    var vaccineStageFound = 0;

    if (vaccineID == "0") {
       toastr.error("Error","Please enter vaccine")
        return false;
    }

    if (vaccineStageID == "0") {
        toastr.error("Error", "Please enter vaccine stage");
        return false;
    }
    if (vaccinationDate == "") {
        toastr.error("Error","Please enter vaccine date");
        return false;
    }
    vaccineFound = $.inArray("" + vaccine + "", vaccineList);
    vaccineStageFound = $.inArray("" + vaccineStage + "", vaccineStageList);


    if ((vaccineFound > -1) && (vaccineStageFound > -1)) {
        toastr.error("Error", "Vaccination already exists.");
        return false;
    } else {


        vaccineList.push("" + vaccine + "");
        vaccineStageList.push("" + vaccineStage + "");

    arrVaccineUI = [];
    arrVaccineUI.push([vaccineID, vaccineStageID, vaccine, vaccineStage, vaccinationDate, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    
    DrawDataTable("dtlVaccines", arrVaccineUI);

    $('#ddlVaccine').val("0");
    $("#ddlVaccineStage").val("0");
    $('#txtVaccinationDate').val("");
   }
}

var physicalExamList = new Array();

function AddPhysicalExam() {
    var examType = $('#ddlExaminationType').find(":selected").text();
    var examTypeID = $('#ddlExaminationType').find(":selected").val();
    var exam = $('#ddlExamination').find(":selected").text();
    var examID = $('#ddlExamination').find(":selected").val();
    var findings = $('#txtExamFindings').val();

    //Validate duplication
    var examFound = 0;

    if (examTypeID == "0") {
        toastr.error("Error","Please enter Examination Type");
        return false;
    }

    if (examID == "0") {
        toastr.error("Error", "Please enter Examination");
        return false;
    }

    examFound = $.inArray("" + exam + "", physicalExamList);


    if (examFound > -1) {
        toastr.error("Error", "Examination already exists.");
        return false;
    } else {


        physicalExamList.push("" + exam + "");
       

        arrPhysicalExamUI = [];
        arrPhysicalExamUI.push([
            examTypeID, examID, examType, exam, findings,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);
        
        DrawDataTable("dtlPhysicalExam", arrPhysicalExamUI);

        $('#ddlExaminationType').val("0");
        $('#ddlExamination').val("0");
        $('#txtExamFindings').val("");
    }
}

var diagnosisList = new Array();
var treatmentList = new Array();

function AddDiagnosis() {
    var diagnosisID = $('#txtDiagnosisID').val();
    var diagnosis = $('#Diagnosis').val();
    var treatment = $('#DiagnosisTreatment').val();

    //Validate duplication
    var diagnosisFound = 0;
    var treatmentFound = 0;

    if (diagnosis == "") {
        toastr.error("Error", "Please enter Diagnosis");
        return false;
    }

    diagnosisFound = $.inArray("" + diagnosisID + "", diagnosisList);
    treatmentFound = $.inArray("" + treatment + "", treatmentList);

    if (diagnosisFound > -1) {
        toastr.error("Error", "Diagnosis and treatment already exists.");
        return false;

    } else {


        diagnosisList.push("" + diagnosisID + "");
        treatmentList.push("" + treatment + "");

        arrDiagnosisUI = [];

        arrDiagnosisUI.push([
            diagnosisID, diagnosis, treatment,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);

        DrawDataTable("dtlDiagnosis", arrDiagnosisUI);

        $('#txtDiagnosisID').val("");
        $('#Diagnosis').val("");
        $('#DiagnosisTreatment').val("");
        
    }
}

var PresentingComplaintsList = new Array();
function AddPresentingComplaints() {
    var presentingComplaintsID = $("#txtPresentingComplaintsID").val();
    
    var presentingComplaints = $('#txtPresentingComplaints').val();
    var onsetDate = $('#txtPCOnsetDate').val();

    //Validate duplication
    var presentingComplaintFound = 0;

    if (presentingComplaints == "") {
        toastr.error("Error", "Please enter Presenting Complaint");
        return false;
    }

    presentingComplaintFound = $.inArray("" + presentingComplaints + "", PresentingComplaintsList);

    if (presentingComplaintFound > -1) {
        toastr.error("Error", "Presenting Complaint already exists.");
        return false;

    } else {


        PresentingComplaintsList.push("" + presentingComplaints + "");

        arrPresentingComplaintUI = [];

        arrPresentingComplaintUI.push([
            presentingComplaintsID, presentingComplaints,onsetDate,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);

        DrawDataTable("dtlPresentingComplaints", arrPresentingComplaintUI);

        $('#txtPresentingComplaints').val("");
        $('#txtPCOnsetDate').val("");
    }
}

function showHideFPControls() {
    var val = $('#onFP').find(":selected").text();
    if (val == "No Family Planning") {
        document.getElementById('divNoFP').style.display = 'block';
        document.getElementById('divOnFP').style.display = 'none';
        $('#fpMethod').val("0");
    }
    else {
        document.getElementById('divOnFP').style.display = 'block';
        document.getElementById('divNoFP').style.display = 'none';
        $('#ddlNoFP').val("0");
    }
        
}

function EnableDisableEDD()
{

    var pregStatus = $('#examinationPregnancyStatus').find(":selected").text();
    
    if (pregStatus != "Pregnant")
    {
        document.getElementById("ExpectedDateOfChildBirth").value = "";
        document.getElementById("ExpectedDateOfChildBirth").setAttribute('disabled', true);
    }
    else {
        var lmpDate = document.getElementById("lmp").value;
        var lmpJSDate = new Date(lmpDate);
        var edd = new Date(lmpJSDate.getTime() + 24192000000);
 
        document.getElementById("ExpectedDateOfChildBirth").removeAttribute('disabled');
        document.getElementById("ExpectedDateOfChildBirth").value = DateFormat(edd);
    }
    
}

function DateFormat(date)
{
    var m_names = new Array("Jan", "Feb", "Mar","Apr", "May", "Jun", "Jul", "Aug", "Sep","Oct", "Nov", "Dec");

    //var d = new Date();
    var curr_date = date.getDate();
    var curr_month = date.getMonth();
    var curr_year = date.getFullYear();
    if (curr_date < 10)
        curr_date = "0" + curr_date
    var result = curr_date + "-" + m_names[curr_month] + "-" + curr_year;
    
    return result;
}

function ChkQtyDispensed()
{
    var qtyPres = $("#txtQuantityPres").val();
    var qtyDisp = $("#txtQuantityDisp").val();

    if(qtyDisp > qtyPres)
    {
        $("#txtQuantityDisp").val("0");
        document.getElementById("txtQuantityDisp").focus();
        toastr.error("Error", "Quantity dispensed cannot be greater than quantity prescribed.");
        $("#btnAddDrugs").prop('disabled', true);
    }
    else {
        $("#btnAddDrugs").prop('disabled', false);
    }
}

var drugList = new Array();
function AddDrugPrescription() {
    var drugId = $("#drugID").val();
    var drugAbbr = $("#drugAbbr").val();
    var drugName = $("#txtDrugs").val();
    var batchId = $('#ddlBatch').find(":selected").val();
    var batchText = $('#ddlBatch').find(":selected").text();
    var dose = $("#txtDose").val();
    var freqId = $('#ddlFreq').find(":selected").val();
    var freqTxt = $('#ddlFreq').find(":selected").text();
    var duration = $("#txtDuration").val();
    var quantityPres = $("#txtQuantityPres").val();
    var quantityDisp = $("#txtQuantityDisp").val();
    batchText = batchText.substring(0, batchText.indexOf('~'));


    if ($('#chkProphylaxis').is(":checked")) {
        var prophylaxis = 1;
    }
    else
    {
        var prophylaxis = 0;
    }
    
    //Validate duplication
    if (batchId == undefined)
        batchId = 0;

    var drugFound = 0;

    if (drugName == "") {
        toastr.error("Error", "Please select drug");
        return false;
    }

    if (dose == "") {
        toastr.error("Error", "Please enter the dose");
        return false;
    }

    if (freqId == "0") {
        toastr.error("Error", "Please enter the frequency");
        return false;
    }

    if (duration == "0") {
        toastr.error("Error", "Please enter the duration");
        return false;
    }

    drugFound = $.inArray("" + drugName + "", drugList);
    
    if (drugFound > -1) {
        toastr.error("Error", drugName + " already exists in the List");
        return false; // message box herer
    }
    else {
        //drugList.push("" + drug + "");


        arrDrugPrescriptionUI = [];

        arrDrugPrescriptionUI.push([
            drugId, batchId, freqId, drugAbbr, drugName, batchText, dose, freqTxt, duration, quantityPres, quantityDisp,
            prophylaxis,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);

        DrawDataTable("dtlDrugPrescription", arrDrugPrescriptionUI);

        $("#txtDrugs").val("");
        $("#ddlBatch").val("");
        $("#txtDose").val("");
        $('#ddlFreq').val("0");
        $("#txtDuration").val("0");
        $("#txtQuantityPres").val("0");
        $("#txtQuantityDisp").val("0");
        $('#chkProphylaxis').attr('checked', false);
    }
}
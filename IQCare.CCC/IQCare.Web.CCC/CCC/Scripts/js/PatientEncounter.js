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

    var adverseEventId = $("#adverseEventId").val();
    var adverseEvent = $("#adverseEvent").val();
    adverseEvent = (adverseEvent === "Other Specify") ? $("#txtAdverseEventOther").val() : adverseEvent;
    var medicineCausingAE = $("#AdverseEventCause").val();
    var adverseEventSeverity = $('#ddlAdverseEventSeverity').find(":selected").text();
    var adverseEventSeverityID = $('#ddlAdverseEventSeverity').find(":selected").val();
    var adverseEventAction = $("#AdverseEventAction").find(":selected").text();
    var adverseEventCommand = "<button type='button' class='btnAddOutcome btn btn-danger fa fa- minus - circle btn-fill' > Add Outcome </button>"; // add the adverseEventCommand

    //Validate duplication
   
    var reactionEventFound = 0;
    var reactionCauseFound = 0;
    var reactionSeverityFound = 0;
    var reactionActionFound = 0;

    if (adverseEvent === "") {
        toastr.error("Error", "Please enter adverse event");
        return false;
    }

    if (medicineCausingAE === "") {
        toastr.error("Error", "Please enter medicine causing adverse event");
        return false;
    }

    if (adverseEventSeverityID === "0") {
        toastr.error("Error", "Please enter Adverse Event Severity");
        return false;
    }

    if (adverseEventAction === "") {
        toastr.error("Error", "Please enter Action");
        return false;
    }
   // reactionEventFound = $.inArray("" + adverseEventId + "", reactionEventList);
    reactionEventFound = $.inArray("" + adverseEvent + "", reactionEventList);
    reactionCauseFound = $.inArray("" + medicineCausingAE + "", reactionCauseList);
    reactionSeverityFound = $.inArray("" + adverseEventSeverityID + "", reactionSeverityList);
    reactionActionFound = $.inArray("" + adverseEventAction + "", reactionActionList);

    if (reactionEventFound > -1){
    
        toastr.error("Error", adverseEvent + " Adverse Event already exists in the List");
        return false; // message box herer
    }
   // reactionEventList.push("" + adverseEventId + "");
    reactionEventList.push("" + adverseEvent + "");
    reactionCauseList.push("" + medicineCausingAE + "");
    reactionSeverityList.push("" + adverseEventSeverityID + "");
    reactionActionList.push("" + adverseEventAction + "");
    arrAdverseEventUI = [];
  
    arrAdverseEventUI.push([
        adverseEventSeverityID,adverseEventId,adverseEvent, medicineCausingAE, adverseEventSeverity, adverseEventAction,
        "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
    ]);
   
    DrawDataTable("dtlAdverseEvents", arrAdverseEventUI);

    //$("#adverseEventId").val("");
    $("#adverseEvent").val("");
    $("#AdverseEventCause").val("");
    $('#ddlAdverseEventSeverity').val("0");
    $("#AdverseEventAction").val("");
}


function AdverseEventOutcome() {

   // var adverseEvent = $("#dtlAdverseEvents tr").find('td').eq(0).html();
    //var adverseEvent = $("dtlAdverseEvents").closest("tr").find("td:eq(0)").text();
    var adverseEvent = $("dtlAdverseEvents>td").eq($(0).index()).text();

    $("#dtlAdverseEvents td").click(function () {

        //var column_num = parseInt($(this).index()) + 1;
        //var row_num = parseInt($(this).parent().index()) + 1;
        var a = $(this).closest("tr").find("td:eq(0)").text();
        //alert(a);
        //alert(row_num); alert(column_num);
        $("#adverseEventLable").text();
        $("#adverseEventLable").text(a);
    });

   
   // alert(adverseEvent);
    $("#AdverseEventOutcomeModal").modal('show');
}

var chronicIllnessList = new Array();
function AddChronicIllness() {
    var chronicIllness = $('#ChronicIllnessName').find(":selected").text();
    var chronicIllnessID = $('#ChronicIllnessName').find(":selected").val();
    var illnessTreatment = $("#illnessTreatment").val();
    var treatmentDose = $('#treatmentDose').val();
    if (treatmentDose > 0)
        treatmentDose = treatmentDose;
    else
        treatmentDose = "";
    var onSet = $('#txtOnsetDate').val();
    //Validate duplication
    var chronicIllnessFound = 0;

    if (chronicIllness === "" || chronicIllness === "Select") {
        toastr.error("Error", "Please enter chronic illness");
        return false;
    }
   //if (!IsNumeric(treatmentDose)) {
   //    toastr.error("Error", "Treatment dose should be numeric.");
   //    return false;
   //}

   chronicIllnessFound = $.inArray("" + chronicIllness + "", chronicIllnessList);


   //if (chronicIllnessFound > -1) {
   //    toastr.error("Error", "Chronic Illness already exists.");
   //    return false;
   // }

    //else {
       
       chronicIllnessList.push("" + chronicIllness + "");
        arrChronicIllnessUI = [];
        arrChronicIllnessUI.push([chronicIllnessID, chronicIllness, illnessTreatment, treatmentDose, onSet,
            "<input type='checkbox' id='chkChronic" + chronicIllnessID + "' >", "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
        
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);

        $('#ChronicIllnessName').val("0");
        $("#illnessTreatment").val("");
        $('#treatmentDose').val("");
        $('#txtOnsetDate').val("");
    //}
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

    if (allergy === "") {
        toastr.error("Error", "Please enter allergy");
        return false;
    }
    if(allergy!=="")
    {
        if (allergyReaction === "") {
            toastr.error("Error", "Type of reaction is required!");
            return false;
        }
        if (severity === "") {
            toastr.error("Error", "Severity is required!");
            return false;
        }
        if (allergyDate === "") {
            toastr.error("Error", "Allergy date is required!");
            return false;
        }
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

    if (vaccineID === "0") {
        toastr.error("Error", "Please enter vaccine");
        return false;
    }

    //if (vaccineStageID == "0") {
    //    toastr.error("Error", "Please enter vaccine stage");
    //    return false;
    //}
    if (vaccinationDate === "") {
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
    var systemTypeText = $('#ddlExaminationType').find(":selected").text();
    var systemTypeID = $('#ddlExaminationType').find(":selected").val();
    var reviewOfSystemsMasterId = $("#hfExaminationReviewSystems").val();
    //console.log(reviewOfSystemsMasterId);
    var findingIDText = $('#ddlExamination').find(":selected").text();
    var findingID = $('#ddlExamination').find(":selected").val();
    var findings = $('#txtExamFindings').val();

    //Validate duplication
    var examFound = 0;

    if (systemTypeID === "0") {
        toastr.error("Error","Please Select System Type");
        return false;
    }

    if (findingID == "0") {
        toastr.error("Error", "Please select a Finding");
        return false;
    }

    examFound = $.inArray("" + findingID + "", physicalExamList);

    //console.log("reviewOfSystemsMasterId " + reviewOfSystemsMasterId);
    //console.log("systemTypeID " + systemTypeID);
    //console.log("findingID " + findingID);
    //console.log("systemTypeText " + systemTypeText);
    //console.log("findingIDText " + findingIDText);
    //console.log("findings " + findings);
    //return false;


    if (examFound > -1) {
        toastr.error("Error", "Examination already exists.");
        return false;
    } else {


        physicalExamList.push("" + findingID + "");
       

        arrPhysicalExamUI = [];
        arrPhysicalExamUI.push([
            reviewOfSystemsMasterId, systemTypeID, findingID, systemTypeText, findingIDText, findings,
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

    var value = $("#Diagnosis").val();
    if  (value=== "") {
        toastr.error("Error", "Please Enter Diagnosis");
        return false;
    }
    var result = value.split("~");
    
    var diagnosisID = result[0] + "~" + result[1]
    var diagnosis = result[2];
    var treatment = $('#DiagnosisTreatment').val();
    if (treatment === "") {
        toastr.error("Error,Please enter Treatment");
        return false;
    }

    //Validate duplication
    var diagnosisFound = 0;
    var treatmentFound = 0;
   
    if (diagnosis === "") {
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
        if (diagnosisListStatus.length > 0) {
            diagnosisListStatus.push({ id: diagnosisID, Disease: diagnosis, Treatment: treatment, deleteflag: false, deleted: true })
        }
       // diagnosisListStatus.push({ id: diagnosisID, deleteflag: false });
   
        treatmentList.push("" + treatment + "");
        DiseaseList.push("" + diagnosis + "");

        deleteflag = false;
        arrDiagnosisUI = [];
      

        arrDiagnosisUI.push([
            diagnosisID, diagnosis, treatment,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);

        DrawDataTable("dtlDiagnosis", arrDiagnosisUI);

       // $('#txtDiagnosisID').val("");
        $('#Diagnosis').select2("val", "0");
        $('#DiagnosisTreatment').val("");
        
    }
}

var PresentingComplaintsList = new Array();
function AddPresentingComplaints() {

    var presentingComplaintsID = $("#txtPresentingComplaintsID").val();
    
    var presentingComplaints = $('#txtPresentingComplaints').val();
  
    var numberOfDays = $('#numberOfDays').val();
    var visitDate = $('#DateOfVisit').datepicker('getDate');
    var onsetDate = moment(visitDate).subtract(numberOfDays, 'days').format('DD-MMM-YYYY');
    
    //Validate duplication
    var presentingComplaintFound = 0;

    if (presentingComplaints === "") {
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
        $('#numberOfDays').val("");
    }
}

function showHideFPControls() {
    var val = $('#onFP').find(":selected").text();
    if (val === "No Family Planning") {
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
        document.getElementById("ExpectedDateOfChildBirth").value = DateFormat1(edd);
    }
    
}

function DateFormat1(date)
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
    var vPres = $("#txtQuantityPres").val();
    var vDisp = $("#txtQuantityDisp").val();

    if (vPres != "")
        var qtyPres = parseFloat(vPres);
    else
        var qtyPres = 0;

    if (vDisp != "")
        var qtyDisp = parseFloat(vDisp);
    else
        var qtyDisp = 0;


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

var drugNameArr = new Array();
var batchNoArr = new Array();
function AddDrugPrescription() {
    var drugId = $("#drugID").val();
    var drugAbbr = $("#drugAbbr").val();
    var drugName = $("#txtDrugs").val();
    var batchId = $('#ddlBatch').find(":selected").val();
    var batchText = $('#ddlBatch').find(":selected").text();
    //var dose = $("#txtDose").val();
    //var freqId = $('#ddlFreq').find(":selected").val();
    //var freqTxt = $('#ddlFreq').find(":selected").text();
    var morning = $("#txtMorning").val();
    var midday = $("#txtMidday").val();
    var evening = $("#txtEvening").val();
    var night = $("#txtNight").val();
    
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
    var batchFound = 0;

    if (drugName === "") {
        toastr.error("Error", "Please select drug");
        return false;
    }

    //if (dose === "" || dose === "0") {
    //    toastr.error("Error", "Please enter the dose");
    //    return false;
    //}

    //if (freqId === "0") {
    //    toastr.error("Error", "Please enter the frequency");
    //    return false;
    //}
    if ((parseInt(morning) || 0) + (parseInt(midday) || 0) + (parseInt(evening) || 0) + (parseInt(night) || 0) === 0) {
        toastr.error("Error", "Please enter the dose");
        return false;
    }

    if (duration == "0" || duration === "") {
        toastr.error("Error", "Please enter the duration");
        return false;
    }

    if (quantityPres === "0" || quantityPres === "") {
        toastr.error("Error", "Please enter the quantity prescribed");
        return false;
    }

    drugFound = $.inArray("" + drugName + "", drugNameArr);
    batchFound = $.inArray("" + batchText + "", batchNoArr);

    
    
    if (drugFound > -1 && batchFound > -1) {
        toastr.error("Error", drugName + " and/or batch no. " + batchText + " already exists in the List");
        return false; // message box herer
    }
    else {
        drugNameArr.push("" + drugName + "");
        batchNoArr.push("" + batchText + "");

        arrDrugPrescriptionUI = [];

        //arrDrugPrescriptionUI.push([
        //    drugId, batchId, freqId, drugAbbr, drugName, batchText, dose, freqTxt, duration, quantityPres, quantityDisp,
        //    prophylaxis,
        //    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        //]);

        arrDrugPrescriptionUI.push([
            drugId, batchId, drugAbbr, drugName, batchText, morning, midday, evening, night, duration, quantityPres, quantityDisp,
            prophylaxis,
            "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
        ]);

        DrawDataTable("dtlDrugPrescription", arrDrugPrescriptionUI);

        $("#txtDrugs").val("");
        $("#ddlBatch").val("");
        $("#txtMorning").val("0");
        $('#ddlMidday').val("0");
        $('#ddlEvening').val("0");
        $('#ddlNight').val("0");
        $("#txtDuration").val("0");
        $("#txtQuantityPres").val("0");
        $("#txtQuantityDisp").val("0");
        $('#chkProphylaxis').attr('checked', false);
    }
}

function ValidateDispensedDate()
{
    var start = new Date($("#txtPrescriptionDate").val());
    var end = new Date($("#txtDateDispensed").val());
    if (end < start)
    {
        toastr.error("Error", "Dispense date cannot be less than Prescribed Date.");
        $("#txtPrescriptionDate").val("");
        $("#txtDateDispensed").val("");
    }
}

function ValidatePrescriptionDate() {
    var start = new Date($("#txtPrescriptionDate").val());
    var end = new Date($("#txtDateDispensed").val());
    if (start > end) {
        toastr.error("Error", "Prescription date cannot be greater than dispensed Date.");
        $("#txtPrescriptionDate").val("");
        $("#txtPrescriptionDate").val("");
    }
}
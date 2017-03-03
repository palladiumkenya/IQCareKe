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
    var adverseEventAction = $("#AdverseEventAction").val();
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

    if ((reactionEventFound > -1) && (reactionCauseFound > -1) && (reactionSeverityFound > -1) && (reactionActionFound > -1)){
    
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
    var treatmentDuration = $('#treatmentDuration').val();
    //Validate duplication
    var chronicIllnessFound = 0;

    if (chronicIllness == "") {
        toastr.error("Error", "Please enter chronic illness");
        return false;
    }
   if (!IsNumeric(treatmentDose)) {
       toastr.error("Error", "Treatment dose should be numeric.");
       return false;
   }

   chronicIllnessFound = $.inArray("" + chronicIllness + "", chronicIllnessList);


   if (chronicIllnessFound > -1) {
       toastr.error("Error", "Chronic Illness already exists.");
       return false;
    }

    else {
       
       chronicIllnessList.push("" + chronicIllness + "");
        arrChronicIllnessUI = [];
       
        arrChronicIllnessUI.push([chronicIllnessID, chronicIllness, illnessTreatment, treatmentDose, treatmentDuration, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
        
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);

        $('#ChronicIllnessName').val("0");
        $("#illnessTreatment").val("");
        $('#treatmentDose').val("");
        $('#treatmentDuration').val("");
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
    }
}

function AddDiagnosis() {
    var diagnosis = $('#Diagnosis').val();
    var treatment = $('#DiagnosisTreatment').val();

    if (diagnosis == "") {
        alert("Please enter Diagnosis");
        return;
    }

    //var chkData = $.grep(arrDiagnosis, function (e) { return e.diagnosis == diagnosis; });

    arrDiagnosisUI = [];
    //if (jQuery.isEmptyObject(chkData) == true) {
    //arrDiagnosis.push({ diagnosis: diagnosis, treatment: treatment });
    arrDiagnosisUI.push([diagnosis, treatment, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    //}
    //else {
    //    alert("Already exits in the table.");
    //}
        DrawDataTable("dtlDiagnosis", arrDiagnosisUI);

        $('#Diagnosis').val("");
        $('#DiagnosisTreatment').val("");
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
        document.getElementById("ctl00_IQCareContentPlaceHolder_ExpectedDateOfChildBirth").value = "";
        document.getElementById("ctl00_IQCareContentPlaceHolder_ExpectedDateOfChildBirth").setAttribute('disabled', true);
    }
    else {
        var lmpDate = document.getElementById("ctl00_IQCareContentPlaceHolder_lmp").value;
        var lmpJSDate = new Date(lmpDate);
        var edd = new Date(lmpJSDate.getTime() + 24192000000);
 
        document.getElementById("ctl00_IQCareContentPlaceHolder_ExpectedDateOfChildBirth").removeAttribute('disabled');
        document.getElementById("ctl00_IQCareContentPlaceHolder_ExpectedDateOfChildBirth").value = DateFormat(edd);
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

function DrawDataTable(ctrlName, arrUI) {

    if (arrUI.length > 0) {
        var table = $("#" + ctrlName).DataTable();
        table.rows.add(arrUI).draw().nodes();
    }
}

function AddAdverseReaction() {
    var adverseEvent = $("#adverseEvent").val();
    var medicineCausingAE = $("#AdverseEventCause").val();
    var adverseEventSeverity = $('#ddlAdverseEventSeverity').find(":selected").text();
    var adverseEventSeverityID = $('#ddlAdverseEventSeverity').find(":selected").val();
    var adverseEventAction = $("#AdverseEventAction").val();

    if (adverseEvent == "") {
        alert("Please enter adverse event");
        return;
    }

    if (medicineCausingAE == "") {
        alert("Please enter medicine causing adverse event");
        return;
    }

    if (adverseEventSeverityID == "0") {
        alert("Please enter Adverse Event Severity");
        return;
    }

    //var chkData = $.grep(arrAdverseEvent, function (e) { return e.adverseEvent == adverseEvent; });
    arrAdverseEventUI = [];

    //if (jQuery.isEmptyObject(chkData) == true) {
        //arrAdverseEvent.push({ adverseEvent: adverseEvent, medicineCausingAE: medicineCausingAE, adverseSeverity: adverseEventSeverityID, adverseAction: adverseEventAction });
    arrAdverseEventUI.push([adverseEventSeverityID,adverseEvent, medicineCausingAE, adverseEventSeverity, adverseEventAction, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    //}
    //else {
    //    alert("Already exits in the table.");
    //}
    DrawDataTable("dtlAdverseEvents", arrAdverseEventUI);

    $("#adverseEvent").val("");
    $("#AdverseEventCause").val("");
    $('#ddlAdverseEventSeverity').val("0");
    $("#AdverseEventAction").val("");
}


function AddChronicIllness() {
    var chronicIllness = $('#ChronicIllnessName').find(":selected").text();
    var chronicIllnessID = $('#ChronicIllnessName').find(":selected").val();
    var illnessTreatment = $("#illnessTreatment").val();
    var treatmentDose = $('#treatmentDose').val();
    var treatmentDuration = $('#treatmentDuration').val();

    if (chronicIllness == "") {
        alert("Please enter chronic illness");
        return;
    }
    else if (!IsNumeric(treatmentDose))
    {
        alert('Treatment dose should be numeric.');
    }
    else
    {
        arrChronicIllnessUI = [];
        arrChronicIllnessUI.push([chronicIllnessID, chronicIllness, illnessTreatment, treatmentDose, treatmentDuration, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
        
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);

        $('#ChronicIllnessName').val("0");
        $("#illnessTreatment").val("");
        $('#treatmentDose').val("");
        $('#treatmentDuration').val("");
    }
}


function AddVaccine() {
    var vaccine = $('#ddlVaccine').find(":selected").text();
    var vaccineID = $('#ddlVaccine').find(":selected").val();
    var vaccineStage = $('#ddlVaccineStage').find(":selected").text();
    var vaccineStageID = $('#ddlVaccineStage').find(":selected").val();
    var vaccinationDate = $('#txtVaccinationDate').val();

    if (vaccineID == "0") {
        alert("Please enter vaccine");
        return;
    }

    if (vaccineStageID == "0")
    {
        alert("Please enter vaccine stage");
        return;
    }
        
    arrVaccineUI = [];
    var chkData = $.grep(arrVaccineUI, function (e) { return e.vaccineID == vaccineID; });
    
    
    if (jQuery.isEmptyObject(chkData) == true)
    {
        arrVaccineUI.push([vaccineID, vaccineStageID, vaccine, vaccineStage, vaccinationDate, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    }
    else {
        alert("Already exits in the table.");
    }
    
    DrawDataTable("dtlVaccines", arrVaccineUI);

    $('#ddlVaccine').val("0");
    $('#ddlVaccineStage').val("0");
    $('#txtVaccinationDate').val("");
}

function AddPhysicalExam() {
    var examType = $('#ddlExaminationType').find(":selected").text();
    var examTypeID = $('#ddlExaminationType').find(":selected").val();
    var exam = $('#ddlExamination').find(":selected").text();
    var examID = $('#ddlExamination').find(":selected").val();
    var findings = $('#txtExamFindings').val();

    if (examTypeID == "0") {
        alert("Please enter Examination Type");
        return;
    }

    if (examID == "0") {
        alert("Please enter Examination");
        return;
    }

    arrPhysicalExamUI = [];
    
    arrPhysicalExamUI.push([examTypeID, examID, examType, exam, findings, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    
    DrawDataTable("dtlPhysicalExam", arrPhysicalExamUI);

    $('#ddlExaminationType').val("0");
    $('#ddlExamination').val("0");
    $('#txtExamFindings').val("");
}

function AddDiagnosis() {
    var diagnosis = $('#Diagnosis').val();
    var treatment = $('#DiagnosisTreatment').val();

    if (diagnosis == "") {
        alert("Please enter Diagnosis");
        return;
    }

    arrDiagnosisUI = [];
    
    arrDiagnosisUI.push([diagnosis, treatment, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    
    DrawDataTable("dtlDiagnosis", arrDiagnosisUI);

    $('#Diagnosis').val("");
    $('#DiagnosisTreatment').val("");
}

function showHideFPControls()
{
    var val = $('#onFP').find(":selected").text();
    if (val == "No Family Planning")
    {
        document.getElementById('divNoFP').style.display = 'block';
        document.getElementById('divOnFP').style.display = 'none';
        $('#fpMethod').val("0");
    }
    else
    {
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

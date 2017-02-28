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
        //var chkData = $.grep(arrChronicIllness, function (e) { return e.chronicIllness == chronicIllnessID; });
        
        arrChronicIllnessUI = [];
        //if (jQuery.isEmptyObject(chkData) == true) {
        //    arrChronicIllness.push({ chronicIllness: chronicIllnessID, treatment: illnessTreatment, dose: treatmentDose, duration: treatmentDuration });
        arrChronicIllnessUI.push([chronicIllnessID, chronicIllness, illnessTreatment, treatmentDose, treatmentDuration, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
        //}
        //else {
        //    alert("Already exits in the table.");
        //}
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);
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
        
    //var chkData = $.grep(arrVaccine, function (e) { return e.vaccine == vaccineID; });
    
    arrVaccineUI = [];
    //if (jQuery.isEmptyObject(chkData) == true) {
    //    arrVaccine.push({ vaccine: vaccineID, vaccineStage: vaccineStageID, vaccinationDate: vaccinationDate });
    arrVaccineUI.push([vaccineID, vaccineStageID, vaccine, vaccineStage, vaccinationDate, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    //}
    //else {
    //    alert("Already exits in the table.");
    //}
    DrawDataTable("dtlVaccines", arrVaccineUI);
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

    //var chkData = $.grep(arrPhysicalExam, function (e) { return e.exam == examID; });
    
    arrPhysicalExamUI = [];
    //if (jQuery.isEmptyObject(chkData) == true) {
    //    arrPhysicalExam.push({ examType: examTypeID, exam: examID, findings: findings });
    arrPhysicalExamUI.push([examTypeID, examID, examType, exam, findings, "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"]);
    //}
    //else {
    //    alert("Already exits in the table.");
    //}
    DrawDataTable("dtlPhysicalExam", arrPhysicalExamUI);
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

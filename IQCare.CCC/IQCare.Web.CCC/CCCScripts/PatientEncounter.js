var arrAdverseEvent = [];
var arrChronicIllness = [];
var arrVaccine = [];

function DrawDataTable(ctrlName, arrUI) {

    if (arrUI.length > 0) {
        var table = $("#" + ctrlName).DataTable();
        table.rows.add(arrUI).draw().nodes();
    }
}

//$('#dtlAdverseEvents').dataTable().fnDestroy();
//$('#dtlAdverseEvents').DataTable({
//    "paging": false,
//    "ordering": false,
//    "info": false
//});
//$('#dtlAdverseEvents').DataTable({
//    "aaData": arrAdverseEvent,
//    "bSort": false,
//    "bPaginate": false,
//    "bFilter": false,
//    "bInfo": false,
//    "autoWidth": true,
//    "aoColumns": [
//    { bSortable: false },
//     { bSortable: false },
//     { bSortable: false },
//     //{ bSortable: false },
//     {
//         bSortable: false,
//         //mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
//     }
//    ]
//});




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

    var chkData = $.grep(arrAdverseEvent, function (e) { return e.adverseEvent == adverseEvent; });
    arrAdverseEventUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrAdverseEvent.push({ adverseEvent: adverseEvent, medicineCausingAE: medicineCausingAE, adverseSeverity: adverseEventSeverityID, adverseAction: adverseEventAction });
        arrAdverseEventUI.push([adverseEvent, medicineCausingAE, adverseEventSeverity, adverseEventAction]);
    }
    else {
        alert("Already exits in the table.");
    }
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
        var chkData = $.grep(arrChronicIllness, function (e) { return e.chronicIllness == chronicIllnessID; });
        arrChronicIllnessUI = [];
        if (jQuery.isEmptyObject(chkData) == true) {
            arrChronicIllness.push({ chronicIllness: chronicIllnessID, treatment: illnessTreatment, dose: treatmentDose, duration: treatmentDuration });
            arrChronicIllnessUI.push([chronicIllness, illnessTreatment, treatmentDose, treatmentDuration]);
        }
        else {
            alert("Already exits in the table.");
        }
        DrawDataTable("dtlChronicIllness", arrChronicIllnessUI);
    }
    
}


function AddVaccine() {
    var vaccine = $('#ddlVaccine').find(":selected").text();
    var vaccineID = $('#ddlVaccine').find(":selected").val();
    var vaccineStage = $('#ddlVaccineStage').find(":selected").text();
    var vaccineStageID = $('#ddlVaccineStage').find(":selected").val();
    var vaccinationDate = $('#txtVaccinationDate').val();

    if (vaccine == "") {
        alert("Please enter vaccine");
        return;
    }

    var chkData = $.grep(arrVaccine, function (e) { return e.vaccine == vaccineID; });
    arrVaccineUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrVaccine.push({ vaccine: vaccineID, vaccineStage: vaccineStageID, vaccinationDate: vaccinationDate });
        arrVaccineUI.push([vaccine, vaccineStage, vaccinationDate]);
    }
    else {
        alert("Already exits in the table.");
    }
    DrawDataTable("dtlVaccines", arrVaccineUI);
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
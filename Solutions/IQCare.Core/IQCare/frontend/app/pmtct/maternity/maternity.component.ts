import {Component, OnInit} from '@angular/core';
import {FormArray, FormGroup} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';

@Component({
    selector: 'app-maternity',
    templateUrl: './maternity.component.html',
    styleUrls: ['./maternity.component.css']
})
export class MaternityComponent implements OnInit {
    isLinear: boolean = false;
    visitDetailsFormGroup: FormArray;
    motherProfileFormGroup: FormArray;
    formType: string;
    diagnosisFormGroup: FormArray;
    deliveryFormGroup: FormArray;
    babyFormGroup: FormArray;
    maternityTestsFormGroup: FormArray;
    maternalDrugAdministrationForGroup: FormArray;
    hivStatusFromGroup: FormGroup;
    partnerTestingFormGroup: FormArray;
    patientEducationFormGroup: FormGroup;
    DischargeFormGroup: FormGroup;
    referralFormGroup: FormGroup;
    nextAppointmentFormGroup: FormGroup;

    constructor(private route: ActivatedRoute) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.formType = 'maternity';
    }

    ngOnInit() {

    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    OnMotherProfileNotify(formGroup: FormGroup): void {
        this.motherProfileFormGroup.push(formGroup);
    }

    onPatientDiagnosis(formGroup: FormGroup): void {
        this.diagnosisFormGroup.push(formGroup);
    }

    onPatientDeliveryNotify(formGroup: FormGroup) {
        this.deliveryFormGroup.push(formGroup);
    }

    onBabyNotify(formGroup: FormGroup): void {
        this.babyFormGroup.push(formGroup);
    }

    onMaternityTests(formGroup: FormGroup): void {
        this.maternityTestsFormGroup.push(formGroup);
    }
    onMaternalDrugAdministration(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPartnerTestingNotify(formGroup: FormGroup): void {
        this.partnerTestingFormGroup.push(formGroup);
    }

}

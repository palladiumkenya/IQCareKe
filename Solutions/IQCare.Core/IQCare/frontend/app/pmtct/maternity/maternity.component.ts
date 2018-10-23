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
    motherProfileForm: FormGroup;
    formType: string;
    diagnosisFormGroup: FormArray;
    deliveryFormGroup: FormArray;
    babyFormGroup: FormArray;
    maternityTestsFormGroup: FormArray;
    maternalDrugAdministrationForGroup: FormArray;
    hivStatusFromGroup: FormGroup;
    PartnerTestingForm: FormArray;
    patientEducationForm: FormArray;
    dischargeFormGroup: FormArray;
    referralForm: FormGroup;
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
        this.motherProfileForm = formGroup;
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
        this.PartnerTestingForm.push(formGroup);
    }
    onPatientDischarge(formGroup: FormGroup): void {
        this.dischargeFormGroup.push(formGroup);
    }
    onPatientEducationNotify(formGroup: FormGroup): void {
        this.patientEducationForm.push(formGroup);
    }

    onPatientreferralNotify(formGroup: FormGroup): void {
        this.referralForm = formGroup;
    }
    onPatientNextAppointent(formGroup: FormGroup): void {
        this.nextAppointmentFormGroup = formGroup;
    }

}

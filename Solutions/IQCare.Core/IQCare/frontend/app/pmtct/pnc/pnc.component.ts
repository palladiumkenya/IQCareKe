import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';

@Component({
    selector: 'app-pnc',
    templateUrl: './pnc.component.html',
    styleUrls: ['./pnc.component.css']
})
export class PncComponent implements OnInit {
    matHistory_PostNatalExam_FormGroup: FormArray;
    drugAdministration_PartnerTesting_FormGroup: FormArray;

    constructor() {
        this.matHistory_PostNatalExam_FormGroup = new FormArray([]);
        this.drugAdministration_PartnerTesting_FormGroup = new FormArray([]);
    }

    ngOnInit() {
    }

    onMaternalHistoryNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onPostNatalExamNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onBabyExaminationNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onDrugAdministrationNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onPartnerTestingNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onPatientEducationNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onCervicalCancerScreeningNotify(formGroup: FormGroup): void {

    }

    onContraceptiveHistoryNotify(formGroup: FormGroup): void {

    }

    onHivStatusNotify(formGroup: FormGroup): void { }
}

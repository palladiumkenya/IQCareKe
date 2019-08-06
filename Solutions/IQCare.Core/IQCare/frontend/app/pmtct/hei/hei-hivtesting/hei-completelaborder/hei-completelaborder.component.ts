import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { Component, OnInit, Inject } from '@angular/core';
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormControl
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-hei-completelaborder',
    templateUrl: './hei-completelaborder.component.html',
    styleUrls: ['./hei-completelaborder.component.css']
})
export class HeiCompletelaborderComponent implements OnInit {
    title: string;
    HeiCompleteHivTestsForm: FormGroup;
    labOrderId: number;
    labOrderTestId: number;
    labTestId: number;
    selectedTesttype: LookupItemView[];
    testResults: LookupItemView[];
    maxDate: Date;

    constructor(
        private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<HeiCompletelaborderComponent>,
        @Inject(MAT_DIALOG_DATA) data
    ) {
        this.title = 'Hei Complete(HIV Testing)';

        this.labOrderId = data.labOrderId;
        this.labOrderTestId = data.labOrderTestId;
        this.labTestId = data.labTestId;
        this.selectedTesttype = data.testtype;
        this.testResults = data.testResults;
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.HeiCompleteHivTestsForm = this._formBuilder.group({
            testType: new FormControl('', [Validators.required]),
            result: new FormControl('', [Validators.required]),
            dateresultscollected: new FormControl('', [Validators.required]),
            resultText: new FormControl('', [Validators.required])
        });

        this.HeiCompleteHivTestsForm.get('testType').setValue(
            this.selectedTesttype
        );

        if (
            this.selectedTesttype['itemName'] == 'Baseline Viral Load (for +ve)'
        ) {
            this.HeiCompleteHivTestsForm.get('result').disable();
            this.HeiCompleteHivTestsForm.get('resultText').enable();
        } else {
            this.HeiCompleteHivTestsForm.get('result').enable();
            this.HeiCompleteHivTestsForm.get('resultText').disable();
        }
    }

    save() {
        if (this.HeiCompleteHivTestsForm.valid) {
            this.dialogRef.close(this.HeiCompleteHivTestsForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }
}

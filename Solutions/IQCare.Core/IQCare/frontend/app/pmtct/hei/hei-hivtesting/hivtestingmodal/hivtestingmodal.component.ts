import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-hivtestingmodal',
    templateUrl: './hivtestingmodal.component.html',
    styleUrls: ['./hivtestingmodal.component.css']
})
export class HivtestingmodalComponent implements OnInit {
    title: string;
    HivTestingForm: FormGroup;
    hivTestType: any;
    testResults: any;
    maxDate: Date;

    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<HivtestingmodalComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'HIV Testing';

        this.hivTestType = data.hivTestType;
        this.testResults = data.testResults;

        this.maxDate = new Date();
    }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            testtype: new FormControl('', [Validators.required]),
            dateofsamplecollection: new FormControl('', [Validators.required]),
            result: new FormControl('', [Validators.required]),
            dateresultscollected: new FormControl('', [Validators.required]),
            comments: new FormControl(''),
            resultText: new FormControl('', [Validators.required])
        });

        this.HivTestingForm.get('resultText').disable({ onlySelf: true });
    }

    onTestTypeChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Baseline Viral Load (for +ve)') {
            this.HivTestingForm.get('resultText').enable({ onlySelf: true });
            this.HivTestingForm.get('result').disable({ onlySelf: true });
        } else {
            this.HivTestingForm.get('resultText').disable({ onlySelf: true });
            this.HivTestingForm.get('result').enable({ onlySelf: true });
        }
    }

    save() {
        if (this.HivTestingForm.valid) {
            this.dialogRef.close(this.HivTestingForm.value);
        } else {
            return;
        }

    }

    close() {
        this.dialogRef.close();
    }
}

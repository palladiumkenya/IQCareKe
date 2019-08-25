import { LookupItemView } from './../../../../shared/_models/LookupItemView';
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
    hivTestType: any[];
    testResults: any;
    YesNo: LookupItemView;
    maxDate: Date;

    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<HivtestingmodalComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'HIV Testing';

        this.hivTestType = data.hivTestType;
        this.testResults = data.testResults;
        this.YesNo = data.YesNo;

        this.maxDate = new Date();
    }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            testtype: new FormControl('', [Validators.required]),
            dateofsamplecollection: new FormControl('', [Validators.required]),
            resultsAvailable: new FormControl('', [Validators.required]),
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
        } else if (event.isUserInput && event.source.selected && event.source.viewValue != 'Baseline Viral Load (for +ve)') {
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

    onResultAvailableChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            const testType = this.hivTestType.filter(obj => obj.itemName == 'Baseline Viral Load (for +ve)');
            const selectedType = this.HivTestingForm.get('testtype').value;
            this.HivTestingForm.get('result').enable({ onlySelf: true });
            if (testType[0].itemId == selectedType.itemId) {
                this.HivTestingForm.get('resultText').enable({ onlySelf: true });
            } else if (selectedType.itemName == 'Baseline Viral Load (for +ve)') {
                this.HivTestingForm.get('result').disable({ onlySelf: true });
            }
            this.HivTestingForm.get('dateresultscollected').enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.HivTestingForm.get('resultText').disable({ onlySelf: true });
            this.HivTestingForm.get('result').disable({ onlySelf: true });
            this.HivTestingForm.get('dateresultscollected').disable({ onlySelf: true });
        }
    }
}

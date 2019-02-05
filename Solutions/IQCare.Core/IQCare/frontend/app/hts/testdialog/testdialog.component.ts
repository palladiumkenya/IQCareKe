import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
    selector: 'app-testdialog',
    templateUrl: './testdialog.component.html',
    styleUrls: ['./testdialog.component.css']
})
export class TestDialogComponent implements OnInit {
    form: FormGroup;
    kitName: string;
    lotNumber: string;
    expiryDate: string;
    hivResult: number;
    title: string;

    hivTestKits: any[];
    hivResultsOptions: any[];

    otherLotNumber: string;
    determineLotNumber: string;
    firstResponseLotNumber: string;

    otherKitexpiryDate: Date;
    determineKitexpiryDate: Date;
    firstResponseKitexpiryDate: Date;

    minDate: any;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<TestDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = data.screeningType;
        this.hivTestKits = data.hivTestKits;
        this.hivResultsOptions = data.hivResultsOptions;

        // Last used kits
        this.otherLotNumber = data.otherLotNumber;
        this.determineLotNumber = data.determineLotNumber;
        this.firstResponseLotNumber = data.firstResponseLotNumber;

        // Expiry Dates
        this.otherKitexpiryDate = data.otherKitexpiryDate;
        this.determineKitexpiryDate = data.determineKitexpiryDate;
        this.firstResponseKitexpiryDate = data.firstResponseKitexpiryDate;

        this.minDate = new Date();
    }

    ngOnInit() {
        this.form = this.fb.group({
            kitName: new FormControl(this.kitName, [Validators.required]),
            lotNumber: new FormControl(this.lotNumber, [Validators.required]),
            expiryDate: new FormControl(this.expiryDate, [Validators.required]),
            hivResult: new FormControl(this.hivResult, [Validators.required])
        });
    }

    onTestKitChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.form.get('lotNumber').setValue(this.otherLotNumber);
            this.form.get('expiryDate').setValue(this.otherKitexpiryDate);
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Determine') {
            this.form.get('lotNumber').setValue(this.determineLotNumber);
            this.form.get('expiryDate').setValue(this.determineKitexpiryDate);
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'First Response') {
            this.form.get('lotNumber').setValue(this.firstResponseLotNumber);
            this.form.get('expiryDate').setValue(this.firstResponseKitexpiryDate);
        }
    }

    save() {
        if (this.form.valid) {
            this.dialogRef.close(this.form.value);
        } else {
            return;
        }

    }

    close() {
        this.dialogRef.close();
    }

}

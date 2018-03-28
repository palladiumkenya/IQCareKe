import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

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

    minDate: any;

    constructor(private fb: FormBuilder,
                private dialogRef: MatDialogRef<TestDialogComponent>,
                @Inject(MAT_DIALOG_DATA) data) {
        this.title = data.screeningType;
        this.hivTestKits = data.hivTestKits;
        this.hivResultsOptions = data.hivResultsOptions;

        this.minDate = new Date();
     }

    ngOnInit() {
        this.form = this.fb.group({
            kitName: [this.kitName, []],
            lotNumber: [this.lotNumber, []],
            expiryDate: [this.expiryDate, []],
            hivResult: [this.hivResult, []]
        });
    }

    save() {
        this.dialogRef.close(this.form.value);
    }

    close() {
        this.dialogRef.close();
    }

}

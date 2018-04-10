import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
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
            kitName: new FormControl(this.kitName, [Validators.required]),
            lotNumber: new FormControl(this.lotNumber, [Validators.required]),
            expiryDate: new FormControl(this.expiryDate, [Validators.required]),
            hivResult: new FormControl(this.hivResult, [Validators.required])
        });
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

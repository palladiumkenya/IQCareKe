import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-immunization',
    templateUrl: './immunization.component.html',
    styleUrls: ['./immunization.component.css']
})
export class ImmunizationComponent implements OnInit {
    title: string;
    public ImmunizationHistoryFormGroup: FormGroup;
    vaccines: LookupItemView[] = [];
    immunizationperiods: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<ImmunizationComponent>,
        @Inject(MAT_DIALOG_DATA) data) {


        this.immunizationperiods = data.immunizationperiods;
        this.vaccines = data.vaccines;
        this.yesnoOptions = data.yesnoOptions;

        this.title = 'Immunization';
    }

    ngOnInit() {
        this.ImmunizationHistoryFormGroup = this._formBuilder.group({
            period: new FormControl('', [Validators.required]),
            immunizationGiven: new FormControl('', [Validators.required]),
            dateImmunized: new FormControl('', [Validators.required]),
            nextSchedule: new FormControl('')
        });
    }

    save() {
        if (this.ImmunizationHistoryFormGroup.valid) {
            this.dialogRef.close(this.ImmunizationHistoryFormGroup.value);
        } else {
            return;
        }

    }

    close() {
        this.dialogRef.close();
    }

}

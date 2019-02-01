import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-milestones-form',
    templateUrl: './milestones-form.component.html',
    styleUrls: ['./milestones-form.component.css']
})
export class MilestonesFormComponent implements OnInit {
    milestonesFormGroup: FormGroup;
    milestoneassessments: LookupItemView[];
    milestonestatuses: LookupItemView[];
    yesnoOptions: LookupItemView[];

    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<MilestonesFormComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.milestoneassessments = data.milestoneassessments;
        this.milestonestatuses = data.milestonestatuses;
        this.yesnoOptions = data.yesnoOptions;
    }

    ngOnInit() {
        this.milestonesFormGroup = this._formBuilder.group({
            milestoneAssessed: new FormControl('', [Validators.required]),
            dateAssessed: new FormControl('', [Validators.required]),
            achieved: new FormControl('', [Validators.required]),
            status: new FormControl('', [Validators.required]),
            comment: new FormControl('')
        });
    }

    Save() {
        if (this.milestonesFormGroup.valid) {
            this.dialogRef.close(this.milestonesFormGroup.value);
        } else {
            return;
        }
    }

    Close() {
        this.dialogRef.close();
    }
}

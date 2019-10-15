import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {LookupItemView} from '../../shared/_models/LookupItemView';

@Component({
    selector: 'app-tracing',
    templateUrl: './tracing.component.html',
    styleUrls: ['./tracing.component.css']
})
export class TracingComponent implements OnInit {
    form: FormGroup;
    maxTracingDate: any;

    tracingDate: string;
    mode: number;
    outcome: number;

    tracingModeOptions: any[];
    tracingOutcomeOptions: any[];
    tracingReasonNotContactedPhone: LookupItemView[];
    tracingReasonNotContactedPhysical: LookupItemView[];
    reasonsOptions: LookupItemView[];

    constructor(
        private dialogRef: MatDialogRef<TracingComponent>,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) data
    ) {
        this.maxTracingDate = new Date();
        this.tracingModeOptions = data.tracingMode;
        this.tracingOutcomeOptions = data.tracingOutcome;
        this.tracingReasonNotContactedPhone = data.tracingReasonNotContactedPhone;
        this.tracingReasonNotContactedPhysical = data.tracingReasonNotContactedPhysical;
    }

    ngOnInit() {
        this.form = this.fb.group({
            tracingDate: new FormControl(this.tracingDate, [Validators.required]),
            mode: new FormControl(this.mode, [Validators.required]),
            outcome: new FormControl(this.outcome, [Validators.required]),
            reasonNotContacted: new FormControl('', [Validators.required]),
            otherReasonSpecify: new FormControl('', [Validators.required])
        });

        this.form.get('reasonNotContacted').disable({onlySelf: true});
        this.form.get('otherReasonSpecify').disable({onlySelf: true});
    }

    onOutcomeChanged(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Not Contacted') {
            this.form.get('reasonNotContacted').enable({onlySelf: true});
        } else if (event.isUserInput && event.source.selected && event.source.viewValue != '') {
            this.form.get('reasonNotContacted').disable({onlySelf: true});
            this.form.get('reasonNotContacted').setValue('');
        }
    }

    reasonNotContactedChanged(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.form.get('otherReasonSpecify').enable({onlySelf: true});
        } else if (event.isUserInput && event.source.selected && event.source.viewValue != '') {
            this.form.get('otherReasonSpecify').disable({onlySelf: true});
            this.form.get('otherReasonSpecify').setValue('');
        }
    }

    onTracingModeChanged(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Phone') {
            this.reasonsOptions = this.tracingReasonNotContactedPhone;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Physical') {
            this.reasonsOptions = this.tracingReasonNotContactedPhysical;
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

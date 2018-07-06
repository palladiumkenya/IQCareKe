import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

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

    constructor(
        private dialogRef: MatDialogRef<TracingComponent>,
        private fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) data
    ) {
        this.maxTracingDate = new Date();
        this.tracingModeOptions = data.tracingMode;
        this.tracingOutcomeOptions = data.tracingOutcome;
    }

    ngOnInit() {
        this.form = this.fb.group({
            tracingDate: new FormControl(this.tracingDate, [Validators.required]),
            mode: new FormControl(this.mode, [Validators.required]),
            outcome: new FormControl(this.outcome, [Validators.required])
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

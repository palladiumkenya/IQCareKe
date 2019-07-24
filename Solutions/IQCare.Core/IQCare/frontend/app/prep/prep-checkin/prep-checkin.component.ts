import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-prep-checkin',
    templateUrl: './prep-checkin.component.html',
    styleUrls: ['./prep-checkin.component.css']
})
export class PrepCheckinComponent implements OnInit {
    form: FormGroup;
    title: string;
    prepEncounterDate: Date;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<PrepCheckinComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'PrEP Check-in';
    }

    ngOnInit() {
        this.form = this.fb.group({
            visitdate: new FormControl('', [Validators.required])
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

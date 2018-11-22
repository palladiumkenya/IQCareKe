import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';

@Component({
    selector: 'app-check-duplicates',
    templateUrl: './check-duplicates.component.html',
    styleUrls: ['./check-duplicates.component.css']
})
export class CheckDuplicatesComponent implements OnInit {
    title: string;
    form: FormGroup;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<CheckDuplicatesComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private dialog: MatDialog) {
        this.title = 'Check For Duplicates';
    }

    ngOnInit() {
        this.form = this.fb.group({

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

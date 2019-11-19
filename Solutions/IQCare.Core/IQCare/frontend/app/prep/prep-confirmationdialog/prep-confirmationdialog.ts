import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

@Component({
    selector: 'app-prepconfirmation-dialog',
    templateUrl: './prep-confirmationdialog.html',
})
export class PrepConfirmationDialogComponent {
    constructor(public dialogRef: MatDialogRef<PrepConfirmationDialogComponent>) { }

    public confirmMessage: string;

}
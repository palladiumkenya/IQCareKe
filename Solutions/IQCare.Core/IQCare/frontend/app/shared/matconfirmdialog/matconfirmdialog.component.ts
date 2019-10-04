import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-matconfirmdialog',
  templateUrl: './matconfirmdialog.component.html',
  styleUrls: ['./matconfirmdialog.component.css']
})
export class MatconfirmdialogComponent implements OnInit {

    constructor(@Inject(MAT_DIALOG_DATA) public data,
    public dialogRef: MatDialogRef<MatconfirmdialogComponent>) {
    }
  ngOnInit() {
  }
  closeDialog() {
    this.dialogRef.close(false);
}

}

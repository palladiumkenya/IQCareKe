import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-records-merge',
  templateUrl: './records-merge.component.html',
  styleUrls: ['./records-merge.component.css']
})
export class RecordsMergeComponent implements OnInit {
    
    constructor(private dialogRef: MatDialogRef<RecordsMergeComponent>,
                @Inject(MAT_DIALOG_DATA) data) {
        console.log(data);
    }
    
    ngOnInit() {}
}

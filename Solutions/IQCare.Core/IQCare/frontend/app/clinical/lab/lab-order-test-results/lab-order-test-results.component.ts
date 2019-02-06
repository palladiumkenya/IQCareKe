import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatPaginator, MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { LaborderService } from '../../_services/laborder.service';

@Component({
  selector: 'app-lab-order-test-results',
  templateUrl: './lab-order-test-results.component.html',
  styleUrls: ['./lab-order-test-results.component.css']
})
export class LabOrderTestResultsComponent implements OnInit {


  @ViewChild(MatPaginator) paginator: MatPaginator;  
  labOrderTestResults :any[] = [];
  testResultsDataSource = new MatTableDataSource(this.labOrderTestResults);
  test_results_displaycolumns : any[] = ['parameter','result','unit'];

  dialogTitle : any;


  constructor(@Inject(MAT_DIALOG_DATA) public dialogData,
   private dialogRef : MatDialogRef<LabOrderTestResultsComponent>) 
  {
    this.labOrderTestResults = dialogData.labOrderTestResults;
    this.dialogTitle = dialogData.dialogTitle;

    this.testResultsDataSource = new MatTableDataSource(this.labOrderTestResults);
    this.testResultsDataSource.paginator = this.paginator;
   
  }

  ngOnInit() {
  }

  
close() {
  this.dialogRef.close();
}
}

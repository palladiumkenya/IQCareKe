import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-lab-test-grid',
  templateUrl: './lab-test-grid.component.html',
  styleUrls: ['./lab-test-grid.component.css']
})
export class LabTestGridComponent implements OnInit {

  @Input("lab_test_data") lab_test_data;
  @Input("dataSource") dataSource //=   new MatTableDataSource(this.lab_test_data);
  lab_test_displaycolumns = ['test', 'orderReason', 'testNotes', 'action'];
  // dataSource =  new MatTableDataSource(this.lab_test_data);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor() {

   }

  ngOnInit() {
    // this.dataSource = new MatTableDataSource(this.lab_test_data);
    // this.dataSource.paginator = this.paginator;
    console.log("I have been notified "+ this.dataSource);

  }


  public onLabTestGridNotify(labtest:any[]) {
    console.log("I have been notified "+ labtest);
  }

}

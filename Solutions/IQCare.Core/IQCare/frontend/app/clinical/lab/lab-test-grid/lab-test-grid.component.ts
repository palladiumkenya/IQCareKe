import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-lab-test-grid',
  templateUrl: './lab-test-grid.component.html',
  styleUrls: ['./lab-test-grid.component.css']
})
export class LabTestGridComponent implements OnInit {

  lab_test_data: any[] = [];
  LabTestDataSource = new MatTableDataSource(this.lab_test_data);
  lab_test_displaycolumns = ['test', 'orderReason', 'testNotes', 'action'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  constructor() { }

  ngOnInit() {
  }

}

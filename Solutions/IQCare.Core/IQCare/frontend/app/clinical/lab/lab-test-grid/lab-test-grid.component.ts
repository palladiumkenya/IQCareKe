import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { LaborderService } from '../../_services/laborder.service';

@Component({
  selector: 'app-lab-test-grid',
  templateUrl: './lab-test-grid.component.html',
  styleUrls: ['./lab-test-grid.component.css']
})
export class LabTestGridComponent implements OnInit {
  @Input("PatientId") PatientId : number;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  labOrderTests: any[] = [];
  labOrderTests_displaycolumns = ['test', 'orderReason', 'testNotes', 'action'];
  dataSource =  new MatTableDataSource(this.labOrderTests);

  constructor(private labOrderService:LaborderService) {
   }

  ngOnInit() {
    
  }

  public getPatientLabOrders(status:string) {
    this.labOrderService.getLabOrders(this.PatientId,status).subscribe(result=>{
         console.log("LAb tests result >> "+ result);
    }) 
  }

}

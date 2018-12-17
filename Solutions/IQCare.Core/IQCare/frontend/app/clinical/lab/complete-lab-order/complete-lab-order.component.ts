import { Component, OnInit, ViewChild } from '@angular/core';
import { LaborderService } from '../../_services/laborder.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-complete-lab-order',
  templateUrl: './complete-lab-order.component.html',
  styleUrls: ['./complete-lab-order.component.css']
})
export class CompleteLabOrderComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;  

  completedLabTests : any[] = [];
  pendingLabTests : any[] = [];

  completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
  pendingLabsDataSource = new MatTableDataSource(this.pendingLabsDataSource);
  
  patientId : number;



  constructor(private labOrderService : LaborderService,route: ActivatedRoute) {
         route.params.subscribe(params=>{
           this.patientId = params['patientId'];
           this.buildCompletedLabsGrid(this.patientId);
        });
   }

  ngOnInit() {
  }


  public buildCompletedLabsGrid(patientId: number) {
      this.labOrderService.getLabTestResults(patientId).subscribe(res=>{
             this.completedLabTests.push({
               labOrderTestId : res.labOrderTestId,
               test : res.labTestName,
               orderDate : res.sampleDate,
               orderReason : res.orderReason,
               labTestId : res.labTestId,
               unit : res.resultUnits,
               resultDate : res.resultDate,
               result : res.resultTexts
             });
        this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
        this.completedLabsDataSource.paginator = this.paginator;

      },(error)=>
      {
          console.log(error + "An error occured while getting completed labs");
      });  
  }

  /**
   * buildPendingLabsGrid
patientId : number   */
  public buildPendingLabsGrid(patientId : number) {
    
  }

}

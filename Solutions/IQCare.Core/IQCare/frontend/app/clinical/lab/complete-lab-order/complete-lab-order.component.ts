import { Component, OnInit, ViewChild } from '@angular/core';
import { LaborderService } from '../../_services/laborder.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { $ } from 'protractor';

@Component({
  selector: 'app-complete-lab-order',
  templateUrl: './complete-lab-order.component.html',
  styleUrls: ['./complete-lab-order.component.css']
})
export class CompleteLabOrderComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;  
  
  labTestResults : any[] = [];
  completedLabTests : any[] = [];
  pendingLabTests : any[] = [];

  completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
  pendingLabsDataSource = new MatTableDataSource(this.pendingLabsDataSource);
  
  patientId : number;

  constructor(private labOrderService : LaborderService,route: ActivatedRoute) {
         route.params.subscribe(params=>{
           this.patientId = params['patientId'];
           this.buildLabTestsGrid(this.patientId);
        });
   }

  ngOnInit() {
  }


  public buildLabTestsGrid(patientId: number) {
      this.labOrderService.getLabTestResults(patientId,null).subscribe(res=>{
             this.labTestResults.push({
               labOrderTestId : res.labOrderTestId,
               test : res.labTestName,
               orderDate : res.sampleDate,
               orderReason : res.orderReason,
               labTestId : res.labTestId,
               unit : res.resultUnits,
               resultDate : res.resultDate,
               result : res.resultTexts,
               status : res.resultStatus
             });

        for (let index = 0; index < this.labTestResults.length; index++) 
        {
            if(this.labTestResults[index].status =='Completed')
               this.completedLabTests.push(this.labTestResults[index]);
            else
               this.pendingLabTests.push(this.labTestResults[index]);
        }
        
        this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
        this.pendingLabsDataSource = new MatTableDataSource(this.pendingLabTests);

        this.completedLabsDataSource.paginator = this.paginator;
        this.pendingLabsDataSource = this.paginator;

      },(error)=>
      {
          console.log(error + "An error occured while getting completed labs");
      });  
  }
}

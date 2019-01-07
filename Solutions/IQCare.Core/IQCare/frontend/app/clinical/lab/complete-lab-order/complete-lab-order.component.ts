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

  pending_labs_displaycolumns : any[] = ['test','orderReason','orderDate','status','action'];
  completed_labs_displaycolumns : any[] = ['test','orderReason','orderDate','result','unit'];

  completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
  pendingLabsDataSource = new MatTableDataSource(this.pendingLabsDataSource);
  
  patientId : number;

  constructor(private labOrderService : LaborderService, private route: ActivatedRoute)
   {
        
   }

  ngOnInit() {
    this.route.params.subscribe(params=>
      {
        this.patientId = params['patientId'];
        this.buildLabTestsGrid(this.patientId);
      });
  }


  public buildLabTestsGrid(patientId: number) {
      this.labOrderService.getLabTestResults(patientId,null).subscribe(res=>{
            if(res.length == 0)
                return;
               res.forEach(test => {               
                this.labTestResults.push({
                  labOrderTestId : test.labOrderTestId,
                  test : test.labTestName,
                  orderDate : test.orderDate,
                  orderReason : test.orderReason,
                  labTestId : test.labTestId,
                  unit : test.resultUnits,
                  resultDate : test.resultDate,
                  result : test.resultTexts,
                  status : test.resultStatus
                });                
               });
                   
             console.log(this.labTestResults.length + '>> Length');

        for (let index = 0; index < this.labTestResults.length; index++) 
        {
          console.log(this.labTestResults[index].status + '>> Status');

            if(this.labTestResults[index].status =='Completed')
               this.completedLabTests.push(this.labTestResults[index]);
            else
               this.pendingLabTests.push(this.labTestResults[index]);
        }
        
        console.log("Pending labs "+ this.pendingLabTests.length);
        this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
        this.pendingLabsDataSource = new MatTableDataSource(this.pendingLabTests);

        this.completedLabsDataSource.paginator = this.paginator;
        this.pendingLabsDataSource.paginator = this.paginator;

      },(error)=>
      {
          console.log(error + "An error occured while getting completed labs");
      });  
  }
}

import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { LaborderService } from '../../_services/laborder.service';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog } from '@angular/material';
import { LabOrderTestResultsComponent } from '../lab-order-test-results/lab-order-test-results.component';

@Component({
  selector: 'app-completed-labs-grid',
  templateUrl: './completed-labs-grid.component.html',
  styleUrls: ['./completed-labs-grid.component.css']
})
export class CompletedLabsGridComponent implements OnInit {

  @Input('PatientId') PatientId : number;
  completed_labs_displaycolumns : any[] = ['test','orderReason','orderDate','result','unit'];

  @ViewChild(MatPaginator) paginator: MatPaginator;  

  completedLabTests : any[] = [];
  completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
  

  constructor(private labOrderService : LaborderService,
    private dialog : MatDialog) { 
   }

  ngOnInit() {
    this.getCompletedLabs(this.PatientId);
  }

  public getCompletedLabs(patientId: number) {

    this.labOrderService.getLabTestResults(patientId,'Complete').subscribe(res=>{
          if(res.length == 0)
              return;
             res.forEach(test => {               
              this.completedLabTests.push({
                labOrderTestId : test.labOrderTestId,
                labOrderId : test.labOrderId,
                test : test.labTestName,
                orderDate : test.orderDate,
                orderReason : test.orderReason,
                labTestId : test.labTestId,
                unit : test.resultUnits,
                resultDate : test.resultDate,
                result : test.result,
                status : test.resultStatus
              });                
             });         
      this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
      this.completedLabsDataSource.paginator = this.paginator;

    },(error)=>
    {
        console.log(error + "An error occured while getting completed labs");
    });  
}

public viewResults(completedLab : any) {
  const  labOrderTestResults : any[] = [];
  this.labOrderService.getLabOrderTestResults(completedLab.labOrderTestId).subscribe(res=>{
    if(res.length == 0)
        return;
       res.forEach(rs => {               
         labOrderTestResults.push({
          parameter: rs.parameter,
          result : rs.result,
          unit : rs.resultUnits
        });                
     });     
     const resultsDialogConfig = new MatDialogConfig();

     resultsDialogConfig.disableClose = false;
     resultsDialogConfig.autoFocus = true;
     resultsDialogConfig.width = '800px';
     
     resultsDialogConfig.data =  {
                            labOrderTestResults : labOrderTestResults,
                            dialogTitle : completedLab.test +' Test Results'
                          };
   
     const dialogRef = this.dialog.open(LabOrderTestResultsComponent, resultsDialogConfig);
     dialogRef.afterClosed().subscribe(
       data => 
       {
         if (!data)
           return;
           console.log(data);
       });  

},(error)=>
{
  console.log(error + "An error occured while getting lab results");
});
}
}

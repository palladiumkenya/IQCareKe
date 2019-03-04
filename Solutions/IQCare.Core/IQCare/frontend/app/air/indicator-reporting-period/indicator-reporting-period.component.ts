import { Component, OnInit, ViewChild } from '@angular/core';
import { IndicatorService } from '../_services/indicator.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-indicator-reporting-period',
  templateUrl: './indicator-reporting-period.component.html',
  styleUrls: ['./indicator-reporting-period.component.css']
})
export class IndicatorReportingPeriodComponent implements OnInit {
  
  reporting_period_displaycolumns : any[] = ['reportName', 'reportDate', 'dateCreated', 'action', 'edit']
  reportingPeriods : any[] = [];
  reportingFormId:number = null;

  reportingPeriodsDataSource = new MatTableDataSource(this.reportingPeriods);
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor(private indicatorService: IndicatorService,
     private activedRoute : ActivatedRoute) 
   {

   }

  ngOnInit() {
    this.activedRoute.params.subscribe(param=>{
       this.reportingFormId = param['reportingFormId'];
       this.getReportingPeriods(this.reportingFormId);       
    })
  }
  
 

  private getReportingPeriods(reportingFormId : number) {
      this.indicatorService.getFormIndicatorReportingPeriods(null).subscribe(r=>
        {
          
        r.forEach(data => {
           this.reportingPeriods.push({
            id : data.id,
            reportName : data.reportName,
            reportDate : data.strReportDate,
            dateCreated : data.dateCreated
          });
        });

      
          this.reportingPeriodsDataSource = new MatTableDataSource(this.reportingPeriods);
          this.reportingPeriodsDataSource.paginator = this.paginator;
      },(error)=> {

        console.log("An error occured while fetching reporting periods " + error);
      })
  }

}

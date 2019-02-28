import { Component, OnInit, ViewChild } from '@angular/core';
import { IndicatorService } from '../_services/indicator.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-indicator-reporting-period',
  templateUrl: './indicator-reporting-period.component.html',
  styleUrls: ['./indicator-reporting-period.component.css']
})
export class IndicatorReportingPeriodComponent implements OnInit {
  
  reporting_period_displaycolumns : any[] = ['reportName', 'reportDate', 'dateCreated', 'action', 'edit']
  reportingPeriods : any[] = [];
 reportingformid:number;

  reportingPeriodsDataSource = new MatTableDataSource(this.reportingPeriods);
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor(private indicatorService: IndicatorService) {

   }

  ngOnInit() {
    this.getReportingPeriods();
  }
  
 

  private getReportingPeriods() {
      this.indicatorService.getFormIndicatorReportingPeriods().subscribe(r=>
        {
          
        r.forEach(data => {
            console.log(data);
            this.reportingformid=data.reportingFormId;
            console.log(this.reportingformid);
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

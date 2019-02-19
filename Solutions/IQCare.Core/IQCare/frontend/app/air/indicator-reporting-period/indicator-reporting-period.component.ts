import { Component, OnInit, ViewChild } from '@angular/core';
import { IndicatorService } from '../_services/indicator.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-indicator-reporting-period',
  templateUrl: './indicator-reporting-period.component.html',
  styleUrls: ['./indicator-reporting-period.component.css']
})
export class IndicatorReportingPeriodComponent implements OnInit {
  
  reporting_period_displaycolumns : any[] = ['reportName','reportDate','dateCreated','action']
  reportingPeriods : any[] = [];

  reportingPeriodsDataSource = new MatTableDataSource(this.reportingPeriods);
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor(private indicatorService: IndicatorService) {

   }

  ngOnInit() {
    this.getReportingPeriods();
  }
  

  private getReportingPeriods() {
      this.indicatorService.getFormIndicatorReportingPeriods().subscribe(r=>{
           this.reportingPeriods.push({
               reportName : r.reportName,
               reportDate : r.strReportDate,
               dateCreated : r.dateCreated
           });

          this.reportingPeriodsDataSource = new MatTableDataSource(this.reportingPeriods);
          this.reportingPeriodsDataSource.paginator = this.paginator;
      },(error)=>{

        console.log("An error occured while fetching reporting periods " + error);
      })
  }


  public viewResults(row : any){
    console.log('View Results '+row);
  }

}

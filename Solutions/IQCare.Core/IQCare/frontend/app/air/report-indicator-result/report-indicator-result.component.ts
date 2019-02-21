import { Component, OnInit } from '@angular/core';
import { IndicatorService } from '../_services/indicator.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-report-indicator-result',
  templateUrl: './report-indicator-result.component.html',
  styleUrls: ['./report-indicator-result.component.css']
})
export class ReportIndicatorResultComponent implements OnInit {
  
  reportSections : any[] = [];
  reportingPeriodId : any;
  reportDate : any;
  reportName : any;

  constructor(private indicatorService: IndicatorService,
   private route : ActivatedRoute) { 
       route.params.subscribe(param => {
            this.reportingPeriodId = param['reportingPeriodId'];
       });
  }

  ngOnInit() 
  {
       this.getReportingPeriodIndicatorResults(this.reportingPeriodId);
  }

    private getReportingPeriodIndicatorResults(periodId: any){
        this.indicatorService.getReportingPeriodIndicatorResults(periodId).subscribe(result=>{
            console.log(result +' Result Details Response');

            this.reportSections = result.ReportSections;
            this.reportDate = result.strReportDate;
            this.reportName = result.reportName;

            console .log('Report Sections '+ this.reportSections);
        },(err)=>{
          console.log(err +' An error occured while fetching reporting period results');
        })   

    }

}

import { Component, OnInit,NgZone } from '@angular/core';
import { IndicatorService } from '../_services/indicator.service';
import { ActivatedRoute ,Router} from '@angular/router';


@Component({
  selector: 'app-report-indicator-result',
  templateUrl: './report-indicator-result.component.html',
  styleUrls: ['./report-indicator-result.component.css']
})
export class ReportIndicatorResultComponent implements OnInit {
  
  reportSections : any[] = [];
  reportingPeriodId : any;
  reportingFormId : any;
  reportDate : any;
  reportName : any;
  errorMessage :any;
  isValid : boolean;

  constructor(private indicatorService: IndicatorService,
   private route : ActivatedRoute,
   public zone: NgZone,
   private router: Router) { 
       route.params.subscribe(param => {
            this.reportingPeriodId = param['reportingPeriodId'];
            this.reportingFormId = param['reportingFormId'];
       });
  }

  ngOnInit() 
  {
       this.getReportingPeriodIndicatorResults(this.reportingPeriodId);
  }

  close()
  {
    this.zone.run(() => {
        this.router.navigate(['/air/'],
            { relativeTo: this.route });
    });
   // console.log(messag
  }

    private getReportingPeriodIndicatorResults(periodId: any) {
        this.indicatorService.getReportingPeriodIndicatorResults(periodId).subscribe(result => {           
            this.reportSections = result.reportSections;
            this.reportDate = result.strReportDate;
            this.reportName = result.reportName;
        }, (err) => {
           console.log(' An error occured while fetching reporting period results: '+ err);
             this.errorMessage = err;
        });   
    }

}

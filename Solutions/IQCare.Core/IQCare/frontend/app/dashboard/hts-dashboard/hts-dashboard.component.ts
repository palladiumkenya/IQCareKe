import { Component, OnInit } from '@angular/core';
import {FacilityService} from '../services/facility.service';

@Component({
    selector: 'app-hts-dashboard',
    templateUrl: './hts-dashboard.component.html',
    styleUrls: ['./hts-dashboard.component.css'],
    providers: [ FacilityService ]
})
export class HtsDashboardComponent implements OnInit {
    public htsSummaryData = [
        {data: [], label: 'Facility HTS Testing Summary'}
    ];
    public htsSummaryLabels = ['Total Persons Tested', 'Total Positive',
        'Total Positive And Linkage Form Completed', 'Total Positive And Enrolled In This CCC'];
    public htsSummaryOptions = {
        scaleShowVerticalLines: true,
        responsive: true,        
    };
    public htsSummaryLegend = true;
    public htsSummaryType = 'bar';

    public linkageSummaryLabels = ['Total Positive', 
        'Total Positive And Linkage Form Completed', 
        'Total Positive And Enrolled In This CCC'];
    public linkageSummaryData = [0, 0, 0];
    public linkageSummaryType = 'pie';
    
    constructor(private facilityService: FacilityService) { }
    
    async ngOnInit() {
        this.facilityService.getHtsFacilityStatistics().subscribe(
            (res) => {
                this.htsSummaryData[0].data = [];
                this.linkageSummaryData = [];
                this.htsSummaryData[0].data.push(res[0].totalTested);
                this.htsSummaryData[0].data.push(res[0].totalPositive);
                this.htsSummaryData[0].data.push(res[0].totalPositiveWithLinkageForm);
                this.htsSummaryData[0].data.push(res[0].totalPositiveAndEnrolledInCCC);

                this.linkageSummaryData[0] = res[0].totalPositive;
                this.linkageSummaryData[1] = res[0].totalPositiveWithLinkageForm;
                this.linkageSummaryData[2] = res[0].totalPositiveAndEnrolledInCCC;
            },
            (error) => {
                console.log(error);
            }
        );
    }
}

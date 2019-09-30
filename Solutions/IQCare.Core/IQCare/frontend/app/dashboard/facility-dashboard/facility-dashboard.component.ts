import { Component, OnInit } from '@angular/core';
import {FacilityService} from '../services/facility.service';
import * as moment from 'moment';
import {FormControl} from '@angular/forms';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';

@Component({
    selector: 'app-facility-dashboard',
    templateUrl: './facility-dashboard.component.html',
    styleUrls: ['./facility-dashboard.component.css'],
    providers: [ FacilityService ]
})
export class FacilityDashboardComponent implements OnInit {
    public doughnutChartLabels = ['Scheduled', 'Seen', 'Missed', 'Unscheduled'];
    public doughnutChartData = [120, 150, 180, 90];
    public doughnutChartType = 'doughnut';

    public pieChartLabels = ['Pending VL Tests', 'Complete VL Tests', 'Total Suppressed', 'Total Unsuppressed'];
    public pieChartData = [120, 150, 180, 90];
    public pieChartType = 'pie';

    public barChartOptions = {
        scaleShowVerticalLines: false,
        responsive: true
    };
    public barChartLabels = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    public barChartType = 'bar';
    public barChartLegend = true;
    public barChartData = [
        {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
        {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
    ];
    
    public appointmentSummaryData = [
        {data: [], label: 'Visit Schedule'}
    ];
    public appointmentSummaryLabels = ['Scheduled', 'Seen', 'Missed', 'Total Visits'];
    public appointmentSummaryOptions = {
        scaleShowVerticalLines: false,
        responsive: true
    };
    public appointmentSummaryLegend = true;
    public appointmentSummaryType = 'bar';
    date = new FormControl(new Date());
    
    
    public ilStatisticsData = [0, 0];
    public ilStatisticsLabels = ['Total Outgoing Messages ', 'Total Incoming Messages'];
    public ilStatisticsType = 'pie';

    public careEndingSummaryLabels = ['Total Dead', 'Total Transfer Out', 'Total Documented LTFU'];
    public careEndingSummaryData = [0, 0, 0];
    public careEndingSummaryType = 'doughnut';
    
    constructor(private facilityService: FacilityService) { }
    
    async ngOnInit() {
        const summaryDate = moment(new Date()).format('YYYY-MM-DD').toString();
        this.getFacilityAppointmentSummary(summaryDate);
        this.getFacilityAllCCCVisitCountSummary(summaryDate);
        this.getIlStatistics();
        this.getCareEndingSummary();
    }

    getFacilityAllCCCVisitCountSummary(summaryDate: string) {
        this.facilityService.getTotalCCCVisits(summaryDate).subscribe(
            (res) => {
                if (res) {
                    this.appointmentSummaryData[0].data[3] = res.totalVisits;
                }
            }
        );
    }

    getFacilityAppointmentSummary(summaryDate: string) {
        this.facilityService.getAppointmentStatistics(summaryDate).subscribe(
            (res) => {
                if (res.length > 0) {
                    this.appointmentSummaryData[0].data = [];
                    this.appointmentSummaryData[0].data.push(res[0].total);
                    this.appointmentSummaryData[0].data.push(res[0].met);
                    this.appointmentSummaryData[0].data.push(res[0].missed);

                    this.getFacilityAllCCCVisitCountSummary(summaryDate);
                } else {
                    this.appointmentSummaryData[0].data = [];
                }
            }
        );
    }
    
    addEvent(event: MatDatepickerInputEvent<unknown>) {
        // @ts-ignore
        const summaryDate = moment(event.value.toString()).format('YYYY-MM-DD').toString();
        this.getFacilityAppointmentSummary(summaryDate);        
    }

    private getIlStatistics() {
        this.facilityService.getILStatistics().subscribe(
            (res) => {
                if (res) {
                    this.ilStatisticsData = [];
                    this.ilStatisticsData[0] = res['inbox'];
                    this.ilStatisticsData[1] = res['outbox'];
                }
            }
        );
    }

    private getCareEndingSummary() {
        this.facilityService.getCareEndingSummary().subscribe(
            (res) => {
                if (res) {
                    this.careEndingSummaryData = [];
                    this.careEndingSummaryData[0] = res['totalPatientsDead'];
                    this.careEndingSummaryData[1] = res['totalPatientsTransferedOut'];
                    this.careEndingSummaryData[2] = res['lostToFollowUp'];
                }
            }
        );
    }
}

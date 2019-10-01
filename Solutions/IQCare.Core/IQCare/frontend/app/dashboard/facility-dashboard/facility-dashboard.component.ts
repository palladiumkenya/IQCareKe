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

    public viralLoadOrderLabels = ['Pending VL Tests', 'Complete VL Tests'];
    public viralLoadOrderData = [0, 0];
    public viralLoadOrderType = 'pie';

    public viralLoadResultsLabels = ['Total Suppressed', 'Total Unsuppressed'];
    public viralLoadResultsData = [0, 0];
    public viralLoadResultsType = 'pie';
    
    public familyTestingStatistics: any[] = [];
    public differentiatedCareStatistics: any[] = [];
    public ilStats: any[] = [];
    
    constructor(private facilityService: FacilityService) { }
    
    async ngOnInit() {
        const summaryDate = moment(new Date()).format('YYYY-MM-DD').toString();
        this.getFacilityAppointmentSummary(summaryDate);
        this.getFacilityAllCCCVisitCountSummary(summaryDate);
        this.getIlStatistics();
        this.getCareEndingSummary();
        this.getViralLoadOrderSummary();
        this.getAllViralLoads();
        this.getFamilyTestingStatistics();
        this.getFacilityDiffentiatedCareModelStatistics();
        this.getILMessageStats();
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

    private getViralLoadOrderSummary() {
        this.facilityService.getViralLoadOrderSummary().subscribe(
            (res) => {
                this.viralLoadOrderData = [];
                let pending = 0;
                let complete = 0;
                for (let i = 0; i < res.length; i ++) {
                    if (res[i]['metric'] == 'Complete') {
                        complete = res[i]['count'];
                    }

                    if (res[i]['metric'] == 'pending') {
                        pending = res[i]['count'];
                    }
                }

                this.viralLoadOrderData[0] = pending;
                this.viralLoadOrderData[1] = complete;
            }
        );
    }

    private getAllViralLoads() {
        this.facilityService.getAllViralLoads().subscribe(
            (res) => {
                this.viralLoadResultsData = [];
                const unsuppressed = res.filter(i => i.resultValues >= 1000 && i.results == 'Complete').length;
                const suppressed = res.filter(i => i.resultValues < 1000 && i.results == 'Complete').length;
                
                this.viralLoadResultsData[0] = suppressed;
                this.viralLoadResultsData[1] = unsuppressed;
            }
        );
    }

    private getFamilyTestingStatistics() {
        this.facilityService.getFamilyTestingStatistics().subscribe(
            (res) => {
                this.familyTestingStatistics = res;
            }
        );
    }

    private getFacilityDiffentiatedCareModelStatistics() {
        this.facilityService.getFacilityDiffentiatedCareModelStatistics().subscribe(
            (res) => {
                this.differentiatedCareStatistics = res;
            }
        );
    }

    private getILMessageStats() {
        this.facilityService.getILMessageStats().subscribe(
            (res) => {
                this.ilStats = res;
                console.log(this.ilStats);
            }
        );
    }
}

import { Component, OnInit, NgZone, Directive, Input, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { Subscription } from 'rxjs';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IndicatorService } from '../_services/indicator.service';
import { FormDetailsService } from '../_services/formdetails.service';

@Component({
    selector: 'app-report-section-setting',
    templateUrl: './report-section-setting.component.html',
    styleUrls: ['./report-section-setting.component.css']
})
export class ReportSectionSettingComponent implements OnInit {


    ReportSections: any[] = [];
    SectionList: any[] = [];
    reportsections_displaycolumns: any[] = ['SectionName', 'Active'];

    FormId: number;
    reportSectionDataSource = new MatTableDataSource(this.ReportSections);
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild('ref') ref;
    constructor(private route: ActivatedRoute,
        private snotifyService: SnotifyService,
        private formdetailsservice: FormDetailsService,
        private notificationService: NotificationService,
        private indicatorService: IndicatorService,
        public zone: NgZone,
        private router: Router,
        private spinner: NgxSpinnerService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.FormId = params['reportingFormId'];

        });

        if (this.FormId > 0) {
            this.getFormSections();
            console.log(this.ReportSections);
        }
    }

    getCheckedValue(sectionname: string, event) {
        //   onClick(event) {
        //event.preventDefault();
        // console.log('onClick this.ref._checked '+ this.ref._checked);
        //  this.ref._checked = !this.ref._checked;
        // }
        for (let i = 0; i < this.ReportSections.length; i++) {
            if (this.ReportSections[i]['SectionName'] == sectionname) {

                this.ReportSections[i]['Active'] = event.checked;


            }

        }

        for (let i = 0; i < this.SectionList.length; i++) {
            if (this.SectionList[i]['SectionName'] == sectionname) {
                this.SectionList[i]['Active'] = event.checked;


            }

        }

    }

    Submit() {
        console.log(this.SectionList);
        this.spinner.show();
        this.formdetailsservice.EditReportSettings(this.SectionList).subscribe(
            (response) => {
                console.log(response);
             

                this.snotifyService.success(response.message, 'Settings', this.notificationService.getConfig());

                this.zone.run(() => {
                    this.router.navigate(['/air/report/'+this.FormId],
                        { relativeTo: this.route });
                });
            },
            (error) => {
                this.snotifyService.error('Error changing settings of section' + error, 'Settings',
                    this.notificationService.getConfig());
                this.spinner.hide();
            },
            () => {
                this.spinner.hide();
            }

        );


    }
    private getFormSections() {
        this.indicatorService.getReportSections(this.FormId).subscribe(r => {
            r.forEach(data => {
                console.log(data);

                this.ReportSections.push({
                    SectionName: data.name,
                    Active: data.active

                });
                this.SectionList.push({
                    Id: data.id,
                    SectionName: data.name,
                    Active: data.active 

                })
            });
            this.reportSectionDataSource = new MatTableDataSource(this.ReportSections);
            this.reportSectionDataSource.paginator = this.paginator;

        }, (error) => {
            console.log("An Error occurred while fetching reporting periods " + error);
        })

    }
}

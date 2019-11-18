import { Component, OnInit, Output, EventEmitter, Inject, NgZone, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import * as moment from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MatStepper } from '@angular/material';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { PrepService } from '../_services/prep.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../shared/_services/notification.service';
@Component({
    selector: 'app-prep-visitcheckin',
    templateUrl: './prep-visitcheckin.component.html',
    styleUrls: ['./prep-visitcheckin.component.css']
})
export class PrepVisitcheckinComponent implements OnInit {

    EnrollmentDate: Date;
    patientId: number;
    personId: number;
    serviceId: number;
    formGroup: FormGroup;
    maxDate: Date;
    ServiceCheckinId: number = 0;
    isLinear: boolean = true;
    OptionToShow: LookupItemView[] = [];
    userId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @ViewChild('stepper') stepper: MatStepper;
    constructor(
        private prepService: PrepService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private personHomeService: PersonHomeService,
        private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private notificationService: NotificationService,
        private dialogRef: MatDialogRef<PrepVisitcheckinComponent>,
        private snotifyService: SnotifyService,
        private spinner: NgxSpinnerService,
        @Inject(MAT_DIALOG_DATA) public data: any

    ) {
        this.maxDate = new Date();
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.patientId = data.patientId;
        this.personId = data.personId;
        this.serviceId = data.serviceId;
    }

    ngOnInit() {



        this.formGroup = this._formBuilder.group({
            PatientCheckinStartTime: new FormControl('', [Validators.required]),
            emrmode: new FormControl('', [Validators.required]),


        });






        this._lookupItemService.getByGroupName('EMRImplementation').subscribe(
            (res) => {
                this.OptionToShow = res['lookupItems'];
            }
        );

    }

    save() {
        if (this.formGroup.valid) {
            this.spinner.show();
            const { PatientCheckinStartTime, emrmode } = this.formGroup.value;
            localStorage.removeItem('PrepCheckinEmrMode');
            localStorage.removeItem('PrepCheckInId');
            localStorage.removeItem('PrepCheckinEmrMode');
            localStorage.removeItem('PrepVisitDate');
            localStorage.removeItem('prepCheckinPatientId');
            localStorage.removeItem('PrepCheckinEmrModenumber')
            this.prepService.PatientCheckin(this.patientId, this.serviceId, this.userId
                , moment(PatientCheckinStartTime).toDate(), parseInt(emrmode, 10), 1, false).subscribe((result) => {
                    let optionlist: LookupItemView[];
                    optionlist = this.OptionToShow.filter(x => x.itemId.toString() == emrmode.toString());
                    if (optionlist.length > 0) {
                        localStorage.setItem('PrepCheckinEmrMode', optionlist[0].itemName);
                    }
                    localStorage.setItem('prepCheckinPatientId', this.patientId.toString());
                    localStorage.setItem('PrepCheckinEmrModenumber',emrmode.toString());
                    this.ServiceCheckinId = result['serviceCheckInId'];
                    if (this.ServiceCheckinId > 0) {
                        this.snotifyService.success(
                            result['message'], 'PatientCheck In ',
                            this.notificationService.getConfig());

                        localStorage.setItem('PrepVisitDate', PatientCheckinStartTime.toString());
                        localStorage.setItem('PrepCheckInId', this.ServiceCheckinId.toString());
                        localStorage.setItem('PrepDateRecorded' , new Date().toString());
                
                        this.dialogRef.close();
                        this.zone.run(() => {
                            this.zone.run(() => {
                                this.router.navigate(
                                    ['/prep/prepfollowupworkflow/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                                    { relativeTo: this.route });
                            });
                        });
                



                    }
                    else {
                        this.snotifyService.error(
                            'Error Checking In the Patient', 'PatientCheck In ',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    }
                }, (error) => {
                    this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
                    this.spinner.hide();
                }, () => {
                    this.spinner.hide();
                });

        }
    }

    close() {
        this.dialogRef.close();
    }

    LoadPrepEnrollmentDate(patientId: number, serviceId: number): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, serviceId).subscribe(
            (result) => {
                if (result != null) {
                    this.EnrollmentDate = result.enrollmentDate;
                }
            }, (error) => {
                console.log(error);
            });
    }

}

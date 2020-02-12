import { FormControlService } from './../../shared/_services/form-control.service';
import { RecordsService } from './../../records/_services/records.service';
import { PersonHomeService } from './../services/person-home.service';
import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { RegistrationService } from '../../registration/_services/registration.service';
import { EnrollmentService } from '../../registration/_services/enrollment.service';
import { Enrollment } from '../../registration/_models/enrollment';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import * as Consent from '../../shared/reducers/app.states';
import { Store } from '@ngrx/store';
import { AppStateService } from '../../shared/_services/appstate.service';
import { AppEnum } from '../../shared/reducers/app.enum';
import { NumberValueAccessor } from '@angular/forms/src/directives';

@Component({
    selector: 'app-reenrollment',
    templateUrl: './reenrollment.component.html',
    styleUrls: ['./reenrollment.component.css']
})
export class ReenrollmentComponent implements OnInit {
    formGroup: FormGroup;
    serviceId: number;
    personId: number;
    userId: number;
    patientId: number;
    maxDate: Date;
    minDate: Date;
    serviceCode: string;

    constructor(private route: ActivatedRoute,
        private fb: FormBuilder,
        private personHomeService: PersonHomeService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private router: Router,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private recordsService: RecordsService,
        private formControlService: FormControlService) { }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { id, serviceId, serviceCode } = params;
            this.personId = id;
            this.serviceId = serviceId;
            this.serviceCode = serviceCode;

        });




        this.userId = JSON.parse(localStorage.getItem('appUserId'));


        this.formGroup = new FormGroup({
            ReEnrollmentDate: new FormControl([Validators.required]),

        });
        this.loadPatient();

    }

    loadPatient(): void {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {

                this.patientId = result.id;

            },
            (error) => {
                console.log(error);
            }
        );
    }


    Save() {
        const { ReEnrollmentDate } = this.formGroup.value;
        this.enrollmentService.reenrollPatient(this.patientId, ReEnrollmentDate, this.userId,
            this.serviceId).subscribe((response) => {
                this.snotifyService.success('Successfully reenrolled' + response['message'],
                    'Reenrollment',
                    this.notificationService.getConfig());
                localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                this.store.dispatch(new Consent.PatientId(this.patientId));
                this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                    this.patientId).subscribe();

                /* this.zone.run(() => {
                     this.zone.run(() => {
                         this.router.navigate(
                             ['/prep/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                             { relativeTo: this.route });
                     });
                 });*/


                this.zone.run(() => {
                    this.zone.run(() => {
                        this.router.navigate(
                            ['/dashboard/personhome/' + this.personId],
                            { relativeTo: this.route });
                    });
                });


            },

                (err) => {
                    this.snotifyService.error('Error Completing reenrollment ' + err, 'Reenrollment', this.notificationService.getConfig());
                });


    }
    cancel() {
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/dashboard/personhome/' + this.personId],
                    { relativeTo: this.route });
            });
        });

    }
    submitReEnrollment() {

        if (this.formGroup.valid == true) {
            this.Save();
        }
    }

}

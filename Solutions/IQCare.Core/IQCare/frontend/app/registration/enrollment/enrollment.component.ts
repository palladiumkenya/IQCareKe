import { Component, NgZone, OnInit } from '@angular/core';
import { Enrollment } from '../_models/enrollment';
import { EnrollmentService } from '../_services/enrollment.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { AppStateService } from '../../shared/_services/appstate.service';
import { AppEnum } from '../../shared/reducers/app.enum';
import { RegistrationService } from '../_services/registration.service';

@Component({
    selector: 'app-enrollment',
    templateUrl: './enrollment.component.html',
    styleUrls: ['./enrollment.component.css'],
})
export class EnrollmentComponent implements OnInit {
    enrollment: Enrollment;
    patientId: number;
    personId: number;
    createdBy: number;
    facilityName: string;
    appPosID: number;

    maxDate: any;

    constructor(private enrollmentService: EnrollmentService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private store: Store<AppState>,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private appStateService: AppStateService,
        private registrationService: RegistrationService) {
        this.maxDate = new Date();
    }
    ngOnInit() {
        // this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.createdBy = JSON.parse(localStorage.getItem('appUserId'));
        this.facilityName = localStorage.getItem('appLocation');
        this.appPosID = JSON.parse(localStorage.getItem('appPosID'));

        this.enrollment = new Enrollment();

        this.enrollment.ServiceAreaId = 2;
    }


    onSubmit() {
        // this.enrollment.PatientId = this.patientId;
        this.enrollment.PersonId = this.personId;
        this.enrollment.CreatedBy = this.createdBy;
        this.enrollment.DateOfEnrollment = this.enrollment.RegistrationDate;

        this.registrationService.addPatient(this.personId, this.createdBy, this.enrollment.DateOfEnrollment).subscribe((res) => {
            console.log(res);
            this.enrollment.PatientId = res['patientId'];
            localStorage.setItem('patientId', res['patientId']);
            localStorage.removeItem('isHtsEnrolled');

            this.enrollmentService.enrollClient(this.enrollment).subscribe(data => {
                this.store.dispatch(new Consent.IsEnrolled(true));
                this.appStateService.addAppState(AppEnum.ENROLLED, this.enrollment.PersonId,
                    this.enrollment.PatientId, null, null).subscribe();
            }, err => {
                this.snotifyService.error('Error Registering to HTS ' + err,
                    'HTS Service Registration', this.notificationService.getConfig());
            }, () => {
                this.snotifyService.success('Successfully Registered to HTS',
                    'HTS Service Registration', this.notificationService.getConfig());
                this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
            });
        });

        /*this.enrollmentService.enrollClient(this.enrollment).subscribe(data => {
            this.store.dispatch(new Consent.IsEnrolled(true));
            this.appStateService.addAppState(AppEnum.ENROLLED, this.enrollment.PersonId, this.enrollment.PatientId,
                null, null).subscribe(res => {
                    console.log(res);
                });
        }, err => {
            this.snotifyService.error('Error Registering to HTS ' + err, 'HTS Service Registration', this.notificationService.getConfig());
        }, () => {
            this.snotifyService.success('Successfully Registered to HTS', 'HTS Service Registration', this.notificationService.getConfig());
            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        });*/
    }
}

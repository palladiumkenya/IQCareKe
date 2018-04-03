import {Component, NgZone, OnInit} from '@angular/core';
import {Enrollment} from '../_models/enrollment';
import {EnrollmentService} from '../_services/enrollment.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import {NotificationService} from '../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

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

    maxDate: any;

    constructor(private enrollmentService: EnrollmentService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
        this.maxDate = new Date();
    }
    ngOnInit() {
        localStorage.setItem('createdBy', JSON.stringify(1));
        console.log(localStorage.getItem('patientId'));
        console.log(localStorage.getItem('personId'));
        this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.createdBy = JSON.parse(localStorage.getItem('createdBy'));

        this.enrollment = new Enrollment();

        this.enrollment.ServiceAreaId  = 2;
    }


    onSubmit() {
        this.enrollment.PatientId = this.patientId;
        this.enrollment.PersonId = this.personId;
        this.enrollment.CreatedBy = this.createdBy;

        console.log(this.enrollment);

        this.enrollmentService.enrollClient(this.enrollment).subscribe(data => {
            this.store.dispatch(new Consent.IsEnrolled(true));
            this.snotifyService.success('Successfully Enrolled Client', 'Enrollment', this.notificationService.getConfig());
            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
            console.log(err);
            this.snotifyService.error('Error enrolling client ' + err, 'Enrollment', this.notificationService.getConfig());
        });
    }
}

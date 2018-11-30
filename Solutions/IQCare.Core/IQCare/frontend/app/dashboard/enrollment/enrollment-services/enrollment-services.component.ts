import { PersonHomeService } from './../../services/person-home.service';
import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { RegistrationService } from '../../../registration/_services/registration.service';
import { EnrollmentService } from '../../../registration/_services/enrollment.service';
import { Enrollment } from '../../../registration/_models/enrollment';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import * as Consent from '../../../shared/reducers/app.states';
import { Store } from '@ngrx/store';

@Component({
    selector: 'app-enrollment-services',
    templateUrl: './enrollment-services.component.html',
    styleUrls: ['./enrollment-services.component.css']
})
export class EnrollmentServicesComponent implements OnInit {
    formGroup: FormGroup;
    serviceId: number;
    personId: number;
    userId: number;
    posId: string;
    patientId: number;
    maxDate: Date;
    serviceCode: string;

    patientTypeOptions: any;

    constructor(private route: ActivatedRoute,
        private fb: FormBuilder,
        private personHomeService: PersonHomeService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private router: Router,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private store: Store<AppState>) {
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.posId = localStorage.getItem('appPosID');
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const { serviceId, id, serviceCode } = params;
                this.serviceId = serviceId;
                this.personId = id;
                this.serviceCode = serviceCode;
            }
        );

        this.formGroup = new FormGroup({
            EnrollmentDate: new FormControl([Validators.required]),
            // Status: new FormControl([Validators.required]),
            identifiers: new FormArray([])
        });

        this.personHomeService.getServiceAreaIdentifiers(this.serviceId).subscribe(
            (res) => {
                const { identifiers, serviceAreaIdentifiers } = res;

                for (let i = 0; i < serviceAreaIdentifiers.length; i++) {
                    for (let j = 0; j < identifiers.length; j++) {
                        if (identifiers[j]['id'] == serviceAreaIdentifiers[i]['identifierId']) {
                            (<FormArray>this.formGroup.get('identifiers')).push(this.fb.group({
                                name: '',
                                displayName: identifiers[j]['displayName'],
                                identifierId: identifiers[j]['id']
                            }));
                        }
                    }

                }
            }
        );

        this.personHomeService.getPatientTypes().subscribe(
            (res) => {
                const { value } = res['lookupItems'][0];
                this.patientTypeOptions = value;
                console.log(res['lookupItems']);
            }
        );
    }

    submitEnrollment() {
        if (this.formGroup.valid) {
            const { EnrollmentDate, Status, identifiers } = this.formGroup.value;

            const enrollment = new Enrollment();
            for (let i = 0; i < identifiers.length; i++) {
                if (identifiers[i]['name'] != null || identifiers[i]['name'] != '') {
                    enrollment.ServiceIdentifiersList.push({
                        'IdentifierId': identifiers[i]['identifierId'],
                        'IdentifierValue': identifiers[i]['name']
                    });
                }
            }
            enrollment.DateOfEnrollment = EnrollmentDate;
            enrollment.ServiceAreaId = this.serviceId;
            enrollment.PersonId = this.personId;
            enrollment.CreatedBy = this.userId;
            enrollment.RegistrationDate = EnrollmentDate;
            enrollment.PosId = this.posId;

            this.registrationService.addPatient(this.personId, this.userId, EnrollmentDate, this.posId).subscribe(
                (res) => {
                    this.patientId = res['patientId'];
                    enrollment.PatientId = this.patientId;
                    this.enrollmentService.enrollClient(enrollment).subscribe(
                        (response) => {
                            this.snotifyService.success('Successfully Enrolled ', 'Enrollment',
                                this.notificationService.getConfig());

                            localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                            this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                            switch (this.serviceCode) {
                                case 'HTS':
                                    this.zone.run(() => {
                                        localStorage.setItem('personId', this.personId.toString());
                                        localStorage.setItem('patientId', this.patientId.toString());
                                        localStorage.setItem('serviceAreaId', this.serviceId.toString());
                                        this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                                    });
                                    break;
                                default:
                                    this.zone.run(() => {
                                        this.router.navigate(
                                            ['/pmtct/patient-encounter/' + this.patientId + '/' + this.personId + '/' + this.serviceId + '/'
                                                + this.serviceCode],
                                            { relativeTo: this.route });
                                    });
                                    break;
                            }
                        },
                        (err) => {
                            this.snotifyService.error('Error completing enrollment ' + err, 'Enrollment',
                                this.notificationService.getConfig());
                        }
                    );
                }
            );
        }
    }
}

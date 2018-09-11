import { PersonHomeService } from './../../services/person-home.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { RegistrationService } from '../../../registration/_services/registration.service';
import { EnrollmentService } from '../../../registration/_services/enrollment.service';
import { Enrollment } from '../../../registration/_models/enrollment';

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

    patientTypeOptions: any;

    constructor(private route: ActivatedRoute,
        private fb: FormBuilder,
        private personHomeService: PersonHomeService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService) {
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.posId = localStorage.getItem('appPosID');
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const { serviceId, id } = params;
                this.serviceId = serviceId;
                this.personId = id;
            }
        );

        this.formGroup = new FormGroup({
            EnrollmentDate: new FormControl([Validators.required]),
            Status: new FormControl([Validators.required]),
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
        console.log(this.formGroup.value);
        if (this.formGroup.valid) {
            const { EnrollmentDate, Status, identifiers } = this.formGroup.value;

            const enrollment = new Enrollment();
            for (let i = 0; i < identifiers.length; i++) {
                enrollment.ServiceIdentifiersList.push({
                    'IdentifierId': identifiers[i]['identifierId'],
                    'IdentifierValue': identifiers[i]['name']
                });
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
                            console.log(response);
                        }
                    );
                }
            );
        }
    }
}

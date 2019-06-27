import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from './../../../../shared/_services/notification.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, NgZone } from '@angular/core';
import { Enrollment } from '../../../../registration/_models/enrollment';
import { LookupItemService } from '../../../../shared/_services/lookup-item.service';
import { RecordsService } from '../../../../records/_services/records.service';
import { PersonHomeService } from '../../../services/person-home.service';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { EnrollmentService } from '../../../../registration/_services/enrollment.service';
import { AppStateService } from '../../../../shared/_services/appstate.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../../../shared/reducers/app.states';
import { AppEnum } from '../../../../shared/reducers/app.enum';
import { forkJoin } from 'rxjs';
import { PersonPopulation } from '../../../../registration/_models/personPopulation';
@Component({
    selector: 'app-hts',
    templateUrl: './hts.component.html',
    styleUrls: ['./hts.component.css'],
    providers: [LookupItemService, RecordsService]
})
export class HtsComponent implements OnInit {
    form: FormGroup;
    serviceId: number;
    personId: number;
    userId: number;
    posId: string;
    patientId: number;
    maxDate: Date;
    minDate: Date;
    serviceCode: string;
    isEdit: boolean = false;
    ageNumber: number;

    priorityPops: LookupItemView[] = [];
    keyPops: LookupItemView[] = [];
    serviceAreaIdentifiers: any[] = [];
    personPopulation: PersonPopulation;

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        private _lookupItemService: LookupItemService,
        private recordsService: RecordsService,
        private personHomeService: PersonHomeService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private router: Router,
        public zone: NgZone,
        private store: Store<AppState>,
        private appStateService: AppStateService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.personPopulation = new PersonPopulation();

        this.route.params.subscribe(
            (params) => {
                const { serviceId, id, serviceCode, edit } = params;
                this.serviceId = serviceId;
                this.personId = id;
                this.serviceCode = serviceCode;

                if (edit == 1) {
                    this.isEdit = true;
                }
            }
        );
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.posId = localStorage.getItem('appPosID');
        this.ageNumber = parseInt(JSON.parse(localStorage.getItem('ageNumber')), 10);

        this.form = this._formBuilder.group({
            EnrollmentDate: new FormControl('', [Validators.required]),
            EnrollmentNumber: new FormControl('', [Validators.required]),
            populationType: new FormControl('', [Validators.required]),
            priorityPop: new FormControl('', [Validators.required]),
            KeyPopulation: new FormControl('', [Validators.required]),
            priorityPopulation: new FormControl('', [Validators.required]),
        });

        if (this.ageNumber < 15) {
            this.form.controls.populationType.disable({ onlySelf: true });
            this.form.controls.priorityPop.disable({ onlySelf: true });
        }
        this.form.controls.KeyPopulation.disable({ onlySelf: true });
        this.form.controls.priorityPopulation.disable({ onlySelf: true });

        this._lookupItemService.getByGroupName('PriorityPopulation').subscribe(
            (result) => {
                this.priorityPops = result['lookupItems'];
            },
            (error) => {

            }
        );


        this._lookupItemService.getByGroupName('HTSKeyPopulation').subscribe(
            (result) => {
                this.keyPops = result['lookupItems'];
            },
            (error) => {

            }
        );

        this.recordsService.getPersonDetails(this.personId).subscribe(
            (res) => {
                const { registrationDate } = res[0];
                if (registrationDate) {
                    this.minDate = registrationDate;
                } else {
                    this.minDate = new Date();
                }
            }
        );

        this.personHomeService.getServiceAreaIdentifiers(this.serviceId).subscribe(
            (res) => {
                const { identifiers, serviceAreaIdentifiers } = res;
                this.serviceAreaIdentifiers = serviceAreaIdentifiers;
            }
        );

        if (this.isEdit) {
            this.loadPatient();
        }
    }

    loadPatient(): void {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                // load enrollment Date
                this.loadHtsEnrollmentDate(result.id);

                // load population types
                this.loadPopulationTypes(this.personId);

                // load hts identifiers
                this.loadIdentifiers(result.id);

                // load priority population
                this.loadPriorityPopulation(this.personId);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPriorityPopulation(personId: number): void {
        this.personHomeService.getPersonPriorityTypes(personId).subscribe(
            (result) => {
                // console.log(result);
                if (result.length > 0) {
                    this.form.controls.priorityPop.setValue(1);
                    this.form.controls.priorityPopulation.enable({ onlySelf: false });
                    const arrayValue = [];
                    result.forEach(element => {
                        arrayValue.push(element.priorityId);
                    });
                    this.form.controls.priorityPopulation.setValue(arrayValue);
                } else {
                    this.form.controls.priorityPop.setValue(2);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadIdentifiers(patientId: number): void {
        this.recordsService.getPatientIdentifiersList(patientId).subscribe(
            (result) => {
                if (result.length > 0) {
                    this.form.get('EnrollmentNumber').setValue(result[0].identifierValue);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPopulationTypes(personId: number) {
        this.personHomeService.getPersonPopulationType(personId).subscribe(
            (result) => {
                if (result.length > 0) {
                    if (result[0].populationType == 'General Population') {
                        this.form.controls.populationType.setValue(1);
                    } else {
                        if (this.ageNumber >= 15) {
                            this.form.controls.populationType.setValue(2);
                            this.form.controls.KeyPopulation.enable({ onlySelf: false });
                            const arrayValue = [];
                            result.forEach(element => {
                                arrayValue.push(element.populationCategory);
                            });
                            this.form.controls.KeyPopulation.setValue(arrayValue);
                        }
                    }
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadHtsEnrollmentDate(patientId: number): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, this.serviceId).subscribe(
            (result) => {
                this.form.controls.EnrollmentDate.setValue(result.enrollmentDate);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public onPopulationTypeChange() {
        const popType = this.form.controls.populationType.value;
        if (popType == 1 && this.ageNumber >= 15) {
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            this.form.controls.KeyPopulation.setValue([]);
        } else if (popType == 2 && this.ageNumber >= 15) {
            this.form.controls.KeyPopulation.enable({ onlySelf: false });
        }
    }

    public onPriorityChange() {
        const priorityPop = this.form.controls.priorityPop.value;
        if (priorityPop == 2 && this.ageNumber >= 15) {
            this.form.controls.priorityPopulation.disable({ onlySelf: true });
            this.form.controls.priorityPopulation.setValue([]);
        } else if (priorityPop == 1 && this.ageNumber >= 15) {
            this.form.controls.priorityPopulation.enable({ onlySelf: false });
        }
    }

    public submitEnrollment() {
        if (!this.form.valid) {
            this.snotifyService.error('Please complete the form before submitting', 'Enrollment',
                this.notificationService.getConfig());
        } else {
            this.save();
        }
    }

    public save() {
        const enrollment = new Enrollment();
        const { EnrollmentDate, EnrollmentNumber, populationType, priorityPop, KeyPopulation, priorityPopulation } = this.form.value;

        this.personPopulation.KeyPopulation = KeyPopulation;
        this.personPopulation.populationType = populationType;
        this.personPopulation.priorityPop = priorityPop;
        this.personPopulation.priorityPopulation = priorityPopulation;

        enrollment.ServiceIdentifiersList.push({
            'IdentifierId': this.serviceAreaIdentifiers[0]['identifierId'],
            'IdentifierValue': EnrollmentNumber
        });

        enrollment.DateOfEnrollment = EnrollmentDate;
        enrollment.ServiceAreaId = this.serviceId;
        enrollment.PersonId = this.personId;
        enrollment.CreatedBy = this.userId;
        enrollment.RegistrationDate = EnrollmentDate;
        enrollment.PosId = this.posId;

        const populationTypes = this.registrationService.addPersonPopulationType(this.personId,
            this.userId, this.personPopulation);
        const addPatient = this.registrationService.addPatient(this.personId, this.userId, EnrollmentDate, this.posId);

        forkJoin([addPatient, populationTypes]).subscribe(
            results => {
                this.patientId = results[0]['patientId'];
                enrollment.PatientId = this.patientId;
                this.enrollmentService.enrollClient(enrollment).subscribe(
                    (response) => {
                        this.snotifyService.success('Successfully Enrolled ', 'Enrollment',
                            this.notificationService.getConfig());

                        localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                        this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                        this.store.dispatch(new Consent.PatientId(this.patientId));
                        this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                            this.patientId).subscribe();


                        this.zone.run(() => {
                            localStorage.setItem('personId', this.personId.toString());
                            localStorage.setItem('patientId', this.patientId.toString());
                            localStorage.setItem('serviceAreaId', this.serviceId.toString());
                            this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                        });
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

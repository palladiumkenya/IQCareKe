import { HivReConfirmatoryTestsCommand } from './../../../_model/HivReConfirmatoryTestsCommand';
import { FormControlService } from './../../../../shared/_services/form-control.service';
import { PersonHomeService } from './../../../services/person-home.service';
import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemService } from '../../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { forkJoin } from 'rxjs';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { PersonPopulation } from '../../../../registration/_models/personPopulation';
import { ServiceEntryPointCommand } from '../../../_model/ServiceEntryPointCommand';
import { EnrollmentService } from '../../../../registration/_services/enrollment.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../../../shared/reducers/app.states';
import { AppEnum } from '../../../../shared/reducers/app.enum';
import { AppStateService } from '../../../../shared/_services/appstate.service';
import { Enrollment } from '../../../../registration/_models/enrollment';
import { SearchService } from '../../../../registration/_services/search.service';
import { RecordsService } from '../../../../records/_services/records.service';

@Component({
    selector: 'app-ccc',
    templateUrl: './ccc.component.html',
    styleUrls: ['./ccc.component.css']
})
export class CccComponent implements OnInit {
    form: FormGroup;
    personId: number;
    patientId: number;
    serviceId: number;
    serviceCode: string;
    userId: number;
    posId: string;
    isEdit: boolean = false;
    ageNumber: number;

    maxDate: Date;
    minDate: Date;
    personPopulation: PersonPopulation;

    keyPops: LookupItemView[] = [];
    patientTypes: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    hivTestTypes: LookupItemView[] = [];
    reConfirmatoryTestResults: LookupItemView[] = [];
    entrypoints: LookupItemView[] = [];
    serviceAreaIdentifiers: any[] = [];
    identifiers: any[] = [];

    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private personHomeService: PersonHomeService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private searchService: SearchService,
        private recordsService: RecordsService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const { id, serviceId, serviceCode, edit } = params;
            this.personId = id;
            this.serviceId = serviceId;
            this.serviceCode = serviceCode;
            localStorage.setItem('partnerId', this.personId.toString());

            if (edit == 1) {
                this.isEdit = true;
            }
        });
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.ageNumber = parseInt(JSON.parse(localStorage.getItem('ageNumber')), 10);
        this.posId = localStorage.getItem('appPosID');
        this.personPopulation = new PersonPopulation();

        this.form = this._formBuilder.group({
            KeyPopulation: new FormControl('', [Validators.required]),
            PatientType: new FormControl('', [Validators.required]),
            populationType: new FormControl('', [Validators.required]),
            EnrollmentDate: new FormControl('', [Validators.required]),
            ReConfirmatoryTest: new FormControl('', [Validators.required]),
            TypeOfTest: new FormControl('', [Validators.required]),
            ReConfirmatoryTestResult: new FormControl('', [Validators.required]),
            ReConfirmatoryTestDate: new FormControl('', [Validators.required]),
            EntryPoint: new FormControl('', [Validators.required]),
        });

        // default form state
        this.form.controls.KeyPopulation.disable({ onlySelf: true });
        this.form.controls.TypeOfTest.disable({ onlySelf: true });
        this.form.controls.ReConfirmatoryTestResult.disable({ onlySelf: true });
        this.form.controls.ReConfirmatoryTestDate.disable({ onlySelf: true });
        this.form.controls.EnrollmentDate.disable({ onlySelf: true });
        this.form.controls.EntryPoint.disable({ onlySelf: true });

        if (this.ageNumber < 15) {
            this.form.controls.populationType.disable({ onlySelf: true });
        }

        this._lookupItemService.getByGroupName('KeyPopulation').subscribe(
            (res) => {
                this.keyPops = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('PatientType').subscribe(
            (res) => {
                this.patientTypes = res['lookupItems'];
                this.patientTypes = this.patientTypes.sort((obj, obj2) => obj2.itemId - obj.itemId);
            }
        );

        this._lookupItemService.getByGroupName('YesNo').subscribe(
            (res) => {
                this.yesNoOptions = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('HivTestTypes').subscribe(
            (res) => {
                this.hivTestTypes = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('ReConfirmatoryTest').subscribe(
            (res) => {
                this.reConfirmatoryTestResults = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('Entrypoint').subscribe(
            (res) => {
                this.entrypoints = res['lookupItems'];
            }
        );

        this.personHomeService.getServiceAreaIdentifiers(this.serviceId).subscribe(
            (res) => {
                const { identifiers, serviceAreaIdentifiers } = res;
                this.serviceAreaIdentifiers = serviceAreaIdentifiers;
                this.identifiers = identifiers;
                serviceAreaIdentifiers.forEach(element => {
                    identifiers.forEach(identifier_element => {
                        if (identifier_element['id'] == element['identifierId']) {
                            if (element.requiredFlag) {
                                if (identifier_element['code'] == 'CCCNumber') {
                                    this.form.addControl(identifier_element['code'], new FormControl('', Validators.compose([
                                        Validators.required,
                                        Validators.pattern(/^((?!(0))[0-9]{10})$/)
                                    ])));
                                } else {
                                    this.form.addControl(identifier_element['code'], new FormControl('', [Validators.required]));
                                }
                                this.form.addControl(identifier_element['id'], new FormControl(identifier_element['id']));
                            } else {
                                this.form.addControl(identifier_element['code'], new FormControl(''));
                                this.form.addControl(identifier_element['id'], new FormControl(identifier_element['id']));
                            }
                        }
                    });
                });
                // console.log(serviceAreaIdentifiers);
            }
        );

        this.recordsService.getPersonDetails(this.personId).subscribe(
            (res) => {
                // console.log(res);
                const { dateOfBirth } = res[0];
                if (dateOfBirth) {
                    this.minDate = dateOfBirth;
                } /*else {
                    this.minDate = new Date();
                }*/

            }
        );

        if (this.isEdit) {
            this.loadCCCEnrollmentData();
        }
    }

    loadCCCEnrollmentData(): void {
        this.form.controls.ReConfirmatoryTest.disable({ onlySelf: true });
        this.form.controls.TypeOfTest.disable({ onlySelf: true });
        this.form.controls.ReConfirmatoryTestResult.disable({ onlySelf: true });
        this.form.controls.ReConfirmatoryTestDate.disable({ onlySelf: true });
        this.form.controls.EnrollmentDate.enable({ onlySelf: false });
        this.form.controls.EntryPoint.enable({ onlySelf: false });

        // load patient
        this.loadPatient();
    }

    loadPatient(): void {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                console.log(result);
                this.form.controls.PatientType.setValue(result.patientType);
                // load population type
                this.loadPopulationTypes(this.personId);

                // load entrypoint
                this.loadEntryPoints(result.id);

                // load identifiers
                this.loadIdentifiers(result.id);

                // load enrollment Date
                this.loadEnrollmentDate(result.id);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadEnrollmentDate(patientId: any): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, this.serviceId).subscribe(
            (result) => {
                // console.log(result);
                this.form.controls.EnrollmentDate.setValue(result.enrollmentDate);
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
                    this.identifiers.forEach(element => {
                        result.forEach(patientIdentifiers => {
                            if (patientIdentifiers.identifierTypeId == element.id) {
                                this.form.get(element.code).setValue(patientIdentifiers.identifierValue);
                            }
                        });
                    });
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadEntryPoints(patientId: number): void {
        this.personHomeService.getPatientServiceAreaEntryPoints(this.serviceId, patientId).subscribe(
            (result) => {
                // console.log(result);
                this.form.controls.EntryPoint.setValue(result.entryPointId);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPopulationTypes(personId: number): void {
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

    onPopulationTypeChange() {
        const popType = this.form.controls.populationType.value;
        if (popType == 1) {
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            this.form.controls.KeyPopulation.setValue([]);
        } else if (popType == 2 && this.ageNumber >= 15) {
            this.form.controls.KeyPopulation.enable({ onlySelf: false });
        }
    }

    onReConfirmatoryTestChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.form.controls.TypeOfTest.enable({ onlySelf: false });
            this.form.controls.ReConfirmatoryTestResult.enable({ onlySelf: false });
            this.form.controls.ReConfirmatoryTestDate.enable({ onlySelf: false });
            this.form.controls.EnrollmentDate.enable({ onlySelf: false });
            this.form.controls.EntryPoint.enable({ onlySelf: false });

            // clear form
            this.form.controls.TypeOfTest.setValue('');
            this.form.controls.ReConfirmatoryTestResult.setValue('');
            this.form.controls.ReConfirmatoryTestDate.setValue('');
            this.form.controls.EnrollmentDate.setValue('');
            this.form.controls.EntryPoint.setValue('');

            this.serviceAreaIdentifiers.forEach(element => {
                this.identifiers.forEach(val => {
                    if (val['id'] == element['identifierId']) {
                        this.form.get(`${val['code']}`).enable({ onlySelf: false });
                    }
                });
            });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.controls.TypeOfTest.disable({ onlySelf: true });
            this.form.controls.ReConfirmatoryTestResult.disable({ onlySelf: true });
            this.form.controls.ReConfirmatoryTestDate.disable({ onlySelf: true });
            this.form.controls.EnrollmentDate.disable({ onlySelf: true });
            this.form.controls.EntryPoint.disable({ onlySelf: true });

            this.serviceAreaIdentifiers.forEach(element => {
                this.identifiers.forEach(val => {
                    if (val['id'] == element['identifierId']) {
                        this.form.get(`${val['code']}`).disable({ onlySelf: true });
                    }
                });
            });
        }
    }

    onReConfirmatoryTestResultChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Positive') {
            this.form.controls.EnrollmentDate.enable({ onlySelf: false });
            this.form.controls.EntryPoint.enable({ onlySelf: false });

            this.serviceAreaIdentifiers.forEach(element => {
                this.identifiers.forEach(val => {
                    if (val['id'] == element['identifierId']) {
                        this.form.get(`${val['code']}`).enable({ onlySelf: false });
                    }
                });
            });
            // clear form
            this.form.controls.EnrollmentDate.setValue('');
            this.form.controls.EntryPoint.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Negative') {
            this.form.controls.EnrollmentDate.disable({ onlySelf: true });
            this.form.controls.EntryPoint.disable({ onlySelf: true });

            this.serviceAreaIdentifiers.forEach(element => {
                this.identifiers.forEach(val => {
                    if (val['id'] == element['identifierId']) {
                        this.form.get(`${val['code']}`).disable({ onlySelf: true });
                    }
                });
            });
        }
    }

    submitEnrollment() {
        if (!this.form.valid) {
            return;
        } else {
            this.enrollmentCheck();
        }
    }

    public enrollmentCheck(): any {
        if (this.isEdit) {
            this.save();
            return;
        }

        const isReconfirmatoryTestDone = this.yesNoOptions.filter(obj => obj.itemId == this.form.controls.ReConfirmatoryTest.value);

        if (isReconfirmatoryTestDone[0].itemName == 'No') {
            this.snotifyService.error('Enrollment cannot be completed without a reconfirmatory test', 'Enrollment',
                this.notificationService.getConfig());
            return;
        } else {
            const reconfirmatoryTestResult = this.reConfirmatoryTestResults.filter(obj =>
                obj.itemId == this.form.controls.ReConfirmatoryTestResult.value);
            if (reconfirmatoryTestResult[0].itemName == 'Negative') {
                this.snotifyService.error('Enrollment to CCC enrollment is for positive clients only', 'Enrollment',
                    this.notificationService.getConfig());
            } else {
                this.save();
            }
        }
    }

    public save() {
        const { EnrollmentDate, KeyPopulation, populationType, EntryPoint,
            TypeOfTest, ReConfirmatoryTestResult, ReConfirmatoryTestDate } = this.form.value;
        this.personPopulation.KeyPopulation = KeyPopulation;
        this.personPopulation.populationType = populationType;

        const hivReConfirmatoryTests: HivReConfirmatoryTestsCommand = {
            PersonId: this.personId,
            TypeOfTest: TypeOfTest,
            TestResult: ReConfirmatoryTestResult,
            TestResultDate: ReConfirmatoryTestDate,
            CreatedBy: this.userId
        };
        const populationTypes = this.registrationService.addPersonPopulationType(this.personId,
            this.userId, this.personPopulation);
        const addPatient = this.registrationService.addPatient(this.personId, this.userId, EnrollmentDate, this.posId);
        const addReconfirmatoryTest = this.registrationService.addReConfirmatoryTest(hivReConfirmatoryTests);

        const enrollment = new Enrollment();
        this.serviceAreaIdentifiers.forEach(
            element => {
                this.identifiers.forEach(val => {
                    if (val['id'] == element['identifierId']) {
                        enrollment.ServiceIdentifiersList.push({
                            'IdentifierId': element['identifierId'],
                            'IdentifierValue': this.form.get(`${val['code']}`).value
                        });
                    }
                });
            }
        );
        enrollment.DateOfEnrollment = EnrollmentDate;
        enrollment.ServiceAreaId = this.serviceId;
        enrollment.PersonId = this.personId;
        enrollment.CreatedBy = this.userId;
        enrollment.RegistrationDate = EnrollmentDate;
        enrollment.PosId = this.posId;

        forkJoin([addPatient, populationTypes, addReconfirmatoryTest]).subscribe(
            res => {
                this.patientId = res[0]['patientId'];
                enrollment.PatientId = this.patientId;
                const entryPoint: ServiceEntryPointCommand = {
                    Id: 0,
                    PatientId: this.patientId,
                    ServiceAreaId: this.serviceId,
                    EntryPointId: EntryPoint,
                    CreateDate: new Date(),
                    CreatedBy: this.userId
                };
                const serviceEntryPoint = this.registrationService.addServiceEntryPoint(entryPoint).subscribe();

                this.enrollmentService.enrollClient(enrollment).subscribe(
                    (response) => {
                        this.snotifyService.success('Successfully Enrolled ', 'Enrollment',
                            this.notificationService.getConfig());

                        localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                        this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                        this.store.dispatch(new Consent.PatientId(this.patientId));
                        this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                            this.patientId).subscribe();

                        this.searchService.setSession(this.personId, this.patientId).subscribe((sessionres) => {
                            console.log(sessionres);
                            window.location.href = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                                '/IQCare/CCC/Patient/PatientHome.aspx';
                        });
                    },
                    (err) => {
                        this.snotifyService.error('Error completing enrollment ' + err, 'Enrollment',
                            this.notificationService.getConfig());
                    }
                );
            },
            error => {
                this.snotifyService.error('An error occured on CCC enrollment: ' + error, 'Enrollment',
                    this.notificationService.getConfig());
            }
        );
    }
}

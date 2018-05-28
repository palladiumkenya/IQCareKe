import { Component, OnInit, NgZone } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { Person, RegistrationVariables } from '../_models/person';
import { Contact } from '../_models/contacts';
import { PersonPopulation } from '../_models/personPopulation';
import { RegistrationService } from '../_services/registration.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { ClientService } from '../../shared/_services/client.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { NotificationService } from '../../shared/_services/notification.service';
import { Partner } from '../../shared/_models/partner';

@Component({
    selector: 'app-person',
    templateUrl: './person.component.html',
    styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
    person: Person;
    contact: Contact;
    personPopulation: PersonPopulation;
    registrationVariables: RegistrationVariables;
    partnerType: Partner;

    userId: number;
    relationshipPartnerOptions: any[];
    relationshipFamilyOptions: any[];
    optionToShow: any[];

    maritalStatuses: any[];
    keyPops: any[];
    gender: any[];
    priorityPops: any[];

    male: number;
    female: number;

    patientName: string;

    maxDate: any;

    isLinear = true;
    formGroup: FormGroup;

    /** Returns a FormArray with the name 'formArray'. */
    get formArray(): AbstractControl | null { return this.formGroup.get('formArray'); }

    constructor(private registrationService: RegistrationService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private clientService: ClientService,
        private store: Store<AppState>,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder) {

        this.maxDate = new Date();
    }

    ngOnInit() {
        this.person = new Person();
        this.contact = new Contact();
        this.personPopulation = new PersonPopulation();
        this.registrationVariables = new RegistrationVariables();
        this.partnerType = new Partner();

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.relationshipPartnerOptions = [];
        this.relationshipFamilyOptions = [];
        this.optionToShow = [];

        // this.getRegistrationOptions();
        this.route.data.subscribe((res) => {
            const options = res['options']['lookupItems'];

            const partnerOptions = ['Partner', 'Co-Wife', 'Spouse'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'HTSMaritalStatus') {
                    this.maritalStatuses = options[i].value;
                } else if (options[i].key == 'HTSKeyPopulation') {
                    this.keyPops = options[i].value;
                } else if (options[i].key == 'Gender') {
                    this.gender = options[i].value;
                    for (let j = 0; j < options[i].value.length; j++) {
                        if (options[i].value[j].itemName == 'Male') {
                            this.male = options[i].value[j].itemId;
                        } else if (options[i].value[j].itemName == 'Female') {
                            this.female = options[i].value[j].itemId;
                        }
                    }
                } else if (options[i].key == 'Relationship') {
                    const returnOptions = options[i].value;
                    for (let j = 0; j < returnOptions.length; j++) {
                        if (partnerOptions.includes(returnOptions[j].itemName)) {
                            this.relationshipPartnerOptions.push(returnOptions[j]);
                        } else {
                            this.relationshipFamilyOptions.push(returnOptions[j]);
                        }
                    }
                } else if (options[i].key == 'PriorityPopulation') {
                    this.priorityPops = options[i].value;
                }
            }

            this.getPersonDetailsForUpdate();
        });

        this.formGroup = this._formBuilder.group({
            formArray: this._formBuilder.array([
                this._formBuilder.group({
                    FirstName: new FormControl(this.person.FirstName, [Validators.required]),
                    MiddleName: new FormControl(this.person.MiddleName),
                    LastName: new FormControl(this.person.LastName, [Validators.required]),
                    Sex: new FormControl(this.person.Sex, [Validators.required]),
                    DateOfBirth: new FormControl(this.person.DateOfBirth, [Validators.required]),
                    MaritalStatus: new FormControl(this.person.MaritalStatus, [Validators.required]),
                    personAge: new FormControl(this.registrationVariables.personAge, [Validators.required]),
                }),
                this._formBuilder.group({
                    PhoneNumber: new FormControl(this.contact.PhoneNumber),
                    Landmark: new FormControl(this.contact.Landmark)
                }),
                this._formBuilder.group({
                    populationType: new FormControl(this.personPopulation.populationType, [Validators.required]),
                    priorityPopulation: new FormControl(this.personPopulation.priorityPopulation, [Validators.required]),
                    priorityPop: new FormControl(this.personPopulation.priorityPop, [Validators.required]),
                    KeyPopulation: new FormControl(this.personPopulation.KeyPopulation, [Validators.required]),
                    partnerRelationship: new FormControl(this.person.partnerRelationship, [Validators.required])
                }),
            ]),
        });

        this.partnerType = JSON.parse(localStorage.getItem('isPartner'));

        if (this.partnerType != null) {
            this.formGroup.controls.formArray['controls'][2].controls.partnerRelationship.enable({ onlySelf: false });
            this.getClientDetails();
            if (this.partnerType.partner == 1) {
                this.optionToShow = this.relationshipPartnerOptions;
            } else if (this.partnerType.family == 1) {
                this.optionToShow = this.relationshipFamilyOptions;
            }

        } else {
            if (JSON.parse(localStorage.getItem('isHtsEnrolled')) == 1) {

            } else {
                this.formGroup.controls.formArray['controls'][2].controls.partnerRelationship.disable({ onlySelf: true });
                localStorage.removeItem('personId');
                localStorage.removeItem('patientId');
                localStorage.removeItem('partnerId');
                localStorage.removeItem('htsEncounterId');
                localStorage.removeItem('patientMasterVisitId');
                localStorage.removeItem('isPartner');
                localStorage.setItem('serviceAreaId', '2');
                localStorage.removeItem('editEncounterId');
            }
            this.store.dispatch(new Consent.ClearState());
        }
    }

    onSubmitForm() {
        console.log(this.formGroup.valid);
        console.log(this.formGroup);
        if (this.formGroup.valid) {
            if (JSON.parse(localStorage.getItem('isHtsEnrolled')) == 1) {
                this.updatePersonSubmit();
            } else {
                this.onNewClientSubmit();
            }

        } else {
            return;
        }
    }

    updatePersonSubmit() {
        this.person = { ...this.person, ...this.formArray.get([0]).value };
        this.contact = Object.assign(this.person, this.formArray.get([1]).value);
        this.personPopulation = { ...this.personPopulation, ...this.formArray.get([2]).value };
        this.person.partnerRelationship = this.formArray.get([2]).value['partnerRelationship'];
        this.person.createdBy = JSON.parse(localStorage.getItem('appUserId'));
        this.person.Id = JSON.parse(localStorage.getItem('personId'));

        if (this.partnerType != null && (this.partnerType.partner == 1 || this.partnerType.family == 1)) {
            this.person.isPartner = true;
            this.person.patientId = JSON.parse(localStorage.getItem('patientId'));
        } else {
            this.person.isPartner = false;
        }

        this.registrationService.updatePersonDetails(this.person).subscribe((personRes) => {
            console.log(personRes);

            const personCont = this.registrationService.updatePersonContact(personRes['id'],
                null, this.contact.PhoneNumber,
                null, null, this.userId);

            const matStatus = this.registrationService.updatePersonMaritalStatus(personRes['id'],
                this.person.MaritalStatus, this.userId);


            /*const populationTypes = this.registrationService.addPersonPopulationType(personRes.Id,
                this.userId, this.personPopulation);*/

            const personLoc = this.registrationService.updatePersonLocation(personRes['id'], 0,
                0, 0, this.userId, this.contact.Landmark);

            forkJoin([personCont, matStatus, personLoc]).subscribe((forkJoinRes) => {
                console.log('success');
            }, (error) => {
                this.snotifyService.error('Error editing client ' + error, 'Registration', this.notificationService.getConfig());
            }, () => {
                this.snotifyService.success('Successfully registered client', 'Registration', this.notificationService.getConfig());
                this.zone.run(() => { this.router.navigate(['/registration/enrollment'], { relativeTo: this.route }); });
            });
        }, (err) => {
            this.snotifyService.error('Error editing client ' + err, 'Registration', this.notificationService.getConfig());
        });
    }

    onNewClientSubmit() {
        this.person = { ...this.person, ...this.formArray.get([0]).value };
        this.contact = Object.assign(this.person, this.formArray.get([1]).value);
        this.personPopulation = { ...this.personPopulation, ...this.formArray.get([2]).value };
        this.person.partnerRelationship = this.formArray.get([2]).value['partnerRelationship'];
        this.person.createdBy = JSON.parse(localStorage.getItem('appUserId'));

        if (this.partnerType != null && (this.partnerType.partner == 1 || this.partnerType.family == 1)) {
            this.person.isPartner = true;
            this.person.patientId = JSON.parse(localStorage.getItem('patientId'));
        } else {
            this.person.isPartner = false;
        }

        // send the person to IQCare API
        this.registrationService.registerClient(this.person).subscribe(data => {
            const personRelation = {};
            personRelation['PersonId'] = data['personId'];
            personRelation['PatientId'] = JSON.parse(localStorage.getItem('patientId'));
            personRelation['RelationshipTypeId'] = this.person.partnerRelationship;
            personRelation['UserId'] = JSON.parse(localStorage.getItem('appUserId'));

            const patientAdd = this.registrationService.addPersonRelationship(personRelation);

            // this.registrationService.addPatient(data['personId'], this.person.DateOfBirth)

            const personCont = this.registrationService.addPersonContact(data['personId'],
                null, this.contact.PhoneNumber,
                null, null, this.userId);


            const matStatus = this.registrationService.addPersonMaritalStatus(data['personId'],
                this.person.MaritalStatus, this.userId);

            const populationTypes = this.registrationService.addPersonPopulationType(data['personId'],
                this.userId, this.personPopulation);

            const personLoc = this.registrationService.addPersonLocation(data['personId'], 0,
                0, 0, this.userId, this.contact.Landmark);

            // join multiple requests
            forkJoin([patientAdd, personCont, matStatus, personLoc, populationTypes]).subscribe(results => {
                if (this.person.isPartner == false) {
                    // localStorage.setItem('patientId', results[0]['patientId']);
                    localStorage.setItem('personId', data['personId']);
                } else {
                    localStorage.setItem('partnerId', data['personId']);
                }
            }, (err) => {
                this.snotifyService.error('Error registering client ' + err,
                    'Registration', this.notificationService.getConfig());
            }, () => {
                if (this.person.isPartner == true) {
                    this.snotifyService.success('Successfully registered partner',
                        'Registration', this.notificationService.getConfig());

                    localStorage.removeItem('isPartner');
                    if (this.partnerType.family == 1) {
                        this.zone.run(() => { this.router.navigate(['/hts/family'], { relativeTo: this.route }); });
                    } else {
                        this.zone.run(() => { this.router.navigate(['/hts/pns'], { relativeTo: this.route }); });
                    }
                } else {
                    this.snotifyService.success('Successfully registered client', 'Registration', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/registration/enrollment'], { relativeTo: this.route }); });
                }
            });

        }, err => {
            this.snotifyService.error('Error registering client ' + err, 'Registration', this.notificationService.getConfig());
        });
    }

    estimateDob() {
        const personAge = this.formGroup.controls.formArray['controls'][0].controls.personAge.value;
        const currentDate = new Date();
        currentDate.setDate(15);
        currentDate.setMonth(5);
        const estDob = moment(currentDate.toISOString());
        const dob = estDob.add((personAge * -1), 'years');

        this.formArray['controls'][0]['controls']['DateOfBirth'].setValue(moment(dob).toDate());
    }

    onDate(event): void {
        this.getAge(event);
    }

    getAge(dateString) {
        const today = new Date();
        const birthDate = new Date(dateString);
        let age = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }

        this.formArray['controls'][0]['controls']['personAge'].setValue(age);
    }

    getClientDetails() {
        this.clientService.getClientDetails(JSON.parse(localStorage.getItem('patientId')),
            JSON.parse(localStorage.getItem('serviceAreaId'))).subscribe(res => {
                const result = res['patientLookup'][0];
                this.patientName = result.firstName + ' ' + result.midName + ' ' + result.lastName;
            });
    }

    onPopulationTypeChange() {
        const popType = this.formGroup.controls['formArray']['controls'][2].controls.populationType.value;
        if (popType == 1) {
            this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.setValue([]);
        } else if (popType == 2) {
            this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.enable({ onlySelf: false });
        }
    }

    onPriorityChange() {
        const priorityPop = this.formGroup.controls['formArray']['controls'][2].controls.priorityPop.value;
        if (priorityPop == 2) {
            this.formGroup.controls['formArray']['controls'][2].controls.priorityPopulation.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2].controls.priorityPopulation.setValue([]);
        } else if (priorityPop == 1) {
            this.formGroup.controls['formArray']['controls'][2].controls.priorityPopulation.enable({ onlySelf: false });
        }
    }

    onSexChange() {
        const clientSex = this.formGroup.controls['formArray']['controls'][0].controls.Sex.value;
        const optionSelected = this.gender.filter(obj => parseInt(obj.itemId, 10) == parseInt(clientSex, 10));

        this.route.data.subscribe((res) => {
            const options = res['options']['lookupItems'];

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'HTSKeyPopulation') {
                    this.keyPops = options[i].value;
                } else if (options[i].key == 'PriorityPopulation') {
                    this.priorityPops = options[i].value;
                }
            }
        });

        if (optionSelected[0].itemName == 'Female') {
            const options = this.keyPops.filter(function (obj) {
                return obj.itemName !== 'MSM';
            });
            this.keyPops = options;

            const optionsVal = this.priorityPops.filter(function (obj) {
                return obj.itemName !== 'MSW';
            });
            this.priorityPops = optionsVal;

        } else if (optionSelected[0].itemName == 'Male') {
            /*const options = this.keyPops.filter(function( obj ) {
                return obj.itemName !== 'FSW';
            });
            this.keyPops = options;*/

            const optionsVal = this.priorityPops.filter(function (obj) {
                return obj.itemName !== 'Adolescent Girls and Young Women';
            });
            this.priorityPops = optionsVal;
        }

        const optionsHtsKeyPops = this.keyPops.filter(function (obj) {
            return obj.itemName !== 'Not Applicable';
        });
        this.keyPops = optionsHtsKeyPops;
    }

    getPersonDetailsForUpdate() {
        if (JSON.parse(localStorage.getItem('isHtsEnrolled')) == 1) {
            const personId = JSON.parse(localStorage.getItem('personId'));
            this.registrationService.getPersonDetails(personId).subscribe((res) => {
                console.log(this.formGroup);
                console.log(res);

                this.formGroup.controls.formArray['controls'][0]['controls'].FirstName.setValue(res.firstName);
                this.formGroup.controls.formArray['controls'][0]['controls'].MiddleName.setValue(res.midName);
                this.formGroup.controls.formArray['controls'][0]['controls'].LastName.setValue(res.lastName);
                this.formGroup.controls.formArray['controls'][0]['controls'].Sex.setValue(res.sex);
                this.formGroup.controls.formArray['controls'][0]['controls'].DateOfBirth.setValue(res.dateOfBirth);
                this.formGroup.controls.formArray['controls'][0]['controls'].MaritalStatus.setValue(res.maritalStatusId);
                this.formGroup.controls.formArray['controls'][0]['controls'].personAge.setValue(res.ageNumber);


                this.formGroup.controls.formArray['controls'][1]['controls'].PhoneNumber.setValue(res.mobileNumber);
                this.formGroup.controls.formArray['controls'][1]['controls'].Landmark.setValue(res.landMark);

                this.registrationService.getPersonPopulationDetails(personId).subscribe((populationRes) => {
                    if (populationRes.length == 0) {
                        this.formGroup.controls.formArray['controls'][2]['controls'].populationType.setValue(1);
                        this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.disable({ onlySelf: true });
                        this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.setValue([]);
                    } else {
                        const keyPopValues = [];

                        for (let i = 0; i < populationRes.length; i++) {
                            if (populationRes[i].populationType == 'General Population') {
                                this.formGroup.controls.formArray['controls'][2]['controls'].populationType.setValue(1);
                                this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.disable({ onlySelf: true });
                                this.formGroup.controls['formArray']['controls'][2].controls.KeyPopulation.setValue([]);
                            } else if (populationRes[i].populationType == 'Key Population') {
                                this.formGroup.controls.formArray['controls'][2]['controls'].populationType.setValue(2);
                                keyPopValues.push(populationRes[i].populationCategory);
                            }
                        }
                        this.formGroup.controls.formArray['controls'][2]['controls'].KeyPopulation.setValue(keyPopValues);
                    }
                });

                this.registrationService.getPersonPriorityDetails(personId).subscribe((priorityRes) => {
                    if (priorityRes.length == 0) {
                        this.formGroup.controls.formArray['controls'][2]['controls'].priorityPop.setValue(2);
                        this.formGroup.controls['formArray']['controls'][2].controls.priorityPopulation.disable({ onlySelf: true });
                        this.formGroup.controls['formArray']['controls'][2].controls.priorityPopulation.setValue([]);
                    } else {
                        this.formGroup.controls.formArray['controls'][2]['controls'].priorityPop.setValue(1);
                        const priorityPersonValues = [];
                        for (let i = 0; i < priorityRes.length; i++) {
                            priorityPersonValues.push(priorityRes[i].PriorityId);
                        }
                        this.formGroup.controls.formArray['controls'][2]['controls'].priorityPopulation.setValue(priorityPersonValues);
                    }
                });

                this.formGroup.controls.formArray['controls'][2]['controls'].partnerRelationship.disable({ onlySelf: true });
                this.formGroup.controls['formArray']['controls'][2].controls.partnerRelationship.setValue('');
            });
        }
    }
}

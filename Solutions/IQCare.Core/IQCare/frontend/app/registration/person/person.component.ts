import {Component, OnInit, NgZone} from '@angular/core';
import {SnotifyService} from 'ng-snotify';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

import {Person, RegistrationVariables} from '../_models/person';
import {Contact} from '../_models/contacts';
import {PersonPopulation} from '../_models/personPopulation';
import {RegistrationService} from '../_services/registration.service';
import {Router, ActivatedRoute} from '@angular/router';
import * as moment from 'moment';
import {forkJoin} from 'rxjs/observable/forkJoin';
import {ClientService} from '../../shared/_services/client.service';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import {NotificationService} from '../../shared/_services/notification.service';
import {overrideProvider} from '@angular/core/src/view';

declare var $: any;

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
    isPartner: string;
    isFamily: string;
    userId: number;
    relationshipPartnerOptions: any[];
    relationshipFamilyOptions: any[];
    optionToShow: any[];

    maritalStatuses: any[];
    keyPops: any[];
    gender: any[];

    male: number;
    female: number;
    patientName: string;

    maxDate: any;

    isLinear = true;
    formGroup: FormGroup;

    /** Returns a FormArray with the name 'formArray'. */
    get formArray(): AbstractControl | null { return this.formGroup.get('formArray'); }

    constructor( private registrationService: RegistrationService,
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
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.relationshipPartnerOptions = [];
        this.relationshipFamilyOptions = [];
        this.optionToShow = [];

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
                    KeyPopulation: new FormControl(this.personPopulation.KeyPopulation, [Validators.required]),
                    partnerRelationship: new FormControl(this.person.partnerRelationship, [Validators.required])
                }),
            ]),
        });

        this.getRegistrationOptions();

        this.isPartner = localStorage.getItem('isPartner');
        this.isFamily = localStorage.getItem('isFamily');
        if (this.isPartner != null && this.isPartner == 'true') {
            this.formGroup.controls.formArray['controls'][2].controls.partnerRelationship.enable({onlySelf: false});
            this.getClientDetails();
            this.optionToShow = this.relationshipPartnerOptions;
        } else {
            this.formGroup.controls.formArray['controls'][2].controls.partnerRelationship.disable({onlySelf: true});
            localStorage.removeItem('personId');
            localStorage.removeItem('patientId');
            localStorage.removeItem('partnerId');
            localStorage.removeItem('htsEncounterId');
            localStorage.removeItem('patientMasterVisitId');
            localStorage.removeItem('isPartner');
            localStorage.removeItem('isFamily');
            localStorage.setItem('serviceAreaId', '2');

            this.store.dispatch(new Consent.ClearState());
        }

        if ((this.isPartner != null && this.isPartner == 'true') && (this.isFamily != null && this.isFamily == 'true')) {
            this.optionToShow = this.relationshipFamilyOptions;
        }
    }

    getRegistrationOptions() {
        this.registrationService.getRegistrationOptions().subscribe(res => {
            const options = res['lookupItems'];
            const partnerOptions = ['Partner', 'Co-Wife', 'Spouse'];
            // console.log(options);
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'MaritalStatus') {
                    this.maritalStatuses = options[i].value;
                } else if (options[i].key == 'KeyPopulation') {
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
                }
            }
        });
    }

    onSubmitForm() {
        console.log(this.formGroup);
        if (this.formGroup.valid) {
            // this.person = Object.assign(this.person, this.formArray.get([0]).value);
            this.person = {...this.person, ...this.formArray.get([0]).value};
            this.contact = Object.assign(this.person, this.formArray.get([1]).value);
            this.personPopulation.KeyPopulation = this.formArray.get([2]).value['KeyPopulation'];
            this.person.partnerRelationship = this.formArray.get([2]).value['partnerRelationship'];
            this.person.createdBy = JSON.parse(localStorage.getItem('appUserId'));

            if (this.isPartner != null && this.isPartner == 'true') {
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

                const patientAdd = !this.person.isPartner ? this.registrationService.addPatient(data['personId'], this.person.DateOfBirth)
                    :  this.registrationService.addPersonRelationship(personRelation);

                const personCont =  this.registrationService.addPersonContact(data['personId'],
                    null, this.contact.PhoneNumber,
                    null, null, this.userId);


                const matStatus = this.registrationService.addPersonMaritalStatus(data['personId'],
                    this.person.MaritalStatus, this.userId);

                const personLoc = this.registrationService.addPersonLocation(data['personId'], 0,
                    0, 0, this.userId, this.contact.Landmark);

                //
                forkJoin([patientAdd, personCont, matStatus, personLoc]).subscribe(results => {
                    if (this.person.isPartner == false) {
                        localStorage.setItem('patientId', results[0]['patientId']);
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
                        if (this.isFamily == 'true') {
                            this.zone.run(() => {this.router.navigate(['/hts/family'], { relativeTo: this.route }); });
                        }
                        this.zone.run(() => { this.router.navigate(['/hts/pns'], { relativeTo: this.route}); });
                    } else {
                        this.snotifyService.success('Successfully registered client', 'Registration', this.notificationService.getConfig());
                        this.zone.run(() => { this.router.navigate(['/registration/enrollment'], { relativeTo: this.route }); });
                    }
                });

            }, err => {
                this.snotifyService.error('Error registering client ' + err, 'Registration', this.notificationService.getConfig());
            });
        } else {
            return;
        }
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
}

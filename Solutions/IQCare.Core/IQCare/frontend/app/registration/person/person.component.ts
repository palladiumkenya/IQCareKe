import {Component, OnInit, NgZone, AfterViewInit} from '@angular/core';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';

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
    userId: number = 1;

    maritalStatuses: any[];
    keyPops: any[];
    gender: any[];

    male: number;
    female: number;
    patientName: string;

    maxDate: any;

    constructor( private registrationService: RegistrationService,
                 private router: Router,
                 private route: ActivatedRoute,
                 public zone: NgZone,
                 private clientService: ClientService,
                 private store: Store<AppState>,
                 private snotifyService: SnotifyService,
                 private notificationService: NotificationService) {

        this.maxDate = new Date();
    }

    ngOnInit() {
        this.getRegistrationOptions();

        this.person = new Person();
        this.contact = new Contact();
        this.personPopulation = new PersonPopulation();
        this.registrationVariables = new RegistrationVariables();

        this.isPartner = localStorage.getItem('isPartner');
        if (this.isPartner != null && this.isPartner == 'true') {
            this.getClientDetails();
        } else {
            localStorage.removeItem('personId');
            localStorage.removeItem('patientId');
            localStorage.removeItem('partnerId');
            localStorage.removeItem('htsEncounterId');
            localStorage.removeItem('patientMasterVisitId');
            localStorage.removeItem('isPartner');
            localStorage.setItem('serviceAreaId', '2');

            this.store.dispatch(new Consent.ClearState());
        }
        const self = this;

        setTimeout(() => {
            $('#myWizard').on('actionclicked.fu.wizard', function(evt, data) {

                if (data.step === 1) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep1').parsley().destroy();
                        $('#datastep1').parsley({
                            excluded: 'input[type=button], input[type=submit], ' +
                            'input[type=reset], input[type=hidden], [max], [disabled], :hidden'
                        });

                        $('#DateOfBirth').parsley().removeError('error');
                        if (self.person.DateOfBirth == null || moment(self.person.DateOfBirth).toDate() > new Date()) {
                            $('#DateOfBirth').parsley().addError('error', {message: 'Date of Birth should not be blank or in the future'});
                        }

                        if ($('#datastep1').parsley().validate()) {
                            // validated
                        } else {
                            evt.preventDefault();
                            return;
                        }
                    }
                } else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep2').parsley().destroy();
                        $('#datastep2').parsley({
                            excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden'
                        });

                        if ($('#datastep2').parsley().validate()) {
                            // validated
                        } else {
                            console.log('Parseley Validated Error');
                            evt.preventDefault();
                            return;
                        }
                    }
                } else if (data.step === 3) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep3').parsley().destroy();
                        $('#datastep3').parsley({
                            excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden'
                        });

                        if ($('#datastep3').parsley().validate()) {
                            /* submit all forms */
                            self.onSubmitForm();
                        } else {
                            console.log('Parseley Validated Error');
                            evt.preventDefault();
                            return;
                        }
                    }
                }
            })
            .on('changed.fu.wizard', function() {})
            .on('stepclicked.fu.wizard', function() {})
            .on('finished.fu.wizard', function(e) {});
        }, 0 );
    }

    getRegistrationOptions() {
        this.registrationService.getRegistrationOptions().subscribe(res => {
            const options = res['lookupItems'];
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
                }
            }
        });
    }

    onSubmitForm() {
        if (this.isPartner != null && this.isPartner == 'true') {
            this.person.isPartner = true;
            this.person.patientId = JSON.parse(localStorage.getItem('patientId'));
        } else {
            this.person.isPartner = false;
        }

        this.registrationService.registerClient(this.person).subscribe(data => {
            const personRelation = new Object();
            personRelation['PersonId'] = data['personId'];
            personRelation['PatientId'] = JSON.parse(localStorage.getItem('patientId'));
            personRelation['RelationshipTypeId'] = this.person.partnerRelationship;
            personRelation['UserId'] = 1; // JSON.parse(localStorage.getItem('userId'));

            const patientAdd = !this.person.isPartner ? this.registrationService.addPatient(data['personId'],
                this.person.DateOfBirth) :  this.registrationService.addPersonRelationship(personRelation);

            const personCont =  this.registrationService.addPersonContact(data['personId'],
                null, this.contact.PhoneNumber,
                null, null, this.userId);


            const matStatus = this.registrationService.addPersonMaritalStatus(data['personId'],
                this.person.MaritalStatus, this.userId);

            forkJoin([patientAdd, personCont, matStatus]).subscribe(results => {
                if (this.person.isPartner == false) {
                    localStorage.setItem('patientId', results[0]['patientId']);
                    localStorage.setItem('personId', data['personId']);
                } else {
                    localStorage.setItem('partnerId', data['personId']);
                }
            }, () => {
                console.log('error');
            }, () => {
                if (this.person.isPartner == true) {
                    this.snotifyService.success('Successfully registered partner', 'Registration', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/hts/pns'], { relativeTo: this.route}); });
                } else {
                    this.snotifyService.success('Successfully registered client', 'Registration', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/registration/enrollment'], { relativeTo: this.route }); });
                }
            });

        }, err => {
            console.log(err);
            this.snotifyService.error('Error registering client ' + err, 'Registration', this.notificationService.getConfig());
        });
    }

    estimateDob(personAge) {
        // console.log(personAge);
        const currentDate = new Date();
        currentDate.setDate(15);
        currentDate.setMonth(5);
        // console.log(currentDate);
        const estDob = moment(currentDate.toISOString());
        const dob = estDob.add((personAge * -1), 'years');

        this.person.DateOfBirth = moment(dob).format('DD-MM-YYYY');
    }

    onDate(event): void {
        // console.log(event);
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

        this.registrationVariables.personAge = age;
    }

    getClientDetails() {
        this.clientService.getClientDetails(JSON.parse(localStorage.getItem('patientId')),
            JSON.parse(localStorage.getItem('serviceAreaId'))).subscribe(res => {
            const result = res['patientLookup'][0];
            this.patientName = result.firstName + ' ' + result.midName + ' ' + result.lastName;
        });
    }
}

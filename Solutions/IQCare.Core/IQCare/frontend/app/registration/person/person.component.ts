import { Component, OnInit, NgZone  } from '@angular/core';

import {Person, RegistrationVariables} from '../_models/person';
import {Contact} from '../_models/contacts';
import {PersonPopulation} from '../_models/personPopulation';
import {RegistrationService} from '../_services/registration.service';
import {Router, ActivatedRoute} from '@angular/router';
import moment = require('moment');

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

    maritalStatuses: any[];
    keyPops: any[];
    gender: any[];

    male: number;
    female: number;

    constructor( private registrationService: RegistrationService,
                 private router: Router,
                 private route: ActivatedRoute,
                 public zone: NgZone) { }

    ngOnInit() {
        this.getRegistrationOptions();

        this.person = new Person();
        this.contact = new Contact();
        this.personPopulation = new PersonPopulation();
        this.registrationVariables = new RegistrationVariables();

        const self = this;

        setTimeout(() => {
            $('#myWizard').on('actionclicked.fu.wizard', function(evt, data) {

                if (data.step === 1) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep1').parsley().destroy();
                        $('#datastep1').parsley({
                            excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden'
                        });

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
        this.registrationService.registerClient(this.person, this.contact,
            this.personPopulation).subscribe(data => {
            // console.log(data);

            localStorage.setItem('personId', data['personId']);
            localStorage.setItem('patientId', data['patientId']);

            this.zone.run(() => { this.router.navigate(['/registration/enrollment'], { relativeTo: this.route }); });
        }, err => {
            console.log(err);
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
}

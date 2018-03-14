import { Component, OnInit } from '@angular/core';
import {Person} from '../_models/person';
import {Contact} from '../_models/contacts';
import {PersonPopulation} from '../_models/personPopulation';
import {RegistrationService} from '../_services/registration.service';

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

    maritalStatuses: any[];
    keyPops: any[];
    gender: any[];

    male: number;
    female: number;

    constructor( private registrationService: RegistrationService) { }

    ngOnInit() {
        this.getRegistrationOptions();

        this.person = new Person();
        this.contact = new Contact();
        this.personPopulation = new PersonPopulation();

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
            const options = res.lookupItems;

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
        /* console.log(this.person);
        console.log(this.contact);
        console.log(this.personPopulation); */

        this.registrationService.registerClient(this.person, this.contact,
            this.personPopulation).subscribe(data => {
            console.log(data);
        }, err => {
            console.log(err);
        });
    }
}

import { County } from './../../_models/county';
import { SnotifyService } from 'ng-snotify';
import { EmergencyContact } from './../../_models/emergencycontact';
import { ClientAddress } from './../../_models/clientaddress';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControl } from '../../../../../node_modules/@angular/forms';
import { Person } from '../../_models/person';
import { ClientContact } from '../../_models/clientcontact';
import { NextOfKin } from '../../_models/nextofkin';
import { MatDatepickerInputEvent } from '../../../../../node_modules/@angular/material';
import * as moment from 'moment';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { CountyService } from '../../_services/county.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    /**
     * Component variables
     */
    isLinear = false;
    formGroup: FormGroup;
    /** Returns a FormArray with the name 'formArray'. */
    get formArray(): AbstractControl | null { return this.formGroup.get('formArray'); }

    person: Person;
    clientAddress: ClientAddress;
    clientContact: ClientContact;
    emergencyContact: EmergencyContact;
    nextOfKin: NextOfKin;
    maxDate: Date;

    counties: County[];
    subCounties: County[];
    wards: County[];

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        private countyService: CountyService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.person = new Person();
        this.clientAddress = new ClientAddress();
        this.clientContact = new ClientContact();
        this.emergencyContact = new EmergencyContact();
        this.nextOfKin = new NextOfKin();

        this.formGroup = this._formBuilder.group({
            formArray: this._formBuilder.array([
                this._formBuilder.group({
                    FirstName: new FormControl(this.person.firstName, [Validators.required]),
                    MiddleName: new FormControl(this.person.middleName),
                    LastName: new FormControl(this.person.lastName, [Validators.required]),
                    Sex: new FormControl(this.person.sex, [Validators.required]),
                    RegistrationDate: new FormControl(this.person.registrationDate, [Validators.required]),
                    DateOfBirth: new FormControl(this.person.dateOfBirth, [Validators.required]),
                    AgeYears: new FormControl(this.person.ageYears, [Validators.required]),
                    AgeMonths: new FormControl(this.person.ageMonths, [Validators.required]),
                    DobPrecision: new FormControl(this.person.dobPrecision, [Validators.required]),
                    MaritalStatus: new FormControl(this.person.maritalStatus, [Validators.required]),
                    EducationLevel: new FormControl(this.person.educationLevel, [Validators.required]),
                    Occupation: new FormControl(this.person.occupation, [Validators.required]),
                    IdentifierType: new FormControl(this.person.identifierType, [Validators.required]),
                    IdentifierNumber: new FormControl(this.person.identifierNumber, [Validators.required])
                }),
                this._formBuilder.group({
                    County: new FormControl(this.clientAddress.county, [Validators.required]),
                    SubCounty: new FormControl(this.clientAddress.subCounty, [Validators.required]),
                    Ward: new FormControl(this.clientAddress.ward, [Validators.required]),
                    NearestHealthCenter: new FormControl(this.clientAddress.nearestHealthCenter, [Validators.required]),
                    Landmark: new FormControl(this.clientAddress.landmark, [Validators.required])
                }),
                this._formBuilder.group({
                    MobileNumber: new FormControl(this.clientContact.mobileNumber),
                    AlternativeMobileNumber: new FormControl(this.clientContact.alternativeMobileNumber),
                    EmailAddress: new FormControl(this.clientContact.email),
                    EmergencyContactInClinic: new FormControl(this.clientContact.emergencyContactInClinic, [Validators.required]),
                    EmergencyContactFirstName: new FormControl(this.emergencyContact.firstName),
                    EmergencyContactMiddleName: new FormControl(this.emergencyContact.middleName),
                    EmergencyContactLastName: new FormControl(this.emergencyContact.lastName),
                    EmergencyContactSex: new FormControl(this.emergencyContact.sex),
                    EmergencyContactRelationship: new FormControl(this.emergencyContact.emergencyContactRelationship),
                    EmergencyContactMobileNumber: new FormControl(this.emergencyContact.emergencyContactRelationship)
                }),
                this._formBuilder.group({
                    NextOfKinFirstName: new FormControl(this.nextOfKin.firstName),
                    NextOfKinMiddleName: new FormControl(this.nextOfKin.middleName),
                    NextOfKinLastName: new FormControl(this.nextOfKin.lastName),
                    NextOfKinSex: new FormControl(this.nextOfKin.sex),
                    NextOfKinRelationship: new FormControl(this.nextOfKin.kinContactRelationship),
                    NextOfKinMobileNumber: new FormControl(this.nextOfKin.kinContactRelationship),
                    NextOfKinConsent: new FormControl(this.nextOfKin.kinConsentToSMS),
                    NextOfKinConsentDeclineReason: new FormControl(this.nextOfKin.consentDeclineReason)
                })
            ])
        });

        this.route.data.subscribe((res) => {
            const { countiesArray } = res;
            this.counties = countiesArray;
        });
    }

    onDate(event: MatDatepickerInputEvent<Date>) {
        this.getAge(event.value);
    }

    getAge(dob: Date): any {
        console.log(dob);
        const today = new Date();

        let age = today.getFullYear() - dob.getFullYear();
        let ageMonths = today.getMonth() - dob.getMonth();
        if (ageMonths < 0 || (ageMonths === 0 && today.getDate() < dob.getDate())) {
            age--;
        }

        if (ageMonths < 0) {
            ageMonths = 12 - (-ageMonths + 1);
        }

        this.formArray['controls'][0]['controls']['AgeYears'].setValue(age);
        this.formArray['controls'][0]['controls']['AgeMonths'].setValue(ageMonths);
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(1);
    }

    estimateDob() {
        const ageYears = this.formGroup.value.formArray[0]['AgeYears'];
        const ageMonths = this.formGroup.value.formArray[0]['AgeMonths'];
        if (!ageYears) {
            this.snotifyService.error('Please enter (age years)', 'Registration', this.notificationService.getConfig());
        }

        if (ageYears < 0) {
            this.snotifyService.error('Age in years should not be negative', 'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeYears'].setValue('');
            return;
        }

        if (ageMonths < 0) {
            this.snotifyService.error('Age in months should not be negative', 'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeMonths'].setValue('');
            return;
        }

        const today = new Date();
        today.setDate(15);
        today.setMonth(5);

        const estDob = moment(today.toISOString());
        let dob = estDob.add((ageYears * -1), 'years');
        if (ageMonths) {
            dob = estDob.add(ageMonths, 'months');
        }

        this.formArray['controls'][0]['controls']['DateOfBirth'].setValue(moment(dob).toDate());
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(0);
    }

    onCountyChange() {
        const county = this.formGroup.value.formArray[1]['County'];
        this.countyService.getSubCounties(county).subscribe((res) => {
            this.subCounties = res;
            this.wards = [];
        });
    }

    onSubCountyChange() {
        const subCountyId = this.formGroup.value.formArray[1]['SubCounty'];
        this.countyService.getWards(subCountyId).subscribe((res) => {
            this.wards = res;
        });
    }
}

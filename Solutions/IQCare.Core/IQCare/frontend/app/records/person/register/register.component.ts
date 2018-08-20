import { forkJoin } from 'rxjs';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { County } from '../../_models/county';
import { SnotifyService } from 'ng-snotify';
import { EmergencyContact } from '../../_models/emergencycontact';
import { ClientAddress } from '../../_models/clientaddress';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';
import { Person } from '../../_models/person';
import { ClientContact } from '../../_models/clientcontact';
import { NextOfKin } from '../../_models/nextofkin';
import { MatDatepickerInputEvent } from '@angular/material';
import * as moment from 'moment';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { CountyService } from '../../_services/county.service';
import { PersonRegistrationService } from '../../_services/person-registration.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    /**
     * Component variables
     */
    isLinear = true;
    registerEmergencyContact: boolean = false;
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
    gender: LookupItemView[];
    maritalStatus: LookupItemView[];
    educationLevel: LookupItemView[];
    occupation: LookupItemView[];
    relationship: LookupItemView[];

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        private countyService: CountyService,
        private personRegistration: PersonRegistrationService) {
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
                    EducationLevel: new FormControl(this.person.EducationLevel),
                    Occupation: new FormControl(this.person.Occupation),
                    IdentifierType: new FormControl(this.person.identifierType),
                    IdentifierNumber: new FormControl(this.person.identifierNumber)
                }),
                this._formBuilder.group({
                    County: new FormControl(this.clientAddress.County, [Validators.required]),
                    SubCounty: new FormControl(this.clientAddress.SubCounty, [Validators.required]),
                    Ward: new FormControl(this.clientAddress.Ward, [Validators.required]),
                    NearestHealthCenter: new FormControl(this.clientAddress.NearestHealthCenter, [Validators.required]),
                    Landmark: new FormControl(this.clientAddress.Landmark, [Validators.required])
                }),
                this._formBuilder.group({
                    MobileNumber: new FormControl(this.clientContact.MobileNumber),
                    AlternativeMobileNumber: new FormControl(this.clientContact.AlternativeMobileNumber),
                    EmailAddress: new FormControl(this.clientContact.EmailAddress),
                    EmergencyContactInClinic: new FormControl(this.clientContact.EmergencyContactInClinic),
                    EmergencyContactFirstName: new FormControl(this.emergencyContact.EmergencyContactFirstName, [Validators.required]),
                    EmergencyContactMiddleName: new FormControl(this.emergencyContact.EmergencyContactMiddleName),
                    EmergencyContactLastName: new FormControl(this.emergencyContact.EmergencyContactLastName, [Validators.required]),
                    EmergencyContactSex: new FormControl(this.emergencyContact.EmergencyContactSex, [Validators.required]),
                    EmergencyContactRelationship: new FormControl(this.emergencyContact.EmergencyContactRelationship,
                        [Validators.required]),
                    EmergencyContactMobileNumber: new FormControl(this.emergencyContact.EmergencyContactMobileNumber)
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
            // console.log(res);
            const { countiesArray, genderArray, maritalStatusArray, educationLevelArray, occupationArray, relationshipArray } = res;
            this.counties = countiesArray;
            this.gender = genderArray;
            this.maritalStatus = maritalStatusArray;
            this.educationLevel = educationLevelArray;
            this.occupation = occupationArray;
            this.relationship = relationshipArray;
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

    onRegisteredInClinic() {
        const isEmergencyContactRegisteredInClinic = this.formArray.value[2]['EmergencyContactInClinic'];
        if (!isEmergencyContactRegisteredInClinic || isEmergencyContactRegisteredInClinic == 1) {
            this.registerEmergencyContact = false;
            // console.log(this.formGroup.controls['formArray']['controls'][2]['EmergencyContactFirstName'].disable({ onlySelf: true }));
            // this.formTesting.controls.acceptedPartnerListing.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactFirstName.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactLastName.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactSex.disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactRelationship.disable({ onlySelf: true });
        } else if (isEmergencyContactRegisteredInClinic == 2) {
            this.registerEmergencyContact = true;
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactFirstName.enable({ onlySelf: false });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactLastName.enable({ onlySelf: false });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactSex.enable({ onlySelf: false });
            this.formGroup.controls['formArray']['controls'][2]['controls'].EmergencyContactRelationship.enable({ onlySelf: false });
        }
    }

    onSubmitForm() {
        console.log(this.formArray.value);
        console.log(this.formGroup.valid);
        if (this.formGroup.valid) {
            this.person = { ...this.formArray.value[0] };
            this.clientAddress = { ...this.formArray.value[1] };
            this.clientContact = { ...this.formArray.value[2] };
            this.emergencyContact = { ...this.formArray.value[2] };



            this.person.personId = 0;
            this.person.createdBy = 1;
            console.log(this.person);
            console.log(this.clientAddress);
            console.log(this.clientContact);
            console.log(this.emergencyContact);
            return;

            this.personRegistration.registerPerson(this.person).subscribe(
                (response) => {
                    console.log(response);
                    const { personId } = response;
                    console.log(personId);

                    // Add Contact
                    const personContact = this.personRegistration.addPersonContact(personId, this.person.createdBy, this.clientContact);
                    // Add Address
                    const personAddress = this.personRegistration.addPersonAddress(personId, this.person.createdBy, this.clientAddress);
                    // Add Marital Status
                    const personMaritalStatus = this.personRegistration.addPersonMaritalStatus(personId,
                        this.person.createdBy, this.person.maritalStatus);
                    // Add Education Level
                    const personEducationLevel = this.personRegistration.addPersonEducationLevel(personId,
                        this.person.createdBy, this.person.EducationLevel);
                    // Add Occupation
                    const personOccupation = this.personRegistration.addPersonOccupation(personId,
                        this.person.createdBy, this.person.Occupation);
                    // Add Emergency Contact
                    const personEmergencyContact = this.personRegistration.registerPersonEmergencyContact(personId, this.emergencyContact);

                    forkJoin([personContact, personAddress, personMaritalStatus,
                        personEducationLevel, personOccupation, personEmergencyContact]).subscribe(
                            (forkRes) => {
                                console.log(forkRes);
                            },
                            (forkError) => {
                                console.log(forkError);
                            },
                            () => {
                                console.log(`complete`);
                            }
                        );
                },
                (error) => {
                    console.log(error);
                }
            );
        } else {
            return;
        }
    }
}

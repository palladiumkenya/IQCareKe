import { CheckDuplicatesComponent } from './../check-duplicates/check-duplicates.component';
import { forkJoin } from 'rxjs';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { County } from '../../_models/county';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { EmergencyContact } from '../../_models/emergencycontact';
import { ClientAddress } from '../../_models/clientaddress';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Component, OnInit, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';
import { Person } from '../../_models/person';
import { ClientContact } from '../../_models/clientcontact';
import { NextOfKin } from '../../_models/nextofkin';
import { MatDatepickerInputEvent, MatDialogConfig, MatDialog, MatTableDataSource } from '@angular/material';
import * as moment from 'moment';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CountyService } from '../../_services/county.service';
import { PersonRegistrationService } from '../../_services/person-registration.service';
import { PersoncontactsComponent } from '../personcontacts/personcontacts.component';
import { RecordsService } from '../../_services/records.service';
import { SearchService } from '../../_services/search.service';
import { Search } from '../../_models/search';

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
    consentSms: LookupItemView[];
    contactCategory: LookupItemView[];
    personIdentifiers: any[];
    yesnoOptions: LookupItemView[];

    clientSearch: Search;

    dataSource: any[];
    newContacts: any[];
    id: number;

    public phonePattern = /^(?:\+254|0|254)(\d{9})$/;

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        private countyService: CountyService,
        private personRegistration: PersonRegistrationService,
        private dialog: MatDialog,
        private recordsService: RecordsService,
        public zone: NgZone,
        private router: Router,
        private searchService: SearchService) {
        this.maxDate = new Date();
        this.clientSearch = new Search();
    }

    ngOnInit() {
        this.person = new Person();
        this.clientAddress = new ClientAddress();
        this.clientContact = new ClientContact();
        this.nextOfKin = new NextOfKin();
        this.dataSource = [];
        this.newContacts = [];
        this.person.PosId = JSON.parse(localStorage.getItem('appPosID'));

        this.formGroup = this._formBuilder.group({
            formArray: this._formBuilder.array([
                this._formBuilder.group({
                    FirstName: new FormControl(this.person.firstName, [Validators.required]),
                    MiddleName: new FormControl(this.person.middleName),
                    LastName: new FormControl(this.person.lastName, [Validators.required]),
                    Sex: new FormControl(this.person.sex, [Validators.required]),
                    registrationDate: new FormControl(this.person.registrationDate, [Validators.required]),
                    DateOfBirth: new FormControl(this.person.dateOfBirth, [Validators.required]),
                    AgeYears: new FormControl(this.person.ageYears, [Validators.required]),
                    AgeMonths: new FormControl(this.person.ageMonths, [Validators.required]),
                    DobPrecision: new FormControl(this.person.dobPrecision, [Validators.required]),
                    MaritalStatus: new FormControl(this.person.maritalStatus),
                    EducationLevel: new FormControl(this.person.EducationLevel),
                    Occupation: new FormControl(this.person.Occupation),
                    IdentifierType: new FormControl(this.person.IdentifierType),
                    IdentifierNumber: new FormControl(this.person.IdentifierNumber, [Validators.required])
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
                }),
                this._formBuilder.group({
                })
            ])
        });

        this.formGroup.controls['formArray']['controls'][0]['controls']['IdentifierNumber'].disable({ onlySelf: true });

        this.route.data.subscribe((res) => {
            // console.log(res);
            const { countiesArray, genderArray, maritalStatusArray, educationLevelArray,
                occupationArray, relationshipArray, consentSmsArray, contactCategoryArray,
                personIdentifiersArray, yesnoArray } = res;
            this.counties = countiesArray;
            this.gender = genderArray;
            this.maritalStatus = maritalStatusArray;
            this.educationLevel = educationLevelArray;
            this.occupation = occupationArray;
            this.relationship = relationshipArray;
            this.consentSms = consentSmsArray;
            this.contactCategory = contactCategoryArray;
            this.personIdentifiers = personIdentifiersArray['identifers'];
            this.yesnoOptions = yesnoArray['lookupItems'];
            // console.log(personIdentifiersArray['identifers']);
        });

        this.route.params.subscribe(params => {
            this.id = params['id'];
        });

        if (this.id) {
            this.getPersonDetails(this.id);
        }
    }

    getPersonDetails(id: number): any {
        this.recordsService.getPersonDetails(id).subscribe(
            (result) => {
                console.log(result);
                const {
                    alternativeNumber, county, countyId, dateOfBirth, dobPrecision, educationLevel, educationLevelId,
                    emailAddress, firstName, gender, lastName, maritalStatus, maritalStatusId, middleName,
                    mobileNumber, nearestHealthCentre, occupation, occupationId, registrationDate, sex,
                    subCounty, subCountyId, village, ward, wardId } = result[0];


                let exact = null;
                if (dobPrecision && dobPrecision == true) {
                    exact = 1;
                } else if (dobPrecision == false) {
                    exact = 0;
                }

                // first tab wizard
                this.formGroup.controls['formArray']['controls'][0]['controls'].FirstName.setValue(firstName);
                this.formGroup.controls['formArray']['controls'][0]['controls'].MiddleName.setValue(middleName);
                this.formGroup.controls['formArray']['controls'][0]['controls'].LastName.setValue(lastName);
                this.formGroup.controls['formArray']['controls'][0]['controls'].Sex.setValue(sex);
                this.formGroup.controls['formArray']['controls'][0]['controls'].registrationDate.setValue(registrationDate);
                this.formGroup.controls['formArray']['controls'][0]['controls'].DateOfBirth.setValue(dateOfBirth);
                this.formGroup.controls['formArray']['controls'][0]['controls'].DobPrecision.setValue(exact);
                this.formGroup.controls['formArray']['controls'][0]['controls'].MaritalStatus.setValue(maritalStatusId);
                this.formGroup.controls['formArray']['controls'][0]['controls'].EducationLevel.setValue(educationLevelId);
                this.formGroup.controls['formArray']['controls'][0]['controls'].Occupation.setValue(occupationId);
                if (dateOfBirth) {
                    this.getAge(moment(dateOfBirth));
                }

                // second tab wizard
                this.formGroup.controls['formArray']['controls'][1]['controls'].County.setValue(countyId);
                this.countyService.getSubCounties(countyId).subscribe((res) => {
                    this.subCounties = res;
                    this.formGroup.controls['formArray']['controls'][1]['controls'].SubCounty.setValue(subCountyId);
                });
                this.countyService.getWards(subCountyId).subscribe((res) => {
                    this.wards = res;
                    this.formGroup.controls['formArray']['controls'][1]['controls'].Ward.setValue(wardId);
                });
                this.formGroup.controls['formArray']['controls'][1]['controls'].NearestHealthCenter.setValue(nearestHealthCentre);
                this.formGroup.controls['formArray']['controls'][1]['controls'].Landmark.setValue(village);

                // third tab wizard
                this.formGroup.controls['formArray']['controls'][2]['controls'].MobileNumber.setValue(mobileNumber);
                this.formGroup.controls['formArray']['controls'][2]['controls'].AlternativeMobileNumber.setValue(alternativeNumber);
                this.formGroup.controls['formArray']['controls'][2]['controls'].EmailAddress.setValue(emailAddress);
            }
        );

        this.personRegistration.getPersonKinContacts(id).subscribe(
            (res) => {
                for (let i = 0; i < res.length; i++) {
                    // console.log(res[i]);
                    this.dataSource.push({
                        'firstName': res[i].firstName,
                        'middleName': res[i].middleName,
                        'lastName': res[i].lastName,
                        'gender': res[i].genderList[0],
                        'contactcategory': res[i].contactCategoryList[0],
                        'relationship': res[i].contactRelationshipList[0],
                        'phoneno': res[i].mobileNo,
                        'consent': null,
                        'disabled': 'none'
                    });
                }
            }
        );

        this.personRegistration.getPersonIdentifiers(id).subscribe(
            (res) => {
                console.log(res);
                if (res.length > 0) {
                    this.formGroup.controls['formArray']['controls'][0]['controls'].IdentifierType.setValue(res[0]['identifierId']);
                    this.formGroup.controls['formArray']['controls'][0]['controls'].IdentifierNumber.setValue(res[0]['identifierValue']);
                    this.formGroup.controls['formArray']['controls'][0]['controls']['IdentifierNumber'].enable({ onlySelf: false });
                }
            }
        );
    }

    onDate(event: MatDatepickerInputEvent<moment.Moment>) {
        this.getAge(event.value, true);
    }

    getAge(dob: moment.Moment, setDobPrecision: boolean = false): any {
        const today = new Date();

        console.log(moment(dob).toDate());
        console.log(dob.toISOString());

        let age = today.getFullYear() - dob.toDate().getFullYear();
        let ageMonths = today.getMonth() - dob.toDate().getMonth();
        if (ageMonths < 0 || (ageMonths === 0 && today.getDate() < dob.toDate().getDate())) {
            age--;
        }

        if (ageMonths < 0) {
            ageMonths = 12 - (-ageMonths + 1);
        }

        this.formArray['controls'][0]['controls']['AgeYears'].setValue(age);
        this.formArray['controls'][0]['controls']['AgeMonths'].setValue(ageMonths);
        if (setDobPrecision) {
            this.formArray['controls'][0]['controls']['DobPrecision'].setValue(1);
        }
    }

    estimateDob() {
        const ageYears = this.formGroup.value.formArray[0]['AgeYears'];
        const ageMonths = this.formGroup.value.formArray[0]['AgeMonths'];

        if (!ageYears) {
            this.snotifyService.error('Please enter (age years)', 'Registration', this.notificationService.getConfig());
            return;
        }

        if (ageYears < 0) {
            this.snotifyService.error('Age in years should not be negative', 'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeYears'].setValue('');
            return;
        }

        if (ageYears > 120) {
            this.snotifyService.error('Age in years should not be more than 120 years old',
                'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeYears'].setValue('');
            return;
        }

        if (ageMonths < 0) {
            this.snotifyService.error('Age in months should not be negative', 'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeMonths'].setValue('');
            return;
        }

        if (ageMonths > 11) {
            this.snotifyService.error('Age in months should not be more than 11', 'Registration', this.notificationService.getConfig());
            this.formArray['controls'][0]['controls']['AgeMonths'].setValue('');
            return;
        }

        if (!ageMonths) {
            this.formArray['controls'][0]['controls']['AgeMonths'].setValue(0);
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

    onSubmitForm(tabIndex: number) {
        if (this.formGroup.valid) {
            this.person = { ...this.formArray.value[0] };
            this.clientAddress = { ...this.formArray.value[1] };
            this.clientContact = { ...this.formArray.value[2] };
            this.person.dateOfBirth = moment(this.formArray.value[0]['DateOfBirth']).toDate();
            this.person.registrationDate = moment(this.person.registrationDate).toDate();

            this.person.personId = 0;
            this.person.createdBy = JSON.parse(localStorage.getItem('appUserId'));
            this.person.PosId = JSON.parse(localStorage.getItem('appPosID'));

            if (this.id) {
                // set person id for update
                this.person.id = this.id;
            }

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
                        this.person.createdBy, this.person['MaritalStatus']);
                    // Add Education Level
                    const personEducationLevel = this.personRegistration.addPersonEducationLevel(personId,
                        this.person.createdBy, this.person.EducationLevel);
                    // Add Occupation
                    const personOccupation = this.personRegistration.addPersonOccupation(personId,
                        this.person.createdBy, this.person.Occupation);
                    // Add Emergency Contact
                    const personEmergencyContact = this.personRegistration.registerPersonEmergencyContact(personId,
                        this.person.createdBy, this.newContacts);
                    // Add Person Identifiers
                    const personIdentifiersAdd = this.personRegistration.addPersonIdentifiers(personId, this.person.createdBy,
                        this.person.IdentifierType, this.person.IdentifierNumber);

                    forkJoin([personContact, personAddress, personMaritalStatus,
                        personEducationLevel, personOccupation, personEmergencyContact,
                        personIdentifiersAdd]).subscribe(
                            (forkRes) => {
                                console.log(forkRes);
                            },
                            (forkError) => {
                                this.snotifyService.error('Error creating person ' + forkError, 'Person Registration',
                                    this.notificationService.getConfig());
                            },
                            () => {
                                this.snotifyService.success('Successfully Registered Person', 'Person Registration',
                                    this.notificationService.getConfig());

                                if (tabIndex == 1) {
                                    this.zone.run(() => {
                                        this.router.navigate(['/dashboard/personhome/' + personId],
                                            { relativeTo: this.route });
                                    });
                                } else if (tabIndex == 2) {
                                    this.person = new Person();
                                    this.clientAddress = new ClientAddress();
                                    this.clientContact = new ClientContact();
                                    this.nextOfKin = new NextOfKin();

                                    window.location.reload();
                                }
                            }
                        );
                },
                (error) => {
                    this.snotifyService.error('Error creating person ' + error, 'Person Registration',
                        this.notificationService.getConfig());
                }
            );
        } else {
            return;
        }
    }

    closeForm() {
        this.router.navigate(['/dashboard'], { relativeTo: this.route });
    }

    addRow() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '90%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
            gender: this.gender,
            relationship: this.relationship,
            consentSms: this.consentSms,
            contactCategory: this.contactCategory,
            yesno: this.yesnoOptions
        };

        const dialogRef = this.dialog.open(PersoncontactsComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                console.log(data);

                this.dataSource.push(
                    {
                        'firstName': data.firstName,
                        'middleName': data.middleName,
                        'lastName': data.lastName,
                        'gender': data.sex,
                        'contactcategory': data.kinContactType,
                        'relationship': data.kinContactRelationship,
                        'phoneno': data.kinMobileNumber,
                        'consent': data.kinConsentToSMS,
                        'disabled': 'all'
                    }
                );

                this.newContacts.push({
                    'firstName': data.firstName,
                    'middleName': data.middleName,
                    'lastName': data.lastName,
                    'gender': data.sex,
                    'contactcategory': data.kinContactType,
                    'relationship': data.kinContactRelationship,
                    'phoneno': data.kinMobileNumber,
                    'consent': data.kinConsentToSMS,
                    'consentDecline': data.consentDeclineReason,
                    'personRegistered': data.registeredPersonId,
                    'posid': this.person.PosId
                });

                console.log(this.newContacts);
            }
        );
    }

    deleteContact(data: any, index: number, event: any) {
        const result = this.snotifyService.confirm('Are you sure you want to delete?', 'Contacts', {
            closeOnClick: true,
            position: SnotifyPosition.centerCenter,
            buttons: [
                {
                    text: 'Yes', action: () => {
                        const contactsFiltered = this.newContacts.filter((obj) => {
                            return obj.firstName !== data.firstName
                                && obj.lastName !== data.lastName
                                && obj.gender.itemId !== data.gender.itemId
                                && obj.relationship.itemId !== data.relationship.itemId
                                && obj.contactcategory !== data.contactcategory.itemId;
                        });

                        this.newContacts = contactsFiltered;
                        this.dataSource.splice(index, 1);

                        console.log(this.newContacts);
                    }, bold: false
                },
                { text: 'No', action: () => console.log('Clicked: No') }
            ]
        });
    }

    onIdentifierTypeChange() {
        if (this.formArray.value[0]['IdentifierType']) {
            this.formGroup.controls['formArray']['controls'][0]['controls']['IdentifierNumber'].enable({ onlySelf: false });
        } else {
            this.formGroup.controls['formArray']['controls'][0]['controls']['IdentifierNumber'].disable({ onlySelf: true });
            this.formGroup.controls['formArray']['controls'][0]['controls']['IdentifierNumber'].setValue('');
        }
    }

    public checkAgeForValidation() {
        const ageInYears = this.formArray['controls'][0]['controls']['AgeYears'].value;
        if (ageInYears < 10) {
            this.formArray['controls'][0]['controls']['MaritalStatus'].disable({ onlySelf: true });
            this.formArray['controls'][0]['controls']['EducationLevel'].disable({ onlySelf: true });
            this.formArray['controls'][0]['controls']['Occupation'].disable({ onlySelf: true });
        } else {
            this.formArray['controls'][0]['controls']['MaritalStatus'].enable();
            this.formArray['controls'][0]['controls']['EducationLevel'].enable();
            this.formArray['controls'][0]['controls']['Occupation'].enable();
        }
    }

    public checkDuplicates() {
        const firstName = this.formGroup.controls['formArray']['controls'][0]['controls']['FirstName'].value;
        const middleName = this.formGroup.controls['formArray']['controls'][0]['controls']['MiddleName'].value;
        const lastName = this.formGroup.controls['formArray']['controls'][0]['controls']['LastName'].value;
        const gender = this.formGroup.controls['formArray']['controls'][0]['controls']['Sex'].value;
        const dateOfBirth = this.formGroup.controls['formArray']['controls'][0]['controls']['DateOfBirth'].value;

        this.clientSearch.firstName = firstName == null ? '' : firstName;
        this.clientSearch.middleName = middleName == null ? '' : middleName;
        this.clientSearch.lastName = lastName == null ? '' : lastName;
        this.clientSearch.identifierValue = '';
        this.clientSearch.mobileNumber = '';
        this.clientSearch.dateOfBirth = dateOfBirth;
        this.clientSearch.sex = gender;

        if (firstName && lastName && gender && dateOfBirth) {
            this.searchService.searchClient(this.clientSearch).subscribe(
                (result) => {
                    console.log(result['personSearch']);
                    if (result && result['personSearch'] && result['personSearch'].length > 0) {
                        const dialogConfig = new MatDialogConfig();

                        dialogConfig.disableClose = true;
                        dialogConfig.autoFocus = true;
                        dialogConfig.height = '90%';
                        dialogConfig.width = '80%';

                        dialogConfig.data = {
                            'persons': result['personSearch']
                        };

                        const dialogRef = this.dialog.open(CheckDuplicatesComponent, dialogConfig);

                        dialogRef.afterClosed().subscribe(
                            data => {
                                if (!data) {
                                    return;
                                }

                                console.log(data);
                                this.zone.run(() => {
                                    this.router.navigate(['/dashboard/personhome/' + data[0]['id']], { relativeTo: this.route });
                                });
                            }
                        );
                    }
                }
            );
        }
    }
}

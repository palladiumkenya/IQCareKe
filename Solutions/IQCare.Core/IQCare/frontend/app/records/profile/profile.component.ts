import { Component, OnInit, NgZone, ViewChild } from '@angular/core';

import { SnotifyService } from 'ng-snotify';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import {
    Person, Person2, EmergencyArray, RegistrationVariables, errorMessages,
    PersonLocation, County, SubCounty, PersonIdentification, Ward, PersonAddress,
    EmergencyEdit, EmergencyListEdit, NextofKinEmergencyEdit, NextofKinEmergencyListEdit
} from '../models/person';
import { RegistrationService } from '../services/RecordsRegistrationService';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { forkJoin, Observable } from 'rxjs';
import { ClientService } from '../../shared/_services/client.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { NotificationService } from '../../shared/_services/notification.service';
import { emergencyValidator } from '../models/emergencyvalidator';
import { Search, SearchList, SearchRegList, SearchContact } from '../models/search';
import { SearchService } from '../services/recordssearch';
import { DataSource } from '@angular/cdk/collections';
import { MatStepper, MatTableDataSource, MatPaginator, MatCheckboxChange, MatRadioChange } from '@angular/material';

import { from as observableFrom } from 'rxjs';
import { of as observableOf } from 'rxjs';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
    nextkintype: number;
    emergencytype: number;
    contactrel: number;
    errors = errorMessages;
    Objectitems: any[] = [];
    nextofkinmessage: Observable<any>;
    personemergencymessage: Observable<any>;
    toggoleShowHide: string = 'hidden';
    PersonIdent: PersonIdentification;
    personemergency: Observable<any>;
    person: Person;
    person2: Person2;
    contacttype: any[];

    registeredclinic: number;
    nextofkinemergencylist: NextofKinEmergencyListEdit;
    nextofkinemergency: NextofKinEmergencyEdit;
    emergencylist: EmergencyListEdit;
    emergency: EmergencyEdit;
    userId: number;
    dob: string;
    registrationVariables: RegistrationVariables;
    personupdate: Person;
    personupdatelocation: PersonLocation;
    personupdateAddress: PersonAddress;
    personlocation: PersonLocation;
    personAddress: PersonAddress;
    relationshipEmergencyOptions: any[];
    relationshipNextofKinOptions: any[];
    IdentifyerTypes: Array<any> = [];
    consent: string;
    emergencycontactarray: EmergencyArray[] = [];
    nextofkincontactarray: EmergencyArray[] = [];
    nextofkincontacttype: any;
    emergencycontacttype: any;
    occupations: any[];
    maritalstatuses: any[];
    educationallevel: any[];
    consentoptions: any[];
    counties: County[];
    countyid: string;
    subcountyid: string;
    consentoptype: string;
    subcounties: SubCounty[];
    selectedLink: boolean;
    wardlist: Ward[];
    gender: any[];
    male: number;
    female: number;
    maxDate: any;
    ConsentType: number;
    formGroup: FormGroup;
    myForm: FormGroup;
    selectedcounty: string;
    get formArray(): AbstractControl | null { return this.formGroup.get('formArray'); }
    SelectedCounty: County = new County(0, 'Select');
    public values: any[];
    items: EmergencyEdit[];
    itemslist: EmergencyListEdit[];
    itemnextofkin: NextofKinEmergencyEdit[];
    itemlistnextofkin: NextofKinEmergencyListEdit[];
    relationshipoptions: any[];

    dataSourceEmergency = new MatTableDataSource();
    newItem: any = {};
    newnextofkin: any = {};
    isLinear = true;
    editorenabled = false;
    index: number;
    nextofkinindex: number;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    editornextofkin = false;
    condition = true;
    relation: number;
    registeredhidden = true;
    public personRelationId: number
    conditionvisible = true;
    displayerror = true;
    displaycontacttype = true;
    searchcontactlist: SearchContact;

    OptionMap: any[];
    displayemgtype: true;
    displayedColumnlist = ['personIdentificationNumber', 'firstName', 'middleName', 'lastName', 'mobileNumber', 'Gender', 'RelationshipOptions', 'contacttype', 'Add'];
    constructor(private registrationService: RegistrationService,
        private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private clientService: ClientService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder) {



        this.maxDate = new Date();


    }
    ngAfterViewInit() {


        this.dataSourceEmergency.paginator = this.paginator;
    }
    ngOnInit() {

        this.PersonIdent = new PersonIdentification;
        this.person2 = new Person2();
        this.person = new Person();
        this.relationshipoptions = [];
        this.contacttype = [];
        this.userId = JSON.parse(localStorage.getItem("appUserId"));
        this.relationshipEmergencyOptions = [];
        this.relationshipNextofKinOptions = [];
        this.registrationVariables = new RegistrationVariables();
        this.personlocation = new PersonLocation();
        this.occupations = [];
        this.OptionMap = [];
        this.searchcontactlist = new SearchContact();
        this.emergency = new EmergencyEdit();
        this.emergencylist = new EmergencyListEdit();
        this.educationallevel = [];
        this.consentoptions = [];
        this.maritalstatuses = [];
        this.personAddress = new PersonAddress();
        this.newItem = [];
        this.items = [];
        this.itemslist = [];
        this.nextofkinemergency = new NextofKinEmergencyEdit();
        this.nextofkinemergencylist = new NextofKinEmergencyListEdit();;
        this.itemnextofkin = [];
        this.itemlistnextofkin = [];
        //this.registrationService.getPersonDetails(13611).subscribe(
        //    (data: any) => {
        //        console.log(data);
        //        console.log(data["personContact"]["mobileNumber"])
        //    });
        this.registrationService.getCounties().subscribe(
            (data: any) => {

                this.counties = data['counties'];


            });


        this.registrationService.getContactTypeOptions().subscribe(
            (data: any) => {
                console.log(data);
                const value = data['lookupItems']['0']['value']
                console.log(value);
                this.contacttype = value;
                for (let i = 0; i < value.length; i++) {
                    if (value[i].itemName == 'Emergency') {
                        this.emergencycontacttype = value[i].itemId;

                        console.log(this.emergencycontacttype);
                    }
                    if (value[i].itemName == 'NextofKin') {
                        this.nextofkincontacttype = value[i].itemId;

                        console.log(this.nextofkincontacttype);
                    }
                }
            }
        );

        this.ConsentType = this.route.snapshot.data['ConsentType']["consentType"];
        console.log(this.ConsentType);
        this.IdentifyerTypes = this.route.snapshot.data["IdentifyerType"]['identifers'];


        console.log(this.IdentifyerTypes);
        this.route.data.subscribe((res) => {
            const rel = res["rel"]["lookupItems"];
            this.relationshipEmergencyOptions = rel;
            this.relationshipNextofKinOptions = rel;
            this.relationshipoptions = rel;
            console.log(rel);
            const EducConsent = res["Educ"]["lookupItems"];
            this.educationallevel = EducConsent;
            console.log(EducConsent);
            const maritalstatus = res["maritalstatusresolve"]["lookupItems"];
            this.maritalstatuses = maritalstatus;
            const genderrel = res["gen"]["lookupItems"];
            this.gender = genderrel;
            const occup = res["occ"]["lookupItems"];
            this.occupations = occup;
            const consentopt = res["Consent"]["lookupItems"];
            this.consentoptions = consentopt;
            this.Objectitems = res["persondetailsresolve"];

            console.log(this.Objectitems);




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
                    Educationallevel: new FormControl(this.person.Educationallevel, [Validators.required]),
                    Occupation: new FormControl(this.person.Occcupation, [Validators.required]),
                    IdentifyerType: new FormControl(this.person.IdentifyerType, [Validators.required]),
                    IdentifyerNumber: new FormControl(this.person.IdentifyerNumber, [Validators.required]),
                    RegistrationDate: new FormControl(this.person.RegistrationDate, [Validators.required]),
                    personAge: new FormControl(this.registrationVariables.personAge, [Validators.required]),
                    personMonth: new FormControl(this.registrationVariables.personMonth, [Validators.required]),
                    DobPrecision: new FormControl(this.person.DobPrecision, [Validators.required])

                }),
                this._formBuilder.group({
                    Mobilenumber: new FormControl(this.personAddress.MobilePhonenumber, [Validators.required]),
                    AlternativeNumber: new FormControl(this.personAddress.Alternativenumber, [Validators.required]),
                    emailaddress: new FormControl(this.personAddress.EmailAddress, [Validators.required]),
                    County: new FormControl(this.personlocation.countyId, [Validators.required]),
                    SubCounty: new FormControl(this.personlocation.subcountyId, [Validators.required]),
                    Ward: new FormControl(this.personlocation.WardId, [Validators.required]),
                    NearestHealthCenter: new FormControl(this.personlocation.NearestHealthCenter, [Validators.required]),
                    LandMark: new FormControl(this.personlocation.LandMark, [Validators.required])
                }),
                this._formBuilder.group({
                    identifierValue: new FormControl(this.searchcontactlist.identifierValue),
                    firstName: new FormControl(this.searchcontactlist.firstName),
                    lastName: new FormControl(this.searchcontactlist.lastName),
                    middleName: new FormControl(this.searchcontactlist.midName),
                    enrollmentNumber: new FormControl(this.searchcontactlist.EnrollmentNumber),
                    emgFirstName: new FormControl(this.emergency.emgFirstName),
                    emgMiddleName: new FormControl(this.emergency.emgMiddleName),
                    emgLastName: new FormControl(this.emergency.emgLastName),
                    emgGender: new FormControl(this.emergency.emgGender),
                    emgRelationShip: new FormControl(this.emergency.emgRelationShip),
                    emgPrimaryMobileNo: new FormControl(this.emergency.emgPrimaryMobileNo),
                    emgConsentToCall: new FormControl(this.emergency.emgConsentToCall),
                    emgLimitedConsent: new FormControl(this.emergency.emgLimitedConsent),
                    emgRegisteredClinic: new FormControl(this.emergency.emgRegisteredClinic),
                    items: this.items

                }),
                this._formBuilder.group({
                    nokFirstName: new FormControl(this.nextofkinemergency.nokFirstName),
                    nokMiddleName: new FormControl(this.nextofkinemergency.nokMiddleName),
                    nokLastName: new FormControl(this.nextofkinemergency.nokLastName),
                    nokGender: new FormControl(this.nextofkinemergency.nokGender),
                    nokRelationShip: new FormControl(this.nextofkinemergency.nokRelationShip),
                    nokPrimaryMobileNo: new FormControl(this.nextofkinemergency.nokPrimaryMobileNo),
                    nokConsentToCall: new FormControl(this.nextofkinemergency.nokConsentToCall),
                    nokLimitedConsent: new FormControl(this.nextofkinemergency.nokLimitedConsent),
                    nokRegisteredClinic: new FormControl(this.nextofkinemergency.nokRegisteredClinic),
                    itemnextofkin: this.itemnextofkin

                })



            ]),



        });

        this.personupdate = new Person();
        if (!(this.Objectitems['personDetail'] === null)) {

            this.personupdate.FirstName = this.Objectitems["personDetail"]["firstName"];
            console.log(this.personupdate.FirstName);
            this.personupdate.LastName = this.Objectitems["personDetail"]["lastName"];


            this.personupdate.Sex = this.Objectitems["personDetail"]["sex"];


            this.personupdate.MiddleName = this.Objectitems["personDetail"]["midName"];


            this.personupdate.DateOfBirth = this.Objectitems["personDetail"]["dateOfBirth"];

        }
        if (!(this.Objectitems['personMaritalStatus'] === null)) {

            this.personupdate.MaritalStatus = this.Objectitems["personMaritalStatus"]["maritalStatusId"];

        }
        if (!(this.Objectitems['personEducation'] === null)) {

            this.personupdate.Educationallevel = this.Objectitems["personEducation"]["educationLevel"];
            console.log(this.personupdate.Educationallevel);

        }
        if (!(this.Objectitems['personOccupation'] === null)) {

            this.personupdate.Occcupation = this.Objectitems["personOccupation"]["occupation"];

        }
        if (!(this.Objectitems['personIdentifier'] === null)) {

            this.personupdate.IdentifyerType = this.Objectitems["personIdentifier"]["identifierId"];


            this.personupdate.IdentifyerNumber = this.Objectitems["personIdentifier"]["identifierValue"];

        }
        if (!(this.Objectitems['patient'] === null)) {

            this.personupdate.RegistrationDate = this.Objectitems["patient"]["registrationDate"];

        }
        if (!(this.personupdate.FirstName === null || this.personupdate.FirstName === undefined || this.personupdate.FirstName.toString() === '')) {
            console.log(this.personupdate.FirstName);
            this.formArray['controls'][0]['controls']['FirstName'].setValue(this.personupdate.FirstName);
        }

        if (!(this.personupdate.LastName === null || this.personupdate.LastName === undefined || this.personupdate.LastName.toString() === '')) {
            this.formArray['controls'][0]['controls']['LastName'].setValue(this.personupdate.LastName);
        }

        if (!(this.personupdate.Sex === null || this.personupdate.Sex === undefined || this.personupdate.Sex.toString() === '')) {
            this.formArray['controls'][0]['controls']['Sex'].setValue(this.personupdate.Sex);
        }

        if (!(this.personupdate.MiddleName === null || this.personupdate.MiddleName === undefined || this.personupdate.MiddleName.toString() === '')) {

            this.formArray['controls'][0]['controls']['MiddleName'].setValue(this.personupdate.MiddleName);
        }

        if (!(this.personupdate.DateOfBirth === null || this.personupdate.DateOfBirth === undefined || this.personupdate.DateOfBirth.toString() === '')) {

            this.dob = this.personupdate.DateOfBirth;
            this.formArray['controls'][0]['controls']['DateOfBirth'].setValue(new Date(this.dob));
        }

        if (!(this.personupdate.MaritalStatus === null || this.personupdate.MaritalStatus === undefined || this.personupdate.MaritalStatus.toString() === '')) {

            this.formArray['controls'][0]['controls']['MaritalStatus'].setValue(this.personupdate.MaritalStatus);
        }

        if (!(this.personupdate.Educationallevel === null || this.personupdate.Educationallevel === undefined || this.personupdate.Educationallevel.toString() === '')) {
            this.formArray['controls'][0]['controls']['Educationallevel'].setValue(this.personupdate.Educationallevel);
        }

        if (!(this.personupdate.Occcupation === null || this.personupdate.Occcupation === undefined || this.personupdate.Occcupation.toString() === '')) {
            this.formArray['controls'][0]['controls']['Occupation'].setValue(this.personupdate.Occcupation);
        }
        if (!(this.personupdate.IdentifyerType === null || this.personupdate.IdentifyerType === undefined || this.personupdate.IdentifyerType.toString() === '')) {
            this.formArray['controls'][0]['controls']['IdentifyerType'].setValue(this.personupdate.IdentifyerType);
        }

        if (!(this.personupdate.IdentifyerNumber === null || this.personupdate.IdentifyerNumber === undefined || this.personupdate.IdentifyerNumber.toString() === '')) {
            this.formArray['controls'][0]['controls']['IdentifyerNumber'].setValue(this.personupdate.IdentifyerNumber);
        }
        if (!(this.personupdate.RegistrationDate === null || this.personupdate.RegistrationDate === undefined || this.personupdate.RegistrationDate.toString() === '')) {
            this.formArray['controls'][0]['controls']['RegistrationDate'].setValue(this.Objectitems["patient"]["registrationDate"]);
        }
        if (!(this.Objectitems["personDetail"]["dobPrecision"] === null)) {
            if (this.Objectitems["personDetail"]["dobPrecision"] = true) {

                this.formArray['controls'][0]['controls']['DobPrecision'].setValue(1);
            }
            else if (this.Objectitems["personDetail"]["dobPrecision"] = false) {
                console.log('testfalse');
                //this.formGroup.controls.formArray['controls'][0].controls.DobPrecision.checked = true;
                //this.formArray['controls'][0]['controls']['DobPrecision'].setValue(0);
                this.formArray[0]['controls'][0]['controls']['DobPrecision'].setValue(0);
            }
        }
        this.getPersonalAge(this.dob);


        this.personupdatelocation = new PersonLocation();
        if (!(this.Objectitems["personLocation"] === null)) {

            this.personupdatelocation.countyId = this.Objectitems["personLocation"]["county"];
            this.personupdatelocation.subcountyId = this.Objectitems["personLocation"]["subCounty"];
            this.personupdatelocation.WardId = this.Objectitems["personLocation"]["ward"];
            this.personupdatelocation.NearestHealthCenter = this.Objectitems["personLocation"]["nearestHealthCentre"];
            this.personupdatelocation.LandMark = this.Objectitems["personLocation"]["landMark"];
        }
        if (!(this.personupdatelocation.countyId === null || this.personupdatelocation.countyId === undefined || this.personupdatelocation.countyId.toString() === '')) {
            this.formArray['controls'][1]['controls']['County'].setValue(this.personupdatelocation.countyId);
            //this.countyid = this.personupdatelocation.countyId.toString();
            this.formGroup.controls.formArray['controls'][1].controls.County.value = this.personupdatelocation.countyId;

            this.OnSubCountySelect(this.personupdatelocation.countyId);

        }
        if (!(this.personupdatelocation.subcountyId === null || this.personupdatelocation.subcountyId === undefined || this.personupdatelocation.subcountyId.toString() === '')) {
            this.OnWardCountListSelect(this.personupdatelocation.subcountyId);
            this.formArray['controls'][1]['controls']['SubCounty'].setValue(this.personupdatelocation.subcountyId);
        }
        if (!(this.personupdatelocation.WardId === null || this.personupdatelocation.WardId === undefined || this.personupdatelocation.WardId.toString() === "")) {
            //this.OnWardCountListSelect(this.personupdatelocation.countyId);
            this.formArray['controls'][1]['controls']['Ward'].setValue(this.personupdatelocation.WardId);
        }
        if (!(this.personupdatelocation.NearestHealthCenter === null || this.personupdatelocation.NearestHealthCenter === undefined || this.personupdatelocation.NearestHealthCenter.toString() === "")) {
            this.formArray['controls'][1]['controls']['NearestHealthCenter'].setValue(this.personupdatelocation.NearestHealthCenter);
        }
        if (!(this.personupdatelocation.LandMark === null || this.personupdatelocation.LandMark === undefined || this.personupdatelocation.LandMark === '')) {
            this.formArray['controls'][1]['controls']['LandMark'].setValue(this.personupdatelocation.LandMark);
        }


        this.personupdateAddress = new PersonAddress();
        if (!(this.Objectitems["personContact"] === null)) {
            this.personupdateAddress.MobilePhonenumber = this.Objectitems["personContact"]['mobileNumber'];
            this.personupdateAddress.Alternativenumber = this.Objectitems['personContact']['alternativeNumber'];
            this.personupdateAddress.EmailAddress = this.Objectitems['personContact']['emailAddress'];
        }
        if (!(this.personupdateAddress.MobilePhonenumber === null || this.personupdateAddress.MobilePhonenumber === undefined || this.personupdateAddress.MobilePhonenumber === '')) {
            this.formArray['controls'][1]['controls']['Mobilenumber'].setValue(this.personupdateAddress.MobilePhonenumber);
        }
        if (!(this.personupdateAddress.Alternativenumber === null || this.personupdateAddress.Alternativenumber === undefined || this.personupdateAddress.Alternativenumber === '')) {
            this.formArray['controls'][1]['controls']['AlternativeNumber'].setValue(this.personupdateAddress.Alternativenumber);
        }
        if (!(this.personupdateAddress.EmailAddress === null || this.personupdateAddress.EmailAddress === undefined || this.personupdateAddress.EmailAddress === '')) {
            this.formArray['controls'][1]['controls']['emailaddress'].setValue(this.personupdateAddress.EmailAddress);
        }


        if (!(this.Objectitems['personEmergencyView'] === null)) {
            const personEmergencyView = this.Objectitems['personEmergencyView'];
            console.log(personEmergencyView);

            for (let i = 0; i < personEmergencyView.length; i++) {
                var value = personEmergencyView[i]['emergencyContactPersonId'];
                var personid = personEmergencyView[i]['personId'];

                this.emergency.emgpersonId = personEmergencyView[i]['personId'];
                this.emergencylist.emgpersonId = this.emergency.emgpersonId;
                console.log('emgpersonid' + this.emergencylist.emgpersonId);
                // this.emergencylist.emgpersonId = this.emergency.emgpersonId;
                this.emergency.emgEmergencyContactPersonId = personEmergencyView[i]['emergencyContactPersonId'];
                this.emergencylist.emgEmergencyContactPersonId = this.emergency.emgEmergencyContactPersonId;

                if (personEmergencyView[i]['registeredToClinic'] == true) {
                    this.emergency.emgRegisteredClinic = 1;
                    this.emergencylist.emgRegisteredClinic = 'Yes';
                }
                else if (personEmergencyView[i]['registeredToClinic'] == false) {
                    this.emergency.emgRegisteredClinic = 0;
                    this.emergencylist.emgRegisteredClinic = 'No';
                }


                this.emergency.emgEmergencyContactType =
                    this.emergency.emgPrimaryMobileNo = personEmergencyView[i]['mobileContact'];
                this.emergencylist.emgPrimaryMobileNo = this.emergency.emgPrimaryMobileNo;
                this.emergency.emgFirstName = personEmergencyView[i]['emergencyFirstName'];
                this.emergencylist.emgFirstName = this.emergency.emgFirstName;
                this.emergency.emgLastName = personEmergencyView[i]['emergencyLastName'];
                this.emergencylist.emgLastName = this.emergency.emgLastName;
                this.emergency.emgMiddleName = personEmergencyView[i]['emergencyMidName'];
                this.emergencylist.emgMiddleName = this.emergency.emgMiddleName;
                this.emergencylist.emgGender = personEmergencyView[i]['gender'];
                if (!(this.emergencylist.emgGender === null || this.emergencylist.emgGender === undefined || this.emergencylist.emgGender.toString() === '0')) {
                    var gender = this.gender.find(s => s.itemName == this.emergencylist.emgGender);
                    this.emergency.emgGender = gender['itemId'];
                }
                this.emergency.emgCreatedBy = personEmergencyView[i]['createdBy'];
                this.emergencylist.emgCreatedBy = this.emergency.emgCreatedBy;
                this.emergency.emgDeleteFlag = personEmergencyView[i]['deleteFlag'];
                this.emergencylist.emgDeleteFlag = this.emergency.emgDeleteFlag;

                this.emergency.emgConsentToCall = personEmergencyView[i]['consentValue'];
                if (!(this.emergency.emgConsentToCall === null || this.emergency.emgConsentToCall === undefined)) {
                    var consent = this.consentoptions.find(s => s.itemId == this.emergency.emgConsentToCall);


                    this.emergencylist.emgConsentToCall = consent['itemName'];
                }
                this.emergency.emgLimitedConsent = personEmergencyView[i]['consentReason'];
                this.emergencylist.emgLimitedConsent = this.emergency.emgLimitedConsent
                this.emergency.emgRelationShip = personEmergencyView[i]['relationshipTypeId'];
                if (!(this.emergency.emgRelationShip === null || this.emergency.emgRelationShip === undefined)) {
                    var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.emergency.emgRelationShip);
                    this.emergencylist.emgRelationShip = rel['itemName'];
                }
                if (!(personEmergencyView[i]['emergencyItemId'] === null || personEmergencyView[i]['emergencyItemId'] === undefined)) {
                    this.emergency.emgEmergencyContactType = personEmergencyView[i]['emergencyItemId']
                    this.emergencylist.emgEmergencyContactType = personEmergencyView[i]['emergencyItemName']
                    this.emergencytype = this.emergency.emgEmergencyContactType;
                }
                if (!(personEmergencyView[i]['nextofkinItemId'] === null || personEmergencyView[i]['nextofkinItemId'] === undefined)) {
                    this.emergency.emgNextofKinContactType = personEmergencyView[i]['nextofkinItemId']
                    this.emergencylist.emgNextofKinContactType = personEmergencyView[i]['nextofkinItemName']
                    this.nextkintype = this.emergency.emgNextofKinContactType;
                }
                //      if (personid === personEmergencyView[i]["personId"] && value === personEmergencyView[i]["emergencyContactPersonId"]) {



                // }
                this.items.push(this.emergency);
                this.itemslist.push(this.emergencylist);
                console.log(this.itemslist);
                console.log(this.emergency);
            }

            //        for (let i = 0; i < personEmergencyView.length; i++) {
            //            if (personEmergencyView[i]["itemName"] === "NextofKin") {


            //                console.log(personEmergencyView[i]["contactType"]);

            //                this.nextofkinemergency.nokpersonId = personEmergencyView[i]["personId"];
            //                this.nextofkinemergencylist.nokpersonId = this.nextofkinemergency.nokpersonId;

            //                //this.emergencylist.emgpersonId = this.emergency.emgpersonId;
            //                this.nextofkinemergency.nokEmergencyContactPersonId = personEmergencyView[i]["emergencyContactPersonId"];
            //                this.nextofkinemergencylist.nokEmergencyContactPersonId = this.nextofkinemergency.nokEmergencyContactPersonId

            //                if (personEmergencyView[i]["registeredToClinic"] == true) {
            //                    this.nextofkinemergency.nokRegisteredClinic = 1;
            //                    this.nextofkinemergencylist.nokRegisteredClinic = 'Yes';
            //                }
            //                else if (personEmergencyView[i]["registeredToClinic"] == false) {
            //                    this.nextofkinemergency.nokRegisteredClinic = 0;
            //                    this.nextofkinemergencylist.nokRegisteredClinic = 'No';
            //                }



            //                this.nextofkinemergency.nokPrimaryMobileNo = personEmergencyView[i]["mobileContact"];
            //                this.nextofkinemergencylist.nokPrimaryMobileNo = this.nextofkinemergency.nokPrimaryMobileNo;
            //                this.nextofkinemergency.nokFirstName = personEmergencyView[i]["emergencyFirstName"];
            //                this.nextofkinemergencylist.nokFirstName = this.nextofkinemergency.nokFirstName;
            //                this.nextofkinemergency.nokLastName = personEmergencyView[i]["emergencyLastName"];
            //                this.nextofkinemergencylist.nokLastName = this.nextofkinemergency.nokLastName;
            //                this.nextofkinemergency.nokMiddleName = personEmergencyView[i]["emergencyMidName"];
            //                this.nextofkinemergencylist.nokMiddleName = this.nextofkinemergency.nokMiddleName;
            //                this.nextofkinemergency.nokGender = personEmergencyView[i]["gender"];
            //                var gender = this.gender.find(s => s.itemId == this.nextofkinemergency.nokGender);
            //                this.nextofkinemergencylist.nokGender = gender["itemName"];

            //                this.nextofkinemergency.nokCreatedBy = personEmergencyView[i]["createdBy"];
            //                this.nextofkinemergencylist.nokCreatedBy = this.nextofkinemergency.nokCreatedBy;
            //                this.nextofkinemergency.nokDeleteFlag = personEmergencyView[i]["deleteFlag"];
            //                this.nextofkinemergencylist.nokDeleteFlag = this.nextofkinemergency.nokDeleteFlag;
            //                this.nextofkinemergency.nokConsentToCall = personEmergencyView[i]["consentValue"];
            //                var consent = this.consentoptions.find(s => s.itemId == this.nextofkinemergency.nokConsentToCall);


            //                this.nextofkinemergencylist.nokConsentToCall = consent["itemName"];

            //                this.nextofkinemergency.nokLimitedConsent = personEmergencyView[i]["consentReason"];
            //                this.nextofkinemergencylist.nokLimitedConsent = this.nextofkinemergency.nokLimitedConsent
            //                this.nextofkinemergency.nokRelationShip = personEmergencyView[i]["relationshipTypeId"];
            //                var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.nextofkinemergency.nokRelationShip);
            //                this.nextofkinemergencylist.nokRelationShip = rel["itemName"];
            //                this.itemnextofkin.push(this.nextofkinemergency);
            //                this.itemlistnextofkin.push(this.nextofkinemergencylist);
            //                console.log(this.itemslist);
            //                console.log(this.emergency);
            //                //this.emergency.emgFirstName=
            //                //console.log(personEmergencyView[i]);
            //                //this.items.push(personEmergencyView[i]);

            //            }
            //        }
            //    }
            //    console.log(this.items);
            // }
        }

        this.OptionMap.push(this.nextofkincontacttype);
        this.OptionMap.push(this.emergencycontacttype);


    }
    OnWardCountListSelect(value: any) {
        this.subcountyid = value;

        if (this.subcountyid == null)
            this.subcounties = null;
        else {
            this.registrationService.getWardList(this.subcountyid).subscribe((data: any) => {


                this.wardlist = data['wards'];

            });
        }
    }
    OnKeyUp() {
        this.LoadContactList();

    }
    LoadContactList() {
        if (this.searchcontactlist == undefined) {
            return observableFrom([]);
            // return Observable.from([]);
        } else {
            this.searchService.searchPersonContact(this.searchcontactlist).subscribe(data => {


                this.dataSourceEmergency.data = data['personSearch'];
                console.log(this.dataSourceEmergency.data);


            });

        }
    }

    selectchangeRel(event: any) {
        this.relation = event;
        this.displayerror = true;


    }
    public onChange(event: MatCheckboxChange) {
        if (event.checked) {
            this.displaycontacttype = true;
            this.displayemgtype = true;
            this.contactrel = parseInt(event.source.value);

            if (this.contactrel.toString() === this.emergencycontacttype.toString()) {
                this.emergencytype = this.contactrel;
            }
            if (this.contactrel.toString() === this.nextofkincontacttype.toString()) {
                this.nextkintype = this.contactrel;
            }

        }


    }
    selectedregistered(event: MatRadioChange) {
        this.registeredclinic = event.value;
        if (this.registeredclinic.toString() === '1') {
            this.registeredhidden = false;
        }
        else {
            this.registeredhidden = true;
        }
    }
    selectchange(value: any) {

        this.consentoptype = value;
        var res = this.consentoptions.find(s => s.itemId == this.consentoptype);

        if (res)
            this.consent = res['itemName']

        if (this.consent == 'Limitedconcent') {
            this.toggoleShowHide = 'visible';
        }
        else {
            this.toggoleShowHide = 'hidden';
        }
    }
    OnSubCountySelect(value: any) {
        this.countyid = value;
        if (this.countyid == null)

            this.subcounties = null;
        else {
            this.registrationService.getSubCounty(this.countyid).subscribe(
                (data: any) => {

                    this.subcounties = data['subCounties'];


                });




        }

    }


    getPersonalAge(datestring) {
        const today = new Date();
        const birthDate = new Date(datestring);
        var dobMonth = birthDate.getMonth();
        var dobDay = birthDate.getDay();
        var dobYear = birthDate.getFullYear();

        var nowDay = today.getDate();
        var nowMonth = today.getMonth() + 1;
        var nowYear = today.getFullYear();

        var ageyear = nowYear - dobYear;
        var ageMonth = nowMonth - dobMonth;
        var ageDay = nowDay - dobDay;

        if (ageMonth <= 0) {
            ageyear--;
            ageMonth = (12 + ageMonth);
        }
        if (nowDay < dobDay) {
            ageMonth--;
            ageDay = 30 + ageDay;
        }

        this.formArray['controls'][0]['controls']['personAge'].setValue(ageyear);
        this.formArray['controls'][0]['controls']['personMonth'].setValue(ageMonth);

    }



    addNextofKinRow() {
        const nokFirstName = this.formGroup.controls.formArray['controls'][3].controls.nokFirstName.value;
        const nokFirstName2 = this.formGroup.controls.formArray['controls'][3].controls.nokFirstName
        const nokMiddleName = this.formGroup.controls.formArray['controls'][3].controls.nokMiddleName.value;
        const nokMiddleName2 = this.formGroup.controls.formArray['controls'][3].controls.nokLastName

        const nokLastName = this.formGroup.controls.formArray['controls'][3].controls.nokLastName.value;
        const nokLastName2 = this.formGroup.controls.formArray['controls'][3].controls.nokLastName
        const nokGender = this.formGroup.controls.formArray['controls'][3].controls.nokGender.value;
        const nokGender2 = this.formGroup.controls.formArray['controls'][3].controls.nokGender
        const nokRelationShip = this.formGroup.controls.formArray['controls'][3].controls.nokRelationShip.value;
        const nokRelationShip2 = this.formGroup.controls.formArray['controls'][3].controls.nokRelationShip
        const nokPrimaryMobileNo = this.formGroup.controls.formArray['controls'][3].controls.nokPrimaryMobileNo.value;
        const nokPrimaryMobileNo2 = this.formGroup.controls.formArray['controls'][3].controls.nokPrimaryMobileNo
        const nokConsentToCall = this.formGroup.controls.formArray['controls'][3].controls.nokConsentToCall.value;
        const nokConsentToCall2 = this.formGroup.controls.formArray['controls'][3].controls.nokConsentToCall
        const nokRegisteredClinic = this.formGroup.controls.formArray['controls'][3].controls.nokRegisteredClinic.value;
        const nokRegisteredClinic2 = this.formGroup.controls.formArray['controls'][3].controls.nokRegisteredClinic
        if (!(nokFirstName === undefined || nokFirstName === '' || !nokFirstName === null)
            || !(nokMiddleName === undefined || nokMiddleName === '' || nokMiddleName === null)
            || !(nokLastName === undefined || nokLastName === '' || nokLastName !== null)
            || !(nokGender === undefined || nokGender === '' || nokGender === null)
            || !(nokRelationShip === undefined || nokRelationShip === '' || nokRelationShip !== null)
            || !(nokPrimaryMobileNo === undefined || nokPrimaryMobileNo === '' || nokPrimaryMobileNo !== null)
            || !(nokConsentToCall === undefined || nokConsentToCall === '' || nokConsentToCall === null)
            || !(nokRegisteredClinic === undefined || nokRegisteredClinic === '' || nokRegisteredClinic === null)) {
            {

                this.nextofkinemergencylist = new NextofKinEmergencyListEdit();
                if (this.newnextofkin.nokRegisteredClinic == '1') {
                    this.nextofkinemergencylist.nokRegisteredClinic = 'Yes';

                }
                else {
                    this.nextofkinemergencylist.nokRegisteredClinic = 'No';
                }

                this.nextofkinemergencylist.nokFirstName = this.newnextofkin.nokFirstName;
                this.nextofkinemergencylist.nokLastName = this.newnextofkin.nokLastName;
                this.nextofkinemergencylist.nokMiddleName = this.newnextofkin.nokMiddleName;
                var consent = this.consentoptions.find(s => s.itemId == this.newnextofkin.nokConsentToCall);
                this.nextofkinemergencylist.nokConsentToCall = consent['itemName'];
                var gender = this.gender.find(s => s.itemId == this.newnextofkin.nokGender);
                this.nextofkinemergencylist.nokGender = gender['itemName'];



                this.nextofkinemergencylist.nokLimitedConsent = this.newnextofkin.nokLimitedConsent;
                var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newnextofkin.nokRelationShip);
                this.nextofkinemergencylist.nokRelationShip = rel['itemName'];
                this.nextofkinemergencylist.nokPrimaryMobileNo = this.newnextofkin.emgPrimaryMobileNo
                this.newnextofkin.deleteflag = false;
                this.nextofkinemergencylist.nokDeleteFlag = false;
                this.itemlistnextofkin.push(this.nextofkinemergencylist);


                this.itemnextofkin.push(this.newnextofkin);

                this.formGroup.controls.formArray['controls'][3].controls.itemnextofkin.setValue(this.itemnextofkin);


                this.newnextofkin = {};
            }
        }
    }
    deletenextofkin(i) {
        this.itemnextofkin[i].nokDeleteFlag = true;
        this.conditionvisible = false;
    }
    undonextofkindelete(i) {
        this.itemnextofkin[i].nokDeleteFlag = false;
        this.conditionvisible = true;
    }


    delete(i) {
        this.items[i].emgDeleteFlag = true;
        this.condition = false;
    }
    undodelete(i) {
        this.items[i].emgDeleteFlag = false;
        this.condition = true;
    }
    editnextofkinlist(i) {
        this.newnextofkin.nokFirstName = this.itemnextofkin[i].nokFirstName;

        this.newnextofkin.nokMiddleName = this.itemnextofkin[i].nokMiddleName;

        this.newnextofkin.nokLastName = this.itemnextofkin[i].nokLastName;

        this.newnextofkin.nokGender = this.itemnextofkin[i].nokGender;

        this.newnextofkin.nokRelationShip = this.itemnextofkin[i].nokRelationShip;
        this.newnextofkin.nokPrimaryMobileNo = this.itemnextofkin[i].nokPrimaryMobileNo;

        this.newnextofkin.nokConsentToCall = this.itemnextofkin[i].nokConsentToCall;
        this.newnextofkin.nokLimitedConsent = this.itemnextofkin[i].nokLimitedConsent;

        this.newnextofkin.nokRegisteredClinic = this.itemnextofkin[i].nokRegisteredClinic;

        this.nextofkinindex = i;

        this.editornextofkin = true;
    }
    editemergencylist(i) {


        this.registeredhidden = true;
        this.newItem.emgFirstName = this.items[i].emgFirstName;

        this.newItem.emgMiddleName = this.items[i].emgMiddleName;

        this.newItem.emgLastName = this.items[i].emgLastName;

        this.newItem.emgGender = this.items[i].emgGender;

        this.newItem.emgRelationShip = this.items[i].emgRelationShip;
        this.newItem.emgPrimaryMobileNo = this.items[i].emgPrimaryMobileNo;

        this.newItem.emgConsentToCall = this.items[i].emgConsentToCall;
        this.newItem.emgLimitedConsent = this.items[i].emgLimitedConsent;

        this.newItem.emgRegisteredClinic = this.items[i].emgRegisteredClinic;

        this.emergencycontacttype = this.items[i].emgEmergencyContactType;
        this.nextkintype = this.items[i].emgNextofKinContactType;

        this.index = i;

        this.editorenabled = true;
    }
    savenextofkinlist() {
        let i = 0;
        i = this.nextofkinindex;
        this.itemnextofkin[i].nokFirstName = this.newnextofkin.nokFirstName;
        this.itemlistnextofkin[i].nokFirstName = this.itemnextofkin[i].nokFirstName
        this.itemnextofkin[i].nokMiddleName = this.newnextofkin.nokMiddleName;
        this.itemlistnextofkin[i].nokMiddleName = this.itemnextofkin[i].nokMiddleName
        this.itemnextofkin[i].nokLastName = this.newnextofkin.nokLastName;
        this.itemlistnextofkin[i].nokLastName = this.newnextofkin.emgLastName;
        this.itemnextofkin[i].nokGender = this.itemnextofkin[i].nokGender;
        this.itemlistnextofkin[i].nokConsentToCall = this.newnextofkin.nokConsentToCall;
        var consent = this.consentoptions.find(s => s.itemId == this.newnextofkin.nokConsentToCall);


        this.itemlistnextofkin[i].nokConsentToCall = consent['itemName'];
        var gender = this.gender.find(s => s.itemId == this.newnextofkin.nokGender);
        this.itemlistnextofkin[i].nokGender = gender['itemName'];

        var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newnextofkin.nokRelationShip);
        this.itemlistnextofkin[i].nokRelationShip = rel['itemName'];
        this.itemnextofkin[i].nokRelationShip = this.newnextofkin.nokRelationShip;
        this.itemnextofkin[i].nokPrimaryMobileNo = this.newnextofkin.nokPrimaryMobileNo;
        this.itemlistnextofkin[i].nokPrimaryMobileNo = this.newnextofkin.nokPrimaryMobileNo;

        this.itemnextofkin[i].nokRegisteredClinic = this.newnextofkin.nokRegisteredClinic;

        if (this.itemnextofkin[i].nokRegisteredClinic.toString() == '1') {
            this.itemlistnextofkin[i].nokRegisteredClinic = 'Yes';
        }
        else {
            this.itemlistnextofkin[i].nokRegisteredClinic = 'No';
        }
        this.editornextofkin = false;
        this.newnextofkin = {};
    }
    saveemergencylist() {
        let i = 0;
        i = this.index;
        console.log(this.items[i].emgFirstName);
        console.log(this.newItem.emgFirstName);
        this.items[i].emgFirstName = this.newItem.emgFirstName;
        this.itemslist[i].emgFirstName = this.items[i].emgFirstName
        this.items[i].emgMiddleName = this.newItem.emgMiddleName;
        this.itemslist[i].emgMiddleName = this.newItem.emgMiddleName
        this.items[i].emgLastName = this.newItem.emgLastName;
        this.itemslist[i].emgLastName = this.newItem.emgLastName;
        this.items[i].emgGender = this.newItem.emgGender;

        this.items[i].emgEmergencyContactType = this.emergencycontacttype;
        this.items[i].emgNextofKinContactType = this.nextofkincontacttype
        this.items[i].emgConsentToCall = this.newItem.emgConsentToCall;

        var nextofkincontacttype = this.contacttype.find(s => s.itemId == this.nextofkincontacttype);
        this.itemslist[i].emgNextofKinContactType = nextofkincontacttype['itemName'];
        var emergencycontacttype = this.contacttype.find(s => s.itemId == this.emergencycontacttype);
        this.itemslist[i].emgEmergencyContactType = emergencycontacttype['itemName'];
        var consent = this.consentoptions.find(s => s.itemId == this.newItem.emgConsentToCall);


        this.itemslist[i].emgConsentToCall = consent['itemName'];
        var gender = this.gender.find(s => s.itemId == this.newItem.emgGender);
        this.itemslist[i].emgGender = gender['itemName'];

        var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newItem.emgRelationShip);
        this.itemslist[i].emgRelationShip = rel['itemName'];
        this.items[i].emgRelationShip = this.newItem.emgRelationShip;
        this.items[i].emgPrimaryMobileNo = this.newItem.emgPrimaryMobileNo;
        this.itemslist[i].emgPrimaryMobileNo = this.newItem.emgPrimaryMobileNo;

        this.items[i].emgRegisteredClinic = this.newItem.emgRegisteredClinic;

        if (!(this.emergencytype.toString() === '' || this.emergencytype.toString() === 'null' || this.emergencytype.toString() === 'undefined')) {
            var emerg = this.contacttype.find(s => s.itemId == this.emergencytype);
            this.emergencylist.emgEmergencyContactType = emerg['itemName'];
        }
        if (!(this.nextkintype.toString() === '' || this.nextkintype.toString() === 'null' || this.nextkintype.toString() === 'undefined')) {
            var nextkin = this.contacttype.find(s => s.itemId == this.nextkintype);
            this.emergencylist.emgNextofKinContactType = nextkin['itemName'];
        }
        if (this.newItem.emgRegisteredClinic == '1') {
            this.itemslist[i].emgRegisteredClinic = 'Yes';
        }
        else {
            this.itemslist[i].emgRegisteredClinic = 'No';
        }
        this.editorenabled = false;
        this.newItem = {};
    }

    cancel() {
        this.newItem = {};
        this.editorenabled = false;

    }
    cancelnextofkin() {
        this.newnextofkin = {};
        this.editornextofkin = false;
    }

    onSubmitForm() {

        if (this.formGroup.valid) {
            this.person.FirstName = this.formArray.get([0]).value['FirstName'];
            this.person.MiddleName = this.formArray.get([0]).value['MiddleName'];
            this.person.LastName = this.formArray.get([0]).value['LastName'];
            this.person.Sex = this.formArray.get([0]).value['Sex'];
            this.person.DateOfBirth = this.formArray.get([0]).value['DateOfBirth'];
            this.person.MaritalStatus = this.formArray.get([0]).value['MaritalStatus'];
            this.person.Educationallevel = this.formArray.get([0]).value['Educationallevel'];
            this.person.Occcupation = this.formArray.get([0]).value['Occupation'];
            this.person.IdentifyerType = this.formArray.get([0]).value['IdentifyerType'];
            this.person.IdentifyerNumber = this.formArray.get([0]).value['IdentifyerNumber'];
            this.person.RegistrationDate = this.formArray.get([0]).value['RegistrationDate'];
            this.person.DobPrecision = this.formArray.get([0]).value['DobPrecision'];
            this.personlocation.countyId = this.formArray.get([1]).value['County'];
            this.personlocation.subcountyId = this.formArray.get([1]).value['SubCounty'];
            console.log(this.personlocation.subcountyId);
            console.log(this.formGroup.controls);
            this.personlocation.WardId = this.formArray.get([1]).value['Ward'];
            // this.personlocation.NearestHealthCenter = this.formArray.get([1]).['NearestHealthCenter'];  

            this.personlocation.NearestHealthCenter = this.formGroup.controls.formArray['controls'][1].controls.NearestHealthCenter.value;
            console.log(this.personlocation.NearestHealthCenter);
            this.personlocation.LandMark = this.formGroup.controls.formArray['controls'][1].controls.LandMark.value;

            console.log(this.personlocation.LandMark);



            this.person2.FirstName = this.formArray.get([0]).value['FirstName'];
            this.person2.MiddleName = this.formArray.get([0]).value['MiddleName'];
            this.person2.LastName = this.formArray.get([0]).value['LastName'];
            this.person2.Sex = this.formArray.get([0]).value['Sex'];
            this.person2.DateOfBirth = this.formArray.get([0]).value['DateOfBirth'];
            this.person2.MaritalStatus = this.formArray.get([0]).value['MaritalStatus'];
            this.person2.CreatedBy = 1;
            this.person2.DobPrecision = this.person.DobPrecision
            this.person2.PersonId = this.Objectitems['personDetail']['id']

            this.personAddress.MobilePhonenumber = this.formArray.get([2]).value['Mobilenumber'];
            this.personAddress.Alternativenumber = this.formArray.get([2]).value['AlternativeNumber'];
            console.log(this.personAddress.Alternativenumber);
            this.personAddress.EmailAddress = this.formArray.get([2]).value['emailaddress'];
            this.items = this.formGroup.controls.formArray['controls'][2].controls.items.value;

            this.itemnextofkin = this.formGroup.controls.formArray['controls'][3].controls.itemnextofkin.value;

            this.registrationService.registerClient(this.person2).subscribe(data => {
                console.log(data);
                this.PersonIdent.PersonId = this.Objectitems['personDetail']['id'];
                console.log(this.PersonIdent.PersonId)
                const personEducation = this.registrationService.addPersonEducationalLevel(this.PersonIdent.PersonId, this.person.Educationallevel, this.person2.CreatedBy);
                const personContact = this.registrationService.addPersonContact(this.PersonIdent.PersonId, '', this.personAddress.MobilePhonenumber, this.personAddress.Alternativenumber, this.personAddress.EmailAddress, this.person2.CreatedBy)
                const personOccupation = this.registrationService.addPersonOccupation(this.PersonIdent.PersonId, this.person.Occcupation, this.person2.CreatedBy);
                const personLocation = this.registrationService.addPersonLocation(this.PersonIdent.PersonId, this.personlocation.countyId, this.personlocation.subcountyId, this.personlocation.WardId, this.person2.CreatedBy, this.personlocation.LandMark, this.personlocation.NearestHealthCenter);
                const personIdentifier = this.registrationService.addPersonIdentifier(this.PersonIdent.PersonId, this.person.IdentifyerType, this.person.IdentifyerNumber, this.person2.CreatedBy);
                let observables: Observable<any>[] = [];
                this.registrationService.cleararray();

                if (this.items != null || this.items != undefined) {
                    for (let i = 0; i < this.items.length; i++) {


                        var limitedconsent = '';
                        // console.log(this.items[i].emgFirstName);
                        if (this.items[i].emgLimitedConsent === undefined) {
                            limitedconsent = '';



                            console.log(this.items[i].emgLimitedConsent);
                            console.log(limitedconsent);
                        }
                        else {
                            limitedconsent = this.items[i].emgLimitedConsent



                            console.log(this.items[i].emgLimitedConsent);
                            console.log(limitedconsent);
                        }

                        this.emergencycontactarray = this.registrationService.addPersonEmergencyContactArray(this.PersonIdent.PersonId, this.items[i].emgFirstName, this.items[i].emgMiddleName, this.items[i].emgLastName, this.items[i].emgGender, this.items[i].emgEmergencyContactPersonId, this.items[i].emgPrimaryMobileNo, 1, false, this.items[i].emgRelationShip, this.ConsentType, this.items[i].emgConsentToCall, limitedconsent, this.items[i].emgRegisteredClinic, this.items[i].emgEmergencyContactType, this.items[i].emgNextofKinContactType);
                    }


                }

                const personEmergencyContact = this.registrationService.addPersonEmergencyContact();

                const addPatientRegistration = this.registrationService.addPatientRegistration(this.PersonIdent.PersonId, this.person2.DateOfBirth, this.person2.CreatedBy, this.person.IdentifyerNumber, this.person.RegistrationDate);
                // const personNextofKincontact = this.registrationService.addPersonEmergencyContact(this.nextofkincontactarray);





                forkJoin([personEducation, personOccupation, personLocation, personIdentifier, personContact, personEmergencyContact, addPatientRegistration
                    // , personNextofKincontact
                ]).subscribe(results => {

                    localStorage.setItem('personId', this.PersonIdent.PersonId.toString());
                    this.snotifyService.success('Successfully updating client', 'Registration', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/'], { relativeTo: this.route }); });
                }, (err) => {
                    this.snotifyService.error('Error editing the registration of the client ' + err, 'Registration', this.notificationService.getConfig());
                });

            }, err => {
                this.snotifyService.error('Error registering client ' + err, 'Registration', this.notificationService.getConfig());
            });
            console.log(this.PersonIdent.PersonId);

            console.log(this.items);
            console.log(this.itemnextofkin);



        } else {
            return;


        }
    }
    onDate(event): void {
        this.getAge(event);
    }

    estimateDob() {
        const personAge = this.formGroup.controls.formArray['controls'][0].controls.personAge.value;
        const personMonth = this.formGroup.controls.formArray['controls'][0].controls.personMonth.value;
        const currentDate = new Date();
        let birthDate = moment().subtract(personAge, 'years').subtract(personMonth, 'months')
        this.formArray['controls'][0]['controls']['DateOfBirth'].setValue(birthDate.toDate());
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(0);

    }
    getAge(datestring) {
        const today = new Date();
        const birthDate = new Date(datestring);
        let dobMonth = birthDate.getMonth();
        let dobDay = birthDate.getDay();
        let dobYear = birthDate.getFullYear();

        let nowDay = today.getDate();
        let nowMonth = today.getMonth() + 1;
        let nowYear = today.getFullYear();

        let ageyear = nowYear - dobYear;
        let ageMonth = nowMonth - dobMonth;
        let ageDay = nowDay - dobDay;

        if (ageMonth <= 0) {
            ageyear--;
            ageMonth = (12 + ageMonth);
        }
        if (nowDay < dobDay) {
            ageMonth--;
            ageDay = 30 + ageDay;
        }

        this.formArray['controls'][0]['controls']['personAge'].setValue(ageyear);
        this.formArray['controls'][0]['controls']['personMonth'].setValue(ageMonth);
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(1);
    }
    addRow() {
        const emgFirstName = this.formGroup.controls.formArray['controls'][2].controls.emgFirstName.value;
        const emgFirstName2 = this.formGroup.controls.formArray['controls'][2].controls.emgFirstName
        const emgMiddleName = this.formGroup.controls.formArray['controls'][2].controls.emgMiddleName.value;
        const emgMiddleName2 = this.formGroup.controls.formArray['controls'][2].controls.emgMiddleName

        const emgLastName = this.formGroup.controls.formArray['controls'][2].controls.emgLastName.value;
        const emgLastName2 = this.formGroup.controls.formArray['controls'][2].controls.emgLastName
        const emgGender = this.formGroup.controls.formArray['controls'][2].controls.emgGender.value;
        const emgGender2 = this.formGroup.controls.formArray['controls'][2].controls.emgGender
        const emgRelationShip = this.formGroup.controls.formArray['controls'][2].controls.emgRelationShip.value;
        const emgRelationShip2 = this.formGroup.controls.formArray['controls'][2].controls.emgRelationShip
        const emgPrimaryMobileNo = this.formGroup.controls.formArray['controls'][2].controls.emgPrimaryMobileNo.value;
        const emgPrimaryMobileNo2 = this.formGroup.controls.formArray['controls'][2].controls.emgPrimaryMobileNo
        const emgConsentToCall = this.formGroup.controls.formArray['controls'][2].controls.emgConsentToCall.value;
        const emgConsentToCall2 = this.formGroup.controls.formArray['controls'][2].controls.emgConsentToCall
        const emgRegisteredClinic = this.formGroup.controls.formArray['controls'][2].controls.emgRegisteredClinic.value;
        const emgRegisteredClinic2 = this.formGroup.controls.formArray['controls'][2].controls.emgRegisteredClinic
        if (!(emgFirstName === undefined || emgFirstName === '' || !emgFirstName === null)
            || !(emgMiddleName === undefined || emgMiddleName === '' || emgMiddleName === null)
            || !(emgLastName === undefined || emgLastName === '' || emgLastName !== null)
            || !(emgGender === undefined || emgGender === '' || emgGender === null)
            || !(emgRelationShip === undefined || emgRelationShip === '' || emgRelationShip !== null)
            || !(emgPrimaryMobileNo === undefined || emgPrimaryMobileNo === '' || emgPrimaryMobileNo !== null)
            || !(emgConsentToCall === undefined || emgConsentToCall === '' || emgConsentToCall === null)
            || !(emgRegisteredClinic === undefined || emgRegisteredClinic === '' || emgRegisteredClinic === null)) {
            {

                this.emergencylist = new EmergencyListEdit();
                if (this.newItem.emgRegisteredClinic == '1') {
                    this.emergencylist.emgRegisteredClinic = 'Yes';
                }
                else {
                    this.emergencylist.emgRegisteredClinic = 'No';
                }


                this.emergencylist.emgFirstName = this.newItem.emgFirstName;
                this.emergencylist.emgLastName = this.newItem.emgLastName;
                this.emergencylist.emgMiddleName = this.newItem.emgMiddleName;
                let consent = this.consentoptions.find(s => s.itemId == this.newItem.emgConsentToCall);


                this.emergencylist.emgConsentToCall = consent['itemName'];
                let gender = this.gender.find(s => s.itemId == this.newItem.emgGender);
                this.emergencylist.emgGender = gender['itemName'];



                this.emergencylist.emgLimitedConsent = this.newItem.emgLimitedConsent;
                let rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newItem.emgRelationShip);
                this.emergencylist.emgRelationShip = rel['itemName'];
                this.emergencylist.emgPrimaryMobileNo = this.newItem.emgPrimaryMobileNo;
                this.emergencylist.emgDeleteFlag = false;
                this.itemslist.push(this.emergencylist);

                this.newItem.deleteflag = false;
                this.items.push(this.newItem);
                this.newItem = {};
                this.formGroup.controls.formArray['controls'][2].controls.items.setValue(this.items);

            }
        }
    }



}


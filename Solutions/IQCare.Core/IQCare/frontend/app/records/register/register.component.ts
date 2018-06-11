import { Component, OnInit ,NgZone} from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators,FormArray } from '@angular/forms';
import { Person,Person2,EmergencyArray, RegistrationVariables,PersonLocation,County,SubCounty,PersonIdentification,Ward,PersonAddress,Emergency,EmergencyList,NextofKinEmergency,NextofKinEmergencyList} from '../models/person';
import { RegistrationService } from '../services/RecordsRegistrationService';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { ClientService } from '../../shared/_services/client.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { NotificationService } from '../../shared/_services/notification.service';
import { Observable } from 'rxjs/Observable';
import { emergencyValidator } from '../models/emergencyvalidator'
import { MatStepper } from '@angular/material';

@Component({
    selector: 'recordsregister',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})


export class RecordsRegisterComponent implements OnInit {
    nextofkinmessage:Observable<any>;
    personemergencymessage: Observable<any>;
    toggoleShowHide: string = "hidden";
    PersonIdent: PersonIdentification;
    personemergency: Observable<any>;
    person: Person;
    person2: Person2;
    nextofkinemergencylist: NextofKinEmergencyList;
    nextofkinemergency: NextofKinEmergency;
    emergencylist: EmergencyList;
    emergency: Emergency;
    userId: number;
    registrationVariables: RegistrationVariables;
    personlocation: PersonLocation;
    personAddress: PersonAddress;
    relationshipEmergencyOptions: any[];
    relationshipNextofKinOptions: any[];
    IdentifyerTypes: Array<any> = [];
    consent: string;
    emergencycontactarray: EmergencyArray[] = [];
    nextofkincontactarray: EmergencyArray[] = [];
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
    items: Emergency[];
    itemslist: EmergencyList[];
    itemnextofkin: NextofKinEmergency[];
    itemlistnextofkin: NextofKinEmergencyList[];
    newItem: any = {};
    newnextofkin: any = {};
    isLinear = true;
    public personRelationId:number
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

        this.PersonIdent = new PersonIdentification;
        this.person2 = new Person2();
        this.person = new Person();
        this.userId = JSON.parse(localStorage.getItem("appUserId"));
        this.relationshipEmergencyOptions = [];
        this.relationshipNextofKinOptions = [];
        this.registrationVariables = new RegistrationVariables();
        this.personlocation = new PersonLocation();
        this.occupations = [];
        this.emergency = new Emergency();
        this.educationallevel = [];
        this.consentoptions = [];
        this.maritalstatuses = [];
        this.personAddress = new PersonAddress();
        this.items = [];
        this.itemslist = [];
        this.nextofkinemergency = new NextofKinEmergency();
        this.itemnextofkin = [];
        this.itemlistnextofkin = [];
        this.registrationService.getCounties().subscribe(
            (data: any) => {

                this.counties = data["counties"];


            });
        this.registrationService.getOppOptions().subscribe(
            (data: any) => {

                this.occupations = data["lookupItems"]["0"]["value"]

            }
        )
        console.log(this.counties);
        this.ConsentType = this.route.snapshot.data["ConsentType"]["consentType"];
        console.log(this.ConsentType);
        this.IdentifyerTypes = this.route.snapshot.data["IdentifyerType"]['identifers'];
        console.log(this.IdentifyerTypes);
        this.route.data.subscribe((res) => {
            const options = res["options"]["lookupItems"];
            const EducConsent = res["EducOppConsent"]["lookupItems"];
            const maritalstatus = res["maritalstatusresolve"]["lookupItems"];


            for (let i = 0; i < EducConsent.length; i++) {

                if (EducConsent[i].key == 'EducationalLevel') {
                    this.educationallevel = EducConsent[i].value;
                }
                else if (EducConsent[i].key == 'ConsentOptions') {
                    this.consentoptions = EducConsent[i].value;

                }

            }

            for (let i = 0; i < maritalstatus.length; i++) {
                if (maritalstatus[i].key == 'MaritalStatus') {
                    this.maritalstatuses = maritalstatus[i].value;
                }
            }

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'Gender') {
                    this.gender = options[i].value;

                }
                else if (options[i].key == 'Relationship') {
                    const returnOptions = options[i].value;
                    for (let j = 0; j < returnOptions.length; j++) {
                        this.relationshipEmergencyOptions.push(returnOptions[j]);
                        this.relationshipNextofKinOptions.push(returnOptions[j]);
                    }
                }
            }



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
                    County: new FormControl(this.personlocation.countyId, [Validators.required]),
                    SubCounty: new FormControl(this.personlocation.subcountyId, [Validators.required]),
                    Ward: new FormControl(this.personlocation.WardId, [Validators.required]),
                    NearestHealthCenter: new FormControl(this.personlocation.NearestHealthCenter),
                    LandMark: new FormControl(this.personlocation.LandMark)
                }),
                this._formBuilder.group({
                    Mobilenumber: new FormControl(this.personAddress.MobilePhonenumber, [Validators.required]),
                    AlternativeNumber: new FormControl(this.personAddress.Alternativenumber, [Validators.required]),
                    emailaddress: new FormControl(this.personAddress.EmailAddress, [Validators.required]),
                    emgFirstName: new FormControl(this.emergency.emgFirstName ),
                    emgMiddleName: new FormControl(this.emergency.emgMiddleName ),
                    emgLastName: new FormControl(this.emergency.emgLastName),
                    emgGender: new FormControl(this.emergency.emgGender),
                    emgRelationShip: new FormControl(this.emergency.emgRelationShip ),
                    emgPrimaryMobileNo: new FormControl(this.emergency.emgPrimaryMobileNo ),
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







        //OnWardCountListSelect(subcountyid){
        //    if (subcountyid == 0)
        //        this.subcounties = null;
        //    else
        //        this.registrationService.getWardList(subcountyid).subscribe(data => { this.wardlist = data });
        //}
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
               
                this.nextofkinemergencylist = new NextofKinEmergencyList();
                if (this.newnextofkin.nokRegisteredClinic == '1') {
                    this.nextofkinemergencylist.nokRegisteredClinic = "Yes";

                }
                else {
                    this.nextofkinemergencylist.nokRegisteredClinic = "No";
                }

                this.nextofkinemergencylist.nokFirstName = this.newnextofkin.nokFirstName;
                this.nextofkinemergencylist.nokLastName = this.newnextofkin.nokLastName;
                this.nextofkinemergencylist.nokMiddleName = this.newnextofkin.nokMiddleName;
                var consent = this.consentoptions.find(s => s.itemId == this.newnextofkin.nokConsentToCall);
                this.nextofkinemergencylist.nokConsentToCall = consent["itemName"];
                var gender = this.gender.find(s => s.itemId == this.newnextofkin.nokGender);
                this.nextofkinemergencylist.nokGender = gender["itemName"];



                this.nextofkinemergencylist.nokLimitedConsent = this.newnextofkin.nokLimitedConsent;
                var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newnextofkin.nokRelationShip);
                this.nextofkinemergencylist.nokRelationShip = rel["itemName"];
                this.nextofkinemergencylist.nokPrimaryMobileNo = this.newnextofkin.emgPrimaryMobileNo

                this.itemlistnextofkin.push(this.nextofkinemergencylist);

             
                this.itemnextofkin.push(this.newnextofkin);

                this.formGroup.controls.formArray['controls'][3].controls.itemnextofkin.setValue(this.itemnextofkin);
                

                this.newnextofkin = {};
            }
        }
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
                
                this.emergencylist = new EmergencyList();
                if (this.newItem.emgRegisteredClinic == '1') {
                    this.emergencylist.emgRegisteredClinic = "Yes";
                }
                else {
                    this.emergencylist.emgRegisteredClinic = "No";
                }


                this.emergencylist.emgFirstName = this.newItem.emgFirstName;
                this.emergencylist.emgLastName = this.newItem.emgLastName;
                this.emergencylist.emgMiddleName = this.newItem.emgMiddleName;
                var consent = this.consentoptions.find(s => s.itemId == this.newItem.emgConsentToCall);


                this.emergencylist.emgConsentToCall = consent["itemName"];
                var gender = this.gender.find(s => s.itemId == this.newItem.emgGender);
                this.emergencylist.emgGender = gender["itemName"];



                this.emergencylist.emgLimitedConsent = this.newItem.emgLimitedConsent;
                var rel = this.relationshipEmergencyOptions.find(s => s.itemId == this.newItem.emgRelationShip);
                this.emergencylist.emgRelationShip = rel["itemName"];
                this.emergencylist.emgPrimaryMobileNo = this.newItem.emgPrimaryMobileNo

                this.itemslist.push(this.emergencylist);


                this.items.push(this.newItem);
                this.newItem = {};
                this.formGroup.controls.formArray['controls'][2].controls.items.setValue(this.items);

            }
        }
    }

    delete(i) {

        this.itemslist.splice(i, 1);
        this.items.splice(i, 1);


    }
    deletenextofkin(i) {
        this.itemnextofkin.splice(i, 1);
        this.itemlistnextofkin.splice(i, 1);
    }

    onDate(event): void {
        this.getAge(event);
    }

    estimateDob() {
        const personAge = this.formGroup.controls.formArray['controls'][0].controls.personAge.value;
        const personMonth = this.formGroup.controls.formArray['controls'][0].controls.personMonth.value;
        const currentDate = new Date();
        var birthDate = moment().subtract(personAge, 'years').subtract(personMonth, 'months')
        this.formArray['controls'][0]['controls']['DateOfBirth'].setValue(birthDate.toDate());
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(0);

    }
    getAge(datestring) {
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
        this.formArray['controls'][0]['controls']['DobPrecision'].setValue(1);
    }
    buttonclick() {


        console.log(this.formGroup.controls);
        this.personlocation.NearestHealthCenter = this.formGroup.controls.formArray['controls'][1].controls.NearestHealthCenter.value;
        this.personlocation.LandMark = this.formGroup.controls.formArray['controls'][1].controls.LandMark.value;
        console.log(this.personlocation.NearestHealthCenter);
        console.log(this.personlocation.LandMark);
       // console.log(this.items);
       // console.log(this.itemnextofkin);
        console.log(this.formArray)
        console.log(this.formGroup.controls.formArray['controls'][2].controls.items);
        this.items = this.formGroup.controls.formArray['controls'][2].controls.items.value;
        console.log(this.items);
        this.itemnextofkin = this.formGroup.controls.formArray['controls'][3].controls.itemnextofkin.value;
        console.log(this.itemnextofkin);
    }

    validate():boolean {
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
        if ((emgFirstName === undefined || emgFirstName === '' || emgFirstName === null)
            ||(emgMiddleName === undefined || emgMiddleName === '' || emgMiddleName === null)
            || (emgLastName === undefined || emgLastName === '' || emgLastName !== null)
            || (emgGender === undefined || emgGender === '' || emgGender === null)
            || (emgRelationShip === undefined || emgRelationShip === '' || emgRelationShip !== null)
            || (emgPrimaryMobileNo === undefined || emgPrimaryMobileNo === '' || emgPrimaryMobileNo !== null)
            || (emgConsentToCall === undefined || emgConsentToCall === '' || emgConsentToCall === null)
            || (emgRegisteredClinic === undefined || emgRegisteredClinic === '' || emgRegisteredClinic === null)) {

            emgFirstName2.setValidators([Validators.required])
            emgFirstName2.updateValueAndValidity({ onlySelf: true })
            emgLastName2.setValidators([Validators.required])
            emgLastName2.updateValueAndValidity({ onlySelf: true })
            emgGender2.setValidators([Validators.required])
            emgGender2.updateValueAndValidity({ onlySelf: true })
            emgRelationShip2.setValidators([Validators.required])
            emgRelationShip2.updateValueAndValidity({ onlySelf: true })
            emgPrimaryMobileNo2.setValidators([Validators.required])
            emgPrimaryMobileNo2.updateValueAndValidity({ onlySelf: true })
            emgConsentToCall2.setValidators([Validators.required])
            emgConsentToCall2.updateValueAndValidity({ onlySelf: true })
            emgRegisteredClinic2.setValidators([Validators.required])
            emgRegisteredClinic2.updateValueAndValidity({ onlySelf: true })


            emgFirstName2.setErrors({ 'incorrect': true });
            emgMiddleName2.setErrors({ 'incorrect': true });
            emgLastName2.setErrors({ 'incorrect': true });
            emgGender2.setErrors({ 'incorrect': true });
            emgRelationShip2.setErrors({ 'incorrect': true });
            emgPrimaryMobileNo2.setErrors({ 'incorrect': true });
            emgConsentToCall2.setErrors({ 'incorrect': true });
            emgRegisteredClinic2.setErrors({ 'incorrect': true });

            return false;




        }
        else {
            return true;
        }

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
            this.person.Occcupation= this.formArray.get([0]).value['Occupation'];                     
            this.person.IdentifyerType = this.formArray.get([0]).value['IdentifyerType'];                           
            this.person.IdentifyerNumber = this.formArray.get([0]).value['IdentifyerNumber'];                           
            this.person.RegistrationDate = this.formArray.get([0]).value['RegistrationDate'];                      
            this.person.DobPrecision = this.formArray.get([0]).value['DobPrecision'];                                  
            this.personlocation.countyId =this.formArray.get([1]).value['County'];    
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
            this.person2.PersonId = 0;
        
           this.personAddress.MobilePhonenumber = this.formArray.get([2]).value['Mobilenumber'];
           this.personAddress.Alternativenumber = this.formArray.get([2]).value['AlternativeNumber'];
           console.log(this.personAddress.Alternativenumber);
            this.personAddress.EmailAddress = this.formArray.get([2]).value['emailaddress'];
            this.items = this.formGroup.controls.formArray['controls'][2].controls.items.value;
         
            this.itemnextofkin = this.formGroup.controls.formArray['controls'][3].controls.itemnextofkin.value;
            
            this.registrationService.registerClient(this.person2).subscribe(data => {
                console.log(data);
                this.PersonIdent.PersonId = data['personId'];
                console.log(this.PersonIdent.PersonId)
                const personEducation = this.registrationService.addPersonEducationalLevel(this.PersonIdent.PersonId, this.person.Educationallevel, this.person2.CreatedBy);
                const personContact = this.registrationService.addPersonContact(this.PersonIdent.PersonId,'', this.personAddress.MobilePhonenumber, this.personAddress.Alternativenumber, this.personAddress.EmailAddress, this.person2.CreatedBy)
                const personOccupation = this.registrationService.addPersonOccupation(this.PersonIdent.PersonId, this.person.Occcupation, this.person2.CreatedBy);
                const personLocation = this.registrationService.addPersonLocation(this.PersonIdent.PersonId, this.personlocation.countyId, this.personlocation.subcountyId, this.personlocation.WardId, this.person2.CreatedBy, this.personlocation.LandMark,this.personlocation.NearestHealthCenter);
                const personIdentifier = this.registrationService.addPersonIdentifier(this.PersonIdent.PersonId, this.person.IdentifyerType, this.person.IdentifyerNumber, this.person2.CreatedBy);
                let observables: Observable<any>[] = []; 
                if (this.items != null || this.items != undefined) {
                    for (let i = 0; i < this.items.length; i++) {
                        var limitedconsent = "";
                        //console.log(this.items[i].emgFirstName);
                        if (this.items[i].emgLimitedConsent === undefined) {
                            limitedconsent = "";
                            
                           
                         
                            console.log(this.items[i].emgLimitedConsent);
                            console.log(limitedconsent);
                        }
                        else {
                            limitedconsent = this.items[i].emgLimitedConsent

                           
                            
                            console.log(this.items[i].emgLimitedConsent);
                            console.log(limitedconsent);
                        }

                        this.emergencycontactarray = this.registrationService.addPersonEmergencyContactArray(this.PersonIdent.PersonId, this.items[i].emgFirstName, this.items[i].emgMiddleName, this.items[i].emgLastName, this.items[i].emgGender, 0, this.items[i].emgPrimaryMobileNo, 1, true, this.items[i].emgRelationShip, this.ConsentType, this.items[i].emgConsentToCall, limitedconsent);
                       


                    }
                }

                if (this.itemnextofkin != null || this.itemnextofkin != undefined) {
                    for (let i = 0; i < this.itemnextofkin.length; i++) {
                        if (this.itemnextofkin[i].nokLimitedConsent === undefined) {
                            limitedconsent = "";
                            
                           
                            console.log(this.itemnextofkin[i].nokLimitedConsent);
                            console.log(limitedconsent);
                        }
                        else {
                            limitedconsent = this.itemnextofkin[i].nokLimitedConsent
                           

                            //console.log(this.itemnextofkin[i].nokMiddleName);
                           
                            console.log(this.itemnextofkin[i].nokLimitedConsent);
                            console.log(limitedconsent);
                        }
                        this.nextofkincontactarray = this.registrationService.addPersonEmergencyContactArray(this.PersonIdent.PersonId, this.itemnextofkin[i].nokFirstName, this.itemnextofkin[i].nokMiddleName, this.itemnextofkin[i].nokLastName, this.itemnextofkin[i].nokGender, 0, this.itemnextofkin[i].nokPrimaryMobileNo, 1, true, this.itemnextofkin[i].nokRelationShip, this.ConsentType, this.itemnextofkin[i].nokConsentToCall, limitedconsent);

                    }
                }

                const personEmergencyContact = this.registrationService.addPersonEmergencyContact(this.emergencycontactarray);
               // const personNextofKincontact = this.registrationService.addPersonEmergencyContact(this.nextofkincontactarray);


                
        
                
                forkJoin([personEducation, personOccupation, personLocation, personIdentifier, personContact, personEmergencyContact
                    //, personNextofKincontact
         ]).subscribe(results => {

                    localStorage.setItem('personId', this.PersonIdent.PersonId.toString());
                 this.snotifyService.success('Successfully registered client', 'Registration', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/'], { relativeTo: this.route }); });
                }, (err) => {
                    this.snotifyService.error('Error registering client ' + err, 'Registration', this.notificationService.getConfig());
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
selectchange(value: any) {
       
        this.consentoptype = value;
        var res = this.consentoptions.find(s => s.itemId==this.consentoptype);
      
        if (res)
            this.consent = res["itemName"]
   
        if (this.consent == "Limitedconcent") {
            this.toggoleShowHide = "visible";
        }
            else 
        {
            this.toggoleShowHide = "hidden";
        }
    }

    OnWardCountListSelect(value: any) {
        this.subcountyid = value;
       
        if (this.subcountyid == null)
            this.subcounties = null;
        else {
            this.registrationService.getWardList(this.subcountyid).subscribe((data: any) => {

              
                this.wardlist = data["wards"];
              
            });
        }
        }


    OnSubCountySelect(value: any) {
        this.countyid = value;
        if (this.countyid == null)

            this.subcounties = null;
        else {
            this.registrationService.getSubCounty(this.countyid).subscribe(
                (data: any) => {
               
                    this.subcounties = data["subCounties"];

                    
                });



               
        }
        
    }
 }
   




import { Component, OnInit ,NgZone} from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Person, RegistrationVariables} from '../models/person';
import { RegistrationService } from '../services/RecordsRegistrationService';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { ClientService } from '../../shared/_services/client.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { NotificationService } from '../../shared/_services/notification.service';

@Component({
  selector: 'recordsregister',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RecordsRegisterComponent implements OnInit {
    person: Person;
    userId: number;
    registrationVariables: RegistrationVariables;
    relationshipEmergencyOptions: any[];
    relationshipNextofKinOptions: any[];
    IdentifyerTypes: Array<any> = [];
    occupations: any[];
    maritalstatuses: any[];
    educationallevel: any[];
    consentoptions: any[];
    gender: any[];
    male: number;
    female: number;
    maxDate: any;
    ConsentType: number;
    formGroup: FormGroup;
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
        this.userId = JSON.parse(localStorage.getItem("appUserId"));
        this.relationshipEmergencyOptions = [];
        this.relationshipNextofKinOptions = [];
        this.registrationVariables = new RegistrationVariables();
        this.occupations = [];
        this.educationallevel = [];
        this.consentoptions = [];
        this.maritalstatuses = [];
        this.ConsentType = this.route.snapshot.data["ConsentType"];
        this.IdentifyerTypes = this.route.snapshot.data["IdentifyerType"]['identifers'];
        console.log(this.IdentifyerTypes);
        this.route.data.subscribe((res) => {
            const options = res["options"]["lookupItems"];
            const EducConsent = res["EducOppConsent"]["lookupItems"];
          
            console.log(options);

        
            for (let i = 0; i < EducConsent.length; i++) {
                if (EducConsent[i].key == 'Occupation') {
                    this.occupations = EducConsent[i].value;
                }
                else if (EducConsent[i].key == 'EducationalLevel') {
                    this.educationallevel = EducConsent[i].value;
                }
                else if (EducConsent[i].key == 'ConsentOptions') {
                    this.consentoptions = EducConsent[i].value;

                }
                else if (EducConsent[i].key == 'MaritalStatus') {
                    this.maritalstatuses = EducConsent[i].value;
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
                    IdentifyerType: new FormControl(this.person.IdentifyerType,[Validators.required]),
                    IdentifyerNumber: new FormControl(this.person.IdentifyerNumber, [Validators.required]),
                    RegistrationDate: new FormControl(this.person.RegistrationDate, [Validators.required]),
                    personAge: new FormControl(this.registrationVariables.personAge, [Validators.required]),
                    personMonth: new FormControl(this.registrationVariables.personMonth, [Validators.required])

                }),


            ]),

        });
    }




}

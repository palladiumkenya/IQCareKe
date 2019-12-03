import { Component, OnInit, Input, NgZone } from '@angular/core';
import { RecordsService } from '../../../../records/_services/records.service';
import { InlineSearchComponent } from './../../../../records/inline-search/inline-search.component';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { OvcService } from '../../../_services/ovc.service';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogConfig, MatDialog, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Person } from '../../../../registration/_models/person';
import { ClientContact } from '../../../../records/_models/clientcontact';
import { PersonRegistrationService } from '../../../../records/_services/person-registration.service';
import { forkJoin } from 'rxjs';


@Component({
    selector: 'app-register',
    templateUrl: './registerfamily.component.html',
    styleUrls: ['./registerfamily.component.css'],
    providers: [RegistrationService]
})
export class RegisterFamilyComponent implements OnInit {


    registerformGroup: FormGroup;
    contactPersonId: number;
    contactAge: number;
    gender: LookupItemView[];
    personId: number;
    caregiveoptions: LookupItemView[] = [];
    patientId: number;
    person: Person;
    clientContact: ClientContact;
    serviceCode: string;
    serviceId: number;
    dataSource: any[] = [];
    newrel: any[];
    createdBy?: number;

    PosId: number;

    public phonePattern = /^(?:\+254|0|254)(\d{9})$/;
    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private _formBuilder: FormBuilder,
        private dialog: MatDialog,
        private ovcService: OvcService,
        private recordsService: RecordsService,
        private registrationService: RegistrationService,
        private snotifyService: SnotifyService,
        private spinner: NgxSpinnerService,
        private personRegistration: PersonRegistrationService,
        private notificationService: NotificationService) { }

    async ngOnInit() {

        this.route.params.subscribe(
            p => {
                const { personId, patientId, serviceCode, serviceId } = p;
                this.personId = personId;
                this.serviceCode = serviceCode;
                this.serviceId = serviceId;
                this.patientId = patientId;
            }
        );
        this.registerformGroup = this._formBuilder.group({


            FirstName: new FormControl('', [Validators.required]),
            MiddleName: new FormControl('', [Validators.required]),
            LastName: new FormControl('', [Validators.required]),
            MobileNumber: new FormControl('', [Validators.required]),
            Caregiverel: new FormControl('', [Validators.required]),
            Sex: new FormControl('', [Validators.required])

        });

        const caregiverlist = await this.ovcService.getByGroupMasterName('CaregiverRelationship').toPromise();
        this.caregiveoptions = caregiverlist;
        const sexoptions = await this.ovcService.getByGroupMasterName('Gender').toPromise();
        this.gender = sexoptions;


    }

    AddFamilyMember() {
        if (!this.registerformGroup.valid) {
            return;

        }
        else {
            const { FirstName, MiddleName, LastName, Sex, MobileNumber, Caregiverel } = this.registerformGroup.value;


            let rel: string;
            const relationshiplist = this.caregiveoptions.filter(x => x.itemId == Caregiverel);
            if (relationshiplist.length > 0) {
                rel = relationshiplist[0].itemDisplayName;
            }
            let sexgender: string;
            const sexgenderlist = this.caregiveoptions.filter(x => x.itemId == Sex);
            if (sexgenderlist.length > 0) {
                sexgender = sexgenderlist[0].itemDisplayName;
            }
            this.dataSource.push(
                {
                    'firstName': FirstName,
                    'middleName': MiddleName,
                    'lastName': LastName,
                    'gender': Sex,
                    'gendertext': sexgender,
                    'relationship': Caregiverel,
                    'relationshiptext': rel,
                    'phoneno': MobileNumber

                }
            );

            this.registerformGroup.controls.FirstName.setValue('');
            this.registerformGroup.controls.MiddleName.setValue('');
            this.registerformGroup.controls.LastName.setValue('');
            this.registerformGroup.controls.Sex.setValue('');
            this.registerformGroup.controls.MobileNumber.setValue('');
            this.registerformGroup.controls.Caregiverel.setValue('');


        }
    }

    deletedetails(data: any, index: number, event: any) {
        const result = this.snotifyService.confirm('Are you sure you want to delete?', 'Contacts', {
            closeOnClick: true,
            position: SnotifyPosition.centerCenter,
            buttons: [
                {
                    text: 'Yes', action: () => {
                        const relFiltered = this.dataSource.filter((obj) => {
                            return obj.firstName !== data.firstName
                                && obj.lastName !== data.lastName
                                && obj.middleName !== data.middleName
                                && obj.gender !== data.gender
                                && obj.relationship !== data.relationship
                                && obj.phoneno !== data.phoneno;
                        });

                        this.newrel = relFiltered;
                        this.dataSource.splice(index, 1);

                        console.log(this.newrel);
                    }, bold: false
                },
                { text: 'No', action: () => console.log('Clicked: No') }
            ]
        });

    }
    SaveData() {
        if (this.dataSource.length > 0) {

            this.spinner.show();
            this.dataSource.forEach(x => {
                this.person = new Person();


                this.clientContact = new ClientContact();
                this.clientContact.MobileNumber = x.phoneno;


                this.createdBy = JSON.parse(localStorage.getItem('appUserId'));
                this.PosId = JSON.parse(localStorage.getItem('appPosID'));


                this.spinner.show();
                this.registrationService.addBasicPerson(x.firstName, x.lastName, x.middleName
                    , x.gender, this.createdBy, this.PosId).subscribe((response) => {
                        const { personId } = response;

                        const personContact = this.personRegistration.addPersonContact(personId, this.person.createdBy, this.clientContact);


                        const personRelation = {};
                        personRelation['PersonId'] = personId;
                        personRelation['PatientId'] = this.patientId;
                        personRelation['RelationshipTypeId'] = x.relationship;
                        personRelation['UserId'] = JSON.parse(localStorage.getItem('appUserId'));

                        const patientAdd = this.registrationService.addPersonRelationship(personRelation);

                        forkJoin([personContact, patientAdd]).subscribe((forkRes) => {

                        },
                            (forkError) => {
                                this.snotifyService.error('Error creating person contact and relationship' + forkError
                                    , 'Person Relationship',
                                    this.notificationService.getConfig());
                                this.spinner.hide();
                            }, () => {
                                this.snotifyService.success('Successfully registered the  Person Contact and relationship',
                                    'Person Relationship', this.notificationService.getConfig());
                                this.spinner.hide();

                            });
                    });


            });
            this.spinner.hide();

            this.zone.run(() => {
                this.router.navigate(
                    ['/ccc/ovcnewEncounter/' + this.personId + '/'
                        + this.patientId + '/' + this.serviceId],
                    { relativeTo: this.route });

            });



        } else {
            this.spinner.hide();
            this.snotifyService.error('Kindly add the caregiver details', 'Caregiver and Relationship to the Patient',
                this.notificationService.getConfig());
        }
    }
    cancel() {
        this.zone.run(() => {
            this.router.navigate(
                ['/ccc/ovcnewEncounter/' + this.personId + '/'
                    + this.patientId + '/' + this.serviceId],
                { relativeTo: this.route });
        });

    }

}

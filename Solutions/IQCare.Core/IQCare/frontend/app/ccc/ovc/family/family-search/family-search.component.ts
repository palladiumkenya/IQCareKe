import { Component, OnInit, Input, NgZone } from '@angular/core';
import { RecordsService } from '../../../../records/_services/records.service';
import { InlineSearchComponent } from './../../../../records/inline-search/inline-search.component';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { OvcService } from '../../../_services/ovc.service';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogConfig, MatDialog, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-family-search',
    templateUrl: './family-search.component.html',
    styleUrls: ['./family-search.component.css'],
    providers: [RegistrationService]
})
export class FamilySearchComponent implements OnInit {
    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender'];
    dataSource = new MatTableDataSource();
    familyformGroup: FormGroup;
    contactPersonId: number;
    contactAge: number;
    personId: number;
    caregiveoptions: LookupItemView[] = [];
    patientId: number;
    serviceCode: string;
    serviceId: number;


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


        this.familyformGroup = this._formBuilder.group({


            Caregiverel: new FormControl('', [Validators.required])

        });
        const caregiverlist = await this.ovcService.getByGroupMasterName('CaregiverRelationship').toPromise();
        this.caregiveoptions = caregiverlist;



    }

    findFamilyOrPartner() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '75%';
        dialogConfig.width = '60%';

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(InlineSearchComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {

                if (!data) {
                    return;
                }

                if (this.personId == data[0]['id']) {
                    this.snotifyService.error('You are trying Client as their own contact', 'Encounter',
                        this.notificationService.getConfig());
                    return;
                }

                this.contactPersonId = data[0]['id'];
                this.contactAge = data[0]['ageNumber'];

                const newContact: [{}] = [{
                    'firstName': data[0]['firstName'],
                    'midName': data[0]['middleName'],
                    'lastName': data[0]['lastName'],
                    'dateOfBirth': data[0]['dateOfBirth'],
                    'gender': data[0]['gender']
                }];
                this.dataSource.data = newContact;
            }
        );
    }
    saveRelationship() {

        if (!this.familyformGroup.valid || !this.contactPersonId) {
            if (!this.contactPersonId) {
                this.snotifyService.error('Select a contact to be listed', 'Encounter',
                    this.notificationService.getConfig());
            }
            return;
        }

        const personRelation = {};
        personRelation['PersonId'] = this.contactPersonId;
        personRelation['PatientId'] = this.patientId;
        personRelation['RelationshipTypeId'] = this.familyformGroup.controls.Caregiverel.value;
        personRelation['UserId'] = JSON.parse(localStorage.getItem('appUserId'));

        const patientAdd = this.registrationService.addPersonRelationship(personRelation);
        patientAdd.subscribe((relationshipResult) => {
            this.spinner.show();
            if (relationshipResult != null) {
                this.zone.run(() => {
                    this.router.navigate(
                        ['/ccc/ovcnewEncounter/' + this.personId + '/'
                            + this.patientId + '/' + this.serviceId],
                        { relativeTo: this.route });
                });


            }
        }, (error) => {
            this.snotifyService.error('Error adding the linkage' + error, 'Linking the Family',
                this.notificationService.getConfig());
            this.spinner.hide();
        }, () => {
            this.spinner.hide();
        });

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

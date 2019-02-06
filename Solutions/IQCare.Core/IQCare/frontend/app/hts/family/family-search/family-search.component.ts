import { InlineSearchComponent } from './../../../records/inline-search/inline-search.component';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogConfig, MatDialog, MatTableDataSource } from '@angular/material';
import { FamilyPartnerControlsService } from '../../_services/family-partner-controls.service';
import { Partner } from '../../../shared/_models/partner';
import { RecordsService } from '../../../records/_services/records.service';
import { RegistrationService } from '../../../registration/_services/registration.service';

@Component({
    selector: 'app-family-search',
    templateUrl: './family-search.component.html',
    styleUrls: ['./family-search.component.css']
})
export class FamilySearchComponent implements OnInit {
    formGroup: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    relationshipPartnerOptions: LookupItemView[] = [];
    relationshipFamilyOptions: LookupItemView[] = [];
    findRegistered: boolean = false;
    registerContact: boolean = false;
    partnerType: Partner;
    relationshipOptions: LookupItemView[] = [];

    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender'];
    dataSource = new MatTableDataSource();
    contactPersonId: number;

    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private _formBuilder: FormBuilder,
        private dialog: MatDialog,
        private service: FamilyPartnerControlsService,
        private recordsService: RecordsService,
        private registrationService: RegistrationService) { }

    ngOnInit() {
        this.formGroup = this._formBuilder.group({
            isRegisteredInFacility: new FormControl('', [Validators.required]),
            relationship: new FormControl('', [Validators.required]),
        });

        this.partnerType = new Partner();
        this.route.data.subscribe(
            (res) => {
                const { yesnoOptions } = res;
                this.yesnoOptions = yesnoOptions['lookupItems'];
            }
        );

        this.route.params.subscribe(
            (res) => {
                if (res && res.personId) {
                    this.registerContact = false;
                    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                    this.formGroup.get('isRegisteredInFacility').setValue(noOption[0].itemId);
                    this.recordsService.getPersonDetails(res.personId).subscribe(
                        (result) => {
                            this.contactPersonId = result[0]['id'];
                            const newContact: [{}] = [{
                                'firstName': result[0]['firstName'],
                                'midName': result[0]['middleName'],
                                'lastName': result[0]['lastName'],
                                'dateOfBirth': result[0]['dateOfBirth'],
                                'gender': result[0]['gender']
                            }];
                            this.dataSource.data = newContact;
                        }
                    );
                }
            }
        );

        this.service.getRelationshipTypes().subscribe(
            (res) => {
                const partnerOptions = ['Partner', 'Co-Wife', 'Spouse'];
                const options = res['lookupItems'];
                for (let j = 0; j < options.length; j++) {
                    if (partnerOptions.includes(options[j].itemName)) {
                        this.relationshipPartnerOptions.push(options[j]);
                    } else {
                        this.relationshipFamilyOptions.push(options[j]);
                    }
                }


                this.partnerType = JSON.parse(localStorage.getItem('isPartner'));
                if (this.partnerType != null) {
                    if (this.partnerType.partner == 1) {
                        this.relationshipOptions = this.relationshipPartnerOptions;
                    } else if (this.partnerType.family == 1) {
                        this.relationshipOptions = this.relationshipFamilyOptions;
                    }
                }
            }
        );
    }

    onRegisteredChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.findRegistered = true;
            this.registerContact = false;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.findRegistered = false;
            this.registerContact = true;
        }
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

                // console.log(data);
                this.contactPersonId = data[0]['id'];
                const newContact: [{}] = [{
                    'firstName': data[0]['firstName'],
                    'midName': data[0]['middleName'],
                    'lastName': data[0]['lastName'],
                    'dateOfBirth': data[0]['dateOfBirth'],
                    'gender': data[0]['gender']
                }];
                this.dataSource.data = newContact;

                console.log(this.dataSource.data);
            }
        );
    }

    registerNewContact() {
        this.zone.run(() => { this.router.navigate(['/record/person'], { relativeTo: this.route }); });
    }

    saveRelationship() {
        if (!this.formGroup.valid) {
            return;
        }
        const personRelation = {};
        personRelation['PersonId'] = this.contactPersonId;
        personRelation['PatientId'] = JSON.parse(localStorage.getItem('patientId'));
        personRelation['RelationshipTypeId'] = this.formGroup.get('relationship').value;
        personRelation['UserId'] = JSON.parse(localStorage.getItem('appUserId'));

        const patientAdd = this.registrationService.addPersonRelationship(personRelation);
        patientAdd.subscribe(
            (relationshipResult) => {
                if (this.partnerType != null) {
                    if (this.partnerType.partner == 1) {
                        this.zone.run(() => { this.router.navigate(['/hts/pns'], { relativeTo: this.route }); });
                    } else if (this.partnerType.family == 1) {
                        this.zone.run(() => { this.router.navigate(['/hts/family'], { relativeTo: this.route }); });
                    }
                }
            }
        );
    }
}

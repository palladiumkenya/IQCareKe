import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { InlineSearchComponent } from '../../../records/inline-search/inline-search.component';

@Component({
    selector: 'app-maternalhistory',
    templateUrl: './maternalhistory.component.html',
    styleUrls: ['./maternalhistory.component.css']
})
export class MaternalhistoryComponent implements OnInit {
    MaternalHistoryForm: FormGroup;

    motherstateOptions: any[] = [];
    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: LookupItemView[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];
    primarycaregiverOptions: any[] = [];


    isMotherRegistered: boolean = false;

    @Input('maternalhistoryOptions') maternalhistoryOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    public cccPattern = /^((?!(0))[0-9]{10})$/;

    constructor(private _formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private dialog: MatDialog,
        private heiservice: HeiService) { }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            motherregisteredinclinic: new FormControl('', [Validators.required]),
            stateofmother: new FormControl('', [Validators.required]),
            primarycaregiver: new FormControl('', [Validators.required]),
            nameofmother: new FormControl('', [Validators.required]),
            motherpersonid: new FormControl('0'),
            cccno: new FormControl('', [Validators.required]),
            pmtctheimotherreceivedrugs: new FormControl('', [Validators.required]),
            pmtctheimotherregimen: new FormControl('', [Validators.required]),
            otherspecify: new FormControl('', [Validators.required]),
            motheronartatinfantenrollment: new FormControl('', [Validators.required]),
            pmtctheimotherdrugsatinfantenrollment: new FormControl('', [Validators.required]),
            id: new FormControl('')
        });

        const {
            motherstateOptions,
            motherreceivedrugsOptions,
            heimotherregimenOptions,
            yesnoOptions,
            motherdrugsatinfantenrollmentOptions,
            primarycaregiverOptions } = this.maternalhistoryOptions[0];
        this.motherstateOptions = motherstateOptions;
        this.motherreceivedrugsOptions = motherreceivedrugsOptions;
        this.heimotherregimenOptions = heimotherregimenOptions;
        this.yesnoOptions = yesnoOptions;
        this.motherdrugsatinfantenrollmentOptions = motherdrugsatinfantenrollmentOptions;
        this.primarycaregiverOptions = primarycaregiverOptions;

        this.notify.emit(this.MaternalHistoryForm);

        if (this.isEdit) {
            this.loadMaternalHistory();
        }
    }

    loadMaternalHistory(): void {
        this.heiservice.getHeiDelivery(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    const isMotherRegistered = result[i].motherRegisteredId ? 'Yes' : 'No';
                    const yesNoOption = this.yesnoOptions.filter(obj => obj.itemName == isMotherRegistered);
                    this.MaternalHistoryForm.get('motherregisteredinclinic').setValue(yesNoOption[0].itemId);
                    this.MaternalHistoryForm.get('stateofmother').setValue(result[i].motherStatusId);
                    this.MaternalHistoryForm.get('primarycaregiver').setValue(result[i].primaryCareGiverID);
                    this.MaternalHistoryForm.get('nameofmother').setValue(result[i].motherName);
                    this.MaternalHistoryForm.get('motherpersonid').setValue(result[i].motherPersonId);
                    this.MaternalHistoryForm.get('cccno').setValue(result[i].motherCCCNumber);
                    this.MaternalHistoryForm.get('pmtctheimotherreceivedrugs').setValue(result[i].motherPMTCTDrugsId);
                    this.MaternalHistoryForm.get('pmtctheimotherregimen').setValue(result[i].motherPMTCTRegimenId);
                    this.MaternalHistoryForm.get('otherspecify').setValue(result[i].motherPMTCTRegimenOther);
                    this.MaternalHistoryForm.get('motheronartatinfantenrollment').setValue(result[i].motherArtInfantEnrolId);
                    this.MaternalHistoryForm.get('pmtctheimotherdrugsatinfantenrollment').setValue(result[i].motherArtInfantEnrolRegimenId);
                    this.MaternalHistoryForm.get('id').setValue(result[i].id);
                }
            },
            (error) => { },
            () => { }
        );
    }

    onMotherReceivedDrugsChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.MaternalHistoryForm.controls['otherspecify'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['otherspecify'].disable({ onlySelf: true });
        }

        if (event.isUserInput && event.source.selected && event.source.viewValue == 'HAART') {
            this.MaternalHistoryForm.controls['pmtctheimotherregimen'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['pmtctheimotherregimen'].disable();
        }
    }

    onMotherOnArtAtInfantEnrollmentChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.MaternalHistoryForm.controls['pmtctheimotherdrugsatinfantenrollment'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['pmtctheimotherdrugsatinfantenrollment'].disable();
        }
    }

    onMotherRegisteredInClinicChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.isMotherRegistered = true;
            // this.MaternalHistoryForm.controls.nameofmother.disable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            // this.MaternalHistoryForm.controls.nameofmother.enable({ onlySelf: false });
            this.isMotherRegistered = false;
        }
    }

    openDialog() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '85%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
        };


        const dialogRef = this.dialog.open(InlineSearchComponent, dialogConfig);


        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const firstName = data[0]['firstName'] ? data[0]['firstName'] : '';
                const middleName = data[0]['middleName'] ? data[0]['middleName'] : '';
                const lastName = data[0]['lastName'] ? data[0]['lastName'] : '';

                const mothernames = firstName + ' ' + middleName + ' ' + lastName;
                this.isMotherRegistered = false;
                this.MaternalHistoryForm.controls.nameofmother.setValue(mothernames);
                this.MaternalHistoryForm.controls.motherpersonid.setValue(data[0]['id']);
            }
        );
    }
}

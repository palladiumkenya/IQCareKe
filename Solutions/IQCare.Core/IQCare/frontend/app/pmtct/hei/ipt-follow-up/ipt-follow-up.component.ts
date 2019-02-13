import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IptClientWorkupComponent } from '../ipt-client-workup/ipt-client-workup.component';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { HeiService } from '../../_services/hei.service';
import { PatientIptWorkup } from '../../_models/hei/PatientIptWorkup';
import { PatientIpt } from '../../_models/hei/PatientIpt';
import * as moment from 'moment';

@Component({
    selector: 'app-ipt-follow-up',
    templateUrl: './ipt-follow-up.component.html',
    styleUrls: ['./ipt-follow-up.component.css']
})
export class IptFollowUpComponent implements OnInit {

    public title: string;
    public yesnoOptions: any[] = [];
    public IPTFollowupFormGroup: FormGroup;
    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    public maxDate: Date = moment().toDate();
    @Input('IPTFollowupOptions') IPTFollowupOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private heiService: HeiService,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<IptFollowUpComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'IPT Client Followup';
        this.yesnoOptions = data.yesNoOptions;
        this.patientId = data.patientId;
        this.userId = data.userId;
        this.patientMasterVisitId = data.patientMasterVisitId;
    }

    ngOnInit() {
        this.IPTFollowupFormGroup = this._formBuilder.group({
            IPTDueDate: new FormControl('', [Validators.required]),
            IPTDateCollected: new FormControl('', [Validators.required]),
            weight: new FormControl('', [Validators.required]),
            hepatoxicity: new FormControl('', [Validators.required]),
            hepatoxicityActionTaken: new FormControl('', [Validators.required]),
            peripheralNeoropathy: new FormControl('', [Validators.required]),
            peripheralNeoropathyActionTaken: new FormControl('', [Validators.required]),
            rash: new FormControl('', [Validators.required]),
            rashActionTaken: new FormControl('', [Validators.required]),
            adherenceMeasurement: new FormControl('', [Validators.required]),
            adherenceMeasurementActionTaken: new FormControl('', [Validators.required]),
        });

        /*const {
            yesnoOption
        } = this.IPTFollowupOptions[0];
        this.yesnoOptions = yesnoOption;*/
    }

    onSave(): void {

        const iptFollowup = {
            Id: 0,
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            IptDueDate: this.IPTFollowupFormGroup.controls['IPTDueDate'].value,
            IptDateCollected: this.IPTFollowupFormGroup.controls['IPTDateCollected'].value,
            Weight: this.IPTFollowupFormGroup.controls['weight'].value,
            Hepatotoxicity: this.IPTFollowupFormGroup.controls['hepatoxicity'].value.itemId,
            Peripheralneoropathy: this.IPTFollowupFormGroup.controls['peripheralNeoropathy'].value.itemId,
            Rash: this.IPTFollowupFormGroup.controls['rash'].value.itemId,
            AdheranceMeasurement: this.IPTFollowupFormGroup.controls['adherenceMeasurement'].value.itemId,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            HepatotoxicityAction: this.IPTFollowupFormGroup.controls['hepatoxicityActionTaken'].value,
            PeripheralneoropathyAction: this.IPTFollowupFormGroup.controls['peripheralNeoropathyActionTaken'].value,
            RashAction: this.IPTFollowupFormGroup.controls['rashActionTaken'].value,
            AdheranceMeasurementAction: this.IPTFollowupFormGroup.controls['adherenceMeasurementActionTaken'].value,
        } as PatientIpt;

        this.heiService.saveIpt(iptFollowup).subscribe(
            (result) => {
                this.snotifyService.success('Successfully saved client follow-up ', 'Ipt Client Follow-Up',
                    this.notificationService.getConfig());

                this.dialogRef.close();
            },
            (error) => {
                this.snotifyService.error('Error saving client follow-up ', 'Ipt Client Follow-Up',
                    this.notificationService.getConfig());
            },
            () => { }
        );

        this.dialogRef.close();
    }

    close(): void {
        this.dialogRef.close();
    }
}

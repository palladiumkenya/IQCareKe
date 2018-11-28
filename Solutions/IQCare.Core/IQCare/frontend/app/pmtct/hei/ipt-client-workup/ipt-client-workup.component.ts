import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { HeiService } from '../../_services/hei.service';
import { PatientIptWorkup } from '../../_models/hei/PatientIptWorkup';

@Component({
    selector: 'app-ipt-client-workup',
    templateUrl: './ipt-client-workup.component.html',
    styleUrls: ['./ipt-client-workup.component.css']
})
export class IptClientWorkupComponent implements OnInit {

    public IPTClientWorkupFormGroup: FormGroup;
    public title: string;
    public yesnoOptions: any[] = [];
    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;

    @Input('tbAssessmentOptions') tbAssessmentOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private herService: HeiService,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<IptClientWorkupComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'IPT Client Workup';
        this.yesnoOptions = data.yesNoOptions;
        this.patientId = data.patientId;
        this.userId = data.userId;
        this.patientMasterVisitId = data.patientMasterVisitId;
    }

    ngOnInit() {

        this.IPTClientWorkupFormGroup = this._formBuilder.group({
            yellowColouredUrine: new FormControl('', [Validators.required]),
            numbness: new FormControl('', [Validators.required]),
            yellowEyes: new FormControl('', [Validators.required]),
            tenderness: new FormControl('', [Validators.required]),
            liverFunctionTest: new FormControl('', [Validators.required]),
            startIpt: new FormControl('', [Validators.required]),
            dateIPTStarted: new FormControl('', [Validators.required])
        });

    }

    onSave(): void {

        const patientIptWorkup = {
            Id: 0,
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            YellowColouredUrine: this.IPTClientWorkupFormGroup.controls['yellowColouredUrine'].value.itemId,
            Numbness: this.IPTClientWorkupFormGroup.controls['numbness'].value.itemId,
            YellownessOfEyes: this.IPTClientWorkupFormGroup.controls['yellowEyes'].value.itemId,
            AbdominalTenderness: this.IPTClientWorkupFormGroup.controls['tenderness'].value.itemId,
            LiverFunctionTests: this.IPTClientWorkupFormGroup.controls['liverFunctionTest'].value.itemId,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            StartIpt: this.IPTClientWorkupFormGroup.controls['startIpt'].value.itemId,
            IptStartDate: this.IPTClientWorkupFormGroup.controls['dateIPTStarted'].value,
            IptRegimen: 0
        } as PatientIptWorkup;

        this.herService.saveIptWorkup(patientIptWorkup).subscribe(
            (result) => {
                this.snotifyService.success('Successfully saved client workup ', 'Ipt Client Workup',
                    this.notificationService.getConfig());

                this.dialogRef.close();
            },
            (error) => {
                this.snotifyService.error('Error saving client workup ' + error, 'Ipt Client Workup',
                    this.notificationService.getConfig());
            },
            () => { }
        );
    }

    close(): void {
        this.dialogRef.close();
    }
}

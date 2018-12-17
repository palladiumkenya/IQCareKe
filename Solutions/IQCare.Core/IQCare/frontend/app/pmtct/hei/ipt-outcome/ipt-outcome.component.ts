import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IptClientWorkupComponent } from '../ipt-client-workup/ipt-client-workup.component';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { HeiService } from '../../_services/hei.service';
import { PatientIptOutcome } from '../../_models/hei/PatientIptOutcome';

@Component({
    selector: 'app-ipt-outcome',
    templateUrl: './ipt-outcome.component.html',
    styleUrls: ['./ipt-outcome.component.css']
})
export class IptOutcomeComponent implements OnInit {
    public IPTClientOutcomeFormGroup: FormGroup;
    public title: string;
    public yesnoOptions: any[] = [];
    public iptOutcomeOptions: any[] = [];
    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;

    @Input('tbAssessmentOptions') tbAssessmentOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private heiService: HeiService,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<IptClientWorkupComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'IPT Client Outcome';
        this.yesnoOptions = data.yesNoOptions;
        this.iptOutcomeOptions = data.iptOutcomeOptions;
        this.patientId = data.patientId;
        this.userId = data.userId;
        this.patientMasterVisitId = data.patientMasterVisitId;
    }


    ngOnInit() {
        this.IPTClientOutcomeFormGroup = this._formBuilder.group({
            iptEvent: new FormControl('', [Validators.required]),
            reasonsDiscontinued: new FormControl('', [Validators.required])
        });
        this.IPTClientOutcomeFormGroup.get('reasonsDiscontinued').disable({ onlySelf: false });
    }

    onIPTEventChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Discontinued') {
            this.IPTClientOutcomeFormGroup.get('iptEvent').enable({ onlySelf: true });
        } else {
            this.IPTClientOutcomeFormGroup.controls['iptEvent'].disable({ onlySelf: true });
        }
    }

    onSave() {
        const iptOutcome = {
            Id: 0,
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            IptEvent: this.IPTClientOutcomeFormGroup.controls['iptEvent'].value.itemId,
            ReasonForDiscontinuation: this.IPTClientOutcomeFormGroup.controls['reasonsDiscontinued'].value,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date()
        } as PatientIptOutcome;

        this.heiService.saveIptOutcome(iptOutcome).subscribe(
            (result) => {
                this.snotifyService.success('Successfully saved client outcome ', 'Ipt Client Outcome',
                    this.notificationService.getConfig());

                this.dialogRef.close();
            },
            (error) => {
                this.snotifyService.error('Error saving client outcome ' + error, 'Ipt Client Outcome',
                    this.notificationService.getConfig());
            },
            () => { }
        );
    }

    close(): void {
        this.dialogRef.close();
    }
}

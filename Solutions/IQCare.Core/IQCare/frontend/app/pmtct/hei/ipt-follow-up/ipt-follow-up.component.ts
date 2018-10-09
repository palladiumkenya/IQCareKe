import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {IptClientWorkupComponent} from '../ipt-client-workup/ipt-client-workup.component';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'app-ipt-follow-up',
    templateUrl: './ipt-follow-up.component.html',
    styleUrls: ['./ipt-follow-up.component.css']
})
export class IptFollowUpComponent implements OnInit {

    public title: string;
    public yesnoOptions: any[] = [];
    public IPTFollowupFormGroup: FormGroup;
    @Input('IPTFollowupOptions') IPTFollowupOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
                private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                public dialogRef: MatDialogRef<IptFollowUpComponent>,
                @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'IPT Client Followup';
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

        const {
            yesnoOption
        } = this.IPTFollowupOptions[0];
        this.yesnoOptions = yesnoOption;
    }
}

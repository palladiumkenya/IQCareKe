import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import * as moment from 'moment';

@Component({
    selector: 'app-discharge',
    templateUrl: './discharge.component.html',
    styleUrls: ['./discharge.component.css']
})
export class DischargeComponent implements OnInit {
    dischargeFormGroup: FormGroup;
    @Input() dischargeOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    public deliveryStateOptions: any[] = [];
    public referralOptions: any[] = [];
    public yesnoOptions: any[] = [];
    public maxDate: Date = moment().toDate();

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.dischargeFormGroup = this.formBuilder.group({
            dischargeDate: new FormControl('', [Validators.required]),
            babyStatus: new FormControl('', [Validators.required])

        });
        const {
            deliveryStates,
            referrals,
            yesNos
        } = this.dischargeOptions[0];
        this.yesnoOptions = yesNos;
        this.deliveryStateOptions = deliveryStates;
        this.referralOptions = referrals;

        this.notify.emit(this.dischargeFormGroup);
    }

}

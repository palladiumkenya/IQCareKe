import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';

@Component({
    selector: 'app-delivery-maternity',
    templateUrl: './delivery-maternity.component.html',
    styleUrls: ['./delivery-maternity.component.css']
})
export class DeliveryMaternityComponent implements OnInit {

    deliveryFormGroup: FormGroup;
    @Input() diagnosisOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    public deliveryModeOptions: any[] = [];
    public bloodlossOptions: any[] = [];
    public motherStateOptions: any[] = [];
    public yesnoOptions: any[] = [];

    constructor(private formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
    }

    ngOnInit() {
        this.deliveryFormGroup = this.formBuilder.group({
            ancVisits: new FormControl('', [Validators.required]),
            deliveryDate: new FormControl('', [Validators.required]),
            gestationAtBirth: new FormControl('', [Validators.required]),
            deliveryTime: new FormControl('', [Validators.required]),
            labourDuration: new FormControl('', [Validators.required]),

            deliveryMode: new FormControl('', [Validators.required]),
            bloodLoss: new FormControl('', [Validators.required]),
            bloodLossCount: new FormControl('', [Validators.required]),
            deliveryCondition: new FormControl('', [Validators.required]),

            placentaComplete: new FormControl('', [Validators.required]),
            maternalDeathsAudited: new FormControl('', [Validators.required]),
            auditDate: new FormControl('', [Validators.required]),
            deliveryComplications: new FormControl('', [Validators.required]),
            deliveryComplicationNotes: new FormControl('', [Validators.required]),
            deliveryConductedBy: new FormControl('', [Validators.required])
        });

        const {
            deliveryModes,
            bloodLoss,
            motherStates,
            yesNos
        } = this.diagnosisOptions[0];
        this.deliveryModeOptions = deliveryModes;
        this.bloodlossOptions = bloodLoss;
        this.motherStateOptions = motherStates;
        this.yesnoOptions = yesNos;
    }

}

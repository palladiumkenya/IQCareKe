import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import * as moment from 'moment';

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
    public deliveryDate: Date;

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

        this.deliveryFormGroup.controls['bloodLossCount'].disable({ onlySelf: true });
        this.deliveryFormGroup.get('maternalDeathsAudited').disable({ onlySelf: true });
        this.deliveryFormGroup.get('auditDate').disable({ onlySelf: true });
        this.deliveryFormGroup.get('deliveryComplicationNotes').disable({ onlySelf: true });




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

    public onDeliveryDateChange() {
        this.deliveryDate = this.deliveryFormGroup.controls['deliveryDate'].value;


        const now = moment(new Date());
        const gestation = moment.duration(now.diff( this.deliveryDate)).asWeeks().toFixed(1);
        this.deliveryFormGroup.controls['gestationAtBirth'].setValue(gestation + ' weeks');

        this.deliveryFormGroup.controls['gestationAtBirth'].disable({ onlySelf: true });
    }

    onBloodLossChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'None') {
            this.deliveryFormGroup.get('bloodLossCount').disable({ onlySelf: true });
        } else {
            this.deliveryFormGroup.get('bloodLossCount').enable({ onlySelf: true });
        }
    }

    onChangeDeliveryConditions(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Dead') {
            this.deliveryFormGroup.get('maternalDeathsAudited').enable({ onlySelf: true });
            this.deliveryFormGroup.get('auditDate').enable({ onlySelf: true });
        } else {
            this.deliveryFormGroup.get('maternalDeathsAudited').disable({ onlySelf: true });
            this.deliveryFormGroup.get('auditDate').disable({ onlySelf: true });
        }
    }

    onDeliveryComplicationsChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.deliveryFormGroup.get('deliveryComplicationNotes').enable({ onlySelf: true });
        } else {
            this.deliveryFormGroup.get('deliveryComplicationNotes').disable({ onlySelf: true });
        }
    }


}

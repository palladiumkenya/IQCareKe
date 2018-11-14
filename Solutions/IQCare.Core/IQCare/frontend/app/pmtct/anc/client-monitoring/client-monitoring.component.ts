import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs/index';
import {SnotifyService} from 'ng-snotify';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {ClientMonitoringEmitter} from '../../emitters/ClientMonitoringEmitter';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {NotificationService} from '../../../shared/_services/notification.service';

export interface Options {
    value: string;
    viewValue: string;
}

@Component({
    selector: 'app-client-monitoring',
    templateUrl: './client-monitoring.component.html',
    styleUrls: ['./client-monitoring.component.css']
})
export class ClientMonitoringComponent implements OnInit {

    private lookupItemView$: Subscription;
    public TBScreeningOptions: any[] = [];
    public whoStagOptions: any[] = [];
    public YesNoNaOptions: any[] = [];
    public cacxMethodOptions: any[] = [];
    public cacxResultOptions: any[] = [];
    public YesNoOptions: any[] = [];

    public clientMonitoringFormGroup: FormGroup;
    @Output() nextStep = new EventEmitter<ClientMonitoringEmitter>();
    @Input() clientMonitoring: ClientMonitoringEmitter;
    @Input() clientMonitoringOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public clientMonitoringData: ClientMonitoringEmitter;

    constructor(private fb: FormBuilder, private lookupItemService: LookupItemService, private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
    }

    ngOnInit() {
        this.clientMonitoringFormGroup = this.fb.group({
            WhoStage: ['', Validators.required],
            viralLoadSampleTaken: ['', Validators.required],
            screenedForTB: ['', Validators.required],
            cacxScreeningDone: ['', Validators.required],
            cacxMethod: ['', Validators.required],
            cacxResult: ['', Validators.required],
            cacxComments: ['', Validators.required]
        });

        const {
            whoStageOptions,
            yesnoOptions,
            yesNoNaOptions,
            tbScreeningOptions,
            cacxMethodOptions,
            cacxResultOptions
        } = this.clientMonitoringOptions[0];
        this.whoStagOptions = whoStageOptions;
        this.YesNoNaOptions = yesNoNaOptions;
        this.TBScreeningOptions = tbScreeningOptions;
        this.cacxResultOptions = cacxResultOptions;
        this.cacxMethodOptions = cacxMethodOptions;
        this.YesNoOptions = yesnoOptions;
        console.log(this.cacxMethodOptions);

      /*  this.getLookupItems('TBScreeningPMTCT', this.TBOptions);
        this.getLookupItems('WHOStage', this.WHOStagOptions);
        this.getLookupItems('YesNoNA', this.YesNoNa);
        this.getLookupItems('CacxMethod', this.CaCxMethods);
        this.getLookupItems('CacxResult', this.CacxResults);
        this.getLookupItems('YesNo', this.YesNos);*/
      this.notify.emit(this.clientMonitoringFormGroup);

    }

    public testingFunc() {
        console.log('');
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItemView$ = this.lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        _options.push({'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public moveNextStep() {
        console.log(this.clientMonitoringFormGroup.value);

        this.clientMonitoringData = {
            WhoStage: parseInt(this.clientMonitoringFormGroup.controls['WhoStage'].value, 10),
            viralLoadSampleTaken: parseInt(this.clientMonitoringFormGroup.controls['viralLoadSampleTaken'].value, 10),
            screenedForTB: parseInt(this.clientMonitoringFormGroup.controls['screenedForTB'].value, 10),
            cacxScreeningDone: parseInt(this.clientMonitoringFormGroup.controls['cacxScreeningDone'].value, 10),
            cacxMethod: parseInt(this.clientMonitoringFormGroup.controls['cacxMethod'].value, 10),
            cacxResult: parseInt(this.clientMonitoringFormGroup.controls['cacxResult'].value, 10),
            cacxComments: this.clientMonitoringFormGroup.controls['cacxComments'].value,
        };
        console.log(this.clientMonitoringData);
        this.nextStep.emit(this.clientMonitoringData);
        this.notify.emit(this.clientMonitoringFormGroup);
    }

}

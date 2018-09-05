import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {Subscription} from 'rxjs/index';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {HIVTestingEmitter} from '../emitters/HIVTestingEmitter';
import {ClientMonitoringEmitter} from '../emitters/ClientMonitoringEmitter';
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
    public TBOptions: any[] = [];
    public WHOStagOptions: any[] = [];
    public YesNoNa: any[] = [];
    public CaCxMethods: any[] = [];
    public CacxResults: any[] = [];
    public YesNos: any[] = [];
    public clientMonitoringFormGroup: FormGroup;
    @Output() nextStep = new EventEmitter <ClientMonitoringEmitter> ();
    @Input() clientMonitoring: ClientMonitoringEmitter;
    public clientMonitoringData: ClientMonitoringEmitter;

  constructor(private fb: FormBuilder , private lookupItemService: LookupItemService, private snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

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
      this.getLookupItems('TBScreeningPMTCT', this.TBOptions);
      this.getLookupItems('WHOStage', this.WHOStagOptions);
      this.getLookupItems('YesNoNA', this.YesNoNa);
      this.getLookupItems('CacxMethod', this.CaCxMethods);
      this.getLookupItems('CacxResult', this.CacxResults);
      this.getLookupItems('YesNo', this.YesNos);

  }

  public testingFunc() {
        console.log('');
    }

    public getLookupItems(groupName: string , _options: any[]) {
        this.lookupItemView$ = this.lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
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
            WhoStage : parseInt(this.clientMonitoringFormGroup.controls['WhoStage'].value, 10),
            viralLoadSampleTaken: parseInt(this.clientMonitoringFormGroup.controls['viralLoadSampleTaken'].value, 10 ),
            screenedForTB: parseInt(this.clientMonitoringFormGroup.controls['screenedForTB'].value, 10 ),
            cacxScreeningDone: parseInt(this.clientMonitoringFormGroup.controls['cacxScreeningDone'].value, 10 ),
            cacxMethod: parseInt(this.clientMonitoringFormGroup.controls['cacxMethod'].value, 10 ),
            cacxResult: parseInt(this.clientMonitoringFormGroup.controls['cacxResult'].value, 10 ),
            cacxComments: this.clientMonitoringFormGroup.controls['cacxComments'].value,
        };
        console.log(this.clientMonitoringData);
        this.nextStep.emit(this.clientMonitoringData);
    }

}

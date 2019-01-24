import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs/index';
import {SnotifyService} from 'ng-snotify';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {ClientMonitoringEmitter} from '../../emitters/ClientMonitoringEmitter';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {NotificationService} from '../../../shared/_services/notification.service';
import {AncService} from '../../_services/anc.service';

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
    private patientScreening$: Subscription;
    private patientwhoStage$: Subscription;

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
    @Input() isEdit: boolean;
    @Input() patientId: number;
    @Input() patientMasterVisitId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public clientMonitoringData: ClientMonitoringEmitter;

    constructor(private fb: FormBuilder, private lookupItemService: LookupItemService, private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private ancService: AncService) {
    }

    ngOnInit() {
        this.clientMonitoringFormGroup = this.fb.group({
            WhoStage: ['', Validators.required],
            viralLoadSampleTaken: ['', Validators.required],
            screenedForTB: ['', Validators.required],
            cacxScreeningDone: ['', Validators.required],
            cacxMethod: ['', Validators.required],
            cacxResult: ['', Validators.required],
            cacxComments: ['n/a', Validators.required]
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

        this.clientMonitoringFormGroup.controls['cacxMethod'].disable({ onlySelf: true });
        this.clientMonitoringFormGroup.controls['cacxResult'].disable({ onlySelf: true });
        this.clientMonitoringFormGroup.controls['cacxComments'].disable({ onlySelf: true });

      /*  this.getLookupItems('TBScreeningPMTCT', this.TBOptions);
        this.getLookupItems('WHOStage', this.WHOStagOptions);
        this.getLookupItems('YesNoNA', this.YesNoNa);
        this.getLookupItems('CacxMethod', this.CaCxMethods);
        this.getLookupItems('CacxResult', this.CacxResults);
        this.getLookupItems('YesNo', this.YesNos);*/
      this.notify.emit(this.clientMonitoringFormGroup);

      if (this.isEdit) {
          this.getPatientScreeningInfo(this.patientId, this.patientMasterVisitId);
          this.getPatientWhoStageInfo(this.patientId, this.patientMasterVisitId);
      }else {
          this.getPatientWhoStageInfoCurrent(this.patientId);
          this.getPatientScreeningInfoByPatientId(this.patientId);
      }

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

    public oncacxScreeningChange(event) {

        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.clientMonitoringFormGroup.controls['cacxMethod'].enable({ onlySelf: true });
            this.clientMonitoringFormGroup.controls['cacxResult'].enable({ onlySelf: true });
            this.clientMonitoringFormGroup.controls['cacxComments'].enable({ onlySelf: true });
        } else {
            this.clientMonitoringFormGroup.controls['cacxMethod'].disable({ onlySelf: true });
            this.clientMonitoringFormGroup.controls['cacxResult'].disable({ onlySelf: true });
            this.clientMonitoringFormGroup.controls['cacxComments'].disable({ onlySelf: true });
        }
    }

    public getPatientWhoStageInfoCurrent(patientId: number) {
        this.patientwhoStage$ = this.ancService.getPatientWhoStageInfoCurrent(patientId)
            .subscribe(
                p => {
                    console.log('patientwho');
                    console.log(p);
                    console.log(p['whoStage']);
                    if (p) {
                        this.clientMonitoringFormGroup.get('WhoStage').setValue(p['whoStage']);
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading patient who stage ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public getPatientWhoStageInfo(patientId: number, patientMasterVisitId: number) {
        this.patientwhoStage$ = this.ancService.getPatientWhoStageInfo(patientId, patientMasterVisitId)
            .subscribe(
                p => {
                    console.log('patientwho');
                    console.log(p);
                    console.log(p['whoStage']);
                    if (p) {
                        this.clientMonitoringFormGroup.get('WhoStage').setValue(p['whoStage']);
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading patient who stage ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public getPatientScreeningInfoByPatientId(patientId: number) {
        this.patientScreening$ = this.ancService.getPatientScreeningInfoByPatientId(patientId)
            .subscribe(
                p => {
                    console.log('patientscreening');
                    console.log(p);
                    const screening = p;
                    if (p) {
                        const cacx = screening.filter(obj => obj.screeningType == 'CaCxScreening');
                        const tb = screening.filter(obj => obj.screeningType == 'TBScreeningPMTCT');
                        const vl = screening.filter(obj => obj.screeningType == 'ViralLoadSampleTaken');

                        console.log(cacx);
                        console.log(cacx[0]['screeningDone']);

                        if (vl.length > 0) {
                            this.clientMonitoringFormGroup.get('screenedForTB').setValue(vl[0]['screeningValueId']);
                        }

                        if (tb.length > 0) {
                            this.clientMonitoringFormGroup.get('screenedForTB').setValue(cacx[0]['screeningDone']);
                        }
                        if (cacx.length > 0) {
                            this.clientMonitoringFormGroup.get('cacxScreeningDone').setValue(cacx[0]['screeningDone']);
                            this.clientMonitoringFormGroup.get('cacxMethod').setValue(cacx[0]['screeningCategoryId']);
                            this.clientMonitoringFormGroup.get('cacxResult').setValue(cacx[0]['screeningValueId']);
                            this.clientMonitoringFormGroup.get('cacxComments').setValue(cacx[0]['comment']);
                        }

                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading patient screening ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public getPatientScreeningInfo(patientId: number, patientMasterVisitId: number) {
        this.patientScreening$ = this.ancService.getPatientScreeningInfo(patientId, patientMasterVisitId)
            .subscribe(
                p => {
                    console.log('patientscreening');
                    console.log(p);
                    const screening = p;
                    if (p) {
                        const cacx = screening.filter(obj => obj.screeningType == 'CaCxScreening');
                        const tb = screening.filter(obj => obj.screeningType == 'TBScreeningPMTCT');
                        const vl = screening.filter(obj => obj.screeningType == 'ViralLoadSampleTaken');

                        console.log(cacx);
                        console.log(cacx[0]['screeningDone']);

                        if (vl.length > 0) {
                            this.clientMonitoringFormGroup.get('screenedForTB').setValue(vl[0]['screeningValueId']);
                        }

                        if (tb.length > 0) {
                            this.clientMonitoringFormGroup.get('screenedForTB').setValue(cacx[0]['screeningDone']);
                        }
                        if (cacx.length > 0) {
                            this.clientMonitoringFormGroup.get('cacxScreeningDone').setValue(cacx[0]['screeningDone']);
                            this.clientMonitoringFormGroup.get('cacxMethod').setValue(cacx[0]['screeningCategoryId']);
                            this.clientMonitoringFormGroup.get('cacxResult').setValue(cacx[0]['screeningValueId']);
                            this.clientMonitoringFormGroup.get('cacxComments').setValue(cacx[0]['comment']);
                        }

                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading patient screening ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }
}

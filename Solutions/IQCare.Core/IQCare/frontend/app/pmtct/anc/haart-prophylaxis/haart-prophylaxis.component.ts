import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';
import {ClientMonitoringEmitter} from '../../emitters/ClientMonitoringEmitter';
import {HAARTProphylaxisEmitter} from '../../emitters/HAARTProphylaxisEmitter';
import {ActivatedRoute} from '@angular/router';
import {ChronicIllnessEmitter} from '../../emitters/ChronicIllnessEmitter';
import {PatientChronicIllness} from '../../_models/PatientChronicIllness';
import * as moment from 'moment';
import {AncService} from '../../_services/anc.service';

export interface Options {
    value: string;
    viewValue: string;
}

@Component({
    selector: 'app-haart-prophylaxis',
    templateUrl: './haart-prophylaxis.component.html',
    styleUrls: ['./haart-prophylaxis.component.css']
})
export class HaartProphylaxisComponent implements OnInit {

    public HaartProphylaxisFormGroup: FormGroup;
    public yesnonaOptions: any[] = [];
    public chronicIllnessOptions: any[] = [];
    public YesNoOptions: any[] = [];

    lookupItemView$: Subscription;
    drugAdministration$: Subscription;
    @Output() nextStep = new EventEmitter<HAARTProphylaxisEmitter>();
    @Input() HaartProphylaxis: ClientMonitoringEmitter;
    @Input() haartProphylaxisOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Output() notify: EventEmitter<object> = new EventEmitter<object>();
    public HaartProphylaxisData: HAARTProphylaxisEmitter;
    public chronicIllness: ChronicIllnessEmitter[] = [];
    public patientchronicIllnessData: PatientChronicIllness[] = [];

    public personId: number;
   // public patientMasterVisitId: number;
    public serviceAreaId: number;
  //  public patientId: number;
    public userId: number;
    public isDisabled: boolean = true ;

    public maxDate: Date = moment().toDate();

    constructor(private route: ActivatedRoute, private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
                private  snotifyService: SnotifyService,
                private notificationService: NotificationService, private ancService: AncService) {
    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.personId = params['personId'];
        });

        this.route.params.subscribe(params => {
            this.serviceAreaId = params['serviceAreaId'];
        });

        this.route.params.subscribe(params => {
            this.patientId = params['patientId'];
        });

        this.userId = this.userId = JSON.parse(localStorage.getItem('appUserId'));


        this.HaartProphylaxisFormGroup = this._formBuilder.group({
            onArvBeforeANCVisit: ['', Validators.required],
            startedHaartANC: ['', Validators.required],
            cotrimoxazole: ['', Validators.required],
            aztFortheBaby: ['', Validators.required],
            nvpForBaby: ['', Validators.required],
            illness: ['', Validators.required],
            otherIllness: ['', Validators.required],
            onSetDate: ['', Validators.required],
            currentTreatment: ['', Validators.required],
            dose: ['', Validators.required]
        });

        this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['currentTreatment'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['dose'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['onSetDate'].disable({ onlySelf: true });

       // this.isDisabled = true;


        const {
            yesnoOptions,
            yesNoNaOptions,
            chronicIllnessOptions,
        } = this.haartProphylaxisOptions[0];
        this.yesnonaOptions = yesnoOptions;
        this.yesnonaOptions = yesNoNaOptions;
        this.chronicIllnessOptions = chronicIllnessOptions;

        /* this.getLookupItems('YesNoNa', this.yesnonas);
         this.getLookupItems('ChronicIllness', this.chronics);
         this.getLookupItems('YesNo', this.YesNos); */
        this.notify.emit({'form': this.HaartProphylaxisFormGroup, 'illness_data': this.chronicIllness });
        if (this.isEdit) {
            this.getPatientDrugAdministrationInfo(this.patientId);
            this.getPatientChronicIllnessInfo(this.patientId);
        }
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
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
        console.log(this.HaartProphylaxisFormGroup.value);

        for (let i = 0; i < this.chronicIllness.length; i++) {
            this.patientchronicIllnessData.push(
                {
                    Id: 0,
                    PatientId: parseInt(this.patientId.toString(), 10),
                    PatientMasterVisitId: parseInt(this.patientMasterVisitId.toString(), 10),
                    ChronicIllness: this.chronicIllness[i]['chronicIllnessId'],
                    Treatment: this.chronicIllness[i]['currentTreatment'],
                    Dose: parseInt(this.chronicIllness[i]['dose'].toString(), 10),
                    DeleteFlag: false,
                    OnsetDate: this.chronicIllness[i]['onSetDate'],
                    Active: 0,
                    CreateBy: this.userId
                });
        }


        this.HaartProphylaxisData = {
            onArvBeforeANCVisit: parseInt(this.HaartProphylaxisFormGroup.controls['onArvBeforeANCVisit'].value, 10),
            startedHaartANC: parseInt(this.HaartProphylaxisFormGroup.controls['startedHaartANC'].value, 10),
            cotrimoxazole: parseInt(this.HaartProphylaxisFormGroup.controls['cotrimoxazole'].value, 10),
            aztFortheBaby: parseInt(this.HaartProphylaxisFormGroup.controls['aztFortheBaby'].value, 10),
            nvpForBaby: parseInt(this.HaartProphylaxisFormGroup.controls['nvpForBaby'].value, 10),
            illness: parseInt(this.HaartProphylaxisFormGroup.controls['illness'].value, 10),
            otherIllness: parseInt(this.HaartProphylaxisFormGroup.controls['otherIllness'].value, 10),
            chronicIllness: this.patientchronicIllnessData
        };
        console.log(this.HaartProphylaxisData);
        this.nextStep.emit(this.HaartProphylaxisData);
        this.notify.emit(this.HaartProphylaxisFormGroup);
    }

    public AddOtherIllness() {
        const illness = this.HaartProphylaxisFormGroup.controls['illness'].value.itemName;
        const illnessId = parseInt(this.HaartProphylaxisFormGroup.controls['illness'].value.itemId, 10);

        if (this.chronicIllness.filter(x => x.chronicIllness === illness).length > 0) {
            this.snotifyService.warning('' + illness + ' exists', 'Counselling', this.notificationService.getConfig());
        } else {
            this.chronicIllness.push({
                chronicIllness: illness,
                chronicIllnessId: illnessId,
                onSetDate: this.HaartProphylaxisFormGroup.controls['onSetDate'].value,
                currentTreatment: this.HaartProphylaxisFormGroup.controls['currentTreatment'].value,
                dose: parseInt(this.HaartProphylaxisFormGroup.controls['dose'].value.toString(), 10)
            });
        }
        console.log(this.chronicIllness);
    }

    public onChangeOtherIllness(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.HaartProphylaxisFormGroup.controls['illness'].enable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['currentTreatment'].enable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['dose'].enable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['illness'].enable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['onSetDate'].enable({ onlySelf: true });
            this.isDisabled = false;
        } else {
            this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['currentTreatment'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['dose'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['onSetDate'].disable({ onlySelf: true });
            this.isDisabled = true;
        }

      /* const OtherIllness = this.HaartProphylaxisFormGroup.controls['otherIllness'].value.itemName;
        if (OtherIllness == 'Yes') {

            this.isDisabled = false;
        } else {
            this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['onsetDate'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['currentTreatment'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['Dose'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
            this.isDisabled = true;
        } */
    }



    public removeRow(idx) {
        this.chronicIllness.splice(idx, 1);
    }

    public onARVBeforeFirstANC(event) {
       console.log('yesno options');
        console.log(this.YesNoOptions);
        const no = this.yesnonaOptions.filter(x => x.itemName = 'No');
        // console.log(option);
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
                this.HaartProphylaxisFormGroup.get('startedHaartANC').setValue(no[0]['itemId']);
        } else {

        }
    }

    public getPatientDrugAdministrationInfo(patientId: number) {
        this.drugAdministration$ = this.ancService.getPatientDrugAdministrationInfo(patientId)
            .subscribe(
                p => {
                    console.log('drug');
                    console.log(p);
                    const drugAdministration = p;

                    if (drugAdministration) {
                        const firstAncVisit = drugAdministration.filter(x => x.strDrugAdministered == 'On ARV before 1st ANC Visit');
                        const haartAnc = drugAdministration.filter(x => x.strDrugAdministered == 'Started HAART in ANC');
                        const cotrim = drugAdministration.filter(x => x.strDrugAdministered == 'Cotrimoxazole');
                        const aztBaby = drugAdministration.filter(x => x.strDrugAdministered == 'AZT for the baby dispensed');
                        const nvpBaby = drugAdministration.filter(x => x.strDrugAdministered ==  'NVP for baby dispensed');

                        console.log(firstAncVisit);
                        console.log(haartAnc);
                        console.log(cotrim);
                        console.log(aztBaby);
                        console.log(nvpBaby);
                        console.log('end drugs admin');

                        if (firstAncVisit.length > 0) {
                            this.HaartProphylaxisFormGroup.get('onArvBeforeANCVisit').setValue(firstAncVisit[0]['value']);
                        }
                        if (haartAnc.length > 0) {
                            this.HaartProphylaxisFormGroup.get('startedHaartANC').setValue(haartAnc[0]['value']);
                        }
                        if (cotrim.length > 0) {
                            this.HaartProphylaxisFormGroup.get('cotrimoxazole').setValue(cotrim[0]['value']);
                        }
                        if (aztBaby.length > 0) {
                            this.HaartProphylaxisFormGroup.get('aztFortheBaby').setValue(aztBaby[0]['value']);
                        }
                        if (nvpBaby.length > 0) {
                            this.HaartProphylaxisFormGroup.get('nvpForBaby').setValue(nvpBaby[0]['value']);
                        }
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

    public getPatientChronicIllnessInfo(patientId: number) {
        this.drugAdministration$ = this.ancService.getPatientChronicIllnessInfo(patientId)
            .subscribe(
                p => {

                    const chronic = p;
                    console.log('chronic ');
                    console.log(chronic);
                    if (chronic.length > 0) {
                        for (let i = 0; i < chronic.length; i ++) {
                            this.chronicIllness.push({
                                chronicIllness: chronic[i]['chronicIllness'],
                                chronicIllnessId: chronic[i]['chronicIllnessId'],
                                onSetDate: chronic[i]['onsetDate'],
                                currentTreatment: chronic[i]['treatment'],
                                dose: chronic[i]['dose']
                            });
                        }
                    }

                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading patient Chronic Illness ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }


}

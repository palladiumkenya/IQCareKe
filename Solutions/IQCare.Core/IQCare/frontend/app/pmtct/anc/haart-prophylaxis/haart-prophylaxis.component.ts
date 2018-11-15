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
    @Output() nextStep = new EventEmitter<HAARTProphylaxisEmitter>();
    @Input() HaartProphylaxis: ClientMonitoringEmitter;
    @Input() haartProphylaxisOptions: any[] = [];
    @Output() notify: EventEmitter<object> = new EventEmitter<object>();
    public HaartProphylaxisData: HAARTProphylaxisEmitter;
    public chronicIllness: ChronicIllnessEmitter[] = [];
    public patientchronicIllnessData: PatientChronicIllness[] = [];

    public personId: number;
    public patientMasterVisitId: number;
    public serviceAreaId: number;
    public patientId: number;
    public userId: number;
    public isDisabled: boolean = false ;

    public maxDate: Date = moment().toDate();

    constructor(private route: ActivatedRoute, private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
                private  snotifyService: SnotifyService,
                private notificationService: NotificationService) {
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

       /* this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['currentTreatment'].disable({ onlySelf: true });
        this.HaartProphylaxisFormGroup.controls['dose'].disable({ onlySelf: true });

        this.isDisabled = true; */

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
                    DeleteFlag: 0,
                    OnsetDate: this.chronicIllness[i]['onSetDate'],
                    Active: false,
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
            this.HaartProphylaxisFormGroup.controls['onsetDate'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['currentTreatment'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['Dose'].disable({ onlySelf: true });
            this.HaartProphylaxisFormGroup.controls['illness'].disable({ onlySelf: true });
            this.isDisabled = true;
        } else {

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

}

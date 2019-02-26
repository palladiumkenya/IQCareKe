import { Component, EventEmitter, Input, OnInit, Output, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { SnotifyService } from 'ng-snotify';

import { Subscription } from 'rxjs/index';
import { PreventiveServiceEmitter } from '../../emitters/PreventiveServiceEmitter';
import { PreventiveEmitter } from '../../emitters/PreventiveEmitter';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import * as moment from 'moment';
import { AncService } from '../../_services/anc.service';

export interface Options {
    value: string;
    viewValue: string;
}

@Component({
    selector: 'app-preventive-services',
    templateUrl: './preventive-services.component.html',
    styleUrls: ['./preventive-services.component.css']
})
export class PreventiveServicesComponent implements OnInit, OnDestroy {
    public PreventiveServicesFormGroup: FormGroup;
    lookupItemView$: Subscription;
    preventiveServicesOptions: any[] = [];
    public YesNoOptions: any[] = [];
    public YesNoNaOptions: any[] = [];
    public FinalResultOptions: any[] = [];
    public maxDate: Date = moment().toDate();
    public minDate: Date;
    public preventiveService$: Subscription;
    public partnerTesting$: Subscription;

    @Output() nextStep = new EventEmitter<PreventiveServiceEmitter>();
    @Input() preventiveServices: PreventiveServiceEmitter;
    @Input() serviceFormOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    public preventiveServicesData: PreventiveServiceEmitter;
    public serviceData: PreventiveEmitter[] = [];


    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private ancService: AncService) {
    }

    ngOnInit() {
        this.PreventiveServicesFormGroup = this._formBuilder.group({
            preventiveServices: ['', Validators.required],
            dateGiven: ['', Validators.required],
            comments: ['', []],
            nextSchedule: ['', Validators.required],
            insecticideTreatedNet: ['', Validators.required],
            insecticideTreatedNetGivenDate: ['', Validators.required],
            antenatalExercise: ['', Validators.required],
            //   insecticideGivenDate: ['', Validators.required],
            PartnerTestingVisit: ['', Validators.required],
            finalHIVResult: ['', Validators.required]
        });

        this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').disable({ onlySelf: true });

        const {
            yesNoNaOptions,
            yesNoOptions,
            preventiveServicesOptions,
            hivFinalResultOptions
        } = this.serviceFormOptions[0];
        this.YesNoNaOptions = yesNoNaOptions;
        this.YesNoOptions = yesNoOptions;
        this.FinalResultOptions = hivFinalResultOptions;
        this.preventiveServicesOptions = preventiveServicesOptions;

        // console.log('preventive service' + hivFinalResultOptions[0].itemName);
        this.notify.emit({ 'form': this.PreventiveServicesFormGroup, 'preventive_service_data': this.serviceData });

        if (this.isEdit) {
            // this.getPatientPreventiveServiceInfo(this.patientId, this.patientMasterVisitId);

            this.PreventiveServicesFormGroup.get('preventiveServices').clearValidators();
            this.PreventiveServicesFormGroup.get('dateGiven').clearValidators();
            this.PreventiveServicesFormGroup.get('comments').clearValidators();
            this.PreventiveServicesFormGroup.get('nextSchedule').clearValidators();

            this.getPatientPartnerTestingInfo(this.patientId, this.patientMasterVisitId);
            this.getPatientPreventiveServiceInfoAll(this.patientId);
        } else {
            this.getPatientPreventiveServiceInfoAll(this.patientId);
        }
        /*this.getLookupItems('PreventiveService', this.services);
         this.getLookupItems('YesNo', this.yesnos);
          this.getLookupItems('HIVFinalResultsPMTCT', this.FinalResults);
          this.getLookupItems('YesNoNa', this.YesNoNas); */
    }

    /* public getLookupItems(groupName: string , _options: any[]) {
         this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
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

     }*/

    public moveNextStep() {
        console.log(this.PreventiveServicesFormGroup.value);

        this.preventiveServicesData = {
            preventiveService: this.serviceData,
            insecticideTreatedNet: parseInt(this.PreventiveServicesFormGroup.controls['insecticideTreatedNet'].value, 10),
            insecticideTreatedNetGivenDate: this.PreventiveServicesFormGroup.controls['insecticideTreatedNetGivenDate'].value,
            antenatalExercise: parseInt(this.PreventiveServicesFormGroup.controls['antenatalExercise'].value, 10),
            //  insecticideGivenDate: this.PreventiveServicesFormGroup.controls['insecticideGivenDate'].value,
            PartnerTestingVisit: parseInt(this.PreventiveServicesFormGroup.controls['PartnerTestingVisit'].value, 10),
            finalHIVResult: parseInt(this.PreventiveServicesFormGroup.controls['finalHIVResult'].value, 10),

        };
        console.log(this.preventiveServicesData);
        this.nextStep.emit(this.preventiveServicesData);
        this.notify.emit(this.PreventiveServicesFormGroup);
    }

    public addTopics() {

        const service = this.PreventiveServicesFormGroup.controls['preventiveServices'].value.itemName;
        const serviceId = this.PreventiveServicesFormGroup.controls['preventiveServices'].value.itemId;


        if (this.serviceData.filter(x => x.preventiveService === service).length > 0) {
            this.snotifyService.warning('' + service + ' exists', 'preventive Service', this.notificationService.getConfig());
        } else {
            this.serviceData.push({
                preventiveService: service,
                preventiveServiceId: serviceId,
                dateGiven: this.PreventiveServicesFormGroup.controls['dateGiven'].value,
                comments: this.PreventiveServicesFormGroup.controls['comments'].value,
                nextSchedule: this.PreventiveServicesFormGroup.controls['nextSchedule'].value
            });
        }
        console.log(this.serviceData);
    }

    public onPartnerTestingChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {

        } else {
            const final = this.FinalResultOptions.filter(x => x.itemName == 'N/A');
            this.PreventiveServicesFormGroup.get('finalHIVResult').setValue(final[0].itemId);
        }
    }

    public onGivenDateChange(event) {
        const givenDate: Date = moment(event.isUserInput && event.source.selected && event.source.viewValue).toDate();
        this.minDate = givenDate;
    }

    public onInsecticideTreatedNetGivenChange(event) {

        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').enable({ onlySelf: true });
        } else {
            this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').setValue('');
            this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').disable({ onlySelf: true });
        }
    }

    public removeRow(idx) {
        this.serviceData.splice(idx, 1);
    }

    public getPatientPreventiveServiceInfoAll(patientId: number) {
        this.preventiveService$ = this.ancService.getPatientPreventiveServiceInfo(patientId)
            .subscribe(
                p => {

                    const service = p;
                    console.log('preventiveservice ');
                    console.log(service);
                    // const myService = service.filter(x => x.patientMasterVisitId == patientMasterVisitId);

                    // console.log(myService);
                    if (service.length > 0) {
                        for (let i = 0; i < service.length; i++) {
                            this.serviceData.push({
                                preventiveService: service[i]['preventiveService'],
                                preventiveServiceId: service[i]['preventiveServiceId'],
                                dateGiven: service[i]['preventiveServiceDate'],
                                comments: service[i]['description'],
                                nextSchedule: service[i]['nextSchedule'],
                            });
                        }
                    }
                    const insecticide = service.filter(x => x.description == 'Insecticide treated nets given');
                    const exercise = service.filter(x => x.description == 'Antenatal exercise');

                    if (insecticide.length > 0) {
                        this.PreventiveServicesFormGroup.get('insecticideTreatedNet').setValue(insecticide[0]['preventiveServiceId']);
                        this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').setValue(insecticide[0]
                        ['preventiveServiceDate']);
                    }

                    if (exercise.length > 0) {
                        this.PreventiveServicesFormGroup.get('antenatalExercise').setValue(exercise[0]['preventiveServiceId']);
                    }
                    console.log(insecticide);
                    console.log(exercise);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading Preventive Services ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public getPatientPreventiveServiceInfo(patientId: number, patientMasterVisitId: number) {
        this.preventiveService$ = this.ancService.getPatientPreventiveServiceInfo(patientId)
            .subscribe(
                p => {

                    const service = p;
                    console.log('preventiveservice ');
                    console.log(service);
                    const myService = service.filter(x => x.patientMasterVisitId == patientMasterVisitId);

                    console.log(myService);
                    if (myService.length > 0) {
                        for (let i = 0; i < myService.length; i++) {
                            this.serviceData.push({
                                preventiveService: myService[i]['preventiveService'],
                                preventiveServiceId: myService[i]['preventiveServiceId'],
                                dateGiven: myService[i]['preventiveServiceDate'],
                                comments: myService[i]['description'],
                                nextSchedule: myService[i]['nextSchedule'],
                            });
                        }
                    }
                    const insecticide = myService.filter(x => x.description == 'Insecticide treated nets given');
                    const exercise = myService.filter(x => x.description == 'Antenatal exercise');

                    if (insecticide.length > 0) {
                        this.PreventiveServicesFormGroup.get('insecticideTreatedNet').setValue(insecticide[0]['preventiveServiceId']);
                        this.PreventiveServicesFormGroup.get('insecticideTreatedNetGivenDate').setValue(insecticide[0]
                        ['preventiveServiceDate']);
                    }

                    if (exercise.length > 0) {
                        this.PreventiveServicesFormGroup.get('antenatalExercise').setValue(exercise[0]['preventiveServiceId']);
                    }
                    console.log(insecticide);
                    console.log(exercise);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading Preventive Services ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public getPatientPartnerTestingInfo(patientId: number, patientMasterVisitId: number) {
        this.partnerTesting$ = this.ancService.getPatientPartnerTestingInfo(patientId)
            .subscribe(
                p => {

                    const service = p;

                    const partnerTesting = service.filter(x => x.patientMasterVisitId == patientMasterVisitId);
                    console.log('partner testing');

                    console.log(partnerTesting);

                    if (partnerTesting.length > 0) {
                        this.PreventiveServicesFormGroup.get('PartnerTestingVisit').setValue(partnerTesting[0]['partnerTested']);
                        this.PreventiveServicesFormGroup.get('finalHIVResult').setValue(partnerTesting[0]['partnerHIVResult']);
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error loading Partner testing data ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    ngOnDestroy(): void {
        this.partnerTesting$.unsubscribe();
        this.preventiveService$.unsubscribe();
    }
}

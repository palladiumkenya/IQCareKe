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
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DataService } from '../../../shared/_services/data.service';
import { PatientPreventiveServiceComponent } from '../patient-preventive-service/patient-preventive-service.component'

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
    ServicesDataEditList: any[] = [];
    public YesNoOptions: any[] = [];
    public YesNoNaOptions: any[] = [];
    public FinalResultOptions: any[] = [];
    public maxDate: Date;
    public minDate: Date;
    public preventiveService$: Subscription;
    public partnerTesting$: Subscription;

    @Output() nextStep = new EventEmitter<PreventiveServiceEmitter>();
    @Input() preventiveServices: PreventiveServiceEmitter;
    @Input() serviceFormOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('visitDate') VisitDate: Date;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    public preventiveServicesData: PreventiveServiceEmitter;
    public serviceData: PreventiveEmitter[] = [];
    public serviceDataEdit: PreventiveEmitter[] = [];


    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private dialog: MatDialog,
        private dataService: DataService,
        private notificationService: NotificationService,
        private ancService: AncService) {
    }

    ngOnInit() {
        this.PreventiveServicesFormGroup = this._formBuilder.group({
            // preventiveServices: ['', (this.isEdit) ? [] : Validators.required],
            //dateGiven: ['', (this.isEdit) ? [] : Validators.required],
            //comments: ['', []],
            //nextSchedule: ['', []],
            insecticideTreatedNet: ['', Validators.required],
            insecticideTreatedNetGivenDate: ['', Validators.required],
            antenatalExercise: ['', Validators.required],
            //   insecticideGivenDate: ['', Validators.required],
            PartnerTestingVisit: ['', Validators.required],
            finalHIVResult: ['', Validators.required]
        });

        this.maxDate = moment(this.VisitDate).toDate();
        this.minDate = moment(this.VisitDate).toDate();
        /*this.dataService.visitDate.subscribe(date => {

            this.maxDate = date;
            this.minDate = date;
        });*/
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

        this.notify.emit({
            'form': this.PreventiveServicesFormGroup, 'preventive_service_data': (this.isEdit) ?
                this.serviceDataEdit : this.serviceData
        });

        if (this.isEdit) {
            // this.getPatientPreventiveServiceInfo(this.patientId, this.patientMasterVisitId);

            /*this.PreventiveServicesFormGroup.get('preventiveServices').clearValidators();
            this.PreventiveServicesFormGroup.get('dateGiven').clearValidators();
            this.PreventiveServicesFormGroup.get('comments').clearValidators();
            this.PreventiveServicesFormGroup.get('nextSchedule').clearValidators(); */

            this.getPatientPartnerTestingInfo(this.patientId, this.patientMasterVisitId);
            this.getPatientPreventiveServiceInfoAll(this.patientId);
        } else {
            this.getPatientPreventiveServiceInfoAll(this.patientId);
        }
    }

    public moveNextStep() {
        const insectedTreatedNet = this.PreventiveServicesFormGroup.controls['insecticideTreatedNet'].value.itemName;

        this.preventiveServicesData = {
            preventiveService: this.serviceData,
            insecticideTreatedNet: parseInt(this.PreventiveServicesFormGroup.controls['insecticideTreatedNet'].value, 10),
            insecticideTreatedNetGivenDate: (insectedTreatedNet === 'No') ?
                this.PreventiveServicesFormGroup.controls['insecticideTreatedNetGivenDate'].value : this.maxDate,
            antenatalExercise: parseInt(this.PreventiveServicesFormGroup.controls['antenatalExercise'].value, 10),
            //  insecticideGivenDate: this.PreventiveServicesFormGroup.controls['insecticideGivenDate'].value,
            PartnerTestingVisit: parseInt(this.PreventiveServicesFormGroup.controls['PartnerTestingVisit'].value, 10),
            finalHIVResult: parseInt(this.PreventiveServicesFormGroup.controls['finalHIVResult'].value, 10),

        };
        this.nextStep.emit(this.preventiveServicesData);
        this.notify.emit(this.PreventiveServicesFormGroup);
    }

    public addTopics() {


        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;
        resultsDialogConfig.width = '700px';
        resultsDialogConfig.height = '300px';

        resultsDialogConfig.data = {
            isEdit: this.isEdit,
            preventiveServicesOptions: this.preventiveServicesOptions,
            maxDate: this.maxDate,
            minDate: this.minDate
        };

        const dialogRef = this.dialog.open(PatientPreventiveServiceComponent, resultsDialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const service = data.preventiveServices.itemName;
                const serviceId = data.preventiveServices.itemId;
                const dateGivens = moment(data.nextSchedule).toDate();

                if (service === '' || data.dateGiven === ''
                ) {
                    this.snotifyService.warning('Please provide service,date given', 'preventive Service',
                        this.notificationService.getConfig());
                    return false;
                }

                if (this.isEdit) {
                    if (this.serviceDataEdit.filter(x => x.preventiveService === service).length > 0) {
                        this.snotifyService.warning('' + service + ' exists', 'preventive Service', this.notificationService.getConfig());
                    } else {
                        this.serviceDataEdit.push({
                            preventiveService: service,
                            preventiveServiceId: serviceId,
                            dateGiven: dateGivens,
                            comments: data.comments,
                            nextSchedule: (data.nextSchedule === '') ?
                                '1900-01-01T00:00:00' : data.nextSchedule
                        });

                        this.ServicesDataEditList.push({
                            preventiveService: service,
                            preventiveServiceId: serviceId,
                            dateGiven: dateGivens,
                            comments: data.comments,
                            nextSchedule: data.nextSchedule,
                            Id: 0
                        });
                    }
                } else {
                    if (this.serviceData.filter(x => x.preventiveService === service).length > 0) {
                        this.snotifyService.warning('' + service + ' exists', 'preventive Service', this.notificationService.getConfig());
                    } else {
                        this.serviceData.push({
                            preventiveService: service,
                            preventiveServiceId: serviceId,
                            dateGiven: dateGivens,
                            comments: data.comments,
                            nextSchedule: data.nextSchedule
                        });

                        this.ServicesDataEditList.push({
                            preventiveService: service,
                            preventiveServiceId: serviceId,
                            dateGiven: dateGivens,
                            comments: data.comments,
                            nextSchedule: data.nextSchedule,
                            Id: 0
                        });
                    }
                }
            });
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

        let Id: number;
        if(this.ServicesDataEditList.length > 0) 
        {
        Id = parseInt(this.ServicesDataEditList[idx].Id, 10);
        if (Id > 0) {
            this.ancService.deletePreventiveServices(Id).subscribe(x => {
                if (x) {

                    this.snotifyService.success('Successfully removed the preventive services  ' + x['preventiveServiceId']  ,
                        'Patient Preventive Services', this.notificationService.getConfig());
                    if (this.isEdit) {
                        this.serviceDataEdit.splice(idx,1);
                    } else {
                    this.serviceData.splice(idx, 1);
                    }
                    this.ServicesDataEditList.splice(idx, 1);
                }
            },
                (err) => {
                    this.snotifyService.success('Error removing the preventive services  ' + err,
                        'Patient Preventive Services', this.notificationService.getConfig());
                });

        } else {
            if (this.isEdit) {
                this.serviceDataEdit.splice(idx,1);
            } else {
            this.serviceData.splice(idx, 1);
            }
            this.ServicesDataEditList.splice(idx, 1);
        }

    }
     else {
        if (this.isEdit) {
            this.serviceDataEdit.splice(idx,1);
        } else {
        this.serviceData.splice(idx, 1);
        }
     }
        // this.counselling_data.splice(idx, 1);


        // this.serviceData.splice(idx, 1);
        //this.ServicesDataEdit.splice(idx,1);

    }



    public getPatientPreventiveServiceInfoAll(patientId: number) {
        this.preventiveService$ = this.ancService.getPatientPreventiveServiceInfo(patientId)
            .subscribe(
                p => {

                    this.ServicesDataEditList = [];
                    const service = p;
                    if (service.length > 0) {
                        for (let i = 0; i < service.length; i++) {
                            this.serviceData.push({
                                preventiveService: service[i]['preventiveService'],
                                preventiveServiceId: service[i]['preventiveServiceId'],
                                dateGiven: service[i]['preventiveServiceDate'],
                                comments: service[i]['description'],
                                nextSchedule: service[i]['nextSchedule'],
                            });

                            this.ServicesDataEditList.push({
                                preventiveService: service[i]['preventiveService'],
                                preventiveServiceId: service[i]['preventiveServiceId'],
                                dateGiven: service[i]['preventiveServiceDate'],
                                comments: service[i]['description'],
                                nextSchedule: service[i]['nextSchedule'],
                                Id: service[i]['id']
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
                },
                (err) => {
                    this.snotifyService.error('Error loading Preventive Services ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                });
    }

    public getPatientPreventiveServiceInfo(patientId: number, patientMasterVisitId: number) {
        this.preventiveService$ = this.ancService.getPatientPreventiveServiceInfo(patientId)
            .subscribe(
                p => {

                    const service = p;
                    const myService = service.filter(x => x.patientMasterVisitId == patientMasterVisitId);

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
                },
                (err) => {
                    this.snotifyService.error('Error loading Preventive Services ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                });
    }

    public getPatientPartnerTestingInfo(patientId: number, patientMasterVisitId: number) {
        this.partnerTesting$ = this.ancService.getPatientPartnerTestingInfo(patientId)
            .subscribe(
                p => {

                    const service = p;

                    const partnerTesting = service.filter(x => x.patientMasterVisitId == patientMasterVisitId);

                    if (partnerTesting.length > 0) {
                        this.PreventiveServicesFormGroup.get('PartnerTestingVisit').setValue(partnerTesting[0]['partnerTested']);
                        this.PreventiveServicesFormGroup.get('finalHIVResult').setValue(partnerTesting[0]['partnerHIVResult']);
                    }
                },
                (err) => {
                    this.snotifyService.error('Error loading Partner testing data ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                });
    }

    ngOnDestroy(): void {
        // this.partnerTesting$.unsubscribe();
        this.preventiveService$.unsubscribe();
    }
}

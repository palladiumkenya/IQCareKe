import { Component, EventEmitter, Input, OnInit, Output, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { Subscription } from 'rxjs/index';
import { ClientMonitoringEmitter } from '../../emitters/ClientMonitoringEmitter';
import { HAARTProphylaxisEmitter } from '../../emitters/HAARTProphylaxisEmitter';
import { ActivatedRoute } from '@angular/router';
import { ChronicIllnessEmitter } from '../../emitters/ChronicIllnessEmitter';
import { PatientChronicIllness } from '../../_models/PatientChronicIllness';
import * as moment from 'moment';
import { AncService } from '../../_services/anc.service';
import { DataService } from '../../_services/data.service';

import { MatDialogConfig, MatDialog } from '@angular/material';
import { PatientChronicillnessComponent } from '../patient-chronicillness/patient-chronicillness.component';

export interface Options {
    value: string;
    viewValue: string;
}

@Component({
    selector: 'app-haart-prophylaxis',
    templateUrl: './haart-prophylaxis.component.html',
    styleUrls: ['./haart-prophylaxis.component.css']
})
export class HaartProphylaxisComponent implements OnInit, OnDestroy {

    public HaartProphylaxisFormGroup: FormGroup;
    public yesnonaOptions: any[] = [];
    public chronicIllnessOptions: any[] = [];
    public YesNoOptions: any[] = [];

    chronicIllnessEditList: any[] = [];
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
    public chronicIllnessEdit: ChronicIllnessEmitter[] = [];
    public patientchronicIllnessData: PatientChronicIllness[] = [];
    public hiv_status: string;

    public personId: number;
    // public patientMasterVisitId: number;
    public serviceAreaId: number;
    //  public patientId: number;
    public userId: number;
    public isDisabled: boolean = true;

    public maxDate: Date = moment().toDate();

    constructor(private route: ActivatedRoute,
        private _formBuilder: FormBuilder,
        private dialog: MatDialog,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private ancService: AncService,
        private dataservice: DataService) {
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
            otherIllness: ['', (this.isEdit) ? [] : Validators.required]
        });

        const {
            yesnoOptions,
            yesNoNaOptions,
            chronicIllnessOptions,
        } = this.haartProphylaxisOptions[0];
        this.yesnonaOptions = yesnoOptions;
        this.yesnonaOptions = yesNoNaOptions;
        this.chronicIllnessOptions = chronicIllnessOptions;

        this.dataservice.currentHivStatus.subscribe(hivStatus => {
            this.hiv_status = hivStatus;
            if (this.hiv_status !== '' && this.hiv_status != 'Positive') {
                this.HaartProphylaxisFormGroup.get('onArvBeforeANCVisit').disable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('startedHaartANC').disable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('cotrimoxazole').disable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('aztFortheBaby').disable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('nvpForBaby').disable({ onlySelf: true });
            } else if (this.hiv_status == 'Positive') {
                this.HaartProphylaxisFormGroup.get('onArvBeforeANCVisit').enable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('startedHaartANC').enable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('cotrimoxazole').enable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('aztFortheBaby').enable({ onlySelf: true });
                this.HaartProphylaxisFormGroup.get('nvpForBaby').enable({ onlySelf: true });
            }
        });

        this.notify.emit({
            'form': this.HaartProphylaxisFormGroup, 'illness_data': (this.isEdit) ?
                this.chronicIllnessEdit : this.chronicIllness
        });
        if (this.isEdit) {
            this.getPatientDrugAdministrationInfo(this.patientId);
            this.getPatientChronicIllnessInfo(this.patientId);
        } else {
            this.getPatientDrugAdministrationInfo(this.patientId);
            this.getPatientChronicIllnessInfo(this.patientId);
        }
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                });
    }    

    public AddOtherIllness() {
        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;
        resultsDialogConfig.width = '600px';
        resultsDialogConfig.height = '300px';

        resultsDialogConfig.data = {
            isEdit: this.isEdit,
            chronicIllnessOptions: this.chronicIllnessOptions,


        };

        const dialogRef = this.dialog.open(PatientChronicillnessComponent, resultsDialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                /* const illness = this.HaartProphylaxisFormGroup.controls['illness'].value.itemName;
                 const illnessId = parseInt(this.HaartProphylaxisFormGroup.controls['illness'].value.itemId, 10);
                 const onsetDates = moment(this.HaartProphylaxisFormGroup.controls['onSetDate'].value).toDate();*/

                const illness = data.illness.itemName;
                const illnessId = parseInt(data.illness.itemId, 10);
                const onsetDates = moment(data.onSetDate).toDate();
                const currentTreatment = data.currentTreatment;

                if (illness === '' || data.onSetDate === '' ||
                    data.currentTreatment === '') {
                    this.snotifyService.warning('illness,onsetDate and current Treatment is required', 
                        this.notificationService.getConfig());
                    return false;
                }

                if (this.isEdit) {
                    if (this.chronicIllnessEdit.filter(x => x.onSetDate === onsetDates && x.chronicIllness === illness).length > 0) {
                        this.snotifyService.warning('' + illness + ' exists', 'Counselling', this.notificationService.getConfig());
                    } else {
                        this.chronicIllnessEdit.push({
                            chronicIllness: illness,
                            chronicIllnessId: illnessId,
                            onSetDate: onsetDates,
                            currentTreatment: currentTreatment // ,
                            // dose: parseInt(this.HaartProphylaxisFormGroup.controls['dose'].value.toString(), 10)
                        });

                        this.chronicIllnessEditList.push({
                            chronicIllness: illness,
                            chronicIllnessId: illnessId,
                            onSetDate: onsetDates,
                            currentTreatment: currentTreatment,
                            Id: 0
                        });
                    }
                } else {
                    if (this.chronicIllness.filter(x => x.chronicIllness === illness).length > 0) {
                        this.snotifyService.warning('' + illness + ' exists', 'Counselling', this.notificationService.getConfig());
                    } else {
                        this.chronicIllness.push({
                            chronicIllness: illness,
                            chronicIllnessId: illnessId,
                            onSetDate: onsetDates,
                            currentTreatment: currentTreatment
                        });
                        this.chronicIllnessEditList.push({
                            chronicIllness: illness,
                            chronicIllnessId: illnessId,
                            onSetDate: onsetDates,
                            currentTreatment: currentTreatment,
                            Id: 0
                        });
                    }
                }
            });
    }

    public onChangeOtherIllness(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue === 'Yes') {
            this.isDisabled = false;
        } else if (event.isUserInput && event.source.selected) {
            this.isDisabled = true;
        }
    }



    public removeRow(idx) {
        //  this.chronicIllness.splice(idx, 1);

        let Id: number;
        if (this.chronicIllnessEditList.length > 0) {
            Id = parseInt(this.chronicIllnessEditList[idx].Id, 10);
            if (Id > 0) {
                this.ancService.deletePatientChronicIllness(Id).subscribe(x => {
                    if (x) {

                        this.snotifyService.success('Successfully removed the patient chronic illness  ' + x['preventiveServiceId'],
                            'Patient Chronic Illness', this.notificationService.getConfig());
                        if (this.isEdit) {
                            this.chronicIllnessEdit.splice(idx, 1);
                        } else {
                            this.chronicIllness.splice(idx, 1);
                        }
                        this.chronicIllnessEditList.splice(idx, 1);

                    }
                },
                    (err) => {
                        this.snotifyService.success('Error removing the patient chronic illness  ' + err,
                            'Patient Chronic Illness', this.notificationService.getConfig());
                    });

            } else {
                if (this.isEdit) {
                    this.chronicIllnessEdit.splice(idx, 1);
                } else {
                    this.chronicIllness.splice(idx, 1);
                }
                this.chronicIllnessEditList.splice(idx, 1);
            }

        } else {
            if (this.isEdit) {
                this.chronicIllnessEdit.splice(idx, 1);
            } else {
                this.chronicIllness.splice(idx, 1);
            }
        }
        // this.counselling_data.splice(idx, 1);



    }

    public onARVBeforeFirstANC(event) {
        const no = this.yesnonaOptions.filter(x => x.itemName = 'No');
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.HaartProphylaxisFormGroup.get('startedHaartANC').setValue(no[0]['itemId']);
        } else {

        }
    }

    public getPatientDrugAdministrationInfo(patientId: number) {
        this.drugAdministration$ = this.ancService.getPatientDrugAdministrationInfo(patientId)
            .subscribe(
                p => {
                    const drugAdministration = p;

                    if (drugAdministration) {
                        const firstAncVisit = drugAdministration.filter(x => x.strDrugAdministered == 'On ARV before 1st ANC Visit');
                        const haartAnc = drugAdministration.filter(x => x.strDrugAdministered == 'Started HAART in ANC');
                        const cotrim = drugAdministration.filter(x => x.strDrugAdministered == 'Cotrimoxazole');
                        const aztBaby = drugAdministration.filter(x => x.strDrugAdministered == 'AZT for the baby dispensed');
                        const nvpBaby = drugAdministration.filter(x => x.strDrugAdministered == 'NVP for baby dispensed');

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

                    this.snotifyService.error('Error loading patient who stage ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                });
    }

    public getPatientChronicIllnessInfo(patientId: number) {
        this.drugAdministration$ = this.ancService.getPatientChronicIllnessInfo(patientId)
            .subscribe(
                p => {

                    const chronic = p;

                    if (chronic.length > 0) {
                        for (let i = 0; i < chronic.length; i++) {
                            this.chronicIllness.push({
                                chronicIllness: chronic[i]['chronicIllness'],
                                chronicIllnessId: chronic[i]['chronicIllnessId'],
                                onSetDate: chronic[i]['onsetDate'],
                                currentTreatment: chronic[i]['treatment'],
                                dose: chronic[i]['dose']
                            });

                            this.chronicIllnessEditList.push({
                                chronicIllness: chronic[i]['chronicIllness'],
                                chronicIllnessId: chronic[i]['chronicIllnessId'],
                                onSetDate: chronic[i]['onsetDate'],
                                currentTreatment: chronic[i]['treatment'],
                                dose: chronic[i]['dose'],
                                Id: chronic[i]['id']
                            });
                        }
                        const yesno = this.yesnonaOptions.filter(x => x.itemName == 'Yes');
                        this.HaartProphylaxisFormGroup.get('otherIllness').setValue(yesno[0]['itemId']);
                    }
                },
                (err) => {

                    this.snotifyService.error('Error loading patient Chronic Illness ' + err, 'WHO', this.notificationService.getConfig());
                },
                () => {
                });
    }

    ngOnDestroy(): void {
        this.drugAdministration$.unsubscribe();
    }




}

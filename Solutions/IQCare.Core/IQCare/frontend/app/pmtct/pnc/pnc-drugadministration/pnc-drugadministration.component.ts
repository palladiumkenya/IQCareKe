import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, EventEmitter, Output, Input, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource } from '@angular/material';
import { DataService } from '../../_services/data.service';

@Component({
    selector: 'app-pnc-drugadministration',
    templateUrl: './pnc-drugadministration.component.html',
    styleUrls: ['./pnc-drugadministration.component.css']
})
export class PncDrugadministrationComponent implements OnInit, AfterViewInit {
    DrugAdministrationForm: FormGroup;
    yesNoNaOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    infantPncDrugOptions: LookupItemView[] = [];
    infantDrugsStartContinueOptions: LookupItemView[] = [];

    drugs_displaycolumns: any[] = ['drugName', 'status', 'action'];
    added_drugs_data: any[] = [];
    old_drugs_data: any[] = [];
    dataSource = new MatTableDataSource(this.added_drugs_data);


    @Input('drugAdministrationOptions') drugAdministrationOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() notifyInfantDrug: EventEmitter<any[]> = new EventEmitter<any[]>();
    hiv_status: string;

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dataservice: DataService,
        private cd: ChangeDetectorRef) { }

    ngOnInit() {
        this.DrugAdministrationForm = this._formBuilder.group({
            startedARTPncVisit: new FormControl('', [Validators.required]),
            haematinics_given: new FormControl('', [Validators.required]),
            infant_drug: new FormControl('', [Validators.required]),
            infant_start: new FormControl('', [Validators.required]),
            id_startedart: new FormControl(''),
            id_haematinics: new FormControl(''),
            id_infantdrug: new FormControl(''),
            id_infantstart: new FormControl(''),
            id_startedart0: new FormControl(''),
        });

        const { yesNoNaOptions,
            yesnoOptions,
            infantPncDrugOptions,
            infantDrugsStartContinueOptions } = this.drugAdministrationOptions[0];
        this.yesNoNaOptions = yesNoNaOptions;
        this.yesnoOptions = yesnoOptions;
        this.infantPncDrugOptions = infantPncDrugOptions;
        this.infantDrugsStartContinueOptions = infantDrugsStartContinueOptions;

        this.notify.emit(this.DrugAdministrationForm);

        this.dataservice.currentHivStatus.subscribe(hivStatus => {
            this.hiv_status = hivStatus;

            if (this.hiv_status !== '' && this.hiv_status != 'Positive') {
                this.DrugAdministrationForm.get('startedARTPncVisit').disable({ onlySelf: true });
                this.DrugAdministrationForm.get('haematinics_given').disable({ onlySelf: true });
                this.DrugAdministrationForm.get('infant_drug').disable({ onlySelf: true });
                this.DrugAdministrationForm.get('infant_start').disable({ onlySelf: true });
            } else {
                this.DrugAdministrationForm.get('startedARTPncVisit').enable({ onlySelf: false });
                this.DrugAdministrationForm.get('haematinics_given').enable({ onlySelf: false });
                this.DrugAdministrationForm.get('infant_drug').enable({ onlySelf: false });
                this.DrugAdministrationForm.get('infant_start').enable({ onlySelf: false });
                this.DrugAdministrationForm.get('id_startedart0').setValue('0');
            }
        });
    }

    ngAfterViewInit(): void {
        if (this.isEdit) {
            this.loadDrugAdministrationInfo();
        }
    }

    loadDrugAdministrationInfo(): void {
        this.pncService.getPncDrugAdministration(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                // console.log(result);
                for (let i = 0; i < result.length; i++) {
                    if (result[i]['strDrugAdministered'] == 'Started HAART in PNC') {
                        this.DrugAdministrationForm.get('startedARTPncVisit').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_startedart').setValue(result[i]['id']);
                    } else if (result[i]['strDrugAdministered'] == 'Haematinics given') {
                        this.DrugAdministrationForm.get('haematinics_given').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_haematinics').setValue(result[i]['id']);
                    } else if (result[i]['description'] == 'Zidovudine (AZT)') {
                        this.DrugAdministrationForm.get('infant_drug').setValue(result[i]['drugAdministered']);
                        this.DrugAdministrationForm.get('id_infantdrug').setValue(result[i]['id']);

                        this.old_drugs_data.push({
                            drugId: result[i]['drugAdministered'],
                            drugName: this.infantPncDrugOptions.filter(x => x.itemId == result[i]['drugAdministered'])[0].displayName,
                            statusId: result[i]['value'],
                            status: this.infantDrugsStartContinueOptions.filter(x => x.itemId == result[i]['value'])[0].displayName
                        });
                    } else if (result[i]['description'] == 'Nevirapine (NVP)') {
                        this.DrugAdministrationForm.get('infant_start').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_infantstart').setValue(result[i]['id']);

                        this.old_drugs_data.push({
                            drugId: result[i]['drugAdministered'],
                            drugName: this.infantPncDrugOptions.filter(x => x.itemId == result[i]['drugAdministered'])[0].displayName,
                            statusId: result[i]['value'],
                            status: this.infantDrugsStartContinueOptions.filter(x => x.itemId == result[i]['value'])[0].displayName
                        });
                    }
                }

                this.dataSource = new MatTableDataSource(this.old_drugs_data);
            },
            (error) => {
                this.snotifyService.error('Fetching Drug Administration ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
    }


    public AddDrug() {
        const drugId = this.DrugAdministrationForm.get('infant_drug').value;
        const statusId = this.DrugAdministrationForm.get('infant_start').value;

        if (drugId == undefined || statusId == undefined) {
            return;
        }


        const drugExists = this.added_drugs_data.filter(x => x.drugId == drugId).length > 0;
        if (drugExists) {
            this.snotifyService.error('Selected drug already added', 'PNC Encounter',
                this.notificationService.getConfig());
            return;
        }

        // new patient drugs
        this.added_drugs_data.push({
            drugId: drugId,
            drugName: this.infantPncDrugOptions.filter(x => x.itemId == drugId)[0].displayName,
            statusId: statusId,
            status: this.infantDrugsStartContinueOptions.filter(x => x.itemId == statusId)[0].displayName
        });

        // new patient drugs
        this.old_drugs_data.push({
            drugId: drugId,
            drugName: this.infantPncDrugOptions.filter(x => x.itemId == drugId)[0].displayName,
            statusId: statusId,
            status: this.infantDrugsStartContinueOptions.filter(x => x.itemId == statusId)[0].displayName
        });

        this.notifyInfantDrug.emit(this.added_drugs_data);
        this.dataSource = new MatTableDataSource(this.added_drugs_data);
    }


    public removeDrug(element: any) {
        const removedDrugIndex = this.added_drugs_data.indexOf(element);
        this.added_drugs_data.splice(removedDrugIndex, 1);
        this.notifyInfantDrug.emit(this.added_drugs_data);
        this.dataSource = new MatTableDataSource(this.added_drugs_data);
    }
}

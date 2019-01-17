import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, EventEmitter, Output, Input, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

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

    @Input('drugAdministrationOptions') drugAdministrationOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

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
    }

    ngAfterViewInit(): void {
        if (this.isEdit) {
            this.loadDrugAdministrationInfo();
        }
    }

    loadDrugAdministrationInfo(): void {
        this.pncService.getPncDrugAdministration(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    if (result[i]['strDrugAdministered'] == 'Started HAART in PNC') {
                        this.DrugAdministrationForm.get('startedARTPncVisit').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_startedart').setValue(result[i]['id']);
                    } else if (result[i]['strDrugAdministered'] == 'Haematinics given') {
                        this.DrugAdministrationForm.get('haematinics_given').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_haematinics').setValue(result[i]['id']);
                    } else if (result[i]['strDrugAdministered'] == 'Infant_Drug') {
                        this.DrugAdministrationForm.get('infant_drug').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_infantdrug').setValue(result[i]['id']);
                    } else if (result[i]['strDrugAdministered'] == 'Infant_Start_Continue') {
                        this.DrugAdministrationForm.get('infant_start').setValue(result[i]['value']);
                        this.DrugAdministrationForm.get('id_infantstart').setValue(result[i]['id']);
                    }
                }
            },
            (error) => {
                this.snotifyService.error('Fetching Drug Administration ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
    }
}

import { SnotifyService } from 'ng-snotify';
import { NotificationService } from './../../../shared/_services/notification.service';
import { MaternityService } from './../../_services/maternity.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-pnc-cervicalcancerscreening',
    templateUrl: './pnc-cervicalcancerscreening.component.html',
    styleUrls: ['./pnc-cervicalcancerscreening.component.css']
})
export class PncCervicalcancerscreeningComponent implements OnInit {
    CervicalCancerScreeningForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    cervicalCancerScreeningMethodOptions: LookupItemView[] = [];
    cervicalCancerScreeningResultsOptions: LookupItemView[] = [];

    @Input('cervicalCancerScreeningOptions') cervicalCancerScreeningOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private maternityService: MaternityService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.CervicalCancerScreeningForm = this._formBuilder.group({
            cervicalcancerscreening: new FormControl('', [Validators.required]),
            method: new FormControl('', [Validators.required]),
            results: new FormControl('', [Validators.required])
        });

        const { yesnoOptions,
            cervicalCancerScreeningMethodOptions,
            cervicalCancerScreeningResultsOptions } = this.cervicalCancerScreeningOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.cervicalCancerScreeningMethodOptions = cervicalCancerScreeningMethodOptions;
        this.cervicalCancerScreeningResultsOptions = cervicalCancerScreeningResultsOptions;

        this.notify.emit(this.CervicalCancerScreeningForm);

        if (this.isEdit) {
            this.loadCervicalCancerScreening();
        }
    }

    loadCervicalCancerScreening(): void {
        this.maternityService.getPatientScreening(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                const cacxMethod = this.cervicalCancerScreeningMethodOptions.filter(obj => obj.masterName == 'CacxMethod');

                for (let i = 0; i < result.length; i++) {
                    if (result[i].screeningTypeId == cacxMethod[0].masterId) {
                        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                        const cacxMethodOption = this.cervicalCancerScreeningMethodOptions.filter(
                            obj => obj.itemId == result[i].screeningCategoryId);
                        const cacxResultOption = this.cervicalCancerScreeningResultsOptions.filter(
                            obj => obj.itemId == result[i].screeningValueId);

                        this.CervicalCancerScreeningForm.get('cervicalcancerscreening').setValue(yesOption[0].itemId);
                        this.CervicalCancerScreeningForm.get('method').setValue(cacxMethodOption[0].itemId);
                        this.CervicalCancerScreeningForm.get('results').setValue(cacxResultOption[0].itemId);

                    }
                }
            },
            (error) => {
                this.snotifyService.error('Fetching patient screening ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            },
            () => { }
        );
    }

    onCervicalCancerScreeningChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.CervicalCancerScreeningForm.controls['method'].enable({ onlySelf: false });
            this.CervicalCancerScreeningForm.controls['results'].enable({ onlySelf: false });
        } else if (event.source.selected && event.source.viewValue == 'No') {
            this.CervicalCancerScreeningForm.controls['method'].disable({ onlySelf: true });
            this.CervicalCancerScreeningForm.controls['results'].disable({ onlySelf: true });
            // default the values to null
            this.CervicalCancerScreeningForm.controls['results'].setValue('');
            this.CervicalCancerScreeningForm.controls['method'].setValue('');
        }
    }

}

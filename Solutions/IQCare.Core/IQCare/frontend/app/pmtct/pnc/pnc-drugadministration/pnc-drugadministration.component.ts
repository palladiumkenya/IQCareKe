import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-pnc-drugadministration',
    templateUrl: './pnc-drugadministration.component.html',
    styleUrls: ['./pnc-drugadministration.component.css']
})
export class PncDrugadministrationComponent implements OnInit {
    DrugAdministrationForm: FormGroup;
    yesNoNaOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    infantPncDrugOptions: LookupItemView[] = [];
    infantDrugsStartContinueOptions: LookupItemView[] = [];

    @Input('drugAdministrationOptions') drugAdministrationOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.DrugAdministrationForm = this._formBuilder.group({
            startedARTPncVisit: new FormControl('', [Validators.required]),
            haematinics_given: new FormControl('', [Validators.required]),
            infant_drug: new FormControl('', [Validators.required]),
            infant_start: new FormControl('', [Validators.required])
        });

        const { yesNoNaOptions, yesnoOptions, infantPncDrugOptions, infantDrugsStartContinueOptions } = this.drugAdministrationOptions[0];
        this.yesNoNaOptions = yesNoNaOptions;
        this.yesnoOptions = yesnoOptions;
        this.infantPncDrugOptions = infantPncDrugOptions;
        this.infantDrugsStartContinueOptions = infantDrugsStartContinueOptions;

        this.notify.emit(this.DrugAdministrationForm);
    }

}

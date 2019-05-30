import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-prep-sti-screening-treatment',
    templateUrl: './prep-sti-screening-treatment.component.html',
    styleUrls: ['./prep-sti-screening-treatment.component.css']
})
export class PrepSTIScreeningTreatmentComponent implements OnInit {
    STIScreeningForm: FormGroup;

    @Input() STIScreeningAndTreatmentOptions: any;

    yesnoOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.STIScreeningForm = this._formBuilder.group({
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
        });

        const { yesnoOptions, stiScreeningOptions } = this.STIScreeningAndTreatmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.stiScreeningOptions = stiScreeningOptions;
    }

}

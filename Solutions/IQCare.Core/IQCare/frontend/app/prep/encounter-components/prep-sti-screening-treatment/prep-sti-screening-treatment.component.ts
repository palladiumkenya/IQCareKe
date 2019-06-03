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
    yesnoOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];

    patientId: number;
    personId: number;

    @Input() STIScreeningAndTreatmentOptions: any;

    constructor(private _formBuilder: FormBuilder) {
        this.patientId = 1;
        this.personId = 1;
    }

    ngOnInit() {
        this.STIScreeningForm = this._formBuilder.group({
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
        });

        this.STIScreeningForm.controls.signsOfSTI.disable({ onlySelf: true });

        const { yesnoOptions, stiScreeningOptions } = this.STIScreeningAndTreatmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.stiScreeningOptions = stiScreeningOptions;
    }

    public onSignsOrSymptomsSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.STIScreeningForm.controls.signsOfSTI.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.STIScreeningForm.controls.signsOfSTI.setValue([]);
            this.STIScreeningForm.controls.signsOfSTI.disable({ onlySelf: true });
        }
    }
}

import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
    maxDate: Date;

    @Input() STIScreeningAndTreatmentOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) {
        this.patientId = 1;
        this.personId = 1;
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.STIScreeningForm = this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
        });

        // emit form to the stepper 
        this.notify.emit(this.STIScreeningForm);

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

import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pregnancy-outcome',
    templateUrl: './pregnancy-outcome.component.html',
    styleUrls: ['./pregnancy-outcome.component.css']
})
export class PregnancyOutcomeComponent implements OnInit {
    PregnancyOutcomeForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    yesNoDontKnowOptions: LookupItemView[] = [];
    pregnancyOutcomeOptions: LookupItemView[] = [];

    @Input() PregnancyOutcomeOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PregnancyOutcomeForm = this._formBuilder.group({
            endedPregnancy: new FormControl('', [Validators.required]),
            outcomeDate: new FormControl('', [Validators.required]),
            pregnancyOutcome: new FormControl('', [Validators.required]),
            birthDefects: new FormControl('', [Validators.required])
        });

        // emit form to the stepper 
        this.notify.emit(this.PregnancyOutcomeForm);

        const { yesnoOptions, yesNoDontKnowOptions, pregnancyOutcomeOptions } = this.PregnancyOutcomeOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.yesNoDontKnowOptions = yesNoDontKnowOptions;
        this.pregnancyOutcomeOptions = pregnancyOutcomeOptions;
    }

}

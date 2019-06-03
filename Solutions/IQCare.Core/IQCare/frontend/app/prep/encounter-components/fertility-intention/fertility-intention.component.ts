import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
    selector: 'app-fertility-intention',
    templateUrl: './fertility-intention.component.html',
    styleUrls: ['./fertility-intention.component.css']
})
export class FertilityIntentionComponent implements OnInit {
    FertilityIntentionForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    fpMethods: LookupItemView[] = [];
    planningPregnancy: LookupItemView[] = [];
    maxDate: Date;

    @Input() FertilityIntentionsOptions: any;

    constructor(private _formBuilder: FormBuilder) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.FertilityIntentionForm = this._formBuilder.group({
            lmp: new FormControl('', [Validators.required]),
            pregnant: new FormControl('', [Validators.required]),
            pregnancyPlanned: new FormControl('', [Validators.required]),
            breastFeeding: new FormControl('', [Validators.required]),
            onFamilyPlanning: new FormControl('', [Validators.required]),
            familyPlanningMethods: new FormControl('', [Validators.required]),
            planningToGetPregnant: new FormControl('', [Validators.required])
        });

        // set default form state
        this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.pregnancyPlanned.disable({ onlySelf: true });

        const { yesnoOptions, fpMethods, planningPregnancy } = this.FertilityIntentionsOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.fpMethods = fpMethods;
        this.planningPregnancy = planningPregnancy;
    }

    onClientFPSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.FertilityIntentionForm.controls.familyPlanningMethods.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.familyPlanningMethods.setValue('');
        }
    }

    onPregnancySelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.FertilityIntentionForm.controls.pregnancyPlanned.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.FertilityIntentionForm.controls.pregnancyPlanned.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.pregnancyPlanned.setValue('');
        }
    }
}

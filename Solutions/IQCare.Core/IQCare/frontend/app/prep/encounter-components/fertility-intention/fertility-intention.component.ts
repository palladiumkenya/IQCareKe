import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
    selector: 'app-fertility-intention',
    templateUrl: './fertility-intention.component.html',
    styleUrls: ['./fertility-intention.component.css']
})
export class FertilityIntentionComponent implements OnInit {
    FertilityIntentionForm: FormGroup;

    constructor(private _formBuilder: FormBuilder) { }

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
    }

}

import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, OnInit } from '@angular/core';
import { FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-prep-encounter',
    templateUrl: './prep-encounter.component.html',
    styleUrls: ['./prep-encounter.component.css']
})
export class PrepEncounterComponent implements OnInit {
    isLinear: boolean = false;

    // Form Groups
    STIScreeningFormGroup: FormArray;
    CircumcisionStatusFormGroup: FormArray;
    FertilityIntentionsFormGroup: FormArray;
    PregnancyOutcomeFormGroup: FormArray;

    yesnoOptions: LookupItemView[];

    STIScreeningAndTreatmentOptions: any[] = [];

    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.data.subscribe(
            (res) => {
                const { yesnoOptions } = res;
                this.yesnoOptions = yesnoOptions['lookupItems'];
            }
        );


        this.STIScreeningAndTreatmentOptions.push({
            'yesnoOptions': this.yesnoOptions
        });
    }

}

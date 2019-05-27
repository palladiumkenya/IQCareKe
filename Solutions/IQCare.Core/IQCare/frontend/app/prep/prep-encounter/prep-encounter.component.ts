import { Component, OnInit } from '@angular/core';
import { FormArray } from '@angular/forms';

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

    constructor() { }

    ngOnInit() {
    }

}

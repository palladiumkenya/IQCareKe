import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pregnancy-outcome',
    templateUrl: './pregnancy-outcome.component.html',
    styleUrls: ['./pregnancy-outcome.component.css']
})
export class PregnancyOutcomeComponent implements OnInit {
    PregnancyOutcomeForm: FormGroup;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PregnancyOutcomeForm = this._formBuilder.group({
            endedPregnancy: new FormControl('', [Validators.required]),
            outcomeDate: new FormControl('', [Validators.required]),
            pregnancyOutcome: new FormControl('', [Validators.required]),
            birthDefects: new FormControl('', [Validators.required])
        });
    }

}

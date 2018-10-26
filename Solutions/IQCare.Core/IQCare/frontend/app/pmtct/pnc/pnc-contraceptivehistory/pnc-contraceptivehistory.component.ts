import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-pnc-contraceptivehistory',
    templateUrl: './pnc-contraceptivehistory.component.html',
    styleUrls: ['./pnc-contraceptivehistory.component.css']
})
export class PncContraceptivehistoryComponent implements OnInit {
    ContraceptiveHistoryForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    familyPlanningMethodOptions: LookupItemView[] = [];

    @Input('contraceptiveHistoryExercise') contraceptiveHistoryExercise: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.ContraceptiveHistoryForm = this._formBuilder.group({
            onFamilyPlanning: new FormControl('', [Validators.required]),
            familyPlanningMethod: new FormControl('', [Validators.required]),
            pncExercisesGiven: new FormControl('', [Validators.required])
        });

        const { yesnoOptions, familyPlanningMethodOptions } = this.contraceptiveHistoryExercise[0];
        this.yesnoOptions = yesnoOptions;
        this.familyPlanningMethodOptions = familyPlanningMethodOptions;

        this.notify.emit(this.ContraceptiveHistoryForm);
    }

    onFamilyPlanningChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].disable({ onlySelf: false });
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].setValue('');
        }
    }
}

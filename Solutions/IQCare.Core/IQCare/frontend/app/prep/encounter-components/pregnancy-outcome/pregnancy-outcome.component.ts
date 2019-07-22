import { PrepService } from './../../_services/prep.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pregnancy-outcome',
    templateUrl: './pregnancy-outcome.component.html',
    styleUrls: ['./pregnancy-outcome.component.css'],
    providers: [PrepService]
})
export class PregnancyOutcomeComponent implements OnInit {
    PregnancyOutcomeForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    yesNoDontKnowOptions: LookupItemView[] = [];
    pregnancyOutcomeOptions: LookupItemView[] = [];

    maxDate: Date;

    @Input() PregnancyOutcomeOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private prepservice: PrepService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.PregnancyOutcomeForm = this._formBuilder.group({
            endedPregnancy: new FormControl('', [Validators.required]),
            outcomeDate: new FormControl('', [Validators.required]),
            pregnancyOutcome: new FormControl('', [Validators.required]),
            birthDefects: new FormControl('', [Validators.required]),
            id: new FormControl()
        });

        // emit form to the stepper 
        this.notify.emit(this.PregnancyOutcomeForm);

        const { yesnoOptions, yesNoDontKnowOptions, pregnancyOutcomeOptions } = this.PregnancyOutcomeOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.yesNoDontKnowOptions = yesNoDontKnowOptions;
        this.pregnancyOutcomeOptions = pregnancyOutcomeOptions;

        if (this.isEdit == 1) {
            this.loadPregnancyOutcome();
        }
    }

    loadPregnancyOutcome(): void {
        this.prepservice.getPregnancyIndicatorLog(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                if (res.length > 0) {
                    // console.log(res);
                    const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                    this.PregnancyOutcomeForm.controls.id.setValue(res[0].id);
                    this.PregnancyOutcomeForm.controls.endedPregnancy.setValue(yesOption[0].itemId);
                    this.PregnancyOutcomeForm.controls.outcomeDate.setValue(res[0].dateOfOutcome);
                    this.PregnancyOutcomeForm.controls.pregnancyOutcome.setValue(res[0].outcome);
                    this.PregnancyOutcomeForm.controls.birthDefects.setValue(res[0].birthDefects);
                } else {
                    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                    // this.PregnancyOutcomeForm.controls.id.setValue(res[0].id);
                    this.PregnancyOutcomeForm.controls.endedPregnancy.setValue(noOption[0].itemId);
                    this.PregnancyOutcomeForm.controls.outcomeDate.setValue('');
                    this.PregnancyOutcomeForm.controls.pregnancyOutcome.setValue('');
                    this.PregnancyOutcomeForm.controls.birthDefects.setValue('');
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    onPregnancyEndedSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PregnancyOutcomeForm.controls.outcomeDate.enable({ onlySelf: true });
            this.PregnancyOutcomeForm.controls.pregnancyOutcome.enable({ onlySelf: true });
            this.PregnancyOutcomeForm.controls.birthDefects.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.PregnancyOutcomeForm.controls.outcomeDate.disable({ onlySelf: true });
            this.PregnancyOutcomeForm.controls.pregnancyOutcome.disable({ onlySelf: true });
            this.PregnancyOutcomeForm.controls.birthDefects.disable({ onlySelf: true });
            this.PregnancyOutcomeForm.controls.outcomeDate.setValue('');
            this.PregnancyOutcomeForm.controls.pregnancyOutcome.setValue('');
            this.PregnancyOutcomeForm.controls.birthDefects.setValue('');
        }
    }

}

import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-pnc-cervicalcancerscreening',
    templateUrl: './pnc-cervicalcancerscreening.component.html',
    styleUrls: ['./pnc-cervicalcancerscreening.component.css']
})
export class PncCervicalcancerscreeningComponent implements OnInit {
    CervicalCancerScreeningForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    cervicalCancerScreeningMethodOptions: LookupItemView[] = [];
    cervicalCancerScreeningResultsOptions: LookupItemView[] = [];

    @Input('cervicalCancerScreeningOptions') cervicalCancerScreeningOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.CervicalCancerScreeningForm = this._formBuilder.group({
            cervicalcancerscreening: new FormControl('', [Validators.required]),
            method: new FormControl('', [Validators.required]),
            results: new FormControl('', [Validators.required])
        });

        const { yesnoOptions,
            cervicalCancerScreeningMethodOptions,
            cervicalCancerScreeningResultsOptions } = this.cervicalCancerScreeningOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.cervicalCancerScreeningMethodOptions = cervicalCancerScreeningMethodOptions;
        this.cervicalCancerScreeningResultsOptions = cervicalCancerScreeningResultsOptions;

        this.notify.emit(this.CervicalCancerScreeningForm);
    }

    onCervicalCancerScreeningChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.CervicalCancerScreeningForm.controls['method'].enable({ onlySelf: false });
            this.CervicalCancerScreeningForm.controls['results'].enable({ onlySelf: false });
        } else if (event.source.selected && event.source.viewValue == 'No') {
            this.CervicalCancerScreeningForm.controls['method'].disable({ onlySelf: true });
            this.CervicalCancerScreeningForm.controls['results'].disable({ onlySelf: true });
            // default the values to null
            this.CervicalCancerScreeningForm.controls['results'].setValue('');
            this.CervicalCancerScreeningForm.controls['method'].setValue('');
        }
    }

}

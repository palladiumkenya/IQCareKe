import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-circumcision-status',
    templateUrl: './circumcision-status.component.html',
    styleUrls: ['./circumcision-status.component.css']
})
export class CircumcisionStatusComponent implements OnInit {
    CircumcisionStatusForm: FormGroup;
    yesNoUnknownOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];

    @Input() CircumcisionStatusOptions: any;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.CircumcisionStatusForm = this._formBuilder.group({
            isClientCircumcised: new FormControl('', [Validators.required]),
            referredToVMMC: new FormControl('')
        });

        // make referral to VMMC disabled by default
        this.CircumcisionStatusForm.controls.referredToVMMC.disable({ onlySelf: true });

        const { yesNoUnknownOptions, yesnoOptions } = this.CircumcisionStatusOptions[0];
        this.yesNoUnknownOptions = yesNoUnknownOptions;
        this.yesnoOptions = yesnoOptions;
    }

    onClientCircumcisedSelection(event) {
        // disable referral to VMMC when client is already circumcised
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.CircumcisionStatusForm.controls.referredToVMMC.disable({ onlySelf: true });
            this.CircumcisionStatusForm.controls.referredToVMMC.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.CircumcisionStatusForm.controls.referredToVMMC.enable({ onlySelf: true });
        }
    }
}

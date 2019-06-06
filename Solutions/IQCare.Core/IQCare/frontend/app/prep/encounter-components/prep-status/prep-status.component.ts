import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-prep-status',
    templateUrl: './prep-status.component.html',
    styleUrls: ['./prep-status.component.css']
})
export class PrepStatusComponent implements OnInit {
    PrepStatusForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    prepStatusOptions: LookupItemView[] = [];
    prepContraindicationsOptions: LookupItemView[] = [];

    @Input() PrepStatusOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PrepStatusForm = this._formBuilder.group({
            signsOrSymptomsHIV: new FormControl('', [Validators.required]),
            contraindications_PrEP_Present: new FormControl('', [Validators.required]),
            adherenceCounselling: new FormControl('', [Validators.required]),
            PrEPStatusToday: new FormControl('', [Validators.required]),
            condomsIssued: new FormControl('', [Validators.required]),
            noCondomsIssued: new FormControl('', [Validators.required]),
        });

        // Set initial form state
        this.PrepStatusForm.controls.noCondomsIssued.disable({ onlySelf: true });

        // emit form to the stepper 
        this.notify.emit(this.PrepStatusForm);

        const { yesnoOptions, prepStatusOptions, prepContraindicationsOptions } = this.PrepStatusOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.prepStatusOptions = prepStatusOptions;
        this.prepContraindicationsOptions = prepContraindicationsOptions;
    }

    onCondomsIssuedSelection(event) {
        // disable referral to VMMC when client is already circumcised
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PrepStatusForm.controls.noCondomsIssued.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.PrepStatusForm.controls.noCondomsIssued.disable({ onlySelf: true });
            this.PrepStatusForm.controls.noCondomsIssued.setValue('');
        }
    }

}

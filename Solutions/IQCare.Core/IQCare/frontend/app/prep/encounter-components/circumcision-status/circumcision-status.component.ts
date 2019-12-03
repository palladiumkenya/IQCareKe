import { PrepService } from './../../_services/prep.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-circumcision-status',
    templateUrl: './circumcision-status.component.html',
    styleUrls: ['./circumcision-status.component.css'],
    providers: [PrepService]
})
export class CircumcisionStatusComponent implements OnInit {
    CircumcisionStatusForm: FormGroup;
    yesNoUnknownOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];

    @Input() CircumcisionStatusOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private prepservice: PrepService) { }

    ngOnInit() {
        this.CircumcisionStatusForm = this._formBuilder.group({
            isClientCircumcised: new FormControl('', [Validators.required]),
            referredToVMMC: new FormControl(''),
            id: new FormControl()
        });

        // emit form to the stepper 
        this.notify.emit(this.CircumcisionStatusForm);

        // make referral to VMMC disabled by default
        this.CircumcisionStatusForm.controls.referredToVMMC.disable({ onlySelf: true });

        const { yesNoUnknownOptions, yesnoOptions } = this.CircumcisionStatusOptions[0];
        this.yesNoUnknownOptions = yesNoUnknownOptions;
        this.yesnoOptions = yesnoOptions;
        this.loadCircumcisionStatus();

        if (this.isEdit == 1) {
            this.loadCircumcisionStatus();
        }
    }

    loadCircumcisionStatus() {
        this.prepservice.getCircumcisionStatus(this.patientId).subscribe(
            (res) => {
                if (res) {
                    this.CircumcisionStatusForm.controls.isClientCircumcised.setValue(res.clientCircumcised);
                    this.CircumcisionStatusForm.controls.referredToVMMC.setValue(res.referredToVMMC);
                    this.CircumcisionStatusForm.controls.id.setValue(res.id);
                }
            },
            (error) => {

            }
        );
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

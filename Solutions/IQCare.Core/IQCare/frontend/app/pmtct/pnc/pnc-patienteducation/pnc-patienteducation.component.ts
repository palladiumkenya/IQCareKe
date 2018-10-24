import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-patienteducation',
    templateUrl: './pnc-patienteducation.component.html',
    styleUrls: ['./pnc-patienteducation.component.css']
})
export class PncPatienteducationComponent implements OnInit {
    PatientEducationForm: FormGroup;
    yesnoOptions: any[] = [];

    @Input('patientEducationOptions') patientEducationOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PatientEducationForm = this._formBuilder.group({
            counselledInfantFeeding: new FormControl('', [Validators.required])
        });

        const { yesnoOptions } = this.patientEducationOptions[0];
        this.yesnoOptions = yesnoOptions;

        this.notify.emit(this.PatientEducationForm);
    }

}

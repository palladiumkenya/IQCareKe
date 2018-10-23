import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-drugadministration',
    templateUrl: './pnc-drugadministration.component.html',
    styleUrls: ['./pnc-drugadministration.component.css']
})
export class PncDrugadministrationComponent implements OnInit {
    DrugAdministrationForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.DrugAdministrationForm = this._formBuilder.group({
            startedARTPncVisit: new FormControl('', [Validators.required]),
            haematinics_given: new FormControl('', [Validators.required]),
            infant_drug: new FormControl('', [Validators.required]),
            infant_start: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.DrugAdministrationForm);
    }

}

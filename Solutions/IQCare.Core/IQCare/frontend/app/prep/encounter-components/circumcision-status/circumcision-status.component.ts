import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-circumcision-status',
    templateUrl: './circumcision-status.component.html',
    styleUrls: ['./circumcision-status.component.css']
})
export class CircumcisionStatusComponent implements OnInit {
    CircumcisionStatusForm: FormGroup;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.CircumcisionStatusForm = this._formBuilder.group({
            isClientCircumcised: new FormControl('', [Validators.required]),
            referredToVMMC: new FormControl('')
        });
    }

}

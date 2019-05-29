import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-prep-status',
    templateUrl: './prep-status.component.html',
    styleUrls: ['./prep-status.component.css']
})
export class PrepStatusComponent implements OnInit {
    PrepStatusForm: FormGroup;

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
    }

}

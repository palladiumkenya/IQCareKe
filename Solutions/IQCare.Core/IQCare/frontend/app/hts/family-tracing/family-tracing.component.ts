import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Tracing} from '../_models/tracing';

@Component({
  selector: 'app-family-tracing',
  templateUrl: './family-tracing.component.html',
  styleUrls: ['./family-tracing.component.css']
})
export class FamilyTracingComponent implements OnInit {
    formGroup: FormGroup;
    tracing: Tracing;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.tracing = new Tracing();
        this.formGroup = this._formBuilder.group({
            mode: new FormControl(this.tracing.mode, [Validators.required]),
            outcome: new FormControl(this.tracing.outcome, [Validators.required]),
            dateFamilyContacted: new FormControl('', [Validators.required]),
            dateReminded: new FormControl('', [Validators.required]),
            consent: new FormControl('', [Validators.required]),
            dateBooked: new FormControl('', [Validators.required]),
        });
    }

    onSubmit() {
        console.log(this.formGroup);
        if (this.formGroup.valid) {

        } else {
            // not valid
            return;
        }
    }

}

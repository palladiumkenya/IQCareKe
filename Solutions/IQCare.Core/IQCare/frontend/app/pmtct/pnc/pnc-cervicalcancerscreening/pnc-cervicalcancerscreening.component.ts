import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-cervicalcancerscreening',
    templateUrl: './pnc-cervicalcancerscreening.component.html',
    styleUrls: ['./pnc-cervicalcancerscreening.component.css']
})
export class PncCervicalcancerscreeningComponent implements OnInit {
    CervicalCancerScreeningForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.CervicalCancerScreeningForm = this._formBuilder.group({
            cervicalcancerscreening: new FormControl('', [Validators.required]),
            method: new FormControl('', [Validators.required]),
            results: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.CervicalCancerScreeningForm);
    }

}

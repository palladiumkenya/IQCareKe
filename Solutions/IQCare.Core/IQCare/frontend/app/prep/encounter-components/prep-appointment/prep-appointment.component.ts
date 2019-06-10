import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-prep-appointment',
    templateUrl: './prep-appointment.component.html',
    styleUrls: ['./prep-appointment.component.css']
})
export class PrepAppointmentComponent implements OnInit {
    PrepAppointmentForm: FormGroup;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PrepAppointmentForm = this._formBuilder.group({
            nextAppoitmentGiven: new FormControl('', [Validators.required]),
            reasonAppointmentNoGiven: new FormControl('', [Validators.required]),
        });
    }

}

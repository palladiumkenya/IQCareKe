import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-prep-appointment',
    templateUrl: './prep-appointment.component.html',
    styleUrls: ['./prep-appointment.component.css']
})
export class PrepAppointmentComponent implements OnInit {
    PrepAppointmentForm: FormGroup;
    minDate: Date;
    yesnoOptions: LookupItemView[] = [];
    reasonsPrepAppointmentNotGivenOptions: LookupItemView[] = [];

    @Input() PrepAppointmentOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) {
        this.minDate = new Date();
    }

    ngOnInit() {
        this.PrepAppointmentForm = this._formBuilder.group({
            nextAppoitmentGiven: new FormControl('', [Validators.required]),
            reasonAppointmentNoGiven: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl('', [Validators.required]),
            clinicalNotes: new FormControl(''),
        });

        // default form state
        this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.disable({ onlySelf: true });

        // emit form to the stepper 
        this.notify.emit(this.PrepAppointmentForm);


        const { yesnoOptions, reasonsPrepAppointmentNotGivenOptions } = this.PrepAppointmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.reasonsPrepAppointmentNotGivenOptions = reasonsPrepAppointmentNotGivenOptions;
    }

    onAppointmentSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.enable({ onlySelf: true });

            // disable date 
            this.PrepAppointmentForm.controls.nextAppointmentDate.disable({ onlySelf: true });
            this.PrepAppointmentForm.controls.nextAppointmentDate.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            // enable date 
            this.PrepAppointmentForm.controls.nextAppointmentDate.enable({ onlySelf: true });

            this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.disable({ onlySelf: true });
            this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.setValue('');
        }
    }
}

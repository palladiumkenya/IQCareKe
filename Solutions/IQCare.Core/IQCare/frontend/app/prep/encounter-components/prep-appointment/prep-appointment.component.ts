import { MaternityService } from './../../../pmtct/_services/maternity.service';
import { PncService } from './../../../pmtct/_services/pnc.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';

import * as moment from 'moment';
@Component({
    selector: 'app-prep-appointment',
    templateUrl: './prep-appointment.component.html',
    styleUrls: ['./prep-appointment.component.css'],
    providers: [PncService, MaternityService]
})
export class PrepAppointmentComponent implements OnInit {
    PrepAppointmentForm: FormGroup;
    minDate: Date;
    yesnoOptions: LookupItemView[] = [];
    reasonsPrepAppointmentNotGivenOptions: LookupItemView[] = [];

    @Input() PrepAppointmentOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncservice: PncService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private maternityservice: MaternityService) {

        if (localStorage.getItem('PrepVisitDate') != null && localStorage.getItem('PrepVisitDate') != undefined) {
            this.minDate = moment(localStorage.getItem('PrepVisitDate')).toDate();
        }
        else {
            this.minDate = new Date();
        }
    }

    ngOnInit() {
        this.PrepAppointmentForm = this._formBuilder.group({
            nextAppoitmentGiven: new FormControl('', [Validators.required]),
            reasonAppointmentNoGiven: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl('', [Validators.required]),
            clinicalNotes: new FormControl(''),
            id: new FormControl()
        });

        // default form state
        this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.disable({ onlySelf: true });

        // emit form to the stepper 
        this.notify.emit(this.PrepAppointmentForm);


        const { yesnoOptions, reasonsPrepAppointmentNotGivenOptions } = this.PrepAppointmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.reasonsPrepAppointmentNotGivenOptions = reasonsPrepAppointmentNotGivenOptions;

        if (this.isEdit == 1) {
            this.loadPrepAppointment();
        }
    }

    loadAppointmentReasons(): void {
        this.maternityservice.getReasonNextAppointmentNotGiven(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                // console.log(res);
                if (res.length > 0) {
                    this.PrepAppointmentForm.controls.reasonAppointmentNoGiven.setValue(res[0].reasonAppointmentNotGiven);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPrepAppointment(): void {
        this.pncservice.getAppointments(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                if (result) {
                    const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                    this.PrepAppointmentForm.get('nextAppointmentDate').setValue(result.appointmentDate);
                    this.PrepAppointmentForm.get('clinicalNotes').setValue(result.description);
                    this.PrepAppointmentForm.get('id').setValue(result.id);
                    this.PrepAppointmentForm.get('nextAppoitmentGiven').setValue(yesOption[0].itemId);
                } else {
                    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                    this.PrepAppointmentForm.get('nextAppoitmentGiven').setValue(noOption[0].itemId);
                    this.loadAppointmentReasons();
                }
            },
            (error) => {
                this.snotifyService.error('Fetching appointments ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
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

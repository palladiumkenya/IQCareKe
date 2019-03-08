import { PncService } from './../../_services/pnc.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import * as moment from 'moment';
import { DataService } from '../../../shared/_services/data.service';

@Component({
    selector: 'app-maternity-next-appointment',
    templateUrl: './maternity-next-appointment.component.html',
    styleUrls: ['./maternity-next-appointment.component.css']
})
export class MaternityNextAppointmentComponent implements OnInit {

    nextAppointmentFormGroup: FormGroup;
    public maxtDate: Date = moment().toDate();
    public minDate: Date = moment().toDate();
    @Input() dischargeOptions: any[] = [];
    @Input() isEdit: boolean;
    @Input() patientId: number;
    @Input() patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private dataService : DataService,
        private pncservice: PncService) {
    }

    ngOnInit() {
        this.nextAppointmentFormGroup = this.formBuilder.group({
            nextAppointmentDate: new FormControl(''),
            remarks: new FormControl(''),
            id: new FormControl('')
        });

         this.dataService.visitDate.subscribe(date=>{
             this.minDate = date;
             console.log(date + ' Miiin Date')
         });

        this.notify.emit(this.nextAppointmentFormGroup);

        if (this.isEdit) {
            this.loadAppointments();
        }
    }

    loadAppointments(): void {
        this.pncservice.getAppointments(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                if (result) {
                    this.nextAppointmentFormGroup.get('nextAppointmentDate').setValue(result.appointmentDate);
                    this.nextAppointmentFormGroup.get('remarks').setValue(result.description);
                    this.nextAppointmentFormGroup.get('id').setValue(result.id);
                }
            },
            (error) => {
                this.snotifyService.error('Fetching appointments ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
    }
}

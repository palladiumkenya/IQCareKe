import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'app-maternity-next-appointment',
    templateUrl: './maternity-next-appointment.component.html',
    styleUrls: ['./maternity-next-appointment.component.css']
})
export class MaternityNextAppointmentComponent implements OnInit {

    nextAppointmentFormGroup: FormGroup;
    @Input() dischargeOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.nextAppointmentFormGroup = this.formBuilder.group({
            nextAppointmentDate: new FormControl('', [Validators.required]),
            remarks: new FormControl('', [Validators.required])
        });
    }

}

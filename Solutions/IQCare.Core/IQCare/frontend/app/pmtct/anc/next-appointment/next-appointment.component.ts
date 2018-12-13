import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, EventEmitter, Output, Input, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { SnotifyService } from 'ng-snotify';

import { Subscription } from 'rxjs/index';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';

@Component({
    selector: 'app-next-appointment',
    templateUrl: './next-appointment.component.html',
    styleUrls: ['./next-appointment.component.css']
})
export class NextAppointmentComponent implements OnInit, AfterViewInit {
    public NextAppointmentFormGroup: FormGroup;
    public LookupItems$: Subscription;
    public yesnos: any[] = [];

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Input('nextAppointmentOptions') nextAppointmentOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private pncservice: PncService) { }

    ngOnInit() {
        this.NextAppointmentFormGroup = this._formBuilder.group({
            scheduledAppointment: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl('', [Validators.required]),
            serviceRemarks: new FormControl(''),
            id: new FormControl('')
        });

        const { YesNo } = this.nextAppointmentOptions[0];
        this.yesnos = YesNo;
        this.notify.emit(this.NextAppointmentFormGroup);
    }

    ngAfterViewInit(): void {
        if (this.isEdit) {
            this.loadNextAppointment();
        }
    }

    loadNextAppointment(): any {
        this.pncservice.getAppointments(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                if (result) {
                    const yesOption = this.yesnos.filter(obj => obj.itemName == 'Yes');
                    this.NextAppointmentFormGroup.get('scheduledAppointment').setValue(yesOption[0].itemId);
                    this.NextAppointmentFormGroup.get('nextAppointmentDate').setValue(result.appointmentDate);
                    this.NextAppointmentFormGroup.get('serviceRemarks').setValue(result.description);
                    this.NextAppointmentFormGroup.get('id').setValue(result.id);
                } else {
                    const noOption = this.yesnos.filter(obj => obj.itemName == 'No');
                    this.NextAppointmentFormGroup.get('scheduledAppointment').setValue(noOption[0].itemId);
                }
            }
        );
    }

    onScheduleAppointmentChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.NextAppointmentFormGroup.controls['nextAppointmentDate'].disable({ onlySelf: true });
            this.NextAppointmentFormGroup.controls.nextAppointmentDate.setValue('');

            this.NextAppointmentFormGroup.controls['serviceRemarks'].disable({ onlySelf: true });
            this.NextAppointmentFormGroup.controls.serviceRemarks.setValue('');
        } else if (event.source.selected) {
            this.NextAppointmentFormGroup.controls['nextAppointmentDate'].enable();
            this.NextAppointmentFormGroup.controls['serviceRemarks'].enable();
        }
    }
}

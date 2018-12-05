import { Component, OnInit, EventEmitter, Output } from '@angular/core';
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
export class NextAppointmentComponent implements OnInit {

    public NextAppointmentFormGroup: FormGroup;
    public LookupItems$: Subscription;
    public yesnos: any[] = [];

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();


    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.NextAppointmentFormGroup = this._formBuilder.group({
            scheduledAppointment: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl('', [Validators.required]),
            serviceRemarks: new FormControl(''),
        });

        this.getLookupOptions('YesNo', this.yesnos);

        this.notify.emit(this.NextAppointmentFormGroup);
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

    public getLookupOptions(groupName: string, masterName: any[]) {
        this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const lookupOptions = p['lookupItems'];
                    for (let i = 0; i < lookupOptions.length; i++) {
                        masterName.push({ 'itemId': lookupOptions[i]['itemId'], 'itemName': lookupOptions[i]['itemName'] });
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.LookupItems$);
                });
    }


}

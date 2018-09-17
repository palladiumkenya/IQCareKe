import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-immunization-history',
    templateUrl: './immunization-history.component.html',
    styleUrls: ['./immunization-history.component.css']
})
export class ImmunizationHistoryComponent implements OnInit {

    public ImmunizationHistoryFormGroup: FormGroup;
    public lookupItems$: Subscription;
    public periods: any[] = [];
    public vaccines: any[] = [];

    @Input('immunizationOptions') immunizationOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {

        this.ImmunizationHistoryFormGroup = this._formBuilder.group({
            period: new FormControl('', [Validators.required]),
            immunizationGiven: new FormControl('', [Validators.required]),
            dateImmunized: new FormControl('', [Validators.required]),
            nextSchedule: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.ImmunizationHistoryFormGroup);
    }


}

import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import * as moment from 'moment';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../../../shared/_services/notification.service';

@Component({
    selector: 'app-immunization',
    templateUrl: './immunization.component.html',
    styleUrls: ['./immunization.component.css']
})
export class ImmunizationComponent implements OnInit {
    title: string;
    public ImmunizationHistoryFormGroup: FormGroup;
    vaccines: LookupItemView[] = [];
    immunizationperiods: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    public maxDate: Date;

    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<ImmunizationComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {


        this.immunizationperiods = data.immunizationperiods;
        this.vaccines = data.vaccines;
        this.yesnoOptions = data.yesnoOptions;

        this.maxDate = new Date();
        this.title = 'Immunization';
    }

    ngOnInit() {
        this.ImmunizationHistoryFormGroup = this._formBuilder.group({
            period: new FormControl('', [Validators.required]),
            immunizationGiven: new FormControl('', [Validators.required]),
            dateImmunized: new FormControl('', [Validators.required]),
            nextSchedule: new FormControl('')
        });
    }

    OnNextScheduleDateChange(event) {
        const momentA = moment(event.value.toDate(), 'DD/MM/YYYY');
        const momentB = moment(this.ImmunizationHistoryFormGroup.get('dateImmunized').value, 'DD/MM/YYYY');
        if (momentA < momentB) {
            this.ImmunizationHistoryFormGroup.get('nextSchedule').setValue('');
            this.snotifyService.info('Next Schedule date should not be before Date of Immunization',
                'Immunization', this.notificationService.getConfig());
        }
    }

    save() {
        if (this.ImmunizationHistoryFormGroup.valid) {
            this.dialogRef.close(this.ImmunizationHistoryFormGroup.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }

}

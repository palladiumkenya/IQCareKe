import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import {MatTableDataSource} from '@angular/material';

@Component({
    selector: 'app-immunization-history',
    templateUrl: './immunization-history.component.html',
    styleUrls: ['./immunization-history.component.css']
})
export class ImmunizationHistoryComponent implements OnInit {

    public ImmunizationHistoryFormGroup: FormGroup;
    public lookupItems$: Subscription;
    public immunizationperiods: any[] = [];
    public vaccines: any[] = [];
    public yesnoOptions: any[] = [];
    public immunization_history_table_data: ImmunizationHistoryTableData[] = [];
    public immunization_history: ImmunizationHistory[] = [];

    displayedColumns = ['period', 'given', 'dateImmunized', 'nextSchedule', 'action'];
    dataSource = new MatTableDataSource(this.immunization_history_table_data);

    @Input('immunizationHistoryOptions') immunizationHistoryOptions: any;
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
        const {
            immunizationPeriod,
            immunizationGiven,
            yesnoOption
                } = this.immunizationHistoryOptions[0];
        this.immunizationperiods = immunizationPeriod;
        this.vaccines = immunizationGiven;
        this.yesnoOptions = yesnoOption;

        this.notify.emit(this.ImmunizationHistoryFormGroup);
    }

   public AddImmunization() {

        console.log(this.ImmunizationHistoryFormGroup.value);
        const period = this.ImmunizationHistoryFormGroup.get('period').value.itemName;
        if (this.immunization_history_table_data.filter(x => x.immunizationPeriod === period).length > 0) {
            this.snotifyService.warning('' + period + ' exists', 'Immunization History', this.notificationService.getConfig());
        } else {
            this.immunization_history_table_data.push({
                immunizationPeriod: period,
                given: this.ImmunizationHistoryFormGroup.controls['immunizationGiven'].value.itemName,
                dateImmunized: new Date(this.ImmunizationHistoryFormGroup.controls['dateImmunized'].value),
                nextSchedule: new Date(this.ImmunizationHistoryFormGroup.controls['nextSchedule'].value)
            });

            this.immunization_history.push({
                immunizationPeriodId: this.ImmunizationHistoryFormGroup.get('period').value.itemId,
                immunizationGivenId: this.ImmunizationHistoryFormGroup.controls['immunizationGiven'].value.itemId,
                dateImmunized: new Date(this.ImmunizationHistoryFormGroup.controls['dateImmunized'].value) ,
                nextScheduled: new Date(this.ImmunizationHistoryFormGroup.controls['nextSchedule'].value)
            });

            console.log(this.immunization_history_table_data);
            this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
        }
    }

    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.immunization_history_table_data.indexOf(row.milestone);
        this.immunization_history_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
    }
}

export interface ImmunizationHistoryTableData {
    immunizationPeriod?: string;
    given?: string;
    dateImmunized?: Date;
    nextSchedule?: Date;
}

export interface ImmunizationHistory {
    immunizationPeriodId?: number;
    immunizationGivenId?: number;
    dateImmunized?: Date;
    nextScheduled?: Date;
}

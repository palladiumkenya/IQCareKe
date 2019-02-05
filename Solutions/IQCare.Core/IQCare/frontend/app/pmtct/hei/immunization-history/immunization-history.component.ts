import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { ImmunizationHistory } from '../../_models/hei/ImmunizationHistory';
import { ImmunizationHistoryTableData } from '../../_models/hei/ImmunizationHistoryTableData';
import * as moment from 'moment';
import { ImmunizationComponent } from './immunization/immunization.component';

@Component({
    selector: 'app-immunization-history',
    templateUrl: './immunization-history.component.html',
    styleUrls: ['./immunization-history.component.css']
})
export class ImmunizationHistoryComponent implements OnInit {
    public immunizationperiods: LookupItemView[] = [];
    public vaccines: any[] = [];
    public yesnoOptions: any[] = [];
    public immunization_history_table_data: ImmunizationHistoryTableData[] = [];
    public immunization_history: ImmunizationHistory[] = [];
    public maxDate: Date;

    displayedColumns = ['period', 'given', 'dateImmunized', 'nextSchedule', 'action'];
    dataSource = new MatTableDataSource(this.immunization_history_table_data);

    @Input('immunizationHistoryOptions') immunizationHistoryOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<object> = new EventEmitter<object>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private heiservice: HeiService,
        private dialog: MatDialog) {
        this.maxDate = new Date();
    }

    ngOnInit() {


        const {
            immunizationPeriod,
            immunizationGiven,
            yesnoOption
        } = this.immunizationHistoryOptions[0];
        this.immunizationperiods = immunizationPeriod;
        this.vaccines = immunizationGiven;
        this.yesnoOptions = yesnoOption;

        this.notify.emit({ 'data': this.immunization_history });

        this.loadImmunizationHistory();
    }

    public loadImmunizationHistory(): void {
        this.heiservice.getImmunizationHistory(this.patientId).subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    const immunizationPeriod = this.immunizationperiods.filter(obj => obj.itemId == result[i].periodId);
                    const immunizationGiven = this.vaccines.filter(obj => obj.itemId == result[i].vaccine);
                    this.immunization_history_table_data.push({
                        immunizationPeriod: immunizationPeriod.length > 0 ? immunizationPeriod[0].itemName : '',
                        given: immunizationGiven.length > 0 ? immunizationGiven[0].itemName : '',
                        dateImmunized: result[i].vaccineDate ? result[i].vaccineDate : null,
                        nextSchedule: result[i].nextSchedule ? result[i].nextSchedule : null
                    });
                }

                this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
            },
            (error) => { },
            () => { }
        );
    }

    public NewImmunization() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            immunizationperiods: this.immunizationperiods,
            vaccines: this.vaccines,
            yesnoOptions: this.yesnoOptions
        };

        const dialogRef = this.dialog.open(ImmunizationComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const period = data.period.itemName;
                const immunizationGiven = data.immunizationGiven.itemName;

                if (this.immunization_history_table_data.filter(x => x.immunizationPeriod === period
                    && x.given == immunizationGiven).length > 0) {
                    this.snotifyService.warning('' + period + ' exists', 'Immunization History', this.notificationService.getConfig());
                } else {

                    let nextScheduleDate;
                    if (data.nextSchedule) {
                        nextScheduleDate = new Date(data.nextSchedule);
                    }

                    this.immunization_history_table_data.push({
                        immunizationPeriod: period,
                        given: data.immunizationGiven.itemName,
                        dateImmunized: new Date(data.dateImmunized),
                        nextSchedule: nextScheduleDate
                    });

                    this.immunization_history.push({
                        immunizationPeriodId: data.period.itemId,
                        immunizationGivenId: data.immunizationGiven.itemId,
                        dateImmunized: new Date(data.dateImmunized),
                        nextScheduled: nextScheduleDate
                    });

                    this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
                }
            }
        );
    }

    /*public AddImmunization() {
        if (this.ImmunizationHistoryFormGroup.invalid) {
            this.snotifyService.warning('Please complete all fields before add', 'Immunization History',
                this.notificationService.getConfig());
            return;
        }

        const period = this.ImmunizationHistoryFormGroup.get('period').value.itemName;
        if (this.immunization_history_table_data.filter(x => x.immunizationPeriod === period).length > 0) {
            this.snotifyService.warning('' + period + ' exists', 'Immunization History', this.notificationService.getConfig());
        } else {

            let nextScheduleDate;
            if (this.ImmunizationHistoryFormGroup.controls['nextSchedule'].value) {
                nextScheduleDate = new Date(this.ImmunizationHistoryFormGroup.controls['nextSchedule'].value);
            }

            this.immunization_history_table_data.push({
                immunizationPeriod: period,
                given: this.ImmunizationHistoryFormGroup.controls['immunizationGiven'].value.itemName,
                dateImmunized: new Date(this.ImmunizationHistoryFormGroup.controls['dateImmunized'].value),
                nextSchedule: nextScheduleDate
            });

            this.immunization_history.push({
                immunizationPeriodId: this.ImmunizationHistoryFormGroup.get('period').value.itemId,
                immunizationGivenId: this.ImmunizationHistoryFormGroup.controls['immunizationGiven'].value.itemId,
                dateImmunized: new Date(this.ImmunizationHistoryFormGroup.controls['dateImmunized'].value),
                nextScheduled: nextScheduleDate
            });

            console.log(this.immunization_history_table_data);
            this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
        }
    }*/

    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.immunization_history_table_data.indexOf(row.milestone);
        this.immunization_history_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.immunization_history_table_data);
    }
}

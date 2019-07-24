import { PrepService } from './../../../prep/_services/prep.service';
import { LookupItemView } from './../../_models/LookupItemView';
import { AdverseEventsAssessmentComponent } from './../adverse-events-assessment/adverse-events-assessment.component';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';
import { NotificationService } from '../../_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-adverse-events-table',
    templateUrl: './adverse-events-table.component.html',
    styleUrls: ['./adverse-events-table.component.css'],
    providers: [PrepService]
})
export class AdverseEventsTableComponent implements OnInit {
    public adverse_events_table_data: AdverseEventsTableData[] = [];
    public newAdverseEventsData: AdverseEventsTableData[] = [];

    displayedColumns = ['adverseEvent', 'severity', 'medicine_causing', 'adverseEventsAction', 'outcome', 'action'];
    dataSource = new MatTableDataSource(this.adverse_events_table_data);

    @Output() notify: EventEmitter<AdverseEventsTableData[]> = new EventEmitter<AdverseEventsTableData[]>();
    @Input() patientId: number;
    @Input() personId: number;

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private prepservice: PrepService) { }

    ngOnInit() {
        // emit new adverse events to stepper 
        this.notify.emit(this.newAdverseEventsData);

        this.loadPatientAdverseEvents();
    }

    public loadPatientAdverseEvents(): void {
        this.prepservice.getPatientAdverseEvents(this.patientId).subscribe(
            (res) => {
                // console.log(res);
                res.forEach(adverseEvent => {
                    this.adverse_events_table_data.push({
                        adverseEvent: adverseEvent.eventName,
                        severity: adverseEvent.severity,
                        medicine_causing: adverseEvent.eventCause,
                        adverseEventsAction: adverseEvent.action,
                    });
                });

                this.dataSource = new MatTableDataSource(this.adverse_events_table_data);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    newAdverseEvents() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(AdverseEventsAssessmentComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const adverseEvent = data.adverseEvent.itemName;
                if (this.adverse_events_table_data.filter(x => x.adverseEvent === adverseEvent).length > 0) {
                    this.snotifyService.warning('' + adverseEvent + ' exists',
                        'Adverse Events', this.notificationService.getConfig());
                } else {
                    this.adverse_events_table_data.push({
                        adverseEvent: data.adverseEvent.itemName,
                        severity: data.severity.itemName,
                        medicine_causing: data.medicine_causing,
                        adverseEventsAction: data.adverseEventsAction.displayName
                    });

                    this.newAdverseEventsData.push({
                        adverseEvent: data.adverseEvent,
                        severity: data.severity,
                        medicine_causing: data.medicine_causing,
                        adverseEventsAction: data.adverseEventsAction
                    });

                    this.dataSource = new MatTableDataSource(this.adverse_events_table_data);
                }
            }
        );
    }
}

export interface AdverseEventsTableData {
    adverseEvent?: any;
    severity?: any;
    medicine_causing?: string;
    adverseEventsAction?: any;
    outcome?: boolean;
}


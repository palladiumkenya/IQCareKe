import { PrepCheckinComponent } from './../prep-checkin/prep-checkin.component';
import { Component, OnInit, NgZone } from '@angular/core';
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PrepService } from '../_services/prep.service';
import { EncounterService } from '../../shared/_services/encounter.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';

@Component({
    selector: 'app-prep-encounter-history',
    templateUrl: './prep-encounter-history.component.html',
    styleUrls: ['./prep-encounter-history.component.css'],
    providers: [EncounterService]
})
export class PrepEncounterHistoryComponent implements OnInit {
    personId: number;
    patientId: number;
    serviceAreaId: number;
    userId: number;

    prepEncounterType: LookupItemView[];

    public prep_history_table_data: AdverseEventsTableData[] = [];
    displayedColumns = ['adverseEvent', 'severity', 'medicine_causing', 'adverseEventsAction'];
    dataSource = new MatTableDataSource(this.prep_history_table_data);

    constructor(private prepService: PrepService,
        private dialog: MatDialog,
        private encounterService: EncounterService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        public zone: NgZone,
        private router: Router) {
        this.personId = 1;
        this.patientId = 1;
        this.serviceAreaId = 7;
    }

    ngOnInit() {
        this.route.data.subscribe(
            (res) => {
                const { prepEncounterTypeOption } = res;
                this.prepEncounterType = prepEncounterTypeOption;
            }
        );
        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        const prepEncounters = this.prepService.getPrepEncounterHistory(this.patientId, this.serviceAreaId);
        prepEncounters.subscribe(
            (result) => {
                console.log(result);
                result.forEach(arrayValue => {
                    this.prep_history_table_data.push({
                        
                    });
                });
                this.dataSource = new MatTableDataSource(this.prep_history_table_data);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    onPrepCheckIn() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(PrepCheckinComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
                    EncounterDate: data.visitdate,
                    PatientId: this.patientId,
                    EncounterType: this.prepEncounterType[0].itemId,
                    ServiceAreaId: this.serviceAreaId,
                    UserId: this.userId
                };

                this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
                    (result) => {
                        localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                        localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/prep/encounter/' + '/' + this.patientId + '/' + this.personId + '/'
                                + result['patientEncounterId'] + '/' + result['patientMasterVisitId']],
                                { relativeTo: this.route });
                        });
                    },
                    (error) => {
                        this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
                    },
                    () => {

                    }
                );
            }
        );
    }
}

export interface AdverseEventsTableData {
    adverseEvent?: LookupItemView;
    severity?: LookupItemView;
    medicine_causing?: string;
    adverseEventsAction?: LookupItemView;
    outcome?: boolean;
}

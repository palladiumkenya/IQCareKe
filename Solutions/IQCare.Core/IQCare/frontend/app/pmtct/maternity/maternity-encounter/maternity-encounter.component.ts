import {Component, NgZone, OnInit, ViewChild} from '@angular/core';
import {MatDialog, MatDialogConfig, MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {Subscription} from 'rxjs/index';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {ActivatedRoute, Router} from '@angular/router';
import {NotificationService} from '../../../shared/_services/notification.service';
import {MaternityService} from '../../_services/maternity.service';
import {PatientMasterVisitEncounter} from '../../_models/PatientMasterVisitEncounter';
import {CheckinComponent} from '../../checkin/checkin.component';

@Component({
    selector: 'app-maternity-encounter',
    templateUrl: './maternity-encounter.component.html',
    styleUrls: ['./maternity-encounter.component.css']
})
export class MaternityEncounterComponent implements OnInit {

    patientId: number;
    personId: number;
    serviceAreaId: number;
    userId: number;

    public lookupItems$: Subscription;
    public encounterTypes: any[] = [];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['Id', 'PatientId', 'visitNumber', 'EncounterStartTime', 'EncounterEndTime', 'edit'];
    dataSource = new MatTableDataSource();

    constructor(private dialog: MatDialog,
                private matService: MaternityService,
                private route: ActivatedRoute,
                private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                public zone: NgZone,
                private router: Router) {
    }

    ngOnInit() {
        this.route.params.subscribe(
            params => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                this.serviceAreaId = params.serviceAreaId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.getLookupItems('EncounterType', this.encounterTypes);
    }

    matCheckIn() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            section: 'maternity'

        };

        const dialogRef = this.dialog.open(CheckinComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const maternityEncounter = this.encounterTypes.filter(obj => obj.itemName == 'maternity-encounter');

                const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
                    EncounterDate: data.visitDate,
                    PatientId: this.patientId,
                    EncounterType: maternityEncounter[0].itemId,
                    ServiceAreaId: this.serviceAreaId,
                    UserId: this.userId
                };

                this.matService.saveMaternityMasterVisit(patientMasterVisitEncounter).subscribe(
                    (result) => {
                        localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                        localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);
                        localStorage.setItem('visitDate', data.visitDate);
                        localStorage.setItem('visitType', JSON.stringify(data.visitType));

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/pmtct/maternity/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
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

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItems$);
                });
    }
}

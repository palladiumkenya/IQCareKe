import { RecordsService } from './../../records/_services/records.service';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Subscription } from 'rxjs/index';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckinComponent } from '../../pmtct/checkin/checkin.component';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../_services/notification.service';
import { LookupItemService } from '../_services/lookup-item.service';
import { PatientEncounter } from '../_models/patient-encounter';
import { EncounterService } from '../_services/encounter.service';
import { SearchService } from '../../registration/_services/search.service';


@Component({
    selector: 'app-patient-encounter',
    templateUrl: './patient-encounter.component.html',
    styleUrls: ['./patient-encounter.component.css'],
    providers: [SearchService]
})
export class PatientEncounterComponent implements OnInit {
    ageNumber: number;
    patientId: number;
    personId: number;
    serviceAreaId: number;
    serviceName: string;
    userId: number;
    patientInCCC: boolean = false;
    hasmchRecord: boolean = false;
    lastPatientMasterVisitId: number;

    public lookupItems$: Subscription;
    public patientEncounterTypes: Subscription;
    public encounterTypes: any[] = [];
    public encounterTypeId: number;

    public encounterDataTable: PatientEncounter[] = [];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['Id', 'PatientId', 'visitNumber', 'EncounterStartTime', 'EncounterEndTime', 'Encounter', 'edit'];
    dataSource = new MatTableDataSource(this.encounterDataTable);

    enrollmentDate: Date;

    constructor(private dialog: MatDialog,
        private route: ActivatedRoute,
        public zone: NgZone,
        private router: Router,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private lookupItemService: LookupItemService,
        private encounterService: EncounterService,
        private recordsService: RecordsService,
        private searchService: SearchService) {

    }

    ngOnInit() {
        this.route.params.subscribe(
            params => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                this.serviceAreaId = params.serviceAreaId;
                this.serviceName = params.serviceName;
            }
        );

        const encounterName = this.serviceName.toLowerCase() + '-encounter';
        this.getLookupItems('EncounterType', this.encounterTypes, '' + encounterName + '');

        this.recordsService.personEnrollmentDetails(this.personId, this.serviceAreaId).subscribe(
            (res) => {
                const patientLookup = res['patientLookup'];
                if (patientLookup.length > 0) {
                    this.enrollmentDate = patientLookup[0]['enrollmentDate'];
                }
            }
        );

        this.recordsService.personEnrollmentDetails(this.personId, 1).subscribe(
            (res) => {
                const patientLookup = res['patientLookup'];
                if (patientLookup.length > 0) {
                    console.log(patientLookup);
                    this.ageNumber = patientLookup[0]['ageNumber'];
                    this.patientInCCC = true;
                }
            }
        );
    }

    matCheckIn() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            section: '' + this.serviceName.toLowerCase() + '',
            'enrollmentDate': this.enrollmentDate
        };
        const dialogRef = this.dialog.open(CheckinComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const maternityEncounter = this.encounterTypes.filter(obj => obj.itemName ==
                    '' + this.serviceName.toLowerCase() + '-encounter');
                this.encounterTypeId = maternityEncounter[0].itemId;

                const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
                    EncounterDate: data.visitDate,
                    PatientId: this.patientId,
                    EncounterType: maternityEncounter[0].itemId,
                    ServiceAreaId: this.serviceAreaId,
                    UserId: this.userId
                };

                this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
                    (result) => {
                        localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                        localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);
                        localStorage.setItem('visitDate', data.visitDate);
                        localStorage.setItem('visitType', JSON.stringify(data.visitType));

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/pmtct/' + this.serviceName.toLowerCase() + '/' + this.patientId + '/' + this.personId +
                                '/' + this.serviceAreaId],
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

    public getPatientEncounters(patientId: number, encounterTypeId: number) {
        this.patientEncounterTypes = this.lookupItemService.getPatientEncountersByType(patientId, encounterTypeId)
            .subscribe(
                p => {
                    // console.log('patient encounters');
                    // console.log(p);
                    if (p.length == 0) { return; }
                    this.encounterDataTable = [];
                    for (let i = 0; i < p.length; i++) {
                        this.encounterDataTable.push({
                            Id: p[i].id,
                            PatientId: p[i].patientId,
                            VisitNumber: p[i].visitNumber,
                            EncounterStartTime: p[i].encounterStartTime,
                            EncounterEndTime: p[i].encounterEndTime,
                            Encounter: p[i].encounter,
                            PatientMasterVisitId: p[i].patientMasterVisitId,
                            EncounterTypeId: p[i].encounterTypeId,
                            PatientEncounterId: p[i].patientEncounterId,
                        });
                        this.lastPatientMasterVisitId = p[i].patientMasterVisitId;
                        this.hasmchRecord = true;
                    }
                    // console.log(this.encounterDataTable);
                    this.dataSource = new MatTableDataSource(this.encounterDataTable);
                    this.dataSource.paginator = this.paginator;
                },
                (error) => {

                },
                () => {

                });
    }

    public getLookupItems(groupName: string, _options: any[], encounterName: string) {
        this.lookupItems$ = this.lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });

                        if (options[i]['itemName'] == '' + encounterName + '') {
                            this.encounterTypeId = options[i]['itemId'];
                        }
                    }
                    // console.log(this.encounterTypeId);
                    this.getPatientEncounters(this.patientId, this.encounterTypeId);
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    // console.log(this.lookupItems$);
                });
    }

    public onEdit(selectedElement: object, serviceArea: number) {
        localStorage.setItem('visitDate', selectedElement['EncounterStartTime']);

        this.zone.run(() => {
            this.router.navigate(['/pmtct/' + this.serviceName.toLowerCase() + '/update'
                + '/' + this.patientId
                + '/' + this.personId
                + '/' + this.serviceAreaId
                + '/' + selectedElement['PatientMasterVisitId']
                + '/' + selectedElement['PatientEncounterId']],
                { relativeTo: this.route });
        });
    }

    public onView(patient: number, patientMasterVisitId: number) { }

    onPharmacyClick() {

        this.zone.run(() => {
            this.router.navigate(['/pharm/' + this.patientId + '/' + this.personId],
                { relativeTo: this.route });
        });
       /* this.searchService.setSession(this.personId, this.patientId)
            .subscribe((sessionres) => {
                this.searchService.setVisitSession(this.lastPatientMasterVisitId, this.ageNumber, 261).subscribe((setVisitSession) => {
                    const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                        '/IQCare/CCC/Encounter/PharmacyPrescription.aspx';
                    const win = window.open(url, '_blank');
                    win.focus();
                });
            });*/
    }
}

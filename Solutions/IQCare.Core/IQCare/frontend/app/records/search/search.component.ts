import { LookupItemView } from './../../shared/_models/LookupItemView';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { Component, OnInit, AfterViewInit, ViewChild, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { Search } from '../_models/search';
import { SearchService } from '../_services/search.service';
import { Router, ActivatedRoute } from '../../../../node_modules/@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Store } from '@ngrx/store';
import * as AppState from '../../shared/reducers/app.states';
import { MatDialog, MatDialogConfig } from '@angular/material';
import {AddWaitingListComponent} from '../../shared/add-waiting-list/add-waiting-list.component'
import {PersonHomeService} from '../../dashboard/services/person-home.service';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css'],
    providers: [ PersonHomeService ],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
            state('expanded', style({ height: '*', visibility: 'visible' })),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})
export class SearchComponent implements OnInit, AfterViewInit {
    genderOptions: LookupItemView[] = [];
    servicesList: any[] = [];
    afterSearch: boolean = false;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['id', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 'ageNumber', 'gender', 'fullName', 'Queue'];
    dataSource = new MatTableDataSource();
    clientSearch: Search;
    expandedElement: any;
    isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');

    constructor(private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private store: Store<AppState>,
        private dialog: MatDialog,
        private lookupitemservice: LookupItemService,
        private personhomeService: PersonHomeService) {
        store.dispatch(new AppState.ClearState());
        this.clientSearch = new Search();

        // Reset Localstorage
        localStorage.removeItem('selectedService');
        localStorage.removeItem('personId');
        localStorage.removeItem('patientId');
        localStorage.removeItem('partnerId');
        localStorage.removeItem('htsEncounterId');
        localStorage.removeItem('patientMasterVisitId');
        localStorage.removeItem('isPartner');
        localStorage.removeItem('editEncounterId');
        localStorage.removeItem('encounterTypeId');
        localStorage.removeItem('facilityList');
        localStorage.removeItem('ff');
        localStorage.removeItem('store');
        localStorage.removeItem('visitDate');
        localStorage.removeItem('visitType');
        localStorage.removeItem('serviceAreaId');
        localStorage.removeItem('onEdit');
        localStorage.removeItem('ageNumber');
        localStorage.removeItem('encounterDate');
        localStorage.removeItem('ageInMonths');

        this.store.dispatch(new AppState.ClearState());
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
    }

    ngOnInit() {
        this.dataSource.sort = this.sort;

        this.lookupitemservice.getByGroupName('Gender').subscribe(
            (result) => {
                this.genderOptions = result['lookupItems'];
            }
        );

        this.personhomeService.getAllServices().subscribe(
            (res) => {
                this.servicesList = res;
            } 
        );
    }

    doSearch() {
        this.searchService.searchClient(this.clientSearch).subscribe(
            (res) => {
                const rows = [];
                res['personSearch'].forEach(element =>
                    rows.push(element, { detailRow: true, element }));
                console.log(res);
                console.log(rows);
                this.dataSource.data = rows;

                this.afterSearch = true;
                console.log('Element is :');
                console.log(this.dataSource.data);
            },
            (error) => {
                this.snotifyService.error('Error searching person ' + error, 'SEARCH', this.notificationService.getConfig());
            },
            () => {

            }
        );
    }

    getSelectedRow(row: any) {
        const personId = row['id'];
        this.zone.run(() => { this.router.navigate(['/dashboard/personhome/' + personId], { relativeTo: this.route }); });
    }
    checkQueue(): boolean {
        let appQueue: number;
        appQueue = parseInt(localStorage.getItem('appQueue'), 10);
        if (appQueue == parseInt('1', 10)) {
           

            return true;

        } else {
            localStorage.removeItem('appQueueMenu');

            return false;
        }
    }

    addWaitingList(row: any) {
        const PersonId = row['id'];
        const PatientId = row['patientId'];
        // this.zone.run(() => { this.router.navigate(['/queue/addWaitingList/' + patientId + '/' + personId], { relativeTo: this.route }); });


        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;
        resultsDialogConfig.height = '100%';
        resultsDialogConfig.width = '100%';


        resultsDialogConfig.data = {
            patientId: PatientId,
            personId: PersonId
        };

        const dialogRef = this.dialog.open(AddWaitingListComponent, resultsDialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                console.log(data);
            });


    }



}

import { Component, OnInit, AfterViewInit, ViewChild, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { Search } from '../_models/search';
import { SearchService } from '../_services/search.service';
import { Router, ActivatedRoute } from '../../../../node_modules/@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css'],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
            state('expanded', style({ height: '*', visibility: 'visible' })),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})
export class SearchComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['id', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 'gender', 'fullName'];
    dataSource = new MatTableDataSource();
    clientSearch: Search;
    expandedElement: any;
    isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');

    constructor(private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.clientSearch = new Search();
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
    }

    ngOnInit() {
        this.dataSource.sort = this.sort;
    }

    OnKeyUp(event) {
        if (event.target.value.length > 2) {
            this.doSearch();
        }
    }

    doSearch() {
        this.searchService.searchClient(this.clientSearch).subscribe(
            (res) => {
                // console.log(res['personSearch']);
                const rows = [];
                res['personSearch'].forEach(element => rows.push(element, { detailRow: true, element }));
                this.dataSource.data = rows;
            },
            (error) => {
                console.error(error);
                this.snotifyService.error('Error searching ' + error, 'SEARCH', this.notificationService.getConfig());
            }
        );
    }

    getSelectedRow(row: any) {
        console.log(row);
        const personId = row['id'];
        this.zone.run(() => { this.router.navigate(['/dashboard/personhome/' + personId], { relativeTo: this.route }); });
    }
}

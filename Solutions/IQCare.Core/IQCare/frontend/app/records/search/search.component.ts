import { Component, OnInit, AfterViewInit, ViewChild, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { Search } from '../_models/search';
import { SearchService } from '../_services/search.service';
import { Router, ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['identifierValue', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 'gender'];
    dataSource = new MatTableDataSource();
    clientSearch: Search;

    constructor(private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone) {
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
                console.log(res);
                this.dataSource.data = res['personSearch'];
            },
            (error) => {
                console.log('error');
            }
        );
    }

    getSelectedRow(row: any) {
        console.log(row);
        const personId = row['id'];
        this.zone.run(() => { this.router.navigate(['/dashboard/personhome/' + personId], { relativeTo: this.route }); });
    }
}

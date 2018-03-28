import {Component, NgZone, OnInit} from '@angular/core';
import {Search} from '../_models/search';
import {SearchService} from '../_services/search.service';
import {DataSource} from '@angular/cdk/collections';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    search: Search;

    displayedColumns = ['IdentifierValue', 'firstName', 'midName', 'lastName', 'dateOfBirth', 'enrollmentDate'];
    dataSource = new SearchDataSource(this.searchService, this.search);

    constructor(private searchService: SearchService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) {
        this.search = new Search();
    }

    ngOnInit() {
        localStorage.removeItem('personId');
        localStorage.removeItem('patientId');
        localStorage.removeItem('partnerId');
        localStorage.removeItem('htsEncounterId');
        localStorage.removeItem('patientMasterVisitId');
        localStorage.setItem('serviceAreaId', '2');
    }

    onSubmit() {
        this.dataSource = new SearchDataSource(this.searchService, this.search);
    }

    getSelectedRow(row) {
        console.log(row);

        localStorage.setItem('personId', row['personId']);
        localStorage.setItem('patientId', row['patientId']);

        this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
    }
}

export class SearchDataSource extends DataSource<any> {
    constructor(private searchService: SearchService, private search: Search) {
        super();
    }

    connect(): Observable<any[]> {
        if (this.search == undefined) {
            return Observable.from([]);
        } else {
            return this.searchService.searchClient(this.search);
        }

    }

    disconnect() {}
}

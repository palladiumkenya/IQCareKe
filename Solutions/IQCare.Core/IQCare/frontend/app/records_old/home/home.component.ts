import { AfterViewInit, Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator, MatTableDataSource, MatCheckboxChange } from '@angular/material';
import { Search, SearchList } from '../models/search';
import { SearchService } from '../services/recordssearch';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { from as observableFrom } from 'rxjs';
import { of as observableOf } from 'rxjs';

import { CollectionViewer } from '@angular/cdk/collections';

@Component({
    selector: 'app-Recordshome',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})



export class RecordsHomeComponent implements OnInit, AfterViewInit {

    @ViewChild(MatPaginator) paginator: MatPaginator;
    element: any[] = [];
    selectedRowIndex: number = -1;
    // public element: any[] = [];
    personsearch: Search;
    displayedColumns = ['firstName', 'middleName', 'lastName', 'dateOfBirth', 'enrollmentNumber'];

    dataSource = new MatTableDataSource();
    constructor(private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone) {
        this.personsearch = new Search();

        this.LoadData();
        // this.dataSource = new SearchDataSource(this.searchService, this.personsearch);
    }


    ngOnInit() {

        localStorage.removeItem('personId');

    }

    ngAfterViewInit() {

        this.dataSource.paginator = this.paginator;
    }



    getSelectedRow(row) {
        this.selectedRowIndex = row.id;
        localStorage.setItem('personId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/recordregistration/patientprofile'], { relativeTo: this.route }); });
        // console.log(row['personId']);
    }


    OnKeyUp() {


        // this.dataSource = new SearchDataSource(this.searchService, this.personsearch);
        this.LoadData();
        console.log(this.dataSource);


    }

    LoadData() {

        if (this.personsearch == undefined) {
            return observableFrom([]);
        } else {
            this.searchService.searchPerson(this.personsearch).subscribe(data => {
                this.dataSource.data = data['personSearch'];
                console.log(this.dataSource.data);
            });
        }
    }
    highlight(row) {
        this.selectedRowIndex = row.id;
    }
    public onChange(event: MatCheckboxChange) {
        if (event.checked) {
            this.personsearch.NotClient = true;
        } else {
            this.personsearch.NotClient = false;
        }
    }

}

export class Element {

    Id: number;
    PersonId: number;
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Sex: number;
    Active?: boolean;
    DeleteFlag?: boolean;
    CreateDate?: Date;
    CreatedBy?: number;
    AuditData: string;
    DateOfBirth?: Date;
    DobPrecision?: boolean;
    PersonIdentifier: string;
    PersonIdentifierType: string;
    PersonIdentifierValue: string;
    PatientIdentifier: string;
    PatientIdentifierType: string;
    PatientIdentifierValue: string;


}



export class SearchDataSource extends DataSource<any>{
    element: any[] = [];
    constructor(private searchService: SearchService, private search: Search) {
        super();

    }

    connect(): Observable<any[]> {
        if (this.search == undefined) {
            return observableFrom([]);
        } else {

            this.searchService.searchPerson(this.search).subscribe((data: any) => {
                this.element = data['personSearch'];
            });
            return observableOf(this.element);
        }

    }

    disconnect() { }
}

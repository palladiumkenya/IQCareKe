import { AfterViewInit, Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { tap } from 'rxjs/operators';
import { Search, SearchList, SearchContact } from '../models/search';
import { SearchService } from '../services/recordssearch'
import { DataSource } from '@angular/cdk/collections';
import { Observable } from "rxjs/Rx";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import {
    TableColumn,
    ColumnMode
} from '@swimlane/ngx-datatable';
import 'rxjs/add/observable/fromEvent';
import { CollectionViewer } from '@angular/cdk/collections';


@Component({
  selector: 'appPerson-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit,AfterViewInit {


    @ViewChild(MatPaginator) paginator: MatPaginator;
    element: any[] = [];
    selectedRowIndex: number = -1;
    selectedEmergencyIndex: number = -1;
    // public element: any[] = [];
    personsearch: SearchContact;
    displayedColumns = ['personIdentificationNumber', 'firstName', 'middleName', 'lastName', 'mobileNumber' , 'Gender','enrollmentNumber','Add'];


    //dataSource = new SearchDataSource(this.searchService, this.personsearch);
    dataSource = new MatTableDataSource();
    constructor(private searchService: SearchService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone) {
        this.personsearch = new SearchContact();

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
        //this.zone.run(() => { this.router.navigate(['/re/patientprofile'], { relativeTo: this.route }); });
        //this.router.navigateByUrl('/patientprofile');
        //console.log(this.router.navigateByUrl('/patientprofile'));
    }


    OnKeyUp() {

       
        //this.dataSource = new SearchDataSource(this.searchService, this.personsearch);
        this.LoadData();
        console.log(this.dataSource);


    }

    LoadData(){

        if (this.personsearch == undefined) {
            return Observable.from([]);

        } else {

            this.searchService.searchPersonContact(this.personsearch).subscribe(data => {


                this.dataSource.data = data["personSearch"];
                console.log(this.dataSource.data);


            });




        }
    }
    highlight(row) {
        this.selectedRowIndex = row.id;
    }


}


export class Element {

    Id: number;
    PersonId: number;
    FirstName: string;
    MiddleName: string;
    LastName: string;
    PhysicalAddress?: string;
    MobileNumber?: string;
    AlternativeNumber?: string;
    EmailAddress?: string;
    EnrollmentNumber?: string;
    PersonIdentificationNumber?: string;
    Gender?: string;
    


}

export class SearchDataSource extends DataSource<any>{
    element: any[] = [];
    constructor(private searchService: SearchService, private search: SearchContact) {
        super();

    }

    connect(): Observable<any[]> {
        if (this.search == undefined) {
            return Observable.from([]);

        } else {

            this.searchService.searchPersonContact(this.search).subscribe((data: any) => {


                this.element = data["personSearch"];
                console.log(this.element);


            });
            return Observable.of(this.element);


        }

    }

    disconnect() { }
}
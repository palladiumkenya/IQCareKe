import { Search } from '../_models/search';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    displayedColumns = ['enrollmentNumber', 'firstName', 'middleName', 'lastName', 'dateOfBirth'];
    dataSource = new MatTableDataSource();
    clientSearch: Search;

    constructor() {
        this.clientSearch = new Search();
    }

    ngOnInit() {
    }

    OnKeyUp(event) {
        console.log('here', event.target.value);
        if (event.target.value.length > 2) {

        }
    }

    doSearch() {

    }

}

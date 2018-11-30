import { PsmartService } from '../_services/psmart.service';
import { Component, OnInit } from '@angular/core';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-psmart',
    templateUrl: './psmart.component.html',
    styleUrls: ['./psmart.component.css']
})
export class PsmartComponent implements OnInit {

    displayedColumns = ['diagnosisMode', 'result', 'resultDate', 'resultCategory', 'mflCode'];
    personId: number;

    constructor(private psmartService: PsmartService) { }

    dataSource = new PsmartDataSource(this.psmartService, this.personId);

    ngOnInit() {
        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.dataSource = new PsmartDataSource(this.psmartService, this.personId);
        this.getPsmartData(this.personId);
    }

    getPsmartData(personId: number) {
        this.psmartService.getPersonPsmartData(personId).subscribe(data => {
        }, err => {
            console.log(err);
        });
    }
}

export class PsmartDataSource extends DataSource<any> {
    constructor(private psmartService: PsmartService, private personId: number) {
        super();
    }

    connect(): Observable<any[]> {
        return this.psmartService.getPersonPsmartData(this.personId);
    }

    disconnect() {

    }
}

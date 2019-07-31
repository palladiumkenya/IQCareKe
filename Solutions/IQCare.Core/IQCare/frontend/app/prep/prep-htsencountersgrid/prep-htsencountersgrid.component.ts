import { Component, OnInit, NgZone, Input, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';
import { PrepService } from '../_services/prep.service';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog } from '@angular/material';


@Component({
    selector: 'app-prep-htsencountersgrid',
    templateUrl: './prep-htsencountersgrid.component.html',
    styleUrls: ['./prep-htsencountersgrid.component.css']
})
export class PrepHtsencountersgridComponent implements OnInit {
    @Input('personId') personId: number;
    displayedColumns = ['encounterDate', 'testType', 'provider', 'resultOne',
        'resultTwo', 'finalResult', 'consent', 'partnerListingConsent', 'serviceArea'];

    HtsEncountersList: any[] = [];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    HTSEncounterDataSource = new MatTableDataSource(this.HtsEncountersList);
    constructor(private prepService: PrepService) { }

    ngOnInit() {
   this.getHTSEncounter();
    }

    public getHTSEncounter() {

        this.prepService.getAllHTSEncounterBypersonId(this.personId).subscribe(res => {
            if (res.length == 0)
                return;
            console.log(res);
            res.forEach(encounter => {
                this.HtsEncountersList.push({
                    encounterDate: encounter.encounterDate,
                    testType: encounter.testType,
                    provider: encounter.provider,
                    resultOne: encounter.resultOne,
                    resultTwo:encounter.resultTwo,
                    finalResult: encounter.finalResult,
                    consent: encounter.consent,
                    partnerListingConsent: encounter.partnerListingConsent,
                    serviceArea: encounter.serviceArea
                });
            });

           console.log(this.HtsEncountersList);
            this.HTSEncounterDataSource = new MatTableDataSource(this.HtsEncountersList);
            this.HTSEncounterDataSource.paginator = this.paginator;

        }, (error) => {
            console.log(error + "An error occurred while loading the hts details");
        });
    }



}
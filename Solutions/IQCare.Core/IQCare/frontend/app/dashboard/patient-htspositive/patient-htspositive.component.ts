import { Component, OnInit, Inject, ViewChild, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PersonHomeService } from '../services/person-home.service';
import { ActivatedRoute, Router, } from '@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { SearchService } from '../../registration/_services/search.service';
import { NgxSpinnerService } from 'ngx-spinner';




@Component({
    selector: 'app-patient-htspositive',
    templateUrl: './patient-htspositive.component.html',
    styleUrls: ['./patient-htspositive.component.css']
})

export class PatientHtsPositiveComponent implements OnInit {
    personId: number;
    serviceId: number;
    patientId: number;
    height: number;
    weight: number;
    ageNumber: number;
    ageInMonths; number;
    enrolledServices: any[] = [];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    services: any[] = [];
    displayedColumns = ['encounterDate', 'testType', 'provider', 'resultOne',
        'resultTwo', 'finalResult', 'consent', 'partnerListingConsent', 'serviceArea'];
    HtsEncountersList: any[] = [];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    HTSEncounterDataSource = new MatTableDataSource(this.HtsEncountersList);
    constructor(public personHomeService: PersonHomeService,
        public zone: NgZone,
        private searchService: SearchService,
        private route: ActivatedRoute,
        private router: Router,
        private spinner: NgxSpinnerService,
        private dialogRef: MatDialogRef<PatientHtsPositiveComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
        this.personId = data.personId;
        this.serviceId = data.serviceId;
        this.patientId = data.patientId;
        this.ageInMonths = data.ageInMonths;
        this.ageNumber = data.ageNumber;

    }
    ngOnInit() {
        this.getHtsEncounters();

    }
    public getHtsEncounters() {
    

        this.personHomeService.getAllHTSEncounterBypersonId(this.personId).subscribe(res => {
            if (res.length == 0)
                return;
            console.log(res);
            res.forEach(encounter => {
                this.HtsEncountersList.push({
                    encounterDate: encounter.encounterDate,
                    testType: encounter.testType,
                    provider: encounter.provider,
                    resultOne: encounter.resultOne,
                    resultTwo: encounter.resultTwo,
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
        }, () => {
         
        });


    }
    public Continue() {
        this.dialogRef.close(true);
    }
    public Close() {
        this.dialogRef.close(false);
    }


}
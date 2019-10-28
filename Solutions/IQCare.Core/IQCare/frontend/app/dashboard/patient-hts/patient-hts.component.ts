import { Component, OnInit, Inject, ViewChild, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PersonHomeService } from '../services/person-home.service';
import { ActivatedRoute, Router, } from '@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { SearchService } from '../../registration/_services/search.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-patient-hts',
    templateUrl: './patient-hts.component.html',
    styleUrls: ['./patient-hts.component.css']
})
export class PatientHtsComponent implements OnInit {
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
        private dialogRef: MatDialogRef<PatientHtsComponent>,
        private spinner: NgxSpinnerService,
        @Inject(MAT_DIALOG_DATA) public data: any) {
        this.personId = data.personId;
        this.serviceId = data.serviceId;
        this.patientId = data.patientId;
        this.ageInMonths = data.ageInMonths;
        this.ageNumber = data.ageNumber;

    }


    ngOnInit() {
        this.getHtsEncounters();
        this.getAllServices();
        this.getPersonEnrolledServices(this.personId);
    }
    getPersonEnrolledServices(personId: number) {
        this.personHomeService.getPersonEnrolledServices(personId).subscribe((res) => {

            this.enrolledServices = res['personEnrollmentList'];
            // console.log(this.enrolledServices);

            if (this.enrolledServices && this.enrolledServices.length > 0) {
                this.patientId = this.enrolledServices[0]['patientId'];
            }
            this.patientIdentifiers = res['patientIdentifiers'];
            this.identifiers = res['identifiers'];
        });

    }

    getAllServices() {
        this.personHomeService.getAllServices().subscribe((res) => {
            this.services = res;
            /*console.log(this.services);
            console.log(this.services);*/
            let index: number;
            index = this.services.findIndex(x => x.code == 'HTS');
            /*console.log(index);*/
        });
    }
    isPersonServiceEnrolled(service: string) {
        let index: number;
        index = this.services.findIndex(x => x.code == service);

        if (this.enrolledServices && this.enrolledServices.length > 0) {
            let returnValue = false;

            for (let i = 0; i < this.enrolledServices.length; i++) {
                if (this.enrolledServices[i].serviceAreaId == this.services[index]['id']) {
                    returnValue = true;
                }

            }

            if (returnValue == false) {
                this.dialogRef.close();
                localStorage.setItem('ageNumber', this.ageNumber.toString());
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/enrollment/hts/' + this.personId + '/' + this.services[index]['id'] + '/'
                        + this.services[index]['code']],
                        { relativeTo: this.route });
                });
            } else {
                this.dialogRef.close();
                localStorage.removeItem('personId');
                localStorage.removeItem('patientId');
                localStorage.removeItem('partnerId');
                localStorage.removeItem('htsEncounterId');
                localStorage.removeItem('patientMasterVisitId');
                localStorage.removeItem('isPartner');
                localStorage.removeItem('editEncounterId');
                localStorage.removeItem('ageInMonths');

                this.searchService.lastHtsEncounter(this.personId).subscribe((res) => {
                    if (res['encounterId']) {
                        localStorage.setItem('htsEncounterId', res['encounterId']);
                    }
                    if (res['patientMasterVisitId'] > 0) {
                        localStorage.setItem('patientMasterVisitId', res['patientMasterVisitId']);
                    }

                    this.zone.run(() => {
                        localStorage.setItem('personId', this.personId.toString());
                        localStorage.setItem('patientId', this.patientId.toString());
                        localStorage.setItem('serviceAreaId', this.services[index]['id'].toString());
                        localStorage.setItem('ageInMonths', this.ageInMonths);
                        this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                    });
                });

            }
        }
    }
    public Continue() {
        this.dialogRef.close();
        this.zone.run(() => {
            this.router.navigate(['/prep/riskassessment/' + this.patientId + '/' + this.personId
                + '/' + this.serviceId], { relativeTo: this.route });
        });
    }

    public DoHts() {
        this.isPersonServiceEnrolled('HTS');
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

    public Close() {
        this.dialogRef.close();
    }

}

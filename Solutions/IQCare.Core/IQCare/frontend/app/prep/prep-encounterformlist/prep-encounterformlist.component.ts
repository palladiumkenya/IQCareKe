import { Component, OnInit,NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { PersonView } from '../../records/_models/personView';
import { SearchService } from '../../registration/_services/search.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { Subscription } from 'rxjs';
import { EncounterService } from '../../shared/_services/encounter.service';

import { PrepCheckinComponent } from './../prep-checkin/prep-checkin.component';
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';

import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
@Component({
  selector: 'app-prep-encounterformlist',
  templateUrl: './prep-encounterformlist.component.html',
  styleUrls: ['./prep-encounterformlist.component.css'],
  providers: [PersonHomeService,SearchService]
})
export class PrepEncounterformlistComponent implements OnInit {
    personId: number;
   public personView$: Subscription;
    Encounterformlistgroup: FormGroup;
    prepEncounterType: LookupItemView[];
     
    public person: PersonView;
    patientId: number;
    userId: number;
    serviceAreaId: number;
    enrolledServices: any[] = [];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    services: any[] = [];
  constructor(
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private personService: PersonHomeService,
    private route: ActivatedRoute,
    private encounterService: EncounterService,
    private snotifyService: SnotifyService,
    private searchService: SearchService,
    private _formBuilder: FormBuilder,
    public zone: NgZone,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.params.subscribe((params) => {
        const {personId, patientId, serviceId} = params;
        this.patientId = patientId;
        this.personId = personId;
        this.serviceAreaId = serviceId;
    });

    this.route.data.subscribe(
        (res) => {
            const { prepEncounterTypeOption } = res;
            this.prepEncounterType = prepEncounterTypeOption;
        });
  
   this.userId = JSON.parse(localStorage.getItem('appUserId'));
    this.getPatientDetailsById(this.personId);
    this.getAllServices();
    this.getPersonEnrolledServices(this.personId);
  
    this.Encounterformlistgroup = this._formBuilder.group({
       encounterforms: new FormControl('', [Validators.required]),
    });
  }

  public getPatientDetailsById(personId: number) {
    this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
        p => {
            this.person = p;




        },
        (err) => {
            this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                this.notificationService.getConfig());
        },
        () => {
            // console.log(this.personView$);
        });
}


  getAllServices() {
    this.personService.getAllServices().subscribe((res) => {
        this.services = res;
        /*console.log(this.services);
        console.log(this.services);*/
        let index: number;
        index = this.services.findIndex(x => x.code == 'HTS');
        /*console.log(index);*/
    });
}


  getPersonEnrolledServices(personId: number) {
    this.personService.getPersonEnrolledServices(personId).subscribe((res) => {

        this.enrolledServices = res['personEnrollmentList'];
        // console.log(this.enrolledServices);

        if (this.enrolledServices && this.enrolledServices.length > 0) {
            this.patientId = this.enrolledServices[0]['patientId'];
        }
        this.patientIdentifiers = res['patientIdentifiers'];
        this.identifiers = res['identifiers'];
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
            localStorage.setItem('ageNumber', this.person.ageNumber.toString());
            this.zone.run(() => {
                this.router.navigate(['/dashboard/enrollment/hts/' + this.personId + '/' + this.services[index]['id'] + '/'
                    + this.services[index]['code']],
                    { relativeTo: this.route });
            });
        } else {

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
                    localStorage.setItem('ageInMonths', this.person.ageInMonths);
                    this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                });
            });

        }
    }
}
  clickEncounter() {
   const {encounterforms} = this.Encounterformlistgroup.value;
   console.log(encounterforms);
  
if (encounterforms == 'monthlyrefill')
{
    this.zone.run(() => {
        this.router.navigate(['/prep/monthlyrefill/' + '/' + this.patientId + '/' + this.personId + '/'
            + this.serviceAreaId],
            { relativeTo: this.route });
    });

} else if (encounterforms == 'preptermination') {
    this.zone.run(() => {
        this.router.navigate(['/prep/prepcareend/' + '/' + this.patientId + '/' + this.personId + '/'
            + this.serviceAreaId],
            { relativeTo: this.route });
    });


} else if (encounterforms == 'prepencounter') {

    const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(PrepCheckinComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
                    EncounterDate: data.visitdate,
                    PatientId: this.patientId,
                    EncounterType: this.prepEncounterType[0].itemId,
                    ServiceAreaId: this.serviceAreaId,
                    UserId: this.userId
                };

                this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
                    (result) => {
                        localStorage.setItem('visitDate', data.visitdate);

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/prep/encounter/' + '/' + this.patientId + '/' + this.personId + '/'
                                + result['patientEncounterId'] + '/' + result['patientMasterVisitId']],
                                { relativeTo: this.route });
                        });
                    },
                    (error) => {
                        this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
                    },
                    () => {

                    }
                );
            }
        );


} else if (encounterforms == 'riskassessment' ) {

    this.zone.run(() => {
        this.router.navigate(['/prep/riskassessment/' + '/' + this.patientId + '/' + this.personId + '/'
            + this.serviceAreaId],
            { relativeTo: this.route });
    });


} else if (encounterforms == 'hts') {
    this.isPersonServiceEnrolled('HTS');

} else if (encounterforms == 'vitals') {
    this.zone.run(() => {
        this.router.navigate(['/clinical/triage/' + this.patientId + '/' + this.personId], { relativeTo: this.route });
    });
} else {
 
    
}




  }

}

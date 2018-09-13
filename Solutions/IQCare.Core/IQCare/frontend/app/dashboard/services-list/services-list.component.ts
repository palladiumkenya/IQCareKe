import { Component, OnInit, Input, NgZone } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientView } from '../_model/PatientView';

@Component({
    selector: 'app-services-list',
    templateUrl: './services-list.component.html',
    styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
    @Input('personId') personId: number;
    @Input('services') services: any[];
    enrolledServices: any[];
    hasItems: boolean = false;
    public patientId: number;
    public Patient: PatientView = {};
    constructor(private personhomeservice: PersonHomeService,
        public zone: NgZone,
        private router: Router,
        private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.getPersonEnrolledServices(this.personId);
        this.getPatientByPersonId(this.personId);

        /*this.route.params.subscribe(params => {
            console.log(params);
            this.patientId = params['serviceAreaId'];
        });*/
    }

    getPersonEnrolledServices(personId: number) {
        this.personhomeservice.getPersonEnrolledServices(personId).subscribe((res) => {
            this.enrolledServices = res['personEnrollmentList'];
            if (this.enrolledServices.length > 0) {
                this.hasItems = true;
            }
            console.log(this.enrolledServices);
        });
    }

    getPatientByPersonId(personId: number) {
        this.personhomeservice.getPatientByPersonId(personId).subscribe((res) => {
            this.Patient = res;
            console.log(this.Patient);
            console.log('patentId:' + this.Patient.patientId);
            if (this.Patient.patientId > 0) {
                this.hasItems = true;
                this.patientId = this.Patient.patientId;
            }
        });
    }

    enrollToService(serviceId: number) {
        this.zone.run(() => {
            this.router.navigate(['/dashboard/enrollment/' + this.personId + '/' + serviceId],
                { relativeTo: this.route });
        });
    }

    newEncounter(serviceId: number) {
        this.zone.run(() => {
            // :patientId/:personId/:serviceAreaId
            this.router.navigate(['/pmtct/anc/' + this.Patient.patientId + '/' + this.personId + '/' + serviceId],
                { relativeTo: this.route });
        });
    }
}

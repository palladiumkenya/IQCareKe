import { Component, OnInit, Input, NgZone } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import { Router, ActivatedRoute } from '@angular/router';

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

    constructor(private personhomeservice: PersonHomeService,
        public zone: NgZone,
        private router: Router,
        private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.getPersonEnrolledServices(this.personId);
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

    enrollToService(serviceId: number) {
        this.zone.run(() => {
            this.router.navigate(['/dashboard/enrollment/' + this.personId + '/' + serviceId],
                { relativeTo: this.route });
        });
    }

    newEncounter(serviceId: number) {
        this.zone.run(() => {
            this.router.navigate(['/pmtct/anc/' + 5 + '/5/' + serviceId], { relativeTo: this.route });
        });
    }
}

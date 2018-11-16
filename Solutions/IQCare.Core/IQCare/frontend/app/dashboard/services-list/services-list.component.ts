import { NotificationService } from './../../shared/_services/notification.service';
import { Component, OnInit, Input, NgZone } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientView } from '../_model/PatientView';
import { SnotifyService } from 'ng-snotify';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';

@Component({
    selector: 'app-services-list',
    templateUrl: './services-list.component.html',
    styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
    @Input('personId') personId: number;
    @Input('services') services: any[];
    @Input('person') person: any;

    enrolledServices: any[];
    patientIdentifiers: any[];
    enrolledService: any[] = [];
    identifiers: any[] = [];

    hasItems: boolean = false;
    public patientId: number;
    public Patient: PatientView = {};

    constructor(
        private personhomeservice: PersonHomeService,
        public zone: NgZone,
        private router: Router,
        private route: ActivatedRoute,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private store: Store<AppState>) {
    }

    ngOnInit() {
        this.getPersonEnrolledServices(this.personId);
    }

    getPersonEnrolledServices(personId: number) {
        this.personhomeservice.getPersonEnrolledServices(personId).subscribe((res) => {
            this.enrolledServices = res['personEnrollmentList'];
            if (this.enrolledServices && this.enrolledServices.length > 0) {
                this.patientId = this.enrolledServices[0]['patientId'];
            }
            this.patientIdentifiers = res['patientIdentifiers'];
            this.identifiers = res['identifiers'];
        });
    }

    enrollToService(serviceId: number) {
        if (serviceId == 1) {
            this.snotifyService.error('Please Access CCC from the Greencard menu', 'Encounter History',
                this.notificationService.getConfig());
            return;
        }
        this.zone.run(() => {
            this.router.navigate(['/dashboard/enrollment/' + this.personId + '/' + serviceId],
                { relativeTo: this.route });
        });
    }

    newEncounter(serviceId: number) {
        const selectedService = this.services.filter(obj => obj.id == serviceId);
        if (selectedService && selectedService.length > 0) {
            const service = selectedService[0]['code'];
            localStorage.setItem('selectedService', service.toLowerCase());

            this.store.dispatch(new Consent.SelectedService(service.toLowerCase()));

            switch (selectedService[0]['code']) {
                case 'HTS':
                    this.zone.run(() => {
                        localStorage.setItem('personId', this.personId.toString());
                        localStorage.setItem('patientId', this.patientId.toString());
                        localStorage.setItem('serviceAreaId', serviceId.toString());
                        this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                    });
                    break;
                case 'CCC':
                    this.snotifyService.error('Please Access CCC from the Greencard menu', 'Encounter History',
                        this.notificationService.getConfig());
                    break;
                default:
                    this.zone.run(() => {
                        this.router.navigate(
                            ['/pmtct/patient-encounter/' + this.patientId + '/' + this.personId + '/' + serviceId + '/'
                                + selectedService[0]['code']],
                            { relativeTo: this.route });
                    });
                    break;
            }
        }
    }

    isPersonServiceEnrolled(service: any): boolean {
        if (this.enrolledServices && this.enrolledServices.length > 0) {
            let returnValue = false;
            for (let i = 0; i < this.enrolledServices.length; i++) {
                if (this.enrolledServices[i].serviceAreaId == service.id) {
                    returnValue = true;
                }
            }
            return returnValue;
        } else {
            return false;
        }
    }

    isServiceEligible(serviceAreaId: number) {
        const selectedService = this.services.filter(obj => obj.id == serviceAreaId);
        let isEligible: boolean = true;
        if (selectedService && selectedService.length > 0) {
            switch (selectedService[0]['code']) {
                case 'ANC':
                    if (this.person.gender == 'Female' && (this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'PNC':
                    if (this.person.gender == 'Female' && (this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'Maternity':
                    if (this.person.gender == 'Female' && (this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'HEI':
                    if (this.person.ageNumber <= 2) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
            }
        }
        return isEligible;
    }

    getServiceEnrollmentDetails(service: any) {
        this.enrolledService = this.enrolledServices.filter(obj => obj.serviceAreaId == service.id);
        this.enrolledService['identifiers'] = [];
        for (const enrollService of this.enrolledService) {
            const serviceIdentifiers = this.patientIdentifiers.filter(obj => obj.patientEnrollmentId == enrollService.id);
            for (let i = 0; i < serviceIdentifiers.length; i++) {
                const selectedIdentifier = this.identifiers.filter(obj => obj.id == serviceIdentifiers[i]['identifierTypeId']);
                if (selectedIdentifier && selectedIdentifier.length > 0) {
                    serviceIdentifiers[i]['identifierTypeId'] = selectedIdentifier[0]['code'];
                }
            }
            this.enrolledService['identifiers'].push(serviceIdentifiers);
        }
        return this.enrolledService;
    }
}

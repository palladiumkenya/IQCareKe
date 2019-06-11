import { AppEnum } from './../../shared/reducers/app.enum';
import { AppStateService } from './../../shared/_services/appstate.service';
import { NotificationService } from './../../shared/_services/notification.service';
import { Component, OnInit, Input, NgZone } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientView } from '../_model/PatientView';
import { SnotifyService } from 'ng-snotify';
import { Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { SearchService } from '../../registration/_services/search.service';
import { EncounterDetails } from '../_model/HtsEncounterdetails';


@Component({
    selector: 'app-services-list',
    templateUrl: './services-list.component.html',
    styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
    @Input('personId') personId: number;
    @Input('services') services: any[];
    @Input('person') person: any;
    @Input('encounterDetail') encounterDetail: EncounterDetails;
    @Input('personVitalWeight') weight: number;
    @Input('riskencounters') riskencounter: any[];
    enrolledServices: any[];
    PatientCCCEnrolled: boolean = false;
    patientIdentifiers: any[];
    enrolledService: any[] = [];
    identifiers: any[] = [];
    patientvitals: any[] = [];
    vitalWeight: number = 0;
    Vitaldone: boolean = true;

    htseligibility: string = ' ';
    EligibilityInformation: any[] = [];
    HTSEligible: boolean = false;
    hasItems: boolean = false;
    public patientId: number;
    public Patient: PatientView = {};
    RiskDone: boolean = true;
    constructor(
        private personhomeservice: PersonHomeService,
        public zone: NgZone,
        private router: Router,
        private route: ActivatedRoute,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private store: Store<AppState>,
        private searchService: SearchService,
        private appStateService: AppStateService,
    ) {
    }

    ngOnInit() {
        this.EligibilityInformation = [];
        this.vitalWeight = this.weight;

        this.getPersonEnrolledServices(this.personId);

        console.log(this.encounterDetail);

        console.log('RiskEncounter');
        console.log(this.riskencounter)

        if (this.EligibilityInformation.length > 0) {
            this.htseligibility = this.EligibilityInformation.join(' ,');
            console.log(this.EligibilityInformation);
            console.log(this.htseligibility);
        }
        console.log('HTSEligibility');
        console.log(this.HTSEligible);
        console.log(this.personId);



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


    enrollToService(serviceId: number, serviceCode: string) {
        if (!this.person.dateOfBirth || this.person.dateOfBirth == '') {
            this.snotifyService.warning('Please update: ' + this.person.firstName + ' ' + this.person.midName + ' ' + this.person.lastName
                + ', DOB before enrollment', 'Enrollment', this.notificationService.getConfig());
            return;
        }

        const selectedService = this.services.filter(obj => obj.id == serviceId);
        if (selectedService && selectedService.length > 0) {
            const service = selectedService[0]['code'];
            switch (selectedService[0]['code']) {
                case 'HTS':
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/enrollment/hts/' + this.personId + '/' + serviceId + '/' + serviceCode],
                            { relativeTo: this.route });
                    });
                    break;
                case 'CCC':
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/enrollment/ccc/' + this.personId + '/' + serviceId + '/' + serviceCode],
                            { relativeTo: this.route });
                    });
                    break;
                case 'PREP':
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/enrollment/prep/' + this.personId + '/' + serviceId + '/' + serviceCode],
                            { relativeTo: this.route });
                    });
                    break;
                default:
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/enrollment/' + this.personId + '/' + serviceId + '/' + serviceCode],
                            { relativeTo: this.route });
                    });
                    break;
            }
        }
    }

    public editEnrollment(serviceId: number, serviceCode: string) {
        switch (serviceCode) {
            case 'CCC':
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/enrollment/ccc/update/' + this.personId + '/' + serviceId + '/' + serviceCode + '/1'],
                        { relativeTo: this.route });
                });
                break;
            case 'HTS':
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/enrollment/hts/update/' + this.personId + '/' + serviceId + '/' + serviceCode + '/1'],
                        { relativeTo: this.route });
                });
                break;
            case 'PREP':
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/enrollment/prep/update/' + this.personId + '/' + serviceId + '/' + serviceCode + '/1'],
                        { relativeTo: this.route });
                });

                break;
            default:
                console.log('test default');
                break;
        }
    }

    newTriage() {
        localStorage.setItem('selectedService', 'triage');
        this.store.dispatch(new Consent.SelectedService('triage'));
        this.zone.run(() => {
            this.router.navigate(['/clinical/triage/' + this.patientId + '/' + this.personId],
                { relativeTo: this.route });
        });
    }

    newEncounter(serviceId: number) {
        const selectedService = this.services.filter(obj => obj.id == serviceId);
        if (selectedService && selectedService.length > 0) {
            const service = selectedService[0]['code'];
            localStorage.setItem('selectedService', service.toLowerCase());

            this.store.dispatch(new Consent.SelectedService(service.toLowerCase()));
            this.appStateService.addAppState(AppEnum.PATIENTID, this.personId, this.patientId).subscribe();

            switch (selectedService[0]['code']) {
                case 'HTS':
                    localStorage.removeItem('personId');
                    localStorage.removeItem('patientId');
                    localStorage.removeItem('partnerId');
                    localStorage.removeItem('htsEncounterId');
                    localStorage.removeItem('patientMasterVisitId');
                    localStorage.removeItem('isPartner');
                    localStorage.removeItem('editEncounterId');

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
                            localStorage.setItem('serviceAreaId', serviceId.toString());
                            this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                        });
                    });
                    break;
                case 'CCC':
                    this.searchService.setSession(this.personId, this.patientId).subscribe((res) => {
                        console.log(res);
                        window.location.href = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                            '/IQCare/CCC/Patient/PatientHome.aspx';
                    });
                    /*this.snotifyService.error('Please Access CCC from the Greencard menu', 'Encounter History',
                        this.notificationService.getConfig());*/
                    break;
                    case 'PREP':
                    this.zone.run(() => {
                        this.router.navigate(
                            ['/prep'] ,
                            { relativeTo: this.route });
                    });
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
    navigateToRiskAssessment(serviceId) {
        this.zone.run(() => {
            this.router.navigate(['/prep/riskassessment/' + this.patientId + '/' + this.personId
                + '/' + serviceId], { relativeTo: this.route });
        });
    }
    navigateToTriage() {
        this.zone.run(() => {
            this.router.navigate(['/clinical/triage/' + this.patientId + '/' + this.personId], { relativeTo: this.route });
        });
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
        let isCCCEnrolled;


        if (this.enrolledServices) {
            isCCCEnrolled = this.enrolledServices.filter(obj => obj.serviceAreaId == 1);
            if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                this.PatientCCCEnrolled = true;
            }
        }

        const selectedService = this.services.filter(obj => obj.id == serviceAreaId);
        let isEligible: boolean = false;
        if (selectedService && selectedService.length > 0) {
            switch (selectedService[0]['code']) {
                case 'ANC':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'PNC':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'Maternity':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'HEI':
                    if (this.person.dateOfBirth && this.person.ageNumber <= 2) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'HTS':
                    if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                        isEligible = false;
                    } else {
                        isEligible = true;
                    }
                    break;
                case 'CCC':
                    isEligible = true;
                    break;
                case 'PREP':
                    if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                        isEligible = false;


                    } else {
                        if (this.person.ageNumber < 15) {
                            isEligible = false;
                            if (this.EligibilityInformation.length > 0) {
                                if (this.EligibilityInformation.includes('Age below 15') == false) {
                                    this.EligibilityInformation.push('Age below 15');
                                }
                            }
                            else {
                                this.EligibilityInformation.push('Age below 15');
                            }

                            /*  if (this.EligibilityInformation.length > 0) {
      
                                 this.htseligibility = this.EligibilityInformation.join(', ');
                             } */
                        } else {
                            this.HTSEligible = this.getHTSEligibility();
                            isEligible = this.HTSEligible;
                            if (isEligible == true) {
                                if (this.vitalWeight > 0 && this.vitalWeight < 35) {
                                    isEligible = false;
                                    if (this.EligibilityInformation.length > 0) {
                                        if (this.EligibilityInformation.includes('Weight less than 35') == false) {
                                            this.EligibilityInformation.push('Weight less than 35');
                                        }
                                    } else {
                                        this.EligibilityInformation.push('Weight less than 35');
                                    }





                                } else if (this.vitalWeight == 0) {
                                    isEligible = false;
                                    this.Vitaldone = false;
                                    /*  if (this.EligibilityInformation.length > 0) {
                                         if (this.EligibilityInformation.includes('Vitals not done') == false) {
                                             this.EligibilityInformation.push('Vitals not done');
                                         }
                                     } else {
                                         this.EligibilityInformation.push('Vitals not done');
                                     } */


                                }
                                else {
                                    isEligible = true;
                                    if (isEligible == true) {
                                        if (this.riskencounter.length <= 0) {
                                            isEligible = false;
                                            this.RiskDone = false;
                                        }
                                    }

                                }

                            }

                        }
                    }
                    console.log('*******Called after Break****');
                    console.log(this.EligibilityInformation);
                    break;

            }
        }

        this.htseligibility = '';
        this.htseligibility = this.EligibilityInformation.join(',');
        return isEligible;
    }
    getHTSEligibility(): boolean {
        let isCCCEnrolled;
        if (this.enrolledServices) {
            isCCCEnrolled = this.enrolledServices.filter(obj => obj.serviceAreaId == 1);
        }
        let isEligible: boolean = false;
        console.log('gethtseligibility');
        console.log(this.encounterDetail);
        if (this.encounterDetail != undefined) {
            if (this.encounterDetail.finalResult == undefined) {
                isEligible = false;
                this.EligibilityInformation.push('HTS not done');
            } else if (this.encounterDetail.finalResult == 'Negative') {
                isEligible = true;
            } else if (this.encounterDetail.finalResult == 'Positive') {

                isEligible = false;
            } else {
                isEligible = true;
            }
        } else {
            if (isCCCEnrolled != undefined) {
                if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                    isEligible = false;
                } else {
                    if (this.EligibilityInformation.length > 0) {
                        if (this.EligibilityInformation.includes('HTS not done') == false) {
                            this.EligibilityInformation.push('HTS not done');
                        }
                    } else {
                        this.EligibilityInformation.push('HTS not done');
                    }



                }
            } else {
                if (this.EligibilityInformation.length > 0) {
                    if (this.EligibilityInformation.includes('HTS not done') == false) {
                        this.EligibilityInformation.push('HTS not done');
                    }
                }
                else {
                    this.EligibilityInformation.push('HTS not done');
                }



            }

        }


        return isEligible;

    }
    validationService(code: string, vital: Boolean): Boolean {
        let visibility = true;
        if (code == 'PREP') {
            visibility = false;
        } else {
            visibility = true;
        }
        return visibility;
    }
    validationHTS(code: string, HTSEligible: Boolean): Boolean {
        let visibility = false;
        if (code == 'PREP') {
            if (HTSEligible == false) {
                visibility = true;
            } else {
                visibility = false;
            }
        } else {
            visibility = false;
        }
        return visibility;
    }
    validationTriage(code: string, vital: Boolean): Boolean {
        let visibility = false;
        if (code == 'PREP') {
            if (vital == false) {
                visibility = true;
            } else {
                visibility = false;
            }

        } else {
            visibility = false;
        }
        return visibility;
    }
    validationRiskAssessment(code: string, riskassessment: Boolean): Boolean {
        let visibility = false;
        if (code == 'PREP') {
            if (riskassessment == false) {
                visibility = true;
            } else {
                visibility = false;
            }

        } else {
            visibility = false;
        }
        return visibility;
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

import {Component, OnInit, OnDestroy, NgZone} from '@angular/core';
import { NotificationService } from './../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import {ActivatedRoute, Router} from '@angular/router';
import { VisitDetails } from './../_models/visitDetails';
import { VisitDetailsService } from '../_services/visit-details.service';
import { PatientEducationEmitter } from '../emitters/PatientEducationEmitter';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { PreventiveService } from '../_models/PreventiveService';
import { Subscription } from 'rxjs/index';
import { ClientMonitoringCommand } from '../_models/ClientMonitoringCommand';
import { ClientMonitoringEmitter } from '../emitters/ClientMonitoringEmitter';
import { AncService } from '../_services/anc.service';
import { HAARTProphylaxisEmitter } from '../emitters/HAARTProphylaxisEmitter';
import { HaartProphylaxisCommand } from '../_models/HaartProphylaxisCommand';
import { PatientDrugAdministration } from '../_models/PatientDrugAdministration';
import { ReferralsEmitter } from '../emitters/ReferralsEmitter';
import { PatientReferral } from '../_models/PatientReferral';
import { ReferralAppointmentCommand } from '../_models/ReferralAppointmentCommand';
import { PatientAppointmet } from '../_models/PatientAppointmet';
import { PreventiveServiceEmitter } from '../emitters/PreventiveServiceEmitter';
import { PatientPreventiveService } from '../_models/PatientPreventiveService';
import { PatientProfile } from '../_models/patientProfile';
import { PregnancyViewModel } from '../_models/viewModel/PregnancyViewModel';
import { HIVTestingEmitter } from '../emitters/HIVTestingEmitter';

@Component({
    selector: 'app-anc',
    templateUrl: './anc.component.html',
    styleUrls: ['./anc.component.css']
})
export class AncComponent implements OnInit, OnDestroy {

    isLinear: true;
    visitDetails: VisitDetails;
    topics: PreventiveService[];
    patientDrug: PatientDrugAdministration[] = [];
    public preventiveServiceData: PreventiveService[] = [];

    public personId: number;
    public patientId: number;
    public serviceAreaId: number;
    public patientMasterVisitId: number;
    public patientEncounterId: number;
    public userId: number;
    public visitDate: Date;

    public saveVisitDetails$;
    public savePatientEducation$: Subscription;
    public saveClientMonitoring$: Subscription;
    public saveHaartProphylaxis$: Subscription;
    public saveReferralAppointment$: Subscription;
    public savePreventiveService$: Subscription;
    public getPatientPregnancy$: Subscription;
    public getPatientProfile$: Subscription;

    public pregnancy: PregnancyViewModel = {};
    public profile: PatientProfile = {};


    constructor(private route: ActivatedRoute, private visitDetailsService: VisitDetailsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService, private ancService: AncService,
        public zone: NgZone,
        private router: Router) {
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.personId = params['id'];
        });

        this.route.params.subscribe(params => {
            this.serviceAreaId = params['serviceAreaId'];
        });

        this.route.params.subscribe(params => {
            this.patientId = params['patientId'];
        });
        /*this.route.params.subscribe(params => {
            this.patientMasterVisitId = params['patientMasterVisitId'];
        });*/
    }


    public onSaveVisitDetails(data: VisitDetails): void {
        this.visitDate = data.VisitDate;

        this.saveVisitDetails$ = this.visitDetailsService.savePatientDetails(data)
            .subscribe(
                p => {
                    console.log(p);
                    const { patientMasterVisitId, pregancyId, profileId, patientEncounterId } = p;
                    this.patientMasterVisitId = patientMasterVisitId;
                    this.patientEncounterId = patientEncounterId;

                    this.snotifyService.success('Visit Details Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding VisitDetails' + err, 'VisitDetails service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.saveVisitDetails$);
                });
    }

    public onSavePatientEducation(data: PatientEducationEmitter): void {

        console.log('testing counselling');
        console.log(data);

        const patientEducation = {
            BreastExamDone: data.breastExamDone,
            TreatedSyphilis: data.treatedSyphilis,
            CreateBy: (this.userId < 1) ? 1 : this.userId,
            CounsellingTopics: data.counsellingTopics,
        } as PatientEducationCommand;

        this.savePatientEducation$ = this.ancService.savePatientEducation(patientEducation)
            .subscribe(
                p => {
                    console.log(p);
                    this.snotifyService.success('Patient Education Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding PatientEducation' + err, 'ANC service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.savePatientEducation$);
                });
    }

    public onSaveClientMonitoring(data: ClientMonitoringEmitter): void {
        const clientMonitoring = {
            PatientId: parseInt(this.patientId.toString(), 10),
            // PatientmasterVisitId: this.patientMasterVisitId,
            PatientMasterVisitId: 21,
            FacilityId: 755,
            WhoStage: parseInt(data.WhoStage.toString(), 10),
            ServiceAreaId: 3,
            ScreeningTypeId: 0,
            ScreeningDone: Boolean(data.cacxScreeningDone),
            ScreeningDate: new Date(),
            ScreeningTB: parseInt(data.screenedForTB.toString(), 10),
            CaCxMethod: parseInt(data.cacxMethod.toString(), 10),
            CaCxResult: parseInt(data.cacxResult.toString(), 10),
            Comments: data.cacxComments.toString(),
            ClinicalNotes: data.cacxComments.toString(),
            CreatedBy: (this.userId < 1) ? 1 : this.userId
        } as ClientMonitoringCommand;

        console.log(clientMonitoring);

        this.saveClientMonitoring$ = this.ancService.saveClientMonitoring(clientMonitoring)
            .subscribe(
                p => {
                    console.log(p);
                    this.snotifyService.success('Client Monitoring Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding client monitoring' + err, 'ANC service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.saveClientMonitoring$);
                });
    }

    public onSaveHaartProphylaxis(data: HAARTProphylaxisEmitter) {

        this.patientDrug.push(
            {
                PatientId: parseInt( this.patientId.toString() , 10) , PatientMasterVisitId: 12, DrugAdministered: data.nvpForBaby,
                Value: data.nvpForBaby, DeleteFlag: 0, Description: '', Id: 0,  CreatedBy: this.userId
            },
            {
                PatientId: parseInt( this.patientId.toString() , 10) , PatientMasterVisitId: 12,
                DrugAdministered: data.aztFortheBaby,
                Value: data.aztFortheBaby, DeleteFlag: 0, Description: '', Id: 0,  CreatedBy: this.userId
            },
            {
                PatientId: parseInt( this.patientId.toString() , 10) , PatientMasterVisitId: 12,
                Value: data.onArvBeforeANCVisit, DeleteFlag: 0, Description: '', Id: 0, CreatedBy: this.userId
            }
        );
        const haartProphylaxis = {
            PatientDrugAdministration: this.patientDrug,
            PatientChronicIllnesses: data.chronicIllness,
            OtherIllness: data.otherIllness
        } as HaartProphylaxisCommand;
        console.log('final output');
        console.log(haartProphylaxis);
        this.saveHaartProphylaxis$ = this.ancService.saveHaartProphylaxis(haartProphylaxis)
            .subscribe(
                p => {
                    console.log(p);
                    this.snotifyService.success('Haart Prophylaxis Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding Haart Prophylaxis' + err, 'ANC service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.saveHaartProphylaxis$);
                });
    }

    public onSavePreventiveService(data: PreventiveServiceEmitter) {

        console.log('test data')
        console.log(data);
        for (let i = 0; i < data.preventiveService.length; i++) {
           this.preventiveServiceData.push(
               {
                   Id: 0,
                   PatientId: 5,
                   PatientMasterVisitId: 12,
                   PreventiveServiceId: data.preventiveService[i]['preventiveServiceId'],
                   PreventiveServiceDate: data.preventiveService[i]['dateGiven'],
                   Description: data.preventiveService[i]['comments'],
                   NextSchedule: data.preventiveService[i]['nextSchedule']
               });
        }

        const preventiveService = {
            preventiveService: this.preventiveServiceData,
            InsecticideGivenDate: new Date(data.insecticideTreatedNetGivenDate),
            AntenatalExercise: data.antenatalExercise,
            PartnerTestingVisit: data.antenatalExercise,
            FinalHIVResult: data.finalHIVResult,
            InsecticideTreatedNet: data.insecticideTreatedNet,
            CreatedBy: this.userId
        } as PatientPreventiveService;
        console.log('preventive services');
console.log(preventiveService);
        this.savePreventiveService$ = this.ancService.savePreventiveServices(preventiveService)
            .subscribe(
                p => {
                    console.log(p);
                    this.snotifyService.success('Preventive Service Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding Preventive Service' + err, 'ANC service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.savePreventiveService$);
                });
    }

    public onSaveReferralAppointment(data: ReferralsEmitter) {
        const patientRef = {
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: 12,
            ReferredFrom: data.referredFrom,
            ReferredTo: data.referredTo,
            ReferralReason: 'n/a',
            ReferralDate: new Date(),
            RefferedBY: this.userId,
            DeleteFlag: 0,
            CreateBy: this.userId
        } as PatientReferral;

        const appointment = {
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: 12,
            AppointmentDate: new Date(data.nextAppointmentDate),
            ReasonId: 0,
            Description: data.serviceRemarks.toString(),
            StatusId: 0,
            DifferentiatedCareId: 0,
            DeleteFlag: 0,
            CreatedBy: this.userId
        } as PatientAppointmet;

        const referral = {
            PatientReferral: patientRef,
            PatientAppointment: appointment
        } as ReferralAppointmentCommand;

        this.saveReferralAppointment$ = this.ancService.saveReferralAppointment(referral)
            .subscribe(
                p => {
                    console.log(p);
                    this.snotifyService.success('Referral Appointment Added Successfully' + p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error Adding Referral Appointment' + err, 'ANC service',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.saveReferralAppointment$);
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/personhome/' + this.patientId],
                            { relativeTo: this.route });
                    });
                });
    }

    public getPatientPregnanc(patientId: number) {
        this.getPatientPregnancy$ = this.visitDetailsService.getPregnancyProfile(this.patientId)
            .subscribe(
                p => {
                    this.pregnancy = p;
                },
                (err) => {
                    this.snotifyService.error('Error fetching pregnancy' + err, 'Pregnancy Profile', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.pregnancy);
                }
            );
    }

    public onSaveHivStatus(data: HIVTestingEmitter) {
        this.personId = 1;
        this.serviceAreaId = 3;

        const htsAncEncounter = {
            'PersonId': this.personId,
            'ProviderId': this.userId,
            'PatientEncounterID': this.patientEncounterId,
            'PatientMasterVisitId': this.patientMasterVisitId,
            'PatientId': this.patientId,
            'EverTested': '',
            'MonthsSinceLastTest': '',
            'MonthSinceSelfTest': '',
            'TestedAs': '',
            'TestingStrategy': '',
            'EncounterRemarks': '',
            'TestEntryPoint': data.ancTestEntryPoint,
            'Consent': data.consentOption,
            'EverSelfTested': '',
            'GeoLocation': '',
            'HasDisability': '',
            'Disabilities': [],
            'TbScreening': '',
            'ServiceAreaId': this.serviceAreaId,
            'EncounterTypeId': '1',
            'EncounterDate': this.visitDate,
            'EncounterType': data.testingDone
        };

        this.ancService.saveHivStatus(htsAncEncounter).subscribe(
            (result) => {
                const { htsEncounterId } = result;
                const hivKitResults = [];
                let testRound;
                if (data.hivTest['itemName'] == 'HIV Test-1') {
                    testRound = 1;
                } else if (data.hivTest['itemName'] == 'HIV Test-2') {
                    testRound = 2;
                }

                hivKitResults.push({
                    'KitId': data.kitName,
                    'KitLotNumber': data.lotNumber,
                    'ExpiryDate': data.expiryDate,
                    'Outcome': data.testResult,
                    'TestRound': testRound
                });

                const finalResultsBody = {
                    'FinalResultHiv1': data.testResult,
                    'FinalResultHiv2': '',
                    'FinalResult': data.finalResult,
                    'FinalResultGiven': data.consentOption,
                    'AcceptedPartnerListing': data.consentOption,
                    'FinalResultsRemarks': ''
                };

                this.ancService.saveHivResults(
                    this.serviceAreaId,
                    this.patientMasterVisitId,
                    this.patientId,
                    this.userId,
                    htsEncounterId,
                    hivKitResults,
                    finalResultsBody).subscribe(
                        (res) => {
                            console.log(`final` + res);
                        }
                    );
            },
            (error) => {

            },
            () => {

            }
        );
    }
    // getPatientProfile();


    ngOnDestroy(): void {
        if (this.saveVisitDetails$) {
            this.saveVisitDetails$.unsubscribe();
            this.saveClientMonitoring$.unsubscribe();
            this.savePatientEducation$.unsubscribe();
            this.saveReferralAppointment$.unsubscribe();
            this.savePreventiveService$.unsubscribe();
        }
    }
}

import { RecordsService } from './../../records/_services/records.service';
import { ClientCircumcisionStatusCommand } from './../_models/commands/ClientCircumcisionStatusCommand';
import { AncService } from './../../pmtct/_services/anc.service';
import { PncService } from './../../pmtct/_services/pnc.service';
import { PrepService } from './../_services/prep.service';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, of, Subscription } from 'rxjs';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import { PrepStatusCommand } from '../_models/commands/PrepStatusCommand';
import { FamilyPlanningCommand } from '../../pmtct/_models/FamilyPlanningCommand';
import { FamilyPlanningMethodCommand } from '../../pmtct/_models/FamilyPlanningMethodCommand';
import { PatientChronicIllness } from '../../pmtct/_models/PatientChronicIllness';
import * as moment from 'moment';
import { AdverseEventsCommand } from '../_models/commands/AdverseEventsCommand';
import { AllergiesCommand } from '../_models/commands/AllergiesCommand';
import { PregnancyIndicatorCommand } from '../_models/commands/PregnancyIndicatorCommand';
import { NextAppointmentCommand } from '../../pmtct/maternity/commands/next-appointment-command';
import { MaternityService } from '../../pmtct/_services/maternity.service';
import { MatStepper } from '@angular/material';
import { PregnancyIndicatorLogCommand } from '../_models/commands/PregnancyIndicatorLogCommand';
import { FamilyPlanningEditCommand } from '../../pmtct/_models/FamilyPlanningEditCommand';
import { PatientFamilyPlanningMethodEditCommand } from '../../pmtct/_models/PatientFamilyPlanningMethodEditCommand';
import { PersonView } from '../../dashboard/_model/personView';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
@Component({
    selector: 'app-prep-encounter',
    templateUrl: './prep-encounter.component.html',
    styleUrls: ['./prep-encounter.component.css'],
    providers: [MaternityService, RecordsService, PersonHomeService]
})
export class PrepEncounterComponent implements OnInit {
    patientId: number;
    personId: number;
    userId: number;
    patientMasterVisitId: number;
    patientEncounterId: number;
    personGender: string;
    isEdit: number;
    Age: number = 20;
    isLinear: boolean = true;
    visitDate: Date;

    // Form Groups
    STIScreeningFormGroup: FormArray;
    CircumcisionStatusFormGroup: FormArray;
    FertilityIntentionsFormGroup: FormArray;
    ChronicIllnessFormGroup: Object[][];
    PrepStatusFormGroup: FormArray;
    AppointmentFormGroup: FormArray;
    LabInvestigationsFormGroup: FormArray;

    yesnoOptions: LookupItemView[];
    stiScreeningOptions: LookupItemView[];
    yesNoUnknownOptions: LookupItemView[];
    familyPlanningMethodsOptions: LookupItemView[];
    planningPregnancyOptions: LookupItemView[];
    yesNoDontKnowOptions: LookupItemView[];
    pregnancyOutcomeOptions: LookupItemView[];
    prepContraindicationsOptions: LookupItemView[];
    prepStatusOptions: LookupItemView[];
    reasonsPrepAppointmentNotGivenOptions: LookupItemView[];
    pregnancyStatusOptions: LookupItemView[];
    screenedForSTIOptions: LookupItemView[];
    DateStatus?: Date;
    STIScreeningAndTreatmentOptions: any[] = [];
    CircumcisionStatusOptions: any[] = [];
    FertilityIntentionsOptions: any[] = [];
    PregnancyOutcomeOptions: any[] = [];
    PrepStatusOptions: any[] = [];
    PrepAppointmentOptions: any[] = [];
    minDate: Date;
    serviceId: number;
    public chronic_illness_data: PatientChronicIllness[] = [];
    public adverseEvents_data: AdverseEventsCommand[] = [];
    public allergies_data: AllergiesCommand[] = [];

    @ViewChild('stepper') stepper: MatStepper;

    // optional depending on sex
    isOptionalObsGyn: boolean = false;
    isOptionalCircumcision: boolean = false;
    public person: PersonView;
    public personView$: Subscription;

    constructor(private route: ActivatedRoute,
        private prepService: PrepService,
        private pncService: PncService,
        private ancService: AncService,
        private matService: MaternityService,
        public zone: NgZone,
        private router: Router,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private personHomeService: PersonHomeService,
        private recordsService: RecordsService) {
        this.STIScreeningFormGroup = new FormArray([]);
        this.CircumcisionStatusFormGroup = new FormArray([]);
        this.FertilityIntentionsFormGroup = new FormArray([]);
        this.ChronicIllnessFormGroup = [];
        this.PrepStatusFormGroup = new FormArray([]);
        this.AppointmentFormGroup = new FormArray([]);
        this.LabInvestigationsFormGroup = new FormArray([]);

    }

    ngOnInit() {
        // Get PatientId and PersonId from query params
        this.route.params.subscribe(
            params => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                this.patientMasterVisitId = params.patientMasterVisitId;
                this.patientEncounterId = params.patientEncounterId;
                this.isEdit = params.edit;
                this.serviceId = params.serviceId;
            }
        );
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        // this.visitDate = new Date(localStorage.getItem('visitDate'));

        // Get data from route resolvers
        this.route.data.subscribe(
            (res) => {
                const { yesNoOptions, stiScreeningTreatmentOptions, yesNoUnknownOptions,
                    familyPlanningMethodsOptions, planningPregnancyOptions,
                    yesNoDontKnowOptions, pregnancyOutcomeOptions, prepContraindicationsOptions,
                    prepStatusOptions, reasonsPrepAppointmentNotGivenOptions,
                    pregnancyStatusOptions, screenedForSTIOptions } = res;
                this.yesnoOptions = yesNoOptions['lookupItems'];
                this.stiScreeningOptions = stiScreeningTreatmentOptions['lookupItems'];
                this.yesNoUnknownOptions = yesNoUnknownOptions['lookupItems'];
                this.familyPlanningMethodsOptions = familyPlanningMethodsOptions['lookupItems'];
                this.planningPregnancyOptions = planningPregnancyOptions['lookupItems'];
                this.yesNoDontKnowOptions = yesNoDontKnowOptions['lookupItems'];
                this.pregnancyOutcomeOptions = pregnancyOutcomeOptions['lookupItems'];
                this.prepStatusOptions = prepStatusOptions['lookupItems'];
                this.prepContraindicationsOptions = prepContraindicationsOptions['lookupItems'];
                this.reasonsPrepAppointmentNotGivenOptions = reasonsPrepAppointmentNotGivenOptions['lookupItems'];
                this.pregnancyStatusOptions = pregnancyStatusOptions['lookupItems'];
                this.screenedForSTIOptions = screenedForSTIOptions['lookupItems'];
            }
        );


        this.STIScreeningAndTreatmentOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'stiScreeningOptions': this.stiScreeningOptions,
            'screenedForSTIOptions': this.screenedForSTIOptions
        });

        this.CircumcisionStatusOptions.push({
            'yesNoUnknownOptions': this.yesNoUnknownOptions,
            'yesnoOptions': this.yesnoOptions,
        });

        this.FertilityIntentionsOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'fpMethods': this.familyPlanningMethodsOptions,
            'planningPregnancy': this.planningPregnancyOptions,
            'pregnancyStatusOptions': this.pregnancyStatusOptions

        });

        this.PregnancyOutcomeOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'yesNoDontKnowOptions': this.yesNoDontKnowOptions,
            'pregnancyOutcomeOptions': this.pregnancyOutcomeOptions
        });

        this.PrepStatusOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'prepStatusOptions': this.prepStatusOptions,
            'prepContraindicationsOptions': this.prepContraindicationsOptions
        });

        this.PrepAppointmentOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'reasonsPrepAppointmentNotGivenOptions': this.reasonsPrepAppointmentNotGivenOptions
        });

        this.recordsService.getPersonDetails(this.personId).subscribe(
            (res) => {
                if (res.length > 0) {
                    this.personGender = res[0]['gender'];

                }
            }
        );

        this.getPatientDetailsById(this.personId);
    }


    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personHomeService.getPatientByPersonId(personId).subscribe(
            p => {
                // console.log(p);
                this.person = p;
                if (this.person != null) {

                    if (this.person.dateOfBirth != null && this.person.dateOfBirth != undefined) {
                        this.minDate = this.person.dateOfBirth;
                    }
                    if (this.person.ageNumber != null && this.person.ageNumber != undefined) {
                        this.Age = this.person.ageNumber;
                    } else {
                        this.Age = 20;
                    }
                }

            },
            (err) => {
                this.snotifyService.error('Error retrieving the patient details ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }

    public onVisitDetailsNext() {
        if (this.personGender.toLowerCase() == 'male') {
            this.stepper.selectedIndex = 1;
            this.stepper._stateChanged();
            this.isOptionalObsGyn = true;
        } else if (this.personGender.toLowerCase() == 'female') {
            this.stepper.selectedIndex = 2;
            this.stepper._stateChanged();
            this.isOptionalCircumcision = true;
            localStorage.setItem('visitDate', this.STIScreeningFormGroup.value[0]['visitDate']);


        }
    }

    public onCircumcisionNext() {
        this.isOptionalObsGyn = true;
        this.stepper._stateChanged();
        this.stepper.selectedIndex = 3;
    }
    public onObsGynPrevious() {
        if (this.isOptionalCircumcision == true) {
            this.stepper._stateChanged();
            this.stepper.selectedIndex = 0;
        }

    }

    onPrepStiScreeningTreatmentNotify(formGroup: FormGroup): void {
        this.STIScreeningFormGroup.push(formGroup);
    }

    onCircumcisionStatusNotify(formGroup: FormGroup): void {
        this.CircumcisionStatusFormGroup.push(formGroup);
    }

    onFertilityIntentionNotify(formGroup: FormGroup): void {
        this.FertilityIntentionsFormGroup.push(formGroup);
    }

    onPregnancyOutcomeNotify(formGroup: FormGroup): void {
        this.FertilityIntentionsFormGroup.push(formGroup);
    }

    onChroniIllnessNotify(chronicIllnesses: any[]): void {
        this.ChronicIllnessFormGroup.push(chronicIllnesses);
    }

    onAllergiesNotify(allergies: any[]): void {
        this.ChronicIllnessFormGroup.push(allergies);
    }

    onAdverseEventsNotify(adverseEvents: any[]): void {
        this.ChronicIllnessFormGroup.push(adverseEvents);
    }

    onPrepStatusNotify(formGroup: FormGroup): void {
        console.log(this.PrepStatusFormGroup);
        this.PrepStatusFormGroup.push(formGroup);
    }

    onPrepAppointmentNotify(formGroup: FormGroup): void {

        this.AppointmentFormGroup.push(formGroup);
    }
    onLabInvestigations(formGroup: FormGroup): void {
        this.LabInvestigationsFormGroup.push(formGroup);
    }

    onSubmitForm() {
        if (this.isEdit == 1) {
            this.onPrepEncounterEdit();
        } else {
            this.onPrepNewEncounter();
        }
    }

    onPrepNewEncounter() {

        const PrepStatusToday = this.PrepStatusFormGroup.value[0]['PrEPStatusToday'];
        if (PrepStatusToday) {
            const statusname = this.prepStatusOptions.filter(x => x.itemId == parseInt(PrepStatusToday.toString(), 10));
            if (statusname.length > 0) {
                if (statusname[0].itemName == 'Restart') {
                    const daterestart = this.PrepStatusFormGroup.value[0]['DateRestarted'];
                    if (daterestart !== '' && daterestart != null) {
                        this.DateStatus = moment(daterestart).toDate();
                    }
                }
                else if (statusname[0].itemName == 'Start') {
                    const datestart = this.PrepStatusFormGroup.value[0]['DateInitiated'];
                    if (datestart !== '' && datestart != null) {
                        this.DateStatus = moment(datestart).toDate();
                    }
                }
                else {
                    this.DateStatus = null;
                }
            }
            else {
                this.DateStatus = null;
            }

        }
        // create prep status command
        const prepStatusCommand: PrepStatusCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientEncounterId: this.patientEncounterId,
            SignsOrSymptomsHIV: this.PrepStatusFormGroup.value[0]['signsOrSymptomsHIV'],
            AdherenceCounsellingDone: this.PrepStatusFormGroup.value[0]['adherenceCounselling'],
            //   ContraindicationsPrepPresent: this.PrepStatusFormGroup.value[0]['contraindications_PrEP_Present'],
            PrepStatusToday: this.PrepStatusFormGroup.value[0]['PrEPStatusToday'],
            CreatedBy: this.userId,
            CondomsIssued: this.PrepStatusFormGroup.value[0]['condomsIssued'],
            NoOfCondoms: this.PrepStatusFormGroup.value[0]['noCondomsIssued'],
            DateField: this.DateStatus
        };





        // is client on family planning
        const familyPlanningCommand: FamilyPlanningCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            FamilyPlanningStatusId: this.FertilityIntentionsFormGroup.value[0]['onFamilyPlanning'],
            ReasonNotOnFPId: 0,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            VisitDate: new Date(),
            AuditData: ''
        };

        // family planning method
        const familyPlanningMethodCommand: FamilyPlanningMethodCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientFPId: 0,
            FPMethodId: this.FertilityIntentionsFormGroup.value[0]['familyPlanningMethods'],
            Active: true,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: ''
        };

        const clientCircumcisionStatusCommand: ClientCircumcisionStatusCommand = {
            Id: 0,
            PatientId: this.patientId,
            ClientCircumcised: this.CircumcisionStatusFormGroup.value[0]['isClientCircumcised'],
            ReferredToVMMC: this.CircumcisionStatusFormGroup.value[0]['referredToVMMC'],
            CreatedBy: this.userId,
            CreateDate: new Date(),
            DeleteFlag: false
        };

        for (let i = 0; i < this.ChronicIllnessFormGroup[0].length; i++) {
            this.chronic_illness_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                ChronicIllness: this.ChronicIllnessFormGroup[0][i]['illness']['itemId'],
                Treatment: this.ChronicIllnessFormGroup[0][i]['currentTreatment'],
                DeleteFlag: false,
                OnsetDate: moment(this.ChronicIllnessFormGroup[0][i]['onsetDate']).toDate(),
                Active: 0,
                CreateBy: this.userId
            });
        }

        for (let i = 0; i < this.ChronicIllnessFormGroup[1].length; i++) {
            this.allergies_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                Allergen: '',
                DeleteFlag: false,
                CreateBy: this.userId,
                CreateDate: new Date(),
                AuditData: '',
                Reaction: 0,
                Severity: 0,
                OnsetDate: new Date()
            });
        }

        for (let i = 0; i < this.ChronicIllnessFormGroup[2].length; i++) {
            this.adverseEvents_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                EventName: this.ChronicIllnessFormGroup[2][i]['adverseEvent']['displayName'],
                EventCause: this.ChronicIllnessFormGroup[2][i]['medicine_causing'],
                Severity: this.ChronicIllnessFormGroup[2][i]['severity']['itemId'],
                Action: this.ChronicIllnessFormGroup[2][i]['adverseEventsAction']['displayName'],
                DeleteFlag: false,
                CreateBy: this.userId,
                CreateDate: new Date(),
                AuditData: '',
                AdverseEventId: this.ChronicIllnessFormGroup[2][i]['adverseEvent']['itemId']
            });
        }


        let pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Not Pregnant');
        const isPregnant = this.yesnoOptions.filter(obj => obj.itemId == this.FertilityIntentionsFormGroup.value[0]['pregnant']);
        if (isPregnant.length > 0) {
            if (isPregnant[0].itemName == 'Yes') {
                pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Pregnant');
            }
        }

        const pregnancyIndicatorCommand: PregnancyIndicatorCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            LMP: this.FertilityIntentionsFormGroup.value[0]['lmp'],
            EDD: null,
            PregnancyStatusId: pregnancyOption[0].itemId,
            PregnancyPlanned: this.FertilityIntentionsFormGroup.value[0]['pregnancyPlanned'],
            PlanningToGetPregnant: this.FertilityIntentionsFormGroup.value[0]['planningToGetPregnant'],
            BreastFeeding: this.FertilityIntentionsFormGroup.value[0]['breastFeeding'],
            ANCProfile: false,
            ANCProfileDate: null,
            Active: false,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: null,
            VisitDate: this.STIScreeningFormGroup.value[0]['visitDate']
        };

        const nextAppointmentCommand: NextAppointmentCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceId,
            AppointmentDate: this.AppointmentFormGroup.value[0]['nextAppointmentDate']
                ? moment(this.AppointmentFormGroup.value[0]['nextAppointmentDate']).toDate() : null,
            Description: this.AppointmentFormGroup.value[0]['clinicalNotes'],
            StatusDate: new Date(),
            DifferentiatedCareId: 0,
            AppointmentReason: 'Follow up',
            CreatedBy: this.userId
        };

        const reasons: any = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReasonAppointmentNotGiven: this.AppointmentFormGroup.value[0]['reasonAppointmentNoGiven']
        };

        const STIScreeningCommand: any = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreatedBy: this.userId,
            ScreeningDate: this.STIScreeningFormGroup.value[0]['visitDate'],
            VisitDate: this.STIScreeningFormGroup.value[0]['visitDate'],
            Screenings: []
        };

        for (let i = 0; i < this.screenedForSTIOptions.length; i++) {
            let value;

            if (this.screenedForSTIOptions[i].itemName == 'STITreatmentOffered') {
                value = this.STIScreeningFormGroup.value[0]['stiTreatmentOffered'];
            } else if (this.screenedForSTIOptions[i].itemName == 'STILabInvestigationDone') {
                value = this.STIScreeningFormGroup.value[0]['stiReferredLabInvestigation'];
            } else if (this.screenedForSTIOptions[i].itemName == 'STIScreeningDone') {
                value = this.STIScreeningFormGroup.value[0]['signsOrSymptomsOfSTI'];
            }

            if (this.screenedForSTIOptions[i].itemName !== 'STISymptoms') {
                STIScreeningCommand.Screenings.push({
                    ScreeningTypeId: this.screenedForSTIOptions[i].masterId,
                    ScreeningCategoryId: this.screenedForSTIOptions[i].itemId,
                    ScreeningValueId: value
                });

            }

        }

        if (this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present.length > 0) {
            for (let t = 0; t < this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present.length; t++) {


                STIScreeningCommand.Screenings.push({
                    ScreeningTypeId: this.prepContraindicationsOptions[0].masterId,
                    ScreeningCategoryId: this.prepContraindicationsOptions[0].masterId,
                    ScreeningValueId: this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present[t],
                    Comment: ''
                });


            }
        }

        let stioptions = [];
        stioptions = this.screenedForSTIOptions.filter(x => x.itemName == 'STISymptoms');
        if (this.STIScreeningFormGroup.value[0].signsOfSTI !== undefined) {
        if (this.STIScreeningFormGroup.value[0].signsOfSTI.length > 0) {
            for (let t = 0; t < this.STIScreeningFormGroup.value[0].signsOfSTI.length; t++) {
                let arraystis: LookupItemView[];
                let comment: string;
                arraystis = this.stiScreeningOptions.filter(x => x.itemId == this.STIScreeningFormGroup.value[0].signsOfSTI[t]);
                if (arraystis[0].itemDisplayName == 'Others (O)') {
                    comment = this.STIScreeningFormGroup.value[0].Specify;
                } else {
                    comment = '';
                }

                STIScreeningCommand.Screenings.push({
                    ScreeningTypeId: stioptions[0].masterId,
                    ScreeningCategoryId: stioptions[0].itemId,
                    ScreeningValueId: this.STIScreeningFormGroup.value[0].signsOfSTI[t],
                    Comment: comment
                });


            }
        }
    }

        const pregnancyIndicatorLog: PregnancyIndicatorLogCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            LMP: this.FertilityIntentionsFormGroup.value[0]['lmp'],
            EDD: moment(this.FertilityIntentionsFormGroup.value[0]['lmp'], 'DD-MM-YYYY').add(280, 'days').toDate(),
            Outcome: this.FertilityIntentionsFormGroup.value[1]['pregnancyOutcome'],
            DateOfOutcome: this.FertilityIntentionsFormGroup.value[1]['outcomeDate'],
            CreatedBy: this.userId,
            BirthDefects: this.FertilityIntentionsFormGroup.value[1]['birthDefects']
        };

        const prepStiScreeningTreatmentCommand = this.prepService.StiScreeningTreatment(STIScreeningCommand);
        const prepStatusApiCommand = this.prepService.savePrepStatus(prepStatusCommand);

        // add family planning for females
        const pncFamilyPlanning = this.personGender.toLowerCase() == 'male' ? of([]) :
            this.pncService.savePncFamilyPlanning(familyPlanningCommand);

        const chronicIllness = this.ancService.savePatientChronicIllness(this.chronic_illness_data);
        const adverseEvents = this.prepService.savePatientAdverseEvents(this.adverseEvents_data);
        const allergies = this.prepService.savePatientAllergies(this.allergies_data);

        // add circumcision for males
        const circumcisionStatus = this.personGender.toLowerCase() == 'male' ?
            this.prepService.saveCircumcisionStatus(clientCircumcisionStatusCommand) : of([]);

        // add pregnancy for pregnancy indicator
        const pregnancyIndicator = this.personGender.toLowerCase() == 'male' ? of([]) :
            this.prepService.savePregnancyIndicatorCommand(pregnancyIndicatorCommand);
        const matNextAppointment = this.matService.saveNextAppointment(nextAppointmentCommand);
        const hasPregnancyOutcome = this.yesnoOptions.filter(obj => obj.itemId ==
            this.FertilityIntentionsFormGroup.value[1]['endedPregnancy']);

        let pregnancyIndicatorLogCommand;
        if (this.personGender.toLocaleLowerCase() == 'male') {
            pregnancyIndicatorLogCommand = of([]);
        } else if (hasPregnancyOutcome.length > 0 && hasPregnancyOutcome[0].itemName == 'No') {
            pregnancyIndicatorLogCommand = of([]);
        } else {
            pregnancyIndicatorLogCommand = this.personGender.toLocaleLowerCase() == 'male' ? of([]) :
                this.prepService.savePregnancyIndicatorLogCommand(pregnancyIndicatorLog);
        }

        const reasonAppointmentHasValue = this.AppointmentFormGroup.value[0]['reasonAppointmentNoGiven'];
        let reasonsCommand;
        if (reasonAppointmentHasValue) {
            reasonsCommand = this.matService.saveReasonNextAppointmentNotGiven(reasons);
        } else {
            reasonsCommand = of([]);
        }

        forkJoin([
            prepStatusApiCommand,
            pncFamilyPlanning,
            chronicIllness,
            adverseEvents,
            allergies,
            circumcisionStatus,
            pregnancyIndicator,
            matNextAppointment,
            prepStiScreeningTreatmentCommand,
            pregnancyIndicatorLogCommand,
            reasonsCommand]).subscribe(
                (result) => {
                    familyPlanningMethodCommand.PatientFPId = result[1]['patientId'];
                    const pncFamilyPlanningMethod = this.pncService.savePncFamilyPlanningMethod(familyPlanningMethodCommand).subscribe(
                        (res) => {
                            console.log(`family planning method`);
                            console.log(res);
                        }
                    );

                    this.zone.run(() => {
                        this.zone.run(() => {
                            this.router.navigate(
                                ['/prep/prepfollowupworkflow/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                                { relativeTo: this.route });
                        });
                    });
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    onPrepEncounterEdit() {

        const PrepStatusToday = this.PrepStatusFormGroup.value[0]['PrEPStatusToday'];
        if (PrepStatusToday) {
            const statusname = this.prepStatusOptions.filter(x => x.itemId == parseInt(PrepStatusToday.toString(), 10));
            if (statusname.length > 0) {
                if (statusname[0].itemName == 'Restart') {
                    const daterestart = this.PrepStatusFormGroup.value[0]['DateRestarted'];
                    if (daterestart !== '' && daterestart != null) {
                        this.DateStatus = moment(daterestart).toDate();
                    }
                }
                else if (statusname[0].itemName == 'Start') {
                    const datestart = this.PrepStatusFormGroup.value[0]['DateInitiated'];
                    if (datestart !== '' && datestart != null) {
                        this.DateStatus = moment(datestart).toDate();
                    }
                }
                else {
                    this.DateStatus = null;
                }
            }
            else {
                this.DateStatus = null;
            }

        }
        // create prep status command
        const prepStatusCommand: PrepStatusCommand = {
            Id: this.PrepStatusFormGroup.value[0]['id'],
            PatientId: this.patientId,
            PatientEncounterId: this.patientEncounterId,
            SignsOrSymptomsHIV: this.PrepStatusFormGroup.value[0]['signsOrSymptomsHIV'],
            AdherenceCounsellingDone: this.PrepStatusFormGroup.value[0]['adherenceCounselling'] == '' ?
                0 : this.PrepStatusFormGroup.value[0]['adherenceCounselling'],
            //  ContraindicationsPrepPresent: this.PrepStatusFormGroup.value[0]['contraindications_PrEP_Present'],
            PrepStatusToday: this.PrepStatusFormGroup.value[0]['PrEPStatusToday'] == '' ? 0
                : this.PrepStatusFormGroup.value[0]['PrEPStatusToday'],
            CreatedBy: this.userId,
            CondomsIssued: this.PrepStatusFormGroup.value[0]['condomsIssued'] == '' ? 0
                : this.PrepStatusFormGroup.value[0]['condomsIssued'],
            NoOfCondoms: this.PrepStatusFormGroup.value[0]['noCondomsIssued'] == '' ? 0
                : this.PrepStatusFormGroup.value[0]['noCondomsIssued'],
            DateField: this.DateStatus
        };

        // circumcision 
        const clientCircumcisionStatusCommand: ClientCircumcisionStatusCommand = {
            Id: this.CircumcisionStatusFormGroup.value[0]['id'],
            PatientId: this.patientId,
            ClientCircumcised: this.CircumcisionStatusFormGroup.value[0]['isClientCircumcised'],
            ReferredToVMMC: this.CircumcisionStatusFormGroup.value[0]['referredToVMMC'],
            CreatedBy: this.userId,
            CreateDate: new Date(),
            DeleteFlag: false
        };

        const STIScreeningCommand: any = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreatedBy: this.userId,
            ScreeningDate: this.STIScreeningFormGroup.value[0]['visitDate'],
            VisitDate: this.STIScreeningFormGroup.value[0]['visitDate'],
            Screenings: []
        };

        for (let i = 0; i < this.screenedForSTIOptions.length; i++) {
            let value;

            if (this.screenedForSTIOptions[i].itemName == 'STITreatmentOffered') {
                value = this.STIScreeningFormGroup.value[0]['stiTreatmentOffered'];
            } else if (this.screenedForSTIOptions[i].itemName == 'STILabInvestigationDone') {
                value = this.STIScreeningFormGroup.value[0]['stiReferredLabInvestigation'];
            } else if (this.screenedForSTIOptions[i].itemName == 'STIScreeningDone') {
                value = this.STIScreeningFormGroup.value[0]['signsOrSymptomsOfSTI'];
            }

            if (this.screenedForSTIOptions[i].itemName !== 'STISymptoms') {
                STIScreeningCommand.Screenings.push({
                    ScreeningTypeId: this.screenedForSTIOptions[i].masterId,
                    ScreeningCategoryId: this.screenedForSTIOptions[i].itemId,
                    ScreeningValueId: value
                });

            }

        }

        let stioptions = [];
        stioptions = this.screenedForSTIOptions.filter(x => x.itemName == 'STISymptoms');

        if (this.STIScreeningFormGroup.value[0].signsOfSTI !== undefined) {
            if (this.STIScreeningFormGroup.value[0].signsOfSTI.length > 0) {
                for (let t = 0; t < this.STIScreeningFormGroup.value[0].signsOfSTI.length; t++) {

                    let arraystis: LookupItemView[];
                    let comment: string;
                    arraystis = this.stiScreeningOptions.filter(x => x.itemId == this.STIScreeningFormGroup.value[0].signsOfSTI[t]);
                    if (arraystis[0].itemDisplayName == 'Others (O)') {
                        comment = this.STIScreeningFormGroup.value[0].Specify;
                    } else {
                        comment = '';
                    }

                    console.log(this.STIScreeningFormGroup.value[0].signsOfSTI[t]);
                    STIScreeningCommand.Screenings.push({
                        ScreeningTypeId: stioptions[0].masterId,
                        ScreeningCategoryId: stioptions[0].itemId,
                        ScreeningValueId: this.STIScreeningFormGroup.value[0].signsOfSTI[t],
                        Comment: comment
                    });


                }
            }
        }

        if (this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present.length > 0) {
            for (let t = 0; t < this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present.length; t++) {


                STIScreeningCommand.Screenings.push({
                    ScreeningTypeId: this.prepContraindicationsOptions[0].masterId,
                    ScreeningCategoryId: this.prepContraindicationsOptions[0].masterId,
                    ScreeningValueId: this.PrepStatusFormGroup.value[0].contraindications_PrEP_Present[t],
                    Comment: ''
                });


            }
        }

        for (let i = 0; i < this.ChronicIllnessFormGroup[0].length; i++) {
            this.chronic_illness_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                ChronicIllness: this.ChronicIllnessFormGroup[0][i]['illness']['itemId'],
                Treatment: this.ChronicIllnessFormGroup[0][i]['currentTreatment'],
                DeleteFlag: false,
                OnsetDate: moment(this.ChronicIllnessFormGroup[0][i]['onsetDate']).toDate(),
                Active: 0,
                CreateBy: this.userId
            });
        }

        for (let i = 0; i < this.ChronicIllnessFormGroup[1].length; i++) {
            this.allergies_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                Allergen: '',
                DeleteFlag: false,
                CreateBy: this.userId,
                CreateDate: new Date(),
                AuditData: '',
                Reaction: 0,
                Severity: 0,
                OnsetDate: new Date()
            });
        }

        for (let i = 0; i < this.ChronicIllnessFormGroup[2].length; i++) {
            this.adverseEvents_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                EventName: this.ChronicIllnessFormGroup[2][i]['adverseEvent']['displayName'],
                EventCause: this.ChronicIllnessFormGroup[2][i]['medicine_causing'],
                Severity: this.ChronicIllnessFormGroup[2][i]['severity']['itemId'],
                Action: this.ChronicIllnessFormGroup[2][i]['adverseEventsAction']['displayName'],
                DeleteFlag: false,
                CreateBy: this.userId,
                CreateDate: new Date(),
                AuditData: '',
                AdverseEventId: this.ChronicIllnessFormGroup[2][i]['adverseEvent']['itemId']
            });
        }

        const pregnancyIndicatorLog: PregnancyIndicatorLogCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            LMP: this.FertilityIntentionsFormGroup.value[0]['lmp'],
            EDD: moment(this.FertilityIntentionsFormGroup.value[0]['lmp'], 'DD-MM-YYYY').add(280, 'days').toDate(),
            Outcome: this.FertilityIntentionsFormGroup.value[1]['pregnancyOutcome'],
            DateOfOutcome: this.FertilityIntentionsFormGroup.value[1]['outcomeDate'],
            CreatedBy: this.userId,
            BirthDefects: this.FertilityIntentionsFormGroup.value[1]['birthDefects']
        };

        const hasPregnancyOutcome = this.yesnoOptions.filter(obj => obj.itemId ==
            this.FertilityIntentionsFormGroup.value[1]['endedPregnancy']);
        let pregnancyIndicatorLogCommand;
        if (this.personGender.toLocaleLowerCase() == 'male') {
            pregnancyIndicatorLogCommand = of([]);
        } else if (hasPregnancyOutcome.length > 0 && hasPregnancyOutcome[0].itemName == 'No') {
            pregnancyIndicatorLogCommand = of([]);
        } else {
            pregnancyIndicatorLogCommand = this.personGender.toLocaleLowerCase() == 'male' ? of([]) :
                this.prepService.savePregnancyIndicatorLogCommand(pregnancyIndicatorLog);
        }

        const reasons: any = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReasonAppointmentNotGiven: this.AppointmentFormGroup.value[0]['reasonAppointmentNoGiven']
        };

        const reasonAppointmentHasValue = this.AppointmentFormGroup.value[0]['reasonAppointmentNoGiven'];
        let reasonsCommand;
        if (reasonAppointmentHasValue) {
            reasonsCommand = this.matService.saveReasonNextAppointmentNotGiven(reasons);
        } else {
            reasonsCommand = of([]);
        }
 
        const AppointmentId = this.AppointmentFormGroup.value[0]['id'];
        const updateNextAppointment = {
            AppointmentId: (AppointmentId == undefined || AppointmentId ===''  || AppointmentId === null) ? 0 : AppointmentId,
            AppointmentDate: this.AppointmentFormGroup.value[0]['nextAppointmentDate'],
            Description: this.AppointmentFormGroup.value[0]['clinicalNotes']
        };

         const familyPlanningId = this.FertilityIntentionsFormGroup.value[0]['id_familyPlanning'];
        const familyPlanningEditCommand: FamilyPlanningEditCommand = {
            Id: (familyPlanningId === undefined || familyPlanningId === '' || familyPlanningId === null) ? 0 : familyPlanningId,
            FamilyPlanningStatusId: this.FertilityIntentionsFormGroup.value[0]['onFamilyPlanning'],
            ReasonNotOnFPId: 0
        };

        const fpMethodId = this.FertilityIntentionsFormGroup.value[0]['fpMethodId'];
       

        const updateFamilyPlanningMethodCommand: PatientFamilyPlanningMethodEditCommand = {
            Id: (fpMethodId === undefined || fpMethodId === ''  || fpMethodId === null) ? 0 : fpMethodId ,
            FPMethodId: this.FertilityIntentionsFormGroup.value[0]['familyPlanningMethods'],
            PatientId: this.patientId,
            PatientFPId: (this.FertilityIntentionsFormGroup.value[0]['id_familyPlanning'] === null)
            ? 0 : this.FertilityIntentionsFormGroup.value[0]['id_familyPlanning'],
            UserId: this.userId
        };

        // add family planning for females
        const pncFamilyPlanning = this.personGender.toLowerCase() == 'male' ? of([]) :
            this.pncService.updateFamilyPlanning(familyPlanningEditCommand);

        const prepStiScreeningTreatmentCommand = this.prepService.UpdateStiScreeningTreatment(STIScreeningCommand);
        const prepStatusApiCommand = this.prepService.savePrepStatus(prepStatusCommand);
        // add circumcision for males
        const circumcisionStatus = this.personGender.toLowerCase() == 'male' ?
            this.prepService.saveCircumcisionStatus(clientCircumcisionStatusCommand) : of([]);
        const chronicIllness = this.ancService.savePatientChronicIllness(this.chronic_illness_data);
        const adverseEvents = this.prepService.savePatientAdverseEvents(this.adverseEvents_data);
        const allergies = this.prepService.savePatientAllergies(this.allergies_data);
        const updateAppointmentCommand = this.matService.updateNextAppointment(updateNextAppointment);
        const pncFamilyPlanningMethodEdit = this.pncService.updatePncFamilyPlanningMethod(updateFamilyPlanningMethodCommand);

        forkJoin([
            prepStiScreeningTreatmentCommand,
            prepStatusApiCommand,
            circumcisionStatus,
            chronicIllness,
            adverseEvents,
            allergies,
            pregnancyIndicatorLogCommand,
            reasonsCommand,
            updateAppointmentCommand,
            pncFamilyPlanning,
            pncFamilyPlanningMethodEdit
        ]).subscribe(
            (result) => {
                // console.log(result);

                this.zone.run(() => {
                    this.router.navigate(['/prep/' + this.patientId + '/' + this.personId + '/'
                        + this.serviceId], { relativeTo: this.route });
                });
            },
            (error) => {
                console.log(error);
            }
        );
    }
}


import { FormControlService } from './../../../../shared/_services/form-control.service';
import { PersonHomeService } from './../../../services/person-home.service';
import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemService } from '../../../../shared/_services/lookup-item.service';
import { Subscription } from 'rxjs';

import { NotificationService } from '../../../../shared/_services/notification.service';
import { PersonView } from '../../../_model/personView';
import { SnotifyService } from 'ng-snotify';
import { MatSelect } from '@angular/material';
import { PrepStatusCommand } from '../../../../prep/_models/commands/PrepStatusCommand';
import { debounceTime } from 'rxjs/operators';
import { forkJoin, ReplaySubject, Subject, Observable } from 'rxjs';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { PersonPopulation } from '../../../../registration/_models/personPopulation';
import { ServiceEntryPointCommand } from '../../../_model/ServiceEntryPointCommand';

import { EnrollmentService } from '../../../../registration/_services/enrollment.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../../../shared/reducers/app.states';
import { AppEnum } from '../../../../shared/reducers/app.enum';
import { AppStateService } from '../../../../shared/_services/appstate.service';
import { Enrollment } from '../../../../registration/_models/enrollment';
import { SearchService } from '../../../../registration/_services/search.service';
import { RecordsService } from '../../../../records/_services/records.service';
import { CircumcisionCommand } from '../../../_model/ClientCircumcisionStatusCommand';
import { PregnancyIndicatorCommand } from '../../../_model/PregnancyIndicatorCommand';
import * as moment from 'moment';
import { take } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { FamilyPlanningCommand } from '../../../../pmtct/_models/FamilyPlanningCommand';
import { FamilyPlanningMethodCommand } from '../../../../pmtct/_models/FamilyPlanningMethodCommand';
import { PatientFamilyPlanningMethodEditCommand } from '../../../../pmtct/_models/PatientFamilyPlanningMethodEditCommand';
import { FamilyPlanningEditCommand } from '../../../../pmtct/_models/FamilyPlanningEditCommand';
import { NextAppointmentCommand } from '../../../../prep/_models/commands/nextAppointmentCommand';
import { PrepService } from '../../../../prep/_services/prep.service'
import { PatientChronicIllness } from '../../../../pmtct/_models/PatientChronicIllness';
import { AdverseEventsCommand } from '../../../../prep/_models/commands/AdverseEventsCommand';
import { AllergiesCommand } from '../../../../prep/_models/commands/AllergiesCommand';
import { AncService } from '../../../../pmtct/_services/anc.service';



@Component({
    selector: 'app-prep',
    templateUrl: './prep.component.html',
    styleUrls: ['./prep.component.css'],
    providers: [PrepService, AncService]
})
export class PrepComponent implements OnInit {
    public person: PersonView;
    public personView$: Subscription;
    form: FormGroup;
    personId: number;
    circumcisedId: number;
    patientId: number;
    serviceId: number;
    serviceCode: string;
    userId: number;
    posId: string;
    isEdit: boolean = false;
    isVisible: boolean = false;
    maxDate: Date;
    hasLabBeenOrdered: boolean = false;
    hasPharmacyBeenOrdered: boolean = false;
    minDate: Date;
    DateStatus?: Date;
    Appointmentid: number;
    facilityselected: any[] = [];
    personPopulation: PersonPopulation;
    ClientTypes: any[] = [];
    entrypoints: LookupItemView[] = [];
    PrepRegimen: LookupItemView[] = [];
    ChronicIllnessFormGroup: Object[][];
    keyPops: LookupItemView[] = [];
    disPops: LookupItemView[] = [];
    patientTypes: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    serviceAreaIdentifiers: any[] = [];
    pregnancyOption: LookupItemView[] = [];
    identifiers: any[] = [];
    clientCircumcisionStatusCommand: CircumcisionCommand;
    years: any[] = [];
    arraysplit: any[] = [];
    currentDate: Date;
    facilities: any[] = [];
    isSchoolVisible: boolean = false;
    isPopulationTypeVisible: boolean = false;
    isMale: boolean = true;
    isFemale: boolean = false;
    patientMasterVisitId: number;
    public hivpositiveprofileOptions: any[] = [];
    public hivsavedprofileOptions: any[] = [];
    patientIdValue: any[] = [];
    // public filteredfacilities: ReplaySubject<any[]> = new ReplaySubject<any[]>();
    public filteredfacilities: Observable<any[]>;
    public FacilitySelected: FormControl = new FormControl();
    partnercccenrollmentoptions: LookupItemView[] = [];
    sexwithoutcondomoptions: LookupItemView[] = [];
    patientIdentifieroptions: LookupItemView[] = [];
    yesNoDontKnowOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    fpMethods: LookupItemView[] = [];
    planningPregnancy: LookupItemView[] = [];
    pregnancyStatusOptions: LookupItemView[] = [];
    yesNoUnknownOptions: LookupItemView[] = [];
    populationtype: any[] = [];
    partnerhivstatusOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];
    stiOptions: LookupItemView[] = [];
    screenedForSTIOptions: LookupItemView[] = [];
    prepstatusOptions: LookupItemView[] = [];
    prepStatusOptions: LookupItemView[] = [];
    prepContraindicationsOptions: LookupItemView[] = [];
    Daterestartedvisible: boolean = false;
    DateInitiatedvisible: boolean = false;

    
    dateLMP: Date;
    minLMpDate: Date;
    visitDate: Date;

    public chronic_illness_data: PatientChronicIllness[] = [];
    public adverseEvents_data: AdverseEventsCommand[] = [];
    public allergies_data: AllergiesCommand[] = [];
    facilityList: any[] = [];
    protected _onDestroy = new Subject<void>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private ancService: AncService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private _lookupItemService: LookupItemService,
        private personHomeService: PersonHomeService,
        private prepService: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private searchService: SearchService,
        private recordsService: RecordsService) {
        this.ChronicIllnessFormGroup = [];
        this.maxDate = new Date();
        this.clientCircumcisionStatusCommand = new CircumcisionCommand();
        this.isVisible = false;
        this.FacilitySelected.valueChanges.pipe(debounceTime(400)).subscribe(data => {
            this.personHomeService.filterFacilities(data).subscribe(res => {

                this.filteredfacilities = res['facilityList'];
            });
        });


    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { id, serviceId, serviceCode, edit } = params
            this.personId = id;
            this.serviceId = serviceId;
            this.serviceCode = serviceCode;



            if (edit == 1) {
                this.isEdit = true;
            }

            this.userId = JSON.parse(localStorage.getItem('appUserId'));
            this.posId = localStorage.getItem('appPosID');
        });
        this.route.data.subscribe((res) => {


            const { PartnerCCCEnrollmentArray,
                PatientIdentifierArray
                , SexWithoutCondomArray
                , PatientIdArray
            } = res;
            console.log(res);
            this.sexwithoutcondomoptions = SexWithoutCondomArray['lookupItems'];
            this.patientIdentifieroptions = PatientIdentifierArray['lookupItems'];
            this.partnercccenrollmentoptions = PartnerCCCEnrollmentArray['lookupItems'];
            this.patientIdValue = PatientIdArray;
            if (this.patientIdValue['id'] != null || this.patientIdValue['id'] != undefined) {
                this.patientId = parseInt(this.patientIdValue['id'].toString(), 10);
                console.log('PatientId Resolver' + this.patientId);
            }
        });
        this.getPatientDetailsById(this.personId);

        this.personPopulation = new PersonPopulation();

        this.form = this._formBuilder.group({
            ClientTransferIn: new FormControl('', [Validators.required]),
            Referredfrom: new FormControl('', [Validators.required]),
            EnrollmentDate: new FormControl('', [Validators.required]),
            EnrollmentNumber: new FormControl('', Validators.compose([
                Validators.required,
                Validators.pattern(/^[\d]{5}$/),
                // Validators.maxLength(5)
            ])),
            MFLCode: new FormControl('', [Validators.required]),
            Year: new FormControl('', [Validators.required]),
            TransferInDate: new FormControl(''),
            DateLastUsed: new FormControl(''),
            InitiationDate: new FormControl(''),
            TransferInMflCode: new FormControl(''),
            CurrentRegimen: new FormControl(''),
            ClinicalNotes: new FormControl(''),
            IsSchool: new FormControl(''),
            PartnerHivStatus: new FormControl(''),
            //  KeyPopulation: new FormControl(''),
            // populationType: new FormControl('', [Validators.required]),
            //  DiscordantCouple: new FormControl(''),
            //  PrevPrepUse: new FormControl(''),
            Months: new FormControl(''),
            partnercccenrollment: new FormControl(''),
            CCCNumber: new FormControl('', Validators.pattern(/^((?!(0))[0-9]{10})$/)),
            partnerARTStartDate: new FormControl(''),
            partnerHivStatusDate: new FormControl(''),
            HivSerodiscordantduration: new FormControl(''),
            partnersexcondoms: new FormControl(''),
            hivpartnerchildren: new FormControl(''),
            isClientCircumcised: new FormControl(''),
            lmp: new FormControl(''),
            EDD: new FormControl(''),
            pregnant: new FormControl(''),
            pregnancyPlanned: new FormControl(''),
            breastFeeding: new FormControl(''),
            onFamilyPlanning: new FormControl(''),
            familyPlanningMethods: new FormControl(''),
            planningToGetPregnant: new FormControl(''),
            id_familyPlanning: new FormControl(),
            fpMethodId: new FormControl(),
            nextAppointmentGiven: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl(''),
            AppointmentId: new FormControl(''),

            Specify: new FormControl(''),
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
            stiTreatmentOffered: new FormControl(''),
            stiReferredLabInvestigation: new FormControl(''),
            signsOrSymptomsHIV: new FormControl('', [Validators.required]),
            contraindications_PrEP_Present: new FormControl(''),
            adherenceCounselling: new FormControl('', [Validators.required]),
            PrEPStatusToday: new FormControl('', [Validators.required]),
            condomsIssued: new FormControl('', [Validators.required]),
            noCondomsIssued: new FormControl('', [Validators.required]),
            DateRestarted: new FormControl(''),
            DateInitiated: new FormControl(''),
            idprep: new FormControl()



        });

        this.getYears();
        this.isVisible = false;
        this.form.controls.EDD.disable({ onlySelf: true });

        this.form.controls.partnerHivStatusDate.disable({ onlySelf: true });
        this.form.controls.partnercccenrollment.disable({ onlySelf: true });
        this.form.controls.CCCNumber.disable({ onlySelf: true });
        this.form.controls.partnerARTStartDate.disable({ onlySelf: true });
        this.form.controls.HivSerodiscordantduration.disable({ onlySelf: true });
        this.form.controls.partnersexcondoms.disable({ onlySelf: true });
        this.form.controls.Months.disable({ onlySelf: true });


        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                this.patientId = result.id;

            }
        );
        this._lookupItemService.getByGroupName('KeyPopulation').subscribe(
            (res) => {
                this.keyPops = res['lookupItems'];
            }
        );
        this._lookupItemService.getByGroupName('FPMethod').subscribe((res) => {
            this.fpMethods = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('HivStatus').subscribe((res) => {
            this.partnerhivstatusOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('YesNoDont_Know').subscribe((res) => {
            this.yesNoDontKnowOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('YesNoUnknown').subscribe((res) => {
            this.yesNoUnknownOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('PlanningPregnancy').subscribe((res) => {
            this.planningPregnancy = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('PlanningPregnancy').subscribe((res) => {
            this.planningPregnancy = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('PregnancyStatus').subscribe((res) => {
            this.pregnancyStatusOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('YesNo').subscribe((res) => {
            this.yesnoOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('ScreenedForSTI').subscribe((res) => {
            this.screenedForSTIOptions = res['lookupItems'];
        });

        this._lookupItemService.getByGroupName('PrEP_Status').subscribe((res) => {
            this.prepstatusOptions = res['lookupItems'];
            this.prepStatusOptions = res['lookupItems'];
        });
        this._lookupItemService.getByGroupName('ContraindicationsPrEP').subscribe((res) => {
            this.prepContraindicationsOptions = res['lookupItems'];
        });



        this.form.controls.TransferInMflCode.valueChanges.pipe(debounceTime(400)).subscribe(data => {
            this.personHomeService.filtermflcode(data).subscribe(res => {

                this.filteredfacilities = res['facilityList'];
            });
        });


        this.form.controls.Specify.disable({ onlySelf: true });
        this._lookupItemService.getByGroupName('DiscordantCouple').subscribe(
            (res) => {
                this.disPops = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('PatientType').subscribe(
            (res) => {
                this.patientTypes = res['lookupItems'];
                this.patientTypes.filter(x => x.itemName !== 'Transit').map(o => {
                    if (o.itemName == 'New') {
                        this.ClientTypes.push({
                            'itemId': o.itemId,
                            'itemDisplayName': 'No'
                        });
                    } else if (o.itemName == 'Transfer-In') {
                        this.ClientTypes.push({
                            'itemId': o.itemId,
                            'itemDisplayName': 'Yes'
                        });
                    }
                });


            }
        );
        //  this.form.controls.KeyPopulation.disable({ onlySelf: true });
        // this.form.controls.DiscordantCouple.disable({ onlySelf: true });

        this.form.controls.HivSerodiscordantduration.disable({ onlySelf: true });
        this.LoadPopulationTypes(this.personId);
        this.loadPriorityPopulation(this.personId);

        console.log('Population');
        console.log(this.populationtype);


        this._lookupItemService.getByGroupName('Entrypoint').subscribe(
            (res) => {
                this.entrypoints = res['lookupItems'];
            }
        );


        this.personHomeService.getServiceAreaIdentifiers(this.serviceId).subscribe(
            (res) => {

                const { identifiers, serviceAreaIdentifiers } = res;
                this.serviceAreaIdentifiers = serviceAreaIdentifiers;


            }
        );
        this._lookupItemService.getFacilityList().subscribe((res) => {

            this.facilities = res['facilityList'];

        });
        this._lookupItemService.getByGroupName('PrEPRegimen').subscribe(
            (res) => {
                this.PrepRegimen = res['lookupItems'];
            }
        );


        // this.form.controls.Weeks.disable({ onlySelf: true });
        this.form.controls.Months.disable({ onlySelf: true });
        this.form.controls.InitiationDate.disable({ onlySelf: true });
        this.form.controls.DateLastUsed.disable({ onlySelf: true });
        this.form.controls.CCCNumber.disable({ onlySelf: true });



        this.form.controls.MFLCode.setValue(this.posId);



        /*this.form.controls.FacilityListSelected.setValue(this.facilities);
        this.filteredfacilities.next(this.facilities.slice(0, 10));*/
        // this.loadPrepStartEventPatient();

        if (this.isEdit) {
            this.loadPatient();




        }



        /* this.filteredfacilities = this.form.controls.FacilitySelected
         .valueChanges.pipe(
             startWith(''),
             map(value => this.filter(value))
             );


            
         }
           
         filter(value: string): any[] {
           const filterValue = value.toLowerCase();
       
           return this.facilities.filter(option => option.toLowerCase().includes(filterValue));
         }*/


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
    loadPrepStartEventPatient() {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                this.loadPrepStartEvent(result.id);

            }
        );
    }
    loadcontraIndications(patientid, patientmastervisitid): void {
        this.prepService.getStiScreeningTreatment(patientid, patientmastervisitid).subscribe(
            (res) => {
                if (res.length > 0) {
                    const contraindications = [];
                    res.forEach(element => {
                        if (element.screeningTypeName == 'ContraindicationsPrEP') {
                            contraindications.push(element.screeningValueId);
                        }
                    });

                    this.form.controls.contraindications_PrEP_Present.setValue(contraindications);

                }
            },
            (error) => {
                console.log(error);
            }
        );
    }
    onPrepStatusChange(event) {
        const value = event.source.value;
        if (event.source.viewValue === 'Start' && event.source.selected == true) {

            const dateinitiated = this.form.controls.DateInitiated.value;
            if (dateinitiated == null || dateinitiated == '') {
                //this.loadPrepStartEventPatient();
            }

            this.DateInitiatedvisible = true;
            this.Daterestartedvisible = false;
        } else if (event.source.viewValue === 'Restart' && event.source.selected == true) {
            const daterestarted = this.form.controls.DateRestarted.value;
            if (daterestarted == null || daterestarted == '') {
                //  this.loadPrepStartEventPatient();
            }
            this.DateInitiatedvisible = false;
            this.Daterestartedvisible = true;
        } else if (event.source.viewValue !== 'Restart' && event.source.selected == true) {
            this.DateInitiatedvisible = false;
            this.Daterestartedvisible = false;
        } else if (event.source.viewValue !== 'Start' && event.source.selected == true) {
            this.DateInitiatedvisible = false;
            this.Daterestartedvisible = false;
        }


    }
    Oncontraindications(event) {
        const value = event.source.value;



        if (event.source.viewValue !== 'None' && event.source.selected == true) {




            for (let i = 0; i < event.source._parent.options.length; i++) {

                if (event.source._parent.options._results[i].viewValue
                    === 'None') {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }

        if (event.source.viewValue === 'None' && event.source.selected == true) {



            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    !== event.source.viewValue) {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }


        for (let i = 0; i < event.source._parent.options.length; i++) {

            if (event.source._parent.options._results[i].viewValue
                !== 'None' && event.source._parent.options._results[i].selected == true) {

                this.form.controls.adherenceCounselling.setValue('');
                this.form.controls.adherenceCounselling.disable({ onlySelf: true });
                this.form.controls.PrEPStatusToday.setValue('');
                this.form.controls.PrEPStatusToday.disable({ onlySelf: true });
                this.form.controls.DateRestarted.setValue('');
                this.form.controls.DateRestarted.disable({ onlySelf: true });
                this.form.controls.DateInitiated.setValue('');
                this.form.controls.DateInitiated.disable({ onlySelf: true });
            }



            if (event.source._parent.options._results[i].viewValue
                === 'None' && event.source._parent.options._results[i].selected == true) {
                this.form.controls.adherenceCounselling.enable({ onlySelf: true });

                this.form.controls.PrEPStatusToday.enable({ onlySelf: true });

                this.form.controls.DateRestarted.enable({ onlySelf: true });

                this.form.controls.DateInitiated.enable({ onlySelf: true });

            }




        }

        console.log(this.form.controls.PrEPStatusToday.value);
        console.log(this.form.controls.adherenceCounselling.value);

    }
    loadPrepStatus(patientid: number, patientmastervisitid: number): void {
        this.prepService.getPrepStatus(patientid, patientmastervisitid).subscribe(
            (res) => {
                console.log('PrepStatus');
                console.log(res);
                if (res.length > 0) {
                    let itemid: number;
                    let itemarray: any[] = [];
                    itemarray = this.prepStatusOptions.filter(x => x.itemId == parseInt(res[0].prepStatusToday.toString(), 10));
                    if (itemarray.length > 0) {
                        if (itemarray[0].itemDisplayName == 'Start') {
                            this.form.controls.DateInitiated.setValue(moment(res[0].DateField).toDate());
                        }
                        else if (itemarray[0].itemDisplayName == 'Restart') {
                            this.form.controls.DateRestarted.setValue(moment(res[0].DateField).toDate());
                        }
                    }
                    this.form.controls.signsOrSymptomsHIV.setValue(res[0].signsOrSymptomsHIV);
                    //  this.PrepStatusForm.controls.contraindications_PrEP_Present.setValue(res[0].contraindicationsPrepPresent);
                    this.form.controls.adherenceCounselling.setValue(res[0].adherenceCounsellingDone);
                    this.form.controls.PrEPStatusToday.setValue(res[0].prepStatusToday);
                    this.form.controls.condomsIssued.setValue(res[0].condomsIssued);
                    this.form.controls.noCondomsIssued.setValue(res[0].noOfCondoms);
                    this.form.controls.idprep.setValue(res[0].id);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public onLMPDateChange() {
        this.dateLMP = this.form.controls.lmp.value;

        this.visitDate = this.form.controls.EnrollmentDate.value;
        if (this.visitDate !== undefined && this.visitDate !== null) {
            this.minLMpDate = moment(moment(this.visitDate).subtract(42, 'weeks').format('')).toDate();

            if (moment(this.dateLMP).isBefore(this.minLMpDate)) {

                this.snotifyService.error('Current LMP Date CANNOT be More than 9 months before the VisitDate', 'Mother Profile',
                    this.notificationService.getConfig());
                this.form.controls.lmp.setValue('');
                return false;
            }


        }


    }



    onCondomsIssuedSelection(event) {
        // disable referral to VMMC when client is already circumcised
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.form.controls.noCondomsIssued.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.controls.noCondomsIssued.disable({ onlySelf: true });
            this.form.controls.noCondomsIssued.setValue('');
        }
    }

    loadPrepStartEvent(patientid: number) {
        let startitemarray: any[] = [];
        let startid: number;
        startitemarray = this.prepStatusOptions.filter(x => x.itemDisplayName == 'Start');

        if (startitemarray.length > 0) {
            startid = parseInt(startitemarray[0].itemId.toString(), 10);
        }
        this.prepService.getPatientStartEncounterEventDate(patientid, startid).subscribe((res) => {
            console.log('Prep Start Event Status');
            console.log(res);
            if (res != null) {
                this.form.controls.DateInitiated.setValue(moment(res['dateRestarted']).toDate());
            }
        });


    }
    loadSTIScreening(patientid: number, patientmastervisitid: number): void {
        this.prepService.getStiScreeningTreatment(patientid, patientmastervisitid).subscribe(
            (res) => {
                const STISymptoms = [];
                const stiScreeningObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STIScreeningDone');
                const stiSignsAndSymptomsObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STISymptoms');
                const stiLabInvestigationDoneObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STILabInvestigationDone');
                const stiTreatmentOfferedObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STITreatmentOffered');
                const othersItem = this.stiScreeningOptions.filter(obj => obj.itemName == 'Others (O)');

                if (res.length > 0) {
                    console.log('STIScreening');
                    console.log(res);
                    res.forEach(element => {
                        if (element.screeningTypeName == 'ScreenedForSTI' && element.screeningCategoryId == stiScreeningObject[0].itemId) {
                            this.form.controls.signsOrSymptomsOfSTI.setValue(element.screeningValueId);
                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiSignsAndSymptomsObject[0].itemId) {
                            STISymptoms.push(element.screeningValueId);
                            if (element.screeningValueId == othersItem[0].itemId) {
                                this.form.controls.Specify.setValue(element.comment);
                            }

                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiLabInvestigationDoneObject[0].itemId) {
                            this.form.controls.stiReferredLabInvestigation.setValue(element.screeningValueId);
                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiTreatmentOfferedObject[0].itemId) {
                            this.form.controls.stiTreatmentOffered.setValue(element.screeningValueId);
                        }
                    });

                    this.form.controls.signsOfSTI.setValue(STISymptoms);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }
    OnSTISelection(event) {
        const value = event.source.value;
        const othersItem = this.stiScreeningOptions.filter(obj => obj.itemId == value);
        if (othersItem[0].itemDisplayName == 'Others (O)' && event.source.selected == true) {
            this.form.controls.Specify.enable({ onlySelf: true });
        }
        if (othersItem[0].itemDisplayName == 'Others (O)' && event.source.selected == false) {

            this.form.controls.Specify.setValue('');
            this.form.controls.Specify.disable({ onlySelf: true });
        }


    }
    public onSignsOrSymptomsSelection(event) {
        
            this.form.controls.contraindications_PrEP_Present.enable({onlySelf: true});
        if (event.source.selected == true && event.source.viewValue == 'Yes') {
            this.form.controls.adherenceCounselling.setValue('');
            this.form.controls.adherenceCounselling.disable({ onlySelf: true });
            this.form.controls.PrEPStatusToday.setValue('');
            this.form.controls.PrEPStatusToday.disable({ onlySelf: true });
            this.form.controls.DateRestarted.setValue('');
            this.form.controls.DateRestarted.disable({ onlySelf: true });
            this.form.controls.DateInitiated.setValue('');
            this.form.controls.DateInitiated.disable({ onlySelf: true });
            this.form.controls.contraindications_PrEP_Present.setValue('');
            this.form.controls.contraindications_PrEP_Present.disable({onlySelf:true});
           

        }  
        if ( event.source.selected == true && event.source.viewValue == 'No') { 
           
            this.form.controls.adherenceCounselling.enable({ onlySelf: true });
           
            this.form.controls.PrEPStatusToday.enable({ onlySelf: true });
           
            this.form.controls.DateRestarted.enable({ onlySelf: true });
        
            this.form.controls.DateInitiated.enable({ onlySelf: true });
            this.form.controls.contraindications_PrEP_Present.enable({onlySelf: true});
           
        }
    }

    onReferLabSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.hasLabBeenOrdered = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.hasLabBeenOrdered = false;
        }
    }

    onSTITreatmentelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.hasPharmacyBeenOrdered = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.hasPharmacyBeenOrdered = false;
        }
    }

    onLabClick() {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                //this.loadPrepStartEvent(result.id);
                this.zone.run(() => {
                    this.router.navigate(['/clinical/completeorder/' + result.id + '/' + this.personId],
                        { relativeTo: this.route });
                });

            }
        );
    }
    onPharmacyClick() {

        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                //this.loadPrepStartEvent(result.id);
                this.zone.run(() => {
                    this.router.navigate(['/pharm/' + result.id + '/' + this.personId],
                        { relativeTo: this.route });
                });

            }
        );



    }

    loadhivprofiles(patientid: number) {
        this.personHomeService.getHivPartnerProfile(patientid).subscribe((result) => {

            if (result != null) {
                let outcome: any[] = [];
                outcome = result['patientProfiles'];

                if (outcome.length > 0) {
                    outcome.forEach(r => {
                        let sexwithouttext: string[];
                        sexwithouttext = this.sexwithoutcondomoptions
                            .filter(x => x.itemId == parseInt(r['sexWithoutCondoms'].toString(), 10))
                            .map(t => t.displayName);
                        let CCCEnrollmentText: string[];
                        CCCEnrollmentText = this.partnercccenrollmentoptions.
                            filter(x => x.itemId == r['cccEnrollment'])
                            .map(x => x.displayName);
                        let PartnerHivStatusText = this.partnerhivstatusOptions.filter(x => x.itemId == r['partnerHivStatus']).map(x => x.displayName);
                        this.hivpositiveprofileOptions.push({
                            HivPositiveStatusDate: r['hivPositiveStatusDate'],
                            PartnerHivStatus: PartnerHivStatusText,
                            CCCEnrollment: r['cccEnrollment'],
                            CCCEnrollmentText: CCCEnrollmentText,
                            partnerARTStartDate: r['partnerARTStartDate'],
                            HivSerodiscordantduration: r['hivSeroDiscordantDuration'],
                            SexWithoutCondoms: sexwithouttext,
                            NumberofChildren: r['numberofChildren'],
                            CCCNumber: r['cccNumber'],
                            DeleteFlag: false,
                            Id: r['id']





                        });
                        this.hivsavedprofileOptions.push({
                            Id: r['id'],
                            PatientId: this.patientId,
                            PartnerHivStatus: r['partnerHivStatus'],
                            HivPositiveStatusDate: r['hivPositiveStatusDate'],
                            CCCEnrollment: r['cccEnrollment'],
                            PartnerARTStartDate: r['partnerARTStartDate'],
                            HivSerodiscordantduration: r['hivSeroDiscordantDuration'],
                            SexWithoutCondoms: r['sexWithoutCondoms'],
                            NumberofChildren: r['numberofChildren'],
                            CCCNumber: r['cccNumber'],
                            CreatedBy: r['createdBy'],
                            DeleteFlag: false


                        });




                    });
                }
            }
        });
    }

    loadAppointments(patientid: number, patientmastervisitid: number) {
        this.personHomeService.getAppointments(patientid, patientmastervisitid).subscribe(
            (result) => {
                if (result) {

                    const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                    this.form.get('nextAppointmentDate').setValue(result.appointmentDate);

                    this.Appointmentid = result.id;
                    //this.form.get('Appointmentid').setValue(result.id);
                    this.form.get('nextAppointmentGiven').setValue(yesOption[0].itemId);
                } else {
                    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                    this.form.get('nextAppointmentGiven').setValue(noOption[0].itemId);

                }
            },
            (error) => {
                this.snotifyService.error('Fetching appointments ' + error, 'Prep Initiation',
                    this.notificationService.getConfig());
            }
        );
    }


    onAppointmentSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {


            // disable date 
            this.form.controls.nextAppointmentDate.disable({ onlySelf: true });
            this.form.controls.nextAppointmentDate.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            // enable date 
            this.form.controls.nextAppointmentDate.enable({ onlySelf: true });


        }
    }
    loadEnrollmentPatientMasterVisitId(patientId: number) {
        this.personHomeService.getPatientEnrollmentMasterVisitByServiceAreaId(patientId, this.serviceId).subscribe(
            (result) => {

                this.patientMasterVisitId = result[0]['id'];
                this.loadPregnancyIndicator(this.patientMasterVisitId, patientId);
                this.loadAppointments(patientId, this.patientMasterVisitId);
                this.loadFamilyPlanning(patientId, this.patientMasterVisitId);
                this.loadSTIScreening(patientId, this.patientMasterVisitId);
                this.loadcontraIndications(patientId, this.patientMasterVisitId);
                this.loadPrepStatus(patientId, this.patientMasterVisitId);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPregnancyIndicator(patientMasterVisitId: number, patientid: number): void {
        this.personHomeService.getPregnancyIndicator(patientid, patientMasterVisitId).subscribe(
            (res) => {
                if (res) {
                    this.form.controls.lmp.setValue(res.lmp);
                    this.form.controls.planningToGetPregnant.setValue(res.planningToGetPregnant);
                    this.form.controls.breastFeeding.setValue(res.breastFeeding);

                    // set pregnancy status
                    const pregnancyStatus = this.pregnancyStatusOptions.filter(obj => obj.itemId == res.pregnancyStatusId);
                    if (pregnancyStatus[0].displayName == 'Pregnant') {
                        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                        this.form.controls.pregnant.setValue(yesOption[0].itemId);
                    } else {
                        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                        this.form.controls.pregnant.setValue(noOption[0].itemId);
                    }
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadFamilyPlanningMethod(patientid): void {
        this.personHomeService.getFamilyPlanningMethod(patientid).subscribe(
            (res) => {

                if (res.length > 0) {
                    res.forEach(element => {
                        this.form.controls.familyPlanningMethods.setValue(element.fpMethodId);
                        this.form.controls.fpMethodId.setValue(element.id);
                    });
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadFamilyPlanning(patientid: number, patientmastervisitid: number): void {
        this.personHomeService.getFamilyPlanning(patientid, patientmastervisitid).subscribe(
            (res) => {
                if (res.length > 0) {
                    res.forEach(element => {
                        if (element.patientMasterVisitId == this.patientMasterVisitId) {
                            this.form.controls.onFamilyPlanning.setValue(element.familyPlanningStatusId);
                            this.form.controls.id_familyPlanning.setValue(element.id);
                        }
                    });
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadCircumcisionStatus(patientid: number) {
        this.personHomeService.getCircumcisionStatus(patientid).subscribe(
            (res) => {
                if (res) {
                    this.form.controls.isClientCircumcised.setValue(res.clientCircumcised);
                    this.circumcisedId = res.Id;

                }
            },
            (error) => {

            }
        );
    }
    OnPartnerHivSelection(event) {
        let selectedvalue: string;
        selectedvalue = event.source.viewValue;
        selectedvalue = selectedvalue.toString().toLowerCase();
        if (event.source.selected == true) {
            if (selectedvalue.toLowerCase() === 'positive') {
                this.form.controls.partnerHivStatusDate.enable({ onlySelf: true });
                this.form.controls.partnercccenrollment.enable({ onlySelf: true });
                this.form.controls.CCCNumber.enable({ onlySelf: true });
                this.form.controls.partnerARTStartDate.enable({ onlySelf: true });
                this.form.controls.HivSerodiscordantduration.enable({ onlySelf: true });
                this.form.controls.partnersexcondoms.enable({ onlySelf: true });
                this.form.controls.Months.enable({ onlySelf: true });

            }
            else {

                this.form.controls.partnercccenrollment.setValue('');
                this.form.controls.partnerHivStatusDate.setValue('');
                this.form.controls.partnerARTStartDate.setValue('');
                this.form.controls.HivSerodiscordantduration.setValue('');
                this.form.controls.partnersexcondoms.setValue('');
                this.form.controls.hivpartnerchildren.setValue('');
                this.form.controls.CCCNumber.setValue('');

                this.form.controls.partnerHivStatusDate.disable({ onlySelf: true });
                this.form.controls.partnercccenrollment.disable({ onlySelf: true });
                this.form.controls.CCCNumber.disable({ onlySelf: true });
                this.form.controls.partnerARTStartDate.disable({ onlySelf: true });
                this.form.controls.HivSerodiscordantduration.disable({ onlySelf: true });
                this.form.controls.partnersexcondoms.disable({ onlySelf: true });
                this.form.controls.Months.disable({ onlySelf: true });




            }

        }

    }
    OnPartnerenrollmentSelection(event) {

        let selectedvalue: string;
        selectedvalue = event.source.viewValue;
        selectedvalue = selectedvalue.toString().toLowerCase();
        if (event.source.selected == true) {
            if (selectedvalue === 'yes') {
                this.form.controls.CCCNumber.enable({ onlySelf: true });



            } else if (selectedvalue === 'no') {
                this.form.controls.CCCNumber.disable({ onlySelf: true });
                this.form.controls.CCCNumber.setValue('');


            } else if (selectedvalue === 'unknown') {
                this.form.controls.CCCNumber.disable({ onlySelf: true });
                this.form.controls.CCCNumber.setValue('');

            }
            else {
                this.form.controls.CCCNumber.disable({ onlySelf: true });
                this.form.controls.CCCNumber.setValue('');

            }
        }
        else {
            this.form.controls.CCCNumber.disable({ onlySelf: true });
            this.form.controls.CCCNumber.setValue('');

        }

    }

    addHivPartner() {

        const { partnercccenrollment,
            partnerHivStatusDate,
            partnerARTStartDate,
            HivSerodiscordantduration, partnersexcondoms, hivpartnerchildren, CCCNumber, PartnerHivStatus } = this.form.value;
        if (PartnerHivStatus === null || PartnerHivStatus === undefined) {
            this.snotifyService.error('Kindly note PartnerHiVStatus ' +
                ' is' +
                'required for Hiv Partner Profile', 'Hiv Positive Partner Profile',
                this.notificationService.getConfig());
            return;

        }
        if (PartnerHivStatus) {
            const PartnerArray = this.partnerhivstatusOptions.filter(x => x.itemId == PartnerHivStatus);
            if (PartnerArray.length > 0) {
                if (PartnerArray[0].itemDisplayName.toLowerCase() === "positive") {
                    if (partnercccenrollment !== undefined && partnercccenrollment !== null) {
                        if (parseInt(partnercccenrollment, 10) > 0) {

                            let CCCEnrollmentText = this.partnercccenrollmentoptions.filter(x => x.itemId == partnercccenrollment).map(x => x.displayName);
                            let sexwithouttext = this.sexwithoutcondomoptions.filter(x => x.itemId == partnersexcondoms).map(x => x.displayName);
                            this.hivpositiveprofileOptions.push({
                                HivPositiveStatusDate: partnerHivStatusDate,
                                PartnerHivStatus: PartnerArray[0].itemDisplayName,
                                CCCEnrollment: partnercccenrollment,
                                CCCEnrollmentText: CCCEnrollmentText,
                                partnerARTStartDate: partnerARTStartDate,
                                HivSerodiscordantduration: HivSerodiscordantduration,
                                SexWithoutCondoms: sexwithouttext,
                                NumberofChildren: hivpartnerchildren,
                                CCCNumber: CCCNumber,
                                DeleteFlag: false,
                                Id: 0




                            });
                            this.hivsavedprofileOptions.push({
                                Id: 0,
                                PatientId: this.patientId,
                                HivPositiveStatusDate: partnerHivStatusDate,
                                PartnerHivStatus: PartnerHivStatus,
                                CCCEnrollment: partnercccenrollment,
                                PartnerARTStartDate: partnerARTStartDate,
                                HivSerodiscordantduration: HivSerodiscordantduration,
                                SexWithoutCondoms: partnersexcondoms,
                                NumberofChildren: hivpartnerchildren,
                                CCCNumber: CCCNumber,
                                CreatedBy: this.userId,
                                DeleteFlag: false


                            });

                            this.form.controls.partnercccenrollment.setValue('');
                            this.form.controls.partnerHivStatusDate.setValue('');
                            this.form.controls.partnerARTStartDate.setValue('');
                            this.form.controls.HivSerodiscordantduration.setValue('');
                            this.form.controls.partnersexcondoms.setValue('');
                            this.form.controls.hivpartnerchildren.setValue('');
                            this.form.controls.CCCNumber.setValue('');
                        } else {
                            this.snotifyService.error('Kindly note  Partner CCC Enrollment' +
                                ' is' +
                                'required for Hiv Partner Profile', 'Hiv Positive Partner Profile',
                                this.notificationService.getConfig());
                            return;
                        }
                    } else {
                        this.snotifyService.error('Kindly note partnercccenrollment' +
                            ' is' +
                            'required for Hiv Partner Profile', 'Hiv Positive Partner Profile',
                            this.notificationService.getConfig());
                        return;
                    }
                }
                else {


                    this.hivpositiveprofileOptions.push({
                        HivPositiveStatusDate: '',
                        PartnerHivStatus: PartnerArray[0].itemDisplayName,
                        CCCEnrollment: '',
                        CCCEnrollmentText: '',
                        partnerARTStartDate: '',
                        HivSerodiscordantduration: '',
                        SexWithoutCondoms: '',
                        NumberofChildren: '',
                        CCCNumber: '',
                        DeleteFlag: false,
                        Id: 0




                    });
                    this.hivsavedprofileOptions.push({
                        Id: 0,
                        PatientId: this.patientId,
                        HivPositiveStatusDate: partnerHivStatusDate,
                        PartnerHivStatus: PartnerHivStatus,
                        CCCEnrollment: partnercccenrollment,
                        PartnerARTStartDate: partnerARTStartDate,
                        HivSerodiscordantduration: HivSerodiscordantduration,
                        SexWithoutCondoms: partnersexcondoms,
                        NumberofChildren: hivpartnerchildren,
                        CCCNumber: CCCNumber,
                        CreatedBy: this.userId,
                        DeleteFlag: false


                    });



                }
            }
        }



    }
    removeRow(index) {

        let idx: number;
        idx = index;

        if (parseInt(this.hivpositiveprofileOptions[idx].Id, 10) < 1) {
            this.hivpositiveprofileOptions.splice(idx, 1);
            this.hivsavedprofileOptions.splice(idx, 1);
        } else {
            this.hivpositiveprofileOptions.splice(idx, 1);
            this.hivsavedprofileOptions[idx].DeleteFlag = true;
        }




    }

    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personHomeService.getPatientByPersonId(personId).subscribe(
            p => {

                this.person = p;

                if (this.person != null) {

                    if (this.person.dateOfBirth != null && this.person.dateOfBirth != undefined) {
                        this.minDate = this.person.dateOfBirth;
                    }

                    if (this.person.gender != null && this.person.gender != undefined) {
                        if (this.person.gender.toLowerCase() == 'male') {
                            this.isMale = true;
                            this.isFemale = false;
                            this._lookupItemService.getByGroupName('STIScreeningTreatment').subscribe((res) => {
                                this.stiScreeningOptions = res['lookupItems'];
                                this.stiOptions = this.stiScreeningOptions.filter(x => x.itemName !== 'Cervicitis and/or Cervical discharge'
                                    && x.itemName !== 'Vaginitis or Vaginal discharge (VG)'
                                    && x.itemName !== 'Pelvic Inflammatory Disease (PID)');

                            });

                        } else if (this.person.gender.toLowerCase() == 'female') {
                            this.isFemale = true;
                            this.isMale = false;
                            this._lookupItemService.getByGroupName('STIScreeningTreatment').subscribe((res) => {
                                this.stiScreeningOptions = res['lookupItems'];
                                this.stiOptions = this.stiScreeningOptions;

                            });
                        }
                    }

                }

                localStorage.setItem('personId', this.person.personId.toString());
                this.store.dispatch(new Consent.PersonId(this.person.personId));

                if (this.person.patientId && this.person.patientId > 0) {
                    this.store.dispatch(new Consent.PatientId(this.person.patientId));
                    localStorage.setItem('patientId', this.person.patientId.toString());
                }
                if (parseInt(this.person.age, 10) <= 19) {
                    this.isSchoolVisible = true;
                }

            },
            (err) => {
                this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {

            });
    }

    loadPatientOVStatus(): void {
        this.personHomeService.getPatientOVCStatusDetails(this.personId).subscribe(
            (result) => {
                if (result != null) {
                    const arrayValue = [];

                    arrayValue.push(result);

                    if (arrayValue[0]['inSchool'] != null) {
                        let inschool: string;
                        inschool = arrayValue[0]['inSchool'].toString();
                        this.form.controls.IsSchool.setValue(parseInt(inschool, 10));

                    }
                }

            }

        );
    }
    onClientFPSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.form.controls.familyPlanningMethods.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.form.controls.familyPlanningMethods.setValue('');
        }
    }
    loadEntryPoints(pat: number): void {
        this.personHomeService.getPatientServiceAreaEntryPoints(this.serviceId, pat).subscribe(
            (result) => {

                this.form.controls.Referredfrom.setValue(result.entryPointId);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPrevTransferIn(): void {
        this.personHomeService.getPatientTransferInDetails(this.serviceId, this.personId).subscribe(
            (res) => {

                if (res != null) {
                    const arrayValue = [];
                    arrayValue.push(res);

                    let itemid: number;
                    itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'Yes');

                    if (arrayValue.length > 0) {

                        this.form.controls.ClientTransferIn.setValue(this.ClientTypes[itemid]['itemId']);
                        this.isVisible = true;
                        console.log(arrayValue);
                        if (arrayValue[0] != null) {
                            if (arrayValue[0]['transferInDate'] != null) {
                                this.form.controls.TransferInDate.setValue(arrayValue[0]['transferInDate']);
                            }
                            if (arrayValue[0]['mflCode'] != null) {
                                this.form.controls.TransferInMflCode.setValue(arrayValue[0]['mflCode']);



                                //  this.form.controls.FacilityListSelected.setValue(arrayValue[0]['facilityFrom']);
                                // this.FacilitySelected.setValue(arrayValue[0]['facilityFrom']);

                                this.personHomeService.getFacility(arrayValue[0]['mflCode']).subscribe(
                                    (result) => {
                                        if (result.length > 0) {

                                            this.filteredfacilities = result;
                                            this.FacilitySelected.setValue(result[0]);
                                        }
                                    }
                                );
                            }

                            // this.filtercorrectfacilities(arrayValue[0]['facilityFrom']);
                            if (arrayValue[0]['transferInNotes'] != null) {
                                this.form.controls.ClinicalNotes.setValue(arrayValue[0]['transferInNotes']);
                            }
                            if (arrayValue[0]['currentTreatment'] != null) {
                                let currentTreatment: string;
                                currentTreatment = arrayValue[0]['currentTreatment'].toString();
                                this.form.controls.CurrentRegimen.setValue(parseInt(currentTreatment, 10));
                            }
                        }

                    } else {

                        itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'No');

                        this.form.controls.ClientTransferIn.setValue(itemid);

                    }

                } else {
                    let itemid: number;
                    itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'No');

                    this.form.controls.ClientTransferIn.setValue(this.ClientTypes[itemid]['itemId']);
                    // this.form.controls.PrevPrepUse.setValue(itemid);


                }

            }
            ,
            (error) => {
                console.log(error);
            }
        );
    }

    loadPrevPrepUse(): void {
        this.personHomeService.getPatientARVDetails(this.serviceId, this.personId).subscribe(
            (res) => {

                const arrayValue = [];
                arrayValue.push(res);

                if (arrayValue.length > 0) {
                    //   this.form.controls.PrevPrepUse.setValue(1);

                    if (arrayValue[0] != null) {

                        //  this.form.controls.Weeks.enable({ onlySelf: true });
                        this.form.controls.Months.enable({ onlySelf: true });
                        this.form.controls.InitiationDate.enable({ onlySelf: true });
                        this.form.controls.DateLastUsed.enable({ onlySelf: true });



                        if (arrayValue[0]['months'] != null) {
                            this.form.controls.Months.setValue(arrayValue[0]['months']);
                        }
                        if (arrayValue[0]['initiationDate'] != null) {
                            this.form.controls.InitiationDate.setValue(moment(arrayValue[0]['initiationDate'].toString()).toDate());
                        }

                        if (arrayValue[0]['dateLastUsed'] != null) {
                            this.form.controls.DateLastUsed.setValue(moment(arrayValue[0]['dateLastUsed'].toString()).toDate());
                        }
                    }
                } else {

                    this.form.controls.Months.disable({ onlySelf: true });
                    this.form.controls.DateLastUsed.disable({ onlySelf: true });
                    this.form.controls.InitiationDate.disable({ onlySelf: true });
                }


            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadIdentifiers(pat: number): void {
        this.recordsService.getPatientIdentifiersList(pat).subscribe(
            (result) => {
                if (result.length > 0) {

                    let t: number;
                    const array: any[] = [];
                    let arrayreplace: string;
                    let arraynumber: string;
                    let mflcodenumber: string;
                    let arrayreplace2: string;

                    let id: number;
                    let value: string;
                    id = this.serviceAreaIdentifiers[0]['identifierId'];
                    array.push(result);

                    t = array[0].findIndex(x => x.identifierTypeId === id);

                    value = array[0][t].identifierValue.toString();
                    arraynumber = value.replace(/\//g, "");
                    console.log(arraynumber);
                    mflcodenumber = arraynumber.substring(0, 5);
                    arrayreplace = arraynumber.split(mflcodenumber).pop();

                    let stringvalue: string;
                    stringvalue = arrayreplace.substring(0, 4);

                    arrayreplace2 = arrayreplace.split(stringvalue).pop();
                    this.form.get('MFLCode').setValue(mflcodenumber);

                    this.form.get('EnrollmentNumber').setValue(arrayreplace2);
                    let years: string;
                    years = stringvalue;
                    this.form.get('Year').setValue(parseInt(years, 10));

                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPatient(): void {
        this.personHomeService.getPatientModelByPersonId(this.personId).subscribe(
            (result) => {
                this.LoadPrepEnrollmentDate(result.id);
                this.loadEntryPoints(result.id);
                // this.loadPopulationTypes(this.personId);
                this.loadEnrollmentPatientMasterVisitId(result.id);
                this.loadCircumcisionStatus(result.id);
                this.loadhivprofiles(result.id);
                this.loadFamilyPlanningMethod(result.id);



                // load hts identifiers
                this.loadIdentifiers(result.id);

                this.loadPrevPrepUse();
                this.loadPrevTransferIn();
                this.loadPatientOVStatus();
            }
        );
    }

    LoadPrepEnrollmentDate(patientId: number): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, this.serviceId).subscribe(
            (result) => {
                this.form.controls.EnrollmentDate.setValue(result.enrollmentDate);
            },
            (error) => {
                console.log(error);
            }
        );
    }


    LoadPopulationTypes(personId: number): void {
        this.personHomeService.getPersonPopulationType(personId).subscribe(
            (result) => {


                if (result.length > 0) {

                    console.log(result);
                    this.isPopulationTypeVisible = true;
                    result.forEach(element => {
                        if (element.populationType == 'General Population') {
                            this.populationtype.push('General Population');

                        } else if (element.populationType == 'Discordant Couple') {
                            this.form.controls.HivSerodiscordantduration.enable({ onlySelf: true });
                            this.populationtype.push('Discordant Couple');

                        }

                    });
                    console.log(this.populationtype);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPriorityPopulation(personId: number): void {
        this.personHomeService.getPersonPriorityTypes(personId).subscribe(
            (result) => {
                // console.log(result);
                if (result.length > 0) {
                    this.isPopulationTypeVisible = true;
                    this.populationtype.push('PriorityPopulation');
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    TransferIn(event) {
        if (event.source.selected == true) {
            if (event.source.viewValue === 'Yes') {
                this.isVisible = true;
                //this.form.controls.PrevPrepUse.setValue(1);
                this.form.controls.Months.enable({ onlySelf: true });
                this.form.controls.InitiationDate.enable({ onlySelf: true });
                this.form.controls.DateLastUsed.enable({ onlySelf: true });
             

            } else {
                this.isVisible = false;
                this.form.controls.TransferInDate.setValue('');
                this.form.controls.FacilitySelected.setValue('');
                this.form.controls.CurrentRegimen.setValue('');
                this.form.controls.ClinicalNotes.setValue('');
                this.form.controls.TransferInMflCode.setValue('');
                this.form.controls.DateLastUsed.disable({ onlySelf: true });
                //this.form.controls.PrevPrepUse.setValue('');
                this.form.controls.Months.disable({ onlySelf: true });
                this.form.controls.InitiationDate.disable({ onlySelf: true });
                this.form.controls.Months.setValue('');
                this.form.controls.InitiationDate.setValue('');
                this.form.controls.DateLastUsed.setValue('');

            }
        }
    }

    // tslint:disable-next-line: use-life-cycle-interface
    /* ngAfterViewInit() {
         this.setInitialValue();
     }
     
     // tslint:disable-next-line: use-life-cycle-interface
     ngOnDestroy() {
         this._onDestroy.next();
         this._onDestroy.complete();
     } */

    // protected setInitialValue() {
    /**  this.filteredfacilities
          .pipe(take(1), takeUntil(this._onDestroy))
          .subscribe(() => {
              // setting the compareWith property to a comparison function
              // triggers initializing the selection according to the initial value of
              // the form control (i.e. _initializeSelection())
              // this needs to be done after the filteredBanks are loaded initially
              // and after the mat-option elements are available
              //  this.form.controls.FacilitySelected.compareWith = (a: Facility, b: Facility) => a && b && a.id === b.id;
          });*/

    // this.filteredfacilities.next(this.facilities.slice(0, 10));
    /*  this.filteredfacilities.pipe(take(1)).subscribe(() => {
    
      });
    } */
    displayfn(facility?: any): string | undefined {
        return facility ? facility.name : undefined;
    }


    displaymflcode(mflCode?: any): string | undefined {
        return mflCode ? mflCode : undefined;
    }
    /*protected filtercorrectfacilities(value) {
        if (!this.facilities) {
            return;
        }
    
    
        let search = value;
    
        if (!search) {
            this.filteredfacilities.next(this.facilities.slice(0, 10));
            return;
        } else {
            search = search.toLowerCase();
        }
        this.filteredfacilities.next(
            this.facilities.filter(f => f.name.toLowerCase().indexOf(search) > -1).slice(0, 10)
        );
    
    
    } */

    getYears() {
        let year: number;
        this.currentDate = new Date();
        year = this.currentDate.getFullYear();

        for (let i = year; i >= 1900; i--) {
            this.years.push(i);
        }





    }

    /*onTextChanged(event) {
        this.filtercorrectfacilities(event.target.value);
    } */

    /*   changePrevUse(event) {
     
           let selectedvalue: number;
           selectedvalue = event.source.value;
     
           if (event.source.selected == true) {
               if (selectedvalue === 1) {
                   //  this.form.controls.Weeks.enable({ onlySelf: true });
                   this.form.controls.Months.enable({ onlySelf: true });
                   this.form.controls.InitiationDate.enable({ onlySelf: true });
               } else {
                   // this.form.controls.Weeks.disable({ onlySelf: true });
                   this.form.controls.Months.disable({ onlySelf: true });
                   this.form.controls.InitiationDate.disable({ onlySelf: true });
               }
           }
     
       }*/

    changemflcode(event) {

        let value: string;
        value = event.option.viewValue.toString();

        let arr: string[] = [];
        arr = value.split('-');

        this.personHomeService.getFacility(arr[0].toString()).subscribe(
            (result) => {
                if (result.length > 0) {

                    this.filteredfacilities = result;
                    this.FacilitySelected.setValue(result[0]);
                }
            }
        );




    }
    changecode(event) {

        this.FacilitySelected.setValue('');
    }
    change(event) {


        this.form.controls.TransferInMflCode.setValue(event.option.value.mflCode);


    }
    onPregnancySelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.form.controls.pregnancyPlanned.enable({ onlySelf: true });

            // disable
            this.form.controls.onFamilyPlanning.disable({ onlySelf: true });
            this.form.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.form.controls.planningToGetPregnant.disable({ onlySelf: true });


            if (this.form.controls.lmp.value !== null &&
                this.form.controls.lmp.value !== undefined &&
                this.form.controls.lmp.value !== '') {
                this.form.controls.EDD.setValue(
                    moment(this.form.controls.lmp.value
                        , 'DD-MM-YYYY').add(280, 'days').toDate());

            }
            // Reset values
            this.form.controls.onFamilyPlanning.setValue('');
            this.form.controls.familyPlanningMethods.setValue('');
            this.form.controls.planningToGetPregnant.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.controls.pregnancyPlanned.disable({ onlySelf: true });
            this.form.controls.pregnancyPlanned.setValue('');
            this.form.controls.EDD.disable({ onlySelf: true });
            this.form.controls.EDD.setValue('');

            // enable
            this.form.controls.onFamilyPlanning.enable({ onlySelf: true });
            // this.FertilityIntentionForm.controls.familyPlanningMethods.enable({ onlySelf: true });
            this.form.controls.planningToGetPregnant.enable({ onlySelf: true });
        } else {
            this.form.controls.EDD.disable({ onlySelf: true });
            this.form.controls.EDD.setValue('');
        }
    }

    onPopulationTypeChange() {
        const popType = this.form.controls.populationType.value;
        if (popType == 1) {
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            // this.form.controls.DiscordantCouple.disable({ onlySelf: false });
            this.form.controls.KeyPopulation.setValue([]);
            // this.form.controls.DiscordantCouple.setValue([]);
        } else if (popType == 2) {
            this.form.controls.KeyPopulation.enable({ onlySelf: false });
            //  this.form.controls.DiscordantCouple.disable({ onlySelf: false });
            //  this.form.controls.DiscordantCouple.setValue([]);
        } else if (popType == 3) {
            //  this.form.controls.DiscordantCouple.enable({ onlySelf: false });
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            this.form.controls.KeyPopulation.setValue([]);
        }
    }

    public SaveValues() {

        const { EnrollmentDate
            , EnrollmentNumber, MFLCode, Year, ClientTransferIn,
            FacilitySelected, InitiationDate,
            TransferInDate, TransferInMflCode, CurrentRegimen, ClinicalNotes
            , isClientCircumcised, lmp, pregnant, DateLastUsed,
            pregnancyPlanned, breastFeeding, onFamilyPlanning, familyPlanningMethods, planningToGetPregnant
        } = this.form.value;



        let itemdisplayname: any[] = [];
        itemdisplayname = this.ClientTypes.filter(x => x.itemId == ClientTransferIn).map(o => {
            return o.itemDisplayName.toString();
        });



        if (this.person.gender.toLowerCase() === 'male') {

            if (isClientCircumcised == null || isClientCircumcised == '' || isClientCircumcised == undefined) {
                this.snotifyService.error('Kindly note is Client Circumcised   ' +
                    ' is required', 'IsClientCircumcised',
                    this.notificationService.getConfig());
                return;
            }
        }
        if (this.person.gender.toLowerCase() === 'female') {
            if ((lmp == '' || lmp == undefined || lmp == null) &&
                (pregnant == '' || pregnant == undefined || pregnant == null) &&
                (pregnancyPlanned == '' || pregnancyPlanned == undefined || pregnancyPlanned == null) &&
                (breastFeeding == '' || breastFeeding == undefined || breastFeeding == null) &&
                (onFamilyPlanning == '' || onFamilyPlanning == undefined || onFamilyPlanning == null) &&
                (familyPlanningMethods == '' || familyPlanningMethods == undefined || familyPlanningMethods == null) &&
                (planningToGetPregnant == '' || planningToGetPregnant == undefined || planningToGetPregnant == null)) {
                this.snotifyService.error('Kindly note all fields under female only section ' +
                    'is required', 'Female Section Details',
                    this.notificationService.getConfig());
                return;
            }
        }
        if (itemdisplayname[0] == 'Yes') {
            if ((TransferInMflCode == '' || TransferInMflCode == undefined) &&
                (TransferInDate == '' || TransferInDate == undefined)
                && (TransferInMflCode == '' || TransferInMflCode == undefined)
                && (this.FacilitySelected.value == null)
                && (CurrentRegimen == '' || CurrentRegimen == undefined)) {
                this.snotifyService.error('Kindly note MFLCode,Transfer In facility,Transfer In Date and' +
                    'Current Regimen is' +
                    'required in the transfer in status', 'TransferDetails',
                    this.notificationService.getConfig());
                return;

                if (InitiationDate == null || InitiationDate == '' || InitiationDate == undefined) {
                    this.snotifyService.error('Kindly note Prep Previous Start Date ' +
                        ' is required', 'InitiationDate',
                        this.notificationService.getConfig());
                    return;
                }

            }

        }

        if (this.form.valid == true) {
            this.save();
        } else {
            return;
        }



    }

    public Cancel() {

        this.zone.run(() => {
            this.router.navigate(
                ['/dashboard/personhome/' + this.personId],
                { relativeTo: this.route });
        });
    }

    public save() {
        this.spinner.show();
        const enrollment = new Enrollment();

        const { EnrollmentDate //KeyPopulation, populationType
            , EnrollmentNumber, MFLCode, Year, ClientTransferIn
            , TransferInDate, TransferInMflCode, CurrentRegimen, ClinicalNotes, InitiationDate, IsSchool,
            Months, Referredfrom, isClientCircumcised, lmp, pregnant,
            pregnancyPlanned, breastFeeding, onFamilyPlanning, DateLastUsed,
            familyPlanningMethods, planningToGetPregnant,
            id_familyPlanning, fpMethodId, nextAppointmentDate, nextAppointmentGiven, Appointmentid, PartnerHivStatus
        } = this.form.value;
        // this.personPopulation.KeyPopulation = KeyPopulation;
        //this.personPopulation.populationType = populationType;
        // this.personPopulation.DiscordantCouplePopulation = DiscordantCouple;

        let itemdisplayname: any[] = [];
        itemdisplayname = this.ClientTypes.filter(x => x.itemId == ClientTransferIn).map(o => {
            return o.itemDisplayName.toString();
        });

        let idnumber: string;
        idnumber = MFLCode + + Year + EnrollmentNumber;
        enrollment.ServiceIdentifiersList.push({
            'IdentifierId': this.serviceAreaIdentifiers[0]['identifierId'],
            'IdentifierValue': idnumber
        });
        enrollment.DateOfEnrollment = EnrollmentDate;
        enrollment.ServiceAreaId = this.serviceId;
        enrollment.PersonId = this.personId;
        enrollment.CreatedBy = this.userId;
        enrollment.RegistrationDate = EnrollmentDate;
        enrollment.PosId = this.posId;



        //const populationTypes = this.registrationService.addPersonPopulationType(this.personId,
        //    this.userId, this.personPopulation);
        const addPatient = this.registrationService.addPatient(this.personId, this.userId, EnrollmentDate, this.posId);

        forkJoin([addPatient]).subscribe(
            results => {
                this.patientId = results[0]['patientId'];
                enrollment.PatientId = this.patientId;
                const entryPoint: ServiceEntryPointCommand = {
                    Id: 0,
                    PatientId: this.patientId,
                    ServiceAreaId: this.serviceId,
                    EntryPointId: Referredfrom,
                    CreateDate: new Date(),
                    CreatedBy: this.userId
                };


                const serviceEntryPoint = this.registrationService.addServiceEntryPoint(entryPoint).subscribe();
                this.enrollmentService.enrollClient(enrollment).subscribe(
                    (response) => {
                        if (response != null) {
                            this.snotifyService.success('Successfully Enrolled ', 'Enrollment',
                                this.notificationService.getConfig());

                            localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                            this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                            this.store.dispatch(new Consent.PatientId(this.patientId));
                            this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                                this.patientId).subscribe();

                            this.loadEnrollmentPatientMasterVisitId(this.patientId);

                            if (itemdisplayname[0] == 'Yes') {

                                let transferinitiationdate: any;
                                if (InitiationDate == null || InitiationDate == '' || InitiationDate == undefined) {
                                    transferinitiationdate = TransferInDate
                                } else {
                                    transferinitiationdate = InitiationDate
                                }
                                this.registrationService.addPatientTransferIn(this.patientId, this.serviceId
                                    , TransferInDate, transferinitiationdate,
                                    CurrentRegimen, this.FacilitySelected.value.name
                                    , TransferInMflCode, '0', ClinicalNotes, this.userId, false)
                                    .subscribe(
                                        (res) => {
                                            let transferInId: number;
                                            transferInId = res['transferInId'];
                                            if (transferInId > 0) {
                                                this.snotifyService.success('TransferIn details successfully saved', 'Transfer Status');
                                            }

                                        },
                                        (error) => {
                                            this.snotifyService.error('Error saving TransferIn Details ' + error, 'Transfer Status',
                                                this.notificationService.getConfig());

                                        });


                                this.registrationService.addPatientARVHistory(this.patientId,
                                    this.serviceId, 'ART', 'PrEP', '', this.userId, false,
                                    Months, InitiationDate, DateLastUsed).subscribe(
                                        (res) => {
                                            let result: number;
                                            result = res['aRVHistoryId'];
                                            if (result > 0) {
                                                this.snotifyService.success('Successfully Saved Previous Prep Use'
                                                    , 'Previous Prep Use',
                                                    this.notificationService.getConfig());
                                            }


                                        },
                                        (error) => {
                                            this.snotifyService.error('Error saving Previous Prep Details ' + error, 'Previous Prep Use',
                                                this.notificationService.getConfig());

                                        });


                            }

                            if (IsSchool !== null && IsSchool !== undefined && IsSchool.length > 1) {
                                this.registrationService.addPatientOvcStatus(this.personId, 0, IsSchool
                                    , true, false, this.userId).subscribe((res) => {

                                        let OvcStatus
                                            : Number;
                                        OvcStatus = res['oVCStatusId'];
                                        if (OvcStatus > 0) {
                                            this.snotifyService.success('Successfully Saved Attending  School Details'
                                                , 'Attend School',
                                                this.notificationService.getConfig());
                                        }

                                    },
                                        (error) => {
                                            this.snotifyService.error('Error saving Attend School Details ' + error, 'Attend School',
                                                this.notificationService.getConfig());
                                        });
                            }


                            if (this.person.gender.toLowerCase() == 'male') {
                                if (isClientCircumcised && this.isEdit == false) {
                                    this.clientCircumcisionStatusCommand.Id = 0;
                                    this.clientCircumcisionStatusCommand.PatientId = this.patientId;
                                    this.clientCircumcisionStatusCommand.ClientCircumcised = parseInt(isClientCircumcised, 10);
                                    this.clientCircumcisionStatusCommand.ReferredToVMMC = 0;
                                    this.clientCircumcisionStatusCommand.CreatedBy = this.userId;
                                    this.clientCircumcisionStatusCommand.CreateDate = new Date();
                                    this.clientCircumcisionStatusCommand.DeleteFlag = false;


                                }
                                else if (isClientCircumcised && this.isEdit == true) {

                                    this.clientCircumcisionStatusCommand.Id = this.circumcisedId;
                                    this.clientCircumcisionStatusCommand.PatientId = this.patientId;
                                    this.clientCircumcisionStatusCommand.ClientCircumcised = parseInt(isClientCircumcised, 10);
                                    this.clientCircumcisionStatusCommand.ReferredToVMMC = 0;
                                    this.clientCircumcisionStatusCommand.CreatedBy = this.userId;
                                    this.clientCircumcisionStatusCommand.CreateDate = new Date();
                                    this.clientCircumcisionStatusCommand.DeleteFlag = false;
                                }


                                const circumcisionStatus = this.personHomeService.saveCircumcisionStatus(this.clientCircumcisionStatusCommand).subscribe((res) => {


                                }, (error) => {
                                    console.log(error);
                                });
                            }

                            this.personHomeService.getPatientEnrollmentMasterVisitByServiceAreaId(this.patientId, this.serviceId).subscribe(
                                (result) => {
                                    this.patientMasterVisitId = result[0]['id'];

                                    if (this.patientMasterVisitId != null && this.patientMasterVisitId > 0) {
                                        const yes = this.yesnoOptions.filter(x => x.itemName.toLowerCase() == 'yes');

                                        const PrepStatusToday = this.form.controls.PrEPStatusToday.value;
                                        if (PrepStatusToday) {
                                            const statusname = this.prepStatusOptions.filter(x => x.itemId == parseInt(PrepStatusToday.toString(), 10));
                                            if (statusname.length > 0) {
                                                if (statusname[0].itemName == 'Restart') {
                                                    const daterestart = this.form.controls.DateRestarted.value;
                                                    if (daterestart !== '' && daterestart != null) {
                                                        this.DateStatus = moment(daterestart).toDate();
                                                    }
                                                }
                                                else if (statusname[0].itemName == 'Start') {
                                                    const datestart = this.form.controls.DateInitiated.value;
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



                                        const STIScreeningCommand: any = {
                                            PatientId: this.patientId,
                                            PatientMasterVisitId: this.patientMasterVisitId,
                                            CreatedBy: this.userId,
                                            ScreeningDate: EnrollmentDate,
                                            VisitDate: EnrollmentDate,
                                            Screenings: []
                                        };

                                        for (let i = 0; i < this.screenedForSTIOptions.length; i++) {
                                            let value;

                                            if (this.screenedForSTIOptions[i].itemName == 'STITreatmentOffered') {
                                                value = this.form.controls.stiTreatmentOffered.value;
                                            } else if (this.screenedForSTIOptions[i].itemName == 'STILabInvestigationDone') {
                                                value = this.form.controls.stiReferredLabInvestigation.value;
                                            } else if (this.screenedForSTIOptions[i].itemName == 'STIScreeningDone') {
                                                value = this.form.controls.signsOrSymptomsOfSTI.value;
                                            }

                                            if (this.screenedForSTIOptions[i].itemName !== 'STISymptoms') {
                                                STIScreeningCommand.Screenings.push({
                                                    ScreeningTypeId: this.screenedForSTIOptions[i].masterId,
                                                    ScreeningCategoryId: this.screenedForSTIOptions[i].itemId,
                                                    ScreeningValueId: value
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
                                                Allergen: this.ChronicIllnessFormGroup[1][i]['allergy']['itemId'],
                                                DeleteFlag: false,
                                                CreateBy: this.userId,
                                                CreateDate: new Date(),
                                                AuditData: '',
                                                Reaction: this.ChronicIllnessFormGroup[1][i]['reactionType']['itemId'],
                                                Severity: this.ChronicIllnessFormGroup[1][i]['severity']['itemId'],
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
                                        const chronicIllness = this.ancService.savePatientChronicIllness(this.chronic_illness_data).subscribe((res) => {

                                        }, (error) => {
                                            console.log(error);
                                        });
                                        const adverseEvents = this.prepService.savePatientAdverseEvents(this.adverseEvents_data).subscribe((res) => {

                                        }, (error) => {
                                            console.log(error);
                                        });
                                        const allergies = this.prepService.savePatientAllergies(this.allergies_data).subscribe((res) => {

                                        }, (error) => {
                                            console.log(error);
                                        });

                                        let stioptions = [];
                                        stioptions = this.screenedForSTIOptions.filter(x => x.itemName == 'STISymptoms');

                                        if (this.form.controls.signsOfSTI.value.length > 0) {
                                            for (let t = 0; t < this.form.controls.signsOfSTI.value.length; t++) {
                                                let arraystis: LookupItemView[];
                                                let comment: string;
                                                arraystis = this.stiScreeningOptions.filter(x => x.itemId == this.form.controls.signsOfSTI.value[t]);
                                                if (arraystis[0].itemDisplayName == 'Others (O)') {
                                                    comment = this.form.controls.Specify.value;
                                                } else {
                                                    comment = '';
                                                }

                                                STIScreeningCommand.Screenings.push({
                                                    ScreeningTypeId: stioptions[0].masterId,
                                                    ScreeningCategoryId: stioptions[0].itemId,
                                                    ScreeningValueId: this.form.controls.signsOfSTI.value[t],
                                                    Comment: comment
                                                });


                                            }

                                            if (this.form.controls.contraindications_PrEP_Present.value.length > 0) {
                                                for (let t = 0; t < this.form.controls.contraindications_PrEP_Present.value.length; t++) {


                                                    STIScreeningCommand.Screenings.push({
                                                        ScreeningTypeId: this.prepContraindicationsOptions[0].masterId,
                                                        ScreeningCategoryId: this.prepContraindicationsOptions[0].masterId,
                                                        ScreeningValueId: this.form.controls.contraindications_PrEP_Present.value[t],
                                                        Comment: ''
                                                    });


                                                }
                                            }
                                        }
                                        if (this.isEdit == true) {

                                           

                                            const prepStatusCommand: PrepStatusCommand = {
                                                Id: this.form.controls.idprep.value == null ? 0 : this.form.controls.idprep.value,
                                                PatientId: this.patientId,
                                                PatientEncounterId: this.patientMasterVisitId,
                                                SignsOrSymptomsHIV: this.form.controls.signsOrSymptomsHIV.value,
                                                AdherenceCounsellingDone: this.form.controls.adherenceCounselling.value == "" ? 0 : this.form.controls.adherenceCounselling.value,
                                                PrepStatusToday: this.form.controls.PrEPStatusToday.value == "" ? 0 : this.form.controls.PrEPStatusToday.value,
                                                CreatedBy: this.userId,
                                                CondomsIssued: this.form.controls.condomsIssued.value == "" ? 0 : this.form.controls.condomsIssued.value,
                                                NoOfCondoms: this.form.controls.noCondomsIssued.value == "" ? 0 : this.form.controls.noCondomsIssued.value,
                                                DateField: EnrollmentDate
                                            };

                                            const prepStiScreeningTreatmentCommand = this.prepService.UpdateStiScreeningTreatment(STIScreeningCommand).subscribe((res) => {

                                            }, (error) => {
                                                console.log(error);
                                            });
                                            const prepStatusApiCommand = this.prepService.savePrepStatus(prepStatusCommand).subscribe((res) => {

                                            }, (error) => {
                                                console.log(error);
                                            });
                                        }
                                        else {
                                            const prepStatusCommand: PrepStatusCommand = {
                                                Id: 0,
                                                PatientId: this.patientId,
                                                PatientEncounterId: this.patientMasterVisitId,
                                                SignsOrSymptomsHIV: this.form.controls.signsOrSymptomsHIV.value,
                                                AdherenceCounsellingDone: this.form.controls.adherenceCounselling.value == "" ? 0 : this.form.controls.adherenceCounselling.value,
                                                PrepStatusToday: this.form.controls.PrEPStatusToday.value == "" ? 0 : this.form.controls.PrEPStatusToday.value,
                                                CreatedBy: this.userId,
                                                CondomsIssued: this.form.controls.condomsIssued.value == "" ? 0 : this.form.controls.condomsIssued.value,
                                                NoOfCondoms: this.form.controls.noCondomsIssued.value == "" ? 0 : this.form.controls.noCondomsIssued.value,
                                                DateField: EnrollmentDate
                                            };

                                            const prepStiScreeningTreatmentCommand = this.prepService.StiScreeningTreatment(STIScreeningCommand).subscribe((res) => {


                                            }, (error) => {
                                                console.log(error);
                                            });
                                            const prepStatusApiCommand = this.prepService.savePrepStatus(prepStatusCommand).subscribe((res) => {


                                            }, (error) => {
                                                console.log(error);
                                            });

                                        }



                                        if (this.person.gender.toLowerCase() == 'female') {
                                            this.pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Not Pregnant');
                                            const isPregnant = this.yesnoOptions.filter(obj => obj.itemId == pregnant);
                                            if (isPregnant.length > 0) {
                                                if (isPregnant[0].itemName == 'Yes') {
                                                    this.pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Pregnant');
                                                }
                                            }

                                            const pregnancyIndicatorCommand: PregnancyIndicatorCommand = {
                                                PatientId: this.patientId,
                                                PatientMasterVisitId: this.patientMasterVisitId,
                                                LMP: lmp,
                                                EDD: null,
                                                PregnancyStatusId: this.pregnancyOption[0].itemId,
                                                PregnancyPlanned: pregnancyPlanned,
                                                PlanningToGetPregnant: planningToGetPregnant,
                                                BreastFeeding: breastFeeding,
                                                ANCProfile: false,
                                                ANCProfileDate: null,
                                                Active: false,
                                                DeleteFlag: false,
                                                CreatedBy: this.userId,
                                                CreateDate: new Date(),
                                                AuditData: null,
                                                VisitDate: EnrollmentDate
                                            };

                                            const pregnancyIndicator = this.personHomeService.savePregnancyIndicatorCommand(pregnancyIndicatorCommand).subscribe((res) => {


                                            }, (error) => {
                                                console.log(error);
                                            });
                                            const familyPlanningCommand: FamilyPlanningCommand = {
                                                Id: 0,
                                                PatientId: this.patientId,
                                                PatientMasterVisitId: this.patientMasterVisitId,
                                                FamilyPlanningStatusId: onFamilyPlanning,
                                                ReasonNotOnFPId: 0,
                                                DeleteFlag: false,
                                                CreatedBy: this.userId,
                                                CreateDate: new Date(),
                                                VisitDate: new Date(),
                                                AuditData: ''
                                            };


                                            if (this.isEdit == false) {
                                                // family planning method

                                                const familyPlanningMethodCommand: FamilyPlanningMethodCommand = {
                                                    Id: 0,
                                                    PatientId: this.patientId,
                                                    PatientFPId: 0,
                                                    FPMethodId: familyPlanningMethods,
                                                    Active: true,
                                                    DeleteFlag: false,
                                                    CreatedBy: this.userId,
                                                    CreateDate: new Date(),
                                                    AuditData: ''
                                                };

                                                this.personHomeService.savePncFamilyPlanning(familyPlanningCommand).subscribe((res) => {
                                                    familyPlanningMethodCommand.PatientFPId = res['patientId'];
                                                    if (familyPlanningMethods !== undefined) {
                                                        const pncFamilyPlanningMethod = this.personHomeService.savePncFamilyPlanningMethod
                                                            (familyPlanningMethodCommand).subscribe(
                                                                (res) => {
                                                                    console.log(`family planning method`);
                                                                    console.log(res);
                                                                }, (error) => {
                                                                    console.log(error);
                                                                });
                                                    }
                                                });
                                            }
                                            if (this.isEdit == true) {
                                                const familyPlanningEditCommand: FamilyPlanningEditCommand = {
                                                    Id: id_familyPlanning,
                                                    FamilyPlanningStatusId: onFamilyPlanning,
                                                    ReasonNotOnFPId: 0
                                                };

                                                const fpMethod = fpMethodId;
                                                const updateFamilyPlanningMethodCommand: PatientFamilyPlanningMethodEditCommand = {
                                                    Id: fpMethodId ? fpMethod : 0,
                                                    FPMethodId: familyPlanningMethods,
                                                    PatientId: this.patientId,
                                                    PatientFPId: id_familyPlanning,
                                                    UserId: this.userId
                                                };

                                                const pncFamilyPlanning = this.personHomeService.updateFamilyPlanning(familyPlanningEditCommand);
                                                const pncFamilyPlanningMethodEdit = this.personHomeService.updatePncFamilyPlanningMethod(updateFamilyPlanningMethodCommand);

                                                forkJoin([pncFamilyPlanning, pncFamilyPlanningMethodEdit]).subscribe(
                                                    (result) => {
                                                        console.log(result);


                                                    },
                                                    (error) => {
                                                        console.log(error);
                                                    }
                                                );
                                            }
                                        }

                                        if (this.isEdit == false) {
                                            const nextAppointmentCommand: NextAppointmentCommand = {
                                                PatientId: this.patientId,
                                                PatientMasterVisitId: this.patientMasterVisitId,
                                                ServiceAreaId: this.serviceId,
                                                AppointmentDate: nextAppointmentDate
                                                    ? moment(nextAppointmentDate).toDate() : null,
                                                Description: '',
                                                StatusDate: new Date(),
                                                DifferentiatedCareId: 0,
                                                AppointmentReason: 'Follow up',
                                                CreatedBy: this.userId
                                            };
                                            const matNextAppointment = this.personHomeService.saveNextAppointment(nextAppointmentCommand).subscribe((res) => {
                                                console.log(res);

                                            }, (error) => {
                                                console.log(error);
                                            });
                                        }

                                    }

                                },
                                (error) => {
                                    console.log(error);
                                }
                            );




                            if (this.isEdit == true) {
                                if (this.Appointmentid == null) {
                                    const nextAppointmentCommand: NextAppointmentCommand = {
                                        PatientId: this.patientId,
                                        PatientMasterVisitId: this.patientMasterVisitId,
                                        ServiceAreaId: this.serviceId,
                                        AppointmentDate: nextAppointmentDate
                                            ? moment(nextAppointmentDate).toDate() : null,
                                        Description: '',
                                        StatusDate: new Date(),
                                        DifferentiatedCareId: 0,
                                        AppointmentReason: 'Follow up',
                                        CreatedBy: this.userId
                                    };
                                    const matNextAppointment = this.personHomeService.saveNextAppointment(nextAppointmentCommand).subscribe((res) => {
                                        console.log(res);

                                    }, (error) => {
                                        console.log(error);
                                    });
                                }
                                else {
                                    const updateNextAppointment = {
                                        AppointmentId: this.Appointmentid,
                                        AppointmentDate: nextAppointmentDate,
                                        Description: ''
                                    };

                                    const updateAppointmentCommand = this.personHomeService.updateNextAppointment(updateNextAppointment).subscribe((res) => {
                                        console.log(res);

                                    }, (error) => {
                                        console.log(error);
                                    });
                                }
                            }


                            const PatientProfile = this.personHomeService.AddHivPartnerProfile
                                (this.patientId, this.hivsavedprofileOptions, this.userId).subscribe((res) => {

                                }, (error) => {
                                    this.snotifyService.error('Error saving hiv partner profile ' + error, 'HIV Status Partner Profile',
                                        this.notificationService.getConfig());
                                });
                            this.zone.run(() => {
                                this.zone.run(() => {
                                    this.router.navigate(
                                        ['/prep/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                                        { relativeTo: this.route });
                                });
                            });




                        }




                    },

                    (err) => {
                        this.snotifyService.error('Error completing enrollment ' + err, 'Enrollment',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );
            }
        );
    }
}





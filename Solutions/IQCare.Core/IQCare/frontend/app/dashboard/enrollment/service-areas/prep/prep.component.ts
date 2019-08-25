
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

@Component({
    selector: 'app-prep',
    templateUrl: './prep.component.html',
    styleUrls: ['./prep.component.css']
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
    minDate: Date;
    facilityselected: any[] = [];
    personPopulation: PersonPopulation;
    ClientTypes: any[] = [];
    entrypoints: LookupItemView[] = [];
    PrepRegimen: LookupItemView[] = [];
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
    isMale: boolean = true;
    isFemale: boolean = false;
    patientMasterVisitId: number;
    public hivpositiveprofileOptions: any[] = [];
    public hivsavedprofileOptions: any[] = [];
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


    facilityList: any[] = [];
    protected _onDestroy = new Subject<void>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private _lookupItemService: LookupItemService,
        private personHomeService: PersonHomeService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private registrationService: RegistrationService,
        private enrollmentService: EnrollmentService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private searchService: SearchService,
        private recordsService: RecordsService) {
        this.maxDate = new Date();

        this.isVisible = false;
        this.FacilitySelected.valueChanges.pipe(debounceTime(400)).subscribe(data => {
            this.personHomeService.filterFacilities(data).subscribe(res => {

                this.filteredfacilities = res['facilityList'];
            });
        });

    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { id, serviceId, serviceCode, edit } = params;
            this.personId = id;
            this.serviceId = serviceId;
            this.serviceCode = serviceCode;


            console.log(this.serviceId);
            if (edit == 1) {
                this.isEdit = true;
            }

            this.userId = JSON.parse(localStorage.getItem('appUserId'));
            this.posId = localStorage.getItem('appPosID');
        });
        this.route.data.subscribe((res) => {

            console.log(res);
            const { PartnerCCCEnrollmentArray,
                PatientIdentifierArray
                , SexWithoutCondomArray
            } = res;
            this.sexwithoutcondomoptions = SexWithoutCondomArray['lookupItems'];
            this.patientIdentifieroptions = PatientIdentifierArray['lookupItems'];
            this.partnercccenrollmentoptions = PartnerCCCEnrollmentArray['lookupItems'];
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
            KeyPopulation: new FormControl(''),
            populationType: new FormControl('', [Validators.required]),
            //  DiscordantCouple: new FormControl(''),
            PrevPrepUse: new FormControl(''),
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
            AppointmentId: new FormControl('')



        });

        this.getYears();
        this.isVisible = false;



        this._lookupItemService.getByGroupName('KeyPopulation').subscribe(
            (res) => {
                this.keyPops = res['lookupItems'];
            }
        );
        this._lookupItemService.getByGroupName('FPMethod').subscribe((res) => {
            this.fpMethods = res['lookupItems'];
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

        this.form.controls.TransferInMflCode.valueChanges.pipe(debounceTime(400)).subscribe(data => {
            this.personHomeService.filtermflcode(data).subscribe(res => {

                this.filteredfacilities = res['facilityList'];
            });
        });


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
                console.log(this.ClientTypes);
                console.log(this.patientTypes);

            }
        );
        this.form.controls.KeyPopulation.disable({ onlySelf: true });
        // this.form.controls.DiscordantCouple.disable({ onlySelf: true });

        this.loadPopulationTypes(this.personId);

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


        if (this.isEdit) {
            this.loadPatient();
            this.loadFamilyPlanningMethod();
            this.loadFamilyPlanning();
            this.loadCircumcisionStatus();
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
    loadhivprofiles() {
        /*  this.personHomeService.getHivPartnerProfile(this.patientId).subscribe((result) => {
                      this.hivpositiveprofileOptions
          });*/
    }
    loadAppointments() {
        this.personHomeService.getAppointments(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                if (result) {
                    const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                    this.form.get('nextAppointmentDate').setValue(result.appointmentDate);

                    this.form.get('Appointmentid').setValue(result.id);
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
                this.loadPregnancyIndicator(this.patientMasterVisitId);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPregnancyIndicator(patientMasterVisitId: number): void {
        this.personHomeService.getPregnancyIndicator(this.patientId, patientMasterVisitId).subscribe(
            (res) => {
                if (res) {
                    this.form.controls.lmp.setValue(res.lmp);
                    this.form.controls.pregnancyPlanned.setValue(res.pregnancyPlanned);
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

    loadFamilyPlanningMethod(): void {
        this.personHomeService.getFamilyPlanningMethod(this.patientId).subscribe(
            (res) => {
                // console.log(res);
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

    loadFamilyPlanning(): void {
        this.personHomeService.getFamilyPlanning(this.patientId, this.patientMasterVisitId).subscribe(
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

    loadCircumcisionStatus() {
        this.personHomeService.getCircumcisionStatus(this.patientId).subscribe(
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
            HivSerodiscordantduration, partnersexcondoms, hivpartnerchildren, CCCNumber } = this.form.value;
        if (partnercccenrollment !== undefined && partnercccenrollment !== null) {
            if (parseInt(partnercccenrollment, 10) > 0) {

                let CCCEnrollmentText = this.partnercccenrollmentoptions.filter(x => x.itemId == partnercccenrollment).map(x => x.displayName);
                let sexwithouttext = this.sexwithoutcondomoptions.filter(x => x.itemId == partnersexcondoms).map(x => x.displayName);
                this.hivpositiveprofileOptions.push({
                    HivPositiveStatusDate: partnerHivStatusDate,
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
                    CCCEnrollment: partnercccenrollment,
                    PartnerARTStartDate: partnerARTStartDate,
                    HivSerodiscordantduration: HivSerodiscordantduration,
                    SexWithoutCondoms: partnersexcondoms,
                    NumberofChildren: hivpartnerchildren,
                    CCCNumber: CCCNumber,
                    CreatedBy: this.userId,
                    DeleteFlag: false


                });
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
                // console.log(p);
                this.person = p;
                console.log(this.person);
                if (this.person != null) {

                    if (this.person.dateOfBirth != null && this.person.dateOfBirth != undefined) {
                        this.minDate = this.person.dateOfBirth;
                    }

                    if (this.person.gender != null && this.person.gender != undefined) {
                        if (this.person.gender.toLowerCase() == 'male') {
                            this.isMale = true;
                            this.isFemale = false;
                        } else if (this.person.gender.toLowerCase() == 'female') {
                            this.isFemale = true;
                            this.isMale = false;
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
                // console.log(this.personView$);
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
                // console.log(result);
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

                const arrayValue = [];
                arrayValue.push(res);
                console.log(arrayValue);
                let itemid: number;
                itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'Yes');

                if (arrayValue.length > 0) {

                    this.form.controls.ClientTransferIn.setValue(this.ClientTypes[itemid]['itemId']);
                    this.isVisible = true;
                    this.form.controls.TransferInDate.setValue(arrayValue[0]['transferInDate']);
                    this.form.controls.TransferInMflCode.setValue(arrayValue[0]['mflCode']);
                    console.log(this.form.controls.CurrentRegimen);

                    //  this.form.controls.FacilityListSelected.setValue(arrayValue[0]['facilityFrom']);
                    // this.FacilitySelected.setValue(arrayValue[0]['facilityFrom']);

                    this.personHomeService.getFacility(arrayValue[0]['mflCode']).subscribe(
                        (result) => {
                            if (result.length > 0) {
                                console.log(result);
                                this.filteredfacilities = result;
                                this.FacilitySelected.setValue(result[0]);
                            }
                        }
                    );

                    // this.filtercorrectfacilities(arrayValue[0]['facilityFrom']);
                    this.form.controls.ClinicalNotes.setValue(arrayValue[0]['transferInNotes']);
                    let currentTreatment: string;
                    currentTreatment = arrayValue[0]['currentTreatment'].toString();
                    this.form.controls.CurrentRegimen.setValue(parseInt(currentTreatment, 10));

                } else {
                    itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'No');

                    this.form.controls.PrevPrepUse.setValue(itemid);

                }

            },
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
                console.log(arrayValue);
                if (arrayValue.length > 0) {
                    //   this.form.controls.PrevPrepUse.setValue(1);

                    //  this.form.controls.Weeks.enable({ onlySelf: true });
                    this.form.controls.Months.enable({ onlySelf: true });
                    this.form.controls.InitiationDate.enable({ onlySelf: true });
                    this.form.controls.DateLastUsed.enable({ onlySelf: true });




                    // this.form.controls.Weeks.setValue(arrayValue[0]['weeks']);
                    this.form.controls.Months.setValue(arrayValue[0]['months']);
                    if (arrayValue[0]['initiationDate'] != null) {
                        this.form.controls.InitiationDate.setValue(moment(arrayValue[0]['initiationDate'].toString()).toDate());
                    }

                    if (arrayValue[0]['dateLastUsed'] != null) {
                        this.form.controls.DateLastUsed.setValue(moment(arrayValue[0]['dateLastUsed'].toString()).toDate());
                    }
                } else {
                    //this.form.controls.PrevPrepUse.setValue(0);
                    // this.form.controls.Weeks.disable({ onlySelf: true });
                    this.form.controls.Months.disable({ onlySelf: true });
                    this.form.controls.DateLastUsed.disable({ onlySelf: true });
                    this.form.controls.InitiationDate.disable({ onlySelf: true });
                }
                console.log('prevprepuse');
                console.log(res);
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
                    console.log(result);
                    let t: number;
                    const array: any[] = [];


                    let id: number;
                    let value: string;
                    id = this.serviceAreaIdentifiers[0]['identifierId'];
                    array.push(result);
                    console.log(array);
                    t = array[0].findIndex(x => x.identifierTypeId === id);
                    console.log(t);
                    value = array[0][t].identifierValue.toString();
                    this.arraysplit = value.split('/');
                    this.form.get('EnrollmentNumber').setValue(this.arraysplit[2]);
                    let years: string;
                    years = this.arraysplit[1].toString();
                    this.form.get('Year').setValue(parseInt(years, 10));
                    console.log(this.form.controls.Year);
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
                this.loadPopulationTypes(this.personId);
                this.loadEnrollmentPatientMasterVisitId(result.id);


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


    loadPopulationTypes(personId: number): void {
        this.personHomeService.getPersonPopulationType(personId).subscribe(
            (result) => {
                console.log('populationtype');
                console.log(result);
                if (result.length > 0) {
                    if (result[0].populationType == 'General Population') {
                        this.form.controls.populationType.setValue(1);
                    } else if (result[0].populationType == 'Discordant Couple') {
                        this.form.controls.populationType.setValue(3);
                        /* this.form.controls.DiscordantCouple.enable({ onlySelf: false });
                         const arrayValue = [];
                         result.forEach(element => {
                             arrayValue.push(element.populationCategory);
                         });
                         this.form.controls.DiscordantCouple.setValue(arrayValue); */

                    } else {
                        this.form.controls.populationType.setValue(2);
                        this.form.controls.KeyPopulation.enable({ onlySelf: false });
                        const arrayValue = [];
                        result.forEach(element => {
                            arrayValue.push(element.populationCategory);
                        });
                        this.form.controls.KeyPopulation.setValue(arrayValue);
                    }


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
                this.form.controls.FacilityListSelected.setValue('');
                this.form.controls.CurrentRegimen.setValue('');
                this.form.controls.ClinicalNotes.setValue('');
                this.form.controls.TransferInMflCode.setValue('');
                this.form.controls.DateLastUsed.disable({ onlySelf: true });
                //this.form.controls.PrevPrepUse.setValue('');
                this.form.controls.Months.disable({ onlySelf: true });
                this.form.controls.InitiationDate.disable({ onlySelf: true });

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
        console.log(event);
        let value: string;
        value = event.option.viewValue.toString();

        let arr: string[] = [];
        arr = value.split('-');

        this.personHomeService.getFacility(arr[0].toString()).subscribe(
            (result) => {
                if (result.length > 0) {
                    console.log(result);
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
        console.log(this.form.controls.TransferInMflCode.value);
        console.log('FacilitySelected Outcome');
        console.log(this.FacilitySelected.value);

    }
    onPregnancySelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.form.controls.pregnancyPlanned.enable({ onlySelf: true });

            // disable
            this.form.controls.onFamilyPlanning.disable({ onlySelf: true });
            this.form.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.form.controls.planningToGetPregnant.disable({ onlySelf: true });

            // Reset values
            this.form.controls.onFamilyPlanning.setValue('');
            this.form.controls.familyPlanningMethods.setValue('');
            this.form.controls.planningToGetPregnant.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.controls.pregnancyPlanned.disable({ onlySelf: true });
            this.form.controls.pregnancyPlanned.setValue('');

            // enable
            this.form.controls.onFamilyPlanning.enable({ onlySelf: true });
            // this.FertilityIntentionForm.controls.familyPlanningMethods.enable({ onlySelf: true });
            this.form.controls.planningToGetPregnant.enable({ onlySelf: true });
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

        const { EnrollmentDate, KeyPopulation, populationType, EnrollmentNumber, MFLCode, Year, ClientTransferIn,
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
        console.log(this.form);
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

        const { EnrollmentDate, KeyPopulation, populationType, EnrollmentNumber, MFLCode, Year, ClientTransferIn
            , TransferInDate, TransferInMflCode, CurrentRegimen, ClinicalNotes, InitiationDate, IsSchool,
            Months, Referredfrom, isClientCircumcised, lmp, pregnant,
            pregnancyPlanned, breastFeeding, onFamilyPlanning, DateLastUsed,
            familyPlanningMethods, planningToGetPregnant, id_familyPlanning, fpMethodId, nextAppointmentDate, nextAppointmentGiven, Appointmentid
        } = this.form.value;
        this.personPopulation.KeyPopulation = KeyPopulation;
        this.personPopulation.populationType = populationType;
        // this.personPopulation.DiscordantCouplePopulation = DiscordantCouple;

        let itemdisplayname: any[] = [];
        itemdisplayname = this.ClientTypes.filter(x => x.itemId == ClientTransferIn).map(o => {
            return o.itemDisplayName.toString();
        });

        let idnumber: string;
        idnumber = MFLCode + '/' + Year + '/' + EnrollmentNumber;
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



        const populationTypes = this.registrationService.addPersonPopulationType(this.personId,
            this.userId, this.personPopulation);
        const addPatient = this.registrationService.addPatient(this.personId, this.userId, EnrollmentDate, this.posId);

        forkJoin([addPatient, populationTypes]).subscribe(
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
                                const circumcisionStatus = this.personHomeService.saveCircumcisionStatus(this.clientCircumcisionStatusCommand);
                            }
                            if (this.person.gender.toLowerCase() == 'female') {
                                this.pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Not Pregnant');
                                const isPregnant = this.yesnoOptions.filter(obj => obj.itemId == pregnant);
                                if (isPregnant.length > 0) {
                                    if (isPregnant[0].itemName == 'Yes') {
                                        this.pregnancyOption = this.pregnancyStatusOptions.filter(obj => obj.displayName == 'Pregnant');
                                    }
                                }
                                this.personHomeService.getPatientEnrollmentMasterVisitByServiceAreaId(this.patientId, this.serviceId).subscribe(
                                    (result) => {
                                        this.patientMasterVisitId = result[0]['id'];
                                        console.log('results for mastervisitid');
                                        console.log(result);
                                        console.log(this.patientMasterVisitId)
                                        if (this.patientMasterVisitId != null && this.patientMasterVisitId > 0) {
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
                                                console.log(res);

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

                                    },
                                    (error) => {
                                        console.log(error);
                                    }
                                );


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
                                const matNextAppointment = this.personHomeService.saveNextAppointment(nextAppointmentCommand);
                            }
                            else {
                                const updateNextAppointment = {
                                    AppointmentId: Appointmentid,
                                    AppointmentDate: nextAppointmentDate,
                                    Description: ''
                                };

                                const updateAppointmentCommand = this.personHomeService.updateNextAppointment(updateNextAppointment);
                            }
                            const PatientProfile = this.personHomeService.AddHivPartnerProfile
                                (this.patientId, this.hivsavedprofileOptions).subscribe((res) => {

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





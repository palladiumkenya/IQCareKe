
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
import * as moment from 'moment';
import { take } from 'rxjs/operators';

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
    patientId: number;
    serviceId: number;
    serviceCode: string;
    userId: number;
    posId: string;
    isEdit: boolean = false;
    isVisible: boolean = false;
    maxDate: Date;
    minDate: Date;
    personPopulation: PersonPopulation;
    ClientTypes: any[] = [];
    entrypoints: LookupItemView[] = [];
    PrepRegimen: LookupItemView[] = [];
    keyPops: LookupItemView[] = [];
    disPops: LookupItemView[] = [];
    patientTypes: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    serviceAreaIdentifiers: any[] = [];
    identifiers: any[] = [];
    years: any[] = [];
    arraysplit: any[] = [];
    currentDate: Date;
    facilities: any[] = [];
    isSchoolVisible: boolean = false;
    public filteredfacilities: ReplaySubject<any[]> = new ReplaySubject<any[]>();
    public FacilitySelected: FormControl = new FormControl();


    facilityList: any[] = [];
    protected _onDestroy = new Subject<void>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private _formBuilder: FormBuilder,
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
    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { id, serviceId, serviceCode, edit } = params;
            this.personId = id;
            this.serviceId = serviceId;
            this.serviceCode = serviceCode;

            if (edit == 1) {
                this.isEdit = true;
            }

            this.userId = JSON.parse(localStorage.getItem('appUserId'));
            this.posId = localStorage.getItem('appPosID');
        });
        this.personPopulation = new PersonPopulation();

        this.form = this._formBuilder.group({
            ClientTransferIn: new FormControl('', [Validators.required]),
            Referredfrom: new FormControl('', [Validators.required]),
            EnrollmentDate: new FormControl('', [Validators.required]),
            EnrollmentNumber: new FormControl('', Validators.compose([
                Validators.required,
                Validators.pattern(/^([0-9]{5})$/)
            ])),
            MFLCode: new FormControl('', [Validators.required]),
            Year: new FormControl('', [Validators.required]),
            TransferInDate: new FormControl(''),
            InitiationDate: new FormControl('', [Validators.required]),
            FacilityListSelected: new FormControl(''),
            TransferInMflCode: new FormControl(''),
            CurrentRegimen: new FormControl(''),
            ClinicalNotes: new FormControl(''),
            IsSchool: new FormControl(''),
            KeyPopulation: new FormControl(''),
            populationType: new FormControl('', [Validators.required]),
            DiscordantCouple: new FormControl(''),
            PrevPrepUse: new FormControl(''),
            Weeks: new FormControl(''),
            Months: new FormControl('')


        });

        this.getYears();
        this.isVisible = false;
        this._lookupItemService.getByGroupName('KeyPopulation').subscribe(
            (res) => {
                this.keyPops = res['lookupItems'];
            }
        );


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
        this.form.controls.DiscordantCouple.disable({ onlySelf: true });

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

                console.log('serviceidentifiers');
                console.log(this.serviceAreaIdentifiers);
            }
        );
        this._lookupItemService.getFacilityList().subscribe((res) => {
            this.facilityList = res['facilityList'];
            this.facilities = this.facilityList;

        });
        this._lookupItemService.getByGroupName('PrEPRegimen').subscribe(
            (res) => {
                this.PrepRegimen = res['lookupItems'];
            }
        );
        this.form.controls.Weeks.disable({ onlySelf: true });
        this.form.controls.Months.disable({ onlySelf: true });
        this.form.controls.InitiationDate.disable({ onlySelf: true });

        this.form.controls.MFLCode.setValue(this.posId);

       
        this.form.controls.FacilityListSelected.setValue(this.facilities);
        this.filteredfacilities.next(this.facilities.slice(0, 10));

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


    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personHomeService.getPatientByPersonId(personId).subscribe(
            p => {
                // console.log(p);
                this.person = p;

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
                const arrayValue = [];
                arrayValue.push(result);
                if (arrayValue[0]['inSchool'] != null) {
                    let inschool: string;
                    inschool = arrayValue[0]['inSchool'].toString();
                    this.form.controls.IsSchool.setValue(parseInt(inschool, 10));

                }

            }

        );
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

                    this.form.controls.FacilityListSelected.setValue(arrayValue[0]['facilityFrom']);
                    this.FacilitySelected.setValue(arrayValue[0]['facilityFrom']);

                    this.filtercorrectfacilities(arrayValue[0]['facilityFrom']);
                    this.form.controls.ClinicalNotes.setValue(arrayValue[0]['transferInNotes']);
                    let currentTreatment: string;
                    currentTreatment = arrayValue[0]['currentTreatment'].toString();
                    this.form.controls.CurrentRegimen.setValue(parseInt(currentTreatment, 10));

                } else {
                    itemid = this.ClientTypes.findIndex(x => x.itemDisplayName == 'No');

                    this.form.controls.PrevPrepUse.setValue(itemid);

                }
                console.log('prevprepuse');
                console.log(res);
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
                    this.form.controls.PrevPrepUse.setValue(1);

                    this.form.controls.Weeks.enable({ onlySelf: true });
                    this.form.controls.Months.enable({ onlySelf: true });





                    this.form.controls.Weeks.setValue(arrayValue[0]['weeks']);
                    this.form.controls.Months.setValue(arrayValue[0]['months']);
                    if (arrayValue[0]['initiationDate'] != null) {
                        this.form.controls.InitiationDate.setValue(moment(arrayValue[0]['initiationDate'].toString()).toDate());
                    }
                } else {
                    this.form.controls.PrevPrepUse.setValue(0);
                    this.form.controls.Weeks.disable({ onlySelf: true });
                    this.form.controls.Months.disable({ onlySelf: true });
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
                        this.form.controls.DiscordantCouple.enable({ onlySelf: false });
                        const arrayValue = [];
                        result.forEach(element => {
                            arrayValue.push(element.populationCategory);
                        });
                        this.form.controls.DiscordantCouple.setValue(arrayValue);

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
            } else {
                this.isVisible = false;
                this.form.controls.TransferInDate.setValue('');
                this.form.controls.FacilityListSelected.setValue('');
                this.form.controls.CurrentRegimen.setValue('');
                this.form.controls.ClinicalNotes.setValue('');
                this.form.controls.TransferInMflCode.setValue('');
            }
        }
    }

    // tslint:disable-next-line: use-life-cycle-interface
    ngAfterViewInit() {
        this.setInitialValue();
    }

    // tslint:disable-next-line: use-life-cycle-interface
    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

    protected setInitialValue() {
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
        this.filteredfacilities.pipe(take(1)).subscribe(() => {

        });
    }


    protected filtercorrectfacilities(value) {
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


    }

    getYears() {
        let year: number;
        this.currentDate = new Date();
        year = this.currentDate.getFullYear();

        for (let i = year; i >= 1900; i--) {
            this.years.push(i);
        }





    }

    onTextChanged(event) {
        this.filtercorrectfacilities(event.target.value);
    }

    changePrevUse(event) {

        let selectedvalue: number;
        selectedvalue = event.source.value;

        if (event.source.selected == true) {
            if (selectedvalue === 1) {
                this.form.controls.Weeks.enable({ onlySelf: true });
                this.form.controls.Months.enable({ onlySelf: true });
                this.form.controls.InitiationDate.enable({ onlySelf: true });
            } else {
                this.form.controls.Weeks.disable({ onlySelf: true });
                this.form.controls.Months.disable({ onlySelf: true });
                this.form.controls.InitiationDate.disable({ onlySelf: true });
            }
        }

    }
	

    change(event) {
        this.FacilitySelected.setValue('');
        console.log(event.value);
        let index: number;
        index = this.facilities.findIndex(x => x.name == event.value);

        if (index > -1) {
            let mflcode: number;
            mflcode = this.facilities[index].mflCode;
            this.form.controls.TransferInMflCode.setValue(mflcode);
            console.log(this.form.controls.TransferInMflCode.value);
        }
    }

    onPopulationTypeChange() {
        const popType = this.form.controls.populationType.value;
        if (popType == 1) {
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            this.form.controls.DiscordantCouple.disable({ onlySelf: false });
            this.form.controls.KeyPopulation.setValue([]);
            this.form.controls.DiscordantCouple.setValue([]);
        } else if (popType == 2) {
            this.form.controls.KeyPopulation.enable({ onlySelf: false });
            this.form.controls.DiscordantCouple.disable({ onlySelf: false });
            this.form.controls.DiscordantCouple.setValue([]);
        } else if (popType == 3) {
            this.form.controls.DiscordantCouple.enable({ onlySelf: false });
            this.form.controls.KeyPopulation.disable({ onlySelf: true });
            this.form.controls.KeyPopulation.setValue([]);
        }
    }

    public SaveValues() {

        const { EnrollmentDate, KeyPopulation, populationType, DiscordantCouple, EnrollmentNumber, MFLCode, Year, ClientTransferIn,
            FacilityListSelected,
            TransferInDate, TransferInMflCode, CurrentRegimen, ClinicalNotes,
        } = this.form.value;




        let itemdisplayname: any[] = [];
        itemdisplayname = this.ClientTypes.filter(x => x.itemId == ClientTransferIn).map(o => {
            return o.itemDisplayName.toString();
        });


        if (itemdisplayname[0] == 'Yes') {
            if ((TransferInMflCode == '' || TransferInMflCode == undefined) &&
                (TransferInDate == '' || TransferInDate == undefined)
                && (TransferInMflCode == '' || TransferInMflCode == undefined)
                && (FacilityListSelected == '' || FacilityListSelected == undefined)
                && (CurrentRegimen == '' || CurrentRegimen == undefined)) {
                this.snotifyService.error('Kindly note MFLCode,Transfer In facility,Transfer In Date and' +
                    'Current Regimen is' +
                    'required in the transfer in status', 'TransferDetails',
                    this.notificationService.getConfig());
                return;


            } else {
                if (this.form.valid == true) {
                    this.save();
                } else {
                    return;
                }
            }
        } else {

            if (this.form.valid == true) {
                this.save();
            } else {
                return;
            }
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
        const enrollment = new Enrollment();
        const { EnrollmentDate, KeyPopulation, populationType, DiscordantCouple, EnrollmentNumber, MFLCode, Year, ClientTransferIn
            , TransferInDate, TransferInMflCode, CurrentRegimen, ClinicalNotes, InitiationDate, FacilityListSelected, IsSchool,
            PrevPrepUse, Weeks, Months, Referredfrom
        } = this.form.value;
        this.personPopulation.KeyPopulation = KeyPopulation;
        this.personPopulation.populationType = populationType;
        this.personPopulation.DiscordantCouplePopulation = DiscordantCouple;

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
                        this.snotifyService.success('Successfully Enrolled ', 'Enrollment',
                            this.notificationService.getConfig());

                        localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                        this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                        this.store.dispatch(new Consent.PatientId(this.patientId));
                        this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                            this.patientId).subscribe();

                        if (itemdisplayname[0] == 'Yes') {
                            this.registrationService.addPatientTransferIn(this.patientId, this.serviceId
                                , TransferInDate, InitiationDate,
                                CurrentRegimen, FacilityListSelected
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
                        if (PrevPrepUse == '1') {
                            this.registrationService.addPatientARVHistory(this.patientId,
                                this.serviceId, 'ART', 'PrEP', '', this.userId, false,
                                Weeks, Months, InitiationDate, PrevPrepUse).subscribe(
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

                        this.zone.run(() => {
                            this.zone.run(() => {
                                this.router.navigate(
                                    ['/prep/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                                    { relativeTo: this.route });
                            });
                        });
                    },
                    (err) => {
                        this.snotifyService.error('Error completing enrollment ' + err, 'Enrollment',
                            this.notificationService.getConfig());
                    }
                );
            }
        );
    }
}





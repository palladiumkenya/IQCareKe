import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Decode } from './../../shared/_models/Decode';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, of, Subscription } from 'rxjs';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import * as moment from 'moment';
import { PersonView } from '../../dashboard/_model/personView';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { PharmacyService } from '../services/pharmacy.service';
import { Frequency } from '../models/frequency';
import { debounceTime } from 'rxjs/operators';
import { isUndefined } from 'util';
import { MatInput } from '@angular/material/input';
import { RegisterComponent } from '../../records/person/register/register.component';
import { checkNoChanges } from '@angular/core/src/render3/instructions';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { EncounterService } from '../../shared/_services/encounter.service';
@Component({
    selector: 'app-pharm-orderform',
    templateUrl: './pharm-orderform.component.html',
    styleUrls: ['./pharm-orderform.component.css'],
    providers: [
        EncounterService
    ]
})
export class PharmOrderformComponent implements OnInit {
    public pharmFormGroup: FormGroup;
    patientId: number;
    personId: number;
    userId: number;
    maxDate: Date;
    minDate: Date;
    DispensedBy: number;
    PrescribedBy: number;
    public personVitalWeight = 0;
    public person: PersonView;
    public personView$: Subscription;
    edit: number;
    patientMasterVisitId: number;
    treatmentprogramOptions: Decode[];
    periodTakenOptions: LookupItemView[];
    SwitchReasonOptions: LookupItemView[];
    TreatmentplanOptions: LookupItemView[];
    paedsOptions: LookupItemView[];
    AdultOptions: LookupItemView[];
    regimenlineOptions: LookupItemView[];
    EncounterTypeOptions: LookupItemView[];
    batchlistOptions: any[];
    freqoptions: Frequency[];
    facility: any[] = [];
    dosageFrequency: number;
    ActiveModules: any[] = [];
    PMSCM: string;
    SCMSamePointDispense: string;
    PMSCMfiltered: any[] = [];
    SCMSamePointDispensefiltered: any[] = [];
    treatmentstartarray: any[] = [];
    StartTreatment: boolean;
    patType: string;
    pmscmFlag: number = 0;
    DrugList: any[] = [];
    filteredDrugList: any[] = [];
    savestate: boolean = false;
    resetstate: boolean = false;
    closestate: boolean = false;
    regimenOptions: any[] = [];
    filteredlist: any[] = [];
    selectedOption;
    visibleFrequency: boolean = true;
    personvitals: any[];
    visibleMorningEvening: boolean = true;
    DrugArray: any[] = [];
    DrugId: string;
    Disabled: boolean = true;
    DrugAbbr: string;
    EncounterTypeId: number;
    @ViewChild('frmDrug') nameinput: MatInput;
    constructor(
        private snotifyService: SnotifyService,
        private encounterservice: EncounterService,
        private personService: PersonHomeService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private spinner: NgxSpinnerService,
        private pharmacyservice: PharmacyService
    ) {
        this.person = new PersonView();
        this.maxDate = new Date();
        /*  this.savestate = true;
          this.resetstate = true;
          this.closestate = true;*/
    }


    ngOnInit() {

        this.route.params.subscribe(params => {
            this.personId = params['personId'];
            this.patientId = params['patientId'];
            this.patientMasterVisitId = params['patientMasterVisitId'];
            this.edit = params['edit'];



        });


        this.route.data.subscribe((res) => {
            const { AdultArray, PaedsArray, FrequencyArray, ActiveModulesArray, TreatmentStartArray,
                PersonVitalsArray } = res;

            console.log(res);
            this.AdultOptions = AdultArray['lookupItems'];
            this.paedsOptions = PaedsArray['lookupItems'];
            this.facility = FrequencyArray;
            this.ActiveModules = ActiveModulesArray;
            this.PMSCMfiltered = this.ActiveModules.filter(x => parseInt(x.moduleID, 10) == 201);
            this.personvitals = PersonVitalsArray;

            if (this.personvitals.length > 0) {
                this.personVitalWeight = this.personvitals['0'].weight;
            }
            this.treatmentstartarray = TreatmentStartArray;
            if (this.treatmentstartarray != null) {
                this.StartTreatment = this.treatmentstartarray['startTreatment'];
            }
            if (this.PMSCMfiltered.length > 0) {
                this.PMSCM = this.PMSCMfiltered[0].moduleName;
            } else {
                this.PMSCM = '0';
            }
            this.SCMSamePointDispensefiltered = this.ActiveModules.filter(x => x.moduleID == 30);
            if (this.SCMSamePointDispensefiltered.length > 0) {
                this.SCMSamePointDispense = this.SCMSamePointDispensefiltered[0].moduleName;
            } else {
                this.SCMSamePointDispense = '0';
            }



        });




        this.dosageFrequency = this.facility['frequency'];
        console.log('Frequency' + this.dosageFrequency);
        this.getPatientDetailsById(this.personId);

        if (this.dosageFrequency == 1) {
            this.visibleFrequency = true;
            this.visibleMorningEvening = false;
        }
        else {
            this.visibleFrequency = false;
            this.visibleMorningEvening = true;
        }


        this._lookupItemService.getByGroupName('EncounterType').subscribe((res) => {
            this.EncounterTypeOptions = res['lookupItems'];
        });
        this._lookupItemService.getDecodeByCodeId(33).subscribe((res) => {
            this.treatmentprogramOptions = res['decodeItems'];
            console.log(this.treatmentprogramOptions);
        });

        this._lookupItemService.getByGroupName('PeriodDrugsTaken').subscribe(
            (res) => {
                this.periodTakenOptions = res['lookupItems'];
            }
        );

        this._lookupItemService.getByGroupName('TreatmentPlan').subscribe(
            (res) => {
                this.TreatmentplanOptions = res['lookupItems'];
            }
        );
        this.pharmacyservice.getFrequencylist().subscribe((res) => {
            console.log(this.freqoptions);
            console.log(res['frequencyItems']);
            this.freqoptions = res['frequencyItems'];
        });
        this.pharmFormGroup = this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            frmTreatmentProgram: new FormControl(''),
            frmPeriodTaken: new FormControl(''),
            frmTreatmentPlan: new FormControl(''),
            frmReason: new FormControl(''),
            frmRegimenLine: new FormControl(''),
            frmRegimen: new FormControl(''),
            frmDrug: new FormControl(''),
            frmBatchlist: new FormControl(''),
            frmFreq: new FormControl(''),
            txtDuration: new FormControl(''),
            txtMorning: new FormControl(''),
            txtMidday: new FormControl(''),
            txtEvening: new FormControl(''),
            txtNight: new FormControl(''),
            txtQuantityPres: new FormControl(''),
            txtQuantityDisp: new FormControl(''),
            txtDose: new FormControl(''),
            chkProphylaxis: new FormControl(''),
            frmDateDispensed: new FormControl(''),
            frmDatePrescibed: new FormControl('')



        });

        this.getDrugList(0, null, null);
        this.getFilteredDrugList(0, null, null);
        if (this.edit == 1) {
            this.loadExistingRecords();
        }
        this.pharmFormGroup.controls.frmDrug.valueChanges.pipe(
            debounceTime(400)
        ).subscribe(data => {
            if (data != null && data !== '' && data !== undefined) {
                const value = this.pharmFormGroup.controls.frmTreatmentProgram.value;
                if (value != null && value != undefined && value !== '') {
                    if (this.SCMSamePointDispense === 'PM/SCM With Same point dispense') {
                        this.getFilteredDrugList(1, value, data);

                    } else if (this.PMSCM === 'PM/SCM') {
                        this.getFilteredDrugList(1, value, data);
                    }
                    else {
                        this.getFilteredDrugList(0, value, data);
                    }
                }
                else {
                    this.getFilteredDrugList(0, null, data);
                }
            }

        });



    }

    loadExistingRecords() {
        this.pharmacyservice.getPharmacyVisitDetails(this.patientId, this.patientMasterVisitId).subscribe((res) => {
            if (res != null) {
                let array: any[] = [];
                array = res['drugDetails'];
                let VisitDate: Date;
                let Ordereddate: Date;
                let DispensedDate: Date;
                if (res['visitDate'] != null) {
                    VisitDate = res['visitDate'];
                    this.pharmFormGroup.controls.visitDate.setValue(VisitDate);
                }
                if (res['orderedByDate'] != null) {
                    Ordereddate = res['orderedByDate'];
                    this.pharmFormGroup.controls.frmDatePrescibed.setValue(Ordereddate);
                }
                if (res['dispensedDate'] != null) {
                    DispensedDate = res['dispensedDate'];
                    this.pharmFormGroup.controls.frmDateDispensed.setValue(DispensedDate);
                }

                console.log(array);
                if (array.length > 0) {
                    this.DrugArray = [];
                    array.forEach(x => {
                        this.DrugArray.push({
                            DrugName: x.drugName,
                            DrugId: x.drugId,
                            DrugAbb: x.drugAbb,
                            batchId: x.batchId,
                            batchText: x.batchText,
                            Dose: x.dose,
                            Freq: x.freq,
                            FreqText: x.freqText,
                            Duration: x.duration,
                            QuantityPres: x.quantityPres,
                            QUantityDisp: x.qUantityDisp,
                            Reason: x.reason,
                            ReasonText: x.reasontext,
                            Regimen: x.regimen,
                            Regimentext: x.regimentext,
                            Regimenline: x.regimenline,
                            Regimenlinetext: x.regimenlinetext,
                            TreatmentPlan: x.treatmentPlan,
                            TreatmentPlantext: x.treatmentPlantext,
                            TreatmentProgram: x.treatmentProgram,
                            TreatmentProgramText: x.treatmentProgramText,
                            Morning: x.morning,
                            Midday: x.midday,
                            Evening: x.evening,
                            Night: x.night,
                            Period: x.period,
                            PeriodTakenText: x.periodTakentext,
                            Prophylaxis: x.prophylaxis,
                            Disabled: x.qUantityDisp.toString == '0' || x.qUantityDisp.toString == '' || x.qUantityDisp == null ? false : true
                        });



                    });

                }
            }

        }, (err) => {
            this.snotifyService.error('Error getting drug details ' + err, 'Drug Prescribed',
                this.notificationService.getConfig());
        });
    }
    displaydrug(drug?: any): string | undefined {
        return drug ? drug.drugName : undefined;
    }
    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
            p => {
                this.person = p;

                if (this.person != null) {


                    if (this.person.patientType != null) {
                        this.patType = this.person.patientType;

                        if (this.patType.toString().toLowerCase() == 'transit') {
                            this.StartTreatment = true;
                        }
                    }

                    if (this.person.enrollmentDate != null) {
                        this.minDate = this.person.enrollmentDate;
                    }
                    console.log(this.person);
                    if (this.person.ageNumber > 14) {
                        this.regimenlineOptions = this.AdultOptions;




                    }
                    else { this.regimenlineOptions = this.paedsOptions; }
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

    TreatmentProgram(event) {
        const value = this.pharmFormGroup.controls.frmTreatmentProgram.value;

        this._lookupItemService.getByGroupName(value).subscribe(
            (res) => {
                this.regimenlineOptions = [];
                console.log(res);
                this.regimenlineOptions = res['lookupItems'];
            }
        );

        if (this.SCMSamePointDispense === 'PM/SCM With Same point dispense') {
            this.pmscmFlag = 1;
            this.getDrugList(1, value, null);
            this.pharmFormGroup.controls.frmBatchlist.enable({ onlySelf: true });
            this.pharmFormGroup.controls.txtQuantityDisp.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmDateDispensed.enable({ onlySelf: true });


        } else if (this.PMSCM === 'PM/SCM') {
            this.getDrugList(1, value, null);
            this.pharmFormGroup.controls.frmBatchlist.disable({ onlySelf: true });
            this.pharmFormGroup.controls.txtQuantityDisp.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmDateDispensed.disable({ onlySelf: true });
        }
        else {
            this.getDrugList(0, value, null);

            this.pharmFormGroup.controls.frmBatchlist.disable({ onlySelf: true });
            this.pharmFormGroup.controls.txtQuantityDisp.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmDateDispensed.enable({ onlySelf: true });
        }
        if (value === 'PMTCT') {

            if (this.person.gender.toLowerCase() == 'male' && this.person.ageNumber >= 9) {
                this.snotifyService.error('PMTCT is for female patients only who ' +
                    'are older than 9 years', 'Error',
                    this.notificationService.getConfig());
                this.pharmFormGroup.controls.frmTreatmentProgram.setValue('');
            }
            this.pharmFormGroup.controls.frmPeriodTaken.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmTreatmentPlan.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmTreatmentPlan.setValue('');
            this.pharmFormGroup.controls.frmReason.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmRegimenLine.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.setValue('');
            this.pharmFormGroup.controls.frmRegimen.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.setValue('');


        } else if (this.StartTreatment === false && value === 'ART') {
            var treatmentstart = this.TreatmentplanOptions.filter(x => x.itemName === 'Start Treatment');

            this.pharmFormGroup.controls.frmTreatmentPlan.setValue(treatmentstart[0].itemId);
            this.pharmFormGroup.controls.frmPeriodTaken.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
            this.pharmFormGroup.controls.frmTreatmentPlan.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.setValue('');
            this.pharmFormGroup.controls.frmRegimenLine.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.setValue('');
            this.pharmFormGroup.controls.frmRegimen.enable({ onlySelf: true });


            this.drugSwitchInterruptionReason();
            this.getCurrentRegimen();


        } else if (this.StartTreatment === true && value === 'ART') {
            var treatmentstart = this.TreatmentplanOptions.filter(x => x.itemName === 'Continue current treatment');

            this.pharmFormGroup.controls.frmTreatmentPlan.setValue(treatmentstart[0].itemId);
            this.pharmFormGroup.controls.frmPeriodTaken.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
            this.pharmFormGroup.controls.frmTreatmentPlan.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.setValue('');
            this.pharmFormGroup.controls.frmRegimenLine.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.setValue('');
            this.pharmFormGroup.controls.frmRegimen.enable({ onlySelf: true });

            this.drugSwitchInterruptionReason();
            this.getCurrentRegimen();
            //   || value == 'HBV' || value == 'Hepatitis B'
        } else if (value === 'Non-ART') {
            this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
            this.pharmFormGroup.controls.frmPeriodTaken.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmTreatmentPlan.setValue('');
            this.pharmFormGroup.controls.frmTreatmentPlan.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.setValue('');
            this.pharmFormGroup.controls.frmRegimenLine.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.setValue('');
            this.pharmFormGroup.controls.frmRegimen.disable({ onlySelf: true });


        } else {
            this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
            this.pharmFormGroup.controls.frmPeriodTaken.disable({ onlySelf: true });
            this.pharmFormGroup.controls.frmTreatmentPlan.setValue('');
            this.pharmFormGroup.controls.frmTreatmentPlan.enable({ onlySelf: true });

            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimenLine.setValue('');
            this.pharmFormGroup.controls.frmRegimenLine.enable({ onlySelf: true });
            this.pharmFormGroup.controls.frmRegimen.setValue('');
            this.pharmFormGroup.controls.frmRegimen.enable({ onlySelf: true });
        }

    }



    drugSwitchInterruptionReason() {
        let treatmentplan: number;
        let treatmentText: string;
        let treatments: any[] = [];
        treatmentplan = this.pharmFormGroup.controls.frmTreatmentPlan.value;
        treatments = this.TreatmentplanOptions.filter(x => x.itemId == treatmentplan);
        treatmentText = treatments[0].itemName;


        if (treatmentText === 'Continue current treatment' ||
            treatmentText === 'Select' || treatmentText === 'Start Treatment') {
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.disable({ onlySelf: true });

        }
        else if (treatmentText === 'N/A') {
            this.pharmFormGroup.controls.frmReason.setValue('');
            this.pharmFormGroup.controls.frmReason.disable({ onlySelf: true });

        }
        else {
            this.pharmFormGroup.controls.frmReason.enable({ onlySelf: true });
            treatmentText = treatmentText.replace(/\s/g, '');
            this._lookupItemService.getByGroupName(treatmentText).subscribe((res) => {
                this.SwitchReasonOptions = [];
                this.SwitchReasonOptions = res['lookupItems'];
            });

        }





    }


    getCurrentRegimen() {

        let treatmentplan: number;
        let treatmentText: string;
        let treatments: any[] = [];
        treatmentplan = this.pharmFormGroup.controls.frmTreatmentPlan.value;
        treatments = this.TreatmentplanOptions.filter(x => x.itemId == treatmentplan);
        treatmentText = treatments[0].itemName;
        //  var treatmentPlan = this.pharmFormGroup.controls.frmTreatmentPlan.value;
        if (treatmentText === 'Continue current treatment') {
            this.pharmacyservice.getPharmacyCurrentRegimen(this.patientId).subscribe((res) => {
                console.log(res);
            });
        }
    }
    CalculateQtyPrescribed() {

        const { txtDose, txtMorning, txtEvening, txtMidday, txtNight, frmFreq, txtDuration } = this.pharmFormGroup.value;
        if (this.dosageFrequency === 1) {
            let dose: string;
            let result: number;
            let duration: string;
            let freq: string;
            dose = txtDose;
            duration = txtDuration;
            if (dose == '') {
                dose = '0';
            }
            if (duration == '') {
                duration = '0';
            }
            if (frmFreq == '') {
                freq = '0';
            }
            else {

                freq = frmFreq;
            }

            result = parseInt(dose, 10) * parseInt(duration, 10) * parseInt(freq, 10);

            this.pharmFormGroup.controls.txtQuantityPres.setValue(result);
        } else {

            let duration: string;
            duration = txtDuration;
            if (duration == '') {
                duration = '0';
            }


            let result: number;
            result = ((parseInt(txtMorning, 10) || 0) +
                (parseInt(txtMidday, 10) || 0) + (parseInt(txtEvening, 10) || 0)
                + (parseInt(txtNight, 10) || 0)) * parseInt(duration, 10);

            this.pharmFormGroup.controls.txtQuantityPres.setValue(result);
        }
    }
    changefreq(event) {

        const { txtDose, txtMorning, txtEvening, txtMidday, txtNight, frmFreq, txtDuration,
        } = this.pharmFormGroup.value;
        let val: string;
        val = event.source.value;
        if (event.source.selected === true) {
            let dose: string;
            let result: number;
            let duration: string;
            dose = txtDose;
            duration = txtDuration;
            if (dose == '') {
                dose = '0';
            }
            if (duration == '') {
                duration = '0';
            }

            result = parseInt(dose, 10) * parseInt(duration, 10) * parseInt(val, 10);

            this.pharmFormGroup.controls.txtQuantityPres.setValue(result);
        }
    }

    AddCorrectDrugPrescription() {

        let DrugId: string;
        let DrugAbb: string;
        let batchId: number;
        let batchitem: string;
        let val: string;
        let splitval: any[];
        let DrugItem: any[] = [];
        let Dose: number;
        let Freq: number;

        let FreqText: string;
        let Duration: number;
        let Periodtaken: number;
        let midday: number;
        let morning: number;
        let evening: number;
        let night: number;
        let Periodtakentext: string;
        let QuantityPres: number;
        let reason: number;
        let reasontext: string;
        let regimen: number;
        let regimentext: string;
        let regimenline: number;
        let regimenlinetext: string;
        let treatmentprogram: number;
        let treatmentprogramtext: string;
        let treatmentplan: number;
        let treatmentplantext: string;
        const { frmDrug, frmBatchlist, frmFreq, txtDuration, txtDose, txtQuantityDisp,
            txtQuantityPres, chkProphylaxis, frmPeriodTaken, frmReason, frmRegimen,
            txtMorning, txtMidday, txtNight, txtEvening,
            frmRegimenLine, frmTreatmentPlan, frmTreatmentProgram } = this.pharmFormGroup.value;
        if (frmDrug === '' || frmDrug === undefined || frmDrug === '0') {
            this.snotifyService.error('The drug is required ' +
                'Submit Drug Details',
                this.notificationService.getConfig());
            return;

        }
        DrugItem = this.DrugList.filter(x => x.drugName == frmDrug.drugName);
        if (DrugItem.length > 0) {
            DrugId = DrugItem[0].drug_pk;
            val = DrugItem[0].val;
            splitval = val.split('~');
            DrugAbb = splitval[1];

        } else {
            this.snotifyService.error('Select the drug from the drop downlist ' +
                'Submit Drug Details',
                this.notificationService.getConfig());
            return;
        }
        if (frmBatchlist != '' && frmBatchlist != undefined) {
            if (this.batchlistOptions.length > 0) {
                let batcharray: any[] = [];
                batcharray = this.batchlistOptions.filter(x => x.id == frmBatchlist);
                batchId = batcharray[0].id;
                batchitem = batcharray[0].name;


            }
        } else {
            batchId = 0;
            batchitem = '';

        }
        if (this.dosageFrequency == 1) {
            morning = 0;
            midday = 0;
            evening = 0;
            night = 0;
            if (txtDose === '' || txtDose === '0' || txtDose == undefined) {
                this.snotifyService.error('Dosage is required ' +
                    'Dosage Details',
                    this.notificationService.getConfig());
                return;
            } else {
                Dose = txtDose;


            }

            if (frmFreq === '0' || frmFreq == '' || frmFreq == undefined) {
                this.snotifyService.error('Frequency is required ' +
                    ' Frequency Details',
                    this.notificationService.getConfig());
                return;
            } else {
                Freq = frmFreq;
                let arrfreq: any[] = [];
                arrfreq = this.freqoptions.filter(x => x.id == Freq);
                FreqText = arrfreq[0].name;

            }
        } else {
            Dose = 0;
            Freq = 0;

            if (txtMorning === '' || txtMorning === undefined || txtMorning == null) {
                morning = 0;
            } else {
                morning = txtMorning;
            }
            if (txtMidday === '' || txtMidday === undefined || txtMidday == null) {
                midday = 0;
            } else {
                midday = txtMidday;
            }

            if (txtEvening === '' || txtEvening === undefined || txtEvening == null) {
                evening = 0;
            } else {
                evening = txtEvening;
            }
            if (txtNight === '' || txtNight === undefined || txtNight == null) {
                night = 0;
            } else {
                night = txtNight;
            }



        }

        if (txtDuration === '0' || txtDuration == '' || txtDuration == undefined) {
            this.snotifyService.error('Duration is required ' +
                ' Duration Details',
                this.notificationService.getConfig());
            return;
        } else {
            Duration = txtDuration;
        }
        if (txtQuantityPres === '0' || txtQuantityPres == '' || txtQuantityPres === undefined) {

            this.snotifyService.error('Quantity Prescribed is required ' +
                ' Quantity Prescribed  Details',
                this.notificationService.getConfig());
        } else {
            QuantityPres = txtQuantityPres;
        }
        if (frmPeriodTaken === null || frmPeriodTaken === ''
            || frmPeriodTaken === undefined) {
            Periodtaken = 0;
            Periodtakentext = '';
        } else {
            let periodArray: any[] = [];
            periodArray = this.periodTakenOptions.filter(x => x.itemId == frmPeriodTaken);
            Periodtaken = frmPeriodTaken;
            if (periodArray.length > 0) {
                Periodtakentext = periodArray[0].itemDisplayName;
            }
        }
        if (frmReason == null || frmReason === '' || frmReason === undefined) {
            reason = 0;
            reasontext = '';

        }
        else {
            reason = frmReason;
            let reasonarray: any[] = [];
            reasonarray = this.SwitchReasonOptions.filter(x => x.itemId == reason);
            reasontext = reasonarray[0].itemDisplayName;
        }


        if (frmTreatmentProgram === null || frmTreatmentProgram == ''
            || frmTreatmentProgram === undefined) {
            this.snotifyService.error('TreatmentProgram  is required ' +
                ' TreatmentProgram  Details',
                this.notificationService.getConfig());
            return;

        } else {
            treatmentprogramtext = frmTreatmentProgram;
            let treatmentprogramarray: any[] = [];
            treatmentprogramarray = this.treatmentprogramOptions.filter(x => x.name == treatmentprogramtext);
            if (treatmentprogramarray.length > 0) {
                treatmentprogram = treatmentprogramarray[0].id;
            }
        }
        //   || value == 'HBV' || value == 'Hepatitis B'
        if (treatmentprogramtext !== 'Non-ART') {
            if (frmRegimen === null || frmRegimen === '' || frmRegimen === undefined) {
                this.snotifyService.error('Regimen is required ' +
                    ' Regimen  Details',
                    this.notificationService.getConfig());
                return;
            } else {
                regimen = frmRegimen;
                let regimenArray: any[] = [];
                regimenArray = this.regimenOptions.filter(x => x.lookupItemId == regimen);
                if (regimenArray.length > 0) {
                    regimentext = regimenArray[0].displayName;
                }
            }

            if (frmRegimenLine === null || frmRegimenLine == ''
                || frmRegimenLine === undefined) {
                this.snotifyService.error('Regimenline  is required ' +
                    ' Regimen  Details',
                    this.notificationService.getConfig());
                return;

            } else {
                regimenline = frmRegimenLine;
                let regimenlinearray: any[] = [];
                regimenlinearray = this.regimenlineOptions.filter(x => x.itemId == regimenline);
                if (regimenlinearray.length > 0) {
                    regimenlinetext = regimenlinearray[0].itemDisplayName;
                }
            }

            if (frmTreatmentPlan === null || frmTreatmentPlan == ''
                || frmTreatmentPlan === undefined) {
                this.snotifyService.error('TreatmentPlan  is required ' +
                    ' TreatmentPlan  Details',
                    this.notificationService.getConfig());
                return;

            } else {
                treatmentplan = frmTreatmentPlan;
                let treatmentplanarray: any[] = [];
                treatmentplanarray = this.TreatmentplanOptions.filter(x => x.itemId == treatmentplan);
                if (treatmentplanarray.length > 0) {
                    treatmentplantext = treatmentplanarray[0].itemId;
                }
            }
        } else {
            regimen = 0;
            regimentext = '';
            regimenline = 0
            regimenlinetext = '';
            treatmentplan = 0;
            treatmentplantext = '';


        }

        let Prophylaxis: string;
        if (chkProphylaxis == true) {
            Prophylaxis = '1';
        }
        else {
            Prophylaxis = '0';
        }






        if (this.DrugArray.length > 0) {
            let DrugArray: any[] = [];
            DrugArray = this.DrugArray.filter(x => x.DrugName === frmDrug);
            if (DrugArray.length > 0) {
                this.snotifyService.error('Drug already exists in the list ' +
                    ' Drug   Details',
                    this.notificationService.getConfig());
                return;

            } else if (batchId > 0) {
                let batch: any[] = [];
                batch = this.DrugArray.filter(x => x.BatchId == batchId);
                if (batch.length > 0) {
                    this.snotifyService.error('Batch already exists in the list ' +
                        ' Batch  Details',
                        this.notificationService.getConfig());
                    return;
                }
            }
            else {
                this.DrugArray.push({
                    DrugName: frmDrug.drugName,
                    DrugId: DrugId,
                    DrugAbb: DrugAbb,
                    batchId: batchId,
                    batchText: batchitem,
                    Dose: Dose,
                    Freq: Freq,
                    FreqText: FreqText,
                    Duration: Duration,
                    QuantityPres: QuantityPres,
                    QUantityDisp: txtQuantityDisp,
                    Reason: reason,
                    ReasonText: reasontext,
                    Regimen: regimen,
                    Regimentext: regimentext,
                    Regimenline: regimenline,
                    Regimenlinetext: regimenlinetext,
                    TreatmentPlan: treatmentplan,
                    TreatmentPlantext: treatmentplantext,
                    TreatmentProgram: treatmentprogram,
                    TreatmentProgramText: treatmentprogramtext,
                    Morning: morning,
                    Midday: midday,
                    Evening: evening,
                    Night: night,
                    Period: Periodtaken,
                    PeriodTakenText: Periodtakentext,
                    Prophylaxis: chkProphylaxis,
                    Disabled: false

                });
            }

        }
        else {
            this.DrugArray.push({
                DrugName: frmDrug.drugName,
                DrugId: DrugId,
                DrugAbb: DrugAbb,
                batchId: batchId,
                batchText: batchitem,
                Dose: Dose,
                Freq: Freq,
                FreqText: FreqText,
                Duration: Duration,
                Periodtaken: Periodtaken,
                PeriodTakenText: Periodtakentext,
                QuantityPres: QuantityPres,
                QUantityDisp: txtQuantityDisp,
                Reason: reason,
                ReasonText: reasontext,
                Regimen: regimen,
                Regimentext: regimentext,
                Regimenline: regimenline,
                Regimenlinetext: regimenlinetext,
                TreatmentPlan: treatmentplan,
                TreatmentPlantext: treatmentplantext,
                TreatmentProgram: treatmentprogram,
                TreatmentProgramText: treatmentprogramtext,
                Morning: morning,
                Midday: midday,
                Evening: evening,
                Night: night,
                Prophylaxis: Prophylaxis,
                Disabled: false

            });
        }

        console.log(this.DrugArray);


        this.pharmFormGroup.controls.frmDrug.setValue('');
        this.pharmFormGroup.controls.frmBatchlist.setValue('');
        this.pharmFormGroup.controls.txtDose.setValue('');
        this.pharmFormGroup.controls.txtQuantityDisp.setValue('');
        this.pharmFormGroup.controls.txtQuantityPres.setValue('');
        this.pharmFormGroup.controls.chkProphylaxis.setValue('');
        this.pharmFormGroup.controls.frmFreq.setValue('');
        this.pharmFormGroup.controls.txtDuration.setValue('');
        this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
        this.pharmFormGroup.controls.frmReason.setValue('');
        this.pharmFormGroup.controls.frmRegimen.setValue('');
        this.pharmFormGroup.controls.frmRegimenLine.setValue('');

        this.pharmFormGroup.controls.frmPeriodTaken.setValue('');
        this.pharmFormGroup.controls.frmReason.setValue('');


    }


    change(event) {

        let val: string;
        val = event.option.value.val;
        this.DrugId = '';
        this.DrugAbbr = '';
        const result = val.split('~');
        this.DrugId = ' ';
        this.DrugAbbr = '';
        this.DrugId = result[0];
        this.DrugAbbr = result[1];
        if (this.SCMSamePointDispense === 'PM/SCM With Same point dispense') {
            this.getBatches(this.DrugId);
        }
    }
    getBatches(Drugid: string) {
        this.pharmacyservice.getPharmacyDrugBatches(Drugid).subscribe((res) => {
            this.batchlistOptions = res['drugBatches'];

        });
    }
    getFilteredDrugList(pmscm: number, program: string, filteritem: string) {
        this.pharmacyservice.getPharmacyDrugList(pmscm, program, filteritem).subscribe((res) => {
            console.log('filtereddruglist');
            console.log(res);
            this.filteredDrugList = res['drugList'];
        });
    }
    getDrugList(pmscm: number, program: string, filteritem: string) {
        this.pharmacyservice.getPharmacyDrugList(pmscm, program, filteritem).subscribe((res) => {

            this.DrugList = res['drugList'];
        });


    }

    selectRegimenLine(event) {

        let value: number;
        value = event.source.value;
        if (event.source.selected.selected === true) {
            if (value != undefined) {
                const regimen = this.regimenOptions.filter(x => x.lookupItemId == value);
                if (regimen[0].displayName == 'AF2E(TDF + 3TC + DTG)') {


                    if (this.personVitalWeight > 0) {
                        if (this.personVitalWeight < 35 && this.person.ageNumber > 15) {

                            this.snotifyService.error('This regimen is recommended ' +
                                'for paeds who are  15 years old  or weight of 35 kg and above', 'Submit Regimen Details',
                                this.notificationService.getConfig());
                            this.pharmFormGroup.controls.frmRegimen.setValue('');
                            return false;
                        }
                    }
                    if (this.person.ageNumber < 15 && this.personVitalWeight <= 0) {

                        this.snotifyService.error('Kindly take the weight for the patient vitals since the regimen' +
                            'is recommended for paeds who are 15 years old or weight of 35 kg and above'
                            , 'Submit Regimen Details', this.notificationService.getConfig());
                        this.pharmFormGroup.controls.frmRegimen.setValue('');
                        return false;
                    }




                }
            }

        }
    }
    removeRow(index) {

        let idx: number;
        idx = index;
        this.DrugArray.splice(idx, 1);
    }
    selectRegimens(event) {
        let value: number;

        value = event.source.value;
        console.log(event.source.selected);
        if (event.source.selected.selected == true) {
            if (value != undefined) {

                const regimens = this.regimenlineOptions.filter(x => x.itemId == value);
                this.pharmacyservice.getPharmacyRegimens(regimens[0].itemDisplayName.toString().replace(/\s/g, '')).subscribe((res) => {
                    console.log(res);
                    if (res != null) {
                        this.regimenOptions = res['regimens'];
                    }
                });
            }
        }
    }
    Close() {


        this.zone.run(() => {
            this.router.navigate(['/pharm/mainpage/' + this.patientId + '/' + this.personId
            ], { relativeTo: this.route });
        });

    }
    SavePharmacyData() {
        if (this.DrugArray.length <= 0) {

            this.snotifyService.error('Kindly add Drugs to prescribe'
                , 'Drug Prescription Error', this.notificationService.getConfig());
            return;
        }

        const { visitDate, frmDateDispensed, frmDatePrescibed } = this.pharmFormGroup.value;
        if (frmDateDispensed !== null && frmDateDispensed !== '' && frmDateDispensed !== undefined) {
            this.DispensedBy = JSON.parse(localStorage.getItem('appUserId'));

        } else {
            this.DispensedBy = 0;
        }
        if (frmDatePrescibed !== null && frmDatePrescibed !== '' && frmDatePrescibed !== undefined) {
            this.PrescribedBy = JSON.parse(localStorage.getItem('appUserId'));
        }
        else {
            this.PrescribedBy = 0;
        }
        if (this.EncounterTypeOptions.length > 0) {

            let EncounterArray: LookupItemView[] = [];
            EncounterArray = this.EncounterTypeOptions.filter(x => x.itemName == 'Pharmacy-encounter');
            if (EncounterArray.length > 0) {
                this.EncounterTypeId = EncounterArray[0].itemId;
            }
        }
        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        if (this.patientMasterVisitId > 0) {
            let locationId: number;
            locationId = JSON.parse(localStorage.getItem('appLocationId'));
            this.pharmacyservice.AddSaveUpdatePharmacyRecord(this.person.ptn_pk, this.patientMasterVisitId,
                this.patientId, locationId, this.userId,
                frmDatePrescibed, this.PrescribedBy,
                frmDateDispensed, this.pmscmFlag, this.DrugArray,
                moment(visitDate).toDate(), this.DispensedBy).subscribe((res) => {
                    this.snotifyService.success('Saved the pharmacy record' + res['ptn_Pharmacy_Pk']
                        , 'Pharmacy Form', this.notificationService.getConfig());


                        this.zone.run(() => {
                            this.router.navigate(['/pharm/mainpage/' + this.patientId + '/' + this.personId
                            ], { relativeTo: this.route });
                        });

                }, (error) => {
                    this.snotifyService.error('Error saving and updating pharmacy record' + error
                        , 'Pharmacy Form', this.notificationService.getConfig());
                    this.spinner.hide();
                },
                    () => {
                        this.spinner.hide();
                    });
        } else {




            if (visitDate != null && visitDate !== undefined && visitDate !== "") {
                const patientencounter: PatientMasterVisitEncounter = {
                    PatientId: this.patientId,
                    EncounterType: this.EncounterTypeId,
                    ServiceAreaId: 1,
                    UserId: this.userId,
                    EncounterDate: moment(visitDate).toDate()
                };

                this.spinner.show();
                this.encounterservice.savePatientMasterVisit(patientencounter).subscribe(
                    (result) => {
                        localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                        localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);

                        this.patientMasterVisitId = result['patientMasterVisitId'];
                        if (this.patientMasterVisitId > 0) {
                            let locationId: number;
                            locationId = JSON.parse(localStorage.getItem('appLocationId'));
                            this.pharmacyservice.AddSaveUpdatePharmacyRecord(this.person.ptn_pk, this.patientMasterVisitId,
                                this.patientId, locationId, this.userId,
                                frmDatePrescibed, this.PrescribedBy,
                                frmDateDispensed, this.pmscmFlag, this.DrugArray,
                                moment(visitDate).toDate(), this.DispensedBy).subscribe((res) => {
                                    this.snotifyService.success('Saved the pharmacy record' + res['ptn_Pharmacy_Pk']
                                        , 'Pharmacy Form', this.notificationService.getConfig());


                                        this.zone.run(() => {
                                            this.router.navigate(['/pharm/mainpage/' + this.patientId + '/' + this.personId
                                            ], { relativeTo: this.route });
                                        });

                                }, (error) => {
                                    this.snotifyService.error('Error saving and updating pharmacy record' + error
                                        , 'Pharmacy Form', this.notificationService.getConfig());
                                    this.spinner.hide();
                                },
                                    () => {
                                        this.spinner.hide();
                                    });
                        }
                    },
                    (error) => {
                        this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );
            }

            else {

                this.snotifyService.error('VisitDate is required'
                    , 'VisitDate', this.notificationService.getConfig());
                this.spinner.hide();
                return;
            }

        }




    }
}


import { Component, EventEmitter, OnInit, Output, Input, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { select } from '@ngrx/store';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoadedRouterConfig } from '@angular/router/src/config';
import { registerLocaleData } from '@angular/common';
import { PrepService } from '../_services/prep.service';
import * as moment from 'moment';

import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { EncounterService } from '../../shared/_services/encounter.service';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';

@Component({
    selector: 'app-prep-riskassessment',
    templateUrl: './prep-riskassessment.component.html',
    styleUrls: ['./prep-riskassessment.component.css'],
    providers: [
        EncounterService
    ]

})
export class PrepRiskassessmentComponent implements OnInit {
    public PrepRiskAssessmentFormGroup: FormGroup;
    patientmastervisitid: number;
    assessmentOutComeOptions: LookupItemView[] = [];
    clientsBehaviourRiskOptions: LookupItemView[] = [];
    sexualPartnerHivStatusOptions: LookupItemView[] = [];
    clientWillingTakePrepOptions: LookupItemView[] = [];
    riskEducationOptions: LookupItemView[] = [];
    behaviourRiskAssessmentOptions: LookupItemView[] = [];
    ReferralPreventionOptions: LookupItemView[] = [];
    RiskReductionEducationOptions: LookupItemView[] = [];
    EncounterTypeOptions: LookupItemView[] = [];
    ExistingClinicalNotes: any[] = [];
    ExistingRiskAssessmentDetails: any[] = [];
    ExistingSexualPartnerList: any[] = [];
    ExistingClientBehaviourRiskList: any[] = [];
    maxDate: Date;
    personId: number;
    serviceAreaId: number;
    patientId: number;
    RiskAssessmentList: any[] = [];
    ClinicalList: any[] = [];
    RiskViewOptions: any[] = [];
    partnerstatusOptions: any[] = [];
    // selectedRiskReductionEducation: number;
    // selectedReferralPreventionOptions: number;
    // selectedClientWillingTakingPrep: number;
    // selectedRiskEducationOption: number;
    EncounterTypeId: number;
    UserId: number;
    PatientMasterVisitId: number;
    PartnerHIVStatus: number;
    Encounters: any[] = [];
    partnercccenrollmentoptions: LookupItemView[] = [];
    partnerartstartdateoptions: number;
    hivserodiscordantoptions: LookupItemView[] = [];
    sexwithoutcondomoptions: LookupItemView[] = [];
    partnerchildrenoptions: LookupItemView[] = [];
    patientIdentifieroptions: LookupItemView[] = [];
    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private prepservice: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private encounterservice: EncounterService
    ) {
        this.maxDate = new Date();

        // this.PrepRiskAssessmentFormGroup = new FormControl();
    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { patientId, personId, serviceId, patientMasterVisitId } = params;
            this.personId = personId;
            this.serviceAreaId = serviceId;
            this.patientId = patientId;
            this.PatientMasterVisitId = patientMasterVisitId;
        });
        this.route.params.subscribe(params => {

            this.personId = params['personId'];


        });
        this.route.data.subscribe((res) => {
            const { assessmentOutComeArray, clientsBehaviourRiskArray,
                sexualPartnerHivStatusArray,
                clientWillingTakePrepArray,
                riskEducationArray,
                behaviourRiskAssessmentArray,
                ReferralPreventionArray,
                RiskReductionEducationArray,
                EncounterTypeArray,
                PartnerCCCEnrollmentArray,
                PatientIdentifierArray,
                ARTStartDateArray,
                PartnerHIVStatusArray,
                DurationArray,
                SexWithoutCondomArray,
                HivPartnerArray



            } = res;

            this.assessmentOutComeOptions = assessmentOutComeArray['lookupItems'];
            this.clientsBehaviourRiskOptions = clientsBehaviourRiskArray['lookupItems'];
            this.sexualPartnerHivStatusOptions = sexualPartnerHivStatusArray['lookupItems'];
            this.clientWillingTakePrepOptions = clientWillingTakePrepArray['lookupItems'];
            this.riskEducationOptions = riskEducationArray['lookupItems'];
            this.behaviourRiskAssessmentOptions = behaviourRiskAssessmentArray['lookupItems'];
            this.ReferralPreventionOptions = ReferralPreventionArray['lookupItems'];
            this.RiskReductionEducationOptions = RiskReductionEducationArray['lookupItems'];
            this.EncounterTypeOptions = EncounterTypeArray['lookupItems'];
            this.patientIdentifieroptions = PatientIdentifierArray['lookupItems'];
            this.partnerartstartdateoptions = ARTStartDateArray['lookupItems'];
            this.hivserodiscordantoptions = DurationArray['lookupItems'];
            this.partnerchildrenoptions = HivPartnerArray['lookupItems'];
            this.sexwithoutcondomoptions = SexWithoutCondomArray['lookupItems'];
            this.partnerstatusOptions = PartnerHIVStatusArray['lookupItems'];
            this.partnercccenrollmentoptions = PartnerCCCEnrollmentArray['lookupItems'];




            this.PartnerHIVStatus = this.partnerstatusOptions[0]['itemId'];


        });




        this.PrepRiskAssessmentFormGroup = this._formBuilder.group({
            sexualPartnerHivStatus: new FormControl('', [Validators.required]),
            visitDate: new FormControl('', [Validators.required]),
            clientsBehaviourRisks: new FormControl('', [Validators.required]),
            assessmentOutCome: new FormControl('', [Validators.required]),
            SpecifyRiskReductionEducation: new FormControl(''),
            ReferralPreventions: new FormControl('', [Validators.required]),
            RiskReductionEducation: new FormControl('', [Validators.required]),
            SpecifyPreventionReferalServices: new FormControl(''),
            partnerHIVStatusDate: new FormControl(''),
            ClientWillingTakePrep: new FormControl('', [Validators.required]),
            RiskEducation: new FormControl(''),
            SpecifyRiskEducation: new FormControl(''),
            ClinicalNotes: new FormControl(''),
            partnercccenrollment: new FormControl(''),
            partnerARTStartDate: new FormControl(''),
            Months: new FormControl(''),
            partnersexcondoms: new FormControl(''),
            hivpartnerchildren: new FormControl(''),
            CCCNumber: new FormControl('', Validators.pattern(/^((?!(0))[0-9]{10})$/))
        });
        this.RiskViewOptions = this.clientsBehaviourRiskOptions.concat(this.sexualPartnerHivStatusOptions);
        this.RiskViewOptions.forEach(function (e) {
            if (typeof e === 'object') {
                e['checked'] = false;
            }
        });
        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.CCCNumber.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.Months.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });

        if (this.PatientMasterVisitId > 0) {
            this.LoadDetails();
        }
        let assessmentoutcomerisk: any[] = [];
        let NoRiskOutcome: number;
        assessmentoutcomerisk = this.assessmentOutComeOptions.filter(x => x.itemDisplayName == 'No Risk');
        NoRiskOutcome = assessmentoutcomerisk[0]['itemId'];
        this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.setValue(NoRiskOutcome);
        let prepriskencounter: any[] = [];
        prepriskencounter = this.EncounterTypeOptions.filter(x => x.itemDisplayName == 'PrepRiskAssessment-encounter');
        this.EncounterTypeId = prepriskencounter[0]['itemId'];

    }

    LoadDetails() {
        this.prepservice.GetAssessmentDetails(this.patientId, this.PatientMasterVisitId).subscribe((result) => {

            this.ExistingClinicalNotes = [];
            this.ExistingRiskAssessmentDetails = [];
            this.ExistingSexualPartnerList = [];
            this.ExistingClientBehaviourRiskList = [];

            this.ExistingClinicalNotes = result['clinicalNotes'];
            this.ExistingRiskAssessmentDetails = result['riskAssessmentDetails'];
            this.RiskAssessmentList = [];
            this.ClinicalList = [];
            let sexualpartnermasterid: number;
            let clientbehaviourmasterid: number;
            sexualpartnermasterid = this.sexualPartnerHivStatusOptions[0].masterId;
            clientbehaviourmasterid = this.clientsBehaviourRiskOptions[0].masterId;
            this.RiskAssessmentList = this.ExistingRiskAssessmentDetails.map(o => {
                return {
                    'Id': o.id,
                    'Comment': o.comment,
                    'RiskAssessmentid': o.riskAssessmentid,
                    'Value': o.value,
                    'DeleteFlag': o.deleteFlag,
                    'Date': o.date
                };
            });


            this.ClinicalList = this.ExistingClinicalNotes.map(o => {
                return {
                    'Id': o.id,
                    'Comment': o.comment,
                    'ServiceAreaId': this.serviceAreaId,
                    'DeleteFlag': o.deleteFlag
                };
            });

            this.ExistingSexualPartnerList = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == sexualpartnermasterid)
                .map(o => {
                    return o.value;
                });
            this.ExistingClientBehaviourRiskList = this.ExistingRiskAssessmentDetails.
                filter(x => x.riskAssessmentid == clientbehaviourmasterid)
                .map(o => {
                    return o.value;
                });

            let assessmentoutcomemasterid: number;
            assessmentoutcomemasterid = this.assessmentOutComeOptions[0].masterId;
            let assessmentoutcomedetail: any[] = [];
            assessmentoutcomedetail = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == assessmentoutcomemasterid).map(o => {
                    return o.value;
                });
            let riskreductionmasterid: number;
            riskreductionmasterid = this.RiskReductionEducationOptions[0].masterId;
            let riskreductiondetailvalue: any[] = [];
            let riskreductiondetailspecify: any[] = [];
            riskreductiondetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskreductionmasterid).map(o => {
                    return o.value;
                });
            riskreductiondetailspecify = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskreductionmasterid).map(o => {
                    return o.comment;
                });


            let referralpreventionmasterid: number;
            referralpreventionmasterid = this.ReferralPreventionOptions[0].masterId;
            let referralpreventiondetailvalue: any[] = [];
            let referralpreventiondetailspecify: any[] = [];
            referralpreventiondetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == referralpreventionmasterid).map(o => {
                    return o.value;
                });
            referralpreventiondetailspecify = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == referralpreventionmasterid).map(o => {
                    return o.comment;
                });


            let clientwillingprepmasterid: number;
            clientwillingprepmasterid = this.clientWillingTakePrepOptions[0].masterId;
            let clientwillingprepdetail: any[] = [];
            clientwillingprepdetail = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == clientwillingprepmasterid).map(o => {
                    return o.value;
                });

            let riskeducationmasterid: number;
            riskeducationmasterid = this.riskEducationOptions[0].masterId;
            let riskeducationdetailvalue: any[] = [];
            let riskeducationdetailspecify: any[] = [];
            riskeducationdetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskeducationmasterid).map(o => {
                    return o.value;
                });
            riskeducationdetailspecify = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskeducationmasterid).map(o => {
                    return o.comment;
                });

            let clinicalnotesvalue: any[] = [];
            clinicalnotesvalue = this.ExistingClinicalNotes.map(o => {
                return o.comment;
            });

            let partnerhivstatusvalue: any[] = [];
            let partnerhivstatusmasterid: number;
            partnerhivstatusmasterid = this.partnerstatusOptions[0].masterId;
            partnerhivstatusvalue = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnerhivstatusmasterid)
                .map(o => {
                    return o.date;
                });

            let partnerartvalue: any[] = [];
            let partnerartmasterid: number;
            partnerartmasterid = this.partnerartstartdateoptions[0].masterId;
            partnerartvalue = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnerartmasterid)
                .map(o => {
                    return o.date;
                });

            let partnercccenrollmentvalue: any[] = [];
            let partnercccenrollmentmasterid: number;
            partnercccenrollmentmasterid = this.partnercccenrollmentoptions[0].masterId;
            partnercccenrollmentvalue = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnercccenrollmentmasterid)
                .map(o => {
                    return o.value;
                });

            let partnercccvalue: any[] = [];
            let partnercccmasterid: number;
            partnercccmasterid = this.patientIdentifieroptions[0].masterId;
            partnercccvalue = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnercccmasterid)
                .map(o => {
                    return o.comment;
                });

            let partnermonth: any[] = [];
            let partnermonthmasterid: number;
            partnermonthmasterid = this.hivserodiscordantoptions[0].masterId;
            partnermonth = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnermonthmasterid)
                .map(o => {
                    return o.comment;
                });
            let partnersexcondom: any[] = [];
            let partnersexmasterid: number;
            partnersexmasterid = this.sexwithoutcondomoptions[0].masterId;
            partnersexcondom = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == partnersexmasterid)
                .map(o => {
                    return o.value;
                });

            let children: any[] = [];
            let childrenmasterid: number;
            childrenmasterid = this.partnerchildrenoptions[0].masterId;
            children = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == childrenmasterid)
                .map(o => {
                    return o.comment;
                });


            this.prepservice.getPatientMasterVisits(this.patientId, this.PatientMasterVisitId).subscribe((res) => {
                this.Encounters = res;
                if (this.Encounters != null && this.Encounters != undefined && this.Encounters.length > 0) {

                    let visitDate: Date;
                    visitDate = this.Encounters[0]['visitDate'];
                    this.PrepRiskAssessmentFormGroup.controls.visitDate.setValue(visitDate);
                }
            });



            this.PrepRiskAssessmentFormGroup.controls.sexualPartnerHivStatus.setValue(this.ExistingSexualPartnerList);
            this.PrepRiskAssessmentFormGroup.controls.clientsBehaviourRisks.setValue(this.ExistingClientBehaviourRiskList);
            this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.setValue(assessmentoutcomedetail[0]);
            this.PrepRiskAssessmentFormGroup.controls.RiskReductionEducation.setValue(riskreductiondetailvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.setValue(riskreductiondetailspecify[0]);
            this.PrepRiskAssessmentFormGroup.controls.ReferralPreventions.setValue(referralpreventiondetailvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue(referralpreventiondetailspecify[0]);
            this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.setValue(clientwillingprepdetail[0]);
            this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue(riskeducationdetailvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue(riskeducationdetailspecify[0]);
            this.PrepRiskAssessmentFormGroup.controls.ClinicalNotes.setValue(clinicalnotesvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.partnerHIVStatusDate.setValue(partnerhivstatusvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.partnercccenrollment.setValue(partnercccenrollmentvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.setValue(partnerartvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.CCCNumber.setValue(partnercccvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.Months.setValue(partnermonth[0]);
            this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.setValue(partnersexcondom[0]);
            this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.setValue(children[0]);




        },
            (error) => {
                this.snotifyService.error('Error loading existing details' + error, 'RiskAssessment Form Details',
                    this.notificationService.getConfig());
            });
    }
    OnReferralPreventionOffered(event) {
        let val: number;
        let index: number;
        let text: string;


        // val = this.selectedReferralPreventionOptions;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.ReferralPreventionOptions.findIndex(x => x.itemId == val);
                text = this.ReferralPreventionOptions[index].itemDisplayName;
                if (text.toLowerCase() === 'yes') {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.enable({ onlySelf: true });
                }
                if (text.toLowerCase() === 'no') {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });
                }
            }
        }

    }
    OnClientWillingTakingPrep(event) {
        let val: number;
        let index: number;
        let text: string;

        //  val = this.selectedClientWillingTakingPrep;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.clientWillingTakePrepOptions.findIndex(x => x.itemId == val);
                text = this.clientWillingTakePrepOptions[index].itemDisplayName;
                if (text.toLowerCase() === 'no') {
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.enable({ onlySelf: true });
                }
                if (text.toLowerCase() === 'yes') {
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });

                }
            }
        }
    }
    OnRiskEducationOffered(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;

        // val = this.selectedRiskEducationOption;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.riskEducationOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.riskEducationOptions[index].itemDisplayName;

                    if (text.toLowerCase() === 'yes') {
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.enable({ onlySelf: true });
                    }
                    if (text.toLowerCase() === 'no') {
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });

                    }
                }
            }
        }
    }


    OnRiskReductionEducationOffered(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        // val = this.selectedRiskReductionEducation;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.riskEducationOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.riskEducationOptions[index].itemDisplayName;
                    if (text.toLowerCase() === 'yes') {
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.enable({ onlySelf: true });
                    }
                    if (text.toLowerCase() === 'no') {
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.setValue('');
                        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.disable({ onlySelf: true });
                    }
                }
            }
        }

    }
    CheckRiskOptions() {
        let notselected: boolean = true;
        let length: number;
        length = this.RiskViewOptions.length;
        let count: number = 0;
        for (let i = 0; i < this.RiskViewOptions.length; i++) {

            if ((this.RiskViewOptions[i].checked == false) ||
                (this.RiskViewOptions[i].itemName === 'N/A'
                    && this.RiskViewOptions[i].checked == true)) {
                count = count + 1;
            }
            if (count == length) {
                notselected = false;
            }
            if (notselected == false) {
                let assessmentOutCome: any[] = [];
                let itemId: number;
                assessmentOutCome = this.assessmentOutComeOptions.filter(x => x.itemDisplayName == 'No Risk');
                itemId = assessmentOutCome[0]['itemId'];
                this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.setValue(itemId);
            }
        }


        for (let i = 0; i < this.RiskViewOptions.length; i++) {
            let selected: boolean = false;

            if (this.RiskViewOptions[i].checked == true &&
                this.RiskViewOptions[i].itemName !== 'N/A') {
                selected = true;
            }

            if (selected == true) {

                let assessmentOutCome: any[] = [];
                let itemId: number;
                assessmentOutCome = this.assessmentOutComeOptions.filter(x => x.itemDisplayName == 'Risk');
                itemId = assessmentOutCome[0]['itemId'];
                this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.setValue(itemId);

            }
        }



    }
    OnPartnerenrollmentSelection(event) {

        let selectedvalue: string;
        selectedvalue = event.source.viewValue;
        selectedvalue = selectedvalue.toString().toLowerCase();
        if (event.source.selected == true) {
            if (selectedvalue === 'yes') {
                this.PrepRiskAssessmentFormGroup.controls.CCCNumber.enable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.enable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.enable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.Months.enable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.enable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.enable({ onlySelf: true });


            } else if (selectedvalue === 'no') {
                this.PrepRiskAssessmentFormGroup.controls.CCCNumber.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.CCCNumber.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.Months.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.Months.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.setValue('');

            } else if (selectedvalue === 'unknown') {
                this.PrepRiskAssessmentFormGroup.controls.CCCNumber.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.CCCNumber.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.Months.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.Months.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.setValue('');

            }
        }

    }

    OnClientAssessmentSelection(event) {


        const value = event.source.value;

        const objIndex = this.RiskViewOptions.findIndex((obj => obj.itemId == value));
        this.RiskViewOptions[objIndex].checked = event.source.selected;


        if (event.source.viewValue !== 'Not Applicable' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    === 'Not Applicable') {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }

        if (event.source.viewValue === 'Not Applicable' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    !== event.source.viewValue) {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }

        this.CheckRiskOptions();


    }
    OnSexualPartnerSelection(event) {

        const value = event.source.value;

        const objIndex = this.RiskViewOptions.findIndex((obj => obj.itemId == value));
        this.RiskViewOptions[objIndex].checked = event.source.selected;


        if (event.source.viewValue !== 'Not Applicable' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    === 'Not Applicable') {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }
        if (event.source.viewValue === 'Not Applicable' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    !== event.source.viewValue) {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }

        if (event.source.viewValue === 'Not on ART' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {

                if ((event.source._parent.options._results[i].viewValue !== 'Couple is trying to conceive') &&
                    (event.source._parent.options._results[i].viewValue !== event.source.viewValue)) {

                    event.source._parent.options._results[i].deselect();
                    event.source._parent.options._results[i].disabled = true;
                }
                // console.log(event.source._parent.options._results[i].value);
            }
        } else if (event.source.viewValue === 'Not on ART' && event.source.selected == false) {
            for (let i = 0; i < event.source._parent.options.length; i++) {

                if ((event.source._parent.options._results[i].viewValue !== 'Couple is trying to conceive') &&
                    (event.source._parent.options._results[i].viewValue !== event.source.viewValue)) {


                    event.source._parent.options._results[i].disabled = false;
                }
                // console.log(event.source._parent.options._results[i].value);
            }
        }

        this.CheckRiskOptions();
    }

    submit() {

        if (this.PrepRiskAssessmentFormGroup.valid) {
            if (this.PatientMasterVisitId > 0) {

                this.SaveEdit();
            } else {
                this.Save();
            }

        }

    }
    public SaveEdit() {

        this.spinner.show();
        let date: string;
        date = this.PrepRiskAssessmentFormGroup.controls.visitDate.value;
        this.UserId = JSON.parse(localStorage.getItem('appUserId'));
        let partnerhivstatus: any[] = [];
        let clientassessmentstatus: any[] = [];
        let assessmentoutcomestatus: number;
        let riskreductioneducationstatus: number;
        let referralpreventions: number;
        let ClientWillingTakePrepstatus: number;
        let riskeducationstatus: number;

        let SpecifyRiskEducationValue: string;
        let SpecifyPreventionReferalServicesValue: string;
        let SpecifyRiskReductionEducationValue: string;
        let clinicalnotesvalue: string;
        let partnerhivstatusdatedetail: string;
        let partnercccenrollmentdetail: number;
        let CCCnumber: string;
        let Monthdetail: number;
        let artstartdatepartner: string;
        let partnersexwithoutcondoms: number;
        let hivpartnerchildrendetail: number;

        SpecifyRiskReductionEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.value;
        SpecifyPreventionReferalServicesValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.value;
        SpecifyRiskEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.value;
        clinicalnotesvalue = this.PrepRiskAssessmentFormGroup.controls.ClinicalNotes.value;

        partnerhivstatus = this.PrepRiskAssessmentFormGroup.controls.sexualPartnerHivStatus.value;
        clientassessmentstatus = this.PrepRiskAssessmentFormGroup.controls.clientsBehaviourRisks.value;
        assessmentoutcomestatus = this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.value;
        riskreductioneducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskReductionEducation.value;
        referralpreventions = this.PrepRiskAssessmentFormGroup.controls.ReferralPreventions.value;
        ClientWillingTakePrepstatus = this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.value;
        riskeducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskEducation.value;
        partnerhivstatusdatedetail = this.PrepRiskAssessmentFormGroup.controls.partnerHIVStatusDate.value;
        partnercccenrollmentdetail = this.PrepRiskAssessmentFormGroup.controls.partnercccenrollment.value;
        CCCnumber = this.PrepRiskAssessmentFormGroup.controls.CCCNumber.value;
        artstartdatepartner = this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.value;
        partnersexwithoutcondoms = this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.value;
        hivpartnerchildrendetail = this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.value;
        Monthdetail = this.PrepRiskAssessmentFormGroup.controls.Months.value;



        let sexualpartnermasterid: number;
        let sexuallist: any[] = [];
        sexualpartnermasterid = this.sexualPartnerHivStatusOptions[0].masterId;
        sexuallist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == sexualpartnermasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == sexualpartnermasterid)
            .forEach(x => {
                let status: any[] = [];

                status = partnerhivstatus.filter(t => t == x.Value);

                if (x.Value == status) {
                    x.DeleteFlag = false;
                } else {
                    x.DeleteFlag = true;
                }


            });
        let notexists: any[] = [];
        notexists = partnerhivstatus.filter(r => {
            if (sexuallist.findIndex(t => t.Value == r) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': sexualpartnermasterid,
                    'Value': r,
                    'DeleteFlag': false,
                    'Date': ''
                });
                return r;

            }
        });

        let clientbehaviourmasterid: number;
        let clientbehaviourlist: any[] = [];
        clientbehaviourmasterid = this.clientsBehaviourRiskOptions[0].masterId;
        clientbehaviourlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == clientbehaviourmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == clientbehaviourmasterid)
            .forEach(x => {
                let status: any[] = [];

                status = clientassessmentstatus.filter(t => t == x.Value);

                if (x.Value == status) {
                    x.DeleteFlag = false;
                } else {
                    x.DeleteFlag = true;
                }


            });
        let clientnotexists: any[] = [];
        clientnotexists = clientassessmentstatus.filter(r => {
            if (clientbehaviourlist.findIndex(t => t.Value == r) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': clientbehaviourmasterid,
                    'Value': r,
                    'DeleteFlag': false,
                    'Date': ''
                });
                return r;

            }
        });

        let assessmentoutcomemasterid: number;
        let assessmentoutcomelist: any[] = [];
        assessmentoutcomemasterid = this.assessmentOutComeOptions[0].masterId;
        assessmentoutcomelist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == assessmentoutcomemasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == assessmentoutcomemasterid)
            .forEach(x => {
                if (x.Value == assessmentoutcomestatus) {
                    x.DeleteFlag = false;
                } else {
                    x.DeleteFlag = true;
                }
            });
        if (clientbehaviourlist.findIndex(t => t.Value == assessmentoutcomestatus) == -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': assessmentoutcomemasterid,
                'Value': assessmentoutcomestatus,
                'DeleteFlag': false,
                'Date': ''
            });

        }

        let riskreductioneducationmasterid: number;
        let riskreductioneducationlist: any[] = [];
        riskreductioneducationmasterid = this.RiskReductionEducationOptions[0].masterId;
        riskreductioneducationlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskreductioneducationmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskreductioneducationmasterid)
            .forEach(x => {
                if (x.Value == riskreductioneducationstatus) {
                    x.DeleteFlag = false;
                    x.Comment = SpecifyRiskReductionEducationValue;
                } else {
                    x.DeleteFlag = true;
                }
            });
        if (riskreductioneducationlist.findIndex(t => t.Value == riskreductioneducationstatus) == -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': SpecifyRiskReductionEducationValue,
                'RiskAssessmentid': riskreductioneducationmasterid,
                'Value': riskreductioneducationstatus,
                'DeleteFlag': false,
                'Date': ''
            });

        }

        let referralpreventionmasterid: number;
        let referralpreventionlist: any[] = [];
        referralpreventionmasterid = this.ReferralPreventionOptions[0].masterId;
        referralpreventionlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == referralpreventionmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == referralpreventionmasterid)
            .forEach(x => {
                if (x.Value == referralpreventions) {
                    x.DeleteFlag = false;
                    x.Comment = SpecifyPreventionReferalServicesValue;
                } else {
                    x.DeleteFlag = true;
                }
            });
        if (referralpreventionlist.findIndex(t => t.Value == referralpreventions) == -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': SpecifyPreventionReferalServicesValue,
                'RiskAssessmentid': referralpreventionmasterid,
                'Value': referralpreventions,
                'DeleteFlag': false,
                'Date': ''
            });

        }


        let clientwillingprepmasterid: number;
        let clientwillingpreplist: any[] = [];
        clientwillingprepmasterid = this.clientWillingTakePrepOptions[0].masterId;
        clientwillingpreplist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == clientwillingprepmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == clientwillingprepmasterid)
            .forEach(x => {
                if (x.Value == ClientWillingTakePrepstatus) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });
        if (clientwillingpreplist.findIndex(t => t.Value == ClientWillingTakePrepstatus) == -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': clientwillingprepmasterid,
                'Value': ClientWillingTakePrepstatus,
                'DeleteFlag': false,
                'Date': ''
            });

        }

        let riskeducationmasterid: number;
        let riskeducationlist: any[] = [];
        riskeducationmasterid = this.riskEducationOptions[0].masterId;
        riskeducationlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskeducationmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskeducationmasterid)
            .forEach(x => {
                if (x.Value == riskeducationstatus) {
                    x.DeleteFlag = false;
                    x.Comment = SpecifyRiskEducationValue;
                } else {
                    x.DeleteFlag = true;
                }
            });
        if (riskreductioneducationlist.findIndex(t => t.Value == riskeducationstatus) == -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': SpecifyRiskEducationValue,
                'RiskAssessmentid': riskeducationmasterid,
                'Value': riskeducationstatus,
                'DeleteFlag': false,
                'Date': ''
            });

        }

        if (clinicalnotesvalue !== null && clinicalnotesvalue !== undefined) {
            if (clinicalnotesvalue.length < 1) {
                if (this.ClinicalList.length > 0) {
                    this.ClinicalList.forEach(x => {
                        x.DeleteFlag = true;
                    });

                }

            } else {
                if (this.ClinicalList.length > 0) {
                    this.ClinicalList.forEach(x => {
                        x.Comment = clinicalnotesvalue;
                        x.DeleteFlag = false;
                    });

                } else {
                    this.ClinicalList.push({
                        'Id': 0,
                        'Comment': clinicalnotesvalue,
                        'ServiceAreaId': this.serviceAreaId,
                        'DeleteFlag': false


                    });

                }
            }

        } else {
            this.ClinicalList.push({
                'Id': 0,
                'Comment': '',
                'ServiceAreaId': this.serviceAreaId,
                'DeleteFlag': false


            });
        }

        if (partnerhivstatusdatedetail !== null && partnerhivstatusdatedetail !== undefined) {
            let partnerhivstatusmasterid: number;
            let partnerhivstatusvalueid: number;
            let partnerhivstatuslist: any[] = [];
            partnerhivstatusmasterid = this.partnerstatusOptions[0].masterId;
            partnerhivstatusvalueid = this.partnerstatusOptions[0].itemId;
            partnerhivstatuslist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnerhivstatusmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnerhivstatusmasterid)
                .forEach(x => {
                    if (x.Value == partnerhivstatusvalueid) {
                        if (partnerhivstatusdatedetail.length < 1) {
                            x.DeleteFlag = true;

                        } else {
                            x.DeleteFlag = false;
                            x.Date = partnerhivstatusdatedetail;
                        }
                    }
                });
            if (partnerhivstatuslist.findIndex(t => t.Value == partnerhivstatusvalueid) == -1) {
                if (partnerhivstatusdatedetail !== '') {
                    this.RiskAssessmentList.push({
                        'Id': 0,
                        'Comment': '',
                        'RiskAssessmentid': partnerhivstatusmasterid,
                        'Value': partnerhivstatusvalueid,
                        'DeleteFlag': false,
                        'Date': partnerhivstatusdatedetail
                    });
                }

            }

        }

        if (artstartdatepartner !== null && artstartdatepartner !== undefined) {

            let partnerartstartdatemasterid: number;
            let partnerartstartdatevalueid: number;
            let partnerartstartdatelist: any[] = [];
            partnerartstartdatemasterid = this.partnerartstartdateoptions[0].masterId;
            partnerartstartdatevalueid = this.partnerartstartdateoptions[0].itemId;
            partnerartstartdatelist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnerartstartdatemasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnerartstartdatemasterid)
                .forEach(x => {
                    if (x.Value == partnerartstartdatevalueid) {
                        if (artstartdatepartner.length < 1) {
                            x.DeleteFlag = true;


                        } else {
                            x.DeleteFlag = false;
                            x.Date = artstartdatepartner;
                        }

                    }
                });
            if (partnerartstartdatelist.findIndex(t => t.Value == partnerartstartdatevalueid) == -1) {
                if (artstartdatepartner != '') {
                    this.RiskAssessmentList.push({
                        'Id': 0,
                        'Comment': '',
                        'RiskAssessmentid': partnerartstartdatemasterid,
                        'Value': partnerartstartdatevalueid,
                        'DeleteFlag': false,
                        'Date': artstartdatepartner
                    });
                }

            }
        }


        if (partnercccenrollmentdetail != null && partnercccenrollmentdetail !== undefined
        ) {

            let partnercccenrollmentmasterid: number;
            let partnercccenrollmentlist: any[] = [];
            partnercccenrollmentmasterid = this.partnercccenrollmentoptions[0].masterId;

            partnercccenrollmentlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnercccenrollmentmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnercccenrollmentmasterid)
                .forEach(x => {
                    if (x.Value == partnercccenrollmentdetail) {
                        x.DeleteFlag = false;

                    } else {
                        x.DeleteFlag = true;
                    }
                });
            if (partnercccenrollmentlist.findIndex(t => t.Value == partnercccenrollmentdetail) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': partnercccenrollmentmasterid,
                    'Value': partnercccenrollmentdetail,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }


        if (partnersexwithoutcondoms != null && partnersexwithoutcondoms !== undefined
        ) {
            let partnersexcondomsmasterid: number;
            let partnersexcondomslist: any[] = [];
            partnersexcondomsmasterid = this.sexwithoutcondomoptions[0].masterId;

            partnersexcondomslist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnersexcondomsmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == partnersexcondomsmasterid)
                .forEach(x => {
                    if (x.Value == partnersexwithoutcondoms) {
                        x.DeleteFlag = false;

                    } else {
                        x.DeleteFlag = true;
                    }
                });
            if (partnersexcondomslist.findIndex(t => t.Value == partnersexwithoutcondoms) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': partnersexcondomsmasterid,
                    'Value': partnersexwithoutcondoms,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }


        if (CCCnumber != null && CCCnumber !== undefined) {
            let cccnumbermasterid: number;
            let cccnumbervalueid: number;
            let cccnumberlist: any[] = [];
            let findindex: number;
            findindex = this.patientIdentifieroptions.findIndex(x => x.itemName == 'CCCNumber');
            cccnumbermasterid = this.patientIdentifieroptions[findindex].masterId;
            cccnumbervalueid = this.patientIdentifieroptions[findindex].itemId;
            cccnumberlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == cccnumbermasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == cccnumbermasterid)
                .forEach(x => {
                    if (x.Value == cccnumbervalueid) {
                        if (CCCnumber.length < 1) {
                            x.DeleteFlag = true;
                        } else {
                            x.DeleteFlag = false;
                            x.Comment = CCCnumber;
                        }

                    }
                });
            if (cccnumberlist.findIndex(t => t.Value == cccnumbervalueid) == -1) {
                if (CCCnumber.length > 0) {
                    this.RiskAssessmentList.push({
                        'Id': 0,
                        'Comment': CCCnumber.toString(),
                        'RiskAssessmentid': cccnumbermasterid,
                        'Value': cccnumbervalueid,
                        'DeleteFlag': false,
                        'Date': ''
                    });
                }

            }
        }


        if (typeof Monthdetail != 'undefined' && Monthdetail !== undefined && Monthdetail) {
            let hivsexdiscordantmasterid: number;
            let hivsexdiscordantvalueid: number;
            let hivsexdiscordantlist: any[] = [];

            hivsexdiscordantmasterid = this.hivserodiscordantoptions[0].masterId;
            hivsexdiscordantvalueid = this.hivserodiscordantoptions[0].itemId;
            hivsexdiscordantlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == hivsexdiscordantmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == hivsexdiscordantmasterid)
                .forEach(x => {
                    if (x.Value == hivsexdiscordantvalueid) {
                        x.DeleteFlag = false;
                        x.Comment = Monthdetail.toString();

                    } else {
                        x.DeleteFlag = true;
                    }
                });
            if (hivsexdiscordantlist.findIndex(t => t.Value == hivsexdiscordantvalueid) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': Monthdetail.toString(),
                    'RiskAssessmentid': hivsexdiscordantmasterid,
                    'Value': hivsexdiscordantvalueid,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }


        if (typeof hivpartnerchildrendetail != 'undefined' && hivpartnerchildrendetail) {
            let hivpartnerchildrenmasterid: number;
            let hipartnerchildrenvalueid: number;
            let hivpartnechildrenlist: any[] = [];

            hivpartnerchildrenmasterid = this.partnerchildrenoptions[0].masterId;
            hipartnerchildrenvalueid = this.partnerchildrenoptions[0].itemId;
            hivpartnechildrenlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == hivpartnerchildrenmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == hivpartnerchildrenmasterid)
                .forEach(x => {
                    if (x.Value == hipartnerchildrenvalueid) {
                        x.DeleteFlag = false;
                        x.Comment = hivpartnerchildrendetail.toString();

                    } else {
                        x.DeleteFlag = true;
                    }
                });
            if (hivpartnechildrenlist.findIndex(t => t.Value == hipartnerchildrenvalueid) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': hivpartnerchildrendetail.toString(),
                    'RiskAssessmentid': hivpartnerchildrenmasterid,
                    'Value': hipartnerchildrenvalueid,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }




        this.prepservice.AddEditBehaviourRisk(this.EncounterTypeId, this.UserId, this.patientId, this.PatientMasterVisitId, date,
            this.serviceAreaId, this.RiskAssessmentList, this.ClinicalList).subscribe(
                (response) => {
                    this.PatientMasterVisitId = response['patientMasterVisitId'];

                    this.snotifyService.success('Successfully submitted the form', 'Submit RiskAssessment Form',
                        this.notificationService.getConfig());
                    this.zone.run(() => {
                        this.router.navigate(
                            ['/dashboard/personhome/' + this.personId],
                            { relativeTo: this.route });
                    });
                },
                (error) => {
                    this.snotifyService.error('Error submitting the form' + error, 'Submit RiskAssessment Form',
                        this.notificationService.getConfig());
                    this.spinner.hide();

                },
                () => {
                    this.spinner.hide();
                }
            );




    }
    public Save() {

        this.spinner.show();
        let partnerhivstatus: any[] = [];
        let clientassessmentstatus: any[] = [];
        let assessmentoutcomestatus: number;
        let riskreductioneducationstatus: number;
        let referralpreventions: number;
        let ClientWillingTakePrepstatus: number;
        let riskeducationstatus: number;
        this.RiskAssessmentList = [];
        this.ClinicalList = [];
        let partnerhivstatusdatedetail: string;
        let partnercccenrollmentdetail: number;
        let CCCnumber: string;
        let Monthdetail: number;
        let artstartdatepartner: string;
        let partnersexwithoutcondoms: number;
        let hivpartnerchildrendetail: number;
        let SpecifyRiskEducationValue: string;
        let SpecifyPreventionReferalServicesValue: string;
        let SpecifyRiskReductionEducationValue: string;
        let clinicalnotesvalue: string;

        SpecifyRiskReductionEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.value;
        SpecifyPreventionReferalServicesValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.value;
        SpecifyRiskEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.value;
        clinicalnotesvalue = this.PrepRiskAssessmentFormGroup.controls.ClinicalNotes.value;

        partnerhivstatus = this.PrepRiskAssessmentFormGroup.controls.sexualPartnerHivStatus.value;
        clientassessmentstatus = this.PrepRiskAssessmentFormGroup.controls.clientsBehaviourRisks.value;
        assessmentoutcomestatus = this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.value;
        riskreductioneducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskReductionEducation.value;
        referralpreventions = this.PrepRiskAssessmentFormGroup.controls.ReferralPreventions.value;
        ClientWillingTakePrepstatus = this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.value;
        riskeducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskEducation.value;
        partnerhivstatusdatedetail = this.PrepRiskAssessmentFormGroup.controls.partnerHIVStatusDate.value;
        partnercccenrollmentdetail = this.PrepRiskAssessmentFormGroup.controls.partnercccenrollment.value;
        CCCnumber = this.PrepRiskAssessmentFormGroup.controls.CCCNumber.value;
        artstartdatepartner = this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.value;
        partnersexwithoutcondoms = this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.value;
        hivpartnerchildrendetail = this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.value;
        Monthdetail = this.PrepRiskAssessmentFormGroup.controls.Months.value;




        for (let i = 0; i < partnerhivstatus.length; i++) {
            let index: number;

            index = this.sexualPartnerHivStatusOptions.findIndex(x => x.itemId == partnerhivstatus[i]);

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.sexualPartnerHivStatusOptions[index].masterId,
                'Value': this.sexualPartnerHivStatusOptions[index].itemId,
                'DeleteFlag': false,
                'Date': ''
            });

        }

        for (let i = 0; i < clientassessmentstatus.length; i++) {
            let index: number;

            index = this.clientsBehaviourRiskOptions.findIndex(x => x.itemId == clientassessmentstatus[i]);

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.clientsBehaviourRiskOptions[index].masterId,
                'Value': this.clientsBehaviourRiskOptions[index].itemId,
                'DeleteFlag': false,
                'Date': ''
            });

        }


        let assessmentindex: number;

        assessmentindex = this.assessmentOutComeOptions.findIndex(x => x.itemId == assessmentoutcomestatus);

        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': '',
            'RiskAssessmentid': this.assessmentOutComeOptions[assessmentindex].masterId,
            'Value': this.assessmentOutComeOptions[assessmentindex].itemId,
            'DeleteFlag': false,
            'Date': ''
        });



        let riskreductionindex: number;

        riskreductionindex = this.RiskReductionEducationOptions.findIndex(x => x.itemId == riskreductioneducationstatus);
        if (riskreductionindex !== -1) {


            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': SpecifyRiskReductionEducationValue,
                'RiskAssessmentid': this.RiskReductionEducationOptions[riskreductionindex].masterId,
                'Value': this.RiskReductionEducationOptions[riskreductionindex].itemId,
                'DeleteFlag': false,
                'Date': ''
            });
        }


        let referralpreventionindex: number;

        referralpreventionindex = this.ReferralPreventionOptions.findIndex(x => x.itemId == referralpreventions);

        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': SpecifyPreventionReferalServicesValue,
            'RiskAssessmentid': this.ReferralPreventionOptions[referralpreventionindex].masterId,
            'Value': this.ReferralPreventionOptions[referralpreventionindex].itemId,
            'DeleteFlag': false,
            'Date': ''
        });



        let clientakeprepindex: number;

        clientakeprepindex = this.clientWillingTakePrepOptions.findIndex(x => x.itemId == ClientWillingTakePrepstatus);

        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': '',
            'RiskAssessmentid': this.clientWillingTakePrepOptions[clientakeprepindex].masterId,
            'Value': this.clientWillingTakePrepOptions[clientakeprepindex].itemId,
            'DeleteFlag': false,
            'Date': ''
        });



        let riskeducationindex: number;

        riskeducationindex = this.riskEducationOptions.findIndex(x => x.itemId == riskeducationstatus);
        if (riskeducationindex !== -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': SpecifyRiskEducationValue,
                'RiskAssessmentid': this.riskEducationOptions[riskeducationindex].masterId,
                'Value': this.riskEducationOptions[riskeducationindex].itemId,
                'DeleteFlag': false,
                'Date': ''
            });
        }
        let partnerhivstatusmasterid: number;
        let partnerhivstatusvalueid: number;

        partnerhivstatusmasterid = this.partnerstatusOptions[0].masterId;
        partnerhivstatusvalueid = this.partnerstatusOptions[0].itemId;
        if (partnerhivstatusdatedetail !== null && partnerhivstatusdatedetail !== '' && partnerhivstatusdatedetail !== undefined) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': partnerhivstatusmasterid,
                'Value': partnerhivstatusvalueid,
                'DeleteFlag': false,
                'Date': partnerhivstatusdatedetail
            });
        }



        let partnerartstartdatemasterid: number;
        let partnerartstartdatevalueid: number;


        partnerartstartdatemasterid = this.partnerartstartdateoptions[0].masterId;
        partnerartstartdatevalueid = this.partnerartstartdateoptions[0].itemId;
        if (artstartdatepartner !== null && artstartdatepartner !== '' && artstartdatepartner !== undefined) {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': partnerartstartdatemasterid,
                'Value': partnerartstartdatevalueid,
                'DeleteFlag': false,
                'Date': artstartdatepartner
            });


        }




        let partnercccenrollmentmasterid: number;

        partnercccenrollmentmasterid = this.partnercccenrollmentoptions[0].masterId;


        if (partnercccenrollmentdetail !== null && partnercccenrollmentdetail !== undefined && partnercccenrollmentdetail
        ) {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': partnercccenrollmentmasterid,
                'Value': partnercccenrollmentdetail,
                'DeleteFlag': false,
                'Date': ''
            });

        }


        let partnersexcondomsmasterid: number;

        partnersexcondomsmasterid = this.sexwithoutcondomoptions[0].masterId;
        if (partnersexwithoutcondoms !== null && partnersexwithoutcondoms !== undefined &&
            partnersexwithoutcondoms
        ) {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': partnersexcondomsmasterid,
                'Value': partnersexwithoutcondoms,
                'DeleteFlag': false,
                'Date': ''
            });

        }


        let cccnumbermasterid: number;
        let cccnumbervalueid: number;

        let findindex: number;
        findindex = this.patientIdentifieroptions.findIndex(x => x.itemName == 'CCCNumber');
        cccnumbermasterid = this.patientIdentifieroptions[findindex].masterId;
        cccnumbervalueid = this.patientIdentifieroptions[findindex].itemId;
        if (CCCnumber != null || CCCnumber !== undefined || CCCnumber !== '') {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': CCCnumber,
                'RiskAssessmentid': cccnumbermasterid,
                'Value': cccnumbervalueid,
                'DeleteFlag': false,
                'Date': ''
            });
        }


        let hivsexdiscordantmasterid: number;
        let hivsexdiscordantvalueid: number;


        hivsexdiscordantmasterid = this.hivserodiscordantoptions[0].masterId;
        hivsexdiscordantvalueid = this.hivserodiscordantoptions[0].itemId;
        if (typeof Monthdetail != 'undefined' || Monthdetail) {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': Monthdetail,
                'RiskAssessmentid': hivsexdiscordantmasterid,
                'Value': hivsexdiscordantvalueid,
                'DeleteFlag': false,
                'Date': ''
            });
        }


        let hivpartnerchildrenmasterid: number;
        let hipartnerchildrenvalueid: number;

        hivpartnerchildrenmasterid = this.partnerchildrenoptions[0].masterId;
        hipartnerchildrenvalueid = this.partnerchildrenoptions[0].itemId;

        if (typeof hivpartnerchildrendetail != 'undefined' && hivpartnerchildrendetail) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': hivpartnerchildrendetail,
                'RiskAssessmentid': hivpartnerchildrenmasterid,
                'Value': hipartnerchildrenvalueid,
                'DeleteFlag': false,
                'Date': ''
            });

        }



        let clinicalnoteId: number;
        clinicalnoteId = this.behaviourRiskAssessmentOptions[0].itemId;






        this.ClinicalList.push({
            'Id': 0,
            'Comment': clinicalnotesvalue,
            'ServiceAreaId': this.serviceAreaId,
            'DeleteFlag': false

        });

        let date: string;
        date = this.PrepRiskAssessmentFormGroup.controls.visitDate.value;
        this.UserId = JSON.parse(localStorage.getItem('appUserId'));




        const patientencounter: PatientMasterVisitEncounter = {
            PatientId: this.patientId,
            EncounterType: this.EncounterTypeId,
            ServiceAreaId: this.serviceAreaId,
            UserId: this.UserId,
            EncounterDate: moment(date).toDate()

        };
        this.encounterservice.savePatientMasterVisit(patientencounter).subscribe(
            (result) => {
                localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);

                this.patientmastervisitid = result['patientMasterVisitId'];
                // this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                this.prepservice.AddEditBehaviourRisk(this.EncounterTypeId, this.UserId, this.patientId, this.patientmastervisitid, date,
                    this.serviceAreaId, this.RiskAssessmentList, this.ClinicalList).subscribe(
                        (response) => {
                            this.PatientMasterVisitId = response['patientMasterVisitId'];

                            this.snotifyService.success('Successfully submitted the form', 'Submit RiskAssessment Form',
                                this.notificationService.getConfig());

                            this.zone.run(() => {
                                this.router.navigate(
                                    ['/dashboard/personhome/' + this.personId],
                                    { relativeTo: this.route });
                            });
                        },
                        (error) => {
                            this.snotifyService.error('Error submitting the form' + error, 'Submit RiskAssessment Form',
                                this.notificationService.getConfig());
                            this.spinner.hide();

                        },
                        () => {
                            this.spinner.hide();
                        }
                    );

            },
            (error) => {
                this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
            },
            () => {

            }
        );




    }

    public Cancel() {

        this.zone.run(() => {
            this.router.navigate(
                ['/dashboard/personhome/' + this.personId],
                { relativeTo: this.route });
        });
    }
}

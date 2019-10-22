
import { Component, EventEmitter, OnInit, Output, Input, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { select } from '@ngrx/store';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoadedRouterConfig } from '@angular/router/src/config';
import { registerLocaleData } from '@angular/common';
import { PrepService } from '../_services/prep.service';
import * as moment from 'moment';
import { Subscription } from 'rxjs';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { EncounterService } from '../../shared/_services/encounter.service';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { PersonView } from '../../dashboard/_model/personView';
@Component({
    selector: 'app-prep-riskassessment',
    templateUrl: './prep-riskassessment.component.html',
    styleUrls: ['./prep-riskassessment.component.css'],
    providers: [
        EncounterService, PersonHomeService
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
    SpecifyReferralPreventionOptions: LookupItemView[] = [];
    SpecifyRiskEducationOptions: LookupItemView[] = [];
    SpecifyRiskReductionEducationOptions: LookupItemView[] = [];
    SpecifyReferralOptions: LookupItemView[] = [];
    SpecifyRiskReductionEdOptions: LookupItemView[] = [];
    SpecifyRiskEducOptions: LookupItemView[] = [];
    ExistingClinicalNotes: any[] = [];
    ExistingRiskAssessmentDetails: any[] = [];
    ExistingSexualPartnerList: any[] = [];
    careendreasonarray: LookupItemView[] = [];
    ExistingClientBehaviourRiskList: any[] = [];
    ExistingRiskReductionSpecifyList: any[] = [];
    ExistingRiskEducationSpecifyList: any[] = [];
    ExistingReferralPreventionList: any[] = [];
    maxDate: Date;
    personId: number;
    minDate: Date;
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
    public person: PersonView;
    public personView$: Subscription;
    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private prepservice: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private personHomeService: PersonHomeService,
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
            this.getPatientDetailsById(this.personId);


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
                SpecifyReferralPreventionArray,
                SpecifyRiskEducationArray,
                SpecifyRiskReductionEducationArray,
                careendreasonoptions,
                HivPartnerArray



            } = res;

            console.log(res);
            this.assessmentOutComeOptions = assessmentOutComeArray['lookupItems'];
            this.careendreasonarray = careendreasonoptions['lookupItems'];
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
            this.SpecifyReferralPreventionOptions = SpecifyReferralPreventionArray['lookupItems'];
            this.SpecifyRiskEducationOptions = SpecifyRiskEducationArray['lookupItems'];
            this.SpecifyRiskReductionEducationOptions = SpecifyRiskReductionEducationArray['lookupItems'];



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
            ClientWillingTakePrep: new FormControl('', [Validators.required]),
            RiskEducation: new FormControl(''),
            discontinueReason: new FormControl(''),
            SpecifyRiskEducation: new FormControl(''),
            ClinicalNotes: new FormControl(''),

        });


        //console.log(this.person);

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

        this.PrepRiskAssessmentFormGroup.controls.discontinueReason.disable({ onlySelf: true });






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

        this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.disable({ onlySelf: true });

    }

    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personHomeService.getPatientByPersonId(personId).subscribe(
            p => {
                // console.log(p);
                this.person = p;
                if (this.person != null) {
                    console.log(this.person);
                    if (this.person.dateOfBirth != null && this.person.dateOfBirth != undefined) {
                        this.minDate = this.person.dateOfBirth;
                    }

                    if (this.person.gender.toLowerCase() == 'female') {

                        this.SpecifyReferralOptions = this.SpecifyReferralPreventionOptions.filter(x => x.itemName !== 'VMMC');
                        this.SpecifyRiskEducOptions = this.SpecifyRiskEducationOptions.filter(x => x.itemName !== 'VMMC');
                        this.SpecifyRiskReductionEdOptions = this.SpecifyRiskReductionEducationOptions.filter(x => x.itemName !== 'VMMC');



                    } else {
                        this.SpecifyReferralOptions = this.SpecifyReferralPreventionOptions;
                        this.SpecifyRiskEducOptions = this.SpecifyRiskEducationOptions;
                        this.SpecifyRiskReductionEdOptions = this.SpecifyRiskReductionEducationOptions;
                    }

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
    OnAdherenceOutcome(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        // val = this.selectedRiskReductionEducation;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.assessmentOutComeOptions.findIndex(x => x.itemId == val);

                if (index >= 0) {
                    text = this.assessmentOutComeOptions[index].itemDisplayName;
                    if (text.toLowerCase() === 'risk') {
                        this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.enable({ onlySelf: true });

                    } else if (text.toLowerCase() === 'no risk') {
                        this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.disable({ onlySelf: true });
                        this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.setValue('');
                        this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue('');
                        this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });

                        this.PrepRiskAssessmentFormGroup.controls.discontinueReason.setValue('');
                        this.PrepRiskAssessmentFormGroup.controls.discontinueReason.disable({ onlySelf: true });

                    }
                    else {
                        this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.enable({ onlySelf: true });
                    }

                }
            }
        }

    }
    LoadDetails() {
        this.prepservice.GetAssessmentDetails(this.patientId, this.PatientMasterVisitId).subscribe((result) => {

            this.ExistingClinicalNotes = [];
            this.ExistingRiskAssessmentDetails = [];
            this.ExistingSexualPartnerList = [];
            this.ExistingClientBehaviourRiskList = [];
            this.ExistingRiskReductionSpecifyList = [];
            this.ExistingRiskEducationSpecifyList = [];
            this.ExistingReferralPreventionList = [];

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

            riskreductiondetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskreductionmasterid).map(o => {
                    return o.value;
                });

            let riskreductiondetailspecifymasterid: number;
            riskreductiondetailspecifymasterid = this.SpecifyRiskReductionEducationOptions[0].masterId;

            this.ExistingRiskReductionSpecifyList = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskreductiondetailspecifymasterid).map(o => {
                    return o.value;
                });


            let referralpreventionmasterid: number;
            referralpreventionmasterid = this.ReferralPreventionOptions[0].masterId;
            let referralpreventiondetailvalue: any[] = [];

            referralpreventiondetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == referralpreventionmasterid).map(o => {
                    return o.value;
                });

            let referralpreventionservicesspecifymasterid: number;
            referralpreventionservicesspecifymasterid = this.SpecifyReferralPreventionOptions[0].masterId;

            this.ExistingReferralPreventionList = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == referralpreventionservicesspecifymasterid).map(o => {
                    return o.value;
                });


            let clientwillingprepmasterid: number;
            clientwillingprepmasterid = this.clientWillingTakePrepOptions[0].masterId;
            let clientwillingprepdetail: any[] = [];
            clientwillingprepdetail = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == clientwillingprepmasterid).map(o => {
                    return o.value;
                });

            let prepdeclinereason: number;
            prepdeclinereason = this.careendreasonarray[0].masterId;
            let prepdeclinelist: any[] = [];
            prepdeclinelist = this.ExistingRiskAssessmentDetails.filter(x => x.riskAssessmentid == prepdeclinereason).map(o => {
                return o.value;
            });

            let riskeducationmasterid: number;
            riskeducationmasterid = this.riskEducationOptions[0].masterId;
            let riskeducationdetailvalue: any[] = [];

            riskeducationdetailvalue = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskeducationmasterid).map(o => {
                    return o.value;
                });

            let riskeducationspecifymasterid: number;
            riskeducationspecifymasterid = this.SpecifyRiskEducationOptions[0].masterId;
            this.ExistingRiskEducationSpecifyList = this.ExistingRiskAssessmentDetails
                .filter(x => x.riskAssessmentid == riskeducationspecifymasterid).map(o => {
                    return o.value;
                });

            let clinicalnotesvalue: any[] = [];
            clinicalnotesvalue = this.ExistingClinicalNotes.map(o => {
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
            this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.setValue(this.ExistingRiskReductionSpecifyList);
            this.PrepRiskAssessmentFormGroup.controls.ReferralPreventions.setValue(referralpreventiondetailvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue(this.ExistingReferralPreventionList);
            this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.setValue(clientwillingprepdetail[0]);
            this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue(riskeducationdetailvalue[0]);
            this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue(this.ExistingRiskEducationSpecifyList);
            this.PrepRiskAssessmentFormGroup.controls.ClinicalNotes.setValue(clinicalnotesvalue[0]);

            this.PrepRiskAssessmentFormGroup.controls.discontinueReason.setValue(prepdeclinelist);




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
                } else if (text.toLowerCase() === 'no') {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });
                }
                else {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });

                }
            } else {
                this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });

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
                    this.PrepRiskAssessmentFormGroup.controls.discontinueReason.enable({ onlySelf: true });

                } else if (text.toLowerCase() === 'yes') {
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.discontinueReason.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.discontinueReason.setValue('');
                } else {

                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.discontinueReason.disable({ onlySelf: true });
                    this.PrepRiskAssessmentFormGroup.controls.discontinueReason.setValue('');
                }
            } else {
                this.PrepRiskAssessmentFormGroup.controls.RiskEducation.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.RiskEducation.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.discontinueReason.disable({ onlySelf: true });
                this.PrepRiskAssessmentFormGroup.controls.discontinueReason.setValue('');
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
                } else {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });

                }
            } else {
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.disable({ onlySelf: true });

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
                else {
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.setValue('');
                    this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.disable({ onlySelf: true });
                }
            } else {
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.setValue('');
                this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.disable({ onlySelf: true });
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
                    (event.source._parent.options._results[i].viewValue !== event.source.viewValue)
                    && (event.source._parent.options._results[i].viewValue !== 'Not Applicable')) {

                    event.source._parent.options._results[i].deselect();
                    event.source._parent.options._results[i].disabled = true;
                }
                // console.log(event.source._parent.options._results[i].value);
            }
        } else if (event.source.viewValue === 'Not on ART' && event.source.selected == false) {
            for (let i = 0; i < event.source._parent.options.length; i++) {

                if ((event.source._parent.options._results[i].viewValue !== 'Couple is trying to conceive') &&
                    (event.source._parent.options._results[i].viewValue !== event.source.viewValue)
                    && (event.source._parent.options._results[i].viewValue !== 'Not Applicable')) {


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

        let SpecifyRiskEducationValue: any[] = [];
        let SpecifyPreventionReferalServicesValue: any[] = [];
        let SpecifyRiskReductionEducationValue: any[] = [];
        let clinicalnotesvalue: string;
        /*let partnerhivstatusdatedetail: string;
        let partnercccenrollmentdetail: number;
        let CCCnumber: string;
        let Monthdetail: number;
        let artstartdatepartner: string;
        let partnersexwithoutcondoms: number;
        let hivpartnerchildrendetail: number;*/
        let prepdeclinereason: any[] = [];

        SpecifyRiskReductionEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.value;
        SpecifyPreventionReferalServicesValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.value;
        SpecifyRiskEducationValue = this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskEducation.value;
        clinicalnotesvalue = this.PrepRiskAssessmentFormGroup.controls.ClinicalNotes.value;
        prepdeclinereason = this.PrepRiskAssessmentFormGroup.controls.discontinueReason.value
        partnerhivstatus = this.PrepRiskAssessmentFormGroup.controls.sexualPartnerHivStatus.value;
        clientassessmentstatus = this.PrepRiskAssessmentFormGroup.controls.clientsBehaviourRisks.value;
        assessmentoutcomestatus = this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.value;
        riskreductioneducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskReductionEducation.value;
        referralpreventions = this.PrepRiskAssessmentFormGroup.controls.ReferralPreventions.value;
        ClientWillingTakePrepstatus = this.PrepRiskAssessmentFormGroup.controls.ClientWillingTakePrep.value;
        riskeducationstatus = this.PrepRiskAssessmentFormGroup.controls.RiskEducation.value;
        /*partnerhivstatusdatedetail = this.PrepRiskAssessmentFormGroup.controls.partnerHIVStatusDate.value;
        partnercccenrollmentdetail = this.PrepRiskAssessmentFormGroup.controls.partnercccenrollment.value;
        CCCnumber = this.PrepRiskAssessmentFormGroup.controls.CCCNumber.value;
        artstartdatepartner = this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.value;
        partnersexwithoutcondoms = this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.value;
        hivpartnerchildrendetail = this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.value;
        Monthdetail = this.PrepRiskAssessmentFormGroup.controls.Months.value; */



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
        if (assessmentoutcomestatus > 0) {
            if (assessmentoutcomelist.findIndex(t => t.Value == assessmentoutcomestatus) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': assessmentoutcomemasterid,
                    'Value': assessmentoutcomestatus,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }

        let riskreductioneducationmasterid: number;
        let riskreductioneducationlist: any[] = [];
        riskreductioneducationmasterid = this.RiskReductionEducationOptions[0].masterId;
        riskreductioneducationlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskreductioneducationmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == riskreductioneducationmasterid)
            .forEach(x => {
                if (x.Value == riskreductioneducationstatus) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });
        if (riskreductioneducationstatus > 0) {
            if (riskreductioneducationlist.findIndex(t => t.Value == riskreductioneducationstatus) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': riskreductioneducationmasterid,
                    'Value': riskreductioneducationstatus,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }


        let specifyriskreductionmasterid: number;
        let specifyriskreductionlist: any[] = [];
        specifyriskreductionmasterid = this.SpecifyRiskReductionEducationOptions[0].masterId;
        specifyriskreductionlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifyriskreductionmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifyriskreductionmasterid)
            .forEach(x => {
                let status: any[] = [];
                status = SpecifyRiskReductionEducationValue.filter(t => t == x.Value);
                if (x.Value == status) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });


        let specifyriskreductionnotexists: any[] = [];

        specifyriskreductionnotexists = SpecifyRiskReductionEducationValue.filter(r => {
            if (specifyriskreductionlist.findIndex(t => t.Value == r) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': specifyriskreductionmasterid,
                    'Value': r,
                    'DeleteFlag': false,
                    'Date': ''
                });
                return r;

            }
        });


















        let referralpreventionmasterid: number;
        let referralpreventionlist: any[] = [];
        referralpreventionmasterid = this.ReferralPreventionOptions[0].masterId;
        referralpreventionlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == referralpreventionmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == referralpreventionmasterid)
            .forEach(x => {
                if (x.Value == referralpreventions) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });
        if (referralpreventions > 0) {
            if (referralpreventionlist.findIndex(t => t.Value == referralpreventions) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': referralpreventionmasterid,
                    'Value': referralpreventions,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }


        let specifypreventionservicesmasterid: number;
        let specifypreventionserviceslist: any[] = [];
        specifypreventionservicesmasterid = this.SpecifyReferralPreventionOptions[0].masterId;
        specifypreventionserviceslist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifypreventionservicesmasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifypreventionservicesmasterid)
            .forEach(x => {
                let status: any[] = [];
                status = SpecifyPreventionReferalServicesValue.filter(t => t == x.Value);
                if (x.Value == status) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });


        let specifypreventionservicesnotexists: any[] = [];

        specifypreventionservicesnotexists = SpecifyPreventionReferalServicesValue.filter(r => {
            if (specifypreventionserviceslist.findIndex(t => t.Value == r) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': specifypreventionservicesmasterid,
                    'Value': r,
                    'DeleteFlag': false,
                    'Date': ''
                });
                return r;

            }
        });







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
        if (ClientWillingTakePrepstatus > 0) {
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
        }


        let prepdeclinemasterid: number;
        let prepdeclinepreplist: any[] = [];
        prepdeclinemasterid = this.careendreasonarray[0].masterId;
        prepdeclinepreplist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == prepdeclinemasterid);
        this.RiskAssessmentList.filter(x => x.RiskAssessmentid == prepdeclinemasterid)
            .forEach(x => {
                let status: any[] = [];
                status = prepdeclinereason.filter(t => t == x.Value);
                if (x.Value == status) {
                    x.DeleteFlag = false;

                } else {
                    x.DeleteFlag = true;
                }
            });


        let declinereasonnotexists: any[] = [];

        console.log(prepdeclinereason);
        if (prepdeclinereason) {
            declinereasonnotexists = prepdeclinereason.filter(r => {
                if (prepdeclinepreplist.findIndex(t => t.Value == r) == -1) {

                    this.RiskAssessmentList.push({
                        'Id': 0,
                        'Comment': '',
                        'RiskAssessmentid': prepdeclinemasterid,
                        'Value': r,
                        'DeleteFlag': false,
                        'Date': ''
                    });
                    return r;

                }
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

                } else {
                    x.DeleteFlag = true;
                }
            });
        if (riskeducationstatus > 0) {
            if (riskeducationlist.findIndex(t => t.Value == riskeducationstatus) == -1) {

                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': riskeducationmasterid,
                    'Value': riskeducationstatus,
                    'DeleteFlag': false,
                    'Date': ''
                });

            }
        }

        if (SpecifyRiskEducationValue.length > 0) {
            let specifyriskeducationmasterid: number;
            let specifyriskeducationlist: any[] = [];
            specifyriskeducationmasterid = this.SpecifyRiskEducationOptions[0].masterId;
            specifyriskeducationlist = this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifyriskeducationmasterid);
            this.RiskAssessmentList.filter(x => x.RiskAssessmentid == specifyriskeducationmasterid)
                .forEach(x => {
                    let status: any[] = [];



                    status = SpecifyRiskEducationValue.filter(t => t == x.Value);
                    if (x.Value == status) {
                        x.DeleteFlag = false;

                    } else {
                        x.DeleteFlag = true;
                    }

                });


            let specifyriskeducationnotexists: any[] = [];


            specifyriskeducationnotexists = SpecifyRiskEducationValue.filter(r => {
                if (specifyriskeducationlist.findIndex(t => t.Value == r) == -1) {

                    this.RiskAssessmentList.push({
                        'Id': 0,
                        'Comment': '',
                        'RiskAssessmentid': specifyriskeducationmasterid,
                        'Value': r,
                        'DeleteFlag': false,
                        'Date': ''
                    });
                    return r;

                }
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
        /*  let partnerhivstatusdatedetail: string;
          let partnercccenrollmentdetail: number;
          let CCCnumber: string;
          let Monthdetail: number;
          let artstartdatepartner: string;
          let partnersexwithoutcondoms: number;
          let hivpartnerchildrendetail: number;*/
        let SpecifyRiskEducationValue: any[] = [];
        let SpecifyPreventionReferalServicesValue: any[] = [];
        let SpecifyRiskReductionEducationValue: any[] = [];
        let clinicalnotesvalue: string;
        let prepdeclinereason: any[] = [];
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
        /* partnerhivstatusdatedetail = this.PrepRiskAssessmentFormGroup.controls.partnerHIVStatusDate.value;
         partnercccenrollmentdetail = this.PrepRiskAssessmentFormGroup.controls.partnercccenrollment.value;
         CCCnumber = this.PrepRiskAssessmentFormGroup.controls.CCCNumber.value;
         artstartdatepartner = this.PrepRiskAssessmentFormGroup.controls.partnerARTStartDate.value;
         partnersexwithoutcondoms = this.PrepRiskAssessmentFormGroup.controls.partnersexcondoms.value;
         hivpartnerchildrendetail = this.PrepRiskAssessmentFormGroup.controls.hivpartnerchildren.value;
         Monthdetail = this.PrepRiskAssessmentFormGroup.controls.Months.value;*/
        prepdeclinereason = this.PrepRiskAssessmentFormGroup.controls.discontinueReason.value;



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
                'Comment': '',
                'RiskAssessmentid': this.RiskReductionEducationOptions[riskreductionindex].masterId,
                'Value': this.RiskReductionEducationOptions[riskreductionindex].itemId,
                'DeleteFlag': false,
                'Date': ''
            });
        }
        for (let i = 0; i < SpecifyRiskReductionEducationValue.length; i++) {
            let index: number;

            index = this.SpecifyRiskReductionEducationOptions.findIndex(x => x.itemId == SpecifyRiskReductionEducationValue[i]);

            if (index !== -1) {
                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': this.SpecifyRiskReductionEducationOptions[index].masterId,
                    'Value': this.SpecifyRiskReductionEducationOptions[index].itemId,
                    'DeleteFlag': false,
                    'Date': ''
                });
            }

        }




        let referralpreventionindex: number;

        referralpreventionindex = this.ReferralPreventionOptions.findIndex(x => x.itemId == referralpreventions);

        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': '',
            'RiskAssessmentid': this.ReferralPreventionOptions[referralpreventionindex].masterId,
            'Value': this.ReferralPreventionOptions[referralpreventionindex].itemId,
            'DeleteFlag': false,
            'Date': ''
        });


        for (let i = 0; i < SpecifyPreventionReferalServicesValue.length; i++) {
            let index: number;

            index = this.SpecifyReferralPreventionOptions.findIndex(x => x.itemId == SpecifyPreventionReferalServicesValue[i]);

            if (index !== -1) {
                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': this.SpecifyReferralPreventionOptions[index].masterId,
                    'Value': this.SpecifyReferralPreventionOptions[index].itemId,
                    'DeleteFlag': false,
                    'Date': ''
                });
            }

        }



        let clientakeprepindex: number;

        clientakeprepindex = this.clientWillingTakePrepOptions.findIndex(x => x.itemId == ClientWillingTakePrepstatus);

        if (clientakeprepindex !== -1) {
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.clientWillingTakePrepOptions[clientakeprepindex].masterId,
                'Value': this.clientWillingTakePrepOptions[clientakeprepindex].itemId,
                'DeleteFlag': false,
                'Date': ''
            });
        }



        for (let i = 0; i < prepdeclinereason.length; i++) {
            let prepdeclinereasonindex: number;
            prepdeclinereasonindex = this.careendreasonarray.findIndex(x => x.itemId == prepdeclinereason[i]);



            if (prepdeclinereasonindex !== -1) {
                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': this.careendreasonarray[prepdeclinereasonindex].masterId,
                    'Value': this.careendreasonarray[prepdeclinereasonindex].itemId,
                    'DeleteFlag': false,
                    'Date': ''
                });
            }

        }


        let riskeducationindex: number;

        riskeducationindex = this.riskEducationOptions.findIndex(x => x.itemId == riskeducationstatus);
        if (riskeducationindex !== -1) {

            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.riskEducationOptions[riskeducationindex].masterId,
                'Value': this.riskEducationOptions[riskeducationindex].itemId,
                'DeleteFlag': false,
                'Date': ''
            });
        }




        for (let i = 0; i < SpecifyRiskEducationValue.length; i++) {
            let index: number;

            index = this.SpecifyRiskEducationOptions.findIndex(x => x.itemId == SpecifyRiskEducationValue[i]);
            if (index !== -1) {
                this.RiskAssessmentList.push({
                    'Id': 0,
                    'Comment': '',
                    'RiskAssessmentid': this.SpecifyRiskEducationOptions[index].masterId,
                    'Value': this.SpecifyRiskEducationOptions[index].itemId,
                    'DeleteFlag': false,
                    'Date': ''
                });
            }
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

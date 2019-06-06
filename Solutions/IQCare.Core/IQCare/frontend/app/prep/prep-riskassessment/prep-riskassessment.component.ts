
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

@Component({
    selector: 'app-prep-riskassessment',
    templateUrl: './prep-riskassessment.component.html',
    styleUrls: ['./prep-riskassessment.component.css']
})
export class PrepRiskassessmentComponent implements OnInit {
    public PrepRiskAssessmentFormGroup: FormGroup;
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
    selectedRiskReductionEducation: number;
    selectedReferralPreventionOptions: number;
    selectedClientWillingTakingPrep: number;
    selectedRiskEducationOption: number;
    EncounterTypeId: number;
    UserId: number;
    PatientMasterVisitId: number;
    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private prepservice: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService) {
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

            console.log(this.serviceAreaId);
        });
        this.route.data.subscribe((res) => {
            const { assessmentOutComeArray, clientsBehaviourRiskArray,
                sexualPartnerHivStatusArray,
                clientWillingTakePrepArray,
                riskEducationArray,
                behaviourRiskAssessmentArray,
                ReferralPreventionArray,
                RiskReductionEducationArray,
                EncounterTypeArray
            } = res;
            console.log(assessmentOutComeArray);
            this.assessmentOutComeOptions = assessmentOutComeArray['lookupItems'];
            this.clientsBehaviourRiskOptions = clientsBehaviourRiskArray['lookupItems'];
            this.sexualPartnerHivStatusOptions = sexualPartnerHivStatusArray['lookupItems'];
            this.clientWillingTakePrepOptions = clientWillingTakePrepArray['lookupItems'];
            this.riskEducationOptions = riskEducationArray['lookupItems'];
            this.behaviourRiskAssessmentOptions = behaviourRiskAssessmentArray['lookupItems'];
            this.ReferralPreventionOptions = ReferralPreventionArray['lookupItems'];
            this.RiskReductionEducationOptions = RiskReductionEducationArray['lookupItems'];
            this.EncounterTypeOptions = EncounterTypeArray['lookupItems'];








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
            SpecifyRiskEducation: new FormControl(''),
            ClinicalNotes: new FormControl('')
        });
        this.RiskViewOptions = this.clientsBehaviourRiskOptions.concat(this.sexualPartnerHivStatusOptions);
        this.RiskViewOptions.forEach(function (e) {
            if (typeof e === 'object') {
                e['checked'] = false;
            }
        });

        if (this.PatientMasterVisitId > 0) {
            this.LoadDetails();
        }
        let assessmentoutcomerisk: any[] = [];
        let NoRiskOutcome: number;
        assessmentoutcomerisk = this.assessmentOutComeOptions.filter(x => x.itemDisplayName == 'No Risk');
        NoRiskOutcome = assessmentoutcomerisk[0]['itemId'];
        this.PrepRiskAssessmentFormGroup.controls.assessmentOutCome.setValue(NoRiskOutcome);
        let prepriskencounter: any[] = [];
        prepriskencounter = this.EncounterTypeOptions.filter(x => x.itemDisplayName == 'PrepRiskAssessment-encounter')
        this.EncounterTypeId = prepriskencounter[0]['itemId'];
        this.PrepRiskAssessmentFormGroup.controls.SpecifyRiskReductionEducation.disable({ onlySelf: true });
        this.PrepRiskAssessmentFormGroup.controls.SpecifyPreventionReferalServices.disable({ onlySelf: true });
    }

    LoadDetails() {
        this.prepservice.GetAssessmentDetails(this.patientId, this.PatientMasterVisitId).subscribe((result) => {
            console.log('existing results');
            console.log(result);
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
                    'DeleteFlag': o.deleteFlag
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

            let visitDate: Date;
            visitDate = result['visitDate'];
            this.PrepRiskAssessmentFormGroup.controls.visitDate.setValue(visitDate);
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
        },
            (error) => {
                this.snotifyService.error('Error loading existing details' + error, 'RiskAssessment Form Details',
                    this.notificationService.getConfig());
            });
    }
    OnReferralPreventionOffered() {
        let val: number;
        let index: number;
        let text: string;

        val = this.selectedReferralPreventionOptions;
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
    OnClientWillingTakingPrep() {
        let val: number;
        let index: number;
        let text: string;

        val = this.selectedClientWillingTakingPrep;
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
    OnRiskEducationOffered() {
        let val: number;
        let index: number;
        let text: string;

        val = this.selectedRiskEducationOption;
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


    OnRiskReductionEducationOffered() {
        let val: number;
        let index: number;
        let text: string;

        val = this.selectedRiskReductionEducation;
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
    CheckRiskOptions() {
        let notselected: boolean = true;
        let length: number;
        length = this.RiskViewOptions.length;
        let count: number = 0;
        for (let i = 0; i < this.RiskViewOptions.length; i++) {

            if (this.RiskViewOptions[i].checked == false) {
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
            if (this.RiskViewOptions[i].checked == true) {
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

        this.CheckRiskOptions();


    }
    OnSexualPartnerSelection(event) {

        const value = event.source.value;

        const objIndex = this.RiskViewOptions.findIndex((obj => obj.itemId == value));
        this.RiskViewOptions[objIndex].checked = event.source.selected;

        console.log(this.RiskViewOptions);

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
             if (this.PatientMasterVisitId > 0)  {
        
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
                    'DeleteFlag': false
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
                    'DeleteFlag': false
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
                'DeleteFlag': false
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
                'DeleteFlag': false
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
                'DeleteFlag': false
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
                'DeleteFlag': false
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
                'DeleteFlag': false
            });

        }

        if (clinicalnotesvalue == '') {
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

        this.prepservice.AddEditBehaviourRisk(this.EncounterTypeId, this.UserId, this.patientId, 0, date,
            this.serviceAreaId, this.RiskAssessmentList, this.ClinicalList).subscribe(
                (response) => {
                    this.PatientMasterVisitId = response['patientMasterVisitId'];
                    console.log(response);
                    this.snotifyService.success(response.message, 'Submit RiskAssessment Form',
                        this.notificationService.getConfig());
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




        for (let i = 0; i < partnerhivstatus.length; i++) {
            let index: number;
            console.log(partnerhivstatus[i].value);
            console.log(partnerhivstatus[i]);
            index = this.sexualPartnerHivStatusOptions.findIndex(x => x.itemId == partnerhivstatus[i]);
            console.log(this.sexualPartnerHivStatusOptions[index].masterId);
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.sexualPartnerHivStatusOptions[index].masterId,
                'Value': this.sexualPartnerHivStatusOptions[index].itemId,
                'DeleteFlag': false
            });

        }

        for (let i = 0; i < clientassessmentstatus.length; i++) {
            let index: number;

            index = this.clientsBehaviourRiskOptions.findIndex(x => x.itemId == clientassessmentstatus[i]);
            console.log(this.clientsBehaviourRiskOptions[index].masterId);
            this.RiskAssessmentList.push({
                'Id': 0,
                'Comment': '',
                'RiskAssessmentid': this.clientsBehaviourRiskOptions[index].masterId,
                'Value': this.clientsBehaviourRiskOptions[index].itemId,
                'DeleteFlag': false
            });

        }


        let assessmentindex: number;

        assessmentindex = this.assessmentOutComeOptions.findIndex(x => x.itemId == assessmentoutcomestatus);
        console.log(this.assessmentOutComeOptions[assessmentindex].masterId);
        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': '',
            'RiskAssessmentid': this.assessmentOutComeOptions[assessmentindex].masterId,
            'Value': this.assessmentOutComeOptions[assessmentindex].itemId,
            'DeleteFlag': false
        });



        let riskreductionindex: number;

        riskreductionindex = this.RiskReductionEducationOptions.findIndex(x => x.itemId == riskreductioneducationstatus);
        console.log(this.RiskReductionEducationOptions[riskreductionindex].masterId);
        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': SpecifyRiskReductionEducationValue,
            'RiskAssessmentid': this.RiskReductionEducationOptions[riskreductionindex].masterId,
            'Value': this.RiskReductionEducationOptions[riskreductionindex].itemId,
            'DeleteFlag': false
        });



        let referralpreventionindex: number;

        referralpreventionindex = this.ReferralPreventionOptions.findIndex(x => x.itemId == referralpreventions);
        console.log(this.ReferralPreventionOptions[referralpreventionindex].masterId);
        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': SpecifyPreventionReferalServicesValue,
            'RiskAssessmentid': this.ReferralPreventionOptions[referralpreventionindex].masterId,
            'Value': this.ReferralPreventionOptions[referralpreventionindex].itemId,
            'DeleteFlag': false
        });



        let clientakeprepindex: number;

        clientakeprepindex = this.clientWillingTakePrepOptions.findIndex(x => x.itemId == ClientWillingTakePrepstatus);
        console.log(this.clientWillingTakePrepOptions[clientakeprepindex].masterId);
        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': '',
            'RiskAssessmentid': this.clientWillingTakePrepOptions[clientakeprepindex].masterId,
            'Value': this.clientWillingTakePrepOptions[clientakeprepindex].itemId,
            'DeleteFlag': false
        });



        let riskeducationindex: number;

        riskeducationindex = this.riskEducationOptions.findIndex(x => x.itemId == riskeducationstatus);
        console.log(this.riskEducationOptions[riskeducationindex].masterId);
        this.RiskAssessmentList.push({
            'Id': 0,
            'Comment': SpecifyRiskEducationValue,
            'RiskAssessmentid': this.riskEducationOptions[riskeducationindex].masterId,
            'Value': this.riskEducationOptions[riskeducationindex].itemId,
            'DeleteFlag': false
        });



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

        
        this.prepservice.AddEditBehaviourRisk(this.EncounterTypeId, this.UserId, this.patientId, 0, date,
            this.serviceAreaId, this.RiskAssessmentList, this.ClinicalList).subscribe(
                (response) => {
                    this.PatientMasterVisitId = response['patientMasterVisitId'];
                    console.log(response);
                    this.snotifyService.success(response.message, 'Submit RiskAssessment Form',
                        this.notificationService.getConfig());
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
}

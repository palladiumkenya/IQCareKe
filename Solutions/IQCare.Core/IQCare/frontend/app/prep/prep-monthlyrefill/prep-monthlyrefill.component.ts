import { Component, OnInit, Output, Input, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { select } from '@ngrx/store';
import { NgxSpinnerService } from 'ngx-spinner';
import { NextAppointmentCommand } from '../_models/commands/nextAppointmentCommand';
import { LoadedRouterConfig } from '@angular/router/src/config';
import { registerLocaleData } from '@angular/common';
import { PrepService } from '../_services/prep.service';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { EncounterService } from '../../shared/_services/encounter.service';
import { SearchService } from '../../registration/_services/search.service';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import * as moment from 'moment';
import { PatientAppointmentEditCommand } from '../../pmtct/_models/PatientAppointmentEditCommand';
import { Search } from '../../records/_models/search';
import { PersonView } from '../../dashboard/_model/personView';
import { Subscription } from 'rxjs';
import { PersonHomeService } from '../../dashboard/services/person-home.service';

@Component({
    selector: 'app-prep-monthlyrefill',
    templateUrl: './prep-monthlyrefill.component.html',
    styleUrls: ['./prep-monthlyrefill.component.css'],
    providers: [
        EncounterService, SearchService
    ]
})
export class PrepMonthlyrefillComponent implements OnInit {
    public PrepMonthlyRefillFormGroup: FormGroup;
    clientsBehaviourRiskOptions: LookupItemView[] = [];
    sexualPartnerHivStatusOptions: LookupItemView[] = [];
    PrepAdherenceOptions: LookupItemView[] = [];
    AdherenceAssessmentReasonOptions: LookupItemView[] = [];
    RefillPrepStatusOptions: LookupItemView[] = [];
    PrepDiscontinueReasonOptions: LookupItemView[] = [];
    EncounterTypeOptions: LookupItemView[] = [];
    AdherenceCounsellingOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    AppointmentGivenOptions: LookupItemView[] = [];
    PrepAppointmentReasonOptions: LookupItemView[] = [];
    RiskAssessmentDoneOptions: LookupItemView[] = [];
    maxDate: Date;
    minDate: Date;
    personId: number;
    nextappointmentid: number;
    serviceAreaId: number;
    patientId: number;
    patientMasterVisitId: number = 0;
    EncounterTypeId: number;
    UserId: number;
    Age: number = 20;
    ExistingData: any[] = [];
    ExistingClinicalNotes: any[] = [];
    Visible: boolean = false;
    RiskAssessmentVisible: boolean = false;
    details: any[] = [];
    outcomelist: any[] = [];
    remarklist: any[] = [];
    Encounters: any[] = [];
    appointmentStatusId: number;
    appointmentReasonId: number;
    userId: number;

    public person: PersonView;
    public personView$: Subscription;
    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private prepservice: PrepService,
        private searchService: SearchService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private personHomeService: PersonHomeService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private encounterservice: EncounterService) {
        this.maxDate = new Date();

    }

    ngOnInit() {

        this.route.params.subscribe(params => {
            const { patientId, personId, serviceId, patientMasterVisitId } = params;

            this.personId = personId;
            this.serviceAreaId = serviceId;
            this.patientId = patientId;
            this.patientMasterVisitId = patientMasterVisitId;
            this.getPatientDetailsById(this.personId);

        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this._lookupItemService.getByGroupNameAndItemName('AppointmentStatus', 'Pending').subscribe(
            (res) => {
                this.appointmentStatusId = res['itemId'];
            }
        );

        this._lookupItemService.getByGroupNameAndItemName('AppointmentReason', 'Follow Up').subscribe(
            (res) => {
                this.appointmentReasonId = res['itemId'];
            }
        );

        this.route.data.subscribe((res) => {
            const { sexualPartnerHivStatusArray, clientsBehaviourRiskArray,
                PrepAdherenceArray, AdherenceAssessmentReasonArray,
                RefillPrepStatusArray, PrepDiscontinueReasonArray, EncounterTypeArray, yesNoOptions,
                AdherenceCounsellingArray, AppointmentGivenArray,
                PrepAppointmentReasonArray, RiskAssessmentDoneArray
            } = res;
            this.clientsBehaviourRiskOptions = clientsBehaviourRiskArray['lookupItems'];
            this.sexualPartnerHivStatusOptions = sexualPartnerHivStatusArray['lookupItems'];
            this.PrepAdherenceOptions = PrepAdherenceArray['lookupItems'];
            this.AdherenceAssessmentReasonOptions = AdherenceAssessmentReasonArray['lookupItems']
            this.RefillPrepStatusOptions = RefillPrepStatusArray['lookupItems'];
            this.PrepDiscontinueReasonOptions = PrepDiscontinueReasonArray['lookupItems'];
            this.EncounterTypeOptions = EncounterTypeArray['lookupItems'];
            this.AdherenceCounsellingOptions = AdherenceCounsellingArray['lookupItems'];
            this.yesnoOptions = yesNoOptions['lookupItems'];
            this.AppointmentGivenOptions = AppointmentGivenArray['lookupItems'];
            this.PrepAppointmentReasonOptions = PrepAppointmentReasonArray['lookupItems'];
            this.RiskAssessmentDoneOptions = RiskAssessmentDoneArray['lookupItems'];

        });
        let monthlyrefillencounter: any[] = [];
        monthlyrefillencounter = this.EncounterTypeOptions.filter(x => x.itemDisplayName == 'MonthlyRefill-encounter');
        this.EncounterTypeId = monthlyrefillencounter[0]['itemId'];


        this.PrepMonthlyRefillFormGroup = this._formBuilder.group({
            RiskAssessmentDone: new FormControl('', [Validators.required]),
            sexualPartnerHivStatus: new FormControl(''),
            visitDate: new FormControl('', [Validators.required]),
            clientsBehaviourRisks: new FormControl(''),
            adherenceassessment: new FormControl('', [Validators.required]),
            adherenceassessmentreasons: new FormControl(''),
            SpecifyAssessmentReason: new FormControl(''),
            adherencecounselling: new FormControl(''),
            prepstatus: new FormControl('', [Validators.required]),
            prepdiscontinuereason: new FormControl(''),
            SpecifyDiscontinueReason: new FormControl(''),
            nextAppointmentGiven: new FormControl('', [Validators.required]),
            nextAppointmentDate: new FormControl(''),
            AppointmentReason: new FormControl(''),
            Remarks: new FormControl('')
        });


        if (localStorage.getItem('PrepVisitDate') != null && localStorage.getItem('PrepVisitDate') != undefined) {
            this.PrepMonthlyRefillFormGroup.controls.visitDate.setValue(moment(localStorage.getItem('PrepVisitDate')).toDate());
        }

        this.PrepMonthlyRefillFormGroup.controls.adherenceassessmentreasons.disable({ onlySelf: true });
        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');
        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.disable({ onlySelf: true });
        this.PrepMonthlyRefillFormGroup.controls.prepdiscontinuereason.disable({ onlySelf: true });
        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue('');
        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.disable({ onlySelf: true });
        this.PrepMonthlyRefillFormGroup.controls.nextAppointmentDate.disable({ onlySelf: true });
        this.LoadDetails();
        this.LoadAppointments();

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
                    if (this.person.ageNumber != null && this.person.ageNumber != undefined) {
                        this.Age = this.person.ageNumber;
                    } else {
                        this.Age = 20;
                    }
                }

            },
            (err) => {
                this.snotifyService.error('Error loading patient details' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }

    LoadAppointments() {
        if (this.patientMasterVisitId > 0) {

            this.prepservice.getAppointments(this.patientId, this.patientMasterVisitId)
                .subscribe((result) => {
                    console.log(result);
                    if (result != undefined && result != null) {

                        this.nextappointmentid = result['id'];
                        this.PrepMonthlyRefillFormGroup.controls.nextAppointmentDate.setValue(result['appointmentDate']);

                        let reason: any[] = [];
                        reason = this.PrepAppointmentReasonOptions.filter(x => x.itemId == result['reasonId']).map(o => {
                            return o.itemDisplayName;
                        });
                        this.PrepMonthlyRefillFormGroup.controls.AppointmentReason.setValue(reason[0]);
                    }
                },
                    (error) => {
                        console.log(error);
                    });

        }


    }
    LoadDetails() {
        if (this.patientMasterVisitId > 0) {
            this.prepservice.getPatientMasterVisits(this.patientId, this.patientMasterVisitId).subscribe((res) => {
                this.Encounters = res;
                console.log(this.Encounters);
                if (this.Encounters != null) {
                    let visitDate: Date;
                    visitDate = this.Encounters[0]['visitDate'];
                    this.PrepMonthlyRefillFormGroup.controls.visitDate.setValue(visitDate);
                }
            });


            this.prepservice.getMonthlyRefillDetails(this.patientMasterVisitId, this.patientId, this.serviceAreaId).subscribe(
                (result) => {
                    console.log(result);
                    this.ExistingData = result['refilldetails'];
                    this.ExistingClinicalNotes = result['clinicalnote'];
                    console.log(this.ExistingData);
                    console.log(this.ExistingClinicalNotes);

                    if (this.ExistingClinicalNotes != null) {
                        let notes: string;
                        notes = this.ExistingClinicalNotes['clinicalNotes'];
                        this.PrepMonthlyRefillFormGroup.controls.Remarks.setValue(notes);
                    }

                    if (this.ExistingData.length > 0) {
                        let riskassessmentdonemasterid: number;
                        riskassessmentdonemasterid = this.RiskAssessmentDoneOptions[0].masterId;
                        const riskassessmentdonevalue = this.ExistingData.filter(x => x.masterId == riskassessmentdonemasterid).map(t => {
                            return t.itemId;
                        });
                        if (riskassessmentdonevalue) {
                            this.PrepMonthlyRefillFormGroup.controls.RiskAssessmentDone.setValue(riskassessmentdonevalue[0]);
                        }

                        let sexpartnermasterid: number;
                        sexpartnermasterid = this.sexualPartnerHivStatusOptions[0].masterId;
                        console.log(sexpartnermasterid);

                        const sexualstatus = this.ExistingData.filter(x => x.masterId == sexpartnermasterid).map(o => {
                            return o.itemId;
                        });
                        console.log(sexualstatus);
                        if (sexualstatus) {
                            this.PrepMonthlyRefillFormGroup.controls.sexualPartnerHivStatus.setValue(sexualstatus);
                        }
                        let clientassessmasterid: number;
                        clientassessmasterid = this.clientsBehaviourRiskOptions[0].masterId;
                        const clientassessment = this.ExistingData.filter(x => x.masterId == clientassessmasterid).map(o => {
                            return o.itemId;
                        });
                        if (clientassessment) {
                            this.PrepMonthlyRefillFormGroup.controls.clientsBehaviourRisks.setValue(clientassessment);
                        }

                        let adherencemasterid: number;
                        adherencemasterid = this.PrepAdherenceOptions[0].masterId;
                        const adherenceassessment = this.ExistingData.filter(x => x.masterId == adherencemasterid).map(o => {
                            return o.itemId;
                        });
                        console.log(adherenceassessment);
                        if (adherenceassessment) {
                            this.PrepMonthlyRefillFormGroup.controls.adherenceassessment.setValue(adherenceassessment[0]);
                        }
                        let adherencereasonmasterid: number;
                        adherencereasonmasterid = this.AdherenceAssessmentReasonOptions[0].masterId;
                        const adherencereasonlist = this.ExistingData.filter(x => x.masterId == adherencereasonmasterid).map(o => {
                            return o.itemId;
                        });
                        if (adherencereasonlist) {
                            this.PrepMonthlyRefillFormGroup.controls.adherenceassessmentreasons.setValue(adherencereasonlist);
                        }

                        let otheradherence: number;
                        otheradherence = this.AdherenceAssessmentReasonOptions.findIndex(x => x.itemDisplayName === 'Other');
                        let itemotherid: number;
                        itemotherid = this.AdherenceAssessmentReasonOptions[otheradherence].itemId;


                        const adherencereasonspecify = this.ExistingData.filter(x => x.masterId == adherencereasonmasterid
                            && x.itemId == itemotherid).map(o => {
                                return o.comment;
                            });



                        if (adherencereasonspecify) {
                            this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue(adherencereasonspecify[0]);
                        }

                        let adherencecounsellingmasterid: number;
                        adherencecounsellingmasterid = this.AdherenceCounsellingOptions[0].masterId;
                        const adherencecounselling = this.ExistingData.filter(x => x.masterId == adherencecounsellingmasterid).map(o => {
                            return o.itemId;
                        });

                        if (adherencecounselling) {
                            this.PrepMonthlyRefillFormGroup.controls.adherencecounselling.setValue(adherencecounselling[0]);
                        }

                        let prepstatusmasterid: number;
                        prepstatusmasterid = this.RefillPrepStatusOptions[0].masterId;
                        const prepstatusitem = this.ExistingData.filter(x => x.masterId == prepstatusmasterid).map(o => {
                            return o.itemId;
                        });
                        console.log(prepstatusitem);
                        if (prepstatusitem) {
                            this.PrepMonthlyRefillFormGroup.controls.prepstatus.setValue(prepstatusitem[0]);
                        }

                        let prepdiscontinuemasterid: number;
                        prepdiscontinuemasterid = this.PrepDiscontinueReasonOptions[0].masterId;
                        const prepdiscontinueitem = this.ExistingData.filter(x => x.masterId == prepdiscontinuemasterid).map(o => {
                            return o.itemId;
                        });
                        if (prepdiscontinueitem) {
                            this.PrepMonthlyRefillFormGroup.controls.prepdiscontinuereason.setValue(prepdiscontinueitem[0]);
                        }



                        let prepdiscontinuereason: number;
                        prepdiscontinuereason = this.PrepDiscontinueReasonOptions.findIndex(x => x.itemDisplayName === 'Other');
                        let prepdiscontinueotherid: number;
                        prepdiscontinueotherid = this.PrepDiscontinueReasonOptions[prepdiscontinuereason].itemId;

                        const prepdiscontinuespecify = this.ExistingData.filter(x => x.masterId == prepdiscontinuemasterid
                            && x.itemId == prepdiscontinueotherid).map(o => {
                                return o.comment;
                            });

                        if (prepdiscontinuespecify) {
                            this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue(prepdiscontinuespecify[0]);
                        }

                        let appointgivenmasterid: number;
                        appointgivenmasterid = this.AppointmentGivenOptions[0].masterId;
                        const appointgivenitem = this.ExistingData.filter(x => x.masterId == appointgivenmasterid).map(o => {
                            return o.itemId;
                        });

                        if (appointgivenitem) {
                            this.PrepMonthlyRefillFormGroup.controls.nextAppointmentGiven.setValue(appointgivenitem[0]);
                        }
                    }


                }, (error) => {
                    console.log(error);
                });
        }

    }

    OnSexualPartnerSelection(event) {

        const value = event.source.value;



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
                    && (event.source._parent.options._results[i].viewValue !== 'Not Applicable')
                ) {


                    event.source._parent.options._results[i].disabled = false;
                }
                // console.log(event.source._parent.options._results[i].value);
            }
        }


    }
    OnSelectRiskAssessment(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {

                index = this.RiskAssessmentDoneOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.RiskAssessmentDoneOptions[index].itemName;
                    if (text.toLowerCase() == 'yes') {
                        this.RiskAssessmentVisible = true;
                    }
                    else {
                        this.RiskAssessmentVisible = false;
                        this.PrepMonthlyRefillFormGroup.controls.clientsBehaviourRisks.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.sexualPartnerHivStatus.setValue('');

                    }
                }
            }

        }
    }
    OnAdherenceAssessmentSelected(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {
                index = this.PrepAdherenceOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.PrepAdherenceOptions[index].itemName;
                    if (text.toLowerCase() == 'fair') {
                        this.PrepMonthlyRefillFormGroup.controls.adherenceassessmentreasons.enable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');

                    } else if (text.toLowerCase() == 'bad') {
                        this.PrepMonthlyRefillFormGroup.controls.adherenceassessmentreasons.enable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');

                    } else {
                        this.PrepMonthlyRefillFormGroup.controls.adherenceassessmentreasons.disable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.disable({ onlySelf: true });
                    }

                }
            }
        }

    }
    OnPrepSpecifyReason(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {
                index = this.PrepDiscontinueReasonOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.PrepDiscontinueReasonOptions[index].itemName;
                    if (text.toLowerCase() == 'other') {
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.enable({ onlySelf: true });

                    } else {
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.disable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue('');
                    }

                }
            }
        }

    }
    OnPrepStatusSelected(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {
                index = this.RefillPrepStatusOptions.findIndex(x => x.itemId == val);
                if (index >= 0) {
                    text = this.RefillPrepStatusOptions[index].itemName;
                    if (text.toLowerCase() == 'discontinue') {
                        this.PrepMonthlyRefillFormGroup.controls.prepdiscontinuereason.enable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.disable({ onlySelf: true });
                        this.Visible = false;

                    } else if (text.toLowerCase() == 'continue') {
                        this.Visible = true;
                        this.PrepMonthlyRefillFormGroup.controls.prepdiscontinuereason.disable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.disable({ onlySelf: true });
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyDiscontinueReason.setValue('');
                    }

                }
            }
        }
    }

    OnAdherenceReasonsSelected(event) {
        let val: string;
        let index: number;
        let text: string;
        val = event.source.viewValue;
        if (event.source.selected == true) {
            if (val != undefined) {
                index = this.AdherenceAssessmentReasonOptions.findIndex(x => x.itemDisplayName == val);
                if (index >= 0) {
                    text = this.AdherenceAssessmentReasonOptions[index].itemName;
                    if (text.toLowerCase() == 'other') {


                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.enable({ onlySelf: true });

                    } else {

                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.disable({ onlySelf: true });
                    }

                }
            }
        } else if (event.source.selected == false) {
            if (val != undefined) {
                index = this.AdherenceAssessmentReasonOptions.findIndex(x => x.itemDisplayName == val);
                if (index >= 0) {
                    text = this.AdherenceAssessmentReasonOptions[index].itemName;
                    if (text.toLowerCase() == 'other') {

                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.setValue('');
                        this.PrepMonthlyRefillFormGroup.controls.SpecifyAssessmentReason.disable({ onlySelf: true });
                    }

                }

            }

        }

    }
    onPharmacyClick() {

        this.zone.run(() => {
            this.router.navigate(['/pharm/' + this.patientId + '/' + this.personId],
                { relativeTo: this.route });
        });
        //   const { visitDate } = this.PrepMonthlyRefillFormGroup.value;

        /*  if (visitDate != null && visitDate != undefined) {
              if (this.patientMasterVisitId > 0) {
                  this.searchService.setSession(this.personId, this.patientId).subscribe((sessionres) => {
                      this.searchService.setVisitSession(this.patientMasterVisitId, 20, 261).subscribe((setVisitSession) => {
                          const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                              '/IQCare/CCC/Encounter/PharmacyPrescription.aspx';
                          const win = window.open(url, '_blank');
                          win.focus();
                      });
                  });
  
              } else {
  
                  const patientencounter: PatientMasterVisitEncounter = {
                      PatientId: this.patientId,
                      EncounterType: this.EncounterTypeId,
                      ServiceAreaId: this.serviceAreaId,
                      UserId: this.UserId,
                      EncounterDate: moment(visitDate).toDate()
                  };
  
                  if (this.patientMasterVisitId <= 0 || this.patientMasterVisitId == undefined) {
  
  
                      this.encounterservice.savePatientMasterVisit(patientencounter).subscribe(
                          (result) => {
                              localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                              localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);
  
                              this.patientMasterVisitId = result['patientMasterVisitId'];
                              this.searchService.setSession(this.personId, this.patientId)
                                  .subscribe((sessionres) => {
                                      this.searchService.setVisitSession(this.patientMasterVisitId, this.Age, 261)
                                          .subscribe((setVisitSession) => {
                                              const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                                                  '/IQCare/CCC/Encounter/PharmacyPrescription.aspx';
                                              const win = window.open(url, '_blank');
                                              win.focus();
                                          });
                                  });
                          }, (error) => {
                              this.snotifyService.error('Error Accessing the Pharmacy Link' + error, 'Pharmacy Link',
                                  this.notificationService.getConfig());
                          });
                  }
  
              }
          } else {
  
              this.snotifyService.error('Record the visitDate first', 'VisitDate',
                  this.notificationService.getConfig());
              return;
          } */
    }

    onAppointmentSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {


            // disable date 
            this.PrepMonthlyRefillFormGroup.controls.nextAppointmentDate.disable({ onlySelf: true });
            this.PrepMonthlyRefillFormGroup.controls.AppointmentReason.disable({ onlySelf: true });
            this.PrepMonthlyRefillFormGroup.controls.AppointmentReason.setValue('');
            this.PrepMonthlyRefillFormGroup.controls.nextAppointmentDate.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            // enable date 
            this.PrepMonthlyRefillFormGroup.controls.nextAppointmentDate.enable({ onlySelf: true });
            this.PrepMonthlyRefillFormGroup.controls.AppointmentReason.enable({ onlySelf: true });


        }
    }
    public Submit() {
        if (this.PrepMonthlyRefillFormGroup.valid) {
            this.Save();
        }


    }
    public Cancel() {
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/prep/prepfollowupworkflow/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                    { relativeTo: this.route });
            });
        });
    }
    public Save() {

        const {
            sexualPartnerHivStatus, RiskAssessmentDone, visitDate, clientsBehaviourRisks, adherenceassessment, adherenceassessmentreasons,
            SpecifyAssessmentReason,
            adherencecounselling,
            prepstatus,
            prepdiscontinuereason,
            SpecifyDiscontinueReason,
            nextAppointmentGiven,
            AppointmentReason,
            nextAppointmentDate,
            Remarks } = this.PrepMonthlyRefillFormGroup.value;

        if (RiskAssessmentDone) {
            let index: number;
            index = this.RiskAssessmentDoneOptions.findIndex(x => x.itemId == RiskAssessmentDone);
            if (index > -1) {
                this.details.push({
                    'ScreeningTypeId': this.RiskAssessmentDoneOptions[index].masterId,
                    'ScreeningValueId': this.RiskAssessmentDoneOptions[index].itemId,
                    'Comment': ''
                });
            }
        }

        if (sexualPartnerHivStatus) {
            sexualPartnerHivStatus.forEach(x => {

                if (x) {
                    let index: number;

                    index = this.sexualPartnerHivStatusOptions.findIndex(t => t.itemId == x);
                    if (index > -1) {


                        this.details.push({
                            'ScreeningTypeId': this.sexualPartnerHivStatusOptions[index].masterId,
                            'ScreeningValueId': this.sexualPartnerHivStatusOptions[index].itemId,
                            'Comment': ' '
                        });

                    }
                    console.log(x);
                }
            });


        }

        if (clientsBehaviourRisks) {
            clientsBehaviourRisks.forEach(x => {
                if (x) {
                    let index: number;
                    index = this.clientsBehaviourRiskOptions.findIndex(t => t.itemId == x)
                    if (index > -1) {
                        this.details.push({
                            'ScreeningTypeId': this.clientsBehaviourRiskOptions[index].masterId,
                            'ScreeningValueId': this.clientsBehaviourRiskOptions[index].itemId,
                            'Comment': ' '
                        });
                    }
                }
            });
        }

        if (adherenceassessment) {

            let index: number;
            index = this.PrepAdherenceOptions.findIndex(x => x.itemId == adherenceassessment);
            if (index > -1) {
                this.outcomelist.push({
                    'AdherenceType': this.PrepAdherenceOptions[index].masterId,
                    'Score': this.PrepAdherenceOptions[index].itemId,
                });
            }


        }
        if (adherenceassessmentreasons) {
            adherenceassessmentreasons.forEach(x => {
                if (x) {
                    let index: number;
                    let comment: string;

                    index = this.AdherenceAssessmentReasonOptions.findIndex(t => t.itemId == x);
                    if (index > -1) {

                        if (this.AdherenceAssessmentReasonOptions[index].itemDisplayName.toLowerCase() == 'other') {
                            comment = SpecifyAssessmentReason;
                        } else {
                            comment = '';
                        }
                        this.details.push({
                            'ScreeningTypeId': this.AdherenceAssessmentReasonOptions[index].masterId,
                            'ScreeningValueId': this.AdherenceAssessmentReasonOptions[index].itemId,
                            'Comment': comment
                        });

                    }

                }
            });
        }
        if (adherencecounselling) {
            let index: number;
            index = this.AdherenceCounsellingOptions.findIndex(x => x.itemId == adherencecounselling);
            if (index > -1) {
                this.details.push({
                    'ScreeningTypeId': this.AdherenceCounsellingOptions[index].masterId,
                    'ScreeningValueId': this.AdherenceCounsellingOptions[index].itemId,
                    'Comment': ''
                });
            }
        }

        if (prepstatus) {
            let index: number;
            index = this.RefillPrepStatusOptions.findIndex(x => x.itemId == prepstatus);
            if (index > -1) {
                this.details.push({
                    'ScreeningTypeId': this.RefillPrepStatusOptions[index].masterId,
                    'ScreeningValueId': this.RefillPrepStatusOptions[index].itemId,
                    'Comment': ''
                });
            }
        }

        if (prepdiscontinuereason) {


            let index: number;
            let comment: string;

            index = this.PrepDiscontinueReasonOptions.findIndex(t => t.itemId == prepdiscontinuereason);
            if (index > -1) {

                if (this.PrepDiscontinueReasonOptions[index].itemDisplayName.toLowerCase() == 'other') {
                    comment = SpecifyDiscontinueReason;
                } else {
                    comment = '';
                }
                this.details.push({
                    'ScreeningTypeId': this.PrepDiscontinueReasonOptions[index].masterId,
                    'ScreeningValueId': this.PrepDiscontinueReasonOptions[index].itemId,
                    'Comment': comment
                });

            }



        }
        this.remarklist.push({
            'remark': Remarks
        });
        if (nextAppointmentGiven) {

            let index: number;
            index = this.AppointmentGivenOptions.findIndex(x => x.itemId == nextAppointmentGiven);
            if (index > -1) {
                this.details.push({
                    'ScreeningTypeId': this.AppointmentGivenOptions[index].masterId,
                    'ScreeningValueId': this.AppointmentGivenOptions[index].itemId,
                    'Comment': ''
                });
            }

        }
        this.UserId = JSON.parse(localStorage.getItem('appUserId'));


        const patientencounter: PatientMasterVisitEncounter = {
            PatientId: this.patientId,
            EncounterType: this.EncounterTypeId,
            ServiceAreaId: this.serviceAreaId,
            UserId: this.UserId,
            EncounterDate: moment(visitDate).toDate()
        };

        if (this.patientMasterVisitId <= 0 || this.patientMasterVisitId == undefined) {

            this.spinner.show();
            this.encounterservice.savePatientMasterVisit(patientencounter).subscribe(
                (result) => {
                    localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                    localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);

                    this.patientMasterVisitId = result['patientMasterVisitId'];

                    if (nextAppointmentDate) {
                        const nextAppointmentCommand: NextAppointmentCommand = {
                            PatientId: this.patientId,
                            PatientMasterVisitId: this.patientMasterVisitId,
                            ServiceAreaId: this.serviceAreaId,
                            AppointmentDate: nextAppointmentDate
                                ? moment(nextAppointmentDate).toDate() : null,
                            Description: '',
                            StatusDate: new Date(),
                            DifferentiatedCareId: 0,
                            AppointmentReason: AppointmentReason,
                            CreatedBy: this.UserId
                        };
                        const matNextAppointment = this.prepservice.saveNextAppointment(nextAppointmentCommand).subscribe((result) => {
                            this.nextappointmentid = result['appointmentId'];
                            this.snotifyService.success('Successfully Added the Appointment '
                                + result['appointmentId'], 'Appointment Record',
                                this.notificationService.getConfig());
                        }, (error) => {
                            this.snotifyService.error('Error submitting the Appointment' + error, 'Submit Appointment  Detail',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },
                            () => {
                                this.spinner.hide();
                            }
                        );

                    }
                    this.prepservice.AddMonthlyRefill(this.patientId, this.patientMasterVisitId,
                        this.UserId, this.serviceAreaId, moment(visitDate).toDate(), this.outcomelist
                        , this.details, this.remarklist).subscribe(
                            (response) => {
                                this.snotifyService.success('Successfully submitted the form '
                                    + response['message'], 'Submit Monthly Refill Form',
                                    this.notificationService.getConfig());

                                this.zone.run(() => {
                                    this.zone.run(() => {
                                        this.router.navigate(
                                            ['/prep/prepfollowupworkflow/' + this.patientId + '/' +
                                                this.personId + '/' + this.serviceAreaId],
                                            { relativeTo: this.route });
                                    });
                                });
                            },
                            (error) => {
                                this.snotifyService.error('Error submitting the form' + error, 'Submit Montly Refill Form',
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


        } else {
            if (!this.nextappointmentid || this.nextappointmentid == null) {
                if (nextAppointmentDate) {
                    const nextAppointmentCommand: NextAppointmentCommand = {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        ServiceAreaId: this.serviceAreaId,
                        AppointmentDate: nextAppointmentDate
                            ? moment(nextAppointmentDate).toDate() : null,
                        Description: '',
                        StatusDate: new Date(),
                        DifferentiatedCareId: 0,
                        AppointmentReason: AppointmentReason,
                        CreatedBy: this.UserId
                    };
                    const matNextAppointment = this.prepservice.saveNextAppointment(nextAppointmentCommand).subscribe((result) => {
                        this.snotifyService.success('Successfully Added the Appointment '
                            + result['appointmentId'], 'Appointment Record',
                            this.notificationService.getConfig());
                    }, (error) => {
                        this.snotifyService.error('Error submitting the Appointment' + error, 'Submit Appointment  Detail',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                        () => {
                            this.spinner.hide();
                        }
                    );

                }
            } else {
                if (nextAppointmentDate) {
                    const patientAppointmentEditCommand: PatientAppointmentEditCommand = {
                        AppointmentId: this.nextappointmentid,
                        AppointmentDate: nextAppointmentDate,
                        Description: '',
                        UserId: this.UserId,
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DifferentiatedCareId: null,
                        ReasonId: this.appointmentReasonId,
                        ServiceAreaId: this.serviceAreaId,
                        StatusId: this.appointmentStatusId
                    };

                    const matUpdateAppointment = this.prepservice.updateAppointment(patientAppointmentEditCommand).subscribe((result) => {
                        this.snotifyService.success('Successfully Updated the Appointment '
                            + result['message'], 'Appointment Record',
                            this.notificationService.getConfig());
                    }, (error) => {
                        this.snotifyService.error('Error submitting the Appointment' + error, 'Submit Appointment  Detail',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                        () => {
                            this.spinner.hide();
                        }
                    );
                }
            }

            this.prepservice.AddMonthlyRefill(this.patientId, this.patientMasterVisitId,
                this.UserId, this.serviceAreaId, moment(visitDate).toDate(), this.outcomelist
                , this.details, this.remarklist).subscribe(
                    (response) => {
                        this.snotifyService.success('Successfully Edited the form '
                            + response['message'], 'Submit Monthly Refill Form',
                            this.notificationService.getConfig());

                        this.zone.run(() => {
                            this.router.navigate(['/prep/' + '/' + this.patientId + '/' + this.personId + '/'
                                + this.serviceAreaId],
                                { relativeTo: this.route });
                        });
                    },
                    (error) => {
                        this.snotifyService.error('Error submitting and Editing the form' + error, 'Submit Montly Refill Form',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );




        }

    }
}

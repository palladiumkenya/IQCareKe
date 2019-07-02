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
import { EncounterService } from '../../shared/_services/encounter.service';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';

import * as moment from 'moment';

import { LookupItemService } from './../../shared/_services/lookup-item.service';
@Component({
    selector: 'app-prep-careend',
    templateUrl: './prep-careend.component.html',
    styleUrls: ['./prep-careend.component.css'],
    providers: [
        EncounterService
    ]

})
export class PrepCareendComponent implements OnInit {
    public PrepCareEndFormGroup: FormGroup;
    patientmastervisitid: number;
    maxDate: Date;
    personId: number;
    serviceAreaId: number;
    patientId: number;
    Isedit: boolean = false;
    UserId: number;
    patientMasterVisitId: number = 0;
    EncounterTypeId: number;
    careendreasonarray: LookupItemView[] = [];
    encountertypeoptions: LookupItemView[] = [];
    encounterlist: LookupItemView[] = [];
    PatientCareEndList: any[] = [];

    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private encounterservice: EncounterService,
        private prepservice: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService


    ) {

        this.maxDate = new Date();
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const {
                patientId, patientMasterVisitId, serviceId, edit
            } = params;
            this.patientId = params.patientId;

            this.patientMasterVisitId = params.patientMasterVisitId;
            this.serviceAreaId = params.serviceId;
            this.Isedit = edit;



        });

        this.route.data.subscribe((res) => {
            const { careendreasonoptions, EncounterTypeArray } = res;

            this.careendreasonarray = careendreasonoptions['lookupItems'];
            this.encountertypeoptions = EncounterTypeArray['lookupItems'];
        });

        this.PrepCareEndFormGroup = this._formBuilder.group({
            careEndedDate: new FormControl('', [Validators.required]),
            discontinueReason: new FormControl('', [Validators.required]),
            Specify: new FormControl(''),
            DeathDate: new FormControl('')


        });
        this.encounterlist = this.encountertypeoptions.filter(x => x.itemName === 'CareEnded');
        this.EncounterTypeId = this.encounterlist[0]['itemId'];
        console.log(this.EncounterTypeId);

        this.PrepCareEndFormGroup.controls.Specify.disable({ onlySelf: true });
        this.PrepCareEndFormGroup.controls.DeathDate.disable({ onlySelf: true });
        this.PrepCareEndFormGroup.controls.DeathDate.setValue('');
        this.LoadDetails();


    }
    LoadDetails() {

        if (this.patientMasterVisitId > 0) {
            this.prepservice.getPatientCareEndDetails(this.patientMasterVisitId).subscribe((res) => {
                this.PatientCareEndList = res;
               

                
                if (this.PatientCareEndList != undefined) {
                    this.PrepCareEndFormGroup.controls.careEndedDate.setValue(this.PatientCareEndList['exitDate']);
                    this.PrepCareEndFormGroup.controls.DeathDate.setValue(this.PatientCareEndList['dateOfDeath']);
                    this.PrepCareEndFormGroup.controls.Specify.setValue(this.PatientCareEndList['careEndingNotes']);
                    this.PrepCareEndFormGroup.controls.discontinueReason.setValue(this.PatientCareEndList['exitReason'])
                }
               


            });
        }
    }
    OnSelectDiscontinue(event) {
        let val: number;
        let index: number;
        let text: string;
        val = event.source.value;
        if (event.source.selected == true) {
            if (val != undefined) {
                index = this.careendreasonarray.findIndex(x => x.itemId == val);
            }
            if (index >= 0) {
                text = this.careendreasonarray[index].itemName;

                if (text.toLowerCase() === 'other') {
                    this.PrepCareEndFormGroup.controls.Specify.enable({ onlySelf: true });
                    this.PrepCareEndFormGroup.controls.DeathDate.disable({ onlySelf: true });
                    this.PrepCareEndFormGroup.controls.DeathDate.setValue('');
                } else if (text.toLowerCase() === 'death') {
                    this.PrepCareEndFormGroup.controls.DeathDate.enable({ onlySelf: true });
                    this.PrepCareEndFormGroup.controls.Specify.setValue('');
                    this.PrepCareEndFormGroup.controls.Specify.disable({ onlySelf: true });

                } else {
                    this.PrepCareEndFormGroup.controls.Specify.setValue('');
                    this.PrepCareEndFormGroup.controls.Specify.disable({ onlySelf: true });
                    this.PrepCareEndFormGroup.controls.DeathDate.disable({ onlySelf: true });
                    this.PrepCareEndFormGroup.controls.DeathDate.setValue('');

                }
            }
        }
    }
    Submit() {
        console.log(this.PrepCareEndFormGroup);
        if (this.PrepCareEndFormGroup.valid == true) {
            this.Save();
        }

    }
    Save() {
        this.spinner.show();
        let CareEndReason: number;
        let Specify: string;
        let CareEndDate: string;
        let DeathDate: string;

        CareEndDate = this.PrepCareEndFormGroup.controls.careEndedDate.value;
        Specify = this.PrepCareEndFormGroup.controls.Specify.value;
        CareEndReason = this.PrepCareEndFormGroup.controls.discontinueReason.value;
        DeathDate = this.PrepCareEndFormGroup.controls.DeathDate.value;
        console.log(CareEndDate);
        console.log(Specify);
        console.log(CareEndReason);
        this.UserId = JSON.parse(localStorage.getItem('appUserId'));
        if (this.patientMasterVisitId <= 0) {
            const patientencounter: PatientMasterVisitEncounter = {
                PatientId: this.patientId,
                EncounterType: this.EncounterTypeId,
                ServiceAreaId: this.serviceAreaId,
                UserId: this.UserId,
                EncounterDate: moment(new Date()).toDate()
            };

            this.encounterservice.savePatientMasterVisit(patientencounter).subscribe((result) => {
                localStorage.setItem('patientEncounterId', result['patientEncounterId']);
                localStorage.setItem('patientMasterVisitId', result['patientMasterVisitId']);

                this.patientmastervisitid = result['patientMasterVisitId'];

                this.prepservice.careEndPatientdetails(this.patientId, this.serviceAreaId,
                    this.patientmastervisitid, CareEndDate, Specify, CareEndReason, DeathDate, this.UserId).subscribe((response) => {


                        this.snotifyService.success('Successfully careended the patient' + response['message'], 'Patient Termination Form',
                            this.notificationService.getConfig());




                    },
                        (error) => {
                            this.snotifyService.error('Error CareEnding the Patient' + error, 'Patient Termination Form',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },
                        () => {
                            this.spinner.hide();
                        }
                    );
            },

                (error) => {
                    this.snotifyService.error('Error creating patient care termination encounter ' + error, 'Patient Care Termination Form'
                        , this.notificationService.getConfig());
                    this.spinner.hide();
                },
                () => {
                    this.spinner.hide();
                }
            );

        } else {
            this.prepservice.careEndPatientdetails(this.patientId, this.serviceAreaId,
                this.patientMasterVisitId, CareEndDate, Specify, CareEndReason, DeathDate, this.UserId).subscribe((response) => {


                    this.snotifyService.success('Successfully careended the patient' + response['message'], 'Patient Termination Form',
                        this.notificationService.getConfig());




                },
                    (error) => {
                        this.snotifyService.error('Error CareEnding the Patient' + error, 'Patient Termination Form',
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
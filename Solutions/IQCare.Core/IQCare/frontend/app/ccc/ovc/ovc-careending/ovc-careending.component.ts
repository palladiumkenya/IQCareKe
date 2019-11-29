
import { Component, EventEmitter, OnInit, Output, Input, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { select } from '@ngrx/store';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoadedRouterConfig } from '@angular/router/src/config';
import { registerLocaleData } from '@angular/common';
import { OvcService } from '../../_services/ovc.service';
import { EncounterService } from '../../../shared/_services/encounter.service';
import { PatientMasterVisitEncounter } from '../../../pmtct/_models/PatientMasterVisitEncounter';
import { Subscription } from 'rxjs';
import { PersonView } from '../../../dashboard/_model/personView';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import * as moment from 'moment';

import { LookupItemService } from './../../../shared/_services/lookup-item.service';
import { tap } from 'rxjs/operators';

@Component({
    selector: 'app-ovc-careending',
    templateUrl: './ovc-careending.component.html',
    styleUrls: ['./ovc-careending.component.css']
})
export class OvcCareendingComponent implements OnInit {

    public OvcCareEndFormGroup: FormGroup;
    patientmastervisitid: number;
    maxDate: Date;
    minDate: Date;
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
    public person: PersonView;
    public personView$: Subscription;
    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _lookupItemService: LookupItemService,
        private _formBuilder: FormBuilder,
        private spinner: NgxSpinnerService,
        private encounterservice: EncounterService,
        private ovcservice: OvcService,
        private snotifyService: SnotifyService,
        private personHomeService: PersonHomeService,
        private notificationService: NotificationService) {

        this.maxDate = new Date();
    }

    async ngOnInit() {

        this.route.params.subscribe(params => {
            const {
                patientId, personId, patientMasterVisitId, serviceId, edit
            } = params;
            this.patientId = params.patientId;
            this.personId = params.personId;

            this.patientMasterVisitId = params.patientMasterVisitId;
            this.serviceAreaId = params.serviceId;
            this.Isedit = edit;




        });

        this.OvcCareEndFormGroup = this._formBuilder.group({
            careEndedDate: new FormControl('', [Validators.required]),
            discontinueReason: new FormControl('', [Validators.required]),
           


        });
        this.careendreasonarray = await this._lookupItemService.getByGroupName('PrepCareEnd').pipe(tap
            ((res) => res['lookupItems'])).toPromise();
        this.encountertypeoptions = await this._lookupItemService.getByGroupName('EncounterType').pipe(
            tap((res) => res['LookupItems'])).toPromise();


        if (this.encountertypeoptions.length > 0) {
            this.encounterlist = this.encountertypeoptions.filter(x => x.itemName === 'CareEnded');
            this.EncounterTypeId = this.encounterlist[0]['itemId'];
            console.log(this.EncounterTypeId);
        }
    }



    LoadDetails() {

        if (this.patientMasterVisitId > 0) {
            this.ovcservice.getPatientCareEndDetails(this.patientMasterVisitId).subscribe((res) => {
                this.PatientCareEndList = res;



                if (this.PatientCareEndList != undefined) {
                    this.OvcCareEndFormGroup.controls.careEndedDate.setValue(this.PatientCareEndList['exitDate']);
                  
                    this.OvcCareEndFormGroup.controls.discontinueReason.setValue(this.PatientCareEndList['exitReason'])
                }


              
            });
        }
    }



    Submit() {
        console.log(this.OvcCareEndFormGroup);
        if (this.OvcCareEndFormGroup.valid == true) {
            this.Save();
        }

    }

    Cancel() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/ovcFormList/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                { relativeTo: this.route });
        });

    }

    Save() {
        this.spinner.show();
        let CareEndReason: number;
    
        let CareEndDate: string;
      



        
        CareEndDate = this.OvcCareEndFormGroup.controls.careEndedDate.value;
        CareEndReason = this.OvcCareEndFormGroup.controls.discontinueReason.value;


        this.UserId = JSON.parse(localStorage.getItem('appUserId'));
        if (this.patientMasterVisitId <= 0   || this.patientMasterVisitId == undefined) {
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
                console.log(result);
                console.log(this.patientmastervisitid);

                this.ovcservice.careEndPatientdetails(this.patientId, this.serviceAreaId,
                    this.patientmastervisitid, CareEndDate, '' , CareEndReason, '', this.UserId).subscribe((response) => {


                        this.snotifyService.success('Successfully terminated the OVC patient' + response['message'], 'OVC Termination Form',
                            this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(
                                ['/dashboard/personhome/' + this.personId],
                                { relativeTo: this.route });
                        });



                    },
                        (error) => {
                            this.snotifyService.error('Error Terminating the Patient' + error, 'OVC Termination Form',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },
                        () => {
                            this.spinner.hide();
                        }
                    );
            },

                (error) => {
                    this.snotifyService.error('Error creating patient OVC termination encounter ' + error, 'OVC Termination Form'
                        , this.notificationService.getConfig());
                    this.spinner.hide();
                },
                () => {
                    this.spinner.hide();
                }
            );

        } else {
            this.ovcservice.careEndPatientdetails(this.patientId, this.serviceAreaId,
                this.patientMasterVisitId, CareEndDate, '', CareEndReason,null , this.UserId).subscribe((response) => {


                    this.snotifyService.success('Successfully terminated ovc patient' + response['message'], 'OVC Termination Form',
                        this.notificationService.getConfig());

                    this.zone.run(() => {
                        this.router.navigate(
                            ['/dashboard/personhome/' + this.personId],
                            { relativeTo: this.route });
                    });


                },
                    (error) => {
                        this.snotifyService.error('Error terminating the OVC Patient' + error, 'OVC Termination Form',
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

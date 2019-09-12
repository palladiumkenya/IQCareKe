import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { SnotifyService } from 'ng-snotify';

import { Subscription } from 'rxjs/index';
import { ReferralsEmitter } from '../../emitters/ReferralsEmitter';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { AncService } from '../../_services/anc.service';


@Component({
    selector: 'app-referrals',
    templateUrl: './referrals.component.html',
    styleUrls: ['./referrals.component.css']
})
export class ReferralsComponent implements OnInit {
    public ReferralFormGroup: FormGroup;
    public LookupItems$: Subscription;
    public referralOptions: any[] = [];
    public yesnoOptions: any[] = [];
    @Output() nextStep = new EventEmitter<ReferralsEmitter>();
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Input() referral: ReferralsEmitter;
    @Input() referralFormOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input() visitDate: Date;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    public referralData: ReferralsEmitter;

    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private ancService: AncService) { }

    ngOnInit() {
        this.ReferralFormGroup = this._formBuilder.group({
            referredFrom: ['', Validators.required],
            referredTo: ['', Validators.required],
            nextAppointmentDate: ['', Validators.required],
            scheduledAppointment: ['', Validators.required],
            serviceRemarks: ['', []],
            referralid: new FormControl(''),
            appointmentid: new FormControl('')
        });

        const {
            referralOptions,
            yesNoOptions
        } = this.referralFormOptions[0];
        this.referralOptions = referralOptions;
        this.yesnoOptions = yesNoOptions;

        this.notify.emit(this.ReferralFormGroup);

        if (this.isEdit) {
            this.getPatientAppointment(this.PatientId, this.PatientMasterVisitId);
            this.getPatientReferral(this.PatientId, this.PatientMasterVisitId);
        }
    }

    public getPatientAppointment(patientId: number, patientMasterVisitid: number) {
        this.LookupItems$ = this.ancService.getPatientAppointmentAnc(patientId, patientMasterVisitid)
            .subscribe(
                p => {
                    const appointment = p;
                    if (appointment.length > 0) {
                        const ancAppoinment = appointment.filter(obj => obj.description != 'ANC Preventive Services Schedule');
                        if (ancAppoinment.length > 0) {
                            const yesno = this.yesnoOptions.filter(x => x.itemName == 'Yes');
                            this.ReferralFormGroup.get('nextAppointmentDate').setValue(ancAppoinment[0]['appointmentDate']);
                            this.ReferralFormGroup.get('scheduledAppointment').setValue(yesno[0]['itemId']);
                            this.ReferralFormGroup.get('serviceRemarks').setValue(ancAppoinment[0]['description']);
                        } else {
                            const yesno = this.yesnoOptions.filter(x => x.itemName == 'No');
                            this.ReferralFormGroup.get('scheduledAppointment').setValue(yesno[0]['itemId']);
                        }                    
                    } else {
                        const yesno = this.yesnoOptions.filter(x => x.itemName == 'No');
                        this.ReferralFormGroup.get('scheduledAppointment').setValue(yesno[0]['itemId']);
                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching patient appointment' + err, 'ANC',
                        this.notificationService.getConfig());
                },
                () => {
                });
    }

    public getPatientReferral(patientId: number, patientMasterVisitid: number) {
        this.LookupItems$ = this.ancService.getPatientReferral(patientId, patientMasterVisitid)
            .subscribe(
                p => {
                    const referral = p;
                    if (referral) {
                        this.ReferralFormGroup.get('referralid').setValue(referral['id']);
                        this.ReferralFormGroup.get('referredFrom').setValue(referral['referredFrom']);
                        this.ReferralFormGroup.get('referredTo').setValue(referral['referredTo']);
                    }


                },
                (err) => {
                    this.snotifyService.error('Error fetching patient Referral' + err, 'ANC',
                        this.notificationService.getConfig());
                },
                () => {
                });
    }

    public onScheduleAppointmentChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.ReferralFormGroup.controls['serviceRemarks'].enable({ onlySelf: true });
            this.ReferralFormGroup.controls['nextAppointmentDate'].enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue != 'Yes') {
            this.ReferralFormGroup.controls['serviceRemarks'].setValue('');
            this.ReferralFormGroup.controls['serviceRemarks'].disable({ onlySelf: true });
            this.ReferralFormGroup.controls['nextAppointmentDate'].disable({ onlySelf: true });
        }
    }

}

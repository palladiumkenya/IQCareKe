import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

import {SnotifyService} from 'ng-snotify';

import {Subscription} from 'rxjs/index';
import {ReferralsEmitter} from '../../emitters/ReferralsEmitter';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {NotificationService} from '../../../shared/_services/notification.service';
import {AncService} from '../../_services/anc.service';


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
    @Output() nextStep = new EventEmitter <ReferralsEmitter> ();
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Input() referral: ReferralsEmitter;
    @Input() referralFormOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    public referralData: ReferralsEmitter;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService,
              private ancService: AncService ) { }

  ngOnInit() {
      this.ReferralFormGroup = this._formBuilder.group({
          referredFrom: ['', Validators.required],
          referredTo: ['', Validators.required],
          nextAppointmentDate: ['', Validators.required],
          scheduledAppointment: ['', Validators.required],
          serviceRemarks: ['', Validators.required]
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

    public  getPatientAppointment(patientId: number, patientMasterVisitid: number) {
        this.LookupItems$ = this.ancService.getPatientAppointment(patientId, patientMasterVisitid)
            .subscribe(
                p => {
                        const appointment = p;
                        console.log('appointment details');
                        console.log(appointment);
                        if (appointment) {
                            const yesno = this.yesnoOptions.filter(x => x.itemName == 'Yes');
                            this.ReferralFormGroup.get('nextAppointmentDate').setValue(appointment['appointmentDate']);
                            this.ReferralFormGroup.get('scheduledAppointment').setValue(yesno[0]['itemId']);
                            this.ReferralFormGroup.get('serviceRemarks').setValue(appointment['description']);
                        }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching patient appointment' + err, 'ANC',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.LookupItems$);
                });
    }

    public  getPatientReferral(patientId: number, patientMasterVisitid: number) {
        this.LookupItems$ = this.ancService.getPatientReferral(patientId, patientMasterVisitid)
            .subscribe(
                p => {
                    const referral = p;
                    console.log('referral details');
                    console.log(referral);
                    if (referral) {
                        this.ReferralFormGroup.get('referredFrom').setValue(referral['referredFrom']);
                        this.ReferralFormGroup.get('referredTo').setValue(referral['referredTo']);
                    }


                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching patient Referral' + err, 'ANC',
                        this.notificationService.getConfig());
                },
                () => {
                    console.log(this.LookupItems$);
                });
    }

    public moveNextStep() {
        console.log(this.ReferralFormGroup.value);

        this.referralData = {
            referredFrom: parseInt(this.ReferralFormGroup.controls['referredFrom'].value, 10),
            referredTo: parseInt(this.ReferralFormGroup.controls['referredTo'].value, 10 ),
            nextAppointmentDate: this.ReferralFormGroup.controls['nextAppointmentDate'].value,
            scheduledAppointment: parseInt(this.ReferralFormGroup.controls['scheduledAppointment'].value, 10),
            serviceRemarks: this.ReferralFormGroup.controls['serviceRemarks'].value,


        };
        console.log(this.referralData);
        this.nextStep.emit(this.referralData);
    }

    public onScheduleAppointmentChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.ReferralFormGroup.controls['serviceRemarks'].enable({ onlySelf: true });
            this.ReferralFormGroup.controls['nextAppointmentDate'].enable({ onlySelf: true });
        } else {
            this.ReferralFormGroup.controls['serviceRemarks'].setValue('');
            this.ReferralFormGroup.controls['serviceRemarks'].disable({ onlySelf: true });
            this.ReferralFormGroup.controls['nextAppointmentDate'].disable({ onlySelf: true });
        }
    }

}

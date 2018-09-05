import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';
import {ReferralsEmitter} from '../emitters/ReferralsEmitter';

@Component({
  selector: 'app-referrals',
  templateUrl: './referrals.component.html',
  styleUrls: ['./referrals.component.css']
})
export class ReferralsComponent implements OnInit {
  public ReferralFormGroup: FormGroup;
  public LookupItems$: Subscription;
  public referrals: any[] = [];
  public yesnos: any[] = [];
    @Output() nextStep = new EventEmitter <ReferralsEmitter> ();
    @Input() referral: ReferralsEmitter;
    public referralData: ReferralsEmitter;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.ReferralFormGroup = this._formBuilder.group({
          referredFrom: ['', Validators.required],
          referredTo: ['', Validators.required],
          nextAppointmentDate: ['', Validators.required],
          scheduledAppointment: ['', Validators.required],
          serviceRemarks: ['', Validators.required]
      });

      this.getLookupOptions('pmtctReferrals', this.referrals);
      this.getLookupOptions('YesNo', this.yesnos);
  }

    public  getLookupOptions(groupName: string, masterName: any[]) {
        this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const lookupOptions =  p['lookupItems'];
                    for (let i = 0; i < lookupOptions.length; i++) {
                        masterName.push({'itemId': lookupOptions[i]['itemId'], 'itemName': lookupOptions[i]['itemName']});
                    }
                    console.log(this.referrals);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
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
            serviceRemarks: this.ReferralFormGroup.controls['serviceRemarks'].value
        };
        console.log(this.referralData);
        this.nextStep.emit(this.referralData);
    }

}

import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';

@Component({
  selector: 'app-next-appointment',
  templateUrl: './next-appointment.component.html',
  styleUrls: ['./next-appointment.component.css']
})
export class NextAppointmentComponent implements OnInit {

  public NextAppointmentFormGroup: FormGroup;
  public LookupItems$: Subscription;
  public yesnos: any[] = [];


  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.NextAppointmentFormGroup = this._formBuilder.group({
          scheduledAppointment: ['', Validators.required],
          nextAppointmentDate: ['', Validators.required],
          serviceRemarks: ['', Validators.required],
      });

      this.getLookupOptions('YesNo',this.yesnos);
  }

    public  getLookupOptions(groupName: string, masterName: any[]) {
        this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const lookupOptions =  p['lookupItems'];
                    for (let i = 0; i < lookupOptions.length; i++) {
                        masterName.push({'itemId': lookupOptions[i]['itemId'], 'itemName': lookupOptions[i]['itemName']});
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.LookupItems$);
                });
    }


}

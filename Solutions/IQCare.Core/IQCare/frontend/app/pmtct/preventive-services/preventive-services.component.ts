import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-preventive-services',
  templateUrl: './preventive-services.component.html',
  styleUrls: ['./preventive-services.component.css']
})
export class PreventiveServicesComponent implements OnInit {
  public PreventiveServicesFormGroup: FormGroup;
    lookupItemView$: Subscription;
    services: any[] = [];
    public yesnos: any[] = [];


  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
    this.PreventiveServicesFormGroup = this._formBuilder.group({
        preventiveServices: ['', Validators.required],
        dateGiven: ['', Validators.required],
        comments: ['', Validators.required],
        nextSchedule: ['', Validators.required],
        insecticideTreatedNet: ['', Validators.required],
        insecticideGivenNet: ['', Validators.required],
        antenatalExercise: ['', Validators.required],
        insecticideGivenDate: ['', Validators.required],
    });
    this.getLookupItems('PreventiveService', this.services);
     this.getLookupItems('YesNo', this.yesnos);
  }

    public getLookupItems(groupName: string , _options: any[]) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

}

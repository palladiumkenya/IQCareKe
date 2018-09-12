import { Component, OnInit } from '@angular/core';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';

@Component({
  selector: 'app-hei-visit-details',
  templateUrl: './hei-visit-details.component.html',
  styleUrls: ['./hei-visit-details.component.css']
})
export class HeiVisitDetailsComponent implements OnInit {

  public HeiVisitDetailsFormGroup: FormGroup;
  public lookupItems$: Subscription;
  public visitTypes: any[] = [];

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.HeiVisitDetailsFormGroup = this._formBuilder.group({
          visitType: ['', Validators.required],
          visitDate: ['', Validators.required]
      });
      this.getLookupItems('ANCVisitType', this.visitTypes);
  }

    public getLookupItems(groupName: string , _options: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
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
                    console.log(this.lookupItems$);
                });
    }

}

import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-partner-testing',
  templateUrl: './partner-testing.component.html',
  styleUrls: ['./partner-testing.component.css']
})
export class PartnerTestingComponent implements OnInit {
  public PartnerTestingFormGroup: FormGroup;
    lookupItemView$: Subscription;
    public YesNoNas: any[] = [];
    public FinalResults: any[] = [];

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService, private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.PartnerTestingFormGroup = this._formBuilder.group({
          PartnerTestingVisit: ['', Validators.required],
          finalHIVResult: ['', Validators.required]
      });

      this.getLookupItems('HIVFinalResultsPMTCT', this.FinalResults);
      this.getLookupItems('YesNoNa', this.YesNoNas);
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

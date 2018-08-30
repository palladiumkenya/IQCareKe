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
  selector: 'app-haart-prophylaxis',
  templateUrl: './haart-prophylaxis.component.html',
  styleUrls: ['./haart-prophylaxis.component.css']
})
export class HaartProphylaxisComponent implements OnInit {

  public HaartProphylaxisFormGroup: FormGroup;
    public yesnonas: any[] = [];
    public chronics: any[] = [];
    public YesNos: any[] = [];
    lookupItemView$: Subscription;
    LookupItems$: Subscription;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService, private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {

      this.HaartProphylaxisFormGroup = this._formBuilder.group({
          onArvBeforeANCVisit: ['', Validators.required],
          startedHaartANC: ['', Validators.required],
          cotrimoxazole: ['', Validators.required],
          aztFortheBaby: ['', Validators.required],
          nvpForBaby: ['', Validators.required],
          illness: ['', Validators.required],
          otherIllness: ['', Validators.required],
          onSetDate: ['', Validators.required],
          currentTreatment: ['', Validators.required],
          dose: ['', Validators.required]
      });
      this.getLookupItems('YesNoNa', this.yesnonas);
      this.getLookupItems('ChronicIllness', this.chronics);
      this.getLookupItems('YesNo', this.YesNos);
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

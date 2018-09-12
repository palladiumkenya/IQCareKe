import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-immunization-history',
    templateUrl: './immunization-history.component.html',
    styleUrls: ['./immunization-history.component.css']
})
export class ImmunizationHistoryComponent implements OnInit {

    public ImmunizationHistoryFormGroup: FormGroup;
    public lookupItems$: Subscription;

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {

        this.ImmunizationHistoryFormGroup = this._formBuilder.group({
            breastExamDone: ['', Validators.required]
        });
    }

    public getLookupOptions(groupName: string, masterName: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const lookupOptions = p['lookupItems'];
                    for (let i = 0; i < lookupOptions.length; i++) {
                        masterName.push({ 'itemId': lookupOptions[i]['itemId'], 'itemName': lookupOptions[i]['itemName'] });
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItems$);
                });
    }

}

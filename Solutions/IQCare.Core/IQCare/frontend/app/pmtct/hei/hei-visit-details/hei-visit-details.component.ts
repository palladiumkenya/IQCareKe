import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-hei-visit-details',
    templateUrl: './hei-visit-details.component.html',
    styleUrls: ['./hei-visit-details.component.css']
})
export class HeiVisitDetailsComponent implements OnInit {

    public HeiVisitDetailsFormGroup: FormGroup;
    public lookupItems$: Subscription;
    public visitTypes: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
       /* this.HeiVisitDetailsFormGroup = this._formBuilder.group({
            visitType: ['', Validators.required],
            visitDate: ['', Validators.required]
        }); */

        this.HeiVisitDetailsFormGroup = this._formBuilder.group({
            visitType: new FormControl('', [Validators.required]),
            visitDate: new FormControl('', [Validators.required]),
            cohort: new FormControl('')
        });

        this.getLookupItems('ANCVisitType', this.visitTypes);

        this.notify.emit(this.HeiVisitDetailsFormGroup);
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
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

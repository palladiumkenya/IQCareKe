import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MaternityService } from '../../_services/maternity.service';

@Component({
    selector: 'app-hei-visit-details',
    templateUrl: './hei-visit-details.component.html',
    styleUrls: ['./hei-visit-details.component.css']
})
export class HeiVisitDetailsComponent implements OnInit {
    isVisitNumberShown: boolean = false;
    isdayPostPartumShown: boolean = false;
    isCohortShown: boolean = true;
    maxDate: Date;
    visitDetails: any;

    public HeiVisitDetailsFormGroup: FormGroup;
    public lookupItems$: Subscription;
    public visitTypes: any[] = [];

    @Input('formtype') formtype: string;
    @Input('visitDate') visitDate: string;
    @Input('visitType') visitType: string;
    @Input('patientId') patientId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private maternityService: MaternityService) {
        this.maxDate = new Date();
    }

    ngOnInit() {

        this.HeiVisitDetailsFormGroup = this._formBuilder.group({
            visitType: new FormControl('', [Validators.required]),
            visitDate: new FormControl('', [Validators.required]),
            cohort: new FormControl(''),
            visitNumber: new FormControl('', [Validators.required]),
            dayPostPartum: new FormControl('', [Validators.required])
        });

        this.HeiVisitDetailsFormGroup.get('visitDate').setValue(this.visitDate);
        this.HeiVisitDetailsFormGroup.get('visitType').setValue(this.visitType['itemId']);
        this.HeiVisitDetailsFormGroup.get('visitNumber').disable({ onlySelf: true });
        this.HeiVisitDetailsFormGroup.get('dayPostPartum').disable({ onlySelf: true });

        switch (this.formtype) {
            case 'hei':
                this.getLookupItems('HEIVisitType', this.visitTypes);
                break;
            case 'maternity':
                this.getLookupItems('ANCVisitType', this.visitTypes);
                this.HeiVisitDetailsFormGroup.get('visitType').disable({ onlySelf: true });
                this.HeiVisitDetailsFormGroup.get('cohort').disable({ onlySelf: true });
                break;
            case 'pnc':
                this.getLookupItems('PNCVisitType', this.visitTypes);
                this.HeiVisitDetailsFormGroup.get('cohort').disable({ onlySelf: true });
                this.HeiVisitDetailsFormGroup.get('visitNumber').enable({ onlySelf: true });
                this.HeiVisitDetailsFormGroup.get('dayPostPartum').enable({ onlySelf: true });
                this.isVisitNumberShown = true;
                this.isdayPostPartumShown = true;
                this.isCohortShown = false;
                break;
            case 'anc':
                this.getLookupItems('ANCVisitType', this.visitTypes);
                this.isVisitNumberShown = true;
                this.HeiVisitDetailsFormGroup.get('visitNumber').enable({ onlySelf: true });
                this.isCohortShown = false;
                break;
            default:
        }

        this.getCurrentVisitDetails(this.patientId);
        this.notify.emit(this.HeiVisitDetailsFormGroup);
    }

    public getCurrentVisitDetails(patientId: number): void {
        this.visitDetails = this.maternityService.getCurrentVisitDetails(patientId)
            .subscribe(
                p => {
                    const visit = p;
                    console.log(p);
                    if (visit && visit.visitNumber > 1) {
                        const Item = this.visitTypes.filter(x => x.itemName.includes('Follow Up'));
                        this.HeiVisitDetailsFormGroup.get('visitType').patchValue(Item[0].itemId);
                        console.log('visitNumber' + visit.visitNumber );
                        if (this.formtype == 'anc') {
                            this.HeiVisitDetailsFormGroup.get('visitNumber').patchValue(visit.visitNumber );
                        }
                    } else {
                        this.HeiVisitDetailsFormGroup.get('visitNumber').patchValue(1);
                        const Item = this.visitTypes.filter(x => x.itemName.includes('Initial'));
                        // console.log(Item);
                        this.HeiVisitDetailsFormGroup.get('visitType').patchValue(Item[0].itemId);
                    }
                },
                (err) => {

                },
                () => {

                });
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    this.visitTypes = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    // console.log(this.lookupItems$);
                });
    }

}

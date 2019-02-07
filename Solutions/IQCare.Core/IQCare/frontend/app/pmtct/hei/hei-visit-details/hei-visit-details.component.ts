import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs/index';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MaternityService } from '../../_services/maternity.service';
import { HeiService } from '../../_services/hei.service';
import * as moment from 'moment';
import { MatDatepickerInputEvent } from '@angular/material';
import { AncService } from '../../_services/anc.service';

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
    @Input('isEdit') isEdit: boolean;
    @Input('visitDate') visitDate: string;
    @Input('visitType') visitType: string;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Input('personId') personId: number;
    @Input('serviceAreaId') serviceAreaId: number;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private ancService : AncService,
        private heiService: HeiService) {
        this.maxDate = new Date();
    }

    ngOnInit() {

        this.HeiVisitDetailsFormGroup = this._formBuilder.group({
            visitType: new FormControl('', [Validators.required]),
            visitDate: new FormControl('', [Validators.required]),
            cohort: new FormControl(''),
            visitNumber: new FormControl('', [Validators.min(0), Validators.max(40), Validators.required]),
            dayPostPartum: new FormControl('', [Validators.required]),
            id: new FormControl('')
        });

        if (!this.isEdit) {
            this.HeiVisitDetailsFormGroup.get('visitDate').setValue(this.visitDate);
            this.HeiVisitDetailsFormGroup.get('visitType').setValue(this.visitType['itemId']);
        }
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

        this.getCurrentVisitDetails(this.patientId, this.serviceAreaId);
        this.calculateCohort(this.personId);
        this.notify.emit(this.HeiVisitDetailsFormGroup);
    }

    public calculateCohort(personId: number) {
        this.heiService.getPersonDetails(personId).subscribe(
            (res) => {
                console.log(res);
                if (res.length > 0) {
                    const dateOfBirth = res[0]['dateOfBirth'];
                    this.HeiVisitDetailsFormGroup.get('cohort').setValue(moment(dateOfBirth).format('MMM-YYYY'));
                }
            }
        );
    }

    public getCurrentVisitDetails(patientId: number, serviceAreaId: number): void {
        this.visitDetails = this.heiService.getPatientVisitDetails(patientId, serviceAreaId)
            .subscribe(
                p => {
                    const visit = p;
                    console.log('visit data');
                    console.log(p);

                    if (p.length) {
                        const Item = this.visitTypes.filter(x => x.itemName.includes('Follow Up'));
                        if (Item.length > 0) {
                            this.HeiVisitDetailsFormGroup.get('visitType').patchValue(Item[0].itemId);
                            console.log('visitNumber' + visit[0].visitNumber);
                        }

                        // if (this.formtype == 'anc') {
                        this.HeiVisitDetailsFormGroup.get('visitNumber').patchValue(p.length + 1);
                        const visitId = this.visitTypes.filter(x => x.itemName.includes('Initial'));
                        // }

                        if (this.isEdit) {
                            const y = p.filter(obj =>
                                obj.patientId == this.patientId && obj.patientMasterVisitId == this.patientMasterVisitId);
                                console.log(y+ '   Yyyy')
                            if (y != null) {
                                this.HeiVisitDetailsFormGroup.get('dayPostPartum').setValue(y[0].daysPostPartum);
                                this.HeiVisitDetailsFormGroup.get('visitNumber').setValue(y[0].visitNumber);
                                this.HeiVisitDetailsFormGroup.get('id').setValue(y[0].id);

                                this.HeiVisitDetailsFormGroup.get('visitDate').setValue(y[0].visitDate);
                                this.HeiVisitDetailsFormGroup.get('visitType').setValue(y[0].visitType);
                            }
                        }

                    } else {
                        this.HeiVisitDetailsFormGroup.get('visitNumber').patchValue(1);
                        const Item = this.visitTypes.filter(x => x.itemName.includes('Initial'));
                        // console.log(Item);
                        if (Item.length > 0) {
                            this.HeiVisitDetailsFormGroup.get('visitType').patchValue(Item[0].itemId);
                            this.visitType = Item[0];
                        }
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

    public vistDateChange(event: MatDatepickerInputEvent<Date>)
    {
        console.log('Changed Date '+ event.value);
        this.ancService.updateVisitDate(event.value);
    }

}

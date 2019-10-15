import { MaternityService } from './../../_services/maternity.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import * as moment from 'moment';
import {HeiService} from '../../_services/hei.service';

@Component({
    selector: 'app-pnc-maternalhistory',
    templateUrl: './pnc-maternalhistory.component.html',
    styleUrls: ['./pnc-maternalhistory.component.css']
})
export class PncMaternalhistoryComponent implements OnInit {
    deliveryModeOptions: LookupItemView[] = [];

    MaternalHistoryForm: FormGroup;
    @Input('matHistoryOptions') matHistoryOptions: any;
    @Input('patientId') patientId: number;
    @Input('isEdit') isEdit: boolean;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Input() serviceAreaId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    maxDate: Date;

    constructor(private _formBuilder: FormBuilder,
        private maternityService: MaternityService,
        private heiService: HeiService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            dateofdelivery: new FormControl('', [Validators.required]),
            modeofdelivery: new FormControl('', [Validators.required]),
            dayPostPartum: new FormControl('', [Validators.required]),
            deliveryid: new FormControl('')
        });

        this.notify.emit(this.MaternalHistoryForm);

        const { deliveryModeOptions } = this.matHistoryOptions[0];
        this.deliveryModeOptions = deliveryModeOptions;

        this.maternityService.getPatientPregnancy(this.patientId).subscribe(
            res => {
                if (res && res.id) {
                    this.maternityService.getPatientDeliveryInfoByPregnancyId(res.id).subscribe(
                        result => {
                            if (result.length > 0) {
                                this.MaternalHistoryForm.get('dateofdelivery').setValue(result[0].dateOfDelivery);
                                if (result[0].modeOfDelivery) {
                                    const delivery = this.deliveryModeOptions.filter(obj =>
                                        obj.itemName == result[0].modeOfDelivery);
                                    this.MaternalHistoryForm.get('modeofdelivery').setValue(delivery[0].itemId);
                                }
                                this.MaternalHistoryForm.get('deliveryid').setValue(result[0].id);
                                this.calculateDaysPostPartum();
                            } else {
                                this.maternityService.GetPatientDeliveryInfo(this.patientMasterVisitId).subscribe(
                                    deliveryRes => {
                                        if (deliveryRes) {
                                            if (deliveryRes.dateOfDelivery) {
                                                this.MaternalHistoryForm.get('dateofdelivery').setValue(deliveryRes.dateOfDelivery);
                                                this.MaternalHistoryForm.get('deliveryid').setValue(deliveryRes.id);
                                                this.calculateDaysPostPartum();
                                            }
                                            if (deliveryRes.modeOfDelivery) {
                                                const delivery = this.deliveryModeOptions.filter(obj =>
                                                    obj.itemName == deliveryRes.modeOfDelivery);
                                                this.MaternalHistoryForm.get('modeofdelivery').setValue(delivery[0].itemId);
                                            }
                                        }
                                    }
                                );
                            }
                        }
                    );
                }
            }
        );

        this.heiService.getPatientVisitDetails(this.patientId, this.serviceAreaId).subscribe(
            visitDetails => {
                if (visitDetails.length > 0) {
                    this.MaternalHistoryForm.get('dayPostPartum').setValue(visitDetails[0]['daysPostPartum']);
                }
            }
        );

        if (this.isEdit) {
            this.loadPncMaternalHistory();
        }
    }

    public loadPncMaternalHistory(): void {

    }

    public calculateDaysPostPartum() {
        const dateOfDelivery = this.MaternalHistoryForm.get('dateofdelivery').value;
        const a = moment(dateOfDelivery);
        const b = moment(new Date());
        const difference = b.diff(a, 'days');
        this.MaternalHistoryForm.get('dayPostPartum').setValue(difference);
    }
}

import { MaternityService } from './../../_services/maternity.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

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
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    maxDate: Date;

    constructor(private _formBuilder: FormBuilder,
        private maternityService: MaternityService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            dateofdelivery: new FormControl('', [Validators.required]),
            modeofdelivery: new FormControl('', [Validators.required])
        });

        const { deliveryModeOptions } = this.matHistoryOptions[0];
        this.deliveryModeOptions = deliveryModeOptions;

        this.maternityService.getInitialProfileDetailsByPatientd(this.patientId).subscribe(
            (res) => {
                if (res) {
                    this.maternityService.getPatientDeliveryInfoByProfileId(res.id).subscribe(
                        (result) => {
                            if (result.length > 0) {
                                this.MaternalHistoryForm.get('dateofdelivery').setValue(result[0].dateOfDelivery);
                                this.MaternalHistoryForm.get('modeofdelivery').setValue(result[0].modeOfDelivery);
                            }
                        }
                    );
                }
            }
        );

        this.notify.emit(this.MaternalHistoryForm);
    }

}

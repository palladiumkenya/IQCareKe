import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { DataService } from '../../_services/data.service';

@Component({
    selector: 'app-prior-hiv-status',
    templateUrl: './prior-hiv-status.component.html',
    styleUrls: ['./prior-hiv-status.component.css']
})
export class PriorHivStatusComponent implements OnInit {
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Input('patientEncounterId') patientEncounterId: number;
    @Input() priorHivOptions: any[];
    @Input() personId: number;
    @Input() serviceAreaId: number;

    public hivStatusOptions: LookupItemView[] = [];

    priorHivStatusFormGroup: FormGroup;

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private dataservice: DataService) { }

    ngOnInit() {
        this.priorHivStatusFormGroup = this._formBuilder.group({
            priorHivStatus: new FormControl('', [Validators.required])
        });

        const { hivStatusOptions } = this.priorHivOptions[0];
        this.hivStatusOptions = hivStatusOptions;
        // console.log(this.priorHivOptions);

        this.personCurrentHivStatus();
    }

    public personCurrentHivStatus() {
        this.pncService.getPersonCurrentHivStatus(this.personId).subscribe(
            (res) => {
                console.log(res);
                if (res.length > 0) {
                    const hivPositiveResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Positive');
                    if (hivPositiveResult.length > 0) {
                        this.priorHivStatusFormGroup.get('priorHivStatus').setValue(hivPositiveResult[0].itemId);
                        this.dataservice.changeHivStatus('Positive');
                    }
                } else {
                    const hivNegativeResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Negative');
                    if (hivNegativeResult.length > 0) {
                        this.priorHivStatusFormGroup.get('priorHivStatus').setValue(hivNegativeResult[0].itemId);
                    }
                    // this.dataservice.changeHivStatus('Negative');
                }
            },
            (error) => {
                this.snotifyService.error('Error loading previous hiv status ', 'Maternity',
                    this.notificationService.getConfig());
            }
        );
    }
}

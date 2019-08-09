import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {
    FormBuilder,
    FormGroup,
    FormControl,
    Validators
} from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { HeiService } from '../../_services/hei.service';

@Component({
    selector: 'app-hei-outcome',
    templateUrl: './hei-outcome.component.html',
    styleUrls: ['./hei-outcome.component.css']
})
export class HeiOutcomeComponent implements OnInit {
    HeiOutcomeFormGroup: FormGroup;
    @Input() heiOutcomeOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(
        private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private heiservice: HeiService
    ) {}

    ngOnInit() {
        this.HeiOutcomeFormGroup = this._formBuilder.group({
            heiOutcomeOptions: new FormControl('')
        });

        this.notify.emit(this.HeiOutcomeFormGroup);

        this.loadHeiOutcome();
    }

    loadHeiOutcome(): void {
        this.heiservice.getHeiDelivery(this.patientId).subscribe(
            result => {
                if (result.length > 0) {
                    const outCome = result[0];
                    if (outCome.outcome24MonthId) {
                        this.HeiOutcomeFormGroup.get(
                            'heiOutcomeOptions'
                        ).setValue(outCome.outcome24MonthId);
                    }
                }
            },
            error => {
                this.snotifyService.error(
                    'Error getting HeiOutcome ' + error,
                    'HEI Outcome',
                    this.notificationService.getConfig()
                );
            },
            () => {}
        );
    }
}

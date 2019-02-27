import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource } from '@angular/material';
import { PncService } from '../../_services/pnc.service';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { DataService } from '../../_services/data.service';

@Component({
    selector: 'app-mternity-tests',
    templateUrl: './maternity-tests.component.html',
    styleUrls: ['./maternity-tests.component.css']
})
export class MaternityTestsComponent implements OnInit {
    maternityTestsFormGroup: FormGroup;
    @Input() maternityTestOptions: any[] = [];
    @Input() personId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public yesnoOptions: any[] = [];
    public hivStatusOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private pncService: PncService,
        private dataservice: DataService) {
    }

    ngOnInit() {
        this.maternityTestsFormGroup = this._formBuilder.group({
            treatedSyphilis: new FormControl('', [Validators.required]),
            HIVStatusLastANC: new FormControl('', [Validators.required])
        });
        const {
            yesNos, hivStatusOptions
        } = this.maternityTestOptions[0];
        this.yesnoOptions = yesNos;
        this.hivStatusOptions = hivStatusOptions;

        this.personCurrentHivStatus();
    }

    public personCurrentHivStatus() {
        this.pncService.getPersonCurrentHivStatus(this.personId).subscribe(
            (res) => {
                if (res.length > 0) {
                    const hivPositiveResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Positive');
                    if (hivPositiveResult.length > 0) {
                        this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivPositiveResult[0].itemId);
                        this.dataservice.changeHivStatus('Positive');
                    } else {
                        const hivNegativeResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Negative');
                        if (hivNegativeResult.length > 0) {
                            this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivNegativeResult[0].itemId);
                        }
                        this.dataservice.changeHivStatus('Negative');
                    }
                }
            },
            (error) => {
                this.snotifyService.error('Error loading previous hiv status ', 'Maternity',
                    this.notificationService.getConfig());
            }
        );
    }
}

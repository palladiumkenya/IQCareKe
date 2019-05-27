import { NotificationService } from './../../../shared/_services/notification.service';
import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, Output, EventEmitter, Input, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-pnc-partnertesting',
    templateUrl: './pnc-partnertesting.component.html',
    styleUrls: ['./pnc-partnertesting.component.css']
})
export class PncPartnertestingComponent implements OnInit, AfterViewInit {
    PartnerTestingForm: FormGroup;
    yesNoNaOptions: LookupItemView[] = [];
    finalPartnerHivResultOptions: LookupItemView[] = [];

    @Input('partnerTestingOptions') partnerTestingOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.PartnerTestingForm = this._formBuilder.group({
            partnerHivTestDone: new FormControl('', [Validators.required]),
            finalPartnerHivResult: new FormControl('', [Validators.required]),
            id: new FormControl('')
        });

        const { yesNoNaOptions, finalPartnerHivResultOptions } = this.partnerTestingOptions[0];
        this.yesNoNaOptions = yesNoNaOptions;
        this.finalPartnerHivResultOptions = finalPartnerHivResultOptions;

        this.notify.emit(this.PartnerTestingForm);
    }

    ngAfterViewInit(): void {
        if (this.isEdit) {
            this.loadPncPartnerTesting();
        }
    }

    loadPncPartnerTesting(): void {
        this.pncService.getPartnerTesting(this.patientId).subscribe(
            (result) => {
                const partnerTestingVisit = result.filter(obj => obj.patientMasterVisitId == this.patientMasterVisitId);
                for (let i = 0; i < partnerTestingVisit.length; i++) {
                    this.PartnerTestingForm.get('partnerHivTestDone').setValue(result[i].partnerTested);
                    this.PartnerTestingForm.get('finalPartnerHivResult').setValue(result[i].partnerHIVResult);
                    this.PartnerTestingForm.get('id').setValue(result[i].id);
                }
            },
            (error) => {
                this.snotifyService.error('Fetching partner testing ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
    }

    public onPartnerTestingChange(event) {
        const optionNa = this.yesNoNaOptions.filter(x => x.itemName == 'N/A');
        if (event.isUserInput && event.source.selected && event.source.viewValue != 'Yes') {
            this.PartnerTestingForm.get('finalPartnerHivResult').setValue(optionNa[0].itemId);
            this.PartnerTestingForm.get('finalPartnerHivResult').disable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PartnerTestingForm.get('finalPartnerHivResult').setValue('');
            this.PartnerTestingForm.get('finalPartnerHivResult').enable({ onlySelf: true });
        }
    }
}

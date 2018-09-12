import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs/index';
import { NotificationService } from '../../shared/_services/notification.service';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { PatientEducationEmitter } from '../emitters/PatientEducationEmitter';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { HIVTestingEmitter } from '../emitters/HIVTestingEmitter';
import { VisitDetailsService } from '../_services/visit-details.service';
export interface Topic {
    value: number;
    viewValue: string;
}

@Component({
    selector: 'app-hiv-status',
    templateUrl: './hiv-status.component.html',
    styleUrls: ['./hiv-status.component.css']
})

export class HivStatusComponent implements OnInit {

    LookupItems$: Subscription;
    public testVisits: any[] = [];
    public kits: any[] = [];
    public tests: any[] = [];
    public testResults: any[] = [];
    public finalResults: any[] = [];
    public consentOption: number;
    public ancTestEntryPoint: number;

    lookupItemView$: Subscription;
    @Output() nextStep = new EventEmitter<HIVTestingEmitter>();
    @Input() hivTestingData: HIVTestingEmitter;

    HIVStatusFormGroup: FormGroup;
    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private notificationService: NotificationService, private snotifyService: SnotifyService,
        private visitDetailsService: VisitDetailsService) { }

    ngOnInit() {

        this.HIVStatusFormGroup = this._formBuilder.group({
            testingDone: [Validators.required],
            hivTest: [Validators.required],
            kitName: [Validators.required],
            testResult: [Validators.required],
            lotNumber: [Validators.required],
            expiryDate: [Validators.required],
            nextAppointmentDate: [Validators.required],
            finalResult: [Validators.required]
        });

        this.getLookupOptions('PMTCTHIVTestVisit', this.testVisits);
        this.getLookupOptions('HIVTestKits', this.kits);
        this.getLookupOptions('PMTCTHIVTests', this.tests);
        this.getLookupOptions('HIVResults', this.testResults);
        this.getLookupOptions('HIVFinalResults', this.finalResults);
        this.visitDetailsService.getConsentOptions().subscribe(
            (result) => {
                console.log(result);
                const { itemId } = result;
                this.consentOption = itemId;
            }
        );

        this.visitDetailsService.getTestEntryPointANC().subscribe(
            (result) => {
                console.log(result);
                const { itemId } = result;
                this.ancTestEntryPoint = itemId;
            }
        );
    }

    public getLookupOptions(groupName: string, masterName: any[]) {
        this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
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
                    console.log(this.lookupItemView$);
                });
    }

    public moveNextStep() {
        console.log(this.HIVStatusFormGroup.value);
        this.hivTestingData = {
            hivTest: this.HIVStatusFormGroup.controls['hivTest'].value,
            testingDone: parseInt(this.HIVStatusFormGroup.controls['testingDone'].value, 10),
            testResult: parseInt(this.HIVStatusFormGroup.controls['testResult'].value, 10),
            kitName: parseInt(this.HIVStatusFormGroup.controls['kitName'].value, 10),
            lotNumber: this.HIVStatusFormGroup.controls['lotNumber'].value,
            nextAppointmentDate: this.HIVStatusFormGroup.controls['nextAppointmentDate'].value,
            expiryDate: this.HIVStatusFormGroup.controls['expiryDate'].value,
            finalResult: this.HIVStatusFormGroup.controls['finalResult'].value,
            consentOption: this.consentOption,
            ancTestEntryPoint: this.ancTestEntryPoint
        };

        this.nextStep.emit(this.hivTestingData);
    }

}

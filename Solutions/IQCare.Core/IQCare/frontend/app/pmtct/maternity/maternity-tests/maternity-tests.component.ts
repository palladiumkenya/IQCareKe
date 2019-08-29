import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource } from '@angular/material';
import { PncService } from '../../_services/pnc.service';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { DataService } from '../../_services/data.service';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {AncService} from '../../_services/anc.service';

@Component({
    selector: 'app-mternity-tests',
    templateUrl: './maternity-tests.component.html',
    styleUrls: ['./maternity-tests.component.css']
})
export class MaternityTestsComponent implements OnInit {
    maternityTestsFormGroup: FormGroup;
    @Input() maternityTestOptions: any[] = [];
    @Input() personId: number;
    @Input() patientId: number;
    @Input() patientEncounterId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: boolean;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public yesnoOptions: any[] = [];
    public hivStatusOptions: LookupItemView[] = [];
    public syphilisResultsOptions: LookupItemView[] = [];
    public syphilisTestTypes: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private pncService: PncService,
        private dataservice: DataService,
        private lookupservice: LookupItemService,
        private ancService: AncService) {
    }

    async ngOnInit() {
        this.maternityTestsFormGroup = this._formBuilder.group({
            testedSyphilis: new FormControl('', [Validators.required]),
            SyphilisTestUsed: new FormControl('', [Validators.required]),
            SyphilisResults: new FormControl('', [Validators.required]),
            treatedSyphilis: new FormControl('', [Validators.required]),
            HIVStatusLastANC: new FormControl('', [Validators.required])
        });
        this.notify.emit(this.maternityTestsFormGroup);
        
        const {
            yesNos, hivStatusOptions
        } = this.maternityTestOptions[0];
        this.yesnoOptions = yesNos;
        this.hivStatusOptions = hivStatusOptions;

        this.personCurrentHivStatus();

        this.lookupservice.getByGroupName('SyphilisResults').subscribe(
            res => {
                this.syphilisResultsOptions = res['lookupItems'];
            },
            error => {

            }
        );
        
        this.lookupservice.getByGroupName('SyphilisTestType').subscribe(
            res => {
                this.syphilisTestTypes = res['lookupItems'];
            },
            error => {

            }
        );
        
        if (this.isEdit) {
            this.getBaselineAncProfile(this.patientId);
        }
    }

    public onTestedForSyphilisSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.maternityTestsFormGroup.get('SyphilisTestUsed').enable({ onlySelf: true });
            this.maternityTestsFormGroup.get('SyphilisResults').enable({ onlySelf: true });
            this.maternityTestsFormGroup.get('treatedSyphilis').enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.maternityTestsFormGroup.get('SyphilisTestUsed').setValue('');
            this.maternityTestsFormGroup.get('SyphilisTestUsed').disable({ onlySelf: true });

            this.maternityTestsFormGroup.get('SyphilisResults').setValue('');
            this.maternityTestsFormGroup.get('SyphilisResults').disable({ onlySelf: true });

            this.maternityTestsFormGroup.get('treatedSyphilis').setValue('');
            this.maternityTestsFormGroup.get('treatedSyphilis').disable({ onlySelf: true });
            
            
            
        }
    }

    public onSyphilisResultsSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Positive') {
            this.maternityTestsFormGroup.get('treatedSyphilis').enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Negative') {
            this.maternityTestsFormGroup.get('treatedSyphilis').setValue('');
            this.maternityTestsFormGroup.get('treatedSyphilis').disable({ onlySelf: true });

            this.maternityTestsFormGroup.get('treatedSyphilis').clearValidators();
            this.maternityTestsFormGroup.get('treatedSyphilis').updateValueAndValidity();
        }
    }

    public getBaselineAncProfile(patientId: number): void {
        this.ancService.getBaselineAncProfile(patientId).subscribe(
                p => {
                    const baseline = p;
                    console.log(baseline);
                    if (baseline['id'] > 0) {
                        this.maternityTestsFormGroup.get('treatedSyphilis').setValue(baseline['syphilisResults']);
                        this.maternityTestsFormGroup.get('SyphilisTestUsed').setValue(baseline['syphilisTestUsed']);
                        this.maternityTestsFormGroup.get('SyphilisResults').setValue(baseline['syphilisResults']);
                        this.maternityTestsFormGroup.get('testedSyphilis').setValue(baseline['testedForSyphilis']);
                    }
                },
                error1 => {

                }
            );
    }

    public async personCurrentHivStatus() {
        const previousHtsEncounters = await this.pncService.getPatientHtsEncounters(this.patientId).toPromise();
        for (let i = 0; i < previousHtsEncounters.length; i++) {
            const finalResult = previousHtsEncounters[i]['finalResult'];
            if (finalResult == 'Positive') {
                const hivPositiveResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Positive');
                this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivPositiveResult[0].itemId);
                this.dataservice.changeHivStatus('Positive');
            } else if (finalResult == 'Negative') {
                const hivNegativeResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Negative');
                if (hivNegativeResult.length > 0) {
                    this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivNegativeResult[0].itemId);
                }
                this.dataservice.changeHivStatus('Negative');
            }
        }

        if (previousHtsEncounters.length == 0) {
            const confirmedPositive = await this.pncService.getPersonCurrentHivStatus(this.personId).toPromise();
            if (confirmedPositive.length > 0) {
                const hivPositiveResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Positive');
                if (hivPositiveResult.length > 0) {
                    this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivPositiveResult[0].itemId);
                    this.dataservice.changeHivStatus('Positive');
                }
            } else {
                const hivUnknownResult = this.hivStatusOptions.filter(obj => obj.itemName == 'Unknown');
                if (hivUnknownResult.length > 0) {
                    this.maternityTestsFormGroup.get('HIVStatusLastANC').setValue(hivUnknownResult[0].itemId);
                }
            }
        }
    }
}

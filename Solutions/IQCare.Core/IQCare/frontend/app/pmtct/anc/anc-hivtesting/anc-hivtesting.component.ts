import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatDialog, MatDialogConfig, MatTableDataSource } from '@angular/material';
import { HivStatusComponent } from '../hiv-status/hiv-status.component';
import {Subscription} from 'rxjs/index';
import {AncService} from '../../_services/anc.service';

@Component({
    selector: 'app-anc-hivtesting',
    templateUrl: './anc-hivtesting.component.html',
    styleUrls: ['./anc-hivtesting.component.css']
})
export class AncHivtestingComponent implements OnInit {
    ancHivStatusInitialVisitOptions: LookupItemView[];
    yesnoOptions: LookupItemView[];
    hivFinalResultsOptions: LookupItemView[];
    baseline$: Subscription;

    isHivTestingDone: boolean = false;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    displayedColumns = ['testdate', 'testtype', 'kitname', 'lotnumber',
        'expirydate', 'testresult', 'nexthivtest', 'testpoint', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    HivTestingForm: FormGroup;
    @Input('hivTestingOptions') hivTestingOptions: any;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;

    constructor(
        private dialog: MatDialog,
        private _formBuilder: FormBuilder,
        private ancService: AncService
    ) {

    }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            hivStatusBeforeFirstVisit: new FormControl('', [Validators.required]),
            hivTestingDone: new FormControl('', [Validators.required]),
            testType: new FormControl('', [Validators.required]),
            finalTestResult: new FormControl('', [Validators.required])
        });

        this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

        const { ancHivStatusInitialVisitOptions, yesnoOptions, hivFinalResultsOptions } = this.hivTestingOptions[0];
        this.ancHivStatusInitialVisitOptions = ancHivStatusInitialVisitOptions;
        this.yesnoOptions = yesnoOptions;
        this.hivFinalResultsOptions = hivFinalResultsOptions;

        this.notify.emit({ 'form': this.HivTestingForm, 'table_data': this.hiv_testing_table_data });

        if (this.isEdit) {
            this.getBaselineAncProfile(this.PatientId);
        } else {
            this.getBaselineAncProfile(this.PatientId);
        }
    }

    AddHivTests() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(HivStatusComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                console.log(data);

                this.hiv_testing_table_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: 'PNC'
                });

                this.dataSource = new MatTableDataSource(this.hiv_testing_table_data);
            }
        );
    }

    onHivTestDoneChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.isHivTestingDone = true;
            this.HivTestingForm.controls['testType'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].enable({ onlySelf: true });
        } else if (event.source.selected) {
            this.isHivTestingDone = false;
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

            // set default value to null
            this.HivTestingForm.controls['testType'].setValue('');
            this.HivTestingForm.controls['finalTestResult'].setValue('');
        }
    }

    onHivStatusBeforeFirstVisitChange(event) {
        console.log(event);
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Known Positive') {
            this.HivTestingForm.controls['hivTestingDone'].disable({ onlySelf: true });
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected) {
            this.HivTestingForm.controls['hivTestingDone'].enable();
        }
    }

    public getBaselineAncProfile(patientId: number): void {
        this.baseline$ = this.ancService.getBaselineAncProfile(patientId)
            .subscribe(
                p => {
                    const baseline = p;

                    console.log('baseline info');
                    console.log(baseline);
                    console.log(baseline['hivStatusBeforeAnc']);
                    if (baseline['id'] > 0) {
                        this.HivTestingForm.get('hivStatusBeforeFirstVisit').setValue(baseline['hivStatusBeforeAnc']);
                    }
                }
                ,
                error1 => {

                },
                () => {

                }
            );
    }
}

export interface HivTestingTableData {
    testdate?: Date;
    testtype?: any;
    kitname?: any;
    lotnumber?: string;
    expirydate?: Date;
    testresult?: any;
    nexthivtest?: Date;
    testpoint?: string;
}

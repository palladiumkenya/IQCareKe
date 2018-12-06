import { SnotifyService } from 'ng-snotify';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HivStatusComponent } from '../../anc/hiv-status/hiv-status.component';
import { Component, OnInit, Output, EventEmitter, Input, AfterViewInit } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { PncService } from '../../_services/pnc.service';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';

@Component({
    selector: 'app-pnc-hivtesting',
    templateUrl: './pnc-hivtesting.component.html',
    styleUrls: ['./pnc-hivtesting.component.css']
})
export class PncHivtestingComponent implements OnInit, AfterViewInit {
    HivTestingForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];

    lookupItemView$: Subscription;
    LookupItems$: Subscription;

    public testVisits: LookupItemView[] = [];
    public kits: LookupItemView[] = [];
    public tests: LookupItemView[] = [];
    public testResults: LookupItemView[] = [];

    @Input('pncHivOptions') pncHivOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Input('patientEncounterId') patientEncounterId: number;

    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    isHivTestingDone: boolean = false;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    public historical_hiv_testing_data: HivTestingTableData[] = [];

    displayedColumns = ['testdate', 'testtype', 'kitname', 'lotnumber',
        'expirydate', 'testresult', 'nexthivtest', 'testpoint', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    constructor(private dialog: MatDialog,
        private _formBuilder: FormBuilder,
        private pncService: PncService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private _lookupItemService: LookupItemService) { }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            hivTestingDone: new FormControl('', [Validators.required]),
            testType: new FormControl('', [Validators.required]),
            finalTestResult: new FormControl('', [Validators.required])
        });

        this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

        const { yesnoOptions, hivFinalResultsOptions } = this.pncHivOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.hivFinalResultsOptions = hivFinalResultsOptions;

        this.notify.emit({ 'form': this.HivTestingForm, 'table_data': this.hiv_testing_table_data });

        this.getLookupOptions('PMTCTHIVTestVisit', this.testVisits);
        this.getLookupOptions('HIVTestKits', this.kits);
        this.getLookupOptions('PMTCTHIVTests', this.tests);
        this.getLookupOptions('HIVResults', this.testResults);
    }

    ngAfterViewInit() {
        if (this.isEdit) {
            this.loadHivTests();
        }
    }

    loadHivTests(): void {
        this.pncService.getHivTests(this.patientMasterVisitId, this.patientEncounterId).subscribe(
            (result) => {
                if (result && result['encounter'] && result['encounter'].length > 0) {
                    const tests = result['testing'];
                    if (tests.length > 0) {
                        for (let i = 0; i < tests.length; i++) {
                            const testRound = tests[i].testRound == 1 ? 'HIV Test-1' : 'HIV Test-2';
                            const testType = this.tests.find(obj => obj.itemName == testRound);
                            const kitName = this.kits.find(obj => obj.itemId == tests[i].kitId);
                            const lotnumber = tests[i].kitLotNumber;
                            const expirydate = tests[i].expiryDate;
                            const outcome = this.testResults.find(obj => obj.itemId == tests[i].outcome);
                            const testEntryPoint = result['encounter'];


                            this.historical_hiv_testing_data.push({
                                testdate: new Date(),
                                testtype: testType,
                                kitname: kitName,
                                lotnumber: lotnumber,
                                expirydate: expirydate,
                                testresult: outcome,
                                nexthivtest: null,
                                testpoint: 'PNC'
                            });
                        }
                    }


                    this.HivTestingForm.controls.testType.setValue(result['encounter'][0]['encounterType']);
                    const finalTestResult = this.hivFinalResultsOptions.find(
                        obj => obj.itemId == result['encounterResults'][0]['finalResult']);
                    this.HivTestingForm.get('finalTestResult').setValue(finalTestResult.itemId);
                    if (finalTestResult.itemName == 'Positive') {
                        this.HivTestingForm.get('hivTestingDone').disable({ onlySelf: false });
                    }
                    this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
                }
            },
            (error) => {

            }
        );
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

                this.historical_hiv_testing_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: 'PNC'
                });

                this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
                // this.dataSource = new MatTableDataSource(this.hiv_testing_table_data);
            }
        );
    }

    public onRowClicked(row) {
        const index = this.historical_hiv_testing_data.indexOf(row.lotnumber);
        this.historical_hiv_testing_data.splice(index, 1);
        this.hiv_testing_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
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
        }
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
                    // console.log(err);
                    this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    // console.log(this.lookupItemView$);
                });
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

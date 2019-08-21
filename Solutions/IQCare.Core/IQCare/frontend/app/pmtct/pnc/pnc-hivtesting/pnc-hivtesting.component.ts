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
import { DataService } from '../../_services/data.service';

@Component({
    selector: 'app-pnc-hivtesting',
    templateUrl: './pnc-hivtesting.component.html',
    styleUrls: ['./pnc-hivtesting.component.css']
})
export class PncHivtestingComponent implements OnInit, AfterViewInit {
    HivTestingForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];
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
    @Input() visitDate: Date;
    @Input() personId: number;
    @Input() serviceAreaId: number;
    serviceAreaName: string;
    hiv_status: string;
    message: any;

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
        private _lookupItemService: LookupItemService,
        private dataservice: DataService) { }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            hivTestingDone: new FormControl('', [Validators.required]),
            testType: new FormControl('', [Validators.required]),
            finalTestResult: new FormControl('', [Validators.required]),
            screeningTestResult: new FormControl('', [Validators.required]),
            confirmatoryTestResult: new FormControl('', [Validators.required]),
        });

        this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
        this.HivTestingForm.controls['screeningTestResult'].disable({ onlySelf: true });
        this.HivTestingForm.controls['confirmatoryTestResult'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

        const { yesnoOptions, hivFinalResultsOptions } = this.pncHivOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.hivFinalResultsOptions = hivFinalResultsOptions;

        if (this.serviceAreaId == 3) {
            this.serviceAreaName = 'ANC';
        } else if (this.serviceAreaId == 4) {
            this.serviceAreaName = 'PNC';
        } else if (this.serviceAreaId == 5) {
            this.serviceAreaName = 'Maternity';
        } else if (this.serviceAreaId == 6) {
            this.serviceAreaName = 'HEI';
        } else {
            this.serviceAreaName = 'HTS';
        }

        this.notify.emit({ 'form': this.HivTestingForm, 'table_data': this.hiv_testing_table_data });

        this.getLookupOptions('PMTCTHIVTestVisit', this.testVisits);
        this.getLookupOptions('HIVTestKits', this.kits);
        this.getLookupOptions('PMTCTHIVTests', this.tests);
        this.getLookupOptions('HIVResults', this.testResults);

        this.dataservice.currentHivStatus.subscribe(hivStatus => {
            this.hiv_status = hivStatus;

            if (this.hiv_status !== '' && this.hiv_status == 'Positive') {
                const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                if (noOption.length > 0) {
                    this.HivTestingForm.controls['hivTestingDone'].setValue(noOption[0].itemId);
                }
                this.message = 'HIV Positive';
            } else {
                this.HivTestingForm.controls['hivTestingDone'].enable({ onlySelf: true });
                this.message = '';
            }
        });
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


                            this.historical_hiv_testing_data.push({
                                testdate: new Date(),
                                testtype: testType,
                                kitname: kitName,
                                lotnumber: lotnumber,
                                expirydate: expirydate,
                                testresult: outcome,
                                nexthivtest: null,
                                testpoint: this.serviceAreaName
                            });
                        }
                    }

                    if (result['encounterResults'].length > 0) {
                        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                        this.HivTestingForm.get('hivTestingDone').setValue(yesOption[0].itemId);
                        this.HivTestingForm.controls.testType.setValue(result['encounter'][0]['encounterType']);
                        this.HivTestingForm.get('screeningTestResult').setValue(result['encounterResults'][0]['roundOneTestResult']);
                        if (result['encounterResults'][0]['roundTwoTestResult']) {
                            this.HivTestingForm.get('confirmatoryTestResult').setValue(
                                result['encounterResults'][0]['roundTwoTestResult']);
                        } else {
                            this.HivTestingForm.get('confirmatoryTestResult').disable({onlySelf: true });
                        }

                        const finalTestResult = this.hivFinalResultsOptions.find(obj =>
                            obj.itemId == result['encounterResults'][0]['finalResult']);
                        if (finalTestResult != null || finalTestResult != undefined) {
                            this.HivTestingForm.get('finalTestResult').setValue(finalTestResult.itemId);
                            if (finalTestResult.itemName == 'Positive') {
                                this.HivTestingForm.get('hivTestingDone').disable({ onlySelf: false });
                            }
                        }

                    } else {
                        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                        this.HivTestingForm.get('hivTestingDone').setValue(noOption[0].itemId);
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
            'visitDate': this.visitDate
        };

        const dialogRef = this.dialog.open(HivStatusComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                if (data.hivTest.itemName == 'HIV Test-2' && !this.HivTestingForm.get('screeningTestResult').value) {
                    this.snotifyService.info('The screening test should be done first', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // ensure only one screening test is done
                if (this.HivTestingForm.get('screeningTestResult').value && data.hivTest.itemName == 'HIV Test-1') {
                    this.snotifyService.info(data.hivTest.itemName + ' has already been done', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // ensure only one confirmatory test is done
                if (this.HivTestingForm.get('confirmatoryTestResult').value && data.hivTest.itemName == 'HIV Test-2') {
                    this.snotifyService.info(data.hivTest.itemName + ' has already been done', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // ensure screening and confirmatory test use different kits
                const firstTest = this.hiv_testing_table_data.filter(obj => obj.testtype.itemName == 'HIV Test-1');
                if (data.hivTest.itemName == 'HIV Test-2' && firstTest[0].kitname['itemName'] == data.kitName['itemName']) {
                    this.snotifyService.info('The same kitname has been used for screening and confirmatory test.' +
                        'Please select another kitname.', 'Testing', this.notificationService.getConfig());
                    return;
                }

                // screening algorithm
                if (data.hivTest.itemName == 'HIV Test-1' && data.testResult.itemName == 'Negative') {
                    this.HivTestingForm.get('screeningTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Negative')[0].itemId);

                    // set final result and disable screening test
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Negative')[0].itemId);
                    this.HivTestingForm.get('confirmatoryTestResult').disable({onlySelf: true });
                } else if (data.hivTest.itemName == 'HIV Test-1' && data.testResult.itemName == 'Positive') {
                    this.HivTestingForm.get('screeningTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Positive')[0].itemId);
                }

                const screeningValue = this.HivTestingForm.get('screeningTestResult').value;
                const screeningText = this.testResults.filter(obj => obj.itemId == screeningValue);

                // confirmatory algorithm
                if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Negative'
                    && screeningText[0].itemName == 'Negative') {
                    this.HivTestingForm.get('confirmatoryTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Negative')[0].itemId);

                    // set final result
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Negative')[0].itemId);
                } else if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Positive'
                    && screeningText[0].itemName == 'Positive') {
                    this.HivTestingForm.get('confirmatoryTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Positive')[0].itemId);

                    // set final result
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Positive')[0].itemId);
                } else if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Positive'
                    && screeningText[0].itemName == 'Negative') {
                    this.HivTestingForm.get('confirmatoryTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Positive')[0].itemId);

                    // set final result
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Inconclusive')[0].itemId);
                } else if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Negative'
                    && screeningText[0].itemName == 'Positive') {
                    this.HivTestingForm.get('confirmatoryTestResult').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Negative')[0].itemId);

                    // set final result
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Inconclusive')[0].itemId);
                }

                this.hiv_testing_table_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: this.serviceAreaName
                });

                this.historical_hiv_testing_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: this.serviceAreaName
                });

                this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
            }
        );
    }

    public onRowClicked(row) {
        const index = this.historical_hiv_testing_data.indexOf(row.lotnumber);
        this.historical_hiv_testing_data.splice(index, 1);
        this.hiv_testing_table_data.splice(index, 1);
        this.HivTestingForm.controls['screeningTestResult'].setValue('');
        this.HivTestingForm.controls['confirmatoryTestResult'].setValue('');
        this.HivTestingForm.controls['finalTestResult'].setValue('');
        this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
    }

    onHivTestDoneChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.isHivTestingDone = true;
            this.HivTestingForm.controls['testType'].enable({ onlySelf: true });
            this.HivTestingForm.controls['screeningTestResult'].enable({ onlySelf: true });
            this.HivTestingForm.controls['confirmatoryTestResult'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].enable({ onlySelf: true });
        } else if (event.source.selected) {
            this.isHivTestingDone = false;
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['screeningTestResult'].disable({ onlySelf: true });
            this.HivTestingForm.controls['confirmatoryTestResult'].disable({ onlySelf: true });
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

    public onFinalHivResultChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Positive') {
            this.dataservice.changeHivStatus('Positive');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Negative') {
            this.dataservice.changeHivStatus('Negative');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Inconclusive') {
            this.dataservice.changeHivStatus('Inconclusive');
        }
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

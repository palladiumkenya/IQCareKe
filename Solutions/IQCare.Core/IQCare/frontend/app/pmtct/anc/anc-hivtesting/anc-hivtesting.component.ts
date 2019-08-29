import { SnotifyService } from 'ng-snotify';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatDialog, MatDialogConfig, MatTableDataSource } from '@angular/material';
import { HivStatusComponent } from '../hiv-status/hiv-status.component';
import { Subscription } from 'rxjs/index';
import { AncService } from '../../_services/anc.service';
import { PncService } from '../../_services/pnc.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { DataService } from '../../_services/data.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import {EncounterService} from '../../../hts/_services/encounter.service';

@Component({
    selector: 'app-anc-hivtesting',
    templateUrl: './anc-hivtesting.component.html',
    styleUrls: ['./anc-hivtesting.component.css'],
    providers: [EncounterService]
})
export class AncHivtestingComponent implements OnInit {
    ancHivStatusInitialVisitOptions: LookupItemView[];
    yesnoOptions: LookupItemView[];
    hivFinalResultsOptions: LookupItemView[];
    baseline$: Subscription;

    isHivTestingDone: boolean = false;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    public historical_hiv_testing_data: HivTestingTableData[] = [];

    displayedColumns = ['testdate', 'testtype', 'kitname', 'lotnumber',
        'expirydate', 'testresult', 'syphilis', 'nexthivtest', 'testpoint', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    HivTestingForm: FormGroup;
    @Input('hivTestingOptions') hivTestingOptions: any;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    @Input() patientEncounterId: number;
    @Input() personId: number;
    @Input() serviceAreaId: number;
    @Input() visitDate: Date;

    lookupItemView$: Subscription;
    LookupItems$: Subscription;

    duoKitLotNumber: string;
    duoKitexpiryDate: Date;

    firstResponseKitLotNumber: string;
    firstResponseKitexpiryDate: Date;

    determineKitLotNumber: string;
    determineKitexpiryDate: Date;

    otherKitLotNumber: string;
    otherKitexpiryDate: Date;

    public testVisits: LookupItemView[] = [];
    public kits: LookupItemView[] = [];
    public tests: LookupItemView[] = [];
    public testResults: LookupItemView[] = [];
    public syphillisResultsOptions: LookupItemView[] = [];

    serviceAreaName: string;

    constructor(
        private dialog: MatDialog,
        private _formBuilder: FormBuilder,
        private ancService: AncService,
        private pncService: PncService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private dataservice: DataService,
        private _lookupItemService: LookupItemService,
        private encounterService: EncounterService) {

    }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            hivStatusBeforeFirstVisit: new FormControl('', [Validators.required]),
            hivTestingDone: new FormControl('', [Validators.required]),
            testType: new FormControl('', [Validators.required]),
            finalResultScreening: new FormControl('', [Validators.required]),
            finalResultConfirmatory: new FormControl('', [Validators.required]),
            finalTestResult: new FormControl('', [Validators.required])
        });

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

        this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalResultConfirmatory'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalResultScreening'].disable({ onlySelf: true });

        const { ancHivStatusInitialVisitOptions, yesnoOptions, hivFinalResultsOptions } = this.hivTestingOptions[0];
        this.ancHivStatusInitialVisitOptions = ancHivStatusInitialVisitOptions;
        this.yesnoOptions = yesnoOptions;
        this.hivFinalResultsOptions = hivFinalResultsOptions;

        this.getLookupOptions('PMTCTHIVTestVisit', this.testVisits);
        this.getLookupOptions('ScreeningHIVTestKits', this.kits);
        this.getLookupOptions('PMTCTHIVTests', this.tests);
        this.getLookupOptions('HIVResults', this.testResults);
        this.getLookupOptions('SyphilisResults', this.syphillisResultsOptions);

        this.notify.emit({ 'form': this.HivTestingForm, 'table_data': this.hiv_testing_table_data });

        if (this.isEdit) {
            this.getBaselineAncProfile(this.PatientId);
        } else {
            this.getBaselineAncProfile(this.PatientId);
        }

        this.loadHivTests();
        this.personCurrentHivStatus();
        
        setTimeout(() => {
            const otherKit = this.kits.filter(obj => obj.itemName == 'Other');
            const determineKit = this.kits.filter(obj => obj.itemName == 'Determine');
            const firstResponseKit = this.kits.filter(obj => obj.itemName == 'First Response');
            const DuoKit = this.kits.filter(obj => obj.itemName == 'HIV/Syphilis Duo');

            if (DuoKit.length > 0) {
                this.getLastUsedDuoKit(DuoKit[0].itemId);
            }

            if (firstResponseKit.length > 0) {
                this.getLastUsedFirstResponseKit(firstResponseKit[0].itemId);
            }

            if (otherKit.length > 0) {
                this.getLastUsedOtherKit(otherKit[0].itemId);
            }

            if (determineKit.length > 0) {
                this.getLastUsedDetermineKit(determineKit[0].itemId);
            }
        }, 4000);    
    }

    private getLastUsedOtherKit(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.otherKitLotNumber = res.kitLotNumber;
                    this.otherKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    private getLastUsedDetermineKit(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.determineKitLotNumber = res.kitLotNumber;
                    this.determineKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    private getLastUsedFirstResponseKit(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.firstResponseKitLotNumber = res.kitLotNumber;
                    this.firstResponseKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    public getLastUsedDuoKit(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.duoKitLotNumber = res.kitLotNumber;
                    this.duoKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    loadHivTests(): void {
        this.pncService.getHivTests(this.PatientMasterVisitId, this.patientEncounterId).subscribe(
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
                            let syphilis = null;
                            if (result['encounterResults'].length > 0) {
                                syphilis = this.syphillisResultsOptions.find(obj => obj.itemId ==
                                    result['encounterResults'][0]['syphilisResult']);
                            }
                            const testEntryPoint = result['encounter'];


                            this.historical_hiv_testing_data.push({
                                testdate: new Date(),
                                testtype: testType,
                                kitname: kitName,
                                lotnumber: lotnumber,
                                expirydate: expirydate,
                                testresult: outcome,
                                nexthivtest: null,
                                testpoint: this.serviceAreaName,
                                SyphilisResult: syphilis
                            });
                        }
                    }

                    if (result['encounterResults'].length > 0 && result['encounterResults'][0]['finalResult'] > 0) {
                        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                        this.HivTestingForm.get('hivTestingDone').setValue(yesOption[0].itemId);
                        this.HivTestingForm.controls.testType.setValue(result['encounter'][0]['encounterType']);
                        this.HivTestingForm.get('finalResultScreening').setValue(result['encounterResults'][0]['roundOneTestResult']);
                        if (result['encounterResults'][0]['roundTwoTestResult']) {
                            this.HivTestingForm.get('finalResultConfirmatory').setValue(
                                result['encounterResults'][0]['roundTwoTestResult']);
                        } else {
                            this.HivTestingForm.get('finalResultConfirmatory').disable({onlySelf: true });
                        }
                        const finalTestResult = this.hivFinalResultsOptions.find(
                            obj => obj.itemId == result['encounterResults'][0]['finalResult']);
                        if (finalTestResult) {
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
            'visitDate': this.visitDate,
            'duoKitLotNumber': this.duoKitLotNumber,
            'duoKitexpiryDate': this.duoKitexpiryDate,
            'firstResponseKitLotNumber': this.firstResponseKitLotNumber,
            'firstResponseKitexpiryDate': this.firstResponseKitexpiryDate,
            'determineKitLotNumber': this.determineKitLotNumber,
            'determineKitexpiryDate': this.determineKitexpiryDate,
            'otherKitLotNumber': this.otherKitLotNumber,
            'otherKitexpiryDate': this.otherKitexpiryDate
        };

        // console.log(this.serviceAreaName);
        const dialogRef = this.dialog.open(HivStatusComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                if (data.hivTest.itemName == 'HIV Test-2' && data.kitName['itemName'] == 'HIV/Syphilis Duo') {
                    this.snotifyService.info('HIV/Syphilis Duo should not be used as a confirmatory test', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                if (data.hivTest.itemName == 'HIV Test-2' && !this.HivTestingForm.get('finalResultScreening').value) {
                    this.snotifyService.info('The screening test should be done first', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // ensure only one screening test is done
                if (this.HivTestingForm.get('finalResultScreening').value && data.hivTest.itemName == 'HIV Test-1') {
                    this.snotifyService.info(data.hivTest.itemName + ' has already been done', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // ensure only one confirmatory test is done
                if (this.HivTestingForm.get('finalResultConfirmatory').value && data.hivTest.itemName == 'HIV Test-2') {
                    this.snotifyService.info(data.hivTest.itemName + ' has already been done', 'HIV TESTS',
                        this.notificationService.getConfig());
                    return;
                }

                // screening algorithm
                if (data.hivTest.itemName == 'HIV Test-1' && data.testResult.itemName == 'Negative') {
                    this.HivTestingForm.get('finalResultScreening').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Negative')[0].itemId);

                    // set final result and disable screening test
                    this.HivTestingForm.get('finalTestResult').setValue(
                        this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Negative')[0].itemId);
                    this.HivTestingForm.get('finalResultConfirmatory').disable({onlySelf: true });
                } else if (data.hivTest.itemName == 'HIV Test-1' && data.testResult.itemName == 'Positive') {
                    this.HivTestingForm.get('finalResultScreening').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Positive')[0].itemId);
                }

                const screeningTestResult = this.hivFinalResultsOptions.filter(obj => obj.itemId ==
                    this.HivTestingForm.get('finalResultScreening').value);

                // confirmatory algorithm
                if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Negative') {
                    this.HivTestingForm.get('finalResultConfirmatory').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Negative')[0].itemId);

                    if (screeningTestResult.length > 0  && screeningTestResult[0]['itemName'] == 'Positive') {
                        this.HivTestingForm.get('finalTestResult').setValue(
                            this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Inconclusive')[0].itemId);
                    } else if (screeningTestResult.length > 0 && screeningTestResult[0]['itemName'] == 'Negative') {
                        // set final result
                        this.HivTestingForm.get('finalTestResult').setValue(
                            this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Negative')[0].itemId);
                    }
                } else if (data.hivTest.itemName == 'HIV Test-2' && data.testResult.itemName == 'Positive') {
                    this.HivTestingForm.get('finalResultConfirmatory').setValue(
                        this.testResults.filter(obj => obj.itemName == 'Positive')[0].itemId);

                    if (screeningTestResult.length > 0 && screeningTestResult[0]['itemName'] == 'Positive') {
                        this.HivTestingForm.get('finalTestResult').setValue(
                            this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Positive')[0].itemId);
                    } else if (screeningTestResult.length > 0 && screeningTestResult[0]['itemName'] == 'Negative') {
                        // set final result
                        this.HivTestingForm.get('finalTestResult').setValue(
                            this.hivFinalResultsOptions.filter(obj => obj.itemName == 'Inconclusive')[0].itemId);
                    }
                }

                this.hiv_testing_table_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: this.serviceAreaName,
                    SyphilisResult: data.SyphilisResult
                });

                this.historical_hiv_testing_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: this.serviceAreaName,
                    SyphilisResult: data.SyphilisResult
                });

                this.dataSource = new MatTableDataSource(this.historical_hiv_testing_data);
            }
        );
    }

    onHivTestDoneChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.isHivTestingDone = true;
            this.HivTestingForm.controls['testType'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalResultConfirmatory'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalResultScreening'].enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.isHivTestingDone = false;
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalResultConfirmatory'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalResultScreening'].disable({ onlySelf: true });

            // set default value to null
            this.HivTestingForm.controls['testType'].setValue('');
            this.HivTestingForm.controls['finalTestResult'].setValue('');
            this.HivTestingForm.controls['finalResultConfirmatory'].setValue('');
            this.HivTestingForm.controls['finalResultScreening'].setValue('');
        }
    }

    onHivStatusBeforeFirstVisitChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Known Positive') {
            this.HivTestingForm.controls['hivTestingDone'].disable({ onlySelf: true });
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });
            // set hiv_status
            this.dataservice.changeHivStatus('Positive');
        } else if (event.isUserInput && event.source.selected) {
            this.HivTestingForm.controls['hivTestingDone'].enable();
            // set hiv_status
            this.dataservice.changeHivStatus('Negative');
        }
    }

    public getBaselineAncProfile(patientId: number): void {
        this.baseline$ = this.ancService.getBaselineAncProfile(patientId)
            .subscribe(
                p => {
                    const baseline = p;

                    if (baseline) {
                        this.HivTestingForm.get('hivStatusBeforeFirstVisit').setValue(baseline['hivStatusBeforeAnc']);
                    }
                },
                error1 => {

                },
                () => {

                }
            );
    }

    public personCurrentHivStatus() {
        this.pncService.getPersonCurrentHivStatus(this.personId).subscribe(
            (res) => {
                if (res.length > 0) {
                    const hivPositiveResult = this.ancHivStatusInitialVisitOptions.filter(obj => obj.itemName == 'Known Positive');
                    if (hivPositiveResult.length > 0) {
                        this.HivTestingForm.get('hivStatusBeforeFirstVisit').setValue(hivPositiveResult[0].itemId);
                        this.HivTestingForm.get('hivStatusBeforeFirstVisit').disable( { onlySelf: true });

                        // set hiv_status
                        this.dataservice.changeHivStatus('Positive');
                        // set the default to null
                        this.isHivTestingDone = false;
                        this.HivTestingForm.controls['testType'].setValue('');
                        this.HivTestingForm.controls['finalTestResult'].setValue('');
                    } else {
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

    public onHivTestFinalResultChange(event) {
        if (event.isUserInput
            && event.source.selected
            && event.source.viewValue == 'Positive') {
            this.dataservice.changeHivStatus('Positive');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Negative') {
            this.dataservice.changeHivStatus('Negative');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Inconclusive') {
            this.dataservice.changeHivStatus('Inconclusive');
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
    SyphilisResult?: any;
}

import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import {
    FormGroup,
    FormBuilder,
    FormControl,
    Validators
} from '@angular/forms';
import {
    MatTableDataSource,
    MatDialogConfig,
    MatDialog
} from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { HivtestingmodalComponent } from './hivtestingmodal/hivtestingmodal.component';
import * as moment from 'moment';
import { HeiCompletelaborderComponent } from './hei-completelaborder/hei-completelaborder.component';
import { CompleteLabOrderCommand } from '../../_models/hei/CompleteLabOrderCommand';
import { DataService } from '../../_services/data.service';
import { LabOrder } from '../../_models/hei/LabOrder';
import { mergeMap } from 'rxjs/operators';
import { OrdVisitCommand } from '../../_models/hei/OrdVisitCommand';


@Component({
    selector: 'app-hei-hivtesting',
    templateUrl: './hei-hivtesting.component.html',
    styleUrls: ['./hei-hivtesting.component.css']
})
export class HeiHivtestingComponent implements OnInit {
    hivTestType: LookupItemView[] = [];
    testResults: LookupItemView[] = [];
    YesNo: LookupItemView[] = [];
    maxDate: Date;
    pcrLabTestParameters: any[];
    viralLoadLabTestParameters: any[];
    antibodyLabTestParameters: any[];

    locationId: number;
    userId: number;
    pcrLabTest: any;
    viralLoadLabTest: any;
    antibodyLabTest: any;
    visitDate: Date;




    public hiv_testing_table_data: HivTestingTableData[] = [];
    public hiv_testing_history_data: HivTestingTableData[] = [];
    displayedColumns = [
        'testtype',
        'dateofsamplecollection',
        'result',
        'dateresultscollected',
        'status',
        'action'
    ];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    @Input('heiHivTestingOptions') heiHivTestingOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;
    @Input('ptn_pk') ptn_pk: number;

    @Output() notify: EventEmitter<any> = new EventEmitter<any>();

    constructor(
        private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dialog: MatDialog,
        private heiservice: HeiService,
        private dataservice: DataService,
        private spinner: NgxSpinnerService
    ) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        const {
            hivTestType,
            testResults,
            YesNo
        } = this.heiHivTestingOptions[0];
        this.hivTestType = hivTestType.sort(function (a, b) {
            return a.itemId - b.itemId;
        });
        this.testResults = testResults;
        this.YesNo = YesNo;

        this.notify.emit(this.hiv_testing_table_data);

        this.loadPatientCompletedTestTypes();

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.locationId = JSON.parse(localStorage.getItem('appLocationId'));

        this.heiservice.getHeiLabTests().subscribe(result => {
            for (let i = 0; i < result.length; i++) {
                if (result[i].key == 'PCR') {
                    this.pcrLabTest = result[i].value;
                    this.heiservice
                        .getLabTestPametersByLabTestId(this.pcrLabTest.id)
                        .subscribe(res => {
                            this.pcrLabTestParameters = res;
                        });
                } else if (result[i].key == 'Viral Load') {
                    this.viralLoadLabTest = result[i].value;
                    this.heiservice
                        .getLabTestPametersByLabTestId(this.viralLoadLabTest.id)
                        .subscribe(res => {
                            this.viralLoadLabTestParameters = res;
                        });
                } else if (result[i].key == 'HIV Rapid Test') {
                    this.antibodyLabTest = result[i].value;
                    this.heiservice
                        .getLabTestPametersByLabTestId(this.antibodyLabTest.id)
                        .subscribe(res => {
                            this.antibodyLabTestParameters = res;
                        });
                }
            }
        });
    }

    loadPatientCompletedTestTypes(): void {
        this.heiservice.getPatientHeiLabTestsTypes(this.patientId).subscribe(
            res => {
                this.loadHeiHivTests(res);
            },
            error => {
                this.snotifyService.error(
                    'Failed to load hei lab tests ',
                    'HEI',
                    this.notificationService.getConfig()
                );
            }
        );
    }

    loadHeiHivTests(heiLabTests: any[]): void {
        this.heiservice
            .getLabOrderTestResults(this.patientId)
            .subscribe(res => {
                for (let i = 0; i < res.length; i++) {
                    const savedHeiLabTests = heiLabTests.filter(
                        obj => obj.labOrderId == res[i].labOrderId
                    );
                    let testType;
                    if (savedHeiLabTests.length > 0) {
                        testType = this.hivTestType.filter(
                            obj =>
                                obj.itemId ==
                                savedHeiLabTests[0]['heiLabTestTypeId']
                        );
                    } else {
                        testType = this.hivTestType.filter(obj =>
                            obj.itemName.includes(res[i].labTestName)
                        );
                    }
                    const testResultHistorical = this.testResults.filter(obj =>
                        obj.itemName.includes(res[i].result)
                    );
                    let resultValue = null;
                    let resultText = null;
                    if (testResultHistorical.length > 0) {
                        resultValue = testResultHistorical[0];
                    } else {
                        resultText = res[i].result;
                    }

                    this.hiv_testing_history_data.push({
                        testtype: testType[0],
                        dateofsamplecollection: res[i].sampleDate,
                        result: resultValue,
                        dateresultscollected: res[i].resultDate,
                        comments: '',
                        resultText: resultText,
                        status: res[i].resultDate ? 'complete' : 'update',
                        labOrderId: res[i].labOrderId,
                        labOrderTestId: res[i].labOrderTestId,
                        labTestId: res[i].labTestId
                    });
                }

                this.dataSource = new MatTableDataSource(
                    this.hiv_testing_history_data
                );
            });
    }

    AddHivTests() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
            hivTestType: this.hivTestType,
            testResults: this.testResults,
            YesNo: this.YesNo
        };

        const dialogRef = this.dialog.open(
            HivtestingmodalComponent,
            dialogConfig
        );

        dialogRef.afterClosed().subscribe(data => {
            if (!data) {
                return;
            }

            const testExists =
                this.hiv_testing_history_data.filter(
                    x => x.testtype == data.testtype
                ).length > 0;
            if (testExists) {
                this.snotifyService.error(
                    data.testtype.itemName + ' has already been added',
                    'HEI Encounter',
                    this.notificationService.getConfig()
                );
                return;
            }
            if (this.isEdit == true) {
                this.visitDate = new Date(localStorage.getItem('visitDate'));
                const ordVisitCommand: OrdVisitCommand = {
                    Ptn_Pk: this.ptn_pk,
                    LocationID: this.locationId,
                    VisitDate: this.visitDate,
                    UserID: this.userId
                };


                const laborder: LabOrder = {
                    Ptn_Pk: this.ptn_pk,
                    PatientId: this.patientId,
                    LocationId: this.locationId,
                    FacilityId: this.locationId,
                    VisitId: 1,
                    ModuleId: 1,
                    OrderedBy: this.userId,
                    OrderDate: new Date(),
                    ClinicalOrderNotes: '',
                    CreateDate: new Date(),
                    OrderStatus: 'Pending',
                    UserId: this.userId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    LabTests: []
                };

                let labTestId;
                let latTestNotes;
                let labTestName;
                if (
                    data.testtype.itemName == '1st DNA PCR'
                    || data.testtype.itemName == '2nd DNA PCR'
                    || data.testtype.itemName == '3rd DNA PCR'
                    || data.testtype.itemName == 'Repeat confirmatory PCR (for +ve)'
                    || data.testtype.itemName == 'Confirmatory PCR (for  +ve)'
                ) {
                    labTestId = this.pcrLabTest.id;
                    latTestNotes = data.comments;
                    labTestName = this.pcrLabTest.name;

                    this.heiservice.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.pcrLabTestParameters = res;
                        }
                    );
                    // this.pcrLabTestParameters
                } else if (data.testtype.itemName == 'Baseline Viral Load (for +ve)') {
                    labTestId = this.viralLoadLabTest.id;
                    latTestNotes = data.comments;
                    labTestName = this.viralLoadLabTest.name;

                    this.heiservice.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.viralLoadLabTestParameters = res;
                        }
                    );
                } else if (data.testtype.itemName == 'Final Antibody') {
                    labTestId = this.antibodyLabTest.id;
                    latTestNotes = data.comments;
                    labTestName = this.antibodyLabTest.name;

                    this.heiservice.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.antibodyLabTestParameters = res;
                        }
                    );
                }
                if (labTestId) {
                    laborder.LabTests.push({
                        Id: labTestId,
                        Notes: latTestNotes,
                        LabTestName: labTestName
                    });
                }


                const completeLabOrderCommand: CompleteLabOrderCommand = {
                    LabOrderId: 0,
                    LabOrderTestId: 0,
                    LabTestId: 0,
                    UserId: this.userId,
                    LabTestResults: [],
                    DateResultsCollected: moment(new Date()).utc(true).toDate()
                };

                const heiLabTestsTypes: any = {
                    LabOrderId: 0,
                    PatientId: this.patientId,
                    HeiLabTestTypes: []
                };

                this.heiservice.saveOrdVisit(ordVisitCommand, laborder).subscribe((result) => {
                    this.spinner.show();
                    console.log(result);
                    laborder.VisitId = result['visit_Id'];


                    const heiLab = this.heiservice.saveHeiLabOrder(laborder).pipe(
                        mergeMap(res => this.heiservice.getLabOrderTestsByOrderId(res['labOrderId']))
                    ).subscribe(res => {
                        // console.log(res);
                        if (res.length > 0 && res[0]['labOrderId']) {
                            completeLabOrderCommand.LabOrderId = res[0]['labOrderId'];
                            completeLabOrderCommand.LabOrderTestId = res[0]['id'];
                            completeLabOrderCommand.LabTestId = res[0]['labTestId'];

                            // set hei lab test types
                            heiLabTestsTypes.LabOrderId = res[0]['labOrderId'];
                        }


                        completeLabOrderCommand.DateResultsCollected =
                            moment(data.dateresultscollected).utc(true).toDate();
                        if (
                            data.testtype.itemName == '1st DNA PCR'
                            || data.testtype.itemName == '2nd DNA PCR'
                            || data.testtype.itemName == '3rd DNA PCR'
                            || data.testtype.itemName == 'Repeat confirmatory PCR (for +ve)'
                            || data.testtype.itemName == 'Confirmatory PCR (for  +ve)'
                        ) {
                            heiLabTestsTypes.HeiLabTestTypes.push({
                                id: data.testtype.itemId
                            });
                            if (this.pcrLabTestParameters.length > 0) {
                                if (data.result && data.result != null
                                    && data.result != '') {
                                    completeLabOrderCommand.LabTestResults.push({
                                        ParameterId: this.pcrLabTestParameters[0]['id'],
                                        ResultValue: null,
                                        ResultText: data.result.itemName,
                                        ResultOptionId: null,
                                        ResultOption: null,
                                        ResultUnit: null,
                                        ResultUnitId: null,
                                        Undetectable: false,
                                        DetectionLimit: this.pcrLabTestParameters[0]['detectionLimit'],
                                    });
                                }
                            }
                        } else if (
                            data.testtype.itemName == 'Baseline Viral Load (for +ve)') {
                            heiLabTestsTypes.HeiLabTestTypes.push({
                                id: data.testtype.itemId
                            });
                            if (this.viralLoadLabTestParameters.length > 0) {
                                if (data.resultText
                                    && data.resultText != null
                                    && data.resultText != '') {
                                    completeLabOrderCommand.LabTestResults.push({
                                        ParameterId: this.viralLoadLabTestParameters[0]['id'],
                                        ResultValue: data.resultText,
                                        ResultText: null,
                                        ResultOptionId: null,
                                        ResultOption: null,
                                        ResultUnit: null,
                                        ResultUnitId: this.viralLoadLabTestParameters[0]['unitId'],
                                        Undetectable: false,
                                        DetectionLimit: this.viralLoadLabTestParameters[0]['detectionLimit'],
                                    });
                                }
                            }
                        } else if (data.testtype.itemName == 'Final Antibody') {
                            heiLabTestsTypes.HeiLabTestTypes.push({
                                id: data.testtype.itemId
                            });
                            if (this.antibodyLabTestParameters.length > 0) {
                                if (data.result && data.result != null
                                    && data.result != '') {
                                    completeLabOrderCommand.LabTestResults.push({
                                        ParameterId: this.antibodyLabTestParameters[0]['id'],
                                        ResultValue: data.result.itemName == 'Positive' ? 1 : 2,
                                        ResultText: null,
                                        ResultOptionId: null,
                                        ResultOption: null,
                                        ResultUnit: null,
                                        ResultUnitId: this.antibodyLabTestParameters[0]['unitId'],
                                        Undetectable: false,
                                        DetectionLimit: this.antibodyLabTestParameters[0]['detectionLimit'],
                                    });
                                }
                            }
                        }



                        const completeHeiLabOrder = this.heiservice.saveCompleteHeiLabOrder(completeLabOrderCommand).subscribe(
                            (completeRes) => {

                                this.snotifyService.success('Successfuly Completed the LabOrder');

                                // console.log('complete laborder');
                                console.log(completeRes);
                            },
                            (completeError) => {

                                console.log('Error completing laborder' + completeError);
                            }
                        );

                        const heiLabTestsCommand = this.heiservice.saveHeiLabTestsTypes(heiLabTestsTypes).subscribe(
                            (completeRes) => {
                                // console.log('complete laborder');
                                
                                this.snotifyService.success('Successfully completed saving hei lab Tests');
                                console.log(completeRes);

                            },
                            (completeError) => {
                                console.log('Error completing laborder' + completeError);
                                this.spinner.hide();
                            }
                        );
                    });



                }, (error) => {
                    console.log("Error creating VisitId" + error);
                }, () => {
                    this.spinner.hide();
                });

            }

            this.hiv_testing_table_data.push({
                testtype: data.testtype,
                dateofsamplecollection: moment(
                    data.dateofsamplecollection
                ).toDate(),
                result: data.result,
                dateresultscollected: data.dateresultscollected
                    ? moment(
                        moment(data.dateresultscollected).toDate(),
                        'dd-MMM-yyyy'
                    ).toString()
                    : '',
                comments: data.comments,
                resultText: data.resultText,
                status: data.dateresultscollected ? 'complete' : 'pending'
            });

            this.hiv_testing_history_data.push({
                testtype: data.testtype,
                dateofsamplecollection: moment(
                    data.dateofsamplecollection
                ).toDate(),
                result: data.result,
                dateresultscollected: data.dateresultscollected
                    ? moment(
                        moment(data.dateresultscollected).toDate(),
                        'dd-MMM-yyyy'
                    ).toString()
                    : '',
                comments: data.comments,
                resultText: data.resultText,
                status: data.dateresultscollected ? 'complete' : 'pending'
            });

            this.dataSource = new MatTableDataSource(
                this.hiv_testing_history_data
            );
        });
    }

    public onRowClicked(row) {
        const index = this.hiv_testing_table_data.indexOf(row.milestone);
        this.hiv_testing_table_data.splice(index, 1);

        const indexHistory = this.hiv_testing_history_data.indexOf(
            row.milestone
        );
        this.hiv_testing_history_data.splice(indexHistory, 1);

        this.dataSource = new MatTableDataSource(this.hiv_testing_history_data);
    }

    public completeLabOrder(hivTestingTableData) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
            labOrderId: hivTestingTableData.labOrderId,
            labOrderTestId: hivTestingTableData.labOrderTestId,
            labTestId: hivTestingTableData.labTestId,
            testtype: hivTestingTableData.testtype,
            testResults: this.testResults
        };

        const dialogRef = this.dialog.open(
            HeiCompletelaborderComponent,
            dialogConfig
        );
        dialogRef.afterClosed().subscribe(data => {
            if (!data) {
                return;
            }

            const completeLabOrderCommand: CompleteLabOrderCommand = {
                LabOrderId: hivTestingTableData.labOrderId,
                LabOrderTestId: hivTestingTableData.labOrderTestId,
                LabTestId: hivTestingTableData.labTestId,
                UserId: 1,
                LabTestResults: [],
                DateResultsCollected: data.dateresultscollected
            };

            if (
                hivTestingTableData.testtype['itemName'] == '1st DNA PCR' ||
                hivTestingTableData.testtype['itemName'] == '2nd DNA PCR' ||
                hivTestingTableData.testtype['itemName'] == '3rd DNA PCR' ||
                hivTestingTableData.testtype['itemName'] ==
                'Repeat confirmatory PCR (for +ve)' ||
                hivTestingTableData.testtype['itemName'] ==
                'Confirmatory PCR (for  +ve)'
            ) {
                completeLabOrderCommand.LabTestResults.push({
                    ParameterId: this.pcrLabTestParameters[0]['id'],
                    ResultValue: null,
                    ResultText: data.result['itemName'],
                    ResultOptionId: null,
                    ResultOption: null,
                    ResultUnit: null,
                    ResultUnitId: null,
                    Undetectable: false,
                    DetectionLimit: this.pcrLabTestParameters[0][
                        'detectionLimit'
                    ]
                });
            } else if (
                hivTestingTableData.testtype['itemName'] ==
                'Baseline Viral Load (for +ve)'
            ) {
                completeLabOrderCommand.LabTestResults.push({
                    ParameterId: this.viralLoadLabTestParameters[0]['id'],
                    ResultValue: data.resultText,
                    ResultText: null,
                    ResultOptionId: null,
                    ResultOption: null,
                    ResultUnit: 'copies/ml',
                    ResultUnitId: this.viralLoadLabTestParameters[0]['unitId'],
                    Undetectable: false,
                    DetectionLimit: this.viralLoadLabTestParameters[0][
                        'detectionLimit'
                    ]
                });
            } else if (
                hivTestingTableData.testtype['itemName'] == 'Final Antibody'
            ) {
                completeLabOrderCommand.LabTestResults.push({
                    ParameterId: this.antibodyLabTestParameters[0]['id'],
                    ResultValue: data.result['itemName'] == 'Positive' ? 1 : 2,
                    ResultText: null,
                    ResultOptionId: null,
                    ResultOption: null,
                    ResultUnit: null,
                    ResultUnitId: this.antibodyLabTestParameters[0]['unitId'],
                    Undetectable: false,
                    DetectionLimit: this.antibodyLabTestParameters[0][
                        'detectionLimit'
                    ]
                });
            }

            this.heiservice.saveCompleteHeiLabOrder(completeLabOrderCommand)
                .subscribe(
                    res => {
                        this.hiv_testing_history_data = [];
                        this.snotifyService.success(
                            'Successfully Update HIV tests ',
                            'HEI',
                            this.notificationService.getConfig()
                        );
                        this.loadPatientCompletedTestTypes();
                        this.dataservice.labHasBeenCompleted(true);
                    },
                    error => {
                        this.snotifyService.error(
                            'An error occured while updating HEI hiv tests ',
                            'HEI',
                            this.notificationService.getConfig()
                        );
                    }
                );
        });
    }
}

export interface HivTestingTableData {
    testtype?: LookupItemView;
    dateofsamplecollection?: Date;
    result?: LookupItemView;
    dateresultscollected?: string;
    comments?: string;
    resultText: string;
    status: string;
    labOrderId?: number;
    labOrderTestId?: number;
    labTestId?: number;
}

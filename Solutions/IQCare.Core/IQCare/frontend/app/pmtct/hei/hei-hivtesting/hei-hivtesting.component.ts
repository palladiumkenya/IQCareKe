import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { HivtestingmodalComponent } from './hivtestingmodal/hivtestingmodal.component';
import * as moment from 'moment';

@Component({
    selector: 'app-hei-hivtesting',
    templateUrl: './hei-hivtesting.component.html',
    styleUrls: ['./hei-hivtesting.component.css']
})
export class HeiHivtestingComponent implements OnInit {
    hivTestType: LookupItemView[] = [];
    testResults: LookupItemView[] = [];
    maxDate: Date;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    public hiv_testing_history_data: HivTestingTableData[] = [];
    displayedColumns = ['testtype', 'dateofsamplecollection', 'result', 'dateresultscollected', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    @Input('heiHivTestingOptions') heiHivTestingOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<any> = new EventEmitter<any>();

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dialog: MatDialog,
        private heiservice: HeiService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        const { hivTestType, testResults } = this.heiHivTestingOptions[0];
        this.hivTestType = hivTestType.sort(function (a, b) { return a.itemId - b.itemId; });
        this.testResults = testResults;


        this.notify.emit(this.hiv_testing_table_data);

        this.loadHeiHivTests();
    }

    loadHeiHivTests(): void {
        this.heiservice.getLabOrderTestResults(this.patientId).subscribe(
            (res) => {
                console.log(res);
                for (let i = 0; i < res.length; i++) {
                    const testType = this.hivTestType.filter(obj => obj.itemName.includes(res[i].labTestName));
                    const testResultHistorical = this.testResults.filter(obj => obj.itemName.includes(res[i].resultTexts));
                    let resultValue = null;
                    let resultText = null;
                    if (testResultHistorical.length > 0) {
                        resultValue = testResultHistorical[0];
                    } else {
                        resultText = res[i].resultValues;
                    }

                    this.hiv_testing_history_data.push({
                        testtype: testType[0],
                        dateofsamplecollection: res[i].sampleDate,
                        result: resultValue,
                        dateresultscollected: res[i].resultDate,
                        comments: '',
                        resultText: resultText
                    });
                }

                this.dataSource = new MatTableDataSource(this.hiv_testing_history_data);
            }
        );
    }

    AddHivTests() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
            hivTestType: this.hivTestType,
            testResults: this.testResults
        };

        const dialogRef = this.dialog.open(HivtestingmodalComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                this.hiv_testing_table_data.push({
                    testtype: data.testtype,
                    dateofsamplecollection: moment(data.dateofsamplecollection).toDate(),
                    result: data.result,
                    dateresultscollected: moment(data.dateresultscollected).toDate(),
                    comments: data.comments,
                    resultText: data.resultText
                });

                this.dataSource = new MatTableDataSource(this.hiv_testing_table_data);
            }
        );
    }

    public onRowClicked(row) {
        const index = this.hiv_testing_table_data.indexOf(row.milestone);
        this.hiv_testing_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.hiv_testing_table_data);
    }
}

export interface HivTestingTableData {
    testtype?: LookupItemView;
    dateofsamplecollection?: Date;
    result?: LookupItemView;
    dateresultscollected?: Date;
    comments?: string;
    resultText: string;
}

import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { HivtestingmodalComponent } from './hivtestingmodal/hivtestingmodal.component';

@Component({
    selector: 'app-hei-hivtesting',
    templateUrl: './hei-hivtesting.component.html',
    styleUrls: ['./hei-hivtesting.component.css']
})
export class HeiHivtestingComponent implements OnInit {
    hivTestType: any[] = [];
    testResults: any[] = [];
    maxDate: Date;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    displayedColumns = ['testtype', 'dateofsamplecollection', 'result', 'dateresultscollected', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    @Input('heiHivTestingOptions') heiHivTestingOptions: any;
    @Output() notify: EventEmitter<any> = new EventEmitter<any>();

    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dialog: MatDialog) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        const { hivTestType, testResults } = this.heiHivTestingOptions[0];
        this.hivTestType = hivTestType.sort(function (a, b) { return a.itemId - b.itemId; });
        this.testResults = testResults;


        this.notify.emit(this.hiv_testing_table_data);
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
                    dateofsamplecollection: data.dateofsamplecollection,
                    result: data.result,
                    dateresultscollected: data.dateresultscollected,
                    comments: data.comments
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
    testtype?: string;
    dateofsamplecollection?: Date;
    result?: string;
    dateresultscollected?: Date;
    comments?: string;
}

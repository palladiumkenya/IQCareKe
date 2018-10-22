import { HivStatusComponent } from './../../hiv-status/hiv-status.component';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';

@Component({
    selector: 'app-pnc-hivtesting',
    templateUrl: './pnc-hivtesting.component.html',
    styleUrls: ['./pnc-hivtesting.component.css']
})
export class PncHivtestingComponent implements OnInit {
    public hiv_testing_table_data: HivTestingTableData[] = [];
    displayedColumns = ['testdate', 'testtype', 'kitname', 'lotnumber',
        'expirydate', 'testresult', 'finalresult', 'nexthivtest', 'testpoint', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    constructor(private dialog: MatDialog) { }

    ngOnInit() {
    }

    AddHivTests() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(HivStatusComponent, dialogConfig);
    }
}


export interface HivTestingTableData {
    testdate?: string;
    testtype?: string;
    kitname?: string;
    lotnumber?: string;
    expirydate?: Date;
    testresult?: string;
    finalresult?: string;
    nexthivtest?: Date;
    testpoint?: string;
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialogConfig, MatDialog } from '@angular/material';
import { CheckinComponent } from '../../checkin/checkin.component';

@Component({
    selector: 'app-pnc-encounters',
    templateUrl: './pnc-encounters.component.html',
    styleUrls: ['./pnc-encounters.component.css']
})
export class PncEncountersComponent implements OnInit {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns = ['visitdate', 'visittype', 'dateofdelivery', 'modeofdelivery', 'edit'];
    dataSource = new MatTableDataSource();

    constructor(private dialog: MatDialog) { }

    ngOnInit() {
    }

    pncCheckIn() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            section: 'pnc'
        };

        const dialogRef = this.dialog.open(CheckinComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                console.log(data);
            }
        );
    }
}

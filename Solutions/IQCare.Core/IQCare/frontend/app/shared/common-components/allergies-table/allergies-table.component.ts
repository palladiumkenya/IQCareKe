import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { AllergiesComponent } from '../allergies/allergies.component';

@Component({
    selector: 'app-allergies-table',
    templateUrl: './allergies-table.component.html',
    styleUrls: ['./allergies-table.component.css']
})
export class AllergiesTableComponent implements OnInit {
    public allergy_table_data: AllergyTableData[] = [];

    displayedColumns = ['allergy', 'reactionType', 'severity', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.allergy_table_data);

    constructor(private dialog: MatDialog) { }

    ngOnInit() {
    }

    newAllergies() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(AllergiesComponent, dialogConfig);
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

export interface AllergyTableData {
    allergy?: string;
    reactionType?: string;
    severity?: string;
    onsetDate?: Date;
    active?: string;
}

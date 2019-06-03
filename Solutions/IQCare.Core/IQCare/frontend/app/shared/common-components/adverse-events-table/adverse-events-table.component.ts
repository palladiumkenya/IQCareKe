import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';

@Component({
    selector: 'app-adverse-events-table',
    templateUrl: './adverse-events-table.component.html',
    styleUrls: ['./adverse-events-table.component.css']
})
export class AdverseEventsTableComponent implements OnInit {
    public allergy_table_data: AllergyTableData[] = [];

    displayedColumns = ['allergy', 'reactionType', 'severity', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.allergy_table_data);

    constructor(private dialog: MatDialog) { }

    ngOnInit() {
    }

}

export interface AllergyTableData {
    allergy?: string;
    reactionType?: string;
    severity?: string;
    onsetDate?: Date;
    active?: string;
}


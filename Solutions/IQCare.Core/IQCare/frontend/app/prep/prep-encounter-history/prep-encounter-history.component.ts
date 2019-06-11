import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { LookupItemView } from '../../shared/_models/LookupItemView';

@Component({
    selector: 'app-prep-encounter-history',
    templateUrl: './prep-encounter-history.component.html',
    styleUrls: ['./prep-encounter-history.component.css']
})
export class PrepEncounterHistoryComponent implements OnInit {
    personId: number;

    public adverse_events_table_data: AdverseEventsTableData[] = [];
    displayedColumns = ['adverseEvent', 'severity', 'medicine_causing', 'adverseEventsAction'];
    dataSource = new MatTableDataSource(this.adverse_events_table_data);

    constructor() {
        this.personId = 1;
    }

    ngOnInit() {
    }

}

export interface AdverseEventsTableData {
    adverseEvent?: LookupItemView;
    severity?: LookupItemView;
    medicine_causing?: string;
    adverseEventsAction?: LookupItemView;
    outcome?: boolean;
}

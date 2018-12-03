import { SnotifyService } from 'ng-snotify';
import { Component, OnInit, Inject, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatTableDataSource, MatPaginator } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-check-duplicates',
    templateUrl: './check-duplicates.component.html',
    styleUrls: ['./check-duplicates.component.css']
})
export class CheckDuplicatesComponent implements OnInit, AfterViewInit {
    title: string;
    displayedColumns: string[] = ['select', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 'gender'];
    dataSource = new MatTableDataSource();
    @ViewChild(MatPaginator) paginator: MatPaginator;
    selection: SelectionModel<any>;
    persons: any[] = [];

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<CheckDuplicatesComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.title = 'Check For Duplicates';

        this.persons = data.persons;
        this.dataSource.data = this.persons;

        const initialSelection = [];
        const allowMultiSelect = false;
        this.selection = new SelectionModel<any>(allowMultiSelect, initialSelection);
    }

    ngOnInit() {
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected == numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
        this.isAllSelected() ?
            this.selection.clear() :
            this.dataSource.data.forEach(row => this.selection.select(row));
    }

    save() {
        if (this.selection.selected.length == 0) {
            this.snotifyService.info('You have not selected any person', 'DUPLICATE SEARCH', this.notificationService.getConfig());
            return;
        } else {
            this.dialogRef.close(this.selection.selected);
        }
    }

    close() {
        this.dialogRef.close();
    }
}

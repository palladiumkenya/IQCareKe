import { Component, OnInit, Inject, AfterViewInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatPaginator, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { SearchService } from '../_services/search.service';
import { Search } from '../_models/search';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';

@Component({
    selector: 'app-inline-search',
    templateUrl: './inline-search.component.html',
    styleUrls: ['./inline-search.component.css']
})
export class InlineSearchComponent implements OnInit, AfterViewInit {
    title: string;
    form: FormGroup;
    displayedColumns: string[] = ['select', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 'gender'];
    dataSource = new MatTableDataSource();
    @ViewChild(MatPaginator) paginator: MatPaginator;

    selection: SelectionModel<any>;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<InlineSearchComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private searchService: SearchService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.title = 'Person Search';

        const initialSelection = [];
        const allowMultiSelect = false;
        this.selection = new SelectionModel<any>(allowMultiSelect, initialSelection);
    }

    ngOnInit() {
        this.form = this.fb.group({
            firstName: new FormControl(''),
            middleName: new FormControl(''),
            lastName: new FormControl(''),
        });
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

    findPerson() {
        const clientSearch = new Search();
        console.log(this.form.value);

        if (this.form.value.firstName && this.form.value.firstName != '') {
            clientSearch.firstName = this.form.value.firstName;
        }

        if (this.form.value.lastName && this.form.value.lastName != '') {
            clientSearch.lastName = this.form.value.lastName;
        }

        if (this.form.value.middleName && this.form.value.middleName != '') {
            clientSearch.middleName = this.form.value.middleName;
        }


        this.searchService.searchClient(clientSearch).subscribe(
            (res) => {
                this.dataSource.data = res['personSearch'];
            },
            (error) => {
                console.error(error);
                // this.snotifyService.error('Error searching ' + error, 'SEARCH', this.notificationService.getConfig());
            }
        );
    }

    getSelectedPerson() {
        if (this.selection.selected.length == 0) {
            this.snotifyService.info('You have not selected any person', 'SEARCH', this.notificationService.getConfig());
            return;
        } else {
            this.dialogRef.close(this.selection.selected);
        }
    }
}

import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {RecordsService} from '../_services/records.service';
import {MatchDuplicatePerson} from '../_models/matchduplicate';
import {MatTableDataSource} from '@angular/material/table';
import {NgxSpinnerService} from 'ngx-spinner';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {SelectionModel} from '@angular/cdk/collections';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import {PersoncontactsComponent} from '../person/personcontacts/personcontacts.component';
import {RecordsMergeComponent} from '../records-merge/records-merge.component';

@Component({
  selector: 'app-merge',
  templateUrl: './merge.component.html',
  styleUrls: ['./merge.component.css']
})
export class MergeComponent implements OnInit {
    MergeForm: FormGroup;
    
    displayedColumns = ['select', 'firstName', 'middleName', 'lastName', 'dateOfBirth', 
        'patientEnrollmentId', 'enrollmentDate', 'mobileNumber', 'ptn_Pk', 'patientId', 'personId', 'sex', 'groupingFilter'];
    dataSource = new MatTableDataSource();

    selection: SelectionModel<any>;
    
    constructor(private _formBuilder: FormBuilder,
                private recordsService: RecordsService,
                private spinner: NgxSpinnerService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private dialog: MatDialog) {
        const initialSelection = [];
        const allowMultiSelect = true;
        this.selection = new SelectionModel<any>(allowMultiSelect, initialSelection);
    }
    
    async ngOnInit() {
        this.MergeForm = this._formBuilder.group({
            firstName: new FormControl(true),
            middleName: new FormControl(''),
            lastName: new FormControl(true),
            sex: new FormControl(true),
            dob: new FormControl(true),
            identifier: new FormControl(''),
        });
    }
    
    async search() {
        const matchDuplicatePerson: MatchDuplicatePerson = {
            matchFirstName: this.MergeForm.get('firstName').value ? 1 : 0,
            matchMiddleName: this.MergeForm.get('middleName').value ? 1 : 0,
            matchLastname: this.MergeForm.get('lastName').value ? 1 : 0,
            matchSex: this.MergeForm.get('sex').value ? 1 : 0,
            matchDOB: this.MergeForm.get('dob').value ? 1 : 0
        };
        try {
            this.spinner.show();
            const result = await this.recordsService.getDuplicatePersons(matchDuplicatePerson).toPromise();
            this.dataSource = new MatTableDataSource(result);
            this.spinner.hide();
            this.snotifyService.success('Found possible duplicates ',
                'Registration', this.notificationService.getConfig());
        } catch (e) {
            this.snotifyService.error('An error occurred while trying to find duplicates ' + e.error.errors[0].message,
                'Registration', this.notificationService.getConfig());
            this.spinner.hide();
        }        
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

    /** The label for the checkbox on the passed row */
    checkboxLabel(row?: any): string {
        if (!row) {
            return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
        }
        return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
    }

    merge() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '90%';
        dialogConfig.width = '80%';
        
        dialogConfig.data = {
            selectedRecords: this.selection.selected
        };

        const dialogRef = this.dialog.open(RecordsMergeComponent, dialogConfig);
    }
}

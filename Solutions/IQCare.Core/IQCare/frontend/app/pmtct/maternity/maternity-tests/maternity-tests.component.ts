import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {MatTableDataSource} from '@angular/material';

@Component({
    selector: 'app-mternity-tests',
    templateUrl: './maternity-tests.component.html',
    styleUrls: ['./maternity-tests.component.css']
})
export class MaternityTestsComponent implements OnInit {

    maternityTestsFormGroup: FormGroup;
    maternityTestData: any[] = [];
    displayedColumns = ['testName', 'date', 'results', 'action'];
    dataSource = new MatTableDataSource(this.maternityTestData);

    constructor(private _formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.maternityTestsFormGroup = this._formBuilder.group({
            treatedSyphilis: new FormControl('', [Validators.required]),
            HIVStatusLastANC: new FormControl('', [Validators.required])
        });
    }
    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.maternityTestData.indexOf(row.testName);
        this.maternityTestData.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.maternityTestData);
    }
}

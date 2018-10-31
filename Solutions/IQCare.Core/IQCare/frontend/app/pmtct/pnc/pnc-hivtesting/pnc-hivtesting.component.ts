import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HivStatusComponent } from '../../anc/hiv-status/hiv-status.component';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-hivtesting',
    templateUrl: './pnc-hivtesting.component.html',
    styleUrls: ['./pnc-hivtesting.component.css']
})
export class PncHivtestingComponent implements OnInit {
    HivTestingForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];

    @Input('pncHivOptions') pncHivOptions: any;
    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();
    isHivTestingDone: boolean = false;

    public hiv_testing_table_data: HivTestingTableData[] = [];
    displayedColumns = ['testdate', 'testtype', 'kitname', 'lotnumber',
        'expirydate', 'testresult', 'nexthivtest', 'testpoint', 'action'];
    dataSource = new MatTableDataSource(this.hiv_testing_table_data);

    constructor(private dialog: MatDialog,
        private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            hivTestingDone: new FormControl('', [Validators.required]),
            testType: new FormControl('', [Validators.required]),
            finalTestResult: new FormControl('', [Validators.required])
        });

        this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
        this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

        const { yesnoOptions, hivFinalResultsOptions } = this.pncHivOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.hivFinalResultsOptions = hivFinalResultsOptions;

        this.notify.emit({ 'form': this.HivTestingForm, 'table_data': this.hiv_testing_table_data });
    }

    AddHivTests() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(HivStatusComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                this.hiv_testing_table_data.push({
                    testdate: new Date(),
                    testtype: data.hivTest,
                    kitname: data.kitName,
                    lotnumber: data.lotNumber,
                    expirydate: data.expiryDate,
                    testresult: data.testResult,
                    nexthivtest: data.nextAppointmentDate,
                    testpoint: 'PNC'
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

    onHivTestDoneChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.isHivTestingDone = true;
            this.HivTestingForm.controls['testType'].enable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].enable({ onlySelf: true });
        } else if (event.source.selected) {
            this.isHivTestingDone = false;
            this.HivTestingForm.controls['testType'].disable({ onlySelf: true });
            this.HivTestingForm.controls['finalTestResult'].disable({ onlySelf: true });

            // set default value to null
            this.HivTestingForm.controls['testType'].setValue('');
            this.HivTestingForm.controls['finalTestResult'].setValue('');
        }
    }
}


export interface HivTestingTableData {
    testdate?: Date;
    testtype?: any;
    kitname?: any;
    lotnumber?: string;
    expirydate?: Date;
    testresult?: any;
    nexthivtest?: Date;
    testpoint?: string;
}

import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog } from '@angular/material';
import { LaborderService } from '../../_services/laborder.service';
import { FormControlBase } from '../../../shared/_models/dynamic-form/FormControlBase';
import { ResultDataType } from '../../_models/CompleteLabOrderCommand';
import { AddLabResultComponent } from '../add-lab-result/add-lab-result.component';
import { TextboxFormControl, CheckboxFormControl, NumericTextboxFormControl } from '../../../shared/_models/dynamic-form/TextBoxFormControl';

@Component({
    selector: 'app-pending-labs-grid',
    templateUrl: './pending-labs-grid.component.html',
    styleUrls: ['./pending-labs-grid.component.css']
})
export class PendingLabsGridComponent implements OnInit {

    @Input('PatientId') PatientId: number;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    pendingLabsDataSource = new MatTableDataSource(this.pendingLabsDataSource);
    pendingLabTests: any[] = [];
    pending_labs_displaycolumns: any[] = ['test', 'orderReason', 'orderDate', 'status', 'action'];

    labTestParameters: any[] = [];
    formControlCollection: FormControlBase<any>[] = [];
    ResultDataType: ResultDataType;
    disableBtn: boolean = false;

    formControl: FormControlBase<any>[] = [];
    defaultUnitName = 'No Units';

    constructor(private labOrderService: LaborderService,
        private dialog: MatDialog) { }

    ngOnInit() {
        this.getPendingLabs(this.PatientId);
    }

    public getPendingLabs(patientId: number) {
        this.labOrderService.getLabTestResults(patientId, 'Pending').subscribe(
            res => {
                if (res.length == 0) {
                    return;
                }

                res.forEach(test => {
                    this.pendingLabTests.push({
                        labOrderTestId: test.labOrderTestId,
                        labOrderId: test.labOrderId,
                        test: test.labTestName,
                        orderDate: test.orderDate,
                        orderReason: test.orderReason == null || test.orderReason == '' ? 'N/A' : test.orderReason,
                        labTestId: test.labTestId,
                        unit: test.resultUnits,
                        resultDate: test.resultDate,
                        result: test.result,
                        status: test.resultStatus
                    });
                });
                this.pendingLabsDataSource = new MatTableDataSource(this.pendingLabTests);
                this.pendingLabsDataSource.paginator = this.paginator;

            },
            (error) => {
                console.log(error + 'An error occured while getting completed labs');
            });
    }

    public addResult(pendingTest: any) {
        this.disableBtn = true;

        this.formControlCollection = [];
        this.formControl = [];
        this.labTestParameters = [];

        this.labOrderService.getLabTestParameters(pendingTest.labTestId).subscribe(result => {
            if (result.length == 0) {
                return;
            }

            result.forEach(param => {
                this.formControl.push(this.labOrderService.getFormContolFromParam(param));

                this.formControl.push(
                    new TextboxFormControl({
                        key: 'ParameterName_' + param.id,
                        label: 'Parameter Name',
                        value: param.parameterName,
                        required: false,
                        order: 1,
                        disabled: true
                    }));

                this.formControl.push(new TextboxFormControl({
                    key: 'ResultUnit_' + param.id,
                    label: 'Result Unit',
                    value: param.unitName.toUpperCase() == this.defaultUnitName.toUpperCase() ? 'N/A' : param.unitName,
                    required: false,
                    order: 3,
                    disabled: true
                }));

                if (param.parameterName == 'Viral Load') {
                    this.formControl.push(new CheckboxFormControl({
                        key: 'Undetectable_' + param.id,
                        label: 'Undetectable',
                        value: false,
                        required: false,
                        order: 4
                    }));

                    this.formControl.push(new TextboxFormControl({
                        key: 'detectionLimit_' + param.id,
                        label: 'Detection Limit',
                        required: false,
                        order: 5,
                        value: null,
                        disabled: true,
                        pattern: '^\\d*\\.?\\d+$'
                    }));
                }

                this.labTestParameters.push({
                    Id: param.id,
                    ParamName: param.parameterName,
                    LabTestId: param.labTestId,
                    DataType: param.dataType,
                    UnitId: param.unitId,
                    unitName: param.unitName.toUpperCase() == this.defaultUnitName.toUpperCase() ? 'N/A' : param.unitName,
                    formControls: this.formControl.sort((a, b) => a.order - b.order)
                });

                this.formControlCollection = this.formControlCollection.concat(this.formControl);
                this.formControl = [];
            });

            const dialogConfig = new MatDialogConfig();
            dialogConfig.disableClose = true;
            dialogConfig.autoFocus = true;

            dialogConfig.data = {
                labTestParameters: this.labTestParameters,
                formControlCollection: this.formControlCollection,
                labTestId: pendingTest.labTestId,
                labOrderTestId: pendingTest.labOrderTestId,
                labOrderId: pendingTest.labOrderId
            };

            const dialogRef = this.dialog.open(AddLabResultComponent, dialogConfig);
            dialogRef.afterClosed().subscribe(
                data => {
                    if (!data) {
                        return;
                    }
                    console.log(data);
                });
        });
        this.disableBtn = false;
    }
}

import { Component, OnInit, Output, EventEmitter, Inject, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { LaborderService } from '../../_services/laborder.service';
import { CompleteLabOrderCommand, AddLabTestResultCommand, ResultDataType } from '../../_models/CompleteLabOrderCommand';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControlBase } from '../../../shared/_models/dynamic-form/FormControlBase';
import { FormControlService } from '../../../shared/_services/form-control.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-lab-result',
    templateUrl: './add-lab-result.component.html',
    styleUrls: ['./add-lab-result.component.css']
})
export class AddLabResultComponent implements OnInit {

    resultDataType: string;
    resultUnit: string;
    labTest: string;
    labOrderId: any;
    labTestId: any;
    labOrderTestId: any;
    userId: any;
    isText: boolean;
    isNumeric: boolean;
    isSelect: boolean;
    labTestResultOptions: any[];
    resultDataTypes: ResultDataType;
    labTestParameters: any[] = [];
    dialogTitle: string;
    formControlCollection: FormControlBase<any>[] = [];
    disabled = false;

    labResultForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    labTestResults: AddLabTestResultCommand[] = [];

    constructor(private labOrderService: LaborderService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dialogRef: MatDialogRef<AddLabResultComponent>,
        private formControlService: FormControlService,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.labTestParameters = data.labTestParameters;
        this.formControlCollection = data.formControlCollection;
        this.labOrderId = data.labOrderId;
        this.labTestId = data.labTestId;
        this.labOrderTestId = data.labOrderTestId;
        this.labResultForm = this.formControlService.toFormGroup(data.formControlCollection);

        this.resultDataTypes = new ResultDataType();
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.notify.emit(this.labResultForm);

        this.dialogTitle = 'Submit Lab Test Results';
    }

    ngOnInit() {
    }


    get isFormValid() {
        return !this.labResultForm.invalid;
    }

    public submitLabResult() {
        if (this.labResultForm.invalid) {
            return;
        }
        // console.log(JSON.stringify(this.labResultForm.getRawValue()));

        const completeLabCommand: CompleteLabOrderCommand = {
            LabOrderId: this.labOrderId,
            LabOrderTestId: this.labOrderTestId,
            LabTestId: this.labTestId,
            LabTestResults: this.labTestResults,
            UserId: this.userId,
            StrLabTestResults: JSON.stringify(this.labResultForm.getRawValue())
        };

        this.labOrderService.completeLabOrder(completeLabCommand).subscribe(res => {
            this.snotifyService.success('Lab test results submitted sucessfully', 'Lab', this.notificationService.getConfig());
            this.dialogRef.close();

        }, (err) => {
            this.snotifyService.error(err, 'Lab', this.notificationService.getConfig());
            console.log(err, 'Submit Lab');
        }, () => {
            location.reload();

        })

    }
    save() {
        if (!this.labResultForm.valid) {
            return;
        }
        this.dialogRef.close(this.labResultForm.value);
    }

    close() {
        this.dialogRef.close();
    }

    public OnChange(event, key: any) {
        const paramId = key.split('_');
        const detectionLimitControlName = 'detectionLimit_' + paramId[1];
        const resultValueControlName = 'ResultValue_' + paramId[1];

        if (this.disabled) {
            this.labResultForm.get(detectionLimitControlName).disable();
            this.labResultForm.get(resultValueControlName).enable();
        } else {
            this.labResultForm.get(detectionLimitControlName).enable();
            this.labResultForm.get(resultValueControlName).disable();

        }
    }

}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LabTestInfo, AddLabOrderCommand } from '../../_models/AddLabOrderCommand';
import { LaborderService } from '../../_services/laborder.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-lab-order',
    templateUrl: './lab-order.component.html',
    styleUrls: ['./lab-order.component.css']
})
export class LabOrderComponent implements OnInit {

    labOrderFormGroup: FormGroup;
    labTestData: any[];
    @Input() labTestReasons: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() notifyData: EventEmitter<any[]> = new EventEmitter<any[]>();

    labTestReasonOptions: any[];

    constructor(private formBuilder: FormBuilder,
        private labOrderService: LaborderService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {

        this.labOrderFormGroup = this.formBuilder.group({
            labTestId: new FormControl('', [Validators.required]),
            labtestReasonId: new FormControl('', [Validators.required]),
            labTestNotes: new FormControl('', [Validators.required]),
            orderDate: new FormControl('', [Validators.required]),
            clinicalOrderNotes: new FormControl('', [Validators.required])
        });
        this.notify.emit(this.labOrderFormGroup);
        this.notifyData.emit(this.labTestData);
    }

    ngOnInit() {
        this.labTestReasonOptions = this.labTestReasons;
    }



    public AddLabTest() {
        this.labTestData.push({
            testId: this.labOrderFormGroup.get('labTestId').value.id,
            test: this.labOrderFormGroup.get('labTestId').value.itemName,
            orderReason: this.labOrderFormGroup.get('labtestReasonId').value.itemName,
            orderReasonId: this.labOrderFormGroup.get('labtestReasonId').value.itemId,
            testNotes: this.labOrderFormGroup.get('labTestNotes').value
        });
    }


    public SubmitOrder() {
        if (this.labOrderFormGroup.invalid)
            return;

        const labTestInfo: LabTestInfo[] = [];

        this.labTestData.forEach(x => {
            labTestInfo.push({
                Id: x.testId,
                Notes: x.testNotes,
                LabTestName: x.test
            });

            const labOrderCommand: AddLabOrderCommand = {
                Ptn_Pk: 0,
                PatientId: 5,
                LocationId: 5,
                FacilityId: 4,
                VisitId: 3,
                ModuleId: 3,
                OrderedBy: 2,
                OrderDate: this.labOrderFormGroup.get('orderDate').value,
                ClinicalOrderNotes: this.labOrderFormGroup.get('orderNotes').value,
                CreateDate: new Date,
                OrderStatus: 'Pending',
                UserId: 2,
                PatientMasterVisitId: 3,
                LastTests: this.labTestData
            }

            this.labOrderService.addLabOrder(labOrderCommand).subscribe(
                res => {
                    console.log(`Add lab order info`);
                    console.log(res);
                    this.snotifyService.success('Successfully added lab order ', 'Lab', this.notificationService.getConfig());
                },
                (err) => {
                    console.log('An error occured while adding lab order command ' + err);
                    this.snotifyService.error('Error saving lab order ' + err, 'Lab', this.notificationService.getConfig());
                },
                () => {
                    this.snotifyService.success('Lab order details added successfully', 'Lab',
                        this.notificationService.getConfig());
                    this.labOrderFormGroup.reset();
                });

        })

    }

}

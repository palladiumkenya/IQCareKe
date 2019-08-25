import { Component, OnInit, Input, Output, EventEmitter, ViewChild, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LabTestInfo, AddLabOrderCommand } from '../../_models/AddLabOrderCommand';
import { LaborderService } from '../../_services/laborder.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import { EncounterService } from '../../../shared/_services/encounter.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { AddPatientOrdVisitCommand } from '../../../shared/_models/patientordvisit';
import { PatientMasterVisitEncounter } from '../../../pmtct/_models/PatientMasterVisitEncounter';
import { forkJoin } from 'rxjs';
import { MatPaginator, MatTableDataSource } from '@angular/material';

@Component({
    selector: 'app-lab-order',
    templateUrl: './lab-order.component.html',
    styleUrls: ['./lab-order.component.css']
})
export class LabOrderComponent implements OnInit {

    labOrderFormGroup: FormGroup;
    configuredLabTests: any[];
    labTestData: any[] = [];
    patientId: any;
    personId: any;
    patientInfo: any;
    userId: any;
    facilityId: any;
    ordVisitId: any;
    patientMasterVisitId: any;
    encounterType: any;
    serviceAreaId: any;
    labTestReasonOptions: any[];
    maxDate: Date;
    otherReason: boolean = false;
    disabled: boolean = true;

    @ViewChild(MatPaginator) paginator: MatPaginator;

    lab_test_displaycolumns = ['test', 'orderReason', 'testNotes', 'action'];
    dataSource = new MatTableDataSource(this.labTestData);

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    addedLabTestIds: any[] = [];

    constructor(private formBuilder: FormBuilder,
        private labOrderService: LaborderService,
        private personService: PersonHomeService,
        private encouterService: EncounterService,
        private lookUpService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        public zone: NgZone) {

        this.labOrderFormGroup = this.formBuilder.group({
            labTestId: new FormControl('', [Validators.required]),
            labtestReasonId: new FormControl({ value: '', disabled: true }, [Validators.required]),
            labTestNotes: new FormControl(''),
            orderDate: new FormControl('', [Validators.required]),
            clinicalOrderNotes: new FormControl(''),
            otherTestReason: new FormControl('')
        });
        this.notify.emit(this.labOrderFormGroup);
        this.dataSource = new MatTableDataSource(this.labTestData);
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.activatedRoute.params.subscribe(params => {
            this.patientId = params['patientId'];
            this.personId = params['personId'];
            localStorage.setItem('partnerId', this.personId);
            this.personService.getPatientById(this.patientId).subscribe(patient => {
                this.patientInfo = patient;
            });

            this.activatedRoute.data.subscribe(
                (data) => {
                    this.configuredLabTests = data['configuredLabTests'];
                    this.labTestReasonOptions = data['labTestReasonOptions']['lookupItems'];
                });
        });

        this.getEncounterType();
        this.getFacilty();
        this.getServiceArea();
    }

    public AddLabTest() {
        if (this.labOrderFormGroup.invalid) {
            return;
        }

        const labTestId = this.labOrderFormGroup.get('labTestId').value.id;
        const testName = this.labOrderFormGroup.get('labTestId').value.name;
        const labOrderFormControlValue = this.labOrderFormGroup.get('labtestReasonId').value;

        let labOrderReason = labOrderFormControlValue == null ? 'N/A' : labOrderFormControlValue.displayName;

        labOrderReason = labOrderReason === 'Other' ? this.labOrderFormGroup.get('otherTestReason').value : labOrderReason;

        if (this.addedLabTestIds.indexOf(labTestId) >= 0) {
            this.snotifyService.error(testName + ' test already added', 'Lab', this.notificationService.getConfig());
            return;
        }

        this.labTestData.push({
            testId: labTestId,
            test: testName,
            orderReason: labOrderReason,
            testNotes: this.labOrderFormGroup.get('labTestNotes').value
        });

        this.addedLabTestIds.push(labTestId);

        this.dataSource = new MatTableDataSource(this.labTestData);
        this.dataSource.paginator = this.paginator;
        console.log(this.labTestData);
    }


    public removeTest(row: any) {
        console.log('The index ' + this.labTestData.indexOf(row))
    }

    public SubmitOrder() {
        if (this.labOrderFormGroup.invalid) {
            return;
        }

        const ordVisitCommand = this.buildOrdVisitCommand();
        const submitOrdVisit = this.encouterService.savePatientOrdVisit(ordVisitCommand);

        const patientEncounter = this.buildPatientEncounterCommand();
        const submitPatientEncounter = this.encouterService.savePatientMasterVisit(patientEncounter);
        const labOrderCommand = this.buildLabOrderCommand();

        if (labOrderCommand.LabTests.length == 0) {
            this.snotifyService.error('Please add atleast one lab test before submitting the order', 'Lab',
                this.notificationService.getConfig());
            return;
        }

        forkJoin([
            submitOrdVisit,
            submitPatientEncounter
        ])
            .subscribe(
                (result) => {
                    labOrderCommand.VisitId = result[0]['visit_Id'];
                    labOrderCommand.PatientMasterVisitId = result[1]['patientMasterVisitId'];

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
                            this.zone.run(() => {
                                this.router.navigate(['/clinical/completeorder/' + this.patientId + '/' + this.personId],
                                    { relativeTo: this.activatedRoute });
                            });
                            this.labOrderFormGroup.reset();
                        });

                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {
                    console.log(`complete`);
                }
            );
    }

    public buildLabOrderCommand(): AddLabOrderCommand {
        const labTestInfo: LabTestInfo[] = [];

        this.labTestData.forEach(x => {
            labTestInfo.push({
                Id: x.testId,
                Notes: x.testNotes,
                LabTestName: x.test,
                OrderReason: x.orderReason
            });
        });
        const labOrderCommand: AddLabOrderCommand = {
            Ptn_Pk: this.patientInfo.ptn_pk,
            PatientId: this.patientInfo.id,
            LocationId: this.facilityId,
            VisitId: null,
            Module: 'Laboratory',
            OrderedBy: this.userId,
            OrderDate: this.labOrderFormGroup.get('orderDate').value,
            ClinicalOrderNotes: this.labOrderFormGroup.get('clinicalOrderNotes').value,
            CreateDate: new Date,
            OrderStatus: 'Pending',
            UserId: this.userId,
            PatientMasterVisitId: null,
            LabTests: labTestInfo
        };

        console.log('Lab order command ' + labOrderCommand);
        return labOrderCommand;

    }


    public buildOrdVisitCommand(): AddPatientOrdVisitCommand {
        const ordVisitCommand: AddPatientOrdVisitCommand = {
            Ptn_Pk: this.patientInfo.ptn_Pk,
            LocationId: this.facilityId,
            UserId: this.userId,
            VisitDate: new Date()
        };
        return ordVisitCommand;
    }


    public buildPatientEncounterCommand(): PatientMasterVisitEncounter {
        const encounterVisit: PatientMasterVisitEncounter = {
            EncounterDate: new Date,
            EncounterType: this.encounterType,
            PatientId: this.patientId,
            ServiceAreaId: this.serviceAreaId,
            UserId: this.userId
        };

        return encounterVisit;
    }

    public getFacilty() {
        this.lookUpService.getActiveFacility().subscribe(result => {
            this.facilityId = result.id;
        });
    }


    public getServiceArea() {
        this.personService.getServiceArea('ANC').subscribe(result => {
            console.log('Service Area ' + result);
            this.serviceAreaId = result.id;
        });
    }


    public getEncounterType() {
        this.lookUpService.getByGroupNameAndItemName('EncounterType', 'Lab-encounter').subscribe(result => {
            this.encounterType = result.ItemId;
        });
    }

    public labTestReasonChange(reason: any) {
        this.labOrderFormGroup.controls['otherTestReason'].reset();

        this.otherReason = reason.displayName === 'Other';
        if (this.otherReason) {
            this.labOrderFormGroup.controls['otherTestReason'].setValidators(Validators.required);
        }
        else {
            this.labOrderFormGroup.controls['otherTestReason'].clearValidators();
        }
        this.labOrderFormGroup.controls['otherTestReason'].updateValueAndValidity();

    }

    labTestWithOrderReasons: any[] = ['Viral Load', 'CD4'];
    public onLabTestChange(test: any) {
        const exists = this.labTestWithOrderReasons.filter(x => x == test.name).length >= 1
        this.labOrderFormGroup.controls['labtestReasonId'].reset();

        if (exists) {
            this.labOrderFormGroup.controls['labtestReasonId'].enable();
            return;
        }
        else {
            this.labOrderFormGroup.controls['labtestReasonId'].disable();
            this.labOrderFormGroup.controls['otherTestReason'].clearValidators();
            this.otherReason = false;
        }


    }

}



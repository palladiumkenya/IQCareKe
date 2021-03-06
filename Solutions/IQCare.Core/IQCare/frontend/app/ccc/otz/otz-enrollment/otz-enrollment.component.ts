import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MatTableDataSource} from '@angular/material/table';
import {LookupItemView} from '../../../shared/_models/LookupItemView';
import {OtzService} from '../../_services/otz.service';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import {ModulesCoveredComponent} from '../modules-covered/modules-covered.component';
import * as moment from 'moment';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {ActivatedRoute, Router} from '@angular/router';
import {EnrollmentService} from '../../../registration/_services/enrollment.service';
import {Enrollment} from '../../../registration/_models/enrollment';
import {OtzActivityFormCommand} from '../activity-form/activity-form.component';
import {RecordsService} from '../../../records/_services/records.service';
import {PersonHomeService} from '../../../dashboard/services/person-home.service';

@Component({
    selector: 'app-otz-enrollment',
    templateUrl: './otz-enrollment.component.html',
    styleUrls: ['./otz-enrollment.component.css'],
    providers: [EnrollmentService, PersonHomeService]
})
export class OtzEnrollmentComponent implements OnInit {
    OtzEnrollmentForm: FormGroup;
    yesNoOptions: LookupItemView[] = [];
    personId: number;
    patientId: number;
    serviceCode: string;
    serviceId: number;
    userId: number;
    maxDate: Date;
    cccEnrollmentDate: Date;
    serviceAreaIdentifiers: any;

    displayedColumns = ['module', 'dateCovered', 'action'];
    topics_table_data: TopicsTableData[] = [];
    dataSource = new MatTableDataSource(this.topics_table_data);
    
    constructor(private _formBuilder: FormBuilder,
                private otzService: OtzService,
                private dialog: MatDialog,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService,
                private route: ActivatedRoute,
                private enrollmentService: EnrollmentService,
                public zone: NgZone,
                private router: Router,
                private recordsService: RecordsService,
                private personHomeService: PersonHomeService) { }
    
    async ngOnInit() {
        this.OtzEnrollmentForm = this._formBuilder.group({
            enrollmentDate: new FormControl('', [Validators.required]),
            isTransferIn: new FormControl('', [Validators.required]),
            dateInitiallyEnrolled: new FormControl('', [Validators.required]),
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        const yesNoOptions = await this.otzService.getByGroupName('YesNo').toPromise();
        this.yesNoOptions = yesNoOptions['lookupItems'];
        
        this.route.params.subscribe(
            p => {
                const { personId, patientId, serviceCode, serviceId  } = p;
                this.personId = personId;
                this.serviceCode = serviceCode;
                this.serviceId = serviceId;
                this.patientId = patientId;
            }
        );


        const result = await this.personHomeService.getPatientEnrollmentDateByServiceAreaId(this.patientId, 1).toPromise();
        this.cccEnrollmentDate = result.enrollmentDate;

        this.serviceAreaIdentifiers = await this.personHomeService.getServiceAreaIdentifiers(this.serviceId).toPromise();
        this.maxDate = new Date();
    }

    onClientTransferInChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.OtzEnrollmentForm.get('dateInitiallyEnrolled').enable({ onlySelf: true });
            this.OtzEnrollmentForm.get('dateInitiallyEnrolled').setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.OtzEnrollmentForm.get('dateInitiallyEnrolled').disable({ onlySelf: true });
            this.OtzEnrollmentForm.get('dateInitiallyEnrolled').setValue('');
        }
    }

    NewTopicCovered() {
        const otzEnrollmentDate = this.OtzEnrollmentForm.get('enrollmentDate').value;
        const dateInitiallyEnrolled = this.OtzEnrollmentForm.get('dateInitiallyEnrolled').value;
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            otzEnrollmentDate: dateInitiallyEnrolled ? dateInitiallyEnrolled : otzEnrollmentDate
        };

        const dialogRef = this.dialog.open(ModulesCoveredComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const topicData = this.topics_table_data.filter(obj => obj.moduleCompleted.itemName == data.topic.itemName
                    && moment(obj.dateCompleted).format('YYYY-MM-DD') == moment(data.dateCompleted).format('YYYY-MM-DD'));
                if (topicData.length > 0) {
                    this.snotifyService.info(data.topic.itemName + ' already exists for date '
                        + moment(data.dateCompleted).format('YYYY-MM-DD'), 'OTZ Topics',
                        this.notificationService.getConfig());
                    return;
                }

                this.topics_table_data.push({
                    dateCompleted: data.dateCompleted,
                    moduleCompleted: data.topic
                });

                this.dataSource = new MatTableDataSource(this.topics_table_data);
            }
        );
    }

    onRowClicked(row) {
        this.topics_table_data = this.topics_table_data.filter(obj => obj != row);
        this.dataSource = new MatTableDataSource(this.topics_table_data);
    }

    async validate() {
        if (this.OtzEnrollmentForm.valid) {
            const noOption = this.yesNoOptions.filter(obj => obj.itemName == 'No');
            
            const saveCommand: OtzActivityFormCommand = {
                UserId: this.userId,
                PatientId: this.patientId,
                ServiceId: this.serviceId,
                OtzActivity: [],
                AttendedSupportGroup: noOption[0].itemId,
                Remarks: null,
                VisitDate: this.OtzEnrollmentForm.value['enrollmentDate']
            };
            for (let i = 0; i < this.topics_table_data.length; i++) {
                saveCommand.OtzActivity.push({
                    TopicId: this.topics_table_data[i].moduleCompleted.itemId,
                    DateCompleted: this.topics_table_data[i].dateCompleted
                });
            }
            
            if (saveCommand.OtzActivity.length > 0) {
                const otzResult = await this.otzService.saveOtzActivityForm(saveCommand).toPromise();
            }            
            
            const enrollment = new Enrollment();
            const enrollmentNo = Math.random().toString(36).slice(5);
            enrollment.CreatedBy = this.userId;
            enrollment.DateOfEnrollment = this.OtzEnrollmentForm.value['enrollmentDate'];
            enrollment.EnrollmentNo = enrollmentNo;
            enrollment.PatientId = this.patientId;
            enrollment.PersonId = this.personId;
            enrollment.PosId = localStorage.getItem('appPosID');
            enrollment.RegistrationDate = this.OtzEnrollmentForm.value['enrollmentDate'];
            enrollment.ServiceAreaId = this.serviceId;
            enrollment.transferIn = true;
            enrollment.ServiceIdentifiersList.push({
                'IdentifierId': this.serviceAreaIdentifiers['serviceAreaIdentifiers'][0]['identifierId'],
                'IdentifierValue': enrollmentNo
            });
            
            try {
                const result = await this.enrollmentService.enrollClient(enrollment).toPromise();

                this.zone.run(() => {
                    this.router.navigate(['/ccc/encounterHistory/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                        { relativeTo: this.route });
                });
            } catch (e) {
                console.log(e);
            }
        }
    }
}

export interface TopicsTableData {
    moduleCompleted?: any;
    dateCompleted?: Date;
}

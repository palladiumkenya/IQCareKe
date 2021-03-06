import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MatTableDataSource} from '@angular/material/table';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import {ModulesCoveredComponent} from '../modules-covered/modules-covered.component';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../../shared/_services/notification.service';
import * as moment from 'moment';
import {OtzService} from '../../_services/otz.service';
import {LookupItemView} from '../../../shared/_models/LookupItemView';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
    selector: 'app-activity-form',
    templateUrl: './activity-form.component.html',
    styleUrls: ['./activity-form.component.css'],
    providers: [ OtzService ]
})
export class ActivityFormComponent implements OnInit {
    OtzActivityForm: FormGroup;
    maxDate: Date;
    providers: any[] = [];
    yesNoOptions: LookupItemView[] = [];
    userId: number;
    patientId: number;
    serviceId: number;
    personId: number;
    id: number;
    EnrollmentDate: Date;

    displayedColumns = ['module', 'dateCovered', 'action'];    
    topics_table_data: TopicsTableData[] = [];
    dataSource = new MatTableDataSource(this.topics_table_data);

    allModulesDisplayedColumns = ['topic', 'dateCompleted'];
    allModulesTableData: any[] = [];
    allModulesDataSource = new MatTableDataSource(this.allModulesTableData);
    
    constructor(private _formBuilder: FormBuilder,
                private dialog: MatDialog,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private otzService: OtzService,
                private route: ActivatedRoute,
                public zone: NgZone,
                private router: Router) {
        this.maxDate = new Date();
    }
    
    async ngOnInit() {
        this.OtzActivityForm = this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            otzEnrollmentDate: new FormControl('', [Validators.required]),
            attendedSupportGroup: new FormControl('', [Validators.required]),
            provider: new FormControl('', [Validators.required]),
            remarks: new FormControl(''),
        });
        
        let providers = await this.otzService.getProviders().toPromise();
        providers = providers.filter(obj => obj.userName != 'Admin');
        providers = providers.sort((t1, t2) => {
            const name1 = t1.userFirstName.toLowerCase();
            const name2 = t2.userFirstName.toLowerCase();
            if (name1 > name2) { return 1; }
            if (name1 < name2) { return -1; }
            return 0;
        });
        this.providers = providers;
        
        const yesNoOptions = await this.otzService.getByGroupName('YesNo').toPromise();
        this.yesNoOptions = yesNoOptions['lookupItems'];

        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.route.params.subscribe(
            p => {
                const { patientId, personId, serviceId, id } = p;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceId = serviceId;
                this.id = id;
            }
        );
        
        if (this.id) {
            const result = await this.otzService.getOtzActivityForm(this.id).toPromise();
            this.OtzActivityForm.get('visitDate').setValue(result.visitDate);
            this.OtzActivityForm.get('remarks').setValue(result.remarks);
            const  attendedSupportGroup = this.yesNoOptions.filter(obj => obj.itemName == result.attendedSupportGroup);
            this.OtzActivityForm.get('attendedSupportGroup').setValue(attendedSupportGroup[0]);
            const  provider = this.providers.filter(obj => obj.userFirstName + ' ' + obj.userLastName == result.provider);
            this.OtzActivityForm.get('provider').setValue(provider[0]);
            
            const otzEnrollment = await this.otzService.getOtzEnrollment(result.patientId, this.serviceId).toPromise();
            if (otzEnrollment) {
                this.OtzActivityForm.get('otzEnrollmentDate').setValue(otzEnrollment.enrollmentDate);
                this.OtzActivityForm.get('otzEnrollmentDate').disable({onlySelf: true});
                this.EnrollmentDate = otzEnrollment.enrollmentDate;
            }

            const completedOtzModules = await this.otzService.getOtzCompletedModules(result.patientId).toPromise();
            this.allModulesTableData = completedOtzModules;
            this.allModulesDataSource = new MatTableDataSource(this.allModulesTableData);
        } else {
            const otzEnrollment = await this.otzService.getOtzEnrollment(this.patientId, this.serviceId).toPromise();
            if (otzEnrollment) {
                this.OtzActivityForm.get('otzEnrollmentDate').setValue(otzEnrollment.enrollmentDate);
                this.OtzActivityForm.get('otzEnrollmentDate').disable({onlySelf: true});
                this.EnrollmentDate = otzEnrollment.enrollmentDate;
            }
        }
    }

    async validate() {
        if (this.OtzActivityForm.valid) {
            const saveCommand: OtzActivityFormCommand = {
                UserId: this.OtzActivityForm.value.provider.userID,
                PatientId: this.patientId,
                ServiceId: this.serviceId,
                OtzActivity: [],
                AttendedSupportGroup: this.OtzActivityForm.value.attendedSupportGroup.itemId,
                Remarks: this.OtzActivityForm.value.remarks,
                VisitDate: this.OtzActivityForm.value.visitDate
            };
            for (let i = 0; i < this.topics_table_data.length; i++) {
                saveCommand.OtzActivity.push({
                    TopicId: this.topics_table_data[i].moduleCompleted.itemId,
                    DateCompleted: this.topics_table_data[i].dateCompleted
                });
            }
            try {
                const result = await this.otzService.saveOtzActivityForm(saveCommand).toPromise();
                this.zone.run(() => {
                    this.router.navigate(['/ccc/encounterHistory/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                        { relativeTo: this.route });
                });
            } catch (e) {
                console.log(e);
            }            
        }
    }

    NewTopicCovered() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            otzEnrollmentDate: this.EnrollmentDate
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

    close() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/encounterHistory/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                { relativeTo: this.route });
        });
    }
}

export interface TopicsTableData {
    moduleCompleted?: LookupItemView;
    dateCompleted?: Date;
}

export interface OtzActivityFormCommand {
    OtzActivity: OtzActivity[];
    AttendedSupportGroup: number;
    Remarks: string;
    VisitDate: Date;
    UserId: number;
    PatientId: number;
    ServiceId: number;
}

export interface OtzActivity {
    TopicId: number;
    DateCompleted: Date;
}

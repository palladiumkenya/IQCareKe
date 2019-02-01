import { MilestonesFormComponent } from './milestones-form/milestones-form.component';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NotificationService } from '../../../shared/_services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { MilestoneTableData } from '../../_models/hei/MilestoneTableData';
import { MilestoneData } from '../../_models/hei/MilestoneData';

@Component({
    selector: 'app-milestones',
    templateUrl: './milestones.component.html',
    styleUrls: ['./milestones.component.css']
})
export class MilestonesComponent implements OnInit {

    // milestonesFormGroup: FormGroup;
    milestone_data: MilestoneData[] = [];

    @Input('milestoneOptions') milestoneOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<Object> = new EventEmitter<Object>();

    milestoneassessments: LookupItemView[] = [];
    milestonestatuses: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    milestone_table_data: MilestoneTableData[] = [];


    displayedColumns = ['Milestone', 'Date Assessed', 'Achieved', 'status', 'Comment', 'action'];
    dataSource = new MatTableDataSource(this.milestone_table_data);

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private heiservice: HeiService,
        private dialog: MatDialog) { }

    ngOnInit() {
        /*this.milestonesFormGroup = this._formBuilder.group({
            milestoneAssessed: new FormControl('', [Validators.required]),
            dateAssessed: new FormControl('', [Validators.required]),
            achieved: new FormControl('', [Validators.required]),
            status: new FormControl('', [Validators.required]),
            comment: new FormControl('')
        });*/

        const {
            assessed,
            status,
            yesnoOption } = this.milestoneOptions[0];
        this.milestoneassessments = assessed;
        this.milestonestatuses = status;
        this.yesnoOptions = yesnoOption;


        this.notify.emit({ /*'form': this.milestonesFormGroup,*/ 'data': this.milestone_data });

        this.loadMilestones();
    }

    loadMilestones(): any {
        this.heiservice.getMilestoneHistory(this.patientId).subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    const milestone = this.milestoneassessments.filter(obj => obj.itemId == result[i].typeAssessedId);
                    const yesOrNo = result[i].achievedId ? 'Yes' : 'No';
                    const yesnoOption = this.yesnoOptions.filter(obj => obj.itemName == yesOrNo);
                    const status = this.milestonestatuses.filter(obj => obj.itemId == result[i].statusId);

                    this.milestone_table_data.push({
                        milestone: milestone.length > 0 ? milestone[0].itemName : '',
                        dateAssessed: result[i].dateAssessed,
                        achieved: yesnoOption[0].itemName,
                        status: status[0].itemName,
                        comment: result[i].comment
                    });
                }

                this.dataSource = new MatTableDataSource(this.milestone_table_data);
            },
            (error) => {
                this.snotifyService.error('Error fethcing milestones ' + error, 'HEI Milestones', this.notificationService.getConfig());
            },
            () => { }
        );
    }

    public AddMilestone() {

    }

    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.milestone_table_data.indexOf(row.milestone);
        this.milestone_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.milestone_table_data);
    }

    public NewMilestone() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            milestoneassessments: this.milestoneassessments,
            milestonestatuses: this.milestonestatuses,
            yesnoOptions: this.yesnoOptions
        };

        const dialogRef = this.dialog.open(MilestonesFormComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                // console.log(data);

                const milestone = data.milestoneAssessed.itemName;
                if (this.milestone_table_data.filter(x => x.milestone === milestone).length > 0) {
                    this.snotifyService.warning('' + milestone + ' exists', 'HEI Milestones', this.notificationService.getConfig());
                } else {
                    this.milestone_table_data.push({
                        milestone: data.milestoneAssessed.itemName,
                        dateAssessed: new Date(data.dateAssessed),
                        achieved: data.achieved.itemName,
                        status: data.status.itemName,
                        comment: data.comment
                    });
                    this.milestone_data.push({
                        milestoneId: data.milestoneAssessed.itemId,
                        dateAssessed: new Date(data.dateAssessed),
                        achievedId: data.achieved.itemId,
                        statusId: data.status.itemId,
                        comment: data.comment
                    });

                    console.log(this.milestone_table_data);
                    this.dataSource = new MatTableDataSource(this.milestone_table_data);
                }
            });
    }
}



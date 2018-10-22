import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NotificationService} from '../../../shared/_services/notification.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {MatTableDataSource} from '@angular/material';
import {MilestoneTableData} from '../../_models/hei/MilestoneTableData';
import {MilestoneData} from '../../_models/hei/MilestoneData';

@Component({
  selector: 'app-milestones',
  templateUrl: './milestones.component.html',
  styleUrls: ['./milestones.component.css']
})
export class MilestonesComponent implements OnInit {

    milestonesFormGroup: FormGroup;
    milestone_data: MilestoneData[] = [];

    @Input('milestoneOptions') milestoneOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() milestonData: EventEmitter<MilestoneData[]> = new EventEmitter<MilestoneData[]>();

    milestoneassessments: any[] = [];
    milestonestatuses: any[] = [];
    yesnoOptions: any[] = [];
    milestone_table_data: MilestoneTableData[] = [];


    displayedColumns = ['Milestone', 'Date Assessed', 'Achieved', 'status', 'Comment', 'action'];
    dataSource = new MatTableDataSource(this.milestone_table_data);

  constructor(private _formBuilder: FormBuilder,
              private _lookupItemService: LookupItemService,
              private snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.milestonesFormGroup = this._formBuilder.group({
          milestoneAssessed: new FormControl('', [Validators.required]),
          dateAssessed: new FormControl('', [Validators.required]),
          achieved: new FormControl('', [Validators.required]),
          status: new FormControl('', [Validators.required]),
          comment: new FormControl('', [Validators.required])
      });

      const {
          assessed,
          status ,
          yesnoOption} = this.milestoneOptions[0];
      this.milestoneassessments = assessed;
      this.milestonestatuses = status;
      this.yesnoOptions = yesnoOption;


      this.notify.emit(this.milestonesFormGroup);
  }

  public AddMilestone() {
      console.log(this.milestonesFormGroup.value);
        const milestone = this.milestonesFormGroup.get('milestoneAssessed').value.itemName;
      if (this.milestone_table_data.filter(x => x.milestone === milestone).length > 0) {
          this.snotifyService.warning('' + milestone + ' exists', 'HEI Milestones', this.notificationService.getConfig());
      } else {
          this.milestone_table_data.push({
            milestone: this.milestonesFormGroup.controls['milestoneAssessed'].value.itemName,
            dateAssessed: new Date(this.milestonesFormGroup.controls['dateAssessed'].value),
            achieved: this.milestonesFormGroup.controls['achieved'].value.itemName,
            status: this.milestonesFormGroup.controls['status'].value.itemName,
            comment: this.milestonesFormGroup.controls['comment'].value
          });
          this.milestone_data.push({
              milestoneId: this.milestonesFormGroup.controls['milestoneAssessed'].value.itemId,
          dateAssessed: new Date(this.milestonesFormGroup.controls['dateAssessed'].value),
          achievedId: this.milestonesFormGroup.controls['achieved'].value.itemId ,
          statusId: this.milestonesFormGroup.controls['status'].value.itemName,
          comment:  this.milestonesFormGroup.controls['comment'].value
          });

          console.log(this.milestone_table_data);
          this.dataSource = new MatTableDataSource(this.milestone_table_data);
      }
  }
    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.milestone_table_data.indexOf(row.milestone);
        this.milestone_table_data.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.milestone_table_data);
    }
}

/*
export interface MilestoneTableData {
  milestone?: string;
  dateAssessed?: Date;
  achieved?: string;
  status?: string;
  comment?: string;
}

export interface MilestoneData {
    milestoneId?: number;
    dateAssessed?: Date;
    achievedId?: number;
    statusId?: number;
    comment?: string;
}
*/

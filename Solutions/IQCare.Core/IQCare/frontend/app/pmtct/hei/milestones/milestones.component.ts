import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {NotificationService} from '../../../shared/_services/notification.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';

@Component({
  selector: 'app-milestones',
  templateUrl: './milestones.component.html',
  styleUrls: ['./milestones.component.css']
})
export class MilestonesComponent implements OnInit {

    milestonesFormGroup: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
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

      this.notify.emit(this.milestonesFormGroup);
  }

}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-hei-outcome',
  templateUrl: './hei-outcome.component.html',
  styleUrls: ['./hei-outcome.component.css']
})

export class HeiOutcomeComponent implements OnInit {
  @Input() heiOutcomeOptions: any[] = [];

  HeiOutcomeFormGroup: FormGroup;

  @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

  constructor(private _formBuilder: FormBuilder,
    private _lookupItemService: LookupItemService,
    private notificationService: NotificationService,
    private snotifyService: SnotifyService) { }

  ngOnInit() {
    this.HeiOutcomeFormGroup = this._formBuilder.group({
      heiOutcomeOptions: new FormControl('', [Validators.required]),
    });

    this.notify.emit(this.HeiOutcomeFormGroup);
  }

}

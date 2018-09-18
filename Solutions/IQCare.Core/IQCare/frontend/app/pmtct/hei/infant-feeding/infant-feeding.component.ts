import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-infant-feeding',
  templateUrl: './infant-feeding.component.html',
  styleUrls: ['./infant-feeding.component.css']
})

export class InfantFeedingComponent implements OnInit {
    @Input() infantFeedingOptions: any[] = [];

    InfantFeedingFormGroup: FormGroup;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService) { }

  ngOnInit() {
      this.InfantFeedingFormGroup = this._formBuilder.group({
          infantFeedingOptions: new FormControl('', [Validators.required]),
      });

      this.notify.emit(this.InfantFeedingFormGroup);
  }

}

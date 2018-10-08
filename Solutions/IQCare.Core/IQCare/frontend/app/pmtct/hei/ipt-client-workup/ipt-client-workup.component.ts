
import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-ipt-client-workup',
  templateUrl: './ipt-client-workup.component.html',
  styleUrls: ['./ipt-client-workup.component.css']
})
export class IptClientWorkupComponent implements OnInit {

    public IPTClientWorkupFormGroup: FormGroup;
    public title: string;
    public yesnoOptions: any[] = [];

    @Input('IPTClientWorkupOptions') IPTClientWorkupOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  constructor(private _formBuilder: FormBuilder,
              private _lookupItemService: LookupItemService,
              private snotifyService: SnotifyService,
              private notificationService: NotificationService,
              public dialogRef: MatDialogRef<IptClientWorkupComponent>,
              @Inject(MAT_DIALOG_DATA) data) {
      this.title = 'IPT Client Workup';
  }

  ngOnInit() {

      this.IPTClientWorkupFormGroup = this._formBuilder.group({
          yellowColouredUrine: new FormControl('', [Validators.required]),
          numbness: new FormControl('', [Validators.required]),
          yellowEyes: new FormControl('', [Validators.required]),
          tenderness: new FormControl('', [Validators.required]),
          liverFunctionTest: new FormControl('', [Validators.required]),
          dateIPTStarted: new FormControl('', [Validators.required])
      });

      const {
          yesnoOption
      } = this.IPTClientWorkupOptions[0];
      this.yesnoOptions = yesnoOption;
  }

}

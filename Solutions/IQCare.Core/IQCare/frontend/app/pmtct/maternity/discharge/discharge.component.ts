import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'app-discharge',
    templateUrl: './discharge.component.html',
    styleUrls: ['./discharge.component.css']
})
export class DischargeComponent implements OnInit {
    dischargeFormGroup: FormGroup;

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.dischargeFormGroup = this.formBuilder.group({
            dischargeDate: new FormControl('', [Validators.required]),
            babyStatus: new FormControl('', [Validators.required])

        });
    }

}

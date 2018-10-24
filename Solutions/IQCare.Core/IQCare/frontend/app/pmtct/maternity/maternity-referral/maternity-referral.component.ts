import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'app-maternity-referral',
    templateUrl: './maternity-referral.component.html',
    styleUrls: ['./maternity-referral.component.css']
})
export class MaternityReferralComponent implements OnInit {

    referralFormGroup: FormGroup;

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.referralFormGroup = this.formBuilder.group({
            referredFrom: new FormControl('', [Validators.required]),
            referredTo: new FormControl('', [Validators.required])

        });
    }

}

import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Subscription } from 'rxjs';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';

@Component({
    selector: 'app-checkin',
    templateUrl: './checkin.component.html',
    styleUrls: ['./checkin.component.css']
})
export class CheckinComponent implements OnInit {
    title: string;
    form: FormGroup;
    section: string;

    public lookupItems$: Subscription;
    public visitTypes: any[] = [];

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<CheckinComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.title = 'Check In';
        this.section = data.section;
        switch (this.section) {
            case 'hei':
                this.getLookupItems('HEIVisitType', this.visitTypes);
                break;
            case 'maternity':
                this.getLookupItems('ANCVisitType', this.visitTypes);
                break;
            case 'pnc':
                this.getLookupItems('PNCVisitType', this.visitTypes);
                break;
            case 'anc':
                this.getLookupItems('ANCVisitType', this.visitTypes);
                break;
            default:
        }
    }

    ngOnInit() {
        this.form = this.fb.group({
            visitType: new FormControl('', [Validators.required]),
            visitDate: new FormControl('', [Validators.required]),
        });
    }

    save() {
        if (this.form.valid) {
            this.dialogRef.close(this.form.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }

    public getLookupItems(groupName: string, _options: any[]) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItems$);
                });
    }
}

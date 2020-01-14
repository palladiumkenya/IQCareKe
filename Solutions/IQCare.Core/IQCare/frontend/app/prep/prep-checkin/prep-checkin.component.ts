import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Subscription } from 'rxjs';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import * as moment from 'moment';

@Component({
    selector: 'app-prep-checkin',
    templateUrl: './prep-checkin.component.html',
    styleUrls: ['./prep-checkin.component.css']
})
export class PrepCheckinComponent implements OnInit {
    form: FormGroup;
    title: string;
    maxDate: Date;
    prepEncounterDate: Date;
    public visitTypes: any[] = [];
    public lookupItems$: Subscription;

    constructor(private fb: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dialogRef: MatDialogRef<PrepCheckinComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'PrEP Check-in';
        this.maxDate = new Date();
    }

    ngOnInit() {
        //  this.getLookupItems();
        this.form = this.fb.group({
            //  visitType: new FormControl('', [Validators.required]),
            visitdate: new FormControl('', [Validators.required])
        });



        if (localStorage.getItem('PrepVisitDate') != null && localStorage.getItem('PrepVisitDate') != undefined) {
            this.form.controls.visitdate.setValue(moment(localStorage.getItem('PrepVisitDate')).toDate());
        }
    }

    save() {
        if (this.form.valid) {
            this.dialogRef.close(this.form.value);
        } else {
            return;
        }

    }

    public getLookupItems() {


        this.lookupItems$ = this._lookupItemService.getByGroupName('PREPVisitType')
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        this.visitTypes.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    // console.log(this.lookupItems$);
                });
    }



    close() {
        this.dialogRef.close();
    }

}

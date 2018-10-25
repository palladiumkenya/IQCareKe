import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {MatTableDataSource} from '@angular/material';

@Component({
    selector: 'app-baby',
    templateUrl: './baby.component.html',
    styleUrls: ['./baby.component.css']
})
export class BabyComponent implements OnInit {

    babyFormGroup: FormGroup;
    babyData: any[] = [];
    displayedColumns = ['sex', 'birthWeight', 'outcome', 'apgarScore', 'resuscitation', 'deformity', 'teo', 'breastFeeding', 'comment',
        'action'];
    dataSource = new MatTableDataSource(this.babyData);

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.babyFormGroup = this.formBuilder.group({
            babySex: new FormControl('', [Validators.required]),
            birthWeight: new FormControl('', [Validators.required]),
            outcome: new FormControl('', [Validators.required]),
            resuscitationDone: new FormControl('', [Validators.required]),
            deformity: new FormControl('', [Validators.required]),
            teoGiven: new FormControl('', [Validators.required]),
            breastFed: new FormControl('', [Validators.required]),

            agparScore1min: new FormControl('', [Validators.required]),
            agparScore5min: new FormControl('', [Validators.required]),
            agparScore10min: new FormControl('', [Validators.required]),
            notificationNumber: new FormControl('', [Validators.required]),
            comment: new FormControl('', [Validators.required])

        });
    }

    public AddBaby() {

        this.babyData.push({
            sex: this.babyFormGroup.get('babySex').value.itemName,
            birthWight: this.babyFormGroup.controls['birthWeight'].value,
            outcome: this.babyFormGroup.get('outcome').value.itemName,
            apgarScore: this.babyFormGroup.get('agparScore1min').value + ',' + this.babyFormGroup.get('agparScore5min').value + ',' +
            this.babyFormGroup.get('agparScore10min').value,
            resuscitate: this.babyFormGroup.controls['resuscitationDone'].value.itemName,
            deformity:  this.babyFormGroup.controls['deformity'].value.itemName,
            teo:  this.babyFormGroup.controls['teo'].value.itemName,
            breastFeeding:  this.babyFormGroup.controls['breastFeeding'].value.itemName,
        });
    }

    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.babyData.indexOf(row.milestone);
        this.babyData.splice(index, 1);
        this.dataSource = new MatTableDataSource(this.babyData);
    }


}

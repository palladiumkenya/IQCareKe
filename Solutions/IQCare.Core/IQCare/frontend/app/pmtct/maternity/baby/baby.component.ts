import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
    babyDataTable: any[] = [];
    displayedColumns = ['sex', 'birthWeight', 'outcome', 'apgarScore', 'resuscitation', 'deformity', 'teo', 'breastFeeding', 'comment',
        'action'];
    dataSource = new MatTableDataSource(this.babyData);
    @Input() babySectionOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() notifyData: EventEmitter<any[]> = new EventEmitter<any[]>();

    public genderOptions: any[] = [];
    deliveryOutcomeOptions: any[] = [];
    public birthOutcomes: any[] = [];
    yesnoOptions: any[] = [];

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

        const {
            gender,
            deliveryOutcomes,
            birthOutcomes,
            yesNos
        } = this.babySectionOptions[0];
        this.genderOptions = gender;
        this.deliveryOutcomeOptions = deliveryOutcomes;
        this.birthOutcomes = birthOutcomes;
        this.yesnoOptions = yesNos;

        this.notify.emit(this.babyFormGroup);
        this.notifyData.emit(this.babyData);


    }

    public AddBaby() {

        if (this.babyFormGroup.get('babySex').value.itemId == '' && this.babyFormGroup.get('birthWeight').value == '') {

        } else {
            this.babyData.push({
                sex: this.babyFormGroup.get('babySex').value.itemId,
                birthWeight: this.babyFormGroup.get('birthWeight').value,
                outcome: this.babyFormGroup.get('outcome').value.itemId,
                apgarScoreOne: this.babyFormGroup.get('agparScore1min').value,
                apgarScoreFive: this.babyFormGroup.get('agparScore5min').value,
                apgarScoreTen: this.babyFormGroup.get('agparScore10min').value,
                resuscitate: (this.babyFormGroup.get('resuscitationDone').value.itemName == 'Yes') ? true : false,
                deformity:  (this.babyFormGroup.get('deformity').value.itemName == 'Yes') ? true : false,
                teo:  (this.babyFormGroup.get('teoGiven').value.itemName == 'Yes') ? true : false,
                breastFeeding:  (this.babyFormGroup.get('breastFed').value.itemName == 'Yes') ? true : false ,
                comment: this.babyFormGroup.get('comment').value,
                notificationNo: this.babyFormGroup.get('notificationNumber').value
            });

            console.log(this.babyData);

            this.babyDataTable.push({
                sex: this.babyFormGroup.get('babySex').value.itemName,
                birthWeight: this.babyFormGroup.get('birthWeight').value,
                outcome: this.babyFormGroup.get('outcome').value.itemName,
                apgarScore: this.babyFormGroup.get('agparScore1min').value + ' in 1,' +
                '' + this.babyFormGroup.get('agparScore5min').value + ' in 5,' +
                this.babyFormGroup.get('agparScore10min').value + 'in 10',
                resuscitate: this.babyFormGroup.get('resuscitationDone').value.itemName,
                deformity:  this.babyFormGroup.get('deformity').value.itemName,
                teo:  this.babyFormGroup.get('teoGiven').value.itemName,
                breastFeeding:  this.babyFormGroup.get('breastFed').value.itemName,
                comment: this.babyFormGroup.get('comment').value
            });

            console.log(this.babyDataTable);
            this.dataSource = new MatTableDataSource(this.babyDataTable);
        }

    }

    public onRowClicked(row) {
        console.log('row clicked:', row);
        const index = this.babyDataTable.indexOf(row.milestone);
        const index_ = this.babyData.indexOf(row.milestone);
        this.babyDataTable.splice(index, 1);
        this.babyDataTable.splice(index_, 1);
        this.dataSource = new MatTableDataSource(this.babyDataTable);
    }


}

import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {MatTableDataSource} from '@angular/material';
import { MaternityService } from '../../_services/maternity.service';

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
    yesnoOptions: any[] = [];
    birthOutcomes: any[] = [];

    @Input('PatientId') PatientId: number;
    @Input('isEdit') isEdit: boolean;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    showEdit: boolean = false;

    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService, private maternityService: MaternityService) {
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
        this.yesnoOptions = yesNos;
        this.birthOutcomes = birthOutcomes;

        this.notify.emit(this.babyFormGroup);
        this.notifyData.emit(this.babyData);
        if (this.isEdit) {
        this.getDeliveredBabyInfo(this.PatientMasterVisitId);
        }
    }

    public AddBaby() {

        if (this.babyFormGroup.invalid) { 
              return;
        }

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
          //  this.babyFormGroup.reset();
          //  this.babyFormGroup.clearValidators();
    }

    public onRowClicked(row) {
        console.log('row clicked:', row);
        if (this.isEdit) {
           this.setBabyFormValues(row);
        }
        this.showEdit = true;
        // const index = this.babyDataTable.indexOf(row.milestone);
        // const index_ = this.babyData.indexOf(row.milestone);
        // this.babyDataTable.splice(index, 1);
        // this.babyDataTable.splice(index_, 1);
        // this.dataSource = new MatTableDataSource(this.babyDataTable);
    }

    public getDeliveredBabyInfo(masterVisitId: number): void {
        this.maternityService.GetDeliveredBabyInfo(masterVisitId)
            .subscribe(
                bInfo => {
                    if (bInfo == null) {
                       return;
                    }
                 bInfo.forEach(info => {
                    this.babyDataTable.push({
                        sex: info.sex,
                        birthWeight: info.birthWeight,
                        outcome: info.deliveryOutcome,
                        apgarScore: info.apgarScores,
                        resuscitate: info.resuscitationDone ? 'Yes' : 'No',
                        deformity:  info.birthDeformity ? 'Yes' : 'No',
                        teo:  info.teoGiven ? 'Yes' : 'No',
                        breastFeeding: info.breastFedWithinHour ? 'Yes' : 'No',
                        comment: info.comment,
                        notificationNumber: info.birthNotificationNumber,
                        id: info.id
                    });
                 });
                 this.dataSource = new MatTableDataSource(this.babyDataTable);
                },
                (err) => {
                    this.snotifyService.error('Error fetching baby details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }

    public setBabyFormValues(babyInfo: any): void {
                    this.babyFormGroup.controls['babySex'].setValue(this.getLookUpItemId(this.genderOptions, babyInfo.sex));
                    this.babyFormGroup.controls['birthWeight'].setValue(babyInfo.birthWeight);
                    this.babyFormGroup.controls['outcome'].setValue(this.getLookUpItemId(this.deliveryOutcomeOptions, babyInfo.outcome));
                    this.babyFormGroup.controls['resuscitationDone']
                    .setValue(babyInfo.resuscitate ? this.getLookUpItemId(this.yesnoOptions, 'Yes') :
                        this.getLookUpItemId(this.yesnoOptions, 'No'));
                    this.babyFormGroup.controls['deformity'].setValue(babyInfo.deformity ? this.getLookUpItemId(this.yesnoOptions, 'Yes') :
                        this.getLookUpItemId(this.yesnoOptions, 'No'));
                    this.babyFormGroup.controls['teoGiven'].setValue(babyInfo.teo ? this.getLookUpItemId(this.yesnoOptions, 'Yes') :
                        this.getLookUpItemId(this.yesnoOptions, 'No'));
                    this.babyFormGroup.controls['breastFed'].setValue(babyInfo.breastFeeding ?
                        this.getLookUpItemId(this.yesnoOptions, 'Yes') : this.getLookUpItemId(this.yesnoOptions, 'No'));
                    this.babyFormGroup.controls['comment'].setValue(babyInfo.comment);
                    this.babyFormGroup.controls['agparScore1min'].setValue(this.getApgarScoreValue(babyInfo.apgarScore, '1min'));
                    this.babyFormGroup.controls['agparScore5min'].setValue(this.getApgarScoreValue(babyInfo.apgarScore, '5min'));
                    this.babyFormGroup.controls['agparScore10min'].setValue(this.getApgarScoreValue(babyInfo.apgarScore, '10min'));
                    this.babyFormGroup.controls['notificationNumber'].setValue(babyInfo.notificationNumber);
    }

   
    public getApgarScoreValue(apgarScore: string, scoreType: string): any {
        const scoreArr = apgarScore.split(',');
        let score = '0';
        switch (scoreType) {
            case '1min':       
               score = scoreArr[0].split('in')[0];       
                break;
            case '5min':    
            score = scoreArr[1].split('in')[0];             
                break;
            case '10min':    
            score = scoreArr[2].split('in')[0];                        
                break;
            default:
                break;
        }
        return score;
    }

  
    public getLookUpItemId(lookUpOptions: any [], lookupName: string): any {
        for (let index = 0; index < lookUpOptions.length; index++) {
            if (lookUpOptions[index].itemName == lookupName) {
              return lookUpOptions[index];
            }  
        }
        return null;
    }

    
    public UpdateBabyDetails() {
        this.showEdit = false;
        this.babyFormGroup.reset();
        this.babyFormGroup.clearValidators();
    }


}

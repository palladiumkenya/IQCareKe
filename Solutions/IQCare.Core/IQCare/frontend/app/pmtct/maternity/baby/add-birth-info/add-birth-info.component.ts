import { Component, OnInit, Input, Output, EventEmitter, inject, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MaternityService } from '../../../_services/maternity.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../../shared/_services/notification.service';

@Component({
  selector: 'app-add-birth-info',
  templateUrl: './add-birth-info.component.html',
  styleUrls: ['./add-birth-info.component.css']
})
export class AddBirthInfoComponent implements OnInit {

    babyFormGroup: FormGroup;
    babyData: any[] = [];
    @Input() babySectionOptions: any[] = [];

    public genderOptions: any[] = [];
    deliveryOutcomeOptions: any[] = [];
    yesnoOptions: any[] = [];
    birthOutcomes: any[] = [];
    babyInfo: any ;
 

    @Input() PatientId: number;
    @Input() isEdit: boolean;
    @Input() PatientMasterVisitId: number;
    constructor(private formBuilder: FormBuilder,
    private maternityService: MaternityService) {
        
    }

  ngOnInit() {
    this.babyFormGroup = this.formBuilder.group({
      babySex: new FormControl('', [Validators.required]),
      birthWeight: new FormControl('', [Validators.required, Validators.min(0), Validators.max(5)]),
      outcome: new FormControl('', [Validators.required]),
      resuscitationDone: new FormControl('', [Validators.required]),
      deformity: new FormControl('', [Validators.required]),
      teoGiven: new FormControl('', [Validators.required]),
      breastFed: new FormControl('', [Validators.required]),

      agparScore1min: new FormControl('', [Validators.required, Validators.min(0), Validators.max(10)]),
      agparScore5min: new FormControl('', [Validators.required, Validators.min(0),  Validators.max(10)]),
      agparScore10min: new FormControl('', [Validators.required, Validators.min(0),  Validators.max(10)]),
      notificationNumber: new FormControl('', [Validators.required]),
      comment: new FormControl('na', [])
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
   
  }

  public AddBaby() {
        if (this.babyFormGroup.invalid) {
            return;
        }

        this.babyData.push({
            sex: this.babyFormGroup.get('babySex').value.itemId,
            sexStr: this.babyFormGroup.get('babySex').value.itemName,
            birthWeight: this.babyFormGroup.get('birthWeight').value,
            outcome: this.babyFormGroup.get('outcome').value.itemId,
            outcomeName : this.babyFormGroup.get('outcome').value.itemName,
            apgarScore: this.babyFormGroup.get('agparScore1min').value + ' in 1,' +
            '' + this.babyFormGroup.get('agparScore5min').value + ' in 5,' +
            this.babyFormGroup.get('agparScore10min').value + 'in 10',
            apgarScoreOne: this.babyFormGroup.get('agparScore1min').value,
            apgarScoreFive: this.babyFormGroup.get('agparScore5min').value,
            apgarScoreTen: this.babyFormGroup.get('agparScore10min').value,
            resuscitateStr: this.babyFormGroup.get('resuscitationDone').value.itemName,
            resuscitate: (this.babyFormGroup.get('resuscitationDone').value.itemName == 'Yes') ? true : false,
            deformityStr:  this.babyFormGroup.get('deformity').value.itemName,
            deformity:  (this.babyFormGroup.get('deformity').value.itemName == 'Yes') ? true : false,
            teo:  (this.babyFormGroup.get('teoGiven').value.itemName == 'Yes') ? true : false,
            teoStr:  this.babyFormGroup.get('teoGiven').value.itemName,
            breastFeeding:  (this.babyFormGroup.get('breastFed').value.itemName == 'Yes') ? true : false ,
            breastFeedingStr:  this.babyFormGroup.get('breastFed').value.itemName,
            comment: this.babyFormGroup.get('comment').value,
            notificationNo: this.babyFormGroup.get('notificationNumber').value
        });
        this.maternityService.updateBabyDataInfo(this.babyData);
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

public onBabyOutcome(event) {

    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
    const noId = noOption[0]['itemId'];

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Live Birth') {

    }

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Fresh Still Birth') {

    }

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Macerated Still Birth') {

        this.babyFormGroup.get('resuscitationDone').setValue(105);
        this.babyFormGroup.get('agparScore1min').setValue(0);
        this.babyFormGroup.get('agparScore5min').setValue(0);
        this.babyFormGroup.get('agparScore10min').setValue(0);
        this.babyFormGroup.get('notificationNumber').setValue(0);
        this.babyFormGroup.get('comment').setValue('');
    }
}

}

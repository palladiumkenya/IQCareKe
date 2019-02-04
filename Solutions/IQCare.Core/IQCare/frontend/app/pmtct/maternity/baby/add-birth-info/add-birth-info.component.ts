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

    @Input() PatientId: number;
    @Input() isEdit: boolean;
    @Input() PatientMasterVisitId: number;
    constructor(private formBuilder: FormBuilder,
    private maternityService: MaternityService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService,
    private dialogRef : MatDialogRef<AddBirthInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData : any) 
    {
        this.isEdit = this.dialogData.isEdit
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

      agparScore1min: new FormControl('', [Validators.required, Validators.max(10)]),
      agparScore5min: new FormControl('', [Validators.required, Validators.max(10)]),
      agparScore10min: new FormControl('', [Validators.required, Validators.max(10)]),
      notificationNumber: new FormControl('', [Validators.required]),
      comment: new FormControl('na', [])
  });
  
  if(this.isEdit)
  {
     this.babySectionOptions = this.dialogData.babySectionOptions
     this.PatientId = this.dialogData.patientId
     this.PatientMasterVisitId = this.dialogData.patientMasterVisitId
  }
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

    if(this.isEdit){

        var babyInfoCommand = this.maternityService.buildAddBabyCommandModel(this.babyFormGroup);
        babyInfoCommand.PatientMasterVisitId = this.PatientMasterVisitId;
    
        this.maternityService.addNewBabyInfo(babyInfoCommand).subscribe(res=>{
            console.log(res);
            this.snotifyService.success('Baby information added sucessfully', 'Maternity', this.notificationService.getConfig());
            this.dialogRef.close();
        },
        (err)=>
        {
            console.log(err+ " An error occured while adding new baby info")
            this.snotifyService.error('Error adding baby information ' + err, 'Maternity', this.notificationService.getConfig());

        },
        ()=>{
            
        })
        
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


public setBabyFormValues(babyInfo: any): void {
                this.babyFormGroup.controls['babySex'].setValue(this.maternityService.getMaternityLookUpOptionByName(this.genderOptions, babyInfo.sex));
                this.babyFormGroup.controls['birthWeight'].setValue(babyInfo.birthWeight);
                this.babyFormGroup.controls['outcome'].setValue(this.maternityService.getMaternityLookUpOptionByName(this.deliveryOutcomeOptions, babyInfo.outcome));
                this.babyFormGroup.controls['resuscitationDone']
                .setValue(babyInfo.resuscitate ? this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
                    this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
                this.babyFormGroup.controls['deformity'].setValue(babyInfo.deformity ? 
                     this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
                     this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
                this.babyFormGroup.controls['teoGiven'].setValue(babyInfo.teo ? 
                     this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
                     this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
                this.babyFormGroup.controls['breastFed'].setValue(babyInfo.breastFeeding ?
                    this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') : 
                     this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
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

public onBabyOutcome(event) {

    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
    console.log('yesNoOptions');
    console.log(noOption);
    const noId = noOption[0]['itemId'];
    console.log('NoId' + noId);

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Live Birth') {

    }

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Fresh Still Birth') {

    }

    if (event.isUserInput && event.source.selected && event.source.viewValue == 'Macerated Still Birth') {

        this.babyFormGroup.get('resuscitationDone').setValue(105);
      //  this.babyFormGroup.get('deformity').setValue(noId);
      //  this.babyFormGroup.get('teoGiven').setValue(noId);
      //  this.babyFormGroup.get('breastFed').setValue(noId);
        this.babyFormGroup.get('agparScore1min').setValue(0);
        this.babyFormGroup.get('agparScore5min').setValue(0);
        this.babyFormGroup.get('agparScore10min').setValue(0);
        this.babyFormGroup.get('notificationNumber').setValue(0);
        this.babyFormGroup.get('comment').setValue('');
    }
}

}

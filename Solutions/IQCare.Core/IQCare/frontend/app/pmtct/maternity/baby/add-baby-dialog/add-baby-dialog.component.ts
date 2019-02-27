import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MaternityService } from '../../../_services/maternity.service';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { UpdateDeliveredBabyBirthInfoCommand } from '../../commands/baby-condition-command';

@Component({
  selector: 'app-add-baby-dialog',
  templateUrl: './add-baby-dialog.component.html',
  styleUrls: ['./add-baby-dialog.component.css']
})
export class AddBabyDialogComponent implements OnInit {
  @Input() babySectionOptions: any[] = [];
  @Input() PatientId: number;
  @Input() isEdit: boolean;
  @Input() PatientMasterVisitId: number;
  patientDeliveryInfoId: number;
  deliveredBabyInfoId: number;
  babyFormGroup: FormGroup;

  public genderOptions: any[] = [];
  deliveryOutcomeOptions: any[] = [];
  yesnoOptions: any[] = [];
  birthOutcomes: any[] = [];

  isUpdate: boolean;
  isNew: boolean;

  babyInfo: any;
  constructor( private dialogRef: MatDialogRef<AddBabyDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public dialogData: any,
              private formBuilder: FormBuilder,
              private maternityService: MaternityService,
              private snotifyService: SnotifyService,
              private notificationService: NotificationService) { 
                
 
              }

  ngOnInit() {
    this.babyFormGroup = this.formBuilder.group({
      babySex: new FormControl('', [Validators.required]),
      birthWeight: new FormControl('', [Validators.required, Validators.min(0), Validators.max(10)]),
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


       this.babySectionOptions = this.dialogData.babySectionOptions;
       this.PatientId = this.dialogData.patientId;
       this.PatientMasterVisitId = this.dialogData.patientMasterVisitId;
       this.babyInfo = this.dialogData.babyInfo;
       this.isNew = this.dialogData.isNew;
       this.isUpdate = this.dialogData.isUpdate;

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
    
    if (this.isUpdate) {
       this.setBabyFormValues(this.babyInfo);
       this.patientDeliveryInfoId = this.babyInfo.patientDeliveryInformationId;
       this.deliveredBabyInfoId = this.babyInfo.id;
    }
  }

  public AddBaby() {

    if (this.babyFormGroup.invalid) { 
          return;
    }

        const babyInfoCommand = this.maternityService.buildAddBabyCommandModel(this.babyFormGroup);
        babyInfoCommand.PatientMasterVisitId = this.PatientMasterVisitId;
    
        this.maternityService.addNewBabyInfo(babyInfoCommand).subscribe(res => {
            console.log(res);
            this.snotifyService.success('Baby information added sucessfully', 'Maternity', this.notificationService.getConfig());
            this.dialogRef.close();
        },
        (err) => {
            console.log(err + ' An error occured while adding new baby info');
            this.snotifyService.error('Error adding baby information ' + err, 'Maternity', this.notificationService.getConfig());

        },
        () => {
            
        });      
}

public UpdateBabyDetails() {
  if (this.babyFormGroup.invalid) {
      return;
  }    
    const babyInfoCommand = this.maternityService.buildAddBabyCommandModel(this.babyFormGroup);
    babyInfoCommand.Id = this.deliveredBabyInfoId;
    babyInfoCommand.PatientDeliveryInformationId = this.patientDeliveryInfoId;
    babyInfoCommand.PatientMasterVisitId = this.PatientMasterVisitId;
    
    const updateBabyInfoModel: UpdateDeliveredBabyBirthInfoCommand = {
      DeliveredBabyBirthInformation  : babyInfoCommand
    };
        
    this.maternityService.updateBabyInfo(updateBabyInfoModel).subscribe(res => {
      console.log('update response ' + res);
      this.snotifyService.success('Baby information updated sucessfully', 'Maternity', this.notificationService.getConfig());
      this.dialogRef.close();
  },
  (err) => {
      console.log(err + ' An error occured while updating baby info');
      this.snotifyService.error('Error updating baby information ' + err, 'Maternity', this.notificationService.getConfig());

  },
  () => {
      
  });  
}

public setBabyFormValues(babyInfo: any): void {
  this.babyFormGroup.controls['babySex'].setValue(this.maternityService.getMaternityLookUpOptionByName(this.genderOptions,
      babyInfo.sexStr));
  this.babyFormGroup.controls['birthWeight'].setValue(babyInfo.birthWeight);
  this.babyFormGroup.controls['outcome'].setValue(this.maternityService.getMaternityLookUpOptionByName(this.birthOutcomes,
      babyInfo.outcomeName));
  this.babyFormGroup.controls['resuscitationDone']
  .setValue(babyInfo.resuscitateStr == 'Yes' ? this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
      this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
  this.babyFormGroup.controls['deformity'].setValue(babyInfo.deformityStr == 'Yes' ? 
       this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
       this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
  this.babyFormGroup.controls['teoGiven'].setValue(babyInfo.teoStr == 'Yes' ? 
       this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'Yes') :
       this.maternityService.getMaternityLookUpOptionByName(this.yesnoOptions, 'No'));
  this.babyFormGroup.controls['breastFed'].setValue(babyInfo.breastFeedingStr == 'Yes' ?
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

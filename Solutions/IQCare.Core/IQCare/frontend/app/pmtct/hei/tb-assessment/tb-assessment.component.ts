import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import {IptClientWorkupComponent} from '../ipt-client-workup/ipt-client-workup.component';
import {Component, EventEmitter, Inject, inject, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../../shared/_services/notification.service';
import {PersoncontactsComponent} from '../../../records/person/personcontacts/personcontacts.component';
import {IptFollowUpComponent} from '../ipt-follow-up/ipt-follow-up.component';
import {IptOutcomeComponent} from '../ipt-outcome/ipt-outcome.component';

@Component({
  selector: 'app-tb-assessment',
  templateUrl: './tb-assessment.component.html',
  styleUrls: ['./tb-assessment.component.css']
})
export class TbAssessmentComponent implements OnInit {
    public TbAssessmentFormGroup: FormGroup;
    public yesnoOptions: any[] = [];
    public sputumSmearOptions: any[] = [];
    public geneXpertOptions: any[] = [];
    public chestXrayOptions: any[] = [];
    public tbScreeningOutcomeOptions: any[] = [];

    @Input('tbAssessmentOptions') tbAssessmentOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();



  constructor(private _formBuilder: FormBuilder,
              private _lookupItemService: LookupItemService,
              private snotifyService: SnotifyService,
              private notificationService: NotificationService,
              public dialog: MatDialog)  { }

  ngOnInit() {
      this.TbAssessmentFormGroup = this._formBuilder.group({
          currentlyOnAntiTb: new FormControl('', [Validators.required]),
          coughAnyDuration: new FormControl('', [Validators.required]),
          fever: new FormControl('', [Validators.required]),
          weightLoss: new FormControl('', [Validators.required]),
          contactTB: new FormControl('', [Validators.required]),
          sputumSmear: new FormControl('', [Validators.required]),
          geneXpert: new FormControl('', [Validators.required]),
          chestXray: new FormControl('', [Validators.required]),
          invitationContacts: new FormControl('', [Validators.required])
      });

      const {
          yesnoOption,
          sputumSmear,
          genexpert,
          chestXray,
          tbScreeningOutcome
      } = this.tbAssessmentOptions[0];
      this.yesnoOptions = yesnoOption;
      this.sputumSmearOptions = sputumSmear;
      this.geneXpertOptions = genexpert;
      this.chestXrayOptions = chestXray;
      this.tbScreeningOutcomeOptions = tbScreeningOutcome;
      this.notify.emit(this.TbAssessmentFormGroup);


  }

  openIPTClientWorkupDialog(): void {
      const dialogConfig = new MatDialogConfig();

      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.height = '70%';
      dialogConfig.width = '30%';


      const dialogRef = this.dialog.open(IptClientWorkupComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(
          data => {
              if (!data) {
                  return;
              }
              console.log(data);

          }
      );
  }

    openIPTFollowUpDialog(): void {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '30%';


        const dialogRef = this.dialog.open(IptFollowUpComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                console.log(data);

            }
        );
    }

    openIPTOutcomeDialog(): void {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '30%';


        const dialogRef = this.dialog.open(IptOutcomeComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                console.log(data);

            }
        );
    }



}

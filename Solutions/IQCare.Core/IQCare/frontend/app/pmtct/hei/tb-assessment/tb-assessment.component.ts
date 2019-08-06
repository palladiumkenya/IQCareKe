import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material';
import { IptClientWorkupComponent } from '../ipt-client-workup/ipt-client-workup.component';
import {
    Component,
    EventEmitter,
    Inject,
    inject,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges
} from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators
} from '@angular/forms';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { IptFollowUpComponent } from '../ipt-follow-up/ipt-follow-up.component';
import { IptOutcomeComponent } from '../ipt-outcome/ipt-outcome.component';
import { DefaultParameters } from '../../_models/hei/DefaultParameters';

@Component({
    selector: 'app-tb-assessment',
    templateUrl: './tb-assessment.component.html',
    styleUrls: ['./tb-assessment.component.css']
})
export class TbAssessmentComponent implements OnInit, OnChanges {
    public TbAssessmentFormGroup: FormGroup;
    public yesnoOptions: LookupItemView[] = [];
    public sputumSmearOptions: any[] = [];
    public geneXpertOptions: any[] = [];
    public chestXrayOptions: any[] = [];
    public iptOutcomeOptions: any[] = [];
    public tbScreeningOutcomeOptions: any[] = [];
    public cough: string;
    public fever: string;
    public weightLoss: string;
    public contactWithTb: string;
    public iptClientWorkIsDisabled: boolean = true;
    public iptOUtcomeIsDisabled: boolean = true;
    public iptFollowupIsDisabled: boolean = true;

    @Input('tbAssessmentOptions') tbAssessmentOptions: any;
    @Input('defaultParameters') defaultParameters: DefaultParameters;

    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(
        private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public dialog: MatDialog,
        private heiservice: HeiService
    ) {}

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
            invitationContacts: new FormControl('', [Validators.required]),
            tbScreaningOutcome: new FormControl('', [Validators.required]),
            onIPT: new FormControl('', [Validators.required]),
            startIPT: new FormControl('', [Validators.required])
        });
        this.TbAssessmentFormGroup.controls['sputumSmear'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['geneXpert'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['chestXray'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['invitationContacts'].disable({
            onlySelf: true
        });

        this.TbAssessmentFormGroup.controls['coughAnyDuration'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['fever'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['weightLoss'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['contactTB'].disable({
            onlySelf: true
        });
        this.TbAssessmentFormGroup.controls['startIPT'].disable({
            onlySelf: true
        });
        const {
            yesnoOption,
            sputumSmear,
            genexpert,
            chestXray,
            tbScreeningOutcome,
            iptOutcomes
        } = this.tbAssessmentOptions[0];
        this.yesnoOptions = yesnoOption;
        this.sputumSmearOptions = sputumSmear;
        this.geneXpertOptions = genexpert;
        this.chestXrayOptions = chestXray;
        this.tbScreeningOutcomeOptions = tbScreeningOutcome;
        this.iptOutcomeOptions = iptOutcomes;

        this.notify.emit(this.TbAssessmentFormGroup);

        if (this.isEdit) {
            this.loadTBAssessment();
        }
    }

    loadTBAssessment(): void {
        this.heiservice.getTBAssessment(this.patientId).subscribe(
            result => {
                console.log('tbassessment', result);

                for (let i = 0; i < result.length; i++) {
                    // On TB drugs
                    const onAntiTbDrugs = result[i].onAntiTbDrugs
                        ? 'Yes'
                        : 'No';
                    const currentlyOnAntiTb = this.yesnoOptions.filter(
                        obj => obj.itemName == onAntiTbDrugs
                    );
                    this.TbAssessmentFormGroup.get(
                        'currentlyOnAntiTb'
                    ).setValue(currentlyOnAntiTb[0].itemId);
                    // cough
                    let cough;
                    if (result[i].cough != null) {
                        cough = result[i].cough ? 'Yes' : 'No';
                        const coughAnyDuration = this.yesnoOptions.filter(
                            obj => obj.itemName == cough
                        );
                        this.TbAssessmentFormGroup.get(
                            'coughAnyDuration'
                        ).setValue(coughAnyDuration[0].itemId);
                    }

                    // fever
                    let fever;
                    if (result[i].fever != null) {
                        fever = result[i].fever ? 'Yes' : 'No';
                        const feverValue = this.yesnoOptions.filter(
                            obj => obj.itemName == fever
                        );
                        this.TbAssessmentFormGroup.get('fever').setValue(
                            feverValue[0].itemId
                        );
                    }

                    // weightLoss
                    let weightLoss;
                    if (result[i].weightLoss != null) {
                        weightLoss = result[i].weightLoss ? 'Yes' : 'No';
                        const weightLossValue = this.yesnoOptions.filter(
                            obj => obj.itemName == weightLoss
                        );
                        this.TbAssessmentFormGroup.get('weightLoss').setValue(
                            weightLossValue[0].itemId
                        );
                    }

                    // contactTB
                    let contactWithTb;
                    if (result[i].contactWithTb != null) {
                        contactWithTb = result[i].contactWithTb ? 'Yes' : 'No';
                        const contactTB = this.yesnoOptions.filter(
                            obj => obj.itemName == contactWithTb
                        );
                        this.TbAssessmentFormGroup.get('contactTB').setValue(
                            contactTB[0].itemId
                        );
                    }

                    // onIpt
                    let onIpt;
                    if (result[i].onIpt != null) {
                        onIpt = result[i].onIpt ? 'Yes' : 'No';
                        const Ipt = this.yesnoOptions.filter(
                            obj => obj.itemName == onIpt
                        );
                        this.TbAssessmentFormGroup.get('onIPT').setValue(
                            Ipt[0].itemId
                        );
                    }

                    // onAntiTbDrugs
                    let startIPT;
                    if (result[i].everBeenOnIpt != null) {
                        startIPT = result[i].everBeenOnIpt ? 'Yes' : 'No';
                        const _startIPT = this.yesnoOptions.filter(
                            obj => obj.itemName == startIPT
                        );
                        this.TbAssessmentFormGroup.get('startIPT').setValue(
                            _startIPT[0].itemId
                        );
                    }
                }
            },
            error => {
                console.log(error);
            },
            () => {}
        );

        this.heiservice.getPatientIcfAction(this.patientId).subscribe(
            result => {
                console.log(result);
                for (let i = 0; i < result.length; i++) {
                    // ICF Action
                    this.TbAssessmentFormGroup.get('sputumSmear').setValue(
                        result[i].sputumSmear
                    );
                    this.TbAssessmentFormGroup.get('geneXpert').setValue(
                        result[i].geneXpert
                    );
                    this.TbAssessmentFormGroup.get('chestXray').setValue(
                        result[i].chestXray
                    );
                    this.TbAssessmentFormGroup.get(
                        'invitationContacts'
                    ).setValue(result[i].invitationOfContacts);
                }
            },
            error => {
                console.log(error);
            },
            () => {}
        );
    }

    ngOnChanges(changes: SimpleChanges) {
        for (const property in changes) {
            if (property === 'coughAnyDuration') {
                console.log(changes[property].currentValue);
            }
        }
    }

    openIPTClientWorkupDialog(): void {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '30%';

        dialogConfig.data = {
            yesNoOptions: this.yesnoOptions,
            personId: this.defaultParameters.personId,
            patientId: this.defaultParameters.patientId,
            patientMasterVisitId: this.defaultParameters.patientMasterVisitId,
            serviceAreaId: this.defaultParameters.serviceAreaId,
            userId: this.defaultParameters.userId
        };

        const dialogRef = this.dialog.open(
            IptClientWorkupComponent,
            dialogConfig
        );

        dialogRef.afterClosed().subscribe(data => {
            if (!data) {
                return;
            }
            console.log(data);
        });
    }

    openIPTFollowUpDialog(): void {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '70%';
        dialogConfig.width = '30%';

        dialogConfig.data = {
            yesNoOptions: this.yesnoOptions,
            personId: this.defaultParameters.personId,
            patientId: this.defaultParameters.patientId,
            patientMasterVisitId: this.defaultParameters.patientMasterVisitId,
            serviceAreaId: this.defaultParameters.serviceAreaId,
            userId: this.defaultParameters.userId
        };

        const dialogRef = this.dialog.open(IptFollowUpComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(data => {
            if (!data) {
                return;
            }
            console.log(data);
        });
    }

    openIPTOutcomeDialog(): void {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '30%';
        dialogConfig.width = '25%';
        dialogConfig.data = {
            yesNoOptions: this.yesnoOptions,
            iptOutcomeOptions: this.iptOutcomeOptions,
            personId: this.defaultParameters.personId,
            patientId: this.defaultParameters.patientId,
            patientMasterVisitId: this.defaultParameters.patientMasterVisitId,
            serviceAreaId: this.defaultParameters.serviceAreaId,
            userId: this.defaultParameters.userId
        };

        const dialogRef = this.dialog.open(IptOutcomeComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(data => {
            if (!data) {
                return;
            }
            console.log(data);
        });
    }

    onAntiTbDrugsChange(event) {
        if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'No'
        ) {
            this.TbAssessmentFormGroup.get('coughAnyDuration').enable();
            this.TbAssessmentFormGroup.get('fever').enable({ onlySelf: true });
            this.TbAssessmentFormGroup.get('weightLoss').enable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.get('contactTB').enable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.get('tbScreaningOutcome').patchValue(0);
        } else if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'Yes'
        ) {
            const TbOptions = this.tbScreeningOutcomeOptions.filter(
                x => x.itemName == 'TBRx'
            );

            this.TbAssessmentFormGroup.get('coughAnyDuration').disable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.get('fever').disable({ onlySelf: true });
            this.TbAssessmentFormGroup.get('weightLoss').disable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.get('contactTB').disable({
                onlySelf: true
            });

            this.TbAssessmentFormGroup.get('tbScreaningOutcome').patchValue(
                TbOptions[0]['itemId']
            );
        }
    }

    onSymptomaticScreeningChange(event, screening: string) {
        if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'Yes'
        ) {
            switch (screening) {
                case 'cough':
                    this.cough = 'Yes';
                    this.TbAssessmentFormGroup.controls['sputumSmear'].disable({
                        onlySelf: false
                    });
                    break;
                case 'fever':
                    this.fever = 'Yes';
                    break;
                case 'weightloss':
                    this.weightLoss = 'Yes';
                    break;
                case 'contactTb':
                    this.contactWithTb = 'Yes';
                    break;
                default:
            }
            this.enableDisableIcfAction();
            // this.iptClientWorkIsDisabled = false;
        } else if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'No'
        ) {
            switch (screening) {
                case 'cough':
                    this.cough = 'No';

                    break;
                case 'fever':
                    this.fever = 'No';
                    break;
                case 'weightloss':
                    this.weightLoss = 'No';
                    break;
                case 'contactTb':
                    this.contactWithTb = 'No';
                    break;
                default:
            }
            this.enableDisableIcfAction();
        }
    }

    onCurrentIPTChange(event) {
        if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'No'
        ) {
            this.TbAssessmentFormGroup.get('startIPT').enable({
                onlySelf: false
            });
            this.iptFollowupIsDisabled = true;
            this.iptOUtcomeIsDisabled = true;
            // this.iptClientWorkIsDisabled = false;
        } else if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'Yes'
        ) {
            this.TbAssessmentFormGroup.controls['startIPT'].disable({
                onlySelf: true
            });
            this.iptFollowupIsDisabled = false;
            this.iptOUtcomeIsDisabled = false;
            this.iptClientWorkIsDisabled = true;
        }
    }

    onStartIPTChange(event) {
        if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'Yes'
        ) {
            this.iptClientWorkIsDisabled = false;
            this.iptFollowupIsDisabled = true;
            this.iptOUtcomeIsDisabled = true;
        } else if (
            event.isUserInput &&
            event.source.selected &&
            event.source.viewValue == 'No'
        ) {
            this.iptClientWorkIsDisabled = true;
        }
    }

    enableDisableIcfAction() {
        if (
            this.cough == 'Yes' ||
            this.fever == 'Yes' ||
            this.weightLoss == 'Yes' ||
            this.contactWithTb == 'Yes'
        ) {
            this.TbAssessmentFormGroup.get('sputumSmear').enable({
                onlySelf: false
            });
            this.TbAssessmentFormGroup.controls['geneXpert'].enable({
                onlySelf: false
            });
            this.TbAssessmentFormGroup.controls['chestXray'].enable({
                onlySelf: false
            });
            this.TbAssessmentFormGroup.controls['invitationContacts'].enable({
                onlySelf: false
            });
        } else if (
            this.cough == 'No' ||
            this.fever == 'Yes' ||
            this.weightLoss == 'No' ||
            this.contactWithTb == 'No'
        ) {
            this.TbAssessmentFormGroup.controls['sputumSmear'].disable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.controls['geneXpert'].disable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.controls['chestXray'].disable({
                onlySelf: true
            });
            this.TbAssessmentFormGroup.controls['invitationContacts'].disable({
                onlySelf: true
            });
        }
    }
}

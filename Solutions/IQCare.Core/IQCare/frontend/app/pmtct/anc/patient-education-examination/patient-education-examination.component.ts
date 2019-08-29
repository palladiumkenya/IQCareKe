import { Component, EventEmitter, Input, OnInit, Output, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SnotifyService } from 'ng-snotify';
import { PatientEducationEmitter } from '../../emitters/PatientEducationEmitter';
import { CounsellingTopicsEmitters } from '../../emitters/counsellingTopicsEmitters';
import { PatientEducationCommand } from '../../_models/PatientEducationCommand';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';

import * as moment from 'moment';
import { AncService } from '../../_services/anc.service';
import { DataService } from '../../../shared/_services/data.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { PatientCounsellingComponent } from '../patient-counselling/patient-counselling.component'
import { LookupItemView } from '../../../shared/_models/LookupItemView';

export interface PeriodicElement {
    topicId: number;
    topic: string;
    onSetDate: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
    { topicId: 1, topic: 'sex', onSetDate: 'Hydrogen' },
    { topicId: 2, topic: 'church', onSetDate: 'Helium' }
];


@Component({
    selector: 'app-patient-education-examination',
    templateUrl: './patient-education-examination.component.html',
    styleUrls: ['./patient-education-examination.component.css']
})


export class PatientEducationExaminationComponent implements OnInit, OnDestroy {
    PatientEducationFormGroup: FormGroup;

    public yesnos: any[] = [];
    public testResults: any[] = [];
    public userId: number;
    public maxDate: Date;

    public patientCounseling$: Subscription;
    public baseline$: Subscription;

    public patientEducationEmitterData: PatientEducationEmitter;
    public counsellingOptions: any[] = [];
    public yesNoOptions: any[] = [];
    public hivStatusOptions: any[] = [];
    public syphilisResultsOptions: LookupItemView[] = [];
    public syphilisTestTypes: LookupItemView[] = [];

    public counselling_editlist: any[] = [];
    public counselling_data: CounsellingTopicsEmitters[] = [];
    public counseling_db_data: CounsellingTopicsEmitters[] = [];
    @Output() nextStep = new EventEmitter<PatientEducationEmitter>();
    @Output() notify: EventEmitter<object> = new EventEmitter<object>();
    @Input() patientEducationData: PatientEducationCommand;
    @Input() patientEducationFormOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;


    displayedColumns: string[] = ['topicId', 'topic', 'onSetDate'];
    dataSource = ELEMENT_DATA;

    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dataService: DataService,
        private ancService: AncService,
        private lookupservice: LookupItemService) {
    }

    ngOnInit() {
        this.maxDate = this.visitDate;

        this.PatientEducationFormGroup = this._formBuilder.group({
            breastExamDone: ['', Validators.required],
            treatedSyphilis: ['', Validators.required],
            testedSyphilis: ['', Validators.required],
            SyphilisTestUsed: ['', Validators.required],
            SyphilisResults: ['', Validators.required]
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        const {
            yesnoOptions,
            patientEducationOptions,
            hivStatusOptions
        } = this.patientEducationFormOptions[0];
        this.yesNoOptions = yesnoOptions;
        this.counsellingOptions = patientEducationOptions;
        this.hivStatusOptions = hivStatusOptions;


        this.nextStep.emit(this.patientEducationEmitterData);
        this.notify.emit({ 'form': this.PatientEducationFormGroup, 'counselling_data': this.counselling_data });

        if (this.isEdit) {
            this.getPatientCounselingDataAll(this.PatientId);
            this.getBaselineAncProfile(this.PatientId);
        } else {
            this.getPatientCounselingDataAll(this.PatientId);
        }

        this.lookupservice.getByGroupName('SyphilisResults').subscribe(
            res => {
                this.syphilisResultsOptions = res['lookupItems'];
            },
            error => {

            }
        );
        this.lookupservice.getByGroupName('SyphilisTestType').subscribe(
            res => {
                this.syphilisTestTypes = res['lookupItems'];
            },
            error => {

            }
        );
    }

    public onTestedForSyphilisSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PatientEducationFormGroup.get('SyphilisTestUsed').enable({ onlySelf: true });
            this.PatientEducationFormGroup.get('SyphilisResults').enable({ onlySelf: true });
            this.PatientEducationFormGroup.get('treatedSyphilis').enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.PatientEducationFormGroup.get('SyphilisTestUsed').setValue('');
            this.PatientEducationFormGroup.get('SyphilisTestUsed').disable({ onlySelf: true });

            this.PatientEducationFormGroup.get('SyphilisResults').setValue('');
            this.PatientEducationFormGroup.get('SyphilisResults').disable({ onlySelf: true });

            this.PatientEducationFormGroup.get('treatedSyphilis').setValue('');
            this.PatientEducationFormGroup.get('treatedSyphilis').disable({ onlySelf: true });
        }
    }

    public onSyphilisResultsSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Positive') {
            this.PatientEducationFormGroup.get('treatedSyphilis').enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Negative') {
            this.PatientEducationFormGroup.get('treatedSyphilis').setValue('');
            this.PatientEducationFormGroup.get('treatedSyphilis').disable({ onlySelf: true });
            this.PatientEducationFormGroup.get('treatedSyphilis').clearValidators();
            this.PatientEducationFormGroup.get('treatedSyphilis').updateValueAndValidity();
        }
    }

    public addTopics() {
        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;
        resultsDialogConfig.width = '600px';
        resultsDialogConfig.height = '300px';

        resultsDialogConfig.data = {
            isEdit: this.isEdit,
            counsellingOptions: this.counsellingOptions,
            maxDate: this.maxDate
        };

        const dialogRef = this.dialog.open(PatientCounsellingComponent, resultsDialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                const topic = data.counselledOn.itemName;
                const topicId = data.counselledOn.itemId;

                const counsellingDates = data.counsellingDate;

                if (topic === '' ||
                    counsellingDates === '') {
                    this.snotifyService.warning('counselling topic, counselling date required', this.notificationService.getConfig());
                    return false;
                }


                if (this.counselling_data.filter(x => x.counsellingTopic === topic
                    && moment(x.CounsellingDate).format('DD-MM-YYYY') === moment(counsellingDates).format('DD-MM-YYYY'))
                    .length > 0) {
                    this.snotifyService.warning('' + topic + ' exists at the same day', 'Counselling',
                        this.notificationService.getConfig());
                } else {
                    this.counselling_data.push({
                        counselledOn: parseInt(topicId, 10),
                        counsellingTopic: topic,
                        counsellingTopicId: topicId,
                        description: 'n/a',
                        CounsellingDate: counsellingDates

                    });
                    this.counselling_editlist.push({
                        counselledOn: parseInt(topicId, 10),
                        counsellingTopic: topic,
                        counsellingTopicId: topicId,
                        description: 'n/a',
                        CounsellingDate: counsellingDates,
                        Id: 0
                    });
                }

            });

    }

    public removeRow(idx) {


        let Id: number;
        
       if (this.counselling_editlist.length > 0){
            Id = parseInt(this.counselling_editlist[idx].Id, 10);


            if (Id > 0) {
                this.ancService.deletePatientCounselling(Id).subscribe(x => {
                    if (x) {
                        console.log(x);
                        console.log(x['patientCounsellingId']);
                        this.snotifyService.success('Successfully removed the counselled topic ',
                            'Patient Counselled Topic', this.notificationService.getConfig());
                        this.counselling_data.splice(idx, 1);
                        this.counselling_editlist.splice(idx, 1);
                    }
                },
                    (err) => {
                        this.snotifyService.success('Error removing the counselled topic' + err,
                            'Patient Counselled Topic', this.notificationService.getConfig());
                    });

            } else {
                this.counselling_data.splice(idx, 1);
                this.counselling_editlist.splice(idx, 1);
            }
        } else {
            this.counselling_data.splice(idx, 1);
        }

        // this.counselling_data.splice(idx, 1);
        console.log(this.counselling_editlist);
        console.log (this.counselling_data);

    }

    public getPatientCounselingDataAll(patientId: number): void {
        this.patientCounseling$ = this.ancService.getPatientCounselingInfoAll(patientId)
            .subscribe(
                p => {
                    if (p) {
                        for (let i = 0; i < p.length; i++) {
                            this.counselling_data.push({
                                counselledOn: p[i]['counsellingTopicId'],
                                counsellingTopic: p[i]['counsellingTopic'],
                                counsellingTopicId: p['counsellingTopicId'],
                                description: p[i]['description'],
                                CounsellingDate: p[i]['counsellingDate']
                            });

                            this.counselling_editlist.push({
                                counselledOn: p[i]['counsellingTopicId'],
                                counsellingTopic: p[i]['counsellingTopic'],
                                counsellingTopicId: p['counsellingTopicId'],
                                description: p[i]['description'],
                                CounsellingDate: p[i]['counsellingDate'],
                                Id: p[i]['id']
                            });
                        }
                    }
                },
                (err) => {

                },
                () => {

                }
            );
    }

    public getPatientCounselingData(patientId: number, patientMasterVisitId: number): void {
        this.patientCounseling$ = this.ancService.getPatientCounselingInfo(patientId, patientMasterVisitId)
            .subscribe(
                p => {
                    if (p) {
                        for (let i = 0; i < p.length; i++) {
                            this.counselling_data.push({
                                counselledOn: p[i]['counsellingTopicId'],
                                counsellingTopic: p[i]['counsellingTopic'],
                                counsellingTopicId: p['counsellingTopicId'],
                                description: p[i]['description'],
                                CounsellingDate: p[i]['counsellingDate']
                            });
                        }
                    }
                },
                (err) => {

                },
                () => {

                }
            );
    }

    public getBaselineAncProfile(patientId: number): void {
        this.baseline$ = this.ancService.getBaselineAncProfile(patientId)
            .subscribe(
                p => {
                    const baseline = p;
                    if (baseline['id'] > 0) {
                        this.PatientEducationFormGroup.get('breastExamDone').setValue(baseline['breastExamDone']);
                        this.PatientEducationFormGroup.get('treatedSyphilis').setValue(baseline['treatedForSyphilis']);
                        this.PatientEducationFormGroup.get('SyphilisTestUsed').setValue(baseline['syphilisTestUsed']);
                        this.PatientEducationFormGroup.get('SyphilisResults').setValue(baseline['syphilisResults']);
                        this.PatientEducationFormGroup.get('testedSyphilis').setValue(baseline['testedForSyphilis']);
                    }
                },
                error1 => {

                },
                () => {

                }
            );
    }

    ngOnDestroy(): void {
        this.patientCounseling$.unsubscribe();
    }


}

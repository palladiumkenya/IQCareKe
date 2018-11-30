import { Component, OnInit, Output, EventEmitter, Input, ViewChild, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TriageService } from '../_services/triage.service';
import { AddPatientVitalCommand } from '../_models/AddPatientVitalCommand';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { EncounterService } from '../../shared/_services/encounter.service';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { CalculateZscoreCommand } from '../_models/CalculateZscoreCommand';

@Component({
    selector: 'app-triage',
    templateUrl: './triage.component.html',
    styleUrls: ['./triage.component.css']
})
export class TriageComponent implements OnInit {
    @Input('PatientId') PatientId: number;
    public PersonId : number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    public maxDate = moment().toDate();

 vitalsDataTable: any [] = [];
 displayedColumns = ['visitdate', 'height', 'weight', 'bmi', 'diastolic', 'systolic', 'temperature', 'respiratoryrate', 'heartrate',
        'action'];
    dataSource = new MatTableDataSource(this.vitalsDataTable);
    @ViewChild(MatPaginator) paginator: MatPaginator;


    vitalsFormGroup: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    userId: number;
    encounterTypeId: number;

    constructor(private _formBuilder: FormBuilder,
        private triageService: TriageService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public zone: NgZone,
        private router: Router, private route: ActivatedRoute,
        private encounterService: EncounterService,
        private lookupItemService: LookupItemService) { }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                this.PatientId = params.patientId;
                this.PersonId = params.personId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.lookupItemService.getByGroupNameAndItemName('EncounterType', 'Triage-encounter').subscribe(
            (result) => {
                if (result) {
                    this.encounterTypeId = result['itemId'];
                }
            },
            (error) => {
                console.log(error);
            }
        );

        this.vitalsFormGroup = this.BuildVitalsFormGroup();
        this.notify.emit(this.vitalsFormGroup);
        this.getPatientVitalsInfo(this.PatientId);
        this.calculateZscore(7);
    }


    public BuildVitalsFormGroup(): FormGroup {
        return this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            height: new FormControl('', [Validators.required, Validators.max(270)]),
            weight: new FormControl('', [Validators.required, Validators.max(600)]),
            bmi: new FormControl({ value: 0, disabled: true }, [Validators.required]),
            headCircumference: new FormControl(''),
            muac: new FormControl(''),
            weightForAge: new FormControl(''),
            weightForHeight: new FormControl(''),
            bmiZ: new FormControl(''),
            bpDiastolic: new FormControl('', [Validators.required]),
            bpSystolic: new FormControl('', [Validators.required]),
            temperature: new FormControl('', [Validators.required]),
            respiratoryRate: new FormControl(''),
            heartRate: new FormControl(''),
            spo2: new FormControl(''),
            comment: new FormControl('')
        });
    }



    public SubmitPatientVitalInfo() {
        if (this.vitalsFormGroup.invalid) {
            return;
        }

        // call service to add patientmastervisitid
        // however check if there exists a patientmstervisit for the person on the same day
        // if its exists just return the id
        // after return patientVitalCommand.PatientmasterVisitId = result.patientmastervisitid

        const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
            EncounterDate: new Date(),
            PatientId: this.PatientId,
            EncounterType: this.encounterTypeId,
            ServiceAreaId: 0,
            UserId: this.userId
        };

        const patientVitalCommand: AddPatientVitalCommand = {
            PatientId: this.PatientId,
            PatientmasterVisitId: this.PatientMasterVisitId,
            Temperature: this.vitalsFormGroup.get('temperature').value,
            RespiratoryRate: this.vitalsFormGroup.get('respiratoryRate').value,
            HeartRate: this.vitalsFormGroup.get('heartRate').value,
            BpDiastolic: this.vitalsFormGroup.get('bpDiastolic').value,
            BpSystolic: this.vitalsFormGroup.get('bpSystolic').value,
            Height: this.vitalsFormGroup.get('height').value,
            Weight: this.vitalsFormGroup.get('weight').value,
            Spo2: this.vitalsFormGroup.get('spo2').value,
            Bmi: this.vitalsFormGroup.get('bmi').value,
            HeadCircumference: this.vitalsFormGroup.get('headCircumference').value,
            BmiZ: this.vitalsFormGroup.get('bmiZ').value,
            WeightForAge: this.vitalsFormGroup.get('weightForAge').value,
            WeightForHeight: this.vitalsFormGroup.get('weightForHeight').value,
            Comment: this.vitalsFormGroup.get('comment').value,
            Muac: this.vitalsFormGroup.get('muac').value,
            VisitDate: this.vitalsFormGroup.get('visitDate').value
        };

        this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
            (result) => {
                this.PatientMasterVisitId = result.patientMasterVisitId;
                patientVitalCommand.PatientmasterVisitId = this.PatientMasterVisitId;

                this.triageService.AddPatientVitalInfo(patientVitalCommand).subscribe(
                    res => {
                        console.log(`Add Patient Vital info`);
                        console.log(res);
                        this.snotifyService.success('Successfully added vitals ', 'Triage', this.notificationService.getConfig());
                    },
                    (err) => {
                        this.snotifyService.error('Error saving triage ' + err, 'Triage', this.notificationService.getConfig());
                    },
                    () => {
                        this.snotifyService.success('Patient vitals information added sucessfully', 'Triage',
                            this.notificationService.getConfig());

                        this.vitalsFormGroup.reset();
                        this.vitalsFormGroup.clearValidators();

                        this.getPatientVitalsInfo(this.PatientMasterVisitId);
                    }
                );
            },
            (error) => {
                this.snotifyService.error('Error saving patientmastervisitid ' + error, 'Triage', this.notificationService.getConfig());
            },
            () => {

            }
        );
    }

    public calculateBmi() {
        const bmi = this.triageService.calculateBmi(this.vitalsFormGroup.get('weight').value,
            this.vitalsFormGroup.get('height').value);

        this.vitalsFormGroup.controls['bmi'].setValue(bmi.toFixed(2));
    }


    public calculateZscore(personId:number) {
        var patientInfo = this.triageService.getPersonDetails(personId);
        if(patientInfo == null)
           return ;

        if(!this.triageService.qualifiesForZscoreCalculation(patientInfo.dateOfBirth))
          return;
        
        const calculateZscoreCommand : CalculateZscoreCommand = {
           DateOfBirth :patientInfo.dateOfBirth,
           Weight : this.vitalsFormGroup.get('weight').value,
           Height : this.vitalsFormGroup.get('height').value,
           Sex : patientInfo.gender == "Male" ? 1 : 2
        }
        var result = this.triageService.calculateZscore(calculateZscoreCommand);
        console.log(result);
    }


    public getPatientVitalsInfo(masterVisitId: number) {
        this.triageService.GetPatientVitalsInfo(masterVisitId).subscribe(res => {
            if (res == null) {
                return;
            }

            this.vitalsDataTable = [];

            res.forEach(info => {
                this.vitalsDataTable.push({
                    visitDate: info.visitDate,
                    height: info.height,
                    weight: info.weight,
                    bmi: info.bmi,
                    headCircumference: info.headCircumference,
                    muac: info.muac,
                    weightForAge: info.weightForAge,
                    weightForHeight: info.weightForHeight,
                    bmiZ: info.bmiZ,
                    diastolic: info.bpDiastolic,
                    systolic: info.bpSystolic,
                    temperature: info.temperature,
                    respiratoryRate: info.respiratoryRate,
                    heartRate: info.heartRate,
                    spo2: info.spo2,
                    comment: info.comment
                });

                this.dataSource = new MatTableDataSource(this.vitalsDataTable);
                this.dataSource.paginator = this.paginator;

            });

        }, (err) => {
            console.log(err + ' An error occured while getting patient vitals info');

        }, () => {

        }
        );
    }


}

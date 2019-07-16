import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { TriageService } from '../../_services/triage.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { EncounterService } from '../../../shared/_services/encounter.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import { CalculateZscoreCommand } from '../../_models/CalculateZscoreCommand';
import { PatientMasterVisitEncounter } from '../../../pmtct/_models/PatientMasterVisitEncounter';
import { AddPatientVitalCommand, UpdatePatientVitalCommand } from '../../_models/AddPatientVitalCommand';
import * as moment from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { VitalsValidationConstants } from '../../_models/VitalsValidationConstants';

@Component({
    selector: 'app-add-triage',
    templateUrl: './add-triage.component.html',
    styleUrls: ['./add-triage.component.css']
})
export class AddTriageComponent implements OnInit {

    patientId: number;
    personId: number;
    patientMasterVisitId: number;
    public maxDate = moment().toDate();
    dialogTitle: string;
    isEdit: boolean = false;
    triageInfo: any;

    vitalsFormGroup: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    userId: number;
    encounterTypeId: number;
    vitalsId: number;

    VitalsValidation: VitalsValidationConstants;


    constructor(private _formBuilder: FormBuilder,
        private triageService: TriageService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private encounterService: EncounterService,
        private lookupItemService: LookupItemService,
        private personService: PersonHomeService,
        private dialogRef: MatDialogRef<AddTriageComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {

        this.dialogTitle = 'Add Triage Info';
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

        this.patientId = data.patientId;
        this.personId = data.personId;
        this.isEdit = data.isEdit;
        this.triageInfo = data.triageInfo;
        this.vitalsFormGroup = this.BuildVitalsFormGroup();

        this.vitalsFormGroup.controls.height.setValue(data.height);
        this.vitalsFormGroup.controls.weight.setValue(data.weight);
        this.calculateBmi();
        if (this.isEdit) {
            this.dialogTitle = 'Update Triage Info';
            this.setTriageFormValues(this.triageInfo);
        }

        this.notify.emit(this.vitalsFormGroup);
    }

    ngOnInit() {
    }


    public BuildVitalsFormGroup(): FormGroup {

        this.VitalsValidation = new VitalsValidationConstants();
        return this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            height: new FormControl('', [Validators.required,
            Validators.pattern(this.VitalsValidation.NumberValidationPattern),
            Validators.max(this.VitalsValidation.MaximumHeight),
            Validators.min(this.VitalsValidation.MinimumHeight)]),
            weight: new FormControl('', [Validators.required,
            Validators.max(this.VitalsValidation.MaximumWeight),
            Validators.min(this.VitalsValidation.MinimumWeight),
            Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            bmi: new FormControl({ value: 0, disabled: true }, [Validators.required]),
            headCircumference: new FormControl('', [
                Validators.min(this.VitalsValidation.MinimumHeadCircumference),
                Validators.max(this.VitalsValidation.MaximumHeadCircumference),
                Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            muac: new FormControl('', [Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            weightForAge: new FormControl({ value: 0, disabled: true }),
            weightForHeight: new FormControl({ value: 0, disabled: true }),
            bmiZ: new FormControl({ value: 0, disabled: true }),
            bpDiastolic: new FormControl(''),
            bpSystolic: new FormControl(''),
            temperature: new FormControl('', [Validators.required,
            Validators.max(this.VitalsValidation.MaximumTemperature),
            Validators.min(this.VitalsValidation.MinimumTemperature),
            Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            respiratoryRate: new FormControl('', [Validators.max(this.VitalsValidation.RespiratoryRateMax),
            Validators.min(this.VitalsValidation.RespiratoryRateMin),
            Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            heartRate: new FormControl('', [Validators.max(this.VitalsValidation.HeartRateMax),
            Validators.min(this.VitalsValidation.HeartRateMin),
            Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            spo2: new FormControl('', [Validators.pattern(this.VitalsValidation.NumberValidationPattern)]),
            comment: new FormControl('')
        });
    }

    public calculateBmi() {
        if (this.vitalsFormGroup.get('weight').invalid || this.vitalsFormGroup.get('height').invalid) {
            return;
        }
        const bmi = this.triageService.calculateBmi(this.vitalsFormGroup.get('weight').value, this.vitalsFormGroup.get('height').value);
        this.vitalsFormGroup.controls['bmi'].setValue(bmi.toFixed(2));
    }


    public calculateZscore() {
        if (this.vitalsFormGroup.get('weight').invalid || this.vitalsFormGroup.get('height').invalid) {
            return;
        }

        const bmi = this.triageService.calculateBmi(this.vitalsFormGroup.get('weight').value,
            this.vitalsFormGroup.get('height').value);
        this.vitalsFormGroup.controls['bmi'].setValue(bmi.toFixed(2));

        this.personService.getPatientByPersonId(this.personId).subscribe(
            person => {
                if (person == null) {
                    return;
                }

                const calculateZscoreCommand: CalculateZscoreCommand = {
                    DateOfBirth: person.dateOfBirth,
                    Weight: this.vitalsFormGroup.get('weight').value,
                    Height: this.vitalsFormGroup.get('height').value,
                    Sex: person.gender == 'Male' ? 1 : 2
                };

                this.triageService.calculateZscore(calculateZscoreCommand).subscribe(result => {
                    this.vitalsFormGroup.controls['weightForAge'].setValue(result.weightForAge);
                    this.vitalsFormGroup.controls['weightForHeight'].setValue(result.weightForHeight);
                    this.vitalsFormGroup.controls['bmiZ'].setValue(result.bmiz);
                });
            },
            (err) => {
                console.log(err);
            },
            () => {

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
            PatientId: this.patientId,
            EncounterType: this.encounterTypeId,
            ServiceAreaId: 0,
            UserId: this.userId
        };

        const patientVitalCommand = this.buildPatientVitalsModel(this.vitalsFormGroup);

        this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
            (result) => {
                this.patientMasterVisitId = result.patientMasterVisitId;
                patientVitalCommand.PatientmasterVisitId = this.patientMasterVisitId;

                this.triageService.AddPatientVitalInfo(patientVitalCommand).subscribe(
                    res => {
                        console.log(`Add Patient Vital info`);
                        console.log(res);
                        this.snotifyService.success('Patient vitals information added sucessfully', 'Triage',
                            this.notificationService.getConfig());
                        this.dialogRef.close();
                    },
                    (err) => {
                        this.snotifyService.error('Error saving triage ' + err, 'Triage', this.notificationService.getConfig());
                    },
                    () => {
                        location.reload();
                        this.vitalsFormGroup.reset();
                    }
                );
            },
            (error) => {
                this.snotifyService.error(error, 'Triage', this.notificationService.getConfig());
            },
            () => {

            }
        );
    }


    public UpdatePatientVitalInfo() {
        if (this.vitalsFormGroup.invalid)
            return;

        const patientVitalsInfo = this.buildPatientVitalsModel(this.vitalsFormGroup);
        patientVitalsInfo.PatientId = this.patientId;
        patientVitalsInfo.PatientmasterVisitId = this.patientMasterVisitId;
        patientVitalsInfo.Id = this.vitalsId;

        const updateVitalsCommand: UpdatePatientVitalCommand = {
            PatientVitalInfo: patientVitalsInfo
        }


        this.triageService.UpdatePatientVitalInfo(updateVitalsCommand).subscribe(
            res => {
                console.log(res);
                this.snotifyService.success('Patient vitals information updated sucessfully', 'Triage', this.notificationService.getConfig());
            },
            (err) => {
                this.snotifyService.error('Error updating vitals ' + err, 'Triage', this.notificationService.getConfig());
            },
            () => {
                this.dialogRef.close();
                location.reload();
                this.vitalsFormGroup.reset();
            }
        );

    }



    public setTriageFormValues(triageInfo: any) {
        this.patientId = triageInfo.patientId;
        this.patientMasterVisitId = triageInfo.patientMasterVisitId;
        this.vitalsId = triageInfo.id;
        this.vitalsFormGroup.controls['visitDate'].setValue(triageInfo.visitDate);
        this.vitalsFormGroup.controls['height'].setValue(triageInfo.height);
        this.vitalsFormGroup.controls['weight'].setValue(triageInfo.weight);
        this.vitalsFormGroup.controls['bmi'].setValue(triageInfo.bmi)
        this.vitalsFormGroup.controls['temperature'].setValue(triageInfo.temperature);
        this.vitalsFormGroup.controls['respiratoryRate'].setValue(triageInfo.respiratoryRate);
        this.vitalsFormGroup.controls['heartRate'].setValue(triageInfo.heartRate);
        this.vitalsFormGroup.controls['bpDiastolic'].setValue(triageInfo.diastolic);
        this.vitalsFormGroup.controls['bpSystolic'].setValue(triageInfo.systolic);
        this.vitalsFormGroup.controls['spo2'].setValue(triageInfo.spo2);
        this.vitalsFormGroup.controls['weightForAge'].setValue(triageInfo.weightForAge);
        this.vitalsFormGroup.controls['weightForHeight'].setValue(triageInfo.weightForHeight);
        this.vitalsFormGroup.controls['comment'].setValue(triageInfo.comment);
        this.vitalsFormGroup.controls['headCircumference'].setValue(triageInfo.headCircumference);
        this.vitalsFormGroup.controls['muac'].setValue(triageInfo.muac);
        this.vitalsFormGroup.controls['bmiZ'].setValue(triageInfo.bmiZ);
    }

    /**
     * buildPatientVitalsModel
     */
    public buildPatientVitalsModel(formGroup: FormGroup): AddPatientVitalCommand {
        const patientVitalCommand: AddPatientVitalCommand = {
            PatientId: this.patientId,
            PatientmasterVisitId: this.patientMasterVisitId,
            Temperature: this.vitalsFormGroup.get('temperature').value ? this.vitalsFormGroup.get('temperature').value : 0.00,
            RespiratoryRate: this.vitalsFormGroup.get('respiratoryRate').value ? this.vitalsFormGroup.get('respiratoryRate').value : 0.00,
            HeartRate: this.vitalsFormGroup.get('heartRate').value ? this.vitalsFormGroup.get('heartRate').value : 0.00,
            BpDiastolic: this.vitalsFormGroup.get('bpDiastolic').value ? this.vitalsFormGroup.get('bpDiastolic').value : 0,
            BpSystolic: this.vitalsFormGroup.get('bpSystolic').value ? this.vitalsFormGroup.get('bpSystolic').value : 0,
            Height: this.vitalsFormGroup.get('height').value,
            Weight: this.vitalsFormGroup.get('weight').value,
            Spo2: this.vitalsFormGroup.get('spo2').value ? this.vitalsFormGroup.get('spo2').value : 0.00,
            Bmi: this.vitalsFormGroup.get('bmi').value,
            HeadCircumference: this.vitalsFormGroup.get('headCircumference').value ?
                this.vitalsFormGroup.get('headCircumference').value : 0.00,
            BmiZ: this.vitalsFormGroup.get('bmiZ').value,
            WeightForAge: this.vitalsFormGroup.get('weightForAge').value,
            WeightForHeight: this.vitalsFormGroup.get('weightForHeight').value,
            Comment: this.vitalsFormGroup.get('comment').value,
            Muac: this.vitalsFormGroup.get('muac').value ? this.vitalsFormGroup.get('muac').value : 0.00,
            VisitDate: moment(this.vitalsFormGroup.get('visitDate').value).toDate(),
            CreatedBy: this.userId
        };

        return patientVitalCommand;
    }


    public close() {
        this.dialogRef.close();
    }

}

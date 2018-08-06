import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';
export interface Topic {
  value: string;
  viewValue: string;
}

export interface PatientEducation {
    position: number;
    dateDone: string;
    topic: string;
}
const PatientEducation_Data: PatientEducation[] = [
    {position: 1, dateDone: '11/11/2017', topic: 'Birth plans'}
] ;

@Component({
  selector: 'app-patient-education-examination',
  templateUrl: './patient-education-examination.component.html',
  styleUrls: ['./patient-education-examination.component.css']
})

export class PatientEducationExaminationComponent implements OnInit {
    PatientEducationFormGroup: FormGroup;
  topics: Topic[] = [
    {value: '0', viewValue: 'Select'},
    {value: '1', viewValue: 'Birth Plans'},
    {value: '2', viewValue: 'Danger Signs'},
    {value: '3', viewValue: 'Breast Care'}
  ];

  displayedColumns: string[] = ['position', 'dateDone', 'topic'];
    dataSource = PatientEducation_Data;

  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.PatientEducationFormGroup = this._formBuilder.group({
        breastExamDone: ['', Validators.required],
        counselledOn: ['', Validators.required]
    });
  }

}

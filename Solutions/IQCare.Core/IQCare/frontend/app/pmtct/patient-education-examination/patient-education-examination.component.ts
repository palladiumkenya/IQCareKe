import { Component, OnInit } from '@angular/core';
export interface Topic {
  value: string;
  viewValue: string;
}


@Component({
  selector: 'app-patient-education-examination',
  templateUrl: './patient-education-examination.component.html',
  styleUrls: ['./patient-education-examination.component.css']
})
export class PatientEducationExaminationComponent implements OnInit {

  topics: Topic[] = [
    {value: '0', viewValue: 'Select'},
    {value: '1', viewValue: 'Birth Plans'},
    {value: '2', viewValue: 'Danger Signs'},
    {value: '3', viewValue: 'Breast Care'}
  ];
  constructor() { }

  ngOnInit() {
  }

}

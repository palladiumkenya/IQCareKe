import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';

export interface AntenatalProfile {
    testName: string;
    dateDone: string;
    results: string;
    position: number;
}

const AntenatalProfile_Data: AntenatalProfile[] = [
    {position: 1, testName: 'Blood Group', dateDone: '11/11/2017', results: 'O+'}
] ;

@Component({
  selector: 'app-antenatal-profile',
  templateUrl: './antenatal-profile.component.html',
  styleUrls: ['./antenatal-profile.component.css']
})
export class AntenatalProfileComponent implements OnInit {

    AntenatalProfileFormGroup: FormGroup;
    displayedColumns: string[] = ['position', 'testName', 'dateDone', 'results'];
    dataSource = AntenatalProfile_Data;

  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.AntenatalProfileFormGroup = this._formBuilder.group({
        treatedSyphilis: ['', Validators.required]
    });
  }

}

import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';
export interface Topic {
    value: number;
    viewValue: string;
  }

@Component({
  selector: 'app-hiv-status',
  templateUrl: './hiv-status.component.html',
  styleUrls: ['./hiv-status.component.css']
})

export class HivStatusComponent implements OnInit {

    hivTestings: Topic[] = [
        {value: 1, viewValue: 'Initial'},
        {value: 2, viewValue: 'Retest;'}
    ];

    Tests: Topic[] = [
        {value: 1, viewValue: 'HIV Test-1'},
        {value: 2, viewValue: 'HIV Test-2;'}
    ];

    KitNames: Topic[] = [
        {value: 1, viewValue: 'Determine'},
        {value: 2, viewValue: 'First Response'},
        {value: 3, viewValue: 'Other'}
    ];

    testResults: Topic[] = [
        {value: 1, viewValue: 'Negative'},
        {value: 2, viewValue: 'Positive'},
        {value: 3, viewValue: 'Invalid'}
    ];

    finalResults: Topic[] = [
        {value: 1, viewValue: 'Negative'},
        {value: 2, viewValue: 'Positive'},
        {value: 3, viewValue: 'Inconclusive'}
    ];

    HIVStatusFormGroup: FormGroup;
  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit() {

      this.HIVStatusFormGroup = this._formBuilder.group({
          testingDone: ['', Validators.required],
          hivTest: ['', Validators.required],
          kitName: ['', Validators.required],
          testResult: ['', Validators.required]
      });
  }

}

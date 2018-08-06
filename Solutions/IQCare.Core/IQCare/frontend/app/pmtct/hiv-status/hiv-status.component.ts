import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';

@Component({
  selector: 'app-hiv-status',
  templateUrl: './hiv-status.component.html',
  styleUrls: ['./hiv-status.component.css']
})

export class HivStatusComponent implements OnInit {

    HIVStatusFormGroup: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}

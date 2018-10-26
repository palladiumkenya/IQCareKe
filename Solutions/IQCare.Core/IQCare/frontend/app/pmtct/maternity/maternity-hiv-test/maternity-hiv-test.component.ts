import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';

@Component({
  selector: 'app-maternity-hiv-test',
  templateUrl: './maternity-hiv-test.component.html',
  styleUrls: ['./maternity-hiv-test.component.css']
})
export class MaternityHivTestComponent implements OnInit {
 hivTestFormGroup: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}

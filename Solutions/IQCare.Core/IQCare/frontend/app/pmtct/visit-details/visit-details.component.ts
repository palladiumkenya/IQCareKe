import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-visit-details',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.css']
})
export class VisitDetailsComponent implements OnInit {
    visitDetailsFormGroup: FormGroup;
    secondFormGroup: FormGroup;
    isLinear: true;
  constructor(private fb: FormBuilder) {

  }

  ngOnInit() {
    this.visitDetailsFormGroup = this.fb.group({
        visitDate: ['', Validators.required],
        ancVisitNumber: ['', Validators.required],
        dateLMP: ['', Validators.required],
        dateEDD: ['', Validators.required],
        ageAtMenarche: ['', Validators.required],
        parityOne: ['', Validators.required],
        parityTwo: ['', Validators.required]
    });
  }

  test(lmp: Date) {
      alert(lmp);
  }
}

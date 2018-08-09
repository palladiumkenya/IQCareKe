import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-anc',
  templateUrl: './anc.component.html',
  styleUrls: ['./anc.component.css']
})
export class AncComponent implements OnInit {

    isLinear: true;
    
    constructor() {}

  ngOnInit() {
   /* this.ancFormGroup = this.fb.group({
        visitDate: ['', Validators.required],
        dateLMP: ['', Validators.required],
        dateEDD: ['', Validators.required],
        ageAtMenarche: ['', Validators.required],
        parityOne: ['', Validators.required],
        parityTwo: ['', Validators.required]
    });

    this.secondFormGroup = this.fb.group({
        secondCtrl: ['', Validators.required],
    });  */
  }
}

import { Component, OnInit } from '@angular/core';
import {FamilyScreening} from '../_models/familyScreening';

@Component({
  selector: 'app-family-screening',
  templateUrl: './family-screening.component.html',
  styleUrls: ['./family-screening.component.css']
})
export class FamilyScreeningComponent implements OnInit {
    familyScreening: FamilyScreening;

    constructor() { }
    ngOnInit() {
        this.familyScreening = new FamilyScreening();
    }

    onSubmit() {
        console.log(this.familyScreening);
    }
}

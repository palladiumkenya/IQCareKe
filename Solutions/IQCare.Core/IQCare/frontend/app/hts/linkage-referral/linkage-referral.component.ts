import { Component, OnInit } from '@angular/core';
import {Referral} from '../_models/referral';

@Component({
  selector: 'app-linkage-referral',
  templateUrl: './linkage-referral.component.html',
  styleUrls: ['./linkage-referral.component.css']
})
export class LinkageReferralComponent implements OnInit {
    referral: Referral;

    constructor() { }

    ngOnInit() {
        this.referral = new Referral();
    }

}

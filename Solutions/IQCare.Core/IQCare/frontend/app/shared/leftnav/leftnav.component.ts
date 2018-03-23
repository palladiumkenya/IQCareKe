import { Component, OnInit } from '@angular/core';
import {DataService} from '../_services/data.service';

@Component({
  selector: 'app-leftnav',
  templateUrl: './leftnav.component.html',
  styleUrls: ['./leftnav.component.css']
})
export class LeftnavComponent implements OnInit {
    hasConsented: boolean;
    isPositive: boolean;
    isReferred: boolean;
    hasConsentedPartnerListing: boolean;

    constructor(private dataService: DataService) { }
    ngOnInit() {
        this.dataService.currentHasConsented.subscribe(hasConsented => this.hasConsented = hasConsented);
        this.dataService.currentIsPositive.subscribe(isPositive => this.isPositive = isPositive);
    }
}

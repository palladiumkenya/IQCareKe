import { Component, OnInit } from '@angular/core';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-referrals',
  templateUrl: './referrals.component.html',
  styleUrls: ['./referrals.component.css']
})
export class ReferralsComponent implements OnInit {
  fromsFacilities: Options[] = [
    {value: '0', viewValue: 'Another Health Facility'},
    {value: '0', viewValue: 'Community Unit'},
    {value: '0', viewValue: 'N/A'}
  ];
  toFacilities: Options[] = [
    {value: '0', viewValue: 'Another Health Facility'},
    {value: '0', viewValue: 'Community Unit'},
    {value: '0', viewValue: 'N/A'}
  ];
  constructor() { }

  ngOnInit() {
  }

}

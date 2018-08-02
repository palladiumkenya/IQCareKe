import { Component, OnInit } from '@angular/core';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-partner-testing',
  templateUrl: './partner-testing.component.html',
  styleUrls: ['./partner-testing.component.css']
})
export class PartnerTestingComponent implements OnInit {
  services: Options[] = [
    {value: '0', viewValue: 'Yes'},
    {value: '0', viewValue: 'No'},
    {value: '0', viewValue: 'N/A'}
  ];
  partners: Options[] = [
    {value: '0', viewValue: 'Positive'},
    {value: '0', viewValue: 'Negative'},
    {value: '0', viewValue: 'Know Positive'},
    {value: '0', viewValue: 'N/A'}
  ];

  constructor() { }

  ngOnInit() {
  }

}

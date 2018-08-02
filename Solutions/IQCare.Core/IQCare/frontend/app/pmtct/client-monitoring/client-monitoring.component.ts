import { Component, OnInit } from '@angular/core';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-client-monitoring',
  templateUrl: './client-monitoring.component.html',
  styleUrls: ['./client-monitoring.component.css']
})
export class ClientMonitoringComponent implements OnInit {

  whostages: Options[] = [
    {value: '0', viewValue: 'Select'},
    {value: '1', viewValue: 'WHO stage1'},
    {value: '2', viewValue: 'WHO stage2'},
    {value: '3', viewValue: 'WHO stage3'}
  ];

  sampleTakens: Options[] = [
    {value: '0', viewValue: 'Select'},
    {value: '1', viewValue: 'YES'},
    {value: '2', viewValue: 'NO'},
    {value: '3', viewValue: 'N/A'}
  ];

  tbs: Options[] = [
    {value: '0', viewValue: 'Select'},
    {value: '0', viewValue: 'Pr TB  = Presumed TB'},
    {value: '0', viewValue: 'No TB  = Negative TB screen'},
    {value: '0', viewValue: 'INH    = Client was screened negative & started INH'},
    {value: '0', viewValue: 'TB Rx  = Client on TB treatment'},
    {value: '0', viewValue: 'Not Done'}
  ];

  caMethods: Options[] = [
    {value: '0', viewValue: 'Select'},
    {value: '0', viewValue: 'VILI'},  
    {value: '0', viewValue: 'VIA'},
    {value: '0', viewValue: 'Not Done'}
  ];

caResults: Options[]  = [
  {value: '0', viewValue: 'Normal'},
  {value: '0', viewValue: 'Suspected'},
  {value: '0', viewValue: 'Confirmed'},
  {value: '0', viewValue: 'N/A'}
];

  constructor() { }

  ngOnInit() {
  }

}

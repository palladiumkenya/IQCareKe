import { Component, OnInit } from '@angular/core';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-preventive-services',
  templateUrl: './preventive-services.component.html',
  styleUrls: ['./preventive-services.component.css']
})
export class PreventiveServicesComponent implements OnInit {
  services: Options[] = [
    {value: '0', viewValue: 'Tetanus Toxoid 1'},
    {value: '0', viewValue: 'Tetanus Toxoid 2'},
    {value: '0', viewValue: 'Tetanus Toxoid 3'},
    {value: '0', viewValue: 'Tetanus Toxoid 4'},
    {value: '0', viewValue: 'Tetanus Toxoid 5'},
    {value: '0', viewValue: 'Malaria Prophylaxis (IPTp1)'},
    {value: '0', viewValue: 'Malaria Prophylaxis (IPTp2)'},
    {value: '0', viewValue: 'Malaria Prophylaxis (IPTp3)'},
    {value: '0', viewValue: 'Dewormed'},
    {value: '0', viewValue: 'Vitamins'},
    {value: '0', viewValue: 'Calcium'},
    {value: '0', viewValue: 'Iron'},
    {value: '0', viewValue: 'Folate'}
  ];

  constructor() { }

  ngOnInit() {
  }

}

import { Component, OnInit } from '@angular/core';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-haart-prophylaxis',
  templateUrl: './haart-prophylaxis.component.html',
  styleUrls: ['./haart-prophylaxis.component.css']
})
export class HaartProphylaxisComponent implements OnInit {
yesnos: Options[] = [
  {value: '0', viewValue: 'Select'},
  {value: '0', viewValue: 'Yes'},
  {value: '0', viewValue: 'No'},
  {value: '0', viewValue: 'N/A'},
];
  constructor() { }

  ngOnInit() {
  }

}

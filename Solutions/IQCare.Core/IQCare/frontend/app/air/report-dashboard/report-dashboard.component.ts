import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-report-dashboard',
  templateUrl: './report-dashboard.component.html',
  styleUrls: ['./report-dashboard.component.css']
})
export class ReportDashboardComponent implements OnInit {
  @Input('ReportingForm') ReportingForm : any;
  
  constructor() 
  { 

  }
  ngOnInit() 
  {
     console.log(this.ReportingForm.Id +' Reporting Form Info')
  }

}

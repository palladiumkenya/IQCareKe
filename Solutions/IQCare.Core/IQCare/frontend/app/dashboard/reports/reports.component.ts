import { Component, OnInit } from '@angular/core';
import { FormDetailsService } from '../../air/_services/formdetails.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  configuredForms : any[] = [];
  
  constructor(private formReportingService : FormDetailsService) {
  }

  ngOnInit() 
  {
    this.getConfiguredForms();
    console.log(this.configuredForms.length +' configured active Forms')
  }

  private getConfiguredForms() {
    this.formReportingService.getConfiguredReportingForms().subscribe(res=>
     {
       this.configuredForms = res;
       console.log(res.length + ' Forms Length')
     })
  }
}

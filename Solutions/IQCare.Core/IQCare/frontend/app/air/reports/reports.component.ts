import { Component, OnInit } from '@angular/core';
import { FormDetailsService } from '../../air/_services/formdetails.service';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  configuredForms: Observable<any[]>;
  
  constructor(private formReportingService : FormDetailsService) {
  }

  ngOnInit() 
  {
    
   this.configuredForms = this.formReportingService.activeConfiguredReportingForms.asObservable();
    this.formReportingService.getConfiguredReportingForms();
  }

  
}

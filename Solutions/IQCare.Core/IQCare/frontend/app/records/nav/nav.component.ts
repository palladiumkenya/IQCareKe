import { Component, OnInit } from '@angular/core';
 // import { NavigationService } from '../services/navigationservice;
import { Observable } from 'rxjs';
import { NavigationService } from '../services/navigationservice';
@Component({
  // tslint:disable-next-line:component-selector
  selector: 'Record-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class RecordsNavComponent implements OnInit {
    profile: Observable<boolean>;
    constructor(
        private navService: NavigationService) { }
  

    ngOnInit() {

        this.navService.RegistrationProfile();
        this.profile = this.navService.patientprofile;
  }

}

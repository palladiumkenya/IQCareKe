import { Component, OnInit } from '@angular/core';
import { NavigationService } from './../services/navigationservice';
import { Observable } from 'rxjs';
@Component({
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

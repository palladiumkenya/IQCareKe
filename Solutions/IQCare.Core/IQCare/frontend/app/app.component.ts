import { NgxSpinnerService } from 'ngx-spinner';
import { Router, Event, NavigationStart, NavigationCancel, NavigationError, NavigationEnd } from '@angular/router';
import { Component, isDevMode } from '@angular/core';
import { AppLoadService } from './shared/_services/appload.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'app';

    constructor(private appLoadService: AppLoadService,
        private router: Router,
        private spinner: NgxSpinnerService) {
        localStorage.setItem('facilityList', JSON.stringify(this.appLoadService.getFacilities()));

        if (isDevMode) {
            localStorage.setItem('appLocation', 'Demo Site');
            localStorage.setItem('appLocationId', '755');
            localStorage.setItem('appPosID', '13056');
            localStorage.setItem('appUserId', '1');
            localStorage.setItem('appUserName', 'System Admin');
            localStorage.setItem('serviceAreaId', '3');
        }

        this.router.events.subscribe((event: Event) => {
            switch (true) {
                case event instanceof NavigationStart: {
                    this.spinner.show();
                    break;
                }

                case event instanceof NavigationEnd:
                case event instanceof NavigationCancel:
                case event instanceof NavigationError: {
                    this.spinner.hide();
                    break;
                }

                default: {
                    break;
                }
            }
        });
    }
}

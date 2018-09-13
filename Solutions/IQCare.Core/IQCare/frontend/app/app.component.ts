import { Component, isDevMode } from '@angular/core';
import { AppLoadService } from './shared/_services/appload.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'app';

    constructor(private appLoadService: AppLoadService) {
        localStorage.setItem('facilityList', JSON.stringify(this.appLoadService.getFacilities()));

        if (isDevMode) {
            localStorage.setItem('appLocation', 'Demo Site');
            localStorage.setItem('appLocationId', '755');
            localStorage.setItem('appPosID', '13056');
            localStorage.setItem('appUserId', '1');
            localStorage.setItem('appUserName', 'System Admin');
            localStorage.setItem('serviceAreaId', '3');
        }
    }
}

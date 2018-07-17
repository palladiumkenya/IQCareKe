import { Component } from '@angular/core';
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
    }
}

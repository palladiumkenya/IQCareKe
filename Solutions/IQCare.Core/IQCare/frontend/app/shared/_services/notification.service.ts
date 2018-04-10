import { Injectable } from '@angular/core';
import {SnotifyPosition, SnotifyService, SnotifyToastConfig} from 'ng-snotify';

@Injectable()
export class NotificationService {
    timeout = 3000;
    position: SnotifyPosition = SnotifyPosition.centerCenter;
    progressBar = true;
    closeClick = true;
    newTop = true;

    constructor(private snotifyService: SnotifyService) { }

    /*
        Change global configuration
    */

    getConfig(): SnotifyToastConfig {
        this.snotifyService.setDefaults({
            global: {
                newOnTop: this.newTop,
            }
        });

        return {
            position: this.position,
            timeout: this.timeout,
            showProgressBar: this.progressBar,
            closeOnClick: this.closeClick,
        };
    }

}

import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { consentReducer } from './shared/reducers/app.reducers';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AppLoadService } from './shared/_services/appload.service';
import { AppStateService } from './shared/_services/appstate.service';
import { ErrorHandlerService } from './shared/_services/errorhandler.service';


export function init_app(appStateService: AppStateService) {
    return () => appStateService.initializeAppState();
}

@NgModule({
    declarations: [
        AppComponent

    ],
    imports: [
        BrowserModule,
        CoreModule,
        BrowserAnimationsModule,
        SnotifyModule,
        StoreModule.forRoot({ app: consentReducer })
    ],
    providers: [
        {
            provide: 'SnotifyToastConfig',
            useValue: ToastDefaults
        },
        {
            provide: APP_INITIALIZER, useFactory: init_app, deps: [AppStateService], multi: true
        },
        SnotifyService,
        AppLoadService,
        AppStateService,
        ErrorHandlerService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

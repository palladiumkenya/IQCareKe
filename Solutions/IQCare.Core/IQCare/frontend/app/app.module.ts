import { BrowserModule } from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import {consentReducer} from './shared/reducers/app.reducers';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import {AppLoadService} from './shared/_services/appload.service';

/*export function onAppInitGetFacilities(appLoadService: AppLoadService) {
    return () => appLoadService.loadFacilities();
}*/

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
      SnotifyService,
      AppLoadService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

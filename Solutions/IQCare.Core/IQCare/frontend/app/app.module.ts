import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import {consentReducer} from './shared/reducers/app.reducers';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
      BrowserModule,
      CoreModule,
      BrowserAnimationsModule,
      StoreModule.forRoot({ app: consentReducer })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

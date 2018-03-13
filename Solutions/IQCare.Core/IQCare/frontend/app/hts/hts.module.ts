import { NgModule, Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {MatDatepickerModule, MatFormFieldModule, MatInputModule, MatNativeDateModule} from '@angular/material';

import { HtsRoutingModule } from './hts-routing.module';
import { EncounterComponent } from './encounter/encounter.component';
import { LinkageReferralComponent } from './linkage-referral/linkage-referral.component';
import { EncounterService } from './_services/encounter.service';
import { LinkageReferralService } from './_services/linkage-referral.service';
import { PnsformComponent } from './pnsform/pnsform.component';
import { PnsTracingComponent } from './pnstracing/pnstracing.component';
import { FamilyTracingComponent } from './family-tracing/family-tracing.component';
import { FamilyScreeningComponent } from './family-screening/family-screening.component';
import { NoneEventsDirective } from './_directives/none-events.directive';
import { LinkageComponent } from './linkage/linkage.component';


@NgModule({
  imports: [
      CommonModule,
      HttpClientModule,
      HtsRoutingModule,
      FormsModule,
      MatDatepickerModule,
      MatFormFieldModule,
      MatNativeDateModule,
      MatInputModule
  ],
  declarations: [
    EncounterComponent,
    LinkageReferralComponent,
    PnsformComponent,
    PnsTracingComponent,
    FamilyTracingComponent,
    FamilyScreeningComponent,
    NoneEventsDirective,
    LinkageComponent
  ],
  exports: [
  ],
  providers: [
      EncounterService,
      LinkageReferralService
  ]
})
export class HtsModule { }

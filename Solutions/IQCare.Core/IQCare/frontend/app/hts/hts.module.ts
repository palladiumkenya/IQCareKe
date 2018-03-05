import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';

import { HtsRoutingModule } from './hts-routing.module';
import { EncounterComponent } from './encounter/encounter.component';
import { LinkageReferralComponent } from './linkage-referral/linkage-referral.component';
import { EncounterService } from './_services/encounter.service';
import { PnsformComponent } from './pnsform/pnsform.component';
import { PnsTracingComponent } from './pnstracing/pnstracing.component';
import { FamilyTracingComponent } from './family-tracing/family-tracing.component';
import { FamilyScreeningComponent } from './family-screening/family-screening.component';

@NgModule({
  imports: [
      CommonModule,
      HttpClientModule,
      HtsRoutingModule,
      FormsModule,
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
  ],
  declarations: [
    EncounterComponent,
    LinkageReferralComponent,
    PnsformComponent,
    PnsTracingComponent,
    FamilyTracingComponent,
    FamilyScreeningComponent
  ],
  exports: [
  ],
  providers: [
      EncounterService
  ]
})
export class HtsModule { }

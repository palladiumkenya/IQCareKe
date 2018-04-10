import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
    MatAutocompleteModule,
    MatButtonModule, MatButtonToggleModule, MatCardModule, MatCheckboxModule, MatChipsModule,
    MatDatepickerModule, MatDialogModule, MatDividerModule, MatExpansionModule, MatFormFieldModule,
    MatGridListModule, MatIconModule, MatInputModule, MatListModule, MatMenuModule,
    MatNativeDateModule, MatPaginatorModule, MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule, MatSnackBarModule,
    MatSortModule, MatStepperModule, MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';

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
import {SharedModule} from '../shared/shared.module';
import { PnsPartnersComponent } from './pns-partners/pns-partners.component';
import {PnsService} from './_services/pns.service';
import {DataService} from '../shared/_services/data.service';
import { TestingComponent } from './testing/testing.component';
import {TestDialogComponent} from './testdialog/testdialog.component';
import {NotificationService} from '../shared/_services/notification.service';
import {AppLoadService} from '../shared/_services/appload.service';
import { FamilyComponent } from './family/family.component';
import {FamilyService} from './_services/family.service';
import {FamilyScreeningResolver} from './family-screening/familyScreening.resolver';
import {AppStateService} from '../shared/_services/appstate.service';


@NgModule({
  imports: [
      CommonModule, HttpClientModule, HtsRoutingModule, FormsModule, MatDatepickerModule, MatFormFieldModule,
      MatNativeDateModule, MatInputModule, SharedModule, MatDatepickerModule, MatFormFieldModule, MatNativeDateModule,
      MatTableModule, MatAutocompleteModule, MatButtonModule, MatButtonToggleModule, MatCardModule,
      MatCheckboxModule, MatChipsModule, MatDatepickerModule, MatDialogModule, MatDividerModule, MatExpansionModule,
      MatGridListModule, MatIconModule, MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
      MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule, MatSidenavModule, MatSliderModule,
      MatSlideToggleModule, MatSnackBarModule, MatSortModule, MatStepperModule, MatTableModule, MatTabsModule,
      MatToolbarModule, MatTooltipModule, ReactiveFormsModule
  ],
  declarations: [
    EncounterComponent,
    LinkageReferralComponent,
    PnsformComponent,
    PnsTracingComponent,
    FamilyTracingComponent,
    FamilyScreeningComponent,
    NoneEventsDirective,
    LinkageComponent,
    PnsPartnersComponent,
    TestingComponent,
      TestDialogComponent,
      FamilyComponent
  ],
  exports: [
  ],
  providers: [
      EncounterService,
      LinkageReferralService,
      PnsService,
      DataService,
      NotificationService,
      AppLoadService,
      FamilyService,
      FamilyScreeningResolver,
      AppStateService
  ],
  entryComponents: [
      TestDialogComponent
  ]
})
export class HtsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AirRoutingModule } from './air-routing.module';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {FormDetailResolver} from'./_services/customformdetails.resolver';
import {FormDetailsService} from './_services/formdetails.service';
import {SubSectionFilterPipe, IndicatorFilterPipe} from './_model/pipe/subsectionfilter.pipe';
import { NativeDateAdapter, DateAdapter, MatDatepicker } from '@angular/material';
import { CustomDateAdapter }  from './_model/CustomDateAdapter';
import { NotificationService } from '../shared/_services/notification.service';

import {
  MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
  MatCardModule,     MatCheckboxModule, MatChipsModule, MatDatepickerModule,
  MatDialogModule, MatDividerModule, MatExpansionModule,
  MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
  MatListModule, MatMenuModule,    MatNativeDateModule, MatPaginatorModule,
  MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
  MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
  MatSnackBarModule, MatSortModule, MatStepperModule,
  MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';
@NgModule({
  declarations: [ActiveFormReportComponent,SubSectionFilterPipe,IndicatorFilterPipe],
  imports: [
    CommonModule,
    AirRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule,     MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule,    MatNativeDateModule, MatPaginatorModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatSortModule, MatStepperModule,
    MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule

  ],
  providers:[
    FormDetailResolver,
    FormDetailsService,
    NotificationService,
  { provide: DateAdapter, useClass: CustomDateAdapter },
  ]
})
export class AirModule { }

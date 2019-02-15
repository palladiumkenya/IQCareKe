import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AirRoutingModule } from './air-routing.module';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
  declarations: [ActiveFormReportComponent],
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

  ]
})
export class AirModule { }

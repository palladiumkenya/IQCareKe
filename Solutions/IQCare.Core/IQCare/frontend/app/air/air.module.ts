import { ReportsComponent } from './reports/reports.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AirRoutingModule } from './air-routing.module';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormDetailResolver } from './_services/customformdetails.resolver';
import { FormDetailsService } from './_services/formdetails.service';
import { SubSectionFilterPipe, IndicatorFilterPipe } from './_model/pipe/subsectionfilter.pipe';
import { NativeDateAdapter, DateAdapter, MatDatepicker } from '@angular/material';
import { CustomDateAdapter } from './_model/CustomDateAdapter';
import { NotificationService } from '../shared/_services/notification.service';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { IndicatorService } from './_services/indicator.service';
import { IndicatorResultsGridComponent } from './indicator-results-grid/indicator-results-grid.component';
import { RouterModule } from '@angular/router';
import { ReportSectionSettingComponent } from './report-section-setting/report-section-setting.component';
import {

    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatSortModule, MatStepperModule,        // <----- import for date formating(optional)
    MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';
import { ReportDashboardComponent } from './report-dashboard/report-dashboard.component';

@NgModule({

    imports: [FormsModule, ReactiveFormsModule, RouterModule, AirRoutingModule,
        CommonModule,
        MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
        MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
        MatDialogModule, MatDividerModule, MatExpansionModule,
        MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
        MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
        MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
        MatSnackBarModule, MatSortModule, MatStepperModule,        // <----- import for date formating(optional)
        MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule],
    exports: [
        ReportDashboardComponent
    ],
    declarations: [
        ActiveFormReportComponent,
        ReportIndicatorResultComponent,
        IndicatorReportingPeriodComponent,
        IndicatorResultsGridComponent,
        SubSectionFilterPipe,
        IndicatorFilterPipe,
        ReportSectionSettingComponent,
        ReportDashboardComponent,
        ReportsComponent
    ],

    providers: [
        FormDetailResolver,
        FormDetailsService,
        NotificationService,
        { provide: DateAdapter, useClass: CustomDateAdapter },
        AirRoutingModule,
        IndicatorService

    ]

})
export class AirModule { }

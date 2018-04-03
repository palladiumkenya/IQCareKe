import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule, MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule, MatListModule, MatMenuModule,
    MatNativeDateModule, MatPaginatorModule, MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule, MatSortModule,
    MatStepperModule,
    MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';

import { RegistrationRoutingModule } from './registration-routing.module';
import { PersonComponent } from './person/person.component';
import {RegistrationService} from './_services/registration.service';
import { EnrollmentComponent } from './enrollment/enrollment.component';
import {EnrollmentService} from './_services/enrollment.service';
import { HomeComponent } from './home/home.component';
import {SharedModule} from '../shared/shared.module';
import { SearchComponent } from './search/search.component';
import {SearchService} from './_services/search.service';
import {AlertService} from '../shared/_services/alert.service';
import {EncounterService} from '../hts/_services/encounter.service';
import {NotificationService} from '../shared/_services/notification.service';
import {AppLoadService} from '../shared/_services/appload.service';



@NgModule({
    imports: [
        CommonModule,
        RegistrationRoutingModule,
        FormsModule,
        HttpClientModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatNativeDateModule,
        MatInputModule,
        SharedModule,
        MatTableModule,
        MatAutocompleteModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatCardModule,
        MatCheckboxModule,
        MatChipsModule,
        MatDialogModule,
        MatDividerModule,
        MatExpansionModule,
        MatGridListModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatMenuModule,
        MatNativeDateModule,
        MatPaginatorModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatRadioModule,
        MatRippleModule,
        MatSelectModule,
        MatSidenavModule,
        MatSliderModule,
        MatSlideToggleModule,
        MatSnackBarModule,
        MatSortModule,
        MatStepperModule,
        MatTableModule,
        MatTabsModule,
        MatToolbarModule,
        MatTooltipModule
    ],
    declarations: [
        PersonComponent,
        EnrollmentComponent,
        HomeComponent,
        SearchComponent
    ],
    providers: [
        RegistrationService,
        EnrollmentService,
        SearchService,
        AlertService,
        EncounterService,
        NotificationService,
        AppLoadService
    ]

})
export class RegistrationModule { }

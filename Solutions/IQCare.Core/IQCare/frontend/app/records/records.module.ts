import { PersonRegistrationService } from './_services/person-registration.service';
import { OccupationResolver } from './_services/occupation.resolver';
import { CountyResolver } from './_services/county.resolver';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {
    MatPaginatorModule, MatTableModule, MatSortModule,
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule, MatNativeDateModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatStepperModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';

import { RecordsRoutingModule } from './records-routing.module';
import { SearchComponent } from './search/search.component';
import { RegisterComponent } from './person/register/register.component';
import { SharedModule } from '../shared/shared.module';
import { CountyService } from './_services/county.service';
import { RecordsService } from './_services/records.service';
import { GenderResolver } from './_services/gender.resolver';
import { MaritalStatusResolver } from './_services/maritalstatus.resolver';
import { EducationLevelResolver } from './_services/educationallevel.resolver';

@NgModule({
    imports: [
        CommonModule,
        RecordsRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
        MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
        MatDialogModule, MatDividerModule, MatExpansionModule,
        MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
        MatListModule, MatMenuModule, MatNativeDateModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
        MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
        MatSnackBarModule, MatStepperModule, MatTabsModule, MatToolbarModule, MatTooltipModule,
        SharedModule
    ],
    declarations: [SearchComponent, RegisterComponent],
    providers: [
        CountyResolver,
        CountyService,
        GenderResolver,
        RecordsService,
        MaritalStatusResolver,
        EducationLevelResolver,
        OccupationResolver,
        PersonRegistrationService
    ]
})
export class RecordsModule { }

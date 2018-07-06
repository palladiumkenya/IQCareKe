import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import {
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatSortModule, MatStepperModule,
    MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';

import { RecordsRoutingModule } from './records-routing.module';
import { RecordsHomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { RecordsNavComponent } from './nav/nav.component';
import { RegistrationService } from './services/RecordsRegistrationService';
import { RecordsRegisterComponent } from'./register/register.component';
import { RegistrationResolver } from './services/recordsregistration.resolver';
import { EducationOppConsentResolver } from './services/recordsregistration.resolver';
import { ConsentTypeResolver } from './services/recordsregistration.resolver';
import { IdentifierTypeResolver } from './services/recordsregistration.resolver';


@NgModule({
    imports: [CommonModule, RecordsRoutingModule, ReactiveFormsModule, FormsModule,
        HttpClientModule, MatDatepickerModule, MatFormFieldModule, MatNativeDateModule,
        MatInputModule, SharedModule, MatTableModule, MatAutocompleteModule, MatButtonModule,
        MatButtonToggleModule, MatCardModule, MatCheckboxModule, MatChipsModule, MatDialogModule,
        MatDividerModule, MatExpansionModule, MatGridListModule, MatIconModule, MatInputModule,
        MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule, MatProgressBarModule,
        MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule,
        MatSidenavModule, MatSliderModule, MatSlideToggleModule, MatSnackBarModule,
        MatSortModule, MatStepperModule, MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule
    ],
    declarations: [RecordsHomeComponent, RecordsNavComponent, RecordsRegisterComponent

    ],

    providers: [RegistrationService,RegistrationResolver,EducationOppConsentResolver,ConsentTypeResolver,IdentifierTypeResolver]
})

export class RecordModule {}

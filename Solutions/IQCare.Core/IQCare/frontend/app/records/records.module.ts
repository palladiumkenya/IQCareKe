import { PersonRegistrationService } from './_services/person-registration.service';
import { OccupationResolver } from './_services/occupation.resolver';
import { CountyResolver } from './_services/county.resolver';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { QueueModule } from './../queue/queue.module';

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
import { RelationshipResolver } from './_services/relationship.resolver';
import { ViewComponent } from './person/view/view.component';
import { ConsentSmsResolver } from './_services/consentsms.resolver';
import { ContactCategoryResolver } from './_services/contactcategory.resolver';
import { PersoncontactsComponent } from './person/personcontacts/personcontacts.component';
import { PersonIdentifiersResolver } from './_services/personidentifiers.resolver';
import { InlineSearchComponent } from './inline-search/inline-search.component';
import { YesNoResolver } from '../pmtct/_services/yesno.resolver';
import { CheckDuplicatesComponent } from './person/check-duplicates/check-duplicates.component';

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
        SharedModule,
        QueueModule
    ],
    declarations: [SearchComponent, RegisterComponent,
        ViewComponent, PersoncontactsComponent,
        InlineSearchComponent, CheckDuplicatesComponent],
    providers: [
        CountyResolver,
        CountyService,
        GenderResolver,
        RecordsService,
        MaritalStatusResolver,
        EducationLevelResolver,
        OccupationResolver,
        PersonRegistrationService,
        RelationshipResolver,
        ConsentSmsResolver,
        ContactCategoryResolver,
        PersonIdentifiersResolver,
        YesNoResolver
    ],
    entryComponents: [
        PersoncontactsComponent,
        InlineSearchComponent,
        CheckDuplicatesComponent,

    ],
    exports: [
        InlineSearchComponent,
        CheckDuplicatesComponent
    ]
})
export class RecordsModule { }

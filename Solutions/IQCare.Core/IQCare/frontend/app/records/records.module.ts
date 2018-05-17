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
    declarations: [RecordsHomeComponent,RecordsNavComponent

    ]
})

export class RecordModule {}

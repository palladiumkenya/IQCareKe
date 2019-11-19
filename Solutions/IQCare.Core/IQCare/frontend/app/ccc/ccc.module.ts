import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CccRoutingModule} from './ccc-routing.module';
import { ActivityFormComponent } from './otz/activity-form/activity-form.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.module';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatTableModule} from '@angular/material/table';
import {MatDividerModule} from '@angular/material/divider';
import {MatDialogModule} from '@angular/material/dialog';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatRadioModule} from '@angular/material/radio';
import {MatIconModule} from '@angular/material/icon';
import { ModulesCoveredComponent } from './otz/modules-covered/modules-covered.component';
import { OtzEnrollmentComponent } from './otz/otz-enrollment/otz-enrollment.component';
import { EncounterHistoryComponent } from './otz/encounter-history/encounter-history.component';
import { ViewOtzFormComponent } from './otz/view-otz-form/view-otz-form.component';

@NgModule({
    declarations: [ActivityFormComponent, ModulesCoveredComponent, OtzEnrollmentComponent, EncounterHistoryComponent, ViewOtzFormComponent],
    imports: [
        CommonModule, ReactiveFormsModule, SharedModule, MatSelectModule,
        CccRoutingModule, MatFormFieldModule, MatDatepickerModule, MatNativeDateModule,
        MatButtonModule, MatInputModule, MatAutocompleteModule, MatTableModule,
        MatDividerModule, MatDialogModule, MatPaginatorModule, MatRadioModule,
        MatIconModule, FormsModule],
    entryComponents: [
        ModulesCoveredComponent
    ],    
})
export class CccModule { }

import {
    MatStepperModule, MatButtonModule,
    MatFormFieldModule, MatSelectModule,
    MatNativeDateModule, MatDatepickerModule,
    MatInputModule, MatAutocompleteModule, MatTableModule, MatDividerModule, MatDialogModule, MatIconModule,
    MatPaginatorModule, MatRadioModule, MatCheckboxModule
} from '@angular/material';
import { PharmacyService } from './services/pharmacy.service';
import { ReactiveFormsModule } from '@angular/forms';
import { AdultRegimenClassificationResolver, PaedsClassificationResolver } from './services/regimenclassifications.resolver';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PharmRoutingModule } from './pharm-routing-module';
import { PharmOrderformComponent } from './pharm-orderform/pharm-orderform.component';
import { FrequencyTypeResolver } from './services/frequencytype.resolver';
import { ActiveModulesResolver } from './services/activemodules.resolver';
import { TreatmentStartedResolver } from './services/TreatmentStarted.resolver';
import {PersonCurrentVitalsResolver } from './services/Vitals.resolver';
import {NumberDirective} from './OnlyNumberDirective';

@NgModule({
    declarations: [
        PharmOrderformComponent, NumberDirective],
    imports: [
        SharedModule, MatDatepickerModule, MatNativeDateModule, PharmRoutingModule,
        CommonModule, MatStepperModule, ReactiveFormsModule,
        MatButtonModule, MatFormFieldModule, MatSelectModule,
        MatInputModule, MatAutocompleteModule, MatTableModule, MatDividerModule, MatDialogModule,
        MatPaginatorModule, MatRadioModule,
        MatIconModule, ReactiveFormsModule, FormsModule, MatCheckboxModule
    ],
    providers: [
        AdultRegimenClassificationResolver, PaedsClassificationResolver, PharmacyService,
        FrequencyTypeResolver, ActiveModulesResolver, TreatmentStartedResolver, 
        PersonCurrentVitalsResolver
    ]
})

export class PharmModule { }
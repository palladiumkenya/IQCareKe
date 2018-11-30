import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TriageComponent } from './triage/triage.component';
import { ClinicalRoutingModule } from './clinical-routing.module';
import { TriageService } from './_services/triage.service';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
    MatNativeDateModule, MatPaginatorModule, MatInputModule,
    MatDatepickerModule, MatFormFieldModule,
    MatTableModule, MatButtonModule
} from '@angular/material';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        CommonModule,
        ClinicalRoutingModule,
        ReactiveFormsModule,
        MatPaginatorModule,
        MatNativeDateModule,
        MatInputModule, MatDatepickerModule, MatFormFieldModule,
        MatTableModule, MatButtonModule, FormsModule, HttpClientModule, SharedModule
    ],
    declarations: [
        TriageComponent
    ],
    providers: [TriageService]
})
export class ClinicalModule { }

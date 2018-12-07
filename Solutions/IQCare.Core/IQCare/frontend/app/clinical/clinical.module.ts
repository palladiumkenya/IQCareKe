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
    MatTableModule, MatButtonModule, MatSelectModule
} from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { LabOrderComponent } from '../clinical/lab/lab-order/lab-order.component';
import { LabTestGridComponent } from './lab/lab-test-grid/lab-test-grid.component'

@NgModule({
    imports: [
        CommonModule,
        ClinicalRoutingModule,
        ReactiveFormsModule,
        MatPaginatorModule,
        MatNativeDateModule,
        MatInputModule, MatDatepickerModule, MatFormFieldModule,
        MatTableModule, MatButtonModule, FormsModule, HttpClientModule, SharedModule, MatSelectModule
    ],
    declarations: [
        TriageComponent,
        LabOrderComponent,
        LabTestGridComponent
    ],
    providers: [TriageService]
})
export class ClinicalModule { }

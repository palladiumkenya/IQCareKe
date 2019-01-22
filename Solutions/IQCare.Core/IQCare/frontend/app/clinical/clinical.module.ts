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
    MatTableModule, MatButtonModule, MatSelectModule, MatGridListModule, MatDialogModule, MatCheckboxModule
} from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { LabOrderComponent } from '../clinical/lab/lab-order/lab-order.component';
import { LabTestGridComponent } from './lab/lab-test-grid/lab-test-grid.component'
import { LabTestsResolver } from './_services/labtests.resolver';
import { LabTestReasonsResolver } from './_services/labtestreasons.resolver';
import { LaborderService } from './_services/laborder.service';
import { CompleteLabOrderComponent } from './lab/complete-lab-order/complete-lab-order.component';
import { AddLabResultComponent } from './lab/add-lab-result/add-lab-result.component';
import { LabOrderTestResultsComponent } from './lab/lab-order-test-results/lab-order-test-results.component';
import { PendingLabsGridComponent } from './lab/pending-labs-grid/pending-labs-grid.component';
import { CompletedLabsGridComponent } from './lab/completed-labs-grid/completed-labs-grid.component';
import { AddTriageComponent } from './triage/add-triage/add-triage.component';
import { TriageInfoGridComponent } from './triage/triage-info-grid/triage-info-grid.component';

@NgModule({
    imports: [
        CommonModule,
        ClinicalRoutingModule,
        ReactiveFormsModule,
        MatPaginatorModule,
        MatNativeDateModule,
        MatCheckboxModule,
        MatInputModule, MatDatepickerModule, MatFormFieldModule,MatGridListModule,MatDialogModule,
        MatTableModule, MatButtonModule, FormsModule, HttpClientModule, SharedModule, MatSelectModule
    ],
    declarations: [
        TriageComponent,
        LabOrderComponent,
        LabTestGridComponent,
        CompleteLabOrderComponent,
        AddLabResultComponent,
        LabOrderTestResultsComponent,
        PendingLabsGridComponent,
        CompletedLabsGridComponent,
        AddTriageComponent,
        TriageInfoGridComponent
    ],
    entryComponents :[
     AddLabResultComponent,
     LabOrderTestResultsComponent,
     AddTriageComponent
    ],
    providers: [
        TriageService,
        LabTestsResolver,
        LabTestReasonsResolver,
        LaborderService
    ]
})
export class ClinicalModule { }

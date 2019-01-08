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
    MatTableModule, MatButtonModule, MatSelectModule, MatTableDataSource, MatGridListModule, MatDialog, MatDialogModule
} from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { LabOrderComponent } from '../clinical/lab/lab-order/lab-order.component';
import { LabTestGridComponent } from './lab/lab-test-grid/lab-test-grid.component'
import { LabTestsResolver } from './_services/labtests.resolver';
import { LabTestReasonsResolver } from './_services/labtestreasons.resolver';
import { LaborderService } from './_services/laborder.service';
import { CompleteLabOrderComponent } from './lab/complete-lab-order/complete-lab-order.component';
import { AddLabResultComponent } from './lab/add-lab-result/add-lab-result.component';

@NgModule({
    imports: [
        CommonModule,
        ClinicalRoutingModule,
        ReactiveFormsModule,
        MatPaginatorModule,
        MatNativeDateModule,
        MatInputModule, MatDatepickerModule, MatFormFieldModule,MatGridListModule,MatDialogModule,
        MatTableModule, MatButtonModule, FormsModule, HttpClientModule, SharedModule, MatSelectModule,
    ],
    declarations: [
        TriageComponent,
        LabOrderComponent,
        LabTestGridComponent,
        CompleteLabOrderComponent,
        AddLabResultComponent
    ],
    entryComponents :[
     AddLabResultComponent
    ],
    providers: [
        TriageService,
        LabTestsResolver,
        LabTestReasonsResolver,
        LaborderService
    ]
})
export class ClinicalModule { }

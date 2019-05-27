import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepRoutingModule } from './prep-routing.module';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';
import { PrepSTIScreeningTreatmentComponent } from './prep-sti-screening-treatment/prep-sti-screening-treatment.component';
import { MatStepperModule, MatButtonModule, MatFormFieldModule, MatSelectModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [PrepEncounterHistoryComponent, PrepEncounterComponent, PrepSTIScreeningTreatmentComponent],
    imports: [
        CommonModule, MatStepperModule, ReactiveFormsModule,
        MatButtonModule, MatFormFieldModule, MatSelectModule,
        PrepRoutingModule
    ]
})
export class PrepModule { }

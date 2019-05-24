import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepRoutingModule } from './prep-routing.module';

@NgModule({
    declarations: [PrepEncounterHistoryComponent],
    imports: [
        CommonModule,
        PrepRoutingModule
    ]
})
export class PrepModule { }

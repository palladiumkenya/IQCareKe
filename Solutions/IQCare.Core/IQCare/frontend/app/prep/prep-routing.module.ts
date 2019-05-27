import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';

const routes: Routes = [
    {
        path: '',
        component: PrepEncounterHistoryComponent,
        pathMatch: 'full'
    },
    {
        path: 'encounter',
        component: PrepEncounterComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PrepRoutingModule {

}

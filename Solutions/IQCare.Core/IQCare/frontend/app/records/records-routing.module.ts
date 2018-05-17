import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecordsHomeComponent } from './home/home.component';


const routes: Routes = [
    {
        path: '',
        component: RecordsHomeComponent,
        pathMatch: 'full'

    },
    {
        path: 'patientrecord',
        component: RecordsHomeComponent,
        pathMatch: 'full'
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RecordsRoutingModule {}

import { CountyResolver } from './_services/county.resolver';
import { RegisterComponent } from './person/register/register.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
    {
        path: '',
        component: SearchComponent,
        pathMatch: 'full'
    },
    {
        path: 'person',
        children: [
            {
                path: '',
                component: RegisterComponent,
                resolve: {
                    countiesArray: CountyResolver
                }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RecordsRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {PersonComponent} from './person/person.component';
import {EnrollmentComponent} from './enrollment/enrollment.component';
import {HomeComponent} from './home/home.component';
import {SearchComponent} from './search/search.component';
import {RegistrationResolver} from './_services/registration.resolver';

const routes: Routes = [
    {
        path: '',
        component: SearchComponent,
        pathMatch: 'full',
    },
    {
        path: 'home',
        component: HomeComponent,
        pathMatch: 'full'
    },
    {
        path: 'register',
        component: PersonComponent,
        pathMatch: 'full',
        resolve: {
            options: RegistrationResolver
        }
    },
    {
        path: 'enrollment',
        component: EnrollmentComponent,
        pathMatch: 'full'
    },
    {
        path: 'search',
        component: SearchComponent,
        pathMatch: 'full'
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegistrationRoutingModule { }

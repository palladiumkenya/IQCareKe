import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {PersonComponent} from './person/person.component';

const routes: Routes = [
    {
        path: '',
        component: PersonComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegistrationRoutingModule { }

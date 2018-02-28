import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EncounterComponent } from './encounter/encounter.component';
import { LinkageReferralComponent } from './linkage-referral/linkage-referral.component';

const routes: Routes = [
  { 
    path: '', 
    component: EncounterComponent 
  },
  {
    path: 'linkage',
    component: LinkageReferralComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HtsRoutingModule { }

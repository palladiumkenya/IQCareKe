import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EncounterComponent } from './encounter/encounter.component';
import { LinkageReferralComponent } from './linkage-referral/linkage-referral.component';
import {PnsformComponent} from './pnsform/pnsform.component';
import {PnsTracingComponent} from './pnstracing/pnstracing.component';
import {FamilyTracingComponent} from './family-tracing/family-tracing.component';
import {FamilyScreeningComponent} from './family-screening/family-screening.component';
import {LinkageComponent} from './linkage/linkage.component';
import {PnsPartnersComponent} from './pns-partners/pns-partners.component';
import {TestingComponent} from './testing/testing.component';

const routes: Routes = [
    {
        path: '',
        component: EncounterComponent
    },
    {
        path: 'linkage',
        component: LinkageComponent
    },
    {
        path: 'referral',
        component: LinkageReferralComponent
    },
    {
        path: 'pns',
        component: PnsPartnersComponent
    },
    {
        path: 'pnstracing',
        component: PnsTracingComponent
    },
    {
        path: 'familytracing',
        component: FamilyTracingComponent
    },
    {
        path: 'familyscreening',
        component: FamilyScreeningComponent
    },
    {
        path: 'pnsform',
        component: PnsformComponent
    },
    {
        path: 'testing',
        component: TestingComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HtsRoutingModule { }

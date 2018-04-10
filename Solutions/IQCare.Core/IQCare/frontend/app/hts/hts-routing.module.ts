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
import {FamilyComponent} from './family/family.component';
import {FamilyScreeningResolver} from './family-screening/familyScreening.resolver';

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
        path: 'family',
        children: [
            {
                path: '',
                component: FamilyComponent
            },
            {
                path: 'tracing',
                component: FamilyTracingComponent
            },
            {
                path: 'screening',
                component: FamilyScreeningComponent,
                resolve: {
                    options: FamilyScreeningResolver
                }
            },
        ]
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

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
import {FamilyTracingResolver} from './family-tracing/familyTracing.resolver';

const routes: Routes = [
    {
        path: '',
        component: EncounterComponent,
        pathMatch: 'full'
    },
    {
        path: 'linkage',
        component: LinkageComponent,
        pathMatch: 'full'
    },
    {
        path: 'referral',
        component: LinkageReferralComponent,
        pathMatch: 'full'
    },
    {
        path: 'pns',
        component: PnsPartnersComponent,
        pathMatch: 'full'
    },
    {
        path: 'pnstracing',
        component: PnsTracingComponent,
        pathMatch: 'full'
    },
    {
        path: 'family',
        children: [
            {
                path: '',
                component: FamilyComponent,
                pathMatch: 'full'
            },
            {
                path: 'tracing',
                component: FamilyTracingComponent,
                pathMatch: 'full',
                resolve: {
                    options: FamilyTracingResolver
                }
            },
            {
                path: 'screening',
                component: FamilyScreeningComponent,
                pathMatch: 'full',
                resolve: {
                    options: FamilyScreeningResolver
                }
            },
        ]
    },
    {
        path: 'pnsform',
        component: PnsformComponent,
        pathMatch: 'full'
    },
    {
        path: 'testing',
        component: TestingComponent,
        pathMatch: 'full'
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HtsRoutingModule { }

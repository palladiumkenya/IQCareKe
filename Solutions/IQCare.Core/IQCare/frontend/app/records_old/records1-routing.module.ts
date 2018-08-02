
import { Routes, RouterModule } from '@angular/router';
import { RecordsHomeComponent } from './home/home.component';
import { RecordsRegisterComponent } from './register/register.component';
import { GenderResolver } from './services/recordsregistration.resolver';
import { RelationshipResolver } from './services/recordsregistration.resolver';
import { ConsentTypeResolver } from './services/recordsregistration.resolver';
import { IdentifierTypeResolver } from './services/recordsregistration.resolver';
import { MaritalStatusResolver } from './services/recordsregistration.resolver';
import { GetPersonDetailsResolver } from './services/recordsregistration.resolver';
import { OccupationResolver } from './services/recordsregistration.resolver';
import { OppEducationResolver } from './services/recordsregistration.resolver';
import { OppConsentResolver } from './services/recordsregistration.resolver';
import { ProfileComponent } from './profile/profile.component';
import { SearchComponent } from './search/search.component';
import { NgModule } from '@angular/core';
const routes: Routes = [
    {
        path: '',
        component: SearchComponent,
        pathMatch: 'full'
    },
    {
        path: 'rec',
        component: RecordsHomeComponent
    },
    {
        path: 'patientprofile',
        component: ProfileComponent,
        pathMatch: 'full',
        resolve: {
            persondetailsresolve: GetPersonDetailsResolver,
            rel: RelationshipResolver,
            occ: OccupationResolver,
            gen: GenderResolver,
            Consent: OppConsentResolver,
            Educ: OppEducationResolver,
            ConsentType: ConsentTypeResolver,
            IdentifyerType: IdentifierTypeResolver,
            maritalstatusresolve: MaritalStatusResolver

        }

    },
    {
        path: 'searchcontact',
        component: SearchComponent,
        pathMatch: 'full'
    },
    {
        path: 'patientrecord',
        component: RecordsHomeComponent,
        pathMatch: 'full'
    },
    {
        path: 'registerclient',
        component: RecordsRegisterComponent,
        pathMatch: 'full',
        resolve: {
            persondetailsresolve: GetPersonDetailsResolver,
            rel: RelationshipResolver,
            occ: OccupationResolver,
            gen: GenderResolver,
            Consent: OppConsentResolver,
            Educ: OppEducationResolver,
            ConsentType: ConsentTypeResolver,
            IdentifyerType: IdentifierTypeResolver,
            maritalstatusresolve: MaritalStatusResolver
        }

    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RecordsRoutingModule { }

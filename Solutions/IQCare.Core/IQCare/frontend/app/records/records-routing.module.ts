import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecordsHomeComponent } from './home/home.component';
import { RecordsRegisterComponent } from './register/register.component';
import { RegistrationResolver } from './services/recordsregistration.resolver';
import { EducationOppConsentResolver } from './services/recordsregistration.resolver';
import { ConsentTypeResolver } from './services/recordsregistration.resolver';
import { IdentifierTypeResolver } from './services/recordsregistration.resolver';
import { MaritalStatusResolver } from './services/recordsregistration.resolver';

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
    },
    {
        path: 'registerclient',
        component: RecordsRegisterComponent,
        pathMatch: 'full',
        resolve: {
            options: RegistrationResolver,
            EducOppConsent: EducationOppConsentResolver,
            ConsentType: ConsentTypeResolver,
            IdentifyerType: IdentifierTypeResolver,
            maritalstatusresolve:MaritalStatusResolver
        }
        
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RecordsRoutingModule {}

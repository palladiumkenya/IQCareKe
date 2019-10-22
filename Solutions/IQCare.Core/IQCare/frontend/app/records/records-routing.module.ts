import { ViewComponent } from './person/view/view.component';
import { MaritalStatusResolver } from './_services/maritalstatus.resolver';
import { GenderResolver } from './_services/gender.resolver';
import { CountyResolver } from './_services/county.resolver';
import { RegisterComponent } from './person/register/register.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { EducationLevelResolver } from './_services/educationallevel.resolver';
import { OccupationResolver } from './_services/occupation.resolver';
import { RelationshipResolver } from './_services/relationship.resolver';
import { ConsentSmsResolver } from './_services/consentsms.resolver';
import { ContactCategoryResolver } from './_services/contactcategory.resolver';
import { PersonIdentifiersResolver } from './_services/personidentifiers.resolver';
import { YesNoResolver } from '../pmtct/_services/yesno.resolver';
import { PatientEncounterComponent } from '../shared/patient-encounter/patient-encounter.component';
import { PersonNHIFIdentifiersResolver} from './_services/personnhifidentifiers.resolver';

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
                    countiesArray: CountyResolver,
                    genderArray: GenderResolver,
                    maritalStatusArray: MaritalStatusResolver,
                    educationLevelArray: EducationLevelResolver,
                    occupationArray: OccupationResolver,
                    relationshipArray: RelationshipResolver,
                    consentSmsArray: ConsentSmsResolver,
                    contactCategoryArray: ContactCategoryResolver,
                    personIdentifiersArray: PersonIdentifiersResolver,
                    PersonNHIFArray: PersonNHIFIdentifiersResolver,
                    yesnoArray: YesNoResolver
                }
            },
            {
                path: 'view/:id',
                component: ViewComponent
            },
            {
                path: 'update/:id',
                component: RegisterComponent,
                resolve: {
                    countiesArray: CountyResolver,
                    genderArray: GenderResolver,
                    maritalStatusArray: MaritalStatusResolver,
                    educationLevelArray: EducationLevelResolver,
                    occupationArray: OccupationResolver,
                    relationshipArray: RelationshipResolver,
                    consentSmsArray: ConsentSmsResolver,
                    contactCategoryArray: ContactCategoryResolver,
                    personIdentifiersArray: PersonIdentifiersResolver,
                    PersonNHIFArray: PersonNHIFIdentifiersResolver,

                    yesnoArray: YesNoResolver
                }
            }
        ]
    },
    {
        path: 'patient-encounter/:patientId/:personId/:serviceAreaId/:serviceName',
        component: PatientEncounterComponent,
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RecordsRoutingModule { }

import { CccComponent } from './enrollment/service-areas/ccc/ccc.component';
import { EnrollmentServicesComponent } from './enrollment/enrollment-services/enrollment-services.component';
import { ServicesResolver } from './services/services.resolver';
import { PersonHomeComponent } from './person-home/person-home.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PortalComponent } from './portal/portal.component';
import { HtsComponent } from './enrollment/service-areas/hts/hts.component';
import { HTSEncounterResolver } from './services/htsencounter.resolver';
import { PersonCurrentVitalsResolver } from './services/personvitals.resolver';
import { RiskEncounterResolver } from './services/riskencounter.resolver';
import { PrepComponent } from './enrollment/service-areas/prep/prep.component';
import { ReenrollmentComponent } from './reenrollment/reenrollment.component';
import { ExitReasonsResolver } from './services/exitreasons.resolver';
import { HTSEncounterHistoryResolver } from './services/getlatesthtsencounterhistory.resolver';
import { PartnerCCCEnrollmentResolver, SexWithoutCondomResolver, PatientIdentifierResolver} from './services/hivpartnerdetails.resolver';
//import { CareendDetailsResolver } from './services/careendeddetails.resolver';
import {FacilityDashboardComponent} from './facility-dashboard/facility-dashboard.component';
import {HtsDashboardComponent} from './hts-dashboard/hts-dashboard.component';
import { GetpatientIdResolver } from './services/getpatientId.resolver';
import { ServiceAreaCareEndDetailsResolver } from './services/serviceareacareenddetails.resolver';

const routes: Routes = [
    {
        path: '',
        component: PortalComponent,
        pathMatch: 'full',
    },
    {
        path: 'personhome/:id',
        component: PersonHomeComponent,
        resolve: {
            servicesArray: ServicesResolver,
            // HTSEncounterArray: HTSEncounterResolver,
            PersonVitalsArray: PersonCurrentVitalsResolver,
            RiskAssessmentArray: RiskEncounterResolver,
            ExitReasonsArray: ExitReasonsResolver,
           // CarendedArray: CareendDetailsResolver,
            HTSEncounterHistoryArray: HTSEncounterHistoryResolver,
            ServiceAreaCareEndArray: ServiceAreaCareEndDetailsResolver
        }
    },
    {
        path: 'facilityDashboard',
        component: FacilityDashboardComponent,
		pathMatch: 'full'
    },
    {
        path: 'HtsDashboard',
        component: HtsDashboardComponent,
		pathMatch: 'full'
    },
    {
        path: 'reenrollment',
        children: [
            {
                path: ':id/:serviceId/:serviceCode',
                component: ReenrollmentComponent
            }
        ]
    },
    {
        path: 'enrollment',
        children: [
            {
                path: ':id/:serviceId/:serviceCode',
                component: EnrollmentServicesComponent
            },
            {
                path: 'hts',
                children: [
                    {
                        path: ':id/:serviceId/:serviceCode',
                        component: HtsComponent
                    },
                    {
                        path: 'update/:id/:serviceId/:serviceCode/:edit',
                        component: HtsComponent
                    }
                ]

            },
            {
                path: 'ccc',
                children: [
                    {
                        path: ':id/:serviceId/:serviceCode',
                        component: CccComponent
                    },
                    {
                        path: 'update/:id/:serviceId/:serviceCode/:edit',
                        component: CccComponent
                    }
                ]
            },
            {
                path: 'prep',
                children: [
                    {
                        path: ':id/:serviceId/:serviceCode',
                        component: PrepComponent,
                        resolve: {
                            PartnerCCCEnrollmentArray: PartnerCCCEnrollmentResolver,
                            SexWithoutCondomArray: SexWithoutCondomResolver,
                            PatientIdentifierArray: PatientIdentifierResolver,
                            PatientIdArray: GetpatientIdResolver
                            
                        }



                    },
                    {
                        path: 'update/:id/:serviceId/:serviceCode/:edit',
                        component: PrepComponent,
                        resolve: {
                            PartnerCCCEnrollmentArray: PartnerCCCEnrollmentResolver,
                            SexWithoutCondomArray: SexWithoutCondomResolver,
                            PatientIdentifierArray: PatientIdentifierResolver,
                            PatientIdArray:GetpatientIdResolver
                        }

                    }
                ]
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardRoutingModule { }

import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalComponent } from './portal/portal.component';
import { PersonHomeComponent } from './person-home/person-home.component';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule
    ],
    declarations: [PortalComponent, PersonHomeComponent]
})
export class DashboardModule { }

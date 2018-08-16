import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalComponent } from './portal/portal.component';
import { PersonHomeComponent } from './person-home/person-home.component';
import {NotificationService} from "../shared/_services/notification.service";

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule
    ],
    declarations: [
        PortalComponent,
        PersonHomeComponent
    ],
    providers: [
        NotificationService
    ]
})
export class DashboardModule { }

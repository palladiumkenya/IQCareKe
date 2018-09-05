import { ServicesResolver } from './services/services.resolver';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalComponent } from './portal/portal.component';
import { PersonHomeComponent } from './person-home/person-home.component';
import { NotificationService } from '../shared/_services/notification.service';
import { ServicesListComponent } from './services-list/services-list.component';
import { MatCardModule } from '@angular/material';
import { EnrollmentComponent } from './enrollment/enrollment.component';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MatCardModule
    ],
    declarations: [
        PortalComponent,
        PersonHomeComponent,
        ServicesListComponent,
        EnrollmentComponent
    ],
    providers: [
        NotificationService,
        ServicesResolver
    ]
})
export class DashboardModule { }

import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import {ClientService} from './_services/client.service';
import { AlertComponent } from './alert/alert.component';
import {PnstracingService} from '../hts/_services/pnstracing.service';
import { PersonbriefComponent } from './personbrief/personbrief.component';
import {DateAdapter, MAT_DATE_FORMATS, MatCardModule} from '@angular/material';
import {APP_DATE_FORMATS, AppDateAdapter} from './dateadapter/momentDateAdapter';
import {NotificationService} from './_services/notification.service';
import {AppLoadService} from './_services/appload.service';
import {AppStateService} from './_services/appstate.service';
import {ErrorHandlerService} from './_services/errorhandler.service';

@NgModule({
    imports: [
        CommonModule,
        SharedRoutingModule,
        MatCardModule
    ],
    declarations: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent,
        PersonbriefComponent
    ],
    exports: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent,
        PersonbriefComponent
    ],
    providers: [
        ClientService,
        NotificationService,
        PnstracingService,
        AppLoadService,
        AppStateService,
        ErrorHandlerService,
        {
            provide: DateAdapter, useClass: AppDateAdapter
        },
        {
            provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
        }
    ]
})
export class SharedModule { }

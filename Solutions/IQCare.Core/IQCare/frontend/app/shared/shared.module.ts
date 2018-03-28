import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import {ClientService} from './_services/client.service';
import { AlertComponent } from './alert/alert.component';
import {PnstracingService} from '../hts/_services/pnstracing.service';
import { PersonbriefComponent } from './personbrief/personbrief.component';
import {MatCardModule} from '@angular/material';
import {consentReducer} from './reducers/app.reducers';

@NgModule({
    imports: [
        CommonModule,
        SharedRoutingModule,
        MatCardModule,
        StoreModule.forRoot({ app: consentReducer })
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
        PersonbriefComponent,
    ],
    providers: [
        ClientService,
        PnstracingService,
    ]
})
export class SharedModule { }

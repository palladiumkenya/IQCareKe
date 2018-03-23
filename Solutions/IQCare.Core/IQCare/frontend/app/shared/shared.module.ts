import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import {ClientService} from './_services/client.service';
import { AlertComponent } from './alert/alert.component';
import {DataService} from './_services/data.service';

@NgModule({
    imports: [
        CommonModule,
        SharedRoutingModule
    ],
    declarations: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent
    ],
    exports: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent
    ],
    providers: [
        ClientService,
        DataService
    ]
})
export class SharedModule { }

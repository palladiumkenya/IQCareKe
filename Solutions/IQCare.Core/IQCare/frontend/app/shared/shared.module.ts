import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import {ClientService} from './_services/client.service';

@NgModule({
    imports: [
        CommonModule,
        SharedRoutingModule
    ],
    declarations: [
        LeftnavComponent,
        ClientbriefComponent
    ],
    exports: [
        LeftnavComponent,
        ClientbriefComponent
    ],
    providers: [
        ClientService
    ]
})
export class SharedModule { }

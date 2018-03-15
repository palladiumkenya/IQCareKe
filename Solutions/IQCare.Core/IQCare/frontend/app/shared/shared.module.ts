import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';

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
    ]
})
export class SharedModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpModule  } from '@angular/http';
import { FormsModule } from '@angular/forms';

//import { ModuleManagerComponent } from './module-manager/module-manager.component';
//import { ModuleManagerService } from './services/module-manager.service';
import { HtsRoutingModule } from './hts-routing.module';
import { EncounterComponent } from './encounter/encounter.component';
import { LinkageReferralComponent } from './linkage-referral/linkage-referral.component';
import { EncounterService } from './_services/encounter.service';

@NgModule({
  imports: [
      CommonModule,
      HttpClientModule,
      HtsRoutingModule,
      FormsModule
  ],
  declarations: [
    //ModuleManagerComponent,
    EncounterComponent,
    LinkageReferralComponent
  ],
  exports:[
    //ModuleManagerComponent
  ],
  providers: [
      EncounterService
    //ModuleManagerService
  ]
})
export class HtsModule { }

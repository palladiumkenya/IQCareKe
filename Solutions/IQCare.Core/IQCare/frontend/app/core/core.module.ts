import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { CoreRoutingModule } from './core-routing.module';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { FooterComponent } from './footer/footer.component';
import { NavHeaderComponent } from './nav-header/nav-header.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import { NavLeftComponent } from './nav-left/nav-left.component';

@NgModule({
  imports: [
      CommonModule,
      HttpClientModule,
      CoreRoutingModule,
      FormsModule
  ],
  declarations: [
    HeaderComponent,
    NotFoundComponent,
    FooterComponent,
    NavHeaderComponent,
    ClientbriefComponent,
    NavLeftComponent
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    RouterModule,
    NavHeaderComponent,
    ClientbriefComponent,
    NavLeftComponent
  ],
  providers: [ ]
})
export class CoreModule { }

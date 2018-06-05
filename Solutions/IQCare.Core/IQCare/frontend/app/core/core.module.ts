import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { CoreRoutingModule } from './core-routing.module';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { FooterComponent } from './footer/footer.component';
import { NavHeaderComponent } from './nav-header/nav-header.component';

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
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    RouterModule,
    NavHeaderComponent
  ],
    providers: [
    ]
})
export class CoreModule { }

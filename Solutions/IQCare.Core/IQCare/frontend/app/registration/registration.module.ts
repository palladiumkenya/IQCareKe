import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {MatDatepickerModule, MatFormFieldModule, MatInputModule, MatNativeDateModule} from '@angular/material';

import { RegistrationRoutingModule } from './registration-routing.module';
import { PersonComponent } from './person/person.component';
import {RegistrationService} from './_services/registration.service';
import { EnrollmentComponent } from './enrollment/enrollment.component';
import {EnrollmentService} from './_services/enrollment.service';
import { HomeComponent } from './home/home.component';
import {SharedModule} from '../shared/shared.module';



@NgModule({
    imports: [
        CommonModule,
        RegistrationRoutingModule,
        FormsModule,
        HttpClientModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatNativeDateModule,
        MatInputModule,
        SharedModule
    ],
    declarations: [
        PersonComponent,
        EnrollmentComponent,
        HomeComponent
    ],
    providers: [
        RegistrationService,
        EnrollmentService
    ]

})
export class RegistrationModule { }

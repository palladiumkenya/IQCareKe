import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import {QueueRoutingModule} from './queue-routing.module'
import {QueueDetailsService} from './services/queue.service'
import {
    MatPaginatorModule, MatTableModule, MatSortModule,
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule, MatNativeDateModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatStepperModule, MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';
import { AddRoomComponent } from './room/add-room/add-room.component';
import { RoomComponent } from './room/room.component';

import { EditRoomComponent } from './room/edit-room/edit-room.component';
import { LinkRoomComponent } from './room/link-room/link-room.component';
@NgModule({
  declarations: [AddRoomComponent, RoomComponent,  EditRoomComponent, LinkRoomComponent],
  imports: [
    CommonModule,
    FormsModule,
    QueueRoutingModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
    MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
    MatListModule, MatMenuModule, MatNativeDateModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
    MatSnackBarModule, MatStepperModule, MatTabsModule, MatToolbarModule, MatTooltipModule,
    SharedModule
  ],
  providers: [
      QueueModule,
      QueueDetailsService
  ]
})
export class QueueModule { }

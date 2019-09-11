import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { QueueRoutingModule } from './queue-routing.module';
import { QueueDetailsService } from './services/queue.service';
import { LookupItemsListResolver } from './services/LookupitemsList.resolver';
import { DialogService } from './services/dialog.service';
import { SearchService } from '../registration/_services/search.service';
import {PersonHomeService } from '../dashboard/services/person-home.service';

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
import { MatConfirmDialogComponent } from './room/mat-confirm-dialog/mat-confirm-dialog.component';
import { AddWaitingComponent } from './waitinglist/add-waiting/add-waiting.component';
import { ViewWaitinglistComponent } from './waitinglist/view-waitinglist/view-waitinglist.component';

@NgModule({
    declarations: [AddRoomComponent, RoomComponent, EditRoomComponent
        , LinkRoomComponent, MatConfirmDialogComponent, AddWaitingComponent, ViewWaitinglistComponent],
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
        QueueDetailsService,
        DialogService,
        LookupItemsListResolver,
        SearchService,
        PersonHomeService

    ],
    entryComponents: [MatConfirmDialogComponent]
})
export class QueueModule { }

import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddRoomComponent } from './room/add-room/add-room.component';
import { EditRoomComponent } from './room/edit-room/edit-room.component';
import { LinkRoomComponent } from './room/link-room/link-room.component';
import { LookupItemsListResolver } from './services/LookupitemsList.resolver';
import { AddWaitingComponent } from './waitinglist/add-waiting/add-waiting.component';
import { ViewWaitinglistComponent } from './waitinglist/view-waitinglist/view-waitinglist.component';

const routes: Routes = [
    {
        path: '',
        component: AddRoomComponent,
        pathMatch: 'full'
    },
    {
        path: 'rooms/edit/:roomId',
        component: EditRoomComponent,
        pathMatch: 'full'
    },
    {
        path: 'addWaitingList/:patientId/:personId',
        component: AddWaitingComponent,
        pathMatch: 'full'

    },
    {
        path: 'view',
        component: ViewWaitinglistComponent,
        pathMatch: 'full'
    },
    {
        path: 'link',
        component: LinkRoomComponent,
        resolve: {
            LookupItemList: LookupItemsListResolver
        }
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class QueueRoutingModule { }

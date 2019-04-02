import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddRoomComponent } from './room/add-room/add-room.component';
import { EditRoomComponent } from './room/edit-room/edit-room.component';
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
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class QueueRoutingModule { }

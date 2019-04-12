
import { Component, OnInit, Output, EventEmitter, Inject, AfterViewInit, ViewChild, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms'
import { SnotifyService } from 'ng-snotify';
import { QueueDetailsService } from '../../services/queue.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { InsertRoom } from '../../models/model';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { Store } from '@ngrx/store';
import { NgxSpinnerService } from 'ngx-spinner';
import * as moment from 'moment';
@Component({
    selector: 'app-add-room',
    templateUrl: './add-room.component.html',
    styleUrls: ['./add-room.component.css']
})
export class AddRoomComponent implements OnInit {
    NewRoom: InsertRoom;
    roomslisting: any[] = [];
    Rooms: any[] = [];
    exists: boolean;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns: any[] = ['roomName', 'displayName', 'description', 'edit'];
    dataSource = new MatTableDataSource(this.Rooms);
    constructor(private _formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private queuedetailsservice: QueueDetailsService,
        private spinner: NgxSpinnerService

    ) {
        this.NewRoom = new InsertRoom();

        this.roomslisting = [];
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
    }

    ngOnInit() {

        this.GetRooms();

        console.log(this.Rooms);
        console.log(this.dataSource);
    }

    GetRooms() {
        this.queuedetailsservice.getRooms().subscribe(
            (result) => {

                this.roomslisting = result['roomsList'];
                console.log(result['roomsList']);
                this.roomslisting.forEach(data => {
                    this.Rooms.push({
                        id: data.id,
                        roomName: data.roomName,
                        displayName: data.displayName,
                        description: data.description

                    });
                });
                this.dataSource = new MatTableDataSource(this.Rooms);
                this.dataSource.paginator = this.paginator;

            }
        );


    }

AddRoom() {
    
        this.spinner.show();





        this.spinner.show();
        if (this.NewRoom.RoomName != '' && this.NewRoom.DisplayName != '') {
            this.queuedetailsservice.checkRoomExist(this.NewRoom.RoomName).subscribe((result) => {
                console.log(result);
                console.log(result['exists']);
                this.exists = result['exists'];
                if (this.exists == true) {
                    this.snotifyService.error('Kindly ensure the roomName is unique since the room exists', 'Add',
                        this.notificationService.getConfig());

                    this.spinner.hide();
                    return;

                } else {
                    const createdby = parseInt(localStorage.getItem('appUserId'));
                    this.queuedetailsservice.addRoom(this.NewRoom.RoomName,
                        this.NewRoom.DisplayName, this.NewRoom.Description,
                        false, createdby
                        // tslint:disable-next-line: no-shadowed-variable
                        , true, moment(new Date()).format('DD-MMM-YYYY')).subscribe((result) => {
                            this.snotifyService.success(result['message'], 'Add Room',
                                this.notificationService.getConfig());
                            this.Rooms = [];
                            this.GetRooms();
                            this.NewRoom.Description = '';
                            this.NewRoom.DisplayName = '';
                            this.NewRoom.RoomName = ' ';

                        },
                            (error) => {
                                this.snotifyService.error('Error Adding  Room Items ' + error, 'Edit',
                                    this.notificationService.getConfig());
                                this.spinner.hide();

                            },
                            () => {
                                this.spinner.hide();
                            }

                        );

                }
            },
                (error) => {
                    this.snotifyService.error('Error Adding  Room Items ' + error, 'Edit',
                        this.notificationService.getConfig());
                    this.spinner.hide();
                },
                () => {
                    this.spinner.hide();
                }
            );

        }
        else {
            this.snotifyService.error('Kindly note displayName and room name is required', 'Add',
                this.notificationService.getConfig());
   if (this.NewRoom.RoomName !== '' && this.NewRoom.DisplayName!== '') {   
    this.snotifyService.error('Kindly note RoomName and DisplayName is required', 'Room',
    this.notificationService.getConfig());
this.spinner.hide();
   }
   else {   
         this.spinner.show();
       
  
        this.queuedetailsservice.checkRoomExist(this.NewRoom.RoomName
            ).subscribe((result) => {
                console.log(result);
                this.exists = result['exists'];
                if (this.exists == true) {
                    this.snotifyService.error('Kindly note  the room name must be unique  ', 'Room',
                        this.notificationService.getConfig());

                    this.spinner.hide();
                    return;

                } else {
                    const createdby = parseInt(localStorage.getItem('appUserId'));
                    this. queuedetailsservice.addRoom(this.NewRoom.RoomName,this.NewRoom.DisplayName,this.NewRoom.Description, false, createdby,true,moment(moment().toDate()).format('DD-MMM-YYYY')).subscribe((result) => {
                            this.snotifyService.success(result['message'], 'Add Room',
                                this.notificationService.getConfig());
                                this.GetRooms();
                                this.NewRoom = new InsertRoom();

                        },
                            (error) => {
                                this.snotifyService.error('Error adding Room  ' + error, 'Room',
                                    this.notificationService.getConfig());
                                this.spinner.hide();
                            },
                            () => {
                                this.spinner.hide();
                             
                                this.GetRooms();
                            }
                        );
                }
            },
                (error) => {
                    this.snotifyService.error('Error adding patient to the waiting list ' + error, 'WaitingList',
                        this.notificationService.getConfig());
                    this.spinner.hide();
                },
                () => {
                    this.spinner.hide();
                }
            );
        }     
    }
    
}
        


}
    


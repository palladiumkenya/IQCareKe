import { Component, OnInit, Input, Output, EventEmitter, ViewChild, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { QueueDetailsService } from '../../services/queue.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
    selector: 'app-edit-room',
    templateUrl: './edit-room.component.html',
    styleUrls: ['./edit-room.component.css']
})
export class EditRoomComponent implements OnInit {
    formGroup: FormGroup;
    EditRoomId: number = 0;
    EditRoomDetails: any[] = [];
    roomslist: any[] = [];
    roomName: string;
    exists: boolean;
    constructor(private formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private queuedetailsservice: QueueDetailsService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        public zone: NgZone,
        private spinner: NgxSpinnerService) {
        this.formGroup = this.formBuilder.group({
            RoomName: new FormControl('', [Validators.required]),
            RoomDisplayName: new FormControl('', [Validators.required]),
            Roomdescription: new FormControl('')

        });

    }

    ngOnInit() {
        this.activatedRoute.params.subscribe((param) => {
            this.EditRoomId = param.roomId;

        });
        this.GetRoomsById();
        console.log(this.EditRoomDetails);


    }
    SetRoomValues() {
        if (this.EditRoomDetails.length > 0) {
            console.log(this.EditRoomDetails[0]['roomName']);
            console.log(this.formGroup);
            this.formGroup.controls['RoomName'].setValue(this.EditRoomDetails[0]['roomName']);
            this.formGroup.controls['RoomDisplayName'].setValue(this.EditRoomDetails[0]['displayName']);
            this.formGroup.controls['Roomdescription'].setValue(this.EditRoomDetails[0]['description']);
        }
    }

    GetRoomsById() {

        if (this.EditRoomId > 0) {
            this.queuedetailsservice.getRoomsById(this.EditRoomId).subscribe((result) => {
                console.log(result);
                this.roomslist = result['roomsList'];
                console.log(this.roomslist);
                console.log(this.roomslist['id']);
                this.EditRoomDetails.push({
                    id: this.roomslist['id'],
                    roomName: this.roomslist['roomName'],
                    displayName: this.roomslist['displayName'],
                    description: this.roomslist['description']
                });
                this.SetRoomValues();

            },
                (error) => {
                    this.snotifyService.error('Error  Editing Indicator Results ' + error, 'Edit',
                        this.notificationService.getConfig());
                }

            );

        }

    }

    submitResult() {
        if (this.formGroup.valid) {
            this.spinner.show();
            console.log(this.formGroup.controls['RoomName'].value);
            this.roomName = this.formGroup.controls['RoomName'].value;

            if (this.roomName.toString() !== this.EditRoomDetails[0]['roomName'].toString()) {
                this.queuedetailsservice.checkRoomExist(this.roomName).subscribe((result) => {
                    console.log(result);
                    console.log(result['exists']);
                    this.exists = result['exists'];
                    if (this.exists == true) {
                        this.snotifyService.error('Kindly ensure the roomName is unique since the room exists', 'Edit',
                            this.notificationService.getConfig());
                        console.log(this.EditRoomDetails);
                        this.spinner.hide();
                        return;

                    }
                    else {
                        this.EditRoomDetails[0]['roomName'] = this.formGroup.controls['RoomName'].value;
                        this.EditRoomDetails[0]['displayName'] = this.formGroup.controls['RoomDisplayName'].value;
                        this.EditRoomDetails[0]['description'] = this.formGroup.controls['Roomdescription'].value;
                        const createdby = parseInt(localStorage.getItem('appUserId'));
                        this.roomslist['roomName'] = this.formGroup.controls['RoomName'].value;
                        this.roomslist['displayName'] = this.formGroup.controls['RoomDisplayName'].value;
                        this.roomslist['description'] = this.formGroup.controls['Roomdescription'].value;
                        this.roomslist['updatedBy'] = createdby;
                        console.log(this.EditRoomDetails[0]['roomName']);
                        console.log(this.roomslist);
                        // tslint:disable-next-line: no-shadowed-variable
                        this.queuedetailsservice.editRoom(this.roomslist).subscribe((result) => {
                            if (result != null) {
                                this.snotifyService.success(result['message'], 'Save Room',
                                    this.notificationService.getConfig());
                                this.zone.run(() => {
                                    this.router.navigate(['/queue'],
                                        { relativeTo: this.activatedRoute });
                                });
                            }



                        },
                            (error) => {
                                this.snotifyService.error('Error  Editing  Room Items ' + error, 'Save Room',
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
                        this.snotifyService.error('Error  Editing  Room Items ' + error, 'Edit',
                            this.notificationService.getConfig());
                        this.spinner.hide();

                    },
                    () => {
                        this.spinner.hide();
                    }
                );
            } else {
                this.spinner.show();
                this.EditRoomDetails[0]['roomName'] = this.formGroup.controls['RoomName'].value;
                this.EditRoomDetails[0]['displayName'] = this.formGroup.controls['RoomDisplayName'].value;
                this.EditRoomDetails[0]['description'] = this.formGroup.controls['Roomdescription'].value;
                const createdby = parseInt(localStorage.getItem('appUserId'));
                this.roomslist['roomName'] = this.formGroup.controls['RoomName'].value;
                this.roomslist['displayName'] = this.formGroup.controls['RoomDisplayName'].value;
                this.roomslist['description'] = this.formGroup.controls['Roomdescription'].value;
                this.roomslist['updatedBy'] = createdby;

                this.queuedetailsservice.editRoom(this.roomslist).subscribe((result) => {
                    if (result != null) {
                        this.snotifyService.success(result['message'], 'Save Room',
                            this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/queue'],
                                { relativeTo: this.activatedRoute });
                        });
                    }



                },
                    (error) => {
                        this.snotifyService.error('Error  Editing  Room Items ' + error, 'Save Room',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );
                console.log(this.EditRoomDetails[0]['roomName']);
                console.log(this.roomslist);
            }




        }
        else {
            return;
            this.spinner.hide();
        }

    }


}

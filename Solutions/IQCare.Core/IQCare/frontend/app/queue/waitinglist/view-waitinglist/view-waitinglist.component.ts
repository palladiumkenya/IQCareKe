import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { QueueDetailsService } from '../../services/queue.service';
import { SearchService } from '../../../registration/_services/search.service';
import { DialogService } from '../../services/dialog.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AppEnum } from './../../../shared/reducers/app.enum';
import { AppStateService } from './../../../shared/_services/appstate.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../../shared/reducers/app.states';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatTableDataSource, MatButton, MatPaginator, MatSort } from '@angular/material';
import * as moment from 'moment';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import {
    Priority, WaitingList, ServiceRoomList, ServiceList, Person,
    serviceRoom, Roomlist, serviceAreas, servicePoint, PatientList, SpecificRoomLinkage, PatientWaitingList
} from '../../models/model';
@Component({
    selector: 'app-view-waitinglist',
    templateUrl: './view-waitinglist.component.html',
    styleUrls: ['./view-waitinglist.component.css']
})
export class ViewWaitinglistComponent implements OnInit {
    serviceAreas: serviceAreas[] = [];
    firstserviceArea: serviceAreas;
    QueueList: PatientWaitingList[] = [];
    roomitems: any[] = [];
    serviceAreaList: any[] = [];
    servicelistareas: ServiceList[] = [];
    Rooms: Roomlist[] = [];
    formGroup: FormGroup;
    waitingList: any[] = [];
    updated: boolean;
    person: Person;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns: any[] = ['FirstName', 'LastName', 'MiddleName', 'ServiceArea'
        , 'ServicePoint', 'RoomName', 'Priority', 'Action'];
    enrolledServices: any[];
    patientIdentifiers: any[];
    enrolledService: any[] = [];
    identifiers: any[] = [];
    userId: number;

    hasItems: boolean = false;
    configuration: boolean;
    dataSource = new MatTableDataSource(this.QueueList);
    constructor(private formBuilder: FormBuilder,
        private queuedetailsservice: QueueDetailsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        public zone: NgZone,
        private router: Router,
        private dialogService: DialogService,
        private spinner: NgxSpinnerService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private searchService: SearchService,
        private personService: PersonHomeService) {

    }

    ngOnInit() {
        this.getServiceAreaList();
        this.getWaitingList();
        this.getRoomsList();
        this.formGroup = this.formBuilder.group({
            RoomName: new FormControl('', [Validators.required])
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
    }
    getWaitingListByRoomId(id: number) {
        this.queuedetailsservice.getWaitingListByRoomId(id).subscribe
            ((result) => {
                this.waitingList = result['waitingListViews'];
                console.log(result);
                console.log(this.waitingList);
                this.QueueList = [];
                this.waitingList.forEach(x => {
                    this.QueueList.push({
                        FirstName: x.firstName,
                        MiddleName: x.middleName,
                        LastName: x.lastName,
                        Id: x.id,
                        ServiceAreaName: x.serviceAreaName,
                        ServicePointName: x.servicePointName,
                        RoomName: x.roomName,
                        Priority: x.priority,
                        CreatedBy: x.createdBy,
                        CreateDate: x.createDate,
                        PatientId: x.patientId,
                        PersonId: x.personId,
                        Status: x.status
                    });



                });
                console.log(this.QueueList);
                this.dataSource = new MatTableDataSource(this.QueueList);
                this.dataSource.paginator = this.paginator;
            },
                (error) => {
                    this.snotifyService.error('Error extracting the patient waiting list' + error, 'WaitingList',
                        this.notificationService.getConfig());
                },
                () => {
                });

    }
    getWaitingListbyServiceAreaId(id: number) {
        this.queuedetailsservice.getWaitingListByServiceAreaId(id).subscribe
            ((result) => {
                this.waitingList = result['waitingListViews'];
                console.log(result);
                console.log(this.waitingList);
                this.QueueList = [];
                this.waitingList.forEach(x => {
                    this.QueueList.push({
                        FirstName: x.firstName,
                        MiddleName: x.middleName,
                        LastName: x.lastName,
                        Id: x.id,
                        ServiceAreaName: x.serviceAreaName,
                        ServicePointName: x.servicePointName,
                        RoomName: x.roomName,
                        Priority: x.priority,
                        CreatedBy: x.createdBy,
                        CreateDate: x.createDate,
                        PatientId: x.patientId,
                        PersonId: x.personId,
                        Status: x.status
                    });



                });
                console.log(this.QueueList);
                this.dataSource = new MatTableDataSource(this.QueueList);
                this.dataSource.paginator = this.paginator;
            },
                (error) => {
                    this.snotifyService.error('Error extracting the patient waiting list' + error, 'WaitingList',
                        this.notificationService.getConfig());
                },
                () => {
                });
    }
    getWaitingList() {
        this.queuedetailsservice.getWaitingList().subscribe
            ((result) => {
                this.waitingList = result['waitingListViews'];
                console.log(result);
                console.log(this.waitingList);
                this.QueueList = [];
                this.waitingList.forEach(x => {
                    this.QueueList.push({
                        FirstName: x.firstName,
                        MiddleName: x.middleName,
                        LastName: x.lastName,
                        Id: x.id,
                        ServiceAreaName: x.serviceAreaName,
                        ServicePointName: x.servicePointName,
                        RoomName: x.roomName,
                        Priority: x.priority,
                        CreatedBy: x.createdBy,
                        CreateDate: x.createDate,
                        PatientId: x.patientId,
                        PersonId: x.personId,
                        Status: x.status
                    });



                });
                console.log(this.QueueList);
                this.dataSource = new MatTableDataSource(this.QueueList);
                this.dataSource.paginator = this.paginator;
            },
                (error) => {
                    this.snotifyService.error('Error extracting the patient waiting list' + error, 'WaitingList',
                        this.notificationService.getConfig());
                },
                () => {
                });
    }
    AddRoom() {
        this.zone.run(() => {
            this.router.navigate(['/queue/'],
                { relativeTo: this.route });
        });
    }
    checkRooms() {
        if (this.Rooms.length < 1) {
            this.configuration = true;
        } else {
            this.configuration = false;
        }
        return this.configuration;

    }
    getRoomsList() {
        this.queuedetailsservice.getRooms().subscribe((result) => {
            console.log(result);
            this.roomitems = result['roomsList'];
            this.roomitems.forEach(x => {
                this.Rooms.push({
                    id: x.id,
                    displayName: x.roomName
                });

            });

            console.log('//Rooms');
            console.log(this.Rooms);

        },

            (error) => {
                this.snotifyService.error('Error extractiong the room list' + error, 'List',
                    this.notificationService.getConfig());
            },
            () => {
            });

    }
    getServiceAreaList() {
        this.queuedetailsservice.getServiceAreas().subscribe((result) => {

            this.serviceAreaList = result['serviceAreas'];

            this.serviceAreaList.forEach(element => {
                this.serviceAreas.push({
                    id: element.id,
                    displayName: element.displayName
                });
            });

            this.serviceAreaList.forEach(element => {
                this.servicelistareas.push({
                    id: element.id,
                    displayName: element.displayName,
                    code: element.code
                });
            });

        },

            (error) => {
                this.snotifyService.error('Error extractiong the service area list' + error, 'List',
                    this.notificationService.getConfig());
            },
            () => {
            });
    }
    onServiceAreaChange() {
        const area = this.formGroup.controls.ServiceArea.value;
        if (area > 0) {
            this.getWaitingListbyServiceAreaId(area);

        } else {
            this.getWaitingList();
        }

    }

    OnRoomChange() {
        const area = this.formGroup.controls.RoomName.value;
        if (area > 0) {
            this.getWaitingListByRoomId(area);

        } else {
            this.getWaitingList();
        }
    }
    Serve(Id: number) {

        this.dialogService.openConfirmDialog('Kindly confirm you want to serve the patient?')
            .afterClosed().subscribe(res => {
                if (res) {
                    const updatedby = parseInt(localStorage.getItem('appUserId'), 10);
                    const queueIndividual = this.QueueList.find(x => x.Id == Id);

                    this.queuedetailsservice.editQueue(queueIndividual.Id, false
                        , true, updatedby).subscribe((result) => {
                            this.updated = result['updated'];
                            if (this.updated == true) {
                                this.snotifyService.success('Kindly note the item in the '
                                    + '  waiting has been updated', 'WaitingList',
                                    this.notificationService.getConfig());

                                const selectedService = this.servicelistareas.find(obj => obj.displayName
                                    == queueIndividual.ServiceAreaName);

                                this.isPersonServiceEnrolled(selectedService.id, queueIndividual.PersonId, selectedService.code);



                            }

                        },
                            (err) => {
                                this.snotifyService.error('Error serving  patient ' + err, 'Serve',
                                    this.notificationService.getConfig());
                            },
                            () => {
                                // console.log(this.personView$);
                            });



                }
            });

    }

    addWaitingList(row: any) {
        const personId = row['id'];
        const patientId = row['patientId'];
        this.zone.run(() => { this.router.navigate(['/queue/addWaitingList/' + patientId + '/' + personId], { relativeTo: this.route }); });


    }

    public getPatientDetailsById(personId: number) {
        this.personService.getPatientByPersonId(personId).subscribe(
            p => {
                console.log(p);
                this.person = new Person();
                this.person = p;

                localStorage.setItem('personId', this.person.personId.toString());
                this.store.dispatch(new Consent.PersonId(this.person.personId));

                if (this.person.patientId && this.person.patientId > 0) {
                    this.store.dispatch(new Consent.PatientId(this.person.patientId));
                    localStorage.setItem('patientId', this.person.patientId.toString());
                }
            },
            (err) => {
                this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }


    getPersonEnrolledServices(personId: number) {
        this.enrolledService = [];
        this.patientIdentifiers = [];
        this.identifiers = [];
        this.enrolledServices = [];
        let patientId: number;
        this.personService.getPersonEnrolledServices(personId).subscribe((res) => {
            this.enrolledServices = res['personEnrollmentList'];
            console.log('EnrolledServices');
            console.log(this.enrolledServices);
            if (this.enrolledServices && this.enrolledServices.length > 0) {
                patientId = this.enrolledServices[0]['patientId'];
            }
            this.patientIdentifiers = res['patientIdentifiers'];
            this.identifiers = res['identifiers'];
        });
    }

    isPersonServiceEnrolled(serviceId: number, personid: number, code: string) {

        this.enrolledService = [];
        this.patientIdentifiers = [];
        this.identifiers = [];
        this.enrolledServices = [];
        let patientId: number;
        let returnValue = false;
        this.personService.getPersonEnrolledServices(personid).subscribe((res) => {
            this.enrolledServices = res['personEnrollmentList'];
            console.log('EnrolledServices');
            console.log(this.enrolledServices);
            if (this.enrolledServices && this.enrolledServices.length > 0) {
                patientId = this.enrolledServices[0]['patientId'];
            }
            if (this.enrolledServices && this.enrolledServices.length > 0) {

                for (let i = 0; i < this.enrolledServices.length; i++) {
                    if (this.enrolledServices[i].serviceAreaId == serviceId) {
                        returnValue = true;

                    }
                }
                if (returnValue == true) {
                    this.newEncounter(serviceId, personid, patientId);
                } else {
                    this.getPatientDetailsById(personid);
                    let iseligible: Boolean;
                    iseligible = this.isServiceEligible(serviceId);
                    if (iseligible == true) {
                        this.enrollToService(serviceId, code, personid);
                    } else {
                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' +
                                personid], { relativeTo: this.route });
                        });
                    }
                }
            } else {

                this.zone.run(() => {
                    this.router.navigate(['/dashboard/personhome/' +
                        personid], { relativeTo: this.route });
                });

            }

        });

    }




    isServiceEligible(serviceAreaId: number) {
        let isCCCEnrolled;

        if (this.enrolledServices) {
            isCCCEnrolled = this.enrolledServices.filter(obj => obj.serviceAreaId == 1);
        }

        const selectedService = this.servicelistareas.filter(obj => obj.id == serviceAreaId);
        let isEligible: boolean = false;
        if (selectedService && selectedService.length > 0) {
            switch (selectedService[0]['code']) {
                case 'ANC':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'PNC':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'Maternity':
                    if (this.person.gender == 'Female'
                        && ((this.person.dateOfBirth) && this.person.ageNumber >= 9 && this.person.ageNumber <= 49)) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'HEI':
                    if (this.person.dateOfBirth && this.person.ageNumber <= 2) {
                        isEligible = true;
                    } else {
                        isEligible = false;
                    }
                    break;
                case 'HTS':
                    if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                        isEligible = false;
                    } else {
                        isEligible = true;
                    }
                    break;
                case 'CCC':
                    isEligible = true;
                    break;
            }
        }
        return isEligible;
    }


    newEncounter(serviceId: number, personId: number, patientid: number) {
        const selectedService = this.servicelistareas.filter(obj => obj.id == serviceId);
        if (selectedService && selectedService.length > 0) {
            const service = selectedService[0]['code'];
            localStorage.setItem('selectedService', service.toLowerCase());

            this.store.dispatch(new Consent.SelectedService(service.toLowerCase()));
            this.appStateService.addAppState(AppEnum.PATIENTID, personId, patientid).subscribe();

            switch (selectedService[0]['code']) {
                case 'HTS':
                    localStorage.removeItem('personId');
                    localStorage.removeItem('patientId');
                    localStorage.removeItem('partnerId');
                    localStorage.removeItem('htsEncounterId');
                    localStorage.removeItem('patientMasterVisitId');
                    localStorage.removeItem('isPartner');
                    localStorage.removeItem('editEncounterId');

                    this.searchService.lastHtsEncounter(personId).subscribe((res) => {
                        if (res['encounterId']) {
                            localStorage.setItem('htsEncounterId', res['encounterId']);
                        }
                        if (res['patientMasterVisitId'] > 0) {
                            localStorage.setItem('patientMasterVisitId', res['patientMasterVisitId']);
                        }

                        this.zone.run(() => {
                            localStorage.setItem('personId', personId.toString());
                            localStorage.setItem('patientId', patientid.toString());
                            localStorage.setItem('serviceAreaId', serviceId.toString());
                            this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                        });
                    });
                    break;
                case 'CCC':
                    this.searchService.setSession(personId, patientid).subscribe((res) => {
                        window.location.href = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                            '/IQCare/CCC/Patient/PatientHome.aspx';
                    });
                    break;
                default:
                    this.zone.run(() => {
                        this.router.navigate(
                            ['/pmtct/patient-encounter/' + patientid + '/' + personId + '/' + serviceId + '/'
                                + selectedService[0]['code']],
                            { relativeTo: this.route });
                    });
                    break;
            }
        }
    }



    enrollToService(serviceId: number, serviceCode: string, personId: number) {
        if (serviceId == 1) {
            this.snotifyService.error('Please Access CCC from the Greencard menu', 'Encounter History',
                this.notificationService.getConfig());
            return;
        }
        this.zone.run(() => {
            this.router.navigate(['/dashboard/enrollment/' + personId + '/' + serviceId + '/' + serviceCode],
                { relativeTo: this.route });
        });
    }

}

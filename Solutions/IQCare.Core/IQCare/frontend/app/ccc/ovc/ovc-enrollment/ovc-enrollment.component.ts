import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { OvcService } from '../../_services/ovc.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrollmentService } from '../../../registration/_services/enrollment.service';
import { Enrollment } from '../../../registration/_models/enrollment';
import { MatDialog, MatDialogConfig, MatPaginator } from '@angular/material';
import { InlineSearchComponent } from '../../../records/inline-search/inline-search.component';
import { RecordsService } from '../../../records/_services/records.service';
import { Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';
import * as Consent from '../../../shared/reducers/app.states';
import { AppEnum } from '../../../shared/reducers/app.enum';
import { NgxSpinnerService } from 'ngx-spinner';
import { AppStateService } from '../../../shared/_services/appstate.service';
import { PersonHomeService } from './../../../dashboard/services/person-home.service';
import { PncContraceptivehistoryComponent } from '../../../pmtct/pnc/pnc-contraceptivehistory/pnc-contraceptivehistory.component';
import { Store } from '@ngrx/store';
import * as moment from 'moment';


@Component({
    selector: 'app-ovc-enrollment',
    templateUrl: './ovc-enrollment.component.html',
    styleUrls: ['./ovc-enrollment.component.css'],
    providers: [EnrollmentService, PersonHomeService]
})
export class OvcEnrollmentComponent implements OnInit {
    OvcEnrollmentForm: FormGroup;
    yesNoOptions: LookupItemView[] = [];
    gender: LookupItemView[];
    findRegistered: boolean = false;
    registerContact: boolean = false;
    caregiveoptions: LookupItemView[] = [];
    maxDate: Date;
    serviceAreaIdentifiers: any[] = [];
    identifiers: any[] = [];

    dataOutCome: any[];
    @ViewChild(MatPaginator) paginator: MatPaginator;

    CPMISIdentifierId: number;
    OVCIdentifierId: number;
    contactPersonId: number;
    contactAge: number;
    personId: number;
    patientId: number;
    serviceCode: string;
    serviceId: number;
    userId: number;
    edit: number = 0;
    OVCNumber: string;
    displayedColumns = ['firstName', 'midName', 'lastName', 'gender', 'relationshipType'];
    dataSource = new FamilyDataSource(this.ovcService, this.patientId);
    public phonePattern = /^(?:\+254|0|254)(\d{9})$/;
    constructor(private _formBuilder: FormBuilder,
        private ovcService: OvcService,
        private dialog: MatDialog,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private route: ActivatedRoute,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private enrollmentService: EnrollmentService,
        private recordsService: RecordsService,
        private spinner: NgxSpinnerService,
        public zone: NgZone,
        private router: Router,
        private personHomeService: PersonHomeService) { }

    async  ngOnInit() {


        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.route.params.subscribe(
            p => {
                const { personId, patientId, serviceCode, serviceId, edit } = p;
                this.personId = personId;
                this.serviceCode = serviceCode;
                this.serviceId = serviceId;
                this.patientId = patientId;
                this.edit = edit;

                this.dataSource = new FamilyDataSource(this.ovcService, this.patientId);

            }
        );

        this.OvcEnrollmentForm = this._formBuilder.group({
            enrollmentDate: new FormControl('', [Validators.required]),
            isCaregiverEnrolled: new FormControl(''),


            CPMISEnrolled: new FormControl('', [Validators.required]),
            CPMISNumber: new FormControl(''),
            PartnerOVCServices: new FormControl('')

        });

        const yesnoOptions = await this.ovcService.getByGroupName('YesNo').toPromise();
        this.yesNoOptions = yesnoOptions['lookupItems'];
        const sexoptions = await this.ovcService.getByGroupMasterName('Gender').toPromise();
        this.gender = sexoptions['lookupItems'];
        const caregiverlist = await this.ovcService.getByGroupMasterName('CaregiverRelationship').toPromise();
        this.caregiveoptions = caregiverlist['lookupItems'];


        this.OvcEnrollmentForm.controls.CPMISNumber.disable({ onlySelf: true });
    
        if (this.edit != undefined && this.edit == 1) {
            this.ovcService.getEnrollOVCInformation(this.personId).subscribe((res) => {
                if (res != null) {
                    console.log(res['id']);
                    this.OvcEnrollmentForm.controls.PartnerOVCServices.setValue(res['partnerOVCServices']);
                    this.OvcEnrollmentForm.controls.CPMISEnrolled.setValue(res['cpmisEnrolled']);

                }
            });

            this.loadEnrollmentDate(this.patientId);
           
        }


        await this.personHomeService.getServiceAreaIdentifiers(this.serviceId).subscribe(
            (res) => {
                const { identifiers, serviceAreaIdentifiers } = res;
                this.serviceAreaIdentifiers = serviceAreaIdentifiers;
                this.identifiers = identifiers;
                serviceAreaIdentifiers.forEach(element => {
                    identifiers.forEach(identifier_element => {
                        if (identifier_element['id'] == element['identifierId']) {
                            if (element.requiredFlag) {
                                if (identifier_element['code'] == 'OVCNumber') {
                                    this.OVCIdentifierId = identifier_element['id'];


                                } else if (identifier_element['code'] == 'CPMISNumber') {
                                    this.CPMISIdentifierId = identifier_element['id'];
                                }
                            }
                        }
                    });
                });
                if (this.edit != undefined && this.edit == 1) { 

                    this.loadIdentifiers(this.patientId);
                }

                // console.log(serviceAreaIdentifiers);
            }
        );


      

        console.log(this.OvcEnrollmentForm.controls);



    }

    loadIdentifiers(patientId: number): void {
        this.recordsService.getPatientIdentifiersList(patientId).subscribe(
            (result) => {
                if (result.length > 0) {
                    this.identifiers.forEach(element => {
                        result.forEach(patientIdentifiers => {
                            if (patientIdentifiers.identifierTypeId == element.id) {
                                if (element.code == 'CPMISNumber') {
                                    this.OvcEnrollmentForm.controls.CPMISNumber.setValue(patientIdentifiers.identifierValue.toString());
                                } else if (element.code == 'OVCNumber') {
                                    this.OVCNumber = patientIdentifiers.IdentifierValue;
                                }

                            }
                        });
                    });

                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadEnrollmentDate(patientId: any): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, this.serviceId).subscribe(
            (result) => {
                // console.log(result);
                this.OvcEnrollmentForm.controls.enrollmentDate.setValue(result.enrollmentDate);
            },
            (error) => {
                console.log(error);
            }
        );
    }




    OnisCaregiverEnrolled(event) {
        if (event.source.selected && event.source.viewValue == 'Yes') {
            this.findRegistered = true;
            this.registerContact = false;
        } else if (event.source.selected && event.source.viewValue == 'No') {
            this.findRegistered = false;
            this.registerContact = true;
        }

    }
    searchPatient() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/familysearch/' +
                this.personId + '/' + this.patientId + '/' + this.serviceId + '/' + this.serviceCode]);
        });

    }
    registerNewContact() {

        this.zone.run(() => {
            this.router.navigate(['/ccc/registernew/' +
                this.personId + '/' + this.patientId + '/' + this.serviceId + '/' + this.serviceCode]);
        });

    }
    addRow() {

    }
    close() {
        this.zone.run(() => {
            this.router.navigate(
                ['/dashboard/personhome/' + this.personId],
                { relativeTo: this.route });
        });
    }
    validate() {

        console.log(this.OVCIdentifierId);
        console.log(this.CPMISIdentifierId);
        if (this.OvcEnrollmentForm.valid) {

            this.spinner.show();
            const { enrollmentDate, CPMISEnrolled, CPMISNumber, PartnerOVCServices } = this.OvcEnrollmentForm.value;
            const enrollment = new Enrollment();
            const enrollmentNo = Math.random().toString(36).slice(5);
            enrollment.CreatedBy = this.userId;
            enrollment.DateOfEnrollment = enrollmentDate;
            enrollment.EnrollmentNo = enrollmentNo;
            enrollment.PatientId = this.patientId;
            enrollment.PersonId = this.personId;
            enrollment.PosId = localStorage.getItem('appPosID');
            enrollment.RegistrationDate = enrollmentDate;
            enrollment.ServiceAreaId = this.serviceId;
            enrollment.transferIn = true;

            if (this.edit != undefined && this.edit == 1) {
                enrollment.ServiceIdentifiersList.push({
                    'IdentifierId': this.OVCIdentifierId,
                    'IdentifierValue': this.OVCNumber
                });

            } else {
                enrollment.ServiceIdentifiersList.push({
                    'IdentifierId': this.OVCIdentifierId,
                    'IdentifierValue': enrollmentNo
                });
            }
            if (CPMISNumber && CPMISNumber != undefined && CPMISNumber !== '') {
                enrollment.ServiceIdentifiersList.push({
                    'IdentifierId': this.CPMISIdentifierId,
                    'IdentifierValue': CPMISNumber
                });
            }

            try {
                this.enrollmentService.enrollClient(enrollment).subscribe((res) => {
                    this.snotifyService.success('Successfully enrolled the client', 'Enroll Client',
                        this.notificationService.getConfig());
                    localStorage.setItem('selectedService', this.serviceCode.toLowerCase());

                    this.store.dispatch(new Consent.SelectedService(this.serviceCode.toLowerCase()));

                    this.store.dispatch(new Consent.PatientId(this.patientId));
                    this.appStateService.addAppState(AppEnum.PATIENTID, this.personId,
                        this.patientId).subscribe();

                }, (error) => {
                    this.snotifyService.error('Error enrolling client' + error, 'Enroll Cliennt',
                        this.notificationService.getConfig());
                    this.spinner.hide();

                }, () => {
                    this.spinner.hide();
                });

                this.ovcService.enrollOVC(this.personId, PartnerOVCServices,
                    CPMISEnrolled, enrollmentDate, this.userId).subscribe((res) => {

                        this.snotifyService.success('Successfully enrolled for OVC Services ', 'Enroll OVC Service',
                            this.notificationService.getConfig());
                    }, (error) => {
                        this.snotifyService.error('Error enrolling to  ovc services ' + error, 'Enroll OVC Services',
                            this.notificationService.getConfig());
                        this.spinner.hide();

                    }, () => {
                        this.spinner.hide();
                    });

                this.spinner.hide();
                this.zone.run(() => {
                    this.router.navigate(['/ccc/ovcFormList/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                        { relativeTo: this.route });
                });
            } catch (e) {
                console.log(e);
            }

        }

    }

    onCPMISEnrolled(event) {
        if (event.source.selected && event.source.viewValue == 'Yes') {
            this.OvcEnrollmentForm.controls.CPMISNumber.enable({ onlySelf: true });
        } else if (event.source.selected && event.source.viewValue == 'No') {
            this.OvcEnrollmentForm.controls.CPMISNumber.disable({ onlySelf: true });
        }
    }

}


export class FamilyDataSource extends DataSource<any> {
    constructor(private ovcService: OvcService, private patientId: number) {
        super();
    }

    connect(): Observable<any[]> {
        return this.ovcService.getClientFamily(this.patientId);
    }

    disconnect() {

    }
}

